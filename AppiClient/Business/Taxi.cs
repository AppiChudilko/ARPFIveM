using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Business
{
    public class Taxi : BaseScript
    {   
        public static Dictionary<string, int> TaskList = new Dictionary<string, int>();
        public static Dictionary<string, Vector3> TaskList2 = new Dictionary<string, Vector3>();
        
        public static bool IsFindNpc = false;
        public static bool IsTask = false;
        public static bool IsTaskRoad = false;
        public static bool IsTaskRoadFinish = false;
        public static int CheckpointId = -1;
        public static int Warning = 0;
        public static float RoadDistance = 0;
        public static CitizenFX.Core.Ped CurrentPed = null;
        public static List<CitizenFX.Core.Ped> PedList = new List<CitizenFX.Core.Ped>();
        
        public Taxi()
        {
            EventHandlers.Add("ARP:AcceptTaxi", new Action<string>(AcceptTaxi));
            EventHandlers.Add("ARP:GetTaxi", new Action(GetTaxi));
            EventHandlers.Add("ARP:SendNotifTaxi", new Action<string, int, string, string, float, float, float>(SendNotifTaxi));
            Tick += SecTimer;
        }

        private static async Task SecTimer()
        {
            await Delay(1000);
            var pos = GetEntityCoords(GetPlayerPed(-1), true);
            if (IsFindNpc)
            {
                if (IsPedInAnyVehicle(PlayerPedId(), true) && UI.GetCurrentSpeed() < 1)
                {
                    await Delay(5000);

                    foreach (var ped in Main.GetPedListOnRadius(pos, 20f))
                    {
                        if (User.IsAnimal(ped.Model.Hash)) continue;
                        if (IsPedInAnyVehicle(ped.Handle, true)) continue;
                        if (PedList.Contains(ped)) continue;
                        
                        CurrentPed = ped;
                        PedList.Add(CurrentPed);
                        SetEntityAsMissionEntity(CurrentPed.Handle, true, true);
                        IsTask = true;
                        IsFindNpc = false;
                        break;
                    }
                    if (IsTask)
                    {
                        var v = new CitizenFX.Core.Vehicle(GetVehiclePedIsUsing(PlayerPedId())) { LockStatus = VehicleLockStatus.Unlocked };
                        TaskEnterVehicle(CurrentPed.Handle, v.Handle, 15000, 2, 1.0f, 1, 0);
                    }
                }
                else
                {
                    ResetAllTask();
                }
            }

            if (IsTask)
            {
                if (IsPedInAnyVehicle(PlayerPedId(), true))
                {
                    if (!IsTaskRoad)
                    {
                        if (IsPedInAnyVehicle(PlayerPedId(), false) && IsPedInAnyVehicle(CurrentPed.Handle, false))
                        {
                            IsTaskRoad = true;
                            new CitizenFX.Core.Vehicle(GetVehiclePedIsUsing(PlayerPedId()))
                            {
                                LockStatus = VehicleLockStatus.StickPlayerInside
                            };
                            var house = House.GetRandomHouseInLosSantos();
                            var housePos = new Vector3(house.x, house.y, house.z);
                            //User.SetWaypoint(house.x, house.y);
                            Managers.Blip.Create(housePos);
                            Managers.Blip.ShowRoute(true);

                            RoadDistance = CalculateTravelDistanceBetweenPoints(pos.X, pos.Y, pos.Z, house.x, house.y, house.z);
                            if (RoadDistance < 150)
                                RoadDistance = Main.GetDistanceToSquared(housePos, pos);

                            CheckpointId = Managers.Checkpoint.Create(new Vector3(house.x, house.y, house.z), 50f,
                                "taxi:finish");
                            Notification.SendWithTime("~g~Маршрут построен");
                        }
                    }
                    else
                    {
                        if (UI.GetCurrentSpeed() > 55)
                        {
                            Warning++;
                            Notification.SendWithTime("~y~Не превышайте скорость");
                            Notification.SendWithTime("~y~Итоговая стоимость поездки будет ниже");
                            await Delay(5000);
                        }
                    }

                    if (IsTaskRoadFinish && UI.GetCurrentSpeed() < 1)
                    {
                        IsTask = false;
                        var v = new CitizenFX.Core.Vehicle(GetVehiclePedIsUsing(PlayerPedId())) { LockStatus = VehicleLockStatus.Unlocked };
                        TaskLeaveVehicle(CurrentPed.Handle, GetVehiclePedIsUsing(CurrentPed.Handle), 1);
                        int money = Convert.ToInt32(RoadDistance / 20) * User.Bonus;
                        if (money > 1000)
                            money = 1000;

                        int star = 5;
                        if (Warning > 0)
                        {
                            if (Warning > 9)
                                Warning = 9;
                            star = Convert.ToInt32(Math.Round((10 - Warning) / 2f, 1));
                            int moneyWarning = Warning * 5;
                            if (moneyWarning >= money)
                                money = 20;
                            else
                                money = money - moneyWarning;
                            Notification.SendWithTime($"~y~Сумма штрафа: ~s~${moneyWarning:#,#}");
                        }
                        else
                        {
                            money = money + 20;
                            Notification.SendWithTime("~b~Бонус за поездку: ~s~$20");
                        }
                        
                        int moneyTaxi = Convert.ToInt32(money * 0.1);
                        int moneyUser = Convert.ToInt32(money * 0.9);
                        Notification.SendWithTime($"~g~Вы заработали: ~s~${moneyUser:#,#}");
                        Notification.SendWithTime($"~g~Отдали таксопарку: ~s~${moneyTaxi:#,#}");
                        Notification.SendWithTime($"~g~Оценка за поездку: ~s~{star} зв.");
                        User.AddCashMoney(moneyUser);
                        
                        if (v.Model.Hash == 2088999036)
                            Business.AddMoney(92, moneyTaxi);
                        else
                            Business.AddMoney(114, moneyTaxi);

                        await Delay(3500);
                        
                        foreach (var vehicleDoor in v.Doors)
                        {
                            if (vehicleDoor.IsOpen)
                                vehicleDoor.Close();
                        }
                        ResetAllTask();
                    }
                }
                else
                {
                    ResetAllTask();
                }
            }
        }

        public static void SendNotifTaxi(string phone, int id, string zone, string street, float x, float y, float z)
        {
            if (!IsPedInAnyVehicle(PlayerPedId(), true)) return;
            var veh = GetVehiclePedIsUsing(PlayerPedId());
            if (GetPedInVehicleSeat(veh, -1) != PlayerPedId()) return;
            var v = new CitizenFX.Core.Vehicle(veh);
            if (v.Model.Hash != -956048545 && v.Model.Hash != 1208856469 && v.Model.Hash != 1884962369 && v.Model.Hash != 2088999036) return;

            int count = TaskList.Count;
            ++count;
            
            TaskList.Add(count + ". " + phone, id);
            TaskList2.Add(count + ". " + phone, new Vector3(x, y, z));
            Notification.SendPicture($"~y~Район:~s~ {zone}\n~y~Улица:~s~ {street}", "Заказ", phone, "CHAR_TAXI", Notification.TypeChatbox);
        }

        public static void AcceptTaxi(string phone)
        {
            Notification.SendPicture("Водитель уже в пути, ожидайте", "Диспетчер", phone, "CHAR_TAXI", Notification.TypeChatbox);
            Sync.Data.Reset(User.GetServerId(), "GetTaxi");
        }

        public static async void GetTaxi()
        {
            if (await Sync.Data.Has(User.GetServerId(), "GetTaxi"))
            {
                Notification.SendWithTime("~r~Нельзя так часто вызывать такси");
                return;
            }
            var zone = UI.GetPlayerZoneName();
            var street = UI.GetPlayerStreetName();
            var pos = GetEntityCoords(GetPlayerPed(-1), true);
            Sync.Data.Set(User.GetServerId(), "GetTaxi", true);
            Shared.TriggerEventToAllPlayers("ARP:SendNotifTaxi", User.Data.phone_code + "-" + User.Data.phone, User.GetServerId(), zone, street, pos.X, pos.Y, pos.Z);
            await Delay(120000);
            if (!await Sync.Data.Has(User.GetServerId(), "GetTaxi")) return;
            Sync.Data.Reset(User.GetServerId(), "GetTaxi");
            Notification.SendWithTime("~r~Никто из водителей не откликнулся, попробуйте еще раз");
        }

        public static void ResetAllTask()
        {
            Managers.Blip.Delete();
            IsTask = false;
            IsTaskRoad = false;
            IsFindNpc = false;
            IsTaskRoadFinish = false;
            if (CheckpointId > -1)
                Managers.Checkpoint.Delete(CheckpointId);
            CheckpointId = -1;
            Warning = 0;
            if (CurrentPed != null)
                CurrentPed.MarkAsNoLongerNeeded();
            CurrentPed = null;
        }
    }
}