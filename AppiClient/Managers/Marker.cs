using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class Marker : BaseScript
    {
        public static List<MarkerData> MarkerDataList = new List<MarkerData>();
        public static int Inc = 0;
        public static float LoadRangeMarker = 200f;

        public static MarkerColorData Red = new MarkerColorData(244, 67, 54, 100);
        public static MarkerColorData Green = new MarkerColorData(139, 195, 74, 100);
        public static MarkerColorData Blue = new MarkerColorData(33, 150, 243, 100);
        public static MarkerColorData Yellow = new MarkerColorData(255, 235, 59, 100);
        public static MarkerColorData Blue100 = new MarkerColorData(187, 222, 251, 100);
        public static MarkerColorData White = new MarkerColorData(255, 255, 255, 100); 
  
        public Marker()
        {
            Tick += DrawMarkers;
            
            //Create(new Vector3(-662.006f, 679.044f, 152.911f), 1f, 1f, Red.R, Red.G, Red.B, Red.A);
        }

        public static int Create(Vector3 pos, float range, float height, int r, int g, int b, int a)
        {
            Inc++;

            MarkerDataList.Add(
                new MarkerData
                {
                    Id = Inc,
                    X = pos.X,
                    Y = pos.Y,
                    Z = pos.Z,
                    Range = range,
                    Height = height,
                    R = r,
                    G = g,
                    B = b,
                    A = a
                }
            );
            return Inc;
        }
        
        
        public static bool Delete(int markerId)
        {
            return (from item in MarkerDataList where item.Id == markerId select MarkerDataList.Remove(item)).FirstOrDefault();
        }
        
        private static async Task DrawMarkers()
        {            
            var pos = GetEntityCoords(GetPlayerPed(-1), true);
            foreach (var item in MarkerDataList)
            {
                var markerPos = new Vector3(item.X, item.Y, item.Z);
                if (!(Main.GetDistanceToSquared(pos, markerPos) < LoadRangeMarker)) continue;
                UI.DrawMarker(1, markerPos, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(item.Range, item.Range, item.Height), item.R, item.G, item.B, item.A);
            }
        }
    }
}

public class MarkerData
{
    public int Id { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public float Range { get; set; }
    public float Height { get; set; }
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }
    public int A { get; set; }
}

public class MarkerColorData
{
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }
    public int A { get; set; }
    
    public MarkerColorData(int r, int g, int b, int a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
}