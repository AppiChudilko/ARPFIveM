using System;
using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Jobs
{
    public class Builder : BaseScript
    {
        public static bool IsStartWork = false;
        public static int Count = 0;
        public static bool IsProcess = false;
        public static int CurrentCheckpoint = -1;
        
        public static double[,] Pickups =
        {
            /*Сварка*/
            { -172.2479, -991.1088, 253.1315, 79.50198, 0 },
            { -172.7199, -992.7151, 253.1314, 79.54372, 0 },
            { -173.3227, -995.3729, 253.1315, 76.74725, 0 },
            { -174.2205, -998.3889, 253.1315, 79.56352, 0 },
            { -170.3128, -1004.923, 253.1315, 163.8139, 0 },
            { -167.2567, -1005.97, 253.1315, 159.6735, 0 },
            { -164.6741, -1006.969, 253.1314, 160.206, 0 },
            
            /*Молоток*/
            { -152.5995, -983.616, 268.2276, 162.2194, 1 },
            { -163.3634, -955.0497, 268.2273, 69.25684, 1 },
            { -135.2024, -981.0098, 253.3519, 252.521, 1 },
            { -183.3535, -1017.348, 253.3521, 161.8717, 1 },
            { -177.6228, -968.9905, 253.3519, 69.92053, 1 },
            { -166.073, -962.3404, 268.2276, 74.31129, 1 },
            { -192.6118, -1010.344, 253.3521, 66.1328, 1 },
            { -177.2914, -1019.575, 253.3519, 165.48, 1 },
            { -192.6118, -1010.344, 253.3521, 66.1328, 1 },
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
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_117"));
                
                if (User.Skin.SEX == 1)
                {
                    SetPedComponentVariation(GetPlayerPed(-1), 3, 55, 0, 2);
                    SetPedComponentVariation(GetPlayerPed(-1), 8, 36, 0, 2);
                    SetPedComponentVariation(GetPlayerPed(-1), 11, 0, 0, 2);
                }
                else
                {
                    Random rand = new Random();
                    SetPedComponentVariation(GetPlayerPed(-1), 3, 30, 0, 2);
                    SetPedComponentVariation(GetPlayerPed(-1), 8, 59, rand.Next(0, 2), 2);
                    SetPedComponentVariation(GetPlayerPed(-1), 11, 0, 0, 2);
                }

                FindRandomPickup();
            }
        }

        public static void FindRandomPickup()
        {
            if (IsProcess || !IsStartWork) return;
            Random rand = new Random();

            int pickupId = rand.Next(0, 15);
            var pickupPos = new Vector3((float) Pickups[pickupId, 0], (float) Pickups[pickupId, 1], (float) Pickups[pickupId, 2]);
            CurrentCheckpoint = Managers.Checkpoint.CreateWithMarker(pickupPos, 1f, 1f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "job:builder:work");

            Managers.Blip.Create(pickupPos);
        }

        public static async void WorkProcess()
        {
            if (IsProcess || !IsStartWork) return;

            IsProcess = true;
            var pos = GetEntityCoords(GetPlayerPed(-1), true);
            
            for (int i = 0; i < Pickups.Length / 5; i++)
            {
                var pickupPos = new Vector3((float) Pickups[i, 0], (float) Pickups[i, 1], (float) Pickups[i, 2]);
                if (!(Main.GetDistanceToSquared(pos, pickupPos) < 2f)) continue;
                
                SetEntityCoords(GetPlayerPed(-1), pickupPos.X, pickupPos.Y, pickupPos.Z, true, false, false, true);
                User.PedRotation((float) Pickups[i, 3]);
                User.PlayScenario(Convert.ToInt32(Pickups[i, 4]) == 0 ? "WORLD_HUMAN_WELDING" : "WORLD_HUMAN_HAMMERING");
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
            User.Data.skill_builder++;
            Sync.Data.Set(User.GetServerId(), "skill_builder", User.Data.skill_builder);
            
            FindRandomPickup();
        }
        
        public static void TakeMoney()
        {
            int money = Count;

            if (User.Data.skill_builder >= 2 && User.Data.skill_builder < 4)
                money = money * 2;
            else if (User.Data.skill_builder >= 4 && User.Data.skill_builder < 6)
                money = money * 3;
            else if (User.Data.skill_builder >= 6 && User.Data.skill_builder < 8)
                money = money * 4;
            else if (User.Data.skill_builder >= 8 && User.Data.skill_builder < 10)
                money = money * 5;
            else if (User.Data.skill_builder >= 10)
                money = money * 6;
            else
                money = money * 1;
            
            money = money * User.Bonus;
            
            User.AddCashMoney(money);
            Coffer.RemoveMoney(money);

            Count = 0;
            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_118", $"{money:#,#}"));
        }
    }
}