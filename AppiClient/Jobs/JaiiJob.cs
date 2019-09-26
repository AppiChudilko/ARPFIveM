using System;
using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;
using Pickup = CitizenFX.Core.Pickup;

namespace Client.Jobs
{
    public class JailJob : BaseScript
    {
        public static readonly Vector3 CareerPos = new Vector3(2951.45f, 2791.428f, 40.08484f);
        public static readonly Vector3 JailTeleportPos = new Vector3(1774.74f, 2552.335f, 44.56499f);
        public static readonly Vector3 StartJailJobPos = new Vector3(2945.142f, 2746.357f, 42.34696f );
        public static readonly Vector3 JailCareerJobEndPos = new Vector3(2945.142f, 2746.357f, 42.34696f);
        
        public static bool IsStartWork = false;
        public static int Count = 0;
        public static bool IsProcess = false;
        public static int CurrentCheckpoint = -1;

        public static double[,] Pickups =
        {
            /*Точки работы*/
            {2938.384, 2777.199, 38.25064, 303.3068, 1},
            {2928.645, 2792.282, 39.43091, 302.8207, 1},
            {2930.979, 2806.117, 41.2011, 277.9193, 1},
            {2946.037, 2819.363, 41.76299, 235.299, 1},
            {2975.084, 2794.495, 39.8634, 328.7229, 1},
            {2970.787, 2776.091, 37.31322, 343.1534, 1},
            {2955.083, 2772.675, 38.29709, 347.5083, 1},

        };

        public static void JailJobTeleportFromJail()
        {
            User.Teleport(JailCareerJobEndPos, 10);
        }
        
        public static void JailJobTeleportToJail()
        {
            User.Teleport(JailTeleportPos, 10);
        }

        public static void StartOrEndJailJob()
        {
            if (User.Data.jail_time <= 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_128"));
                return;
            }
            if (IsStartWork)
            {
                TakeMoneyJail();

                Managers.Checkpoint.DeleteWithMarker(CurrentCheckpoint);
                
                IsStartWork = false;
                IsProcess = false;
                CurrentCheckpoint = -1; 
                
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_127"));
                
                Managers.Blip.Delete();
                User.Teleport(JailTeleportPos, 10);
            }
            else
            {
                IsStartWork = true;
                Count = 0;
                
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_125"));
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_126"));

                FindRandomPickupJailJob();

                User.Teleport(CareerPos, 10);
            }
        }
        
        public static void FindRandomPickupJailJob()
        {
            if (IsProcess || !IsStartWork) return;
            Random rand = new Random();

            int pickupId = rand.Next(0, 7);
            var pickupPos = new Vector3((float) Pickups[pickupId, 0], (float) Pickups[pickupId, 1], (float) Pickups[pickupId, 2]);
            CurrentCheckpoint = Managers.Checkpoint.CreateWithMarker(pickupPos, 1.2f, 1.5f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "job:jailjob:work");

            Managers.Blip.Create(pickupPos);
        }
        
        public static async void WorkProcessJailJob()
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
            
            FindRandomPickupJailJob();
        }
        
        public static void TakeMoneyJail()
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
            if(money != 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_118", $"{money:#,#}"));
            }
            
            User.AddCashMoney(money);
            Coffer.RemoveMoney(money);

            Count = 0;
            
        }
    }
}