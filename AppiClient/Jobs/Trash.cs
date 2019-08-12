using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Jobs
{
    public class Trash : BaseScript
    {
        public static bool IsStartWork = false;
        
        private static readonly List<string> _trashProp = new List<string>
        {
            "prop_rub_binbag_01",
            "prop_rub_binbag_01b",
            "prop_rub_binbag_03",
            "prop_rub_binbag_03b",
            "prop_rub_binbag_04",
            "prop_rub_binbag_05",
            "prop_rub_binbag_06",
            "prop_rub_binbag_08",
            "prop_rub_boxpile_01",
            "prop_rub_boxpile_02",
            "prop_rub_boxpile_03",
            "prop_rub_boxpile_04",
            "prop_rub_boxpile_04b",
            "prop_rub_boxpile_05",
            "prop_rub_boxpile_06",
            "prop_rub_boxpile_07",
            "prop_rub_boxpile_08",
            "prop_rub_boxpile_09",
            "prop_rub_boxpile_10"
        };

        public Trash()
        {
            Tick += Sec2Timer;
        }
        
        public static void StartOrEnd()
        {
            if (IsStartWork)
            {                
                IsStartWork = false;
                Characher.UpdateCloth(false);
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_115"));
            }
            else
            {
                IsStartWork = true;
                
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
                    SetPedComponentVariation(GetPlayerPed(-1), 8, 59, rand.Next(0, 2), 2);
                    SetPedComponentVariation(GetPlayerPed(-1), 11, 0, 0, 2);
                }
            }
        }
        
        public static async void UnloadTrash(CitizenFX.Core.Vehicle vehicle)
        {
            var unloadPos = new Vector3(1544.018f, -2132.836f, 77.20142f);
            if (Main.GetDistanceToSquared(unloadPos, GetEntityCoords(GetPlayerPed(-1), true)) > 20f)
            {
                Notification.SendWithTime("~r~Вы слишком далеко");
                
                Managers.Checkpoint.CreateWithMarker(unloadPos, 15f, 15f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "checkpoint:withdelete");
                User.SetWaypoint(unloadPos.X, unloadPos.Y);
                return;
            }
            
            int vId = GetHashKey(Managers.Vehicle.GetVehicleNumber(vehicle.Handle));
            if (!await Sync.Data.Has(vId, "Trash"))
            {
                Notification.SendWithTime("~r~Транспорт пуст");
                return;
            }
            
            int countTrash = (int) await Sync.Data.Get(vId, "Trash");
            User.GiveJobMoney(countTrash * 10);
            Notification.SendWithTime("~g~Вы разгрузили транспорт");
            Sync.Data.Reset(vId, "Trash");
        }
        
        public static void TakeTrash()
        {
            if (Sync.Data.HasLocally(User.GetServerId(), "HasTrash"))
            {
                Notification.SendWithTime("~r~Загрузите мусор в транспорт");
                return;
            }

            var plPos = GetEntityCoords(GetPlayerPed(-1), true);
            foreach (var propName in _trashProp)
            {
                int propHandle = GetClosestObjectOfType(plPos.X, plPos.Y, plPos.Z, 1f, (uint) GetHashKey(propName), false, false, false);
                if (propHandle == 0) continue;
                new Prop(propHandle).Delete();
                Shared.TriggerEventToAllPlayers("ARP:DeleteObject", ObjToNet(propHandle));
                
                Sync.Data.SetLocally(User.GetServerId(), "HasTrash", true);
                Notification.Send("~g~Вы взяли мусор\nЗагрузите его в транспорт");
                Notification.Send("~g~Нажмите ~b~E~g~ возле ТС чтобы загрузить");
                
                User.PlayAnimation("pickup_object","pickup_low", 8);
                return;
            }
        }
        
        public static async void PutTrash()
        {
            var plPos = GetEntityCoords(GetPlayerPed(-1), true);
            var vehicle = Main.FindNearestVehicle(plPos, 5f);

            if (vehicle.Handle == 0)
            {
                Notification.SendWithTime("~r~Вы слишком далеко");
                return;
            }

            if (vehicle.Model != 1917016601 && vehicle.Model != -1255698084)
            {
                Notification.SendWithTime("~r~Загрузить можно только в рабочий транспорт");
                return;
            }
            
            if (!Sync.Data.HasLocally(User.GetServerId(), "HasTrash"))
            {
                Notification.SendWithTime("~r~Вы не взяли мусор");
                return;
            }

            int vId = GetHashKey(Managers.Vehicle.GetVehicleNumber(vehicle.Handle));
            if (await Sync.Data.Has(vId, "Trash"))
            {
                int countTrash = (int) await Sync.Data.Get(vId, "Trash");

                if (countTrash > 50)
                {
                    Notification.SendWithTime("~r~Транспорт переполнен");
                    return;
                }

                Sync.Data.ResetLocally(User.GetServerId(), "HasTrash");
                Sync.Data.Set(vId, "Trash", ++countTrash);
                Notification.SendWithTime("~g~Вы загрузили мусор в транспорт");
            }
            else
            {
                Sync.Data.ResetLocally(User.GetServerId(), "HasTrash");
                Sync.Data.Set(vId, "Trash", 1);
                Notification.SendWithTime("~g~Вы загрузили мусор в транспорт");
            }
        }

        private static async Task Sec2Timer()
        {
            await Delay(2000);

            if (IsStartWork)
            {
                var plPos = GetEntityCoords(GetPlayerPed(-1), true);
                if (_trashProp.Select(propName => GetClosestObjectOfType(plPos.X, plPos.Y, plPos.Z, 1f, (uint) GetHashKey(propName), false, false, false)).Any(prop => prop != 0))
                {
                    Notification.Send("Нажмите ~b~E~s~ чтобы взять мусор");
                }
            }
        }
    }
}