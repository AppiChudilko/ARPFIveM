using CitizenFX.Core;

namespace Client.Managers
{
    public class Blip : BaseScript
    {
        public static CitizenFX.Core.Blip LocallyBlip = null;

        public static void Create(Vector3 pos, int sprite = 1, int color = 59, string name = "Работа", float size = 0.8f)
        {
            Delete(); 
                
            LocallyBlip = World.CreateBlip(pos);
            LocallyBlip.Sprite = (BlipSprite) sprite;
            LocallyBlip.Color = (BlipColor) color;
            LocallyBlip.Name = name;
            LocallyBlip.IsShortRange = true;
            LocallyBlip.Scale = size;
        }

        public static void ShowRoute(bool show)
        {
            if (LocallyBlip != null)
                LocallyBlip.ShowRoute = show;
        }

        public static void Delete()
        {
            if (LocallyBlip != null)
                LocallyBlip.Delete();
        }
    }
}