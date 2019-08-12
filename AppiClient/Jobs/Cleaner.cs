using System;
using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Jobs
{
    public class Cleaner : BaseScript
    {
        public static bool IsStartWork = false;
        public static int Count = 0;
        public static bool IsProcess = false;
        public static int CurrentCheckpoint = -1;
        
        public static double[,] Pickups =
        {
            { -1536.631, -451.4576, 34.88205, 137.3628 },
            { -1532.549, -454.8775, 34.88456, 139.3095 },
            { -1553.697, -446.5077, 39.51906, 142.7419 },
            { -1535.673, -459.0544, 39.52384, 142.13 },
            { -1540.909, -454.7619, 39.51913, 137.2621 },
            { -1541.229, -429.4637, 34.59196, 51.92725 },
            { -1554.131, -441.6807, 39.51905, 51.12228 },
            { -1533.775, -420.5667, 34.59194, 37.03086 },
            { -1538.859, -456.4115, 39.52203, 142.7622 },
            { -1534.811, -462.4519, 34.44516, 30.1082 },
            { -1528.802, -462.5171, 34.4021, 214.4953 }
        };
        
        public static void StartOrEnd()
        {
            if (IsStartWork)
            {
                TakeMoney();

                Managers.Checkpoint.DeleteWithMarker(CurrentCheckpoint);
                
                IsStartWork = false;
                IsProcess = false;
                CurrentCheckpoint = -1;
                
                Characher.UpdateCloth(false);
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_115"));
                
                Managers.Blip.Delete();
            }
            else
            {
                IsStartWork = true;
                Count = 0;
                
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_116"));
                FindRandomPickup();
            }
        }

        public static void FindRandomPickup()
        {
            if (IsProcess || !IsStartWork) return;
            Random rand = new Random();

            int pickupId = rand.Next(0, 10);
            var pickupPos = new Vector3((float) Pickups[pickupId, 0], (float) Pickups[pickupId, 1], (float) Pickups[pickupId, 2]);
            CurrentCheckpoint = Managers.Checkpoint.CreateWithMarker(pickupPos, 1f, 1f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "job:cleaner:work");
                
            Managers.Blip.Create(pickupPos);
        }

        public static async void WorkProcess()
        {
            if (IsProcess || !IsStartWork) return;

            IsProcess = true;
            var pos = GetEntityCoords(GetPlayerPed(-1), true);
            
            for (int i = 0; i < Pickups.Length / 4; i++)
            {
                var pickupPos = new Vector3((float) Pickups[i, 0], (float) Pickups[i, 1], (float) Pickups[i, 2]);
                if (!(Main.GetDistanceToSquared(pos, pickupPos) < 2f)) continue;
                
                SetEntityCoords(GetPlayerPed(-1), pickupPos.X, pickupPos.Y, pickupPos.Z, true, false, false, true);
                User.PedRotation((float) Pickups[i, 3]);
                User.PlayScenario("WORLD_HUMAN_MAID_CLEAN");
                User.Freeze(PlayerId(), true);
            
                await Delay(200);
                User.IsBlockAnimation = true;
            }

            await Delay(15000);
            
            IsProcess = false;
            User.IsBlockAnimation = false;
            User.Freeze(PlayerId(), false);
            User.StopScenario();
            Count++;
            User.Data.skill_scrap++;
            Sync.Data.Set(User.GetServerId(), "skill_scrap", User.Data.skill_scrap);
            
            FindRandomPickup();
        }
        
        public static void TakeMoney()
        {
            int money = Count;

            if (User.Data.skill_scrap >= 4 && User.Data.skill_scrap < 6)
                money = money * 2;
            else if (User.Data.skill_scrap >= 6 && User.Data.skill_scrap < 8)
                money = money * 3;
            else if (User.Data.skill_scrap >= 8 && User.Data.skill_scrap < 20)
                money = money * 4;
            else if (User.Data.skill_scrap >= 20)
                money = money * 5;

            money = money * User.Bonus;
            
            User.AddCashMoney(money);
            Coffer.RemoveMoney(money);

            Count = 0;
            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_118", $"{money:#,#}"));
        }
    }
}