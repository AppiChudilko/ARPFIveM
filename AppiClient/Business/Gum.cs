using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Business
{
    public class Gum : BaseScript
    {   
        public static double[,] GumMarkers =
        {
            //0 - Подтягиваться, 1 - Качаться
            {-1224.893, -1600.197, 3.186051, 88.43168, 0},
            {-1204.734, -1564.339, 3.609551, 33.66126, 0},
            {-1244.637, -1614.242, 3.16043, 35.84436, 0},
            {-1200.067, -1571.155, 3.609395, 212.4805, 0},
            {1643.524, 2527.616, 44.56486, 49.52977, 0},
            {1648.871, 2529.834, 44.56486, 232.6238, 0},
            {-1210.138, -1561.369, 3.607929, 70.86428, 1},
            {-1202.65, -1565.784, 3.611413, 34.74633, 1},
            {1644.24, 2533.203, 44.56487, 68.75525, 1},
            { 1639.067, 2527.714, 44.56487, 13.69811, 1}
        };

        public static bool IsStart = false;
        
        public static void LoadAll()
        {
            var blip = World.CreateBlip(new Vector3(-1204.734f, -1564.339f, 4.609551f));
            blip.Sprite = (BlipSprite) 311;
            blip.Name = Lang.GetTextToPlayer("_lang_64");
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            for (int i = 0; i < GumMarkers.Length / 5; i++)
            {
                Vector3 gumPos = new Vector3((float) GumMarkers[i, 0], (float) GumMarkers[i, 1], (float) GumMarkers[i, 2]);

                Managers.Checkpoint.Create(gumPos, 1.4f, "gum");
                //Managers.Marker.Create(gumPos - new Vector3(0, 0, 1), 1f, 1f, Managers.Marker.Blue.R, Managers.Marker.Blue.G, Managers.Marker.Blue.B, Managers.Marker.Blue.A);
            }
        }

        public static async void Start()
        {
            if (IsStart && IsGum())
            {
                Stop();
                return;
            }
            
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            
            for (int i = 0; i < GumMarkers.Length / 5; i++)
            {
                Vector3 gumPos = new Vector3((float) GumMarkers[i, 0], (float) GumMarkers[i, 1], (float) GumMarkers[i, 2]);
                if (Main.GetDistanceToSquared(playerPos, gumPos) > 1.5f) continue;
                
                IsStart = true;
                SetEntityCoords(GetPlayerPed(-1), gumPos.X, gumPos.Y, gumPos.Z, true, false, false, true);

                if ((int) GumMarkers[i, 4] == 0)
                {
                    if (Client.Sync.Data.HasLocally(User.GetServerId(), "PROP_HUMAN_MUSCLE_CHIN_UPS"))
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_65"));
                        return;
                    }
                    
                    Client.Sync.Data.SetLocally(User.GetServerId(), "PROP_HUMAN_MUSCLE_CHIN_UPS", true);
                    
                    User.PedRotation((float) GumMarkers[i, 3]);
                    User.PlayScenario("PROP_HUMAN_MUSCLE_CHIN_UPS");
                    await Delay(30000 + (User.Data.mp0_strength * 500));
                    User.PlayScenario("PROP_HUMAN_MUSCLE_CHIN_UPS");

                    if (User.Data.mp0_strength < 99)
                    {
                        User.Data.mp0_strength++;
                        Client.Sync.Data.Set(User.GetServerId(), "mp0_strength", User.Data.mp0_strength);
                    }
                    
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_65"));
                    
                    await Delay(60000);
                    
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "PROP_HUMAN_MUSCLE_CHIN_UPS");
                }
                else
                {
                    if (Client.Sync.Data.HasLocally(User.GetServerId(), "WORLD_HUMAN_MUSCLE_FREE_WEIGHTS"))
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_65"));
                        return;
                    }
                    
                    Client.Sync.Data.SetLocally(User.GetServerId(), "WORLD_HUMAN_MUSCLE_FREE_WEIGHTS", true);
                    
                    User.PlayScenario("WORLD_HUMAN_MUSCLE_FREE_WEIGHTS");
                    await Delay(30000 + (User.Data.mp0_strength * 500));
                    User.PlayScenario("WORLD_HUMAN_MUSCLE_FREE_WEIGHTS");
                    
                    if (User.Data.mp0_strength < 99)
                    {
                        User.Data.mp0_strength++;
                        Client.Sync.Data.Set(User.GetServerId(), "mp0_strength", User.Data.mp0_strength);
                    }
                    
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_65"));
                    
                    await Delay(60000);
                    
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "WORLD_HUMAN_MUSCLE_FREE_WEIGHTS");
                }
            }
        }

        public static bool IsGum()
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            for (int i = 0; i < GumMarkers.Length / 5; i++)
            {
                Vector3 gumPos = new Vector3((float) GumMarkers[i, 0], (float) GumMarkers[i, 1], (float) GumMarkers[i, 2]);
                if (Main.GetDistanceToSquared(playerPos, gumPos) > 1.5f) continue;
                return true;
            }
            return false;
        }

        public static void Stop()
        {
            IsStart = false;
            User.PlayScenario("forcestop");
        }
        
        
        /*
        Подтягиваться
        
        -1224.893, -1600.197, 4.186051, 88.43168
        -1204.734, -1564.339, 4.609551, 33.66126 //Блип
        -1244.637, -1614.242, 4.16043, 35.84436
        -1200.067, -1571.155, 4.609395, 212.4805
        
        Качаться
        
        -1210.138, -1561.369, 4.607929, 70.86428
        -1202.65, -1565.784, 4.611413, 34.74633
        */
    }
}