using System;
using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Jobs
{
    public class Bugstars : BaseScript
    {
        public static bool IsProcess = false;
        
        public static void FindHouse()
        {
            if (IsProcess)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_109"));
                return;
            }
            
            IsProcess = true;
            var h = House.GetRandomHouseInLosSantos();
            var pos = new Vector3(h.x, h.y, h.z);
            
            Notification.SendPicture(Lang.GetTextToPlayer("_lang_110"), Lang.GetTextToPlayer("_lang_111"), "323-555-4122", "CHAR_MICHAEL", Notification.TypeChatbox);  
            
            Managers.Blip.Create(pos);
            Managers.Checkpoint.CreateWithMarker(pos, 1.1f, 1.1f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "job:bugstars:work");
            User.SetWaypoint(pos.X, pos.Y);
        }

        public static void TakeTool()
        {
            if (Sync.Data.HasLocally(User.GetServerId(), "BugstarsTool"))
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_112"));
                return;
            }
            
            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_113"));
            Sync.Data.SetLocally(User.GetServerId(), "BugstarsTool", true);
        }

        public static async void WorkProcess()
        {
            DoScreenFadeOut(500);

            while (IsScreenFadingOut())
                await Delay(1);
            
            Managers.Blip.Delete();
            await Delay(30000);
            Sync.Data.ResetLocally(User.GetServerId(), "BugstarsTool");
            IsProcess = false;
            
            DoScreenFadeIn(500);
            
            while (IsScreenFadingIn())
                await Delay(1);
            
            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_114"));
            var rand = new Random();
            User.GiveJobMoney(30 + rand.Next(10));
        }
    }
}