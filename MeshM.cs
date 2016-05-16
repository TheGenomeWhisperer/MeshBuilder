
    
/* Author:      Sklug a.k.a TheGenomeWhisperer
|       	    The following program is to fill a hole of the ReBot devs, so I can build my own custom mesh pathing when they are incomplete
|
| NOTE:         These will be inherited in the ReBot.ReBotAPI Class.
| Final Note:   Class does not need Static Main as it will be injected into the Rebot.exe through the "Editor"
|               
|               Full Information on actual use in live profiles: http://www.rebot.to/showthread.php?t=4930
|               MeshBuilder will be shorted for ease of building to Mesh
|               Note: Mesh for Mesh Manager
|               Note: The very last coordinate of all the Vector3 arrays is a "special" node that is built on the in-game Mesh
|
|               Last Update: May 4th, 2016
|
*/  
	
public class MeshM
{
    public static ReBotAPI API;
    public static Fiber<int> Fib;
    
    public static int currentContinent;
    public static Vector3[] currentMesh;
    public static bool PlayerIsHorde;

    // Empty Constructor
    public MeshM() {}
    
    public static void Initialize()
    {
        currentContinent = API.Me.ContinentID;
        PlayerIsHorde = API.Me.IsHorde;
    }
    
    public static Vector3[] GetMesh(int currentCont, bool factionIsHorde, Vector3 destination)
    {
        // Draenor ContinentID
        // All Zone IDs for Continent!
        
        // Dungeon Mesh Checking
        if (QH.IsInInstance())
        {
            return DungeonM.GetDungeonMesh(API.Me.ZoneId);
        }
        
        // If in Draenor
        if (currentCont == 1116)
        {
            return WoDM.GetDraenorMesh(destination,factionIsHorde);
        }

        // No matches
        API.Print("No Custom Meshes Have Been Found at Your Location.  Please submit a request in the forums!"); 
        Vector3[] result = new Vector3[]{};
        return result;
    }
    
        
    public static void SetCorrectMesh(Vector3 destination)
    {
        if (API.Me.ContinentID != currentContinent)
        {
            currentContinent = API.Me.ContinentID;
            API.Print("Player Has Changed Continents... Calculating Route.");
        }
        currentMesh = GetMesh(currentContinent,PlayerIsHorde,destination);
    }
        
        
    // // Method:              "GetClosestNode(Vector3[] hotspots, Vector3 destination)"
    // // This is to be used with the "GetMesh" method
    public static Vector3 GetClosestNode(Vector3[] hotspots, Vector3 destination)
    {
        Vector3 closest = hotspots[0];
        for (int i = 1; i < hotspots.Length - 1; i++)
        {
            if (hotspots[i].Distance2D(destination) < closest.Distance2D(destination))
            {
                closest = hotspots[i];
            }
        }
        return closest;
    }
 

    // Method:          "Navigate(Vector3 destination)"
    public static IEnumerable<int> Navigate(Vector3 destination)
    {   
        // Recheck if VM interruption or something.
        bool reCheck = false;
        Vector3 closestNode;
        Vector3 bridgeNode;
        
        API.Print("Calculating Hotspots for Custom Navigation System...");
                // Why waste time parsing through them all if I am already right there?
        if (API.Me.Distance2DTo(destination) > 3f)
        {
            SetCorrectMesh(destination);
            currentMesh = GetDestinationMesh(currentMesh,destination);
            closestNode = GetClosestNode(currentMesh,destination);
            bridgeNode = currentMesh[currentMesh.Length - 1];
            
            
            // error check Vector3 no-position
            if (currentMesh.Length > 0 && API.Me.Distance2DTo(closestNode) > 500f)
            {
                API.Print("Wow, Player Destination is Quite Far! Re-Calculating More Complex Route...");
                if (IsAnyMeshClose(20f))
                {
                    API.Print("Moving off Current Mesh");
                    
                    Vector3 characterPosition2 = API.Me.Position;
                    currentMesh = GetDestinationMesh(currentMesh,characterPosition2); // Returns the mesh to the one closest to the player now.
                    Vector3[] route3 = GetFastestRoute(currentMesh[currentMesh.Length - 1]);
                    
                    for (int i = 0; i < route3.Length - 1; i++)
                    {
                        while(!API.CTM(route3[i]) && API.Me.Distance2DTo(route3[i]) > 2f)
                        {
                            yield return 100;
                        }
                    }
                    // Arriving at special bridge node.
                    while (!API.CTM(currentMesh[currentMesh.Length - 1]) && API.Me.Distance2DTo(currentMesh[currentMesh.Length - 1]) > 2f)
                    {
                        yield return 100;
                    }
                    API.Print("Activing Mesh Bridge Logic...");
                }
                var check = new Fiber<int>(Flight.FlyTo(Flight.GetClosestFlightToDestination(destination)));
                while (check.Run())
                {
                    yield return 100;
                }
                // reset Values
                closestNode = GetClosestNode(currentMesh,destination);
            }
            
            // Player is far away from custom mesh.. possible Stuck issues could occur. Issue warning!
            if (API.Me.Distance2DTo(closestNode) >= 15f && !QH.IsInInstance())
            {
                API.Print("Warning! Player is " + (int)API.Me.Distance2DTo(closestNode) + " Yards From the Custom Mesh! Very Far! Dangerous to Navigate!");
                API.Print("Attempting to Navigate to Edge of Mesh...");
                
                // Checking if I need to do special pathing off a diff. mesh
                if (IsAnyMeshClose(20f))
                {
                    API.Print("Moving off Current Mesh");
                    
                    Vector3 characterPosition = API.Me.Position;
                    currentMesh = GetDestinationMesh(currentMesh,characterPosition); // Returns the mesh to the one closest to the player now.
                    Vector3[] route2 = GetFastestRoute(currentMesh[currentMesh.Length - 1]);
                    
                    for (int i = 0; i < route2.Length - 1; i++)
                    {
                        while(!API.CTM(route2[i]) && API.Me.Distance2DTo(route2[i]) > 2f)
                        {
                            yield return 100;
                        }
                    }
                    // Arriving at special bridge node.
                    while (!API.CTM(currentMesh[currentMesh.Length - 1]) && API.Me.Distance2DTo(currentMesh[currentMesh.Length - 1]) > 2f)
                    {
                        yield return 100;
                    }
                    API.Print("Activing Mesh Bridge Logic...");
                }
                // Finale MoveTo to special node at edge of mesh.
                while(!API.MoveTo(bridgeNode))
                {
                    yield return 250;
                }
                API.Print("Activing Mesh Bridge Logic...");
            }
            
            SetCorrectMesh(destination);
            currentMesh = GetDestinationMesh(currentMesh,destination);
            Vector3[] result = GetFastestRoute(destination);
            
            API.Print("Taking " + result.Length + " Nodes to Get to Your Destination!");
            // Navigating through CTM normally.
            
            for (int i = 0; i < result.Length - 1; i++)
            {
                while(!API.CTM(result[i]) && API.Me.Distance2DTo(result[i]) > 2f)
                {
                    yield return 100;
                }
            }
        }
        while(!API.CTM(destination) && API.Me.Distance2DTo(destination) > 2f)
        {
            yield return 250;
        }
        yield break;
    }
    
    // Method:              "GetFastestRoute(Vector3 destination")
    // What it Does         In case of a looped pathway, this will determine which one is shorter.  This is critical for mesh, but 
    //                      not useful in a straight line.
    public static Vector3[] GetFastestRoute(Vector3 destination)
    {   
        return CreateStraightPath(currentMesh, destination);
    }
    
    private static Vector3[] CreateStraightPath(Vector3[] hotspots, Vector3 destination)
    {
        int closestMeshNode = GetMeshIndex(hotspots, API.Me.Position);
        int finalMeshNode = GetMeshIndex(hotspots, destination);
        Vector3[] result;
        int count = 0;

        // No mesh found in this area, or it is far away, look into Special Navigation if > 500 yards.
        if (finalMeshNode == -1 || API.Me.Distance2DTo(hotspots[finalMeshNode]) > 500f)
        {
            result = new Vector3[] {new Vector3(0f,0f,0f)};
            return result; 
        }
        
        if (closestMeshNode < finalMeshNode)
        {
            result = new Vector3[finalMeshNode - closestMeshNode + 1];
            for (int i = closestMeshNode; i <= finalMeshNode; i++)
            {
                result[count] = hotspots[i];
                count++;
            }
        }
        else
        {
            result = new Vector3[closestMeshNode - finalMeshNode + 1];
            for (int i = closestMeshNode; i >= finalMeshNode; i--)
            {
                result[count] = hotspots[i];
                count++;
            }
        }
        return result;
    }
    
    // Method:          "GetMeshIndex(Vector3[] hotspots, Vector3 closestPosition)"
    // What it Does:    Determines the position within the mesh array
    public static int GetMeshIndex(Vector3[] hotspots, Vector3 closestPosition)
    {
        if (hotspots.Length == 0)
        {
            return -1;
        }
        int index = 0;
        Vector3 closestMeshNode = hotspots[0];
        for (int i = 1; i < hotspots.Length - 1; i++)
        {
            if (hotspots[i].Distance2D(closestPosition) < closestMeshNode.Distance2D(closestPosition))
            {
                closestMeshNode = hotspots[i];
                index = i;
            }
        }
        return index;
    }
    
    public static bool IsDestinationOnCurrentMesh(Vector3[] hotspots, int closestMeshNode,int finalMeshNode)
    {
        Vector3 breakNode = new Vector3(0f,0f,0f);
        bool result = true;
        
        if (closestMeshNode < finalMeshNode)
        {
            // Forward through array.
            for (int i = closestMeshNode; i <= finalMeshNode; i++)
            {
                if (hotspots[i] == breakNode)
                {
                    result = false;
                    break;
                }
            }
        }
        else
        {
            // Backwards through array.
            for (int i = closestMeshNode; i >= finalMeshNode; i--)
            {
                if (hotspots[i] == breakNode)
                {
                    result = false;
                    break;
                }
            }
        }
        return result;
    }
    
    public static Vector3[] GetDestinationMesh(Vector3[] hotspots, Vector3 destination)
    {
        Vector3[] result;
        Vector3 defaultNode = new Vector3(0f,0f,0f);
        // Escape out if no mesh.
        if (hotspots.Length == 0)
        {
            result = new Vector3[]{defaultNode};
            return result;
        }
        
        // Find the closest node to the destination.
        int index = 0;
        Vector3 closestMeshNode = hotspots[0];
        for (int i = 1; i < hotspots.Length - 1; i++)
        {
            if (hotspots[i] != defaultNode && hotspots[i+1] != defaultNode && hotspots[i].Distance2D(destination) < closestMeshNode.Distance2D(destination))
            {
                closestMeshNode = hotspots[i];
                index = i;
            }
        }
        
        // Now, finding the range of the Destination mesh of the array.
        int topIndex = hotspots.Length - 2;  // one back, plus one more back to skip the bridgeNode
        int lowIndex = 0;
        for (int i = index; i < hotspots.Length; i++)
        {
            if (hotspots[i] == defaultNode)
            {
                topIndex = i - 1;
                break;
            }
        }
        for (int i = index; i >= 0; i--)
        {
            if (hotspots[i] == defaultNode)
            {
                lowIndex = i + 1;
            }
        }
        
        // Now, let's build the new mesh
        result = new Vector3[topIndex - lowIndex + 1];
        int count = 0;
        for (int i = lowIndex; i <= topIndex; i++)
        {
            result[count] = hotspots[i];
            count++;
        }
        // Finish!
        
        return result;
    }
    
    
    
    
    // Method:              "IsAnyMeshClose(float withinHowManyYards)"
    // Purpose:             To determine if player is on a mesh
    private static bool IsAnyMeshClose(float withinHowManyYards)
    {
        SetCorrectMesh(API.Me.Position);
        bool result = false;

        for (int i = 0; i < currentMesh.Length - 1; i++)
        {
            if (API.Me.Distance2DTo(currentMesh[i]) < withinHowManyYards)
            {
                result = true;
                break;
            }
        }
        return result;
    }
    
    // Method:          "InsertionSort(Vector3[] hotspots)"
    // What it does:       "Places all hotspots in direct point A to B line in order of distance from the player.  Will be rarely used.
    public static Vector3[] InsertionSortHotspots(Vector3[] hotspots)
    {
        Vector3 temp;
        for (int i = 0; i < hotspots.Length - 1; i++)
        {
            for (int j = i; j > 0; j--)
            {
                if (API.Me.Distance2DTo(hotspots[j-1]) > API.Me.Distance2DTo(hotspots[j]))
                {
                    temp = hotspots[j-1];
                    hotspots[j-1] = hotspots[j];
                    hotspots[j] = temp;
                }
            }
        }
        return hotspots;
    }
    
    // Method:          "InsertionSort(Vector3[] hotspots)"
    // What it does:     "Places all hotspots in direct point A to B line in order of distance from a given position
    //                   To be mainly used with the GetMesh method.
    public static Vector3[] InsertionSortHotspots(Vector3[] hotspots, Vector3 destination)
    {
        Vector3 temp;
        for (int i = 0; i < hotspots.Length - 1; i++)
        {
            for (int j = i; j > 0; j--)
            {
                if (destination.Distance2D(hotspots[j-1]) > destination.Distance2D(hotspots[j]))
                {
                    temp = hotspots[j-1];
                    hotspots[j-1] = hotspots[j];
                    hotspots[j] = temp;
                }
            }
        }
        return hotspots;
    }
    
    // Determines if the player is standing on the mesh. 
    public static bool IsDestinationMeshClose(float withinHowManyYards, Vector3 destination)
    {
        Vector3 defaultNode = new Vector3(0f,0f,0f);
        SetCorrectMesh(destination);
        bool result = false;
        Vector3[] route;
        
        // A quick check to avoid unnecessary work if already close to destination
        if (API.Me.Distance2DTo(destination) > withinHowManyYards)
        {
            route = GetDestinationMesh(currentMesh, destination);
            // A Quick escape if needed... this means no meshes were even found to check against or they were > 500 yards.
            if (route[0] == defaultNode)
            {
                return false;
            }
             
            for (int i = 0; i < route.Length - 1; i++)
            {
                if (API.Me.Distance2DTo(route[i]) <= withinHowManyYards)
                {
                    result = true;
                    break;
                }
            }
        }
        else
        {
            return true;
        }
        return result;
    }
    
    
    // // Method:             "HotspotGenerator(int seconds)"
    // // What it Does:        Run this method whilst running a path in the game to build the actual mesh.
    public static IEnumerable<int> HotspotGenerator(int seconds)
    {
        API.DisableCombat = true;
        string result = ("Vector3[] hotspots = new Vector3[]{{");
        int count = seconds * 2;
        int index = 0;
        Vector3 currentPosition = API.Me.Position;
        Vector3 tempPosition = currentPosition;

        while (index < count)
        {
            // Verifying Current Position has changed from the last one.
            tempPosition = API.Me.Position;
            if (tempPosition != currentPosition)
            {
                result += ("new Vector3(" + API.Me.Position.X + "f, " + API.Me.Position.Y + "f, " + API.Me.Position.Z + "f),");
                currentPosition = tempPosition;
            }
            yield return 500;
            index++;
            if (index % 6 == 0)
            {
                if (!result.Equals("Vector3[] hotspots = new Vector3[]{{"))
                {
                    API.Print(result.Substring(0, result.Length - 1) + "}};");
                }
                else
                {
                    API.Print("No Hotzones Added Yet.  You may want to start moving!");
                }
            }
        }
        if (!result.Equals("Vector3[] hotspots = new Vector3[]{{"))
        {
            API.Print(result.Substring(0, result.Length - 1) + "}};");
        }
        else
        {
            API.Print("No Hotzones Were Added.  Please Move on a path in-game and the hotspot generator will build the mesh.");
        }
        API.DisableCombat = false;
        yield break;
    }

}



































        
    // // Method:          "SortHotspots(Vector3[] hotspots, Vector3 destination)"
    // private static Vector3[] CreatePath(Vector3[] hotspots, Vector3 destination)
    // {
    //     // Error Checking
    //     if (hotspots.Length == 0)
    //     {
    //         return hotspots;
    //     }
               
    //     // Variable initializations
    //     List<Vector3> finalResult = new List<Vector3>();
    //     List<Vector3> hotspotList = new List<Vector3>(hotspots);
    //     Vector3 position = API.Me.Position;
    //     Vector3 closest;
    //     bool NotDoneSorting = true;
    //     int index = 0;
    //     int count = 0;
    //     bool noReset = true;
    //     // Determining which hotspot is closest to destination.
    //     Vector3 final = GetClosestNode(hotspots,destination);

    //     while(NotDoneSorting)
    //     {
    //         // closest to current node (first will be closest to player)
    //         closest = hotspotList[count];
    //         for (int i = count; i < hotspotList.Count - 1; i++)
    //         {
    //             if (position.Distance2D(hotspotList[i]) < position.Distance2D(closest))
    //             {
    //                 closest = hotspotList[i];
    //                 index = i;
    //             }
    //         }
            
    //         if (closest.Distance2D(position) > 15f && count != 0 && closest != final)
    //         {
    //             // Removing Previous hotspot, then starting over.
    //             hotspotList.RemoveAt(count - 1);
    //             finalResult.Clear();
    //             position = API.Me.Position;
    //             index = 0;
    //             count = 0;
    //             noReset = false;
    //         }

    //         if (noReset)
    //         {
    //             // Now, shift everything below the next closest hotspot to the right;
    //             for (int j = index; j > count; j--)
    //             {
    //                 hotspotList[j] = hotspotList[j-1];
    //             }
    //             hotspotList[count] = closest;
    //             finalResult.Add(closest);
    //             index = 0;
    //             count++;
    //             position = closest;
    //         }
            
    //         // Determining if we reached the last hotspot;
    //         if ((closest == final || closest.Distance2D(destination) <  7f) && noReset)
    //         {
    //             NotDoneSorting = false;
    //         }
            
    //         noReset = true;
    //     }
    //     // Adding Special node
    //     finalResult.Add(hotspots[hotspots.Length - 1]);
    //     // Building new, smaller array
    //     Vector3[] result = new Vector3[finalResult.Count];
    //     for (int i = 0; i < result.Length; i++)
    //     {
    //         result[i] = finalResult[i];
    //     }
    //     return result;
    // }


