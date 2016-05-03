
    
/* Author:      Sklug a.k.a TheGenomeWhisperer
|       	    The following program is to fill a hole of the ReBot devs, so I can build my own custom mesh pathing when they are incomplete
|
| NOTE:         These will be inherited in the ReBot.ReBotAPI Class.
| Final Note:   Class does not need Static Main as it will be injected into the Rebot.exe through the "Editor"
|               
|               Full Information on actual use in live profiles: http://www.rebot.to/showthread.php?t=4930
|               MeshBuilder will be shorted for ease of building to Mesh
|               Note: MM = MeshManager
|
|               Last Update: May 2nd, 2016
|
*/  
	
public class Mesh
{
    public static ReBotAPI API;
    public static Fiber<int> Fib;
    
    public static int currentContinent;
    public static int currentZone;
    public static Vector3[] currentMesh;
    public static bool IsHorde;

    // Empty Constructor
    public Mesh() {}
    
    public static void Initialize()
    {
        currentContinent = API.Me.ContinentID;
        currentZone = API.Me.ZoneId;
        IsHorde = API.Me.IsHorde;
        currentMesh = GetMesh(currentContinent,currentZone,IsHorde);
    }
    
    public static Vector3[] GetMesh(int currentCont, int zoneID, bool factionIsHorde)
    {
        // Draenor ContinentID
        // All Zone IDs for Continent!
        if (currentCont == 1116)
        {
            if (zoneID == 6720 || zoneID == 6868 || zoneID == 6745 || zoneID == 6849 || zoneID == 6861 || zoneID == 6864 || zoneID == 6848 || zoneID == 6875 || zoneID == 6939 || zoneID == 7005 || zoneID == 7209 || zoneID == 7004 || zoneID == 7327 || zoneID == 7328 || zoneID == 7329)
            {
                return getFFR(factionIsHorde);
            }

            // Gorgrond (and caves and phases)
            else if (zoneID == 6721 || zoneID == 6885 || zoneID == 7160 || zoneID == 7185)
            {
                return getGorgrond(factionIsHorde);
            }

            // Talador (and caves and phases)
            else if (zoneID == 6662 || zoneID == 6980 || zoneID == 6979 || zoneID == 7089 || zoneID == 7622)
            {
                return getTalador(factionIsHorde);
            }

            // Spires of Arak
            else if (zoneID == 6722)
            {
                return getSpires(factionIsHorde);
            }

            // Nagrand (and phased caves)
            else if (zoneID == 6755 || zoneID == 7124 || zoneID == 7203 || zoneID == 7204 || zoneID == 7267)
            {
                return getNagrand(factionIsHorde);
            }

            // Shadowmoon Valley (and caves and phases)
            else if (zoneID == 6719 || zoneID == 6976 || zoneID == 7460 || zoneID == 7083 || zoneID == 7078 || zoneID == 7327 || zoneID == 7328 || zoneID == 7329)
            {
                return getSMV(factionIsHorde);
            }
            
            // Tanaan Jungle
            else if (zoneID == 6723)
            {
                return getTanaan(factionIsHorde);
            }

            // Ashran (and mine)
            else if (zoneID == 6941 || zoneID == 7548)
            {
                return getAshran(factionIsHorde);
            }

            // Warspear
            else if (zoneID == 7333)
            {
                return getWarspear(factionIsHorde);
            }
        }
        // Tanaan Intro phased continent
        // Only just the one zoneId (7025)
        else if (currentCont == 1265)
        {
            return getTanaanIntro(factionIsHorde);
        }
        
        // No matches
        Vector3[] result = new Vector3[]{};
        return result;
    }
    
    public static Vector3[] getFFR(bool factionIsHorde)
    {
        List<Vector3> location = new List<Vector3>();
        if (factionIsHorde)
        {
            // Horde Shipyard
            Vector3[] list0 = new Vector3[]{new Vector3(5226.465f, 5103.418f, 5.177947f),new Vector3(5228.128f, 5099.702f, 5.177947f),new Vector3(5231.669f, 5091.785f, 3.380589f),new Vector3(5235.001f, 5084.335f, 3.268428f),new Vector3(5238.147f, 5076.524f, 3.26907f),new Vector3(5241.06f, 5068.825f, 3.269417f),new Vector3(5243.971f, 5061.015f, 3.269331f),new Vector3(5246.577f, 5053.362f, 3.269719f),new Vector3(5249.292f, 5045.341f, 3.270111f),new Vector3(5252.161f, 5036.862f, 3.279385f),new Vector3(5254.744f, 5029.231f, 3.281622f),new Vector3(5257.496f, 5021.1f, 3.281622f),new Vector3(5260.215f, 5013.065f, 4.370923f),new Vector3(5262.909f, 5005.366f, 5.023382f),new Vector3(5268.368f, 4999.576f, 5.022273f),new Vector3(5276.881f, 4999.54f, 5.02261f),new Vector3(5285.001f, 5001.184f, 5.024286f),new Vector3(5293.423f, 5002.99f, 5.022233f),new Vector3(5301.736f, 5005.184f, 5.035467f),new Vector3(5309.441f, 5007.458f, 5.030182f),new Vector3(5317.661f, 5009.984f, 5.145557f),new Vector3(5325.29f, 5012.334f, 5.024195f),new Vector3(5333.139f, 5014.767f, 5.022141f),new Vector3(5340.74f, 5018.779f, 5.02382f),new Vector3(5346.704f, 5024.007f, 5.02382f),new Vector3(5347.966f, 5031.805f, 5.021767f),new Vector3(5346.603f, 5039.763f, 5.022386f),new Vector3(5344.565f, 5048.033f, 3.454549f),new Vector3(5342.144f, 5055.838f, 3.280035f),new Vector3(5339.57f, 5063.75f, 3.281292f),new Vector3(5336.929f, 5071.516f, 3.279427f),new Vector3(5334.284f, 5079.233f, 3.271114f),new Vector3(5331.647f, 5086.923f, 3.271114f),new Vector3(5328.998f, 5094.546f, 3.269061f),new Vector3(5325.908f, 5101.985f, 3.271765f),new Vector3(5322.469f, 5110.187f, 3.269712f),new Vector3(5319.396f, 5117.553f, 3.365296f),new Vector3(5316.603f, 5124.887f, 3.266913f),new Vector3(5313.506f, 5133.693f, 3.26486f),new Vector3(5310.355f, 5141.261f, 3.262807f),new Vector3(5306.733f, 5149.109f, 5.007558f),new Vector3(5304.183f, 5154.635f, 5.179315f),new Vector3(5313.919f, 4999.146f, 5.031353f),new Vector3(5316.989f, 4991.349f, 4.590706f),new Vector3(5319.994f, 4983.496f, 3.261628f),new Vector3(5322.344f, 4976.021f, 3.262311f),new Vector3(5324.748f, 4968.041f, 3.454753f),new Vector3(5327.033f, 4960.454f, 3.355773f),new Vector3(5329.424f, 4952.516f, 3.354637f),new Vector3(5331.841f, 4944.493f, 3.814888f),new Vector3(5333.223f, 4939.904f, 4.313459f),new Vector3(5336.783f, 4942.057f, 3.987006f),new Vector3(5344.255f, 4946.576f, 3.750103f),new Vector3(5350.35f, 4952.551f, 3.886631f),new Vector3(5355.614f, 4958.595f, 3.89053f),new Vector3(5359.577f, 4965.995f, 4.598823f),new Vector3(5363.436f, 4974.154f, 6.17427f),new Vector3(5368.585f, 4980.556f, 6.895992f),new Vector3(5375.232f, 4985.608f, 6.570148f),new Vector3(5381.774f, 4990.581f, 6.060965f),new Vector3(5388.921f, 4995.311f, 5.513795f),new Vector3(5395.682f, 4999.771f, 5.192679f),new Vector3(5402.694f, 5004.194f, 4.248222f),new Vector3(5409.237f, 5008.819f, 3.37357f),new Vector3(5415.642f, 5013.987f, 3.103029f),new Vector3(5272.541f, 4938.235f, 8.941624f),new Vector3(5276.913f, 4938.519f, 7.778503f),new Vector3(5285.406f, 4939.062f, 5.868029f),new Vector3(5293.458f, 4939.168f, 5.076531f),new Vector3(5301.167f, 4940.197f, 3.863107f),new Vector3(5309.34f, 4942.798f, 2.662547f),new Vector3(5337.066f, 4935.606f, 5.080146f),new Vector3(5338.462f, 4933.899f, 5.410695f),new Vector3(5343.997f, 4926.92f, 7.379288f),new Vector3(5349.408f, 4921.278f, 10.13784f),new Vector3(5355.425f, 4915.238f, 13.54762f),new Vector3(5361.338f, 4909.301f, 18.02958f),new Vector3(5367.602f, 4904.31f, 22.16294f),new Vector3(5375.418f, 4902.233f, 25.21158f),new Vector3(5381.681f, 4907.238f, 26.89433f),new Vector3(5386.229f, 4914.455f, 30.41585f),new Vector3(5387.525f, 4922.56f, 33.92372f),new Vector3(5386.115f, 4930.876f, 35.5632f),new Vector3(5387.724f, 4938.739f, 35.5954f),new Vector3(5391.915f, 4946.38f, 34.34087f),new Vector3(5395.266f, 4953.227f, 32.81433f),new Vector3(5395.794f, 4955.711f, 32.35955f),new Vector3(5392.45f, 4935.087f, 36.59919f),new Vector3(5395.52f, 4934.9f, 37.70484f),new Vector3(5403.881f, 4934.011f, 40.71118f),new Vector3(5410.981f, 4929.611f, 43.73222f),new Vector3(5416.117f, 4922.526f, 45.84546f),new Vector3(5419.612f, 4915.138f, 48.19114f),new Vector3(5406.811f, 4930.285f, 42.55336f),new Vector3(5412.805f, 4923.976f, 45.10557f),new Vector3(5418.313f, 4918.179f, 47.16678f),new Vector3(5424.307f, 4911.87f, 50.2869f),new Vector3(5429.672f, 4905.676f, 53.36563f),new Vector3(5435.39f, 4899.387f, 56.10091f),new Vector3(5441.695f, 4894.402f, 58.92585f),new Vector3(5448.286f, 4889.825f, 62.17897f),new Vector3(5455.419f, 4885.038f, 66.39535f),new Vector3(5462.308f, 4880.404f, 70.21847f),new Vector3(5469.073f, 4875.818f, 74.21414f),new Vector3(5475.306f, 4870.763f, 77.82598f),new Vector3(5480.802f, 4864.634f, 82.19171f),new Vector3(5485.893f, 4858.343f, 87.268f),new Vector3(5489.982f, 4850.61f, 93.11131f),new Vector3(5491.314f, 4842.559f, 98.48849f),new Vector3(5490.236f, 4834.032f, 103.627f),new Vector3(5489.203f, 4825.97f, 106.6963f),new Vector3(5487.678f, 4817.781f, 109.5393f),new Vector3(5483.123f, 4810.932f, 111.7618f),new Vector3(5477.436f, 4805.284f, 113.4704f),new Vector3(5471.529f, 4799.627f, 115.9368f),new Vector3(5466.415f, 4793.621f, 118.6344f),new Vector3(5463.415f, 4785.923f, 121.064f)};
            location.AddRange(list0);
        }
        else if (!factionIsHorde)
        {
            // Alliance only pathing
        }
        // Neutral Mesh Zones.
        
        
        // Converting list into a less bloated Array for later use.
        Vector3[] result = new Vector3[location.Count()];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = location[i];
        }
        return result;
    }
    
    public static Vector3[] getTanaanIntro(bool factionIsHorde)
    {
        List<Vector3> location = new List<Vector3>();
        if (factionIsHorde)
        {
            // Horde only pathing
        }
        else if (!factionIsHorde)
        {
            // Alliance only pathing
        }
        
        // Neutral pathing
        // Gul'dan, under the Dark Portal
        Vector3[] hotspots0 = new Vector3[]{new Vector3(4117.031f, -2376.822f, 78.83586f),new Vector3(4113.069f, -2376.832f, 79.05174f),new Vector3(4109.1f, -2376.841f, 79.44233f),new Vector3(4104.893f, -2376.851f, 79.6423f),new Vector3(4100.98f, -2376.86f, 79.64076f),new Vector3(4097.55f, -2376.868f, 79.646f),new Vector3(4093.35f, -2376.878f, 79.65382f),new Vector3(4089.913f, -2376.886f, 79.65726f),new Vector3(4085.825f, -2376.896f, 79.66137f),new Vector3(4081.646f, -2376.906f, 77.60734f),new Vector3(4077.523f, -2376.916f, 75.34579f),new Vector3(4073.532f, -2376.925f, 74.98172f),new Vector3(4069.727f, -2377.579f, 74.98868f),new Vector3(4067.434f, -2380.789f, 74.98933f),new Vector3(4067.037f, -2384.726f, 74.98939f),new Vector3(4068.927f, -2387.505f, 74.98939f),new Vector3(4072.562f, -2386.919f, 74.10859f),new Vector3(4076.432f, -2387.012f, 70.74607f),new Vector3(4080.477f, -2387.114f, 69.53637f),new Vector3(4084.335f, -2388.188f, 69.53637f),new Vector3(4087.126f, -2390.922f, 69.53642f),new Vector3(4089.559f, -2393.585f, 69.53488f),new Vector3(4091.527f, -2397.232f, 69.52975f),new Vector3(4094.285f, -2400.204f, 69.59004f),new Vector3(4095.399f, -2403.69f, 69.82324f),new Vector3(4094.589f, -2407.356f, 69.51762f),new Vector3(4092.061f, -2410.864f, 69.5323f),new Vector3(4089.119f, -2413.675f, 69.5363f),new Vector3(4085.919f, -2416.011f, 69.53687f),new Vector3(4082.862f, -2418.803f, 69.53687f),new Vector3(4079.792f, -2421.612f, 69.53687f),new Vector3(4076.833f, -2424.172f, 69.53532f),new Vector3(4073.868f, -2426.737f, 69.53378f),new Vector3(4070.774f, -2429.333f, 69.85598f),new Vector3(4067.518f, -2431.402f, 69.82574f),new Vector3(4064.256f, -2429.49f, 69.61079f),new Vector3(4061.411f, -2426.505f, 69.53375f),new Vector3(4058.471f, -2423.988f, 69.53473f),new Vector3(4055.511f, -2421.471f, 69.53516f),new Vector3(4052.343f, -2418.779f, 69.53664f),new Vector3(4049.18f, -2416.09f, 69.53668f),new Vector3(4046.292f, -2413.545f, 69.53695f),new Vector3(4043.591f, -2410.825f, 69.53761f),new Vector3(4041.007f, -2407.952f, 69.86374f),new Vector3(4038.351f, -2404.962f, 69.83506f),new Vector3(4038.422f, -2401.016f, 69.835f),new Vector3(4040.812f, -2398.448f, 69.8647f),new Vector3(4062.021f, -2386.419f, 74.98956f),new Vector3(4066.068f, -2386.689f, 74.98964f),new Vector3(4068.414f, -2386.92f, 74.98964f),new Vector3(4067.727f, -2388.568f, 74.98973f),new Vector3(4066.908f, -2392.406f, 75.76965f),new Vector3(4066.594f, -2396.11f, 77.15092f),new Vector3(4046.742f, -2391.365f, 69.53956f),new Vector3(4049.598f, -2388.784f, 69.53956f),new Vector3(4053.205f, -2387.282f, 69.53802f),new Vector3(4057.48f, -2386.891f, 71.20423f),new Vector3(4059.86f, -2386.85f, 73.27399f)};
        location.AddRange(hotspots0);

        // Converting list into a less bloated Array for later use.
        Vector3[] result = new Vector3[location.Count()];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = location[i];
        }
        return result;
        
    }
    
    public static Vector3[] getGorgrond(bool factionIsHorde)
    {
        Vector3[] result = new Vector3[]{};
        return result;
    }
    
    public static Vector3[] getTalador(bool factionIsHorde)
    {
        Vector3[] result = new Vector3[]{};
        return result;
    }
    
    public static Vector3[] getSpires(bool factionIsHorde)
    {
        Vector3[] result = new Vector3[]{};
        return result;
    }
    
    public static Vector3[] getNagrand(bool factionIsHorde)
    {
        Vector3[] result = new Vector3[]{};
        return result;
    }
    
    public static Vector3[] getSMV(bool factionIsHorde)
    {
        Vector3[] result = new Vector3[]{};
        return result;
    }
    
    public static Vector3[] getTanaan(bool factionIsHorde)
    {
        Vector3[] result = new Vector3[]{};
        return result;
    }
    
    public static Vector3[] getAshran(bool factionIsHorde)
    {
        Vector3[] result = new Vector3[]{};
        return result;
    }
    
    public static Vector3[] getWarspear(bool factionIsHorde)
    {
        Vector3[] result = new Vector3[]{};
        return result;
    }
        
    public static void SetCorrectMesh()
    {
        bool reload = false;
        if (API.Me.ContinentID != currentContinent)
        {
            currentContinent = API.Me.ContinentID;
            API.Print("Player Has Changed Continents... Calculating Route.");
            reload = true;
        }
        if (API.Me.ZoneId != currentZone)
        {
            currentZone = API.Me.ZoneId;
            API.Print("New Zone, new Mesh. Loading...");
            reload = true;
        }
        if (reload)
        {
            currentMesh = GetMesh(currentContinent,currentZone,IsHorde);
        }
    }
        
        
    // Method:              "GetClosestNode(Vector3[] hotspots, Vector3 currentNode)"
    // What it Does:        Exactly as I say, gets the closest node to the current node being scanned. 
    //                       Ideally, this will be a "private" method use, but I am keeping public for inner testing.
    private static Vector3 GetClosestNode(Vector3[] hotspots, Vector3 currentNode)
    {
        Vector3 closest;
        for (int i = 0; i < hotspots.Length; i++)
        {
            if (hotspots[i] != currentNode)
            {
                closest = hotspots[i];
                break;
            }
        }
        for (int i = 0; i < hotspots.Length; i++)
        {
            if (hotspots[i] != currentNode && hotspots[i] != closest && hotspots[i].Distance2D(currentNode) < closest.Distance2D(currentNode))
            {
                closest = hotspots[i];
            }
        }
        return closest;
    }
    
    
    // Method:             "HotspotGenerator(int seconds)"
    // What it Does:        Run this method whilst running a path in the game to build the actual mesh.
    public static IEnumerable<int> HotspotGenerator(int seconds)
    {
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
                    API.Print(result.Substring(0,result.Length - 1) + "}};");
                }
                else
                {
                    API.Print("No Hotzones Added Yet.  You may want to start moving!");
                }
            }
        }
        if (!result.Equals("Vector3[] hotspots = new Vector3[]{{"))
        {
            API.Print(result.Substring(0,result.Length - 1) + "}};");
        }
        else
        {
            API.Print("No Hotzones Were Added.  Please Move on a path in-game and the hotspot generator will build the mesh.");
        }
        yield break;
    }
    
    // Method:          "InsertionSort(Vector3[] hotspots)"
    // What it does:       "Places all hotspots in direct point A to B line in order of distance from the player.  Will be rarely used.
    public static void InsertionSortHotspots(Vector3[] hotspots)
    {
        Vector3 temp;
        for (int i = 0; i < hotspots.Length; i++)
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
    }

    // Method:          "NavigateShipyard(Vector3 destination)"
    public static IEnumerable<int> Navigate(Vector3 destination)
    {   
        SetCorrectMesh();
        Vector3[] hotspots = currentMesh;
                // Why waste time parsing through them all if I am already right there?
        if (API.Me.Distance2DTo(destination) > 3f)
        {
            Vector3[] route = GetFastestRoute(destination);
            API.Print("Calculating Hotspots for Custom Navigation System...");
            API.Print("Taking " + route.Length + " Nodes to Get to Your Destination!");
            for (int i = 0; i < route.Length; i++)
            {
                while(!API.CTM(route[i]) && API.Me.Distance2DTo(route[i]) > 1.5f)
                {
                    yield return 100;
                }
            }
        }
        while(!API.CTM(destination) && API.Me.Distance2DTo(destination) > 1.5f)
        {
            yield return 250;
        }
        yield break;
    }
    
    
    // Method:          "SortHotspots(Vector3[] hotspots, Vector3 destination)"
    public static Vector3[] CreatePath(Vector3[] hotspots, Vector3 destination)
    {
        // Error Checking
        if (hotspots.Length == 0)
        {
            return hotspots;
        }
               
        // Variable initializations
        List<Vector3> finalResult = new List<Vector3>();
        List<Vector3> hotspotList = new List<Vector3>(hotspots);
        Vector3 position = API.Me.Position;
        Vector3 closest;
        bool NotDoneSorting = true;
        int index = 0;
        int count = 0;
        bool noReset = true;
        int numWarnings = 0;
        
        // Determining which hotspot is closest to destination.
        Vector3 final = hotspotList[0];
        for (int i = 1; i < hotspotList.Count; i++)
        {
            if (hotspotList[i].Distance2D(destination) < final.Distance2D(destination))
            {
                final = hotspotList[i];
            }
        }
        
        while(NotDoneSorting)
        {
            // closest to current node (first will be closest to player)
            closest = hotspotList[count];
            for (int i = count + 1; i < hotspotList.Count; i++)
            {
                if (hotspotList[i].Distance2D(position) < closest.Distance2D(position))
                {
                    closest = hotspotList[i];
                    index = i;
                }
            }
            
            // Player is far away from custom mesh.. possible Stuck issues could occur. Issue warning!
            if (API.Me.Distance2DTo(closest) > 25f && count == 0 && numWarnings == 0)
            {
                API.Print("Warning! Player is " + (int)API.Me.Distance2DTo(closest) + " Yards From the Custom Mesh! Very Far! Dangerous to Navigate!");
            }
            numWarnings++;
            
            
            if (closest.Distance2D(position) > 10f && count != 0)
            {
                hotspotList.RemoveAt(count - 1);
                finalResult.Clear();
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
                    hotspotList[j] = hotspotList[j-1];
                }
                hotspotList[count] = closest;
                finalResult.Add(closest);
                index = 0;
                count++;
                position = closest;
            }
            
            
            // Determining if we reached the last hotspot;
            if (closest == final && noReset)
            {
                NotDoneSorting = false;
            }
            
            noReset = true;

        }
             
        // Building new, smaller array
        Vector3[] result = new Vector3[finalResult.Count];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = finalResult[i];
        }
        return result;
    }
    
    // Determines if the player is standing on the mesh. 
    public static bool IsMeshClose(float withinHowManyYards, Vector3 destination)
    {
        SetCorrectMesh();
        bool result = false;
        Vector3[] route;
        if (API.Me.Distance2DTo(destination) > withinHowManyYards)
        {
             route = CreatePath(currentMesh, destination);
        }
        else
        {
            return true;
        }
        for (int i = 0; i < route.Length; i++)
        {
            if (API.Me.Distance2DTo(route[i]) <= withinHowManyYards)
            {
                result = true;
            }
        }
        return result;
    }
    
    // Method:              "GetFastestRoute(Vector3 destination")
    // What it Does         In case of a looped pathway, this will determine which one is shorter.  This is critical for mesh, but 
    //                      not useful in a straight line.
    public static Vector3[] GetFastestRoute(Vector3 destination)
    {
        // Re-initialize
        SetCorrectMesh();
        // Sort hotspots so I can get closest 2.
        InsertionSortHotspots(currentMesh);
        
        // to tally all the distances.
        float distance1 = 0.0f;
        float distance2 = 0.0f;
        Vector3[] tempMesh = new Vector3[currentMesh.Length - 1];
        for (int i = 0; i < tempMesh.Length; i++)
        {
            tempMesh[i] = currentMesh[i+1];
        }
        
        Vector3[] path1 = CreatePath(currentMesh, destination);
        Vector3[] path2 = CreatePath(tempMesh, destination);
        Vector3 position = API.Me.Position;
        
        for (int i = 0; i < path1.Length; i++)
        {
            distance1 = distance1 + path1[i].Distance2D(position);
            position = path1[i];
        }

        position = API.Me.Position;
        for (int i = 0; i < path2.Length; i++)
        {
            distance2 = distance2 + path2[i].Distance2D(position);
            position = path2[i];
        }
        if (distance1 < distance2)
        {
            return path1;
        }
        else
        {
            return path2;
        }
    }
}