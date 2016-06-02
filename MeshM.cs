
    
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
|               Last Update: June 1st, 2016
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
        if (currentCont == 1116 || currentCont == 1265)
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
        
        
    // Method:              "GetClosestNode(Vector3[] hotspots, Vector3 destination)"
    // Purpose:             This is to be used with the "GetMesh" method
    public static Vector3 GetClosestNode(Vector3[] hotspots, Vector3 destination)
    {
        Vector3 closest = hotspots[0];

        for (int i = 1; i < hotspots.Length - 2; i++)
        {
            if (hotspots[i].Distance(destination) < closest.Distance(destination))
            {
                closest = hotspots[i];
            }
        }
        return closest;
    }
 

    // Method:          "Navigate(Vector3 destination)"
    public static IEnumerable<int> Navigate(Vector3 destination)
    {   
        // Hold off if on Flightpath.
        var wait = new Fiber<int>(QH.WaitUntilOffTaxi());
        while (wait.Run())
        {
            yield return 100;
        }
        // Recheck if VM interruption or something.
        bool reCheck = false;
        Vector3 closestNode;
        Vector3 bridgeNode;
        
        API.Print("Calculating Hotspots for Custom Navigation System...");
                // Why waste time parsing through them all if I am already right there?
        if (API.Me.Position.Distance(destination) > 3f)
        {
            SetCorrectMesh(destination);
            currentMesh = GetDestinationMesh(currentMesh,destination);
            closestNode = GetClosestNode(currentMesh,API.Me.Position);
            bridgeNode = currentMesh[currentMesh.Length - 2];
            
            // error check Vector3 no-position
            if (currentMesh.Length > 0 && API.Me.Position.Distance(closestNode) > 500f)
            {
                API.Print("Wow, Player Destination is Quite Far! Re-Calculating More Complex Route...");
                if (IsAnyMeshClose(20f))
                {
                    API.Print("Moving off Current Mesh");
                    
                    Vector3 characterPosition2 = API.Me.Position;
                    currentMesh = GetDestinationMesh(currentMesh,characterPosition2); // Returns the mesh to the one closest to the player now.
                    Vector3[] route3 = GetFastestRoute(currentMesh[currentMesh.Length - 2]);
                    
                    if (API.ExecuteLua<bool>("return IsOutdoors();") && !API.Me.IsMounted)
                    {
                        API.MountUp();
                    }
                    for (int i = 0; i < route3.Length - 1; i++)
                    {
                        while(!API.CTM(route3[i]) && API.Me.Position.Distance(route3[i]) > 2f)
                        {
                            yield return 100;
                        }
                    }
                    // Arriving at special bridge node.
                    while (!API.CTM(currentMesh[currentMesh.Length - 2]) && API.Me.Position.Distance(currentMesh[currentMesh.Length - 2]) > 2f)
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
                SetCorrectMesh(destination);
                currentMesh = GetDestinationMesh(currentMesh,destination);
                closestNode = GetClosestNode(currentMesh,API.Me.Position);
                bridgeNode = currentMesh[currentMesh.Length - 2];
            }
            
            // Player is far away from custom mesh.. possible Stuck issues could occur. Issue warning!
            if (API.Me.Position.Distance(closestNode) > 20f && !QH.IsInInstance())
            {
                API.Print("Attempting to Navigate to Edge of Mesh...");
                
                // Checking if I need to do special pathing off a diff. mesh
                if (IsAnyMeshClose(20f))
                {
                    API.Print("Moving off Current Mesh");
                    
                    Vector3 characterPosition = API.Me.Position;
                    currentMesh = GetDestinationMesh(currentMesh,characterPosition); // Returns the mesh to the one closest to the player now.
                    Vector3[] route2 = GetFastestRoute(currentMesh[currentMesh.Length - 2]);
                    
                    if (API.ExecuteLua<bool>("return IsOutdoors();") && !API.Me.IsMounted)
                    {
                        API.MountUp();
                    }
                    
                    for (int i = 0; i < route2.Length - 1; i++)
                    {
                        while(!API.CTM(route2[i]) && API.Me.Position.Distance(route2[i]) > 2f)
                        {
                            yield return 100;
                        }
                    }
                    // Arriving at special bridge node.
                    while (!API.CTM(currentMesh[currentMesh.Length - 2]) && API.Me.Position.Distance(currentMesh[currentMesh.Length - 2]) > 2f)
                    {
                        yield return 100;
                    }
                    API.Print("Activing Mesh Bridge Logic...");
                }
                
                // Qucik check if next mesh is close.
                SetCorrectMesh(destination);
                currentMesh = GetDestinationMesh(currentMesh,destination);
                closestNode = GetClosestNode(currentMesh,API.Me.Position);
                if (API.Me.Position.Distance(closestNode) >= 20f)
                {
                    // Finale MoveTo to special node at edge of mesh.
                    while(!API.MoveTo(bridgeNode))
                    {
                        yield return 250;
                    }
                    API.Print("Activing Mesh Bridge Logic...");
                }
                
            }
            
            SetCorrectMesh(destination);
            currentMesh = GetDestinationMesh(currentMesh,destination);
            Vector3[] result = GetFastestRoute(destination);
            API.Print("Taking " + (result.Length - 1) + " Node" + (result.Length > 2 ? "s" : "") + " to Get to Your Destination!");

            if (API.ExecuteLua<bool>("return IsOutdoors();") && !API.Me.IsMounted)
            {
                API.MountUp();
            }
            // Navigating through CTM normally.
            
            for (int i = 0; i < result.Length - 1; i++)
            {
                while(!API.CTM(result[i]) && API.Me.Position.Distance(result[i]) > 2f)
                {
                    yield return 100;
                }
            }
        }
        while(!API.CTM(destination) && API.Me.Position.Distance(destination) > 2f)
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
        Vector3 specialMeshIsTrue = new Vector3(1f,1f,1f);
        if (currentMesh[currentMesh.Length - 1] == specialMeshIsTrue)
        {
            API.Print("Sorting Complex Mesh...");
            return CreateComplexPath(currentMesh, destination);
        }
        return CreateStraightPath(currentMesh, destination);
    }
    
    private static Vector3[] CreateStraightPath(Vector3[] hotspots, Vector3 destination)
    {
        int closestMeshNode = GetMeshIndex(hotspots, API.Me.Position);
        int finalMeshNode = GetMeshIndex(hotspots, destination);
        Vector3[] result;
        int count = 0;

        // No mesh found in this area, or it is far away, look into Special Navigation if > 500 yards.
        if (finalMeshNode == -1 || API.Me.Position.Distance(hotspots[finalMeshNode]) > 500f)
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
        for (int i = 1; i < hotspots.Length - 2; i++)
        {
            if (hotspots[i].Distance(closestPosition) < closestMeshNode.Distance(closestPosition))
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
        for (int i = 1; i < hotspots.Length - 2; i++)
        {
            if (hotspots[i+2] != defaultNode && hotspots[i].Distance(destination) < closestMeshNode.Distance(destination))
            {
                closestMeshNode = hotspots[i];
                index = i;
            }
            // Skips over mesh divider to check other nearby mesh.
            // This is mainly useful as when setting the current mesh, if many are close within range, this will collect all the relevant meshes
            // Then parse the actual one that is closest.
            else if (hotspots[i+2] == defaultNode)
            {
                i = i+2;
            }
        }
        
        // Now, finding the range of the Destination mesh of the array.
        int topIndex = hotspots.Length - 3;  // one back, plus one more back to skip default node. This is just default.
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
    public static bool IsAnyMeshClose(float withinHowManyYards)
    {
        // error check
        if (withinHowManyYards > 20f)
        {
            withinHowManyYards = 20f;
        }
        
        SetCorrectMesh(API.Me.Position);
        bool result = false;

        for (int i = 0; i < currentMesh.Length - 2; i++)
        {
            if (API.Me.Position.Distance(currentMesh[i]) < withinHowManyYards)
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
        for (int i = 0; i < hotspots.Length - 2; i++)
        {
            for (int j = i; j > 0; j--)
            {
                if (API.Me.Position.Distance(hotspots[j-1]) > API.Me.Position.Distance(hotspots[j]))
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
        for (int i = 0; i < hotspots.Length - 2; i++)
        {
            for (int j = i; j > 0; j--)
            {
                if (destination.Distance(hotspots[j-1]) > destination.Distance(hotspots[j]))
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
        if (API.Me.Position.Distance(destination) > withinHowManyYards)
        {
            route = GetDestinationMesh(currentMesh, destination);
            // A Quick escape if needed... this means no meshes were even found to check against or they were > 500 yards.
            if (route[0] == defaultNode)
            {
                return false;
            }
             
            for (int i = 0; i < route.Length - 2; i++)
            {
                if (API.Me.Position.Distance(route[i]) <= withinHowManyYards)
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
    
    // Method:              "ExitMeshIfPlayerIsOnIt(float withinHowManyYards, Vector3 destination)"
    // What it Does:        It navigates off the current Mesh to the destination if player is already on it
    // Purpose:             Useful as an API for certain situational circumstances. Will not be used often.
    public static IEnumerable<int> ExitMeshIfPlayerIsOnIt(float withinHowManyYards, Vector3 destination)
    {
        if (IsDestinationMeshClose(withinHowManyYards,destination))
        {
            var check = new Fiber<int>(Navigate(destination));
            while(check.Run())
            {
                yield return 100;
            }
        }
        yield break;
    }
    
    // Method:              "ExitCurrentMesh(float withinHowManyYards, Vector3 destination)"
    // What it Does:        It navigates off the current Mesh to the destination if player is already on it
    // Purpose:             Useful as an API for certain situational circumstances. Will not be used often.
    public static IEnumerable<int> ExitCurrentMesh()
    {
        if (IsAnyMeshClose(20f))
        {
            currentMesh = GetDestinationMesh(currentMesh,API.Me.Position); // Returns the mesh to the one closest to the player now.
            var check = new Fiber<int>(Navigate(currentMesh[currentMesh.Length - 2]));
            while(check.Run())
            {
                yield return 100;
            }
        }
        yield break;
    }
    
    // Method:          "CollectNearbyNodes(List<Vector3> hotspots, Vector3 currentNodePosition, int count)"
    // What it Does:    Basically, it collects all nearby nodes to the player.  This is useful to determine which node is 
    //                  ideal to path to next.  It places weights on the nodes based on distance from destination
    // Purpose:         To help navigate complex meshes.
    public static List<Vector3> CollectNearbyNodes(List<Vector3> hotspots, Vector3 currentNodePosition, int count)
    {
        Vector3 closestNode = hotspots[count];
        for (int i = count + 1; i < hotspots.Count - 2; i++)
        {
            if (hotspots[i].Distance(currentNodePosition) < closestNode.Distance(currentNodePosition))
            {
                closestNode = hotspots[i];
            }
        }
        
        float distanceBubble = currentNodePosition.Distance(closestNode) + 7f;
        List<Vector3> result = new List<Vector3>();
        
        for (int i = count; i < hotspots.Count - 2; i++)
        {
            if (hotspots[i].Distance(currentNodePosition) <= distanceBubble)
            {
                result.Add(hotspots[i]);
            }
        }
        return result;
    }
    
    // public static Vector3 CalculateNextNode(List<Vector3> adjacentNodes, List<Vector3> dynamicList, Vector3 finalMeshNode, int position)
    // {
    //     if (adjacentNodes.Count == 0)
    //     {
    //         API.Print("Internal Mesh Error... Please report where and when on the Forums to Sklug!");
    //         return new Vector3(0f,0f,0f);
    //     }
    //     else if (adjacentNodes.Count == 1)
    //     {
    //         return adjacentNodes[0];
    //     }
        
    //     List<Vector3> keyNodes = new List<Vector3>(); // to build
        
    //     Vector3 position = adjacentNodes[0];
    //     Vector3 closest;
    //     bool NotDoneSorting = true;
    //     int index = 0;
    //     int count = position;

    //     float distance = 0.0f;
    //     float tempDistance = 0.0f;
    //     int specialIndex = -1;
        
    //     // This will check each adjacent node
    //     for(int z = 0; z < adjacentNodes.Count; z++)
    //     {
    //         // Parsing the first adjacent node to the next node.
    //         while (NotDoneSorting)
    //         {
    //             closest = dynamicList[count]
    //             for (int i = count; i < dynamicList.Count - 2; i++)
    //             {
    //                 if (dynamicList[i].Distance(position) < closest.Distance(position))
    //                 {
    //                     closest = dynamicList[i];
    //                     index = i;
    //                 }
    //             }
    //             // Adding up total path distance...
    //             tempDistance += closest.Distance(position);
                
    //             // If it hits a wall without hitting final Node
    //             if (closest.Distance(position) > 14f && count != 0 && closest != finalMeshNode)
    //             {
    //                 tempDistance = 999999f; // Insanely large distance to ensure it is not considered.
    //                 break;
    //             }
                
    //             // Adjusting the mesh
    //             // Now, shift everything below the next closest hotspot to the right;
    //             for (int j = index; j > count; j--)
    //             {
    //                 dynamicList[j] = dynamicList[j-1];
    //             }
    //             dynamicList[count] = closest;
    //             index = 0;
    //             count++;
    //             position = closest;
                 
    //             // Determining if we reached the last hotspot;
    //             if (closest == finalMeshNode || closest.Distance(finalMeshNode) < 3f)
    //             {
    //                 NotDoneSorting = false;
    //             }
    //             else if ()
    //             {
                    
    //             }
               
    //         }
            
            
            
            
    //     }
        
  
    
    
    // Method:          "GetWeightedNode(List<Vector3> adjacentNodes, Vector3 closestNodeToDestination)"
    // What it Does:     Returns the Vector3 node to "try" to navigate to next in a mesh path...
    // Purpose:         To filter through the collected "nearbynodes" and determine which is weighted the best.
    public static Vector3 GetWeightedNode(List<Vector3> adjacentNodes, Vector3 closestNodeToDestination)
    {
        if (adjacentNodes.Count == 0)
        {
            API.Print("Internal Mesh Error... Please report where and when on the Forums to Sklug!");
            return new Vector3(0f,0f,0f);
        }
        else if (adjacentNodes.Count == 1)
        {
            return adjacentNodes[0];
        }
        
        float distance = adjacentNodes[0].Distance(closestNodeToDestination);
        Vector3 bestNode = adjacentNodes[0];
        for (int i = 1; i < adjacentNodes.Count - 1; i++)
        {
            if (adjacentNodes[i].Distance(closestNodeToDestination) < distance)
            {
                distance = adjacentNodes[i].Distance(closestNodeToDestination);
                bestNode = adjacentNodes[i];
            }
        }
        return bestNode;
    }

    // Method:          "CreateComplexPath(Vector3[] hotspots, Vector3 destination)"
    // What it does:    This returns a sort of a mesh of nodes to faster navigate to the destination.
    // Purpose:          "This is for complex mesh navigation that is a true mesh rather than an A to B array
    public static Vector3[] CreateComplexPath(Vector3[] hotspots, Vector3 destination)
    {
         // Error Checking
        if (hotspots.Length == 0)
        {
            return hotspots;
        }

        Vector3 finalMeshNode = GetClosestNode(hotspots, destination);  // Final Node to Player
        // Variable initializations
        List<Vector3> finalResult = new List<Vector3>(); // to build
        List<Vector3> dynamicList = new List<Vector3>(hotspots); // to manipulate

        // 
        Vector3 position = API.Me.Position;
        Vector3 closest;
        bool NotDoneSorting = true;
        int index = 0;
        int count = 0;
        bool noReset = true;
        
        while(NotDoneSorting)
        {
            closest = GetWeightedNode(CollectNearbyNodes(dynamicList,position,count),finalMeshNode);
            
            for (int i = count; i < dynamicList.Count - 2; i++)
            {
                if (closest == dynamicList[i])
                {
                    index = i;
                    break;
                }
            }
            if (closest.Distance(position) > 14f && count != 0 && closest != finalMeshNode)
            {
                // Start Over, but remove failed node.
                finalResult.Clear();
                dynamicList.RemoveAt(count - 1);
                position = API.Me.Position;
                index = 0;
                count = 0;
                noReset = false;
            }

            if (noReset)
            {
                // Now, shift everything below the next closest hotspot to the right;
                for (int j = index; j > count; j--)
                {
                    dynamicList[j] = dynamicList[j-1];
                }
                dynamicList[count] = closest;
                finalResult.Add(closest);
                index = 0;
                count++;
                position = closest;
            }
            
            // Determining if we reached the last hotspot;
            if ((closest == finalMeshNode || closest.Distance(destination) < 3f) && noReset)
            {
                NotDoneSorting = false;
            }
            // Resetting 
            noReset = true;
        }
            
        // Adding Special node
        finalResult.Add(hotspots[hotspots.Length - 2]);
        // Building new, smaller array
        Vector3[] result = new Vector3[finalResult.Count];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = finalResult[i];
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
    
    
    // Method:             "HotspotGenerator(int seconds)"
    // What it Does:        Run this method whilst running a path in the game to build the actual mesh.
    // Purpose:             This is to be used to create variable hotspot speeds.
    public static IEnumerable<int> HotspotGenerator(int seconds,int hotspotsPerSecond)
    {
        
        // error check
        if (hotspotsPerSecond > 4)
        {
            hotspotsPerSecond = 4;
        }
        else if (hotspotsPerSecond == 0)
        {
            hotspotsPerSecond = 1;
        }
        
        int yieldTime = 1000 / hotspotsPerSecond;
        
        API.DisableCombat = true;
        string result = ("Vector3[] hotspots = new Vector3[]{{");
        int count = seconds * hotspotsPerSecond;
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
            yield return yieldTime;
            index++;
            if (index % 6 == 0)
            {
                if (!result.Equals("Vector3[] hotspots = new Vector3[]{{"))
                {
                    API.Print(result.Substring(0, result.Length - 1) + ",new Vector3([Insert Bridge Node Here]),new Vector3(0f,0f,1f),new Vector3(0f,0f,0f)}};");
                }
                else
                {
                    API.Print("No Hotzones Added Yet.  You may want to start moving!");
                }
            }
        }
        if (!result.Equals("Vector3[] hotspots = new Vector3[]{{"))
        {
            API.Print(result.Substring(0, result.Length - 1) + ",new Vector3([Insert Bridge Node Here]),new Vector3(0f,0f,1f),new Vector3(0f,0f,0f)}};");
        }
        else
        {
            API.Print("No Hotzones Were Added.  Please Move on a path in-game and the hotspot generator will build the mesh.");
        }
        API.DisableCombat = false;
        yield break;
    }
    
      
}

    