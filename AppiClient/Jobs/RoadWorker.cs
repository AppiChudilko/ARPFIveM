using System;
using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Jobs
{
    public class RoadWorker : BaseScript
    {
        public static bool IsStartWork = false;
        public static int Count = 0;
        public static bool IsProcess = false;
        public static int CurrentCheckpoint = -1;
        
        public static double[,] Pickups =
        {
            /*Сварка*/
            { 53.50541, -711.3873, 29.87694 },
            { 57.57128, -708.451, 30.00957 },
            { 56.72814, -701.3369, 30.09235 },
            { 90.61273, -616.3182, 29.7602 },
            { 91.80334, -607.3279, 30.17802 },
            { 91.80334, -607.3279, 30.17802 }
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
                    SetPedComponentVariation(GetPlayerPed(-1), 8, 59, 0, rand.Next(0, 2));
                    SetPedComponentVariation(GetPlayerPed(-1), 11, 0, 0, 2);
                }

                FindRandomPickup();
            }
        }

        public static void FindRandomPickup()
        {
            if (IsProcess || !IsStartWork) return;
            Random rand = new Random();

            int pickupId = rand.Next(0, 5);
            var pickupPos = new Vector3((float) Pickups[pickupId, 0], (float) Pickups[pickupId, 1], (float) Pickups[pickupId, 2]);
            CurrentCheckpoint = Managers.Checkpoint.CreateWithMarker(pickupPos, 1f, 1f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "job:roadworker:work");

            Managers.Blip.Create(pickupPos);
        }

        public static async void WorkProcess()
        {
            if (IsProcess || !IsStartWork) return;

            IsProcess = true;
            var pos = GetEntityCoords(GetPlayerPed(-1), true);
            
            for (int i = 0; i < Pickups.Length / 3; i++)
            {
                var pickupPos = new Vector3((float) Pickups[i, 0], (float) Pickups[i, 1], (float) Pickups[i, 2]);
                if (!(Main.GetDistanceToSquared(pos, pickupPos) < 2f)) continue;
                
                SetEntityCoords(GetPlayerPed(-1), pickupPos.X, pickupPos.Y, pickupPos.Z, true, false, false, true);
                User.PlayScenario("WORLD_HUMAN_CONST_DRILL");
                User.Freeze(PlayerId(), true);
            
                await Delay(200);
                User.IsBlockAnimation = true;
            }

            await Delay(20000);
            
            IsProcess = false;
            User.IsBlockAnimation = false;
            User.Freeze(PlayerId(), false);
            User.StopScenario();
            Count++;
            
            FindRandomPickup();
        }
        
        public static void TakeMoney()
        {
            int money = Count * 3 * User.Bonus;
            
            User.AddCashMoney(money);
            Coffer.RemoveMoney(money);

            Count = 0;
            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_118", $"{money:#,#}"));
        }
    }
}