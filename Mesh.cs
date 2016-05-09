
    
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
    }
    
    public static Vector3[] GetMesh(int currentCont, int zoneID, bool factionIsHorde, Vector3 destination)
    {
        // Draenor ContinentID
        // All Zone IDs for Continent!
        
        // Dungeon Mesh Checking
        if (QH.IsInInstance())
        {
            return DM.GetDungeonMesh();
        }
        
        if (currentCont == 1116)
        {
            if (zoneID == 6720 || zoneID == 6868 || zoneID == 6745 || zoneID == 6849 || zoneID == 6861 || zoneID == 6864 || zoneID == 6848 || zoneID == 6875 || zoneID == 6939 || zoneID == 7005 || zoneID == 7209 || zoneID == 7004 || zoneID == 7327 || zoneID == 7328 || zoneID == 7329)
            {
                return getFFR(destination,factionIsHorde);
            }

            // Gorgrond (and caves and phases)
            else if (zoneID == 6721 || zoneID == 6885 || zoneID == 7160 || zoneID == 7185)
            {
                return getGorgrond(destination,factionIsHorde);
            }

            // Talador (and caves and phases)
            else if (zoneID == 6662 || zoneID == 6980 || zoneID == 6979 || zoneID == 7089 || zoneID == 7622)
            {
                return getTalador(destination,factionIsHorde);
            }

            // Spires of Arak
            else if (zoneID == 6722)
            {
                return getSpires(destination,factionIsHorde);
            }

            // Nagrand (and phased caves)
            else if (zoneID == 6755 || zoneID == 7124 || zoneID == 7203 || zoneID == 7204 || zoneID == 7267)
            {
                return getNagrand(destination,factionIsHorde);
            }

            // Shadowmoon Valley (and caves and phases)
            else if (zoneID == 6719 || zoneID == 6976 || zoneID == 7460 || zoneID == 7083 || zoneID == 7078 || zoneID == 7327 || zoneID == 7328 || zoneID == 7329)
            {
                return getSMV(destination,factionIsHorde);
            }
            
            // Tanaan Jungle
            else if (zoneID == 6723)
            {
                return getTanaan(destination,factionIsHorde);
            }

            // Ashran (and mine)
            else if (zoneID == 6941 || zoneID == 7548)
            {
                return getAshran(destination,factionIsHorde);
            }

            // Warspear
            else if (zoneID == 7333)
            {
                return getWarspear(destination,factionIsHorde);
            }
        }
        // Tanaan Intro phased continent
        // Only just the one zoneId (7025)
        else if (currentCont == 1265)
        {
            return getTanaanIntro(destination,factionIsHorde);
        }
        
        // No matches
        API.Print("No Custom Meshes Have Been Found at Your Location.  Please submit a request in the forums!"); 
        Vector3[] result = new Vector3[]{};
        return result;
    }
    
    private static Vector3[] getFFR(Vector3 destination, bool factionIsHorde)
    {
        List<Vector3> location = new List<Vector3>();
        // Vector3 generic = new Vector3(0f,0f,0f);
        // bool collectAll = (generic == destination ? true : false);

        if (factionIsHorde)
        {
            // Horde Shipyard
            Vector3[] list0 = new Vector3[]{new Vector3(5226.465f, 5103.418f, 5.177947f),new Vector3(5228.128f, 5099.702f, 5.177947f),new Vector3(5231.669f, 5091.785f, 3.380589f),new Vector3(5235.001f, 5084.335f, 3.268428f),new Vector3(5238.147f, 5076.524f, 3.26907f),new Vector3(5241.06f, 5068.825f, 3.269417f),new Vector3(5243.971f, 5061.015f, 3.269331f),new Vector3(5246.577f, 5053.362f, 3.269719f),new Vector3(5249.292f, 5045.341f, 3.270111f),new Vector3(5252.161f, 5036.862f, 3.279385f),new Vector3(5254.744f, 5029.231f, 3.281622f),new Vector3(5257.496f, 5021.1f, 3.281622f),new Vector3(5260.215f, 5013.065f, 4.370923f),new Vector3(5262.909f, 5005.366f, 5.023382f),new Vector3(5268.368f, 4999.576f, 5.022273f),new Vector3(5276.881f, 4999.54f, 5.02261f),new Vector3(5285.001f, 5001.184f, 5.024286f),new Vector3(5293.423f, 5002.99f, 5.022233f),new Vector3(5301.736f, 5005.184f, 5.035467f),new Vector3(5309.441f, 5007.458f, 5.030182f),new Vector3(5317.661f, 5009.984f, 5.145557f),new Vector3(5325.29f, 5012.334f, 5.024195f),new Vector3(5333.139f, 5014.767f, 5.022141f),new Vector3(5340.74f, 5018.779f, 5.02382f),new Vector3(5346.704f, 5024.007f, 5.02382f),new Vector3(5347.966f, 5031.805f, 5.021767f),new Vector3(5346.603f, 5039.763f, 5.022386f),new Vector3(5344.565f, 5048.033f, 3.454549f),new Vector3(5342.144f, 5055.838f, 3.280035f),new Vector3(5339.57f, 5063.75f, 3.281292f),new Vector3(5336.929f, 5071.516f, 3.279427f),new Vector3(5334.284f, 5079.233f, 3.271114f),new Vector3(5331.647f, 5086.923f, 3.271114f),new Vector3(5328.998f, 5094.546f, 3.269061f),new Vector3(5325.908f, 5101.985f, 3.271765f),new Vector3(5322.469f, 5110.187f, 3.269712f),new Vector3(5319.396f, 5117.553f, 3.365296f),new Vector3(5316.603f, 5124.887f, 3.266913f),new Vector3(5313.506f, 5133.693f, 3.26486f),new Vector3(5310.355f, 5141.261f, 3.262807f),new Vector3(5306.733f, 5149.109f, 5.007558f),new Vector3(5304.183f, 5154.635f, 5.179315f),new Vector3(5313.919f, 4999.146f, 5.031353f),new Vector3(5316.989f, 4991.349f, 4.590706f),new Vector3(5319.994f, 4983.496f, 3.261628f),new Vector3(5322.344f, 4976.021f, 3.262311f),new Vector3(5324.748f, 4968.041f, 3.454753f),new Vector3(5327.033f, 4960.454f, 3.355773f),new Vector3(5329.424f, 4952.516f, 3.354637f),new Vector3(5331.841f, 4944.493f, 3.814888f),new Vector3(5333.223f, 4939.904f, 4.313459f),new Vector3(5336.783f, 4942.057f, 3.987006f),new Vector3(5344.255f, 4946.576f, 3.750103f),new Vector3(5350.35f, 4952.551f, 3.886631f),new Vector3(5355.614f, 4958.595f, 3.89053f),new Vector3(5359.577f, 4965.995f, 4.598823f),new Vector3(5363.436f, 4974.154f, 6.17427f),new Vector3(5368.585f, 4980.556f, 6.895992f),new Vector3(5375.232f, 4985.608f, 6.570148f),new Vector3(5381.774f, 4990.581f, 6.060965f),new Vector3(5388.921f, 4995.311f, 5.513795f),new Vector3(5395.682f, 4999.771f, 5.192679f),new Vector3(5402.694f, 5004.194f, 4.248222f),new Vector3(5409.237f, 5008.819f, 3.37357f),new Vector3(5415.642f, 5013.987f, 3.103029f),new Vector3(5272.541f, 4938.235f, 8.941624f),new Vector3(5276.913f, 4938.519f, 7.778503f),new Vector3(5285.406f, 4939.062f, 5.868029f),new Vector3(5293.458f, 4939.168f, 5.076531f),new Vector3(5301.167f, 4940.197f, 3.863107f),new Vector3(5309.34f, 4942.798f, 2.662547f),new Vector3(5337.066f, 4935.606f, 5.080146f),new Vector3(5338.462f, 4933.899f, 5.410695f),new Vector3(5343.997f, 4926.92f, 7.379288f),new Vector3(5349.408f, 4921.278f, 10.13784f),new Vector3(5355.425f, 4915.238f, 13.54762f),new Vector3(5361.338f, 4909.301f, 18.02958f),new Vector3(5367.602f, 4904.31f, 22.16294f),new Vector3(5375.418f, 4902.233f, 25.21158f),new Vector3(5381.681f, 4907.238f, 26.89433f),new Vector3(5386.229f, 4914.455f, 30.41585f),new Vector3(5387.525f, 4922.56f, 33.92372f),new Vector3(5386.115f, 4930.876f, 35.5632f),new Vector3(5387.724f, 4938.739f, 35.5954f),new Vector3(5391.915f, 4946.38f, 34.34087f),new Vector3(5395.266f, 4953.227f, 32.81433f),new Vector3(5395.794f, 4955.711f, 32.35955f),new Vector3(5392.45f, 4935.087f, 36.59919f),new Vector3(5395.52f, 4934.9f, 37.70484f),new Vector3(5403.881f, 4934.011f, 40.71118f),new Vector3(5410.981f, 4929.611f, 43.73222f),new Vector3(5416.117f, 4922.526f, 45.84546f),new Vector3(5419.612f, 4915.138f, 48.19114f),new Vector3(5406.811f, 4930.285f, 42.55336f),new Vector3(5412.805f, 4923.976f, 45.10557f),new Vector3(5418.313f, 4918.179f, 47.16678f),new Vector3(5424.307f, 4911.87f, 50.2869f),new Vector3(5429.672f, 4905.676f, 53.36563f),new Vector3(5435.39f, 4899.387f, 56.10091f),new Vector3(5441.695f, 4894.402f, 58.92585f),new Vector3(5448.286f, 4889.825f, 62.17897f),new Vector3(5455.419f, 4885.038f, 66.39535f),new Vector3(5462.308f, 4880.404f, 70.21847f),new Vector3(5469.073f, 4875.818f, 74.21414f),new Vector3(5475.306f, 4870.763f, 77.82598f),new Vector3(5480.802f, 4864.634f, 82.19171f),new Vector3(5485.893f, 4858.343f, 87.268f),new Vector3(5489.982f, 4850.61f, 93.11131f),new Vector3(5491.314f, 4842.559f, 98.48849f),new Vector3(5490.236f, 4834.032f, 103.627f),new Vector3(5489.203f, 4825.97f, 106.6963f),new Vector3(5487.678f, 4817.781f, 109.5393f),new Vector3(5483.123f, 4810.932f, 111.7618f),new Vector3(5477.436f, 4805.284f, 113.4704f),new Vector3(5471.529f, 4799.627f, 115.9368f),new Vector3(5466.415f, 4793.621f, 118.6344f),new Vector3(5463.415f, 4785.923f, 121.064f),new Vector3(5463.0f, 4785.6f, 121.2f)};
            //
            
            // Sorting closest nodes
            list0 = InsertionSortHotspots(list0,destination);
            if (destination.Distance2D(list0[0]) < 20f)
            {
                return list0;
            }
            location.AddRange(list0);
        }
        else if (!factionIsHorde)
        {
            // Alliance only pathing
        }
        // Neutral Mesh Zones.
        // Top of Bladespire Tower
        Vector3[] list1 = new Vector3[]{new Vector3(6746.711f, 5860.897f, 325.2147f),new Vector3(6749.725f, 5865.427f, 325.2147f),new Vector3(6753.004f, 5869.792f, 325.435f),new Vector3(6756.189f, 5874.012f, 325.9037f),new Vector3(6759.275f, 5878.102f, 328.1804f),new Vector3(6762.433f, 5882.286f, 330.5016f),new Vector3(6765.552f, 5886.174f, 332.7019f),new Vector3(6769.253f, 5889.802f, 334.7689f),new Vector3(6773.33f, 5893.063f, 336.7725f),new Vector3(6777.589f, 5895.65f, 338.6516f),new Vector3(6782.252f, 5897.961f, 340.45f),new Vector3(6786.818f, 5900.162f, 342.1445f),new Vector3(6791.27f, 5902.308f, 343.7895f),new Vector3(6796.15f, 5904.175f, 345.2531f),new Vector3(6801.419f, 5904.873f, 345.3946f),new Vector3(6806.751f, 5905.196f, 345.5163f),new Vector3(6811.956f, 5905.51f, 345.6311f),new Vector3(6817.154f, 5905.742f, 345.6317f),new Vector3(6822.356f, 5905.571f, 345.6236f),new Vector3(6827.738f, 5905.074f, 345.6154f),new Vector3(6832.921f, 5904.316f, 345.6134f),new Vector3(6837.658f, 5903.408f, 345.6139f),new Vector3(6842.638f, 5902.208f, 345.614f),new Vector3(6847.589f, 5900.695f, 345.6146f),new Vector3(6852.471f, 5898.803f, 345.6156f),new Vector3(6856.773f, 5896.18f, 345.6136f),new Vector3(6860.854f, 5893.509f, 345.6219f),new Vector3(6865.071f, 5890.747f, 345.7559f),new Vector3(6869.505f, 5887.785f, 345.9433f),new Vector3(6873.239f, 5884.707f, 346.1389f),new Vector3(6876.968f, 5880.811f, 347.8372f),new Vector3(6880.272f, 5877.015f, 349.8706f),new Vector3(6883.653f, 5873.129f, 351.952f),new Vector3(6886.85f, 5869.457f, 353.7914f),new Vector3(6890.2f, 5865.151f, 355.7812f),new Vector3(6892.7f, 5860.362f, 357.8365f),new Vector3(6895.285f, 5855.936f, 359.3799f),new Vector3(6897.399f, 5851.456f, 359.3986f),new Vector3(6899.243f, 5846.876f, 359.4157f),new Vector3(6900.723f, 5842.045f, 359.425f),new Vector3(6901.673f, 5837.206f, 359.4268f),new Vector3(6902.683f, 5832.062f, 359.4268f),new Vector3(6903.73f, 5826.731f, 359.4268f),new Vector3(6904.168f, 5821.818f, 359.4268f),new Vector3(6904.216f, 5816.704f, 359.4268f),new Vector3(6903.813f, 5811.254f, 359.4268f),new Vector3(6902.814f, 5806.202f, 359.4268f),new Vector3(6901.776f, 5801.045f, 359.4268f),new Vector3(6900.625f, 5796.067f, 359.4268f),new Vector3(6898.85f, 5791.174f, 359.4268f),new Vector3(6896.998f, 5786.067f, 359.4268f),new Vector3(6895.177f, 5781.064f, 359.4268f),new Vector3(6892.555f, 5776.749f, 359.4268f),new Vector3(6889.48f, 5772.572f, 361.4489f),new Vector3(6886.292f, 5768.707f, 364.0612f),new Vector3(6882.427f, 5765.068f, 366.9061f),new Vector3(6878.673f, 5761.827f, 369.5246f),new Vector3(6874.645f, 5758.349f, 372.4668f),new Vector3(6870.891f, 5755.107f, 375.2462f),new Vector3(6866.931f, 5751.687f, 378.1248f),new Vector3(6862.791f, 5748.156f, 380.484f),new Vector3(6858.731f, 5745.992f, 382.6244f),new Vector3(6853.79f, 5743.843f, 385.1235f),new Vector3(6848.774f, 5742.006f, 387.4997f),new Vector3(6843.792f, 5740.182f, 390.0255f),new Vector3(6839.579f, 5738.64f, 392.2004f),new Vector3(6834.335f, 5736.738f, 395.0303f),new Vector3(6829.715f, 5735.546f, 397.4834f),new Vector3(6824.448f, 5734.772f, 400.3393f),new Vector3(6819.339f, 5734.944f, 401.7113f),new Vector3(6816.453f, 5738.237f, 401.7113f),new Vector3(6815.417f, 5743.34f, 401.7113f),new Vector3(6815.332f, 5748.552f, 404.3263f),new Vector3(6815.344f, 5753.421f, 406.7853f),new Vector3(6815.356f, 5758.662f, 409.6264f),new Vector3(6815.369f, 5763.858f, 410.1511f),new Vector3(6814.775f, 5768.786f, 410.151f),new Vector3(6813.035f, 5773.839f, 410.151f),new Vector3(6810.096f, 5778.067f, 410.151f),new Vector3(6806.61f, 5782.386f, 410.151f),new Vector3(6803.399f, 5786.046f, 410.151f),new Vector3(6799.9f, 5790.034f, 410.151f),new Vector3(6796.407f, 5794.015f, 410.151f),new Vector3(6793.053f, 5797.839f, 410.151f),new Vector3(6789.601f, 5801.853f, 410.151f),new Vector3(6788.016f, 5806.665f, 410.151f),new Vector3(6786.469f, 5810.917f, 410.151f),new Vector3(6785.55f, 5816.308f, 410.151f),new Vector3(6784.65f, 5821.591f, 410.151f),new Vector3(6783.762f, 5826.803f, 410.151f),new Vector3(6784.3f, 5832.236f, 410.151f),new Vector3(6785.242f, 5837.333f, 410.151f),new Vector3(6782.862f, 5841.906f, 410.151f),new Vector3(6780.617f, 5845.515f, 410.151f),new Vector3(6780.68f, 5851.13f, 410.2036f),new Vector3(6781.235f, 5856.103f, 410.3404f),new Vector3(6782.588f, 5861.211f, 410.5194f),new Vector3(6785.191f, 5865.43f, 410.7402f),new Vector3(6788.028f, 5869.827f, 412.219f),new Vector3(6789.195f, 5874.917f, 414.0236f),new Vector3(6789.248f, 5879.956f, 415.0851f),new Vector3(6789.88f, 5885.402f, 416.3937f),new Vector3(6790.03f, 5889.204f, 417.068f),new Vector3(6789.974f, 5889.866f, 417.07f),new Vector3(6748.0f, 5850.6f, 325.2f)};
        // Special Node on edge of Mesh is the last Vector point.
        
        // Sorting closest nodes
        list1 = InsertionSortHotspots(list1,destination);
        if (destination.Distance2D(list1[0]) < 20f)
        {
            return list1;
        }
        location.AddRange(list1);
        
        // Converting list into a less bloated Array for later use.
        Vector3[] result = new Vector3[location.Count()];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = location[i];
        }
        return result;
    }
    
    private static Vector3[] getTanaanIntro(Vector3 destination, bool factionIsHorde)
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
        Vector3[] hotspots0 = new Vector3[]{new Vector3(4117.031f, -2376.822f, 78.83586f),new Vector3(4113.069f, -2376.832f, 79.05174f),new Vector3(4109.1f, -2376.841f, 79.44233f),new Vector3(4104.893f, -2376.851f, 79.6423f),new Vector3(4100.98f, -2376.86f, 79.64076f),new Vector3(4097.55f, -2376.868f, 79.646f),new Vector3(4093.35f, -2376.878f, 79.65382f),new Vector3(4089.913f, -2376.886f, 79.65726f),new Vector3(4085.825f, -2376.896f, 79.66137f),new Vector3(4081.646f, -2376.906f, 77.60734f),new Vector3(4077.523f, -2376.916f, 75.34579f),new Vector3(4073.532f, -2376.925f, 74.98172f),new Vector3(4069.727f, -2377.579f, 74.98868f),new Vector3(4067.434f, -2380.789f, 74.98933f),new Vector3(4067.037f, -2384.726f, 74.98939f),new Vector3(4068.927f, -2387.505f, 74.98939f),new Vector3(4072.562f, -2386.919f, 74.10859f),new Vector3(4076.432f, -2387.012f, 70.74607f),new Vector3(4080.477f, -2387.114f, 69.53637f),new Vector3(4084.335f, -2388.188f, 69.53637f),new Vector3(4087.126f, -2390.922f, 69.53642f),new Vector3(4089.559f, -2393.585f, 69.53488f),new Vector3(4091.527f, -2397.232f, 69.52975f),new Vector3(4094.285f, -2400.204f, 69.59004f),new Vector3(4095.399f, -2403.69f, 69.82324f),new Vector3(4094.589f, -2407.356f, 69.51762f),new Vector3(4092.061f, -2410.864f, 69.5323f),new Vector3(4089.119f, -2413.675f, 69.5363f),new Vector3(4085.919f, -2416.011f, 69.53687f),new Vector3(4082.862f, -2418.803f, 69.53687f),new Vector3(4079.792f, -2421.612f, 69.53687f),new Vector3(4076.833f, -2424.172f, 69.53532f),new Vector3(4073.868f, -2426.737f, 69.53378f),new Vector3(4070.774f, -2429.333f, 69.85598f),new Vector3(4067.518f, -2431.402f, 69.82574f),new Vector3(4064.256f, -2429.49f, 69.61079f),new Vector3(4061.411f, -2426.505f, 69.53375f),new Vector3(4058.471f, -2423.988f, 69.53473f),new Vector3(4055.511f, -2421.471f, 69.53516f),new Vector3(4052.343f, -2418.779f, 69.53664f),new Vector3(4049.18f, -2416.09f, 69.53668f),new Vector3(4046.292f, -2413.545f, 69.53695f),new Vector3(4043.591f, -2410.825f, 69.53761f),new Vector3(4041.007f, -2407.952f, 69.86374f),new Vector3(4038.351f, -2404.962f, 69.83506f),new Vector3(4038.422f, -2401.016f, 69.835f),new Vector3(4040.812f, -2398.448f, 69.8647f),new Vector3(4062.021f, -2386.419f, 74.98956f),new Vector3(4066.068f, -2386.689f, 74.98964f),new Vector3(4068.414f, -2386.92f, 74.98964f),new Vector3(4067.727f, -2388.568f, 74.98973f),new Vector3(4066.908f, -2392.406f, 75.76965f),new Vector3(4066.594f, -2396.11f, 77.15092f),new Vector3(4046.742f, -2391.365f, 69.53956f),new Vector3(4049.598f, -2388.784f, 69.53956f),new Vector3(4053.205f, -2387.282f, 69.53802f),new Vector3(4057.48f, -2386.891f, 71.20423f),new Vector3(4059.86f, -2386.85f, 73.27399f),new Vector3(4119.103f, -2376.414f, 78.8f)};
        
        // Sorting closest nodes
        hotspots0 = InsertionSortHotspots(hotspots0,destination);
        if (destination.Distance2D(hotspots0[0]) < 20f)
        {
            return hotspots0;
        }
        location.AddRange(hotspots0);

        // Converting list into a less bloated Array for later use.
        Vector3[] result = new Vector3[location.Count()];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = location[i];
        }
        return result;
        
    }
    
    private static Vector3[] getGorgrond(Vector3 destination, bool factionIsHorde)
    {
        Vector3[] result = new Vector3[]{};
        return result;
    }
    
    private static Vector3[] getTalador(Vector3 destination, bool factionIsHorde)
    {
        Vector3[] result = new Vector3[]{};
        return result;
    }
    
    private static Vector3[] getSpires(Vector3 destination, bool factionIsHorde)
    {
        Vector3[] result = new Vector3[]{};
        return result;
    }
    
    private static Vector3[] getNagrand(Vector3 destination, bool factionIsHorde)
    {
        Vector3[] result = new Vector3[]{};
        return result;
    }
    
    private static Vector3[] getSMV(Vector3 destination, bool factionIsHorde)
    {
        Vector3[] result = new Vector3[]{};
        return result;
    }
    
    private static Vector3[] getTanaan(Vector3 destination, bool factionIsHorde)
    {
        Vector3[] result = new Vector3[]{};
        return result;
    }
    
    private static Vector3[] getAshran(Vector3 destination, bool factionIsHorde)
    {
        Vector3[] result = new Vector3[]{};
        return result;
    }
    
    private static Vector3[] getWarspear(Vector3 destination, bool factionIsHorde)
    {
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
        if (API.Me.ZoneId != currentZone)
        {
            currentZone = API.Me.ZoneId;
            API.Print("New Zone, new Mesh. Loading...");
        }
        currentMesh = GetMesh(currentContinent,currentZone,IsHorde,destination);
    }
        
        
    // Method:              "GetClosestNode(Vector3[] hotspots, Vector3 destination)"
    // This is to be used with the "GetMesh" method
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
    // What it does:       "Places all hotspots in direct point A to B line in order of distance from a given position
    //                      To be mainly used with the GetMesh method.
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

    // Method:          "NavigateShipyard(Vector3 destination)"
    public static IEnumerable<int> Navigate(Vector3 destination)
    {   
        API.Print("Calculating Hotspots for Custom Navigation System...");
                // Why waste time parsing through them all if I am already right there?
        if (API.Me.Distance2DTo(destination) > 3f)
        {
            SetCorrectMesh(destination);
            Vector3[] route = GetFastestRoute(destination);
            
            // Player is far away from custom mesh.. possible Stuck issues could occur. Issue warning!
            if (API.Me.Distance2DTo(route[0]) >= 15f)
            {
                API.Print("Warning! Player is " + (int)API.Me.Distance2DTo(route[0]) + " Yards From the Custom Mesh! Very Far! Dangerous to Navigate!");
                API.Print("Attempting to Navigate to Edge of Mesh...");
                
                // Checking if I need to do special pathing off a diff. mesh
                if (IsAnyMeshClose(15f))
                {
                    SetCorrectMesh(API.Me.Position);
                    Vector3[] route2 = GetFastestRoute(currentMesh[currentMesh.Length-1]);
                    for (int i = 0; i < route2.Length; i++)
                    {
                        while(!API.CTM(route2[i]) && API.Me.Distance2DTo(route2[i]) > 2f)
                        {
                            yield return 100;
                        }
                    }
                }
                // Finale MoveTo to special node at edge of mesh.
                while(!API.MoveTo(route[route.Length - 1]))
                {
                    yield return 250;
                }
            }
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
        // re-initialize
        if (QH.IsInInstance())
        {
            return CreateStraightPath(currentMesh, destination);
        }
        else
        {
            return CreatePath(currentMesh,destination);
        }
        
        // sort hotspots so i can get closest 2.
        //currentmesh = insertionsorthotspots(currentmesh);
        
        // to tally all the distances.
        // float distance1 = 0.0f;
        // float distance2 = 0.0f;
        // vector3[] tempmesh = new vector3[currentmesh.length - 1];
        // for (int i = 0; i < tempmesh.length; i++)
        // {
        //     tempmesh[i] = currentmesh[i+1];
        // }

        
        // vector3[] path2 = createpath(tempmesh, destination);
        // vector3 position = api.me.position;
        
        // for (int i = 0; i < path1.length; i++)
        // {
        //     distance1 = distance1 + path1[i].distance2d(position);
        //     position = path1[i];
        // }

        // position = api.me.position;
        // for (int i = 0; i < path2.length; i++)
        // {
        //     distance2 = distance2 + path2[i].distance2d(position);
        //     position = path2[i];
        // }
        // if (distance1 < distance2)
        // {
        //     return path1;
        // }
        // else
        // {
        //     return path2;
        // }
    }
    
    private static Vector3[] CreateStraightPath(Vector3[] hotspots, Vector3 destination)
    {
        int closestMeshNode = GetMeshIndex(hotspots, API.Me.Position);
        int finalMeshNode = GetMeshIndex(hotspots, destination);
        Vector3[] result;
        int count = 0;

        if (closestMeshNode == -1 || finalMeshNode == -1)
        {
            API.Print("Warning! Incorrect Mesh Trying to Be Parsed!");
            return result = new Vector3[0];
        }
        
        if (closestMeshNode < finalMeshNode)
        {
            result = new Vector3[finalMeshNode - closestMeshNode + 1];
            // Forward through array.
            for (int i = closestMeshNode; i <= finalMeshNode; i++)
            {
                result[count] = hotspots[i];
                count++;
            }
        }
        else
        {
            result = new Vector3[closestMeshNode - finalMeshNode + 1];
            // Backwards through array.
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
        if (closestMeshNode.Distance2D(closestPosition) >= 25f)
        {
            index = -1;
        }
        return index;
    }
        
    // Method:          "SortHotspots(Vector3[] hotspots, Vector3 destination)"
    private static Vector3[] CreatePath(Vector3[] hotspots, Vector3 destination)
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
        // Determining which hotspot is closest to destination.
        Vector3 final = GetClosestNode(hotspots,destination);

        while(NotDoneSorting)
        {
            // closest to current node (first will be closest to player)
            closest = hotspotList[count];
            for (int i = count; i < hotspotList.Count - 1; i++)
            {
                if (position.Distance2D(hotspotList[i]) < position.Distance2D(closest))
                {
                    closest = hotspotList[i];
                    index = i;
                }
            }
            
            if (closest.Distance2D(position) > 15f && count != 0 && closest != final)
            {
                // Removing Previous hotspot, then starting over.
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
            if ((closest == final || closest.Distance2D(destination) <  7f) && noReset)
            {
                NotDoneSorting = false;
            }
            
            noReset = true;
        }
        // Adding Special node
        finalResult.Add(hotspots[hotspots.Length - 1]);
        API.Print("The final result closest node is: " + finalResult[0].Distance2D(API.Me.Position));
        // Building new, smaller array
        Vector3[] result = new Vector3[finalResult.Count];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = finalResult[i];
        }
        return result;
    }
    
    // Determines if the player is standing on the mesh. 
    public static bool IsDestinationMeshClose(float withinHowManyYards, Vector3 destination)
    {
        SetCorrectMesh(destination);
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
        for (int i = 0; i < route.Length - 1; i++)
        {
            if (API.Me.Distance2DTo(route[i]) <= withinHowManyYards)
            {
                result = true;
                break;
            }
        }
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
            if (API.Me.Distance2DTo(currentMesh[i]) <= withinHowManyYards)
            {
                result = true;
                break;
            }
        }
        return result;
    }
    
    
    // Method:             "HotspotGenerator(int seconds)"
    // What it Does:        Run this method whilst running a path in the game to build the actual mesh.
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