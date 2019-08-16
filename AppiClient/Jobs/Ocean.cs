using System;
using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Jobs
{
    public class Ocean : BaseScript
    {
        public static bool IsStartWork = false;
        public static int Count = 0;
        public static bool IsProcess = false;
        public static int CurrentCheckpoint = -1;
        
        public static double[,] Pickups =
        {
            /*Точки сбора*/
            { -1430.8, -1383.99, 3, 79.50198, 1 },
            { -1397.73, -1454.09, 3.19, 79.54372, 1 },
            { -1482.42, -1518.01, 1, 76.74725, 1 },
            { -1535.37, -1311.64, 0.12, 79.56352, 1 },
            { -1505.4, -1369.41, 1, 159.6735, 1 },
            { -1493.39, -1440.73, -0.2, 160.206, 1 },
            { -1547.31, -1249, 1.2, 162.2194, 1 },
            { -1592, -1194, 0.56, 69.25684, 1 },
            { -1466, -1092, 0, 252.521, 1 },
            { -1471, -1104, -0.5, 161.8717, 1 },
            { -1474, -1107, -0.5, 69.92053, 1 },
            { -1610, -1154, -0.5, 74.31129, 1 },
            { -1436, -1565, -0.5, 66.1328, 1 },
            { -1388, -1596, 1.17, 165.48, 1 },
            { -1356, -1618, 1.5, 66.1328, 1 },
            { -1542, -1277, 0, 66.1328, 1 },
            { -1528, -1328, 0.31, 66.1328, 1 },
            { -1497, -1348, 0.93, 66.1328, 1 },
            { -1505, -1393, 0.6, 66.1328, 1 },
            { -1454, -1467, 1.19, 66.1328, 1 },
            { -1451, -1439, 1.58, 66.1328, 1 },
            
        };
        
        public static void StartOrEndOcean()
        {
            if (IsStartWork)
            {
                TakeMoneyOcean();

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
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_124"));
                
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

                FindRandomPickupOcean();
            }
        }

        public static void FindRandomPickupOcean()
        {
            if (IsProcess || !IsStartWork) return;
            Random rand = new Random();

            int pickupId = rand.Next(0, 15);
            var pickupPos = new Vector3((float) Pickups[pickupId, 0], (float) Pickups[pickupId, 1], (float) Pickups[pickupId, 2]);
            CurrentCheckpoint = Managers.Checkpoint.CreateWithMarker(pickupPos, 1.2f, 1.5f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "job:ocean:work");

            Managers.Blip.Create(pickupPos);
        }

        public static async void WorkProcessOcean()
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
                User.PlayScenario("WORLD_HUMAN_GARDENER_PLANT");
                User.Freeze(PlayerId(), true);
            
                await Delay(110);
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
            
            FindRandomPickupOcean();
        }
        
        public static void TakeMoneyOcean()
        {
            int money = Count;
/*
            if (User.Data.skill_builder >= 2 && User.Data.skill_builder < 4)
                money = money * 2;
            else if (User.Data.skill_builder >= 4 && User.Data.skill_builder < 6)
                money = money * 3;
            else if (User.Data.skill_builder >= 6 && User.Data.skill_builder < 8)        //надо сделать когда будут навыки добавлены
                money = money * 4;
            else if (User.Data.skill_builder >= 8 && User.Data.skill_builder < 10)
                money = money * 5;
            else if (User.Data.skill_builder >= 10)
                money = money * 6;
            else */
                money = money * 3;
            
            money = money * User.Bonus;
            
            User.AddCashMoney(money);
            Coffer.RemoveMoney(money);

            Count = 0;
            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_118", $"{money:#,#}"));
        }
    }
}