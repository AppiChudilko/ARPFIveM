using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class Text3D : BaseScript
    {
        public static List<Text3DData> Text3DDataList = new List<Text3DData>();
        public static int Inc = 0;
        public static float LoadRange3DText = 200f;

        public static Text3DColorData Red = new Text3DColorData(244, 67, 54, 255);
        public static Text3DColorData Green = new Text3DColorData(139, 195, 74, 255);
        public static Text3DColorData Blue = new Text3DColorData(33, 150, 243, 255);
        public static Text3DColorData Yellow = new Text3DColorData(255, 235, 59, 255);
        public static Text3DColorData Blue100 = new Text3DColorData(187, 222, 251, 255);
        public static Text3DColorData White = new Text3DColorData(255, 255, 255, 255); 
  
        public Text3D()
        {
            Tick += Draw3DText;
        }

        public static int Create(string text, Vector3 pos, float range, float fontSize, int r, int g, int b, int a)
        {
            Inc++;

            Text3DDataList.Add(
                new Text3DData
                {
                    Id = Inc,
                    Text = text,
                    X = pos.X,
                    Y = pos.Y,
                    Z = pos.Z,
                    Range = range,
                    FontSize = fontSize,
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
            return (from item in Text3DDataList where item.Id == markerId select Text3DDataList.Remove(item)).FirstOrDefault();
        }
        
        private static async Task Draw3DText()
        {            
            var pos = GetEntityCoords(GetPlayerPed(-1), true);
            foreach (var item in Text3DDataList)
            {
                var markerPos = new Vector3(item.X, item.Y, item.Z);
                if (!(Main.GetDistanceToSquared(pos, markerPos) < item.Range)) continue;
                UI.Draw3DText(item.Text, markerPos, item.FontSize, item.R, item.G, item.B, item.A);
            }
        }
    }
}

public class Text3DData
{
    public int Id { get; set; }
    public string Text { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public float FontSize { get; set; }
    public float Range { get; set; }
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }
    public int A { get; set; }
}

public class Text3DColorData
{
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }
    public int A { get; set; }
    
    public Text3DColorData(int r, int g, int b, int a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
}