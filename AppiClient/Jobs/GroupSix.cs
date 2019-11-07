using System;
using System.Runtime.Remoting;
using CitizenFX.Core;
using Client.Managers;
using Client.Sync;
using static CitizenFX.Core.Native.API;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Ped = CitizenFX.Core.Ped;


namespace Client.Jobs
{
    public class GroupSix : BaseScript
    {
        public static int PedNum = 0;
        public static int Temp = 0;
        public static bool IsProcess = false;
        public static int MoneyInCar = 0;
        public static int MoneyBag = 0;
        public static readonly Vector3 GrSixMenuPos = new Vector3(-42, -664, 32);
        public static int Price = 0;
        
        /*public static double[,] Peds =
        {
            { 1210.057, -3121.55, 4.540324, 50.51484 },
            { 1210.057, -3121.55, 4.540324, 50.51484 },
            { 1751.322, -1585.453, 111.5994, 60.36097 },
            { 21.99591, -449.5897, 64.02998, 327.7911 },
            { 46.58547, -379.8859, 72.94202, 303.93520 },
            { -150.4755, -1104.109, 12.11701, 24.06468 },
            { -127.1905, -959.1509, 38.35626, 179.77 },
            {-446.6152, -894.0262, 28.39282, 51.5372},
            {-452.4932, -948.3916, 28.39283, 315.2392},
            {-491.967, -1029.456, 51.47617, 11.66628},
            {-1161.529, -2034.481, 12.17899, 184.0565},
            {147.6128, 324.8187, 111.1386, 145.9365},
            {244.3799, 374.3475, 104.7382, 181.7057},
            {-1115.342, -1633.874, 6.936214, 247.175},
            {-1229.514, -1405.422, 3.189581, 147.4763},
            {-932.275, 389.6477, 78.14062, 334.3152},
            {-50.2158, 1906.498, 194.3613, 143.7258},
            {328.1824, 2880.407, 42.45092, 195.5598},
            {498.2979, 2951.37, 41.42222, 202.9126},
            {592.7723, 2926.421, 39.91875, 335.3227},
            {2707.574, 2777.558, 36.87803, 74.10024},
            {2600.442, 2804.085, 32.84933, 190.95},
            {2331.42, 3040.298, 47.15134, 251.7551},
            {2335.331, 3057.948, 47.15187, 281.1659},
            {1347.239, 4390.832, 43.35429, 147.2233},
            {1545.504, 2174.404, 77.79837, 52.98627},
        };*/
        
        public static double[,] Pickups =
        {
            { 253.4611, 220.7204, 106.2865 },
            { 251.749, 221.4658, 106.2865 },
            { 248.3227, 222.5736, 106.2867 },
            { 246.4875, 223.2582, 106.2867 },
            { 243.1434, 224.4678, 106.2868 },
            { 241.1435, 225.0419, 106.2868 },
            { 148.5, -1039.971, 29.37775 },
            { 1175.054, 2706.404, 38.09407 },
            { -1212.83, -330.3573, 37.78702 },
            { 314.3541, -278.5519, 54.17077 },
            { -2962.951, 482.8024, 15.7031 },
            { -350.6871, -49.60739, 49.04258 },
            { -111.1722, 6467.846, 31.62671 },
            { -113.3064, 6469.969, 31.62672 },
            { 138.7087, -1705.711, 29.29162 },
            { 1214.091, -472.9952, 66.208 },
            { -276.4055, 6226.398, 31.69552 },
            { -1282.688, -1117.432, 6.990113 },
            { 1931.844, 3730.305, 32.84443 },
            { -33.34319, -154.1892, 57.07654 },
            { -813.5332, -183.2378, 37.5689 },
            { 22.08832, -1106.986, 29.79703 },
            { 252.17, -50.08245, 69.94106 },
            { 842.2239, -1033.294, 28.19486 },
            { -661.947, -935.6796, 21.82924 },
            { -1305.899, -394.5485, 36.69577 },
            { 809.9118, -2157.209, 29.61901 },
            { 2567.651, 294.4759, 108.7349 },
            { -3171.98, 1087.908, 20.83874 },
            { -1117.679, 2698.744, 18.55415 },
            { 1693.555, 3759.9, 34.70533 },
            { -330.36, 6083.885, 31.45477 },
            { -1148.878, -2000.123, 13.18026 },
            { -347.0815, -133.3432, 39.00966 },
            { 726.0679, -1071.613, 28.31101 },
            { -207.0201, -1331.493, 34.89437 },
            { 1187.764, 2639.15, 38.43521 },
            { 101.0262, 6618.267, 32.43771 },
            { -146.2072, -584.2731, 166.0002 },
            //{ 472.2666, -1310.529, 28.22178 },
            { 26.213, -1345.442, 29.49702 },
            { -1223.059, -906.7239, 12.32635 },
            { -1487.533, -379.3019, 40.16339 },
            { 1135.979, -982.2205, 46.4158 },
            { 1699.741, 4924.002, 42.06367 },
            { 374.3559, 327.7817, 103.5664 },
            { -3241.895, 1001.701, 12.83071 },
            { -3039.184, 586.3903, 7.90893 },
            { -2968.295, 390.9566, 15.04331 },
            { 547.8511, 2669.281, 42.1565 },
            { 1165.314, 2709.109, 38.15772 },
            { 1960.845, 3741.882, 32.34375 },
            { 1729.792, 6414.979, 35.03723 },
            { -657.087, -857.313, 23.490 },
        };

        /*public static async void Start()
        {
            var player = GetPlayerPed(-1);
            int veh = GetVehiclePedIsIn(PlayerPedId(), false);
            var vehicle = new CitizenFX.Core.Vehicle(veh);
            if (vehicle.PassengerCount == 0)
            {
                Notification.SendWithTime("~r~Работать можно только с напарниками");
                return;
            }
            Debug.WriteLine("1");
            foreach (var p in new PlayerList())
            {

                Debug.WriteLine("3");
                if (p.Character == vehicle.GetPedOnSeat(0))
                {
                    Debug.WriteLine("4");
                    TriggerServerEvent("ARP:GrSix:Partner");
                    Debug.WriteLine("5");
                    Shared.TriggerEventToPlayer(p.ServerId,"ARP:GrSix:Partner",p, VehToNet(veh));//тут блядь сделать надо сука
                    Debug.WriteLine("6");
                }
            }
        }

        public static void DropCheckpoint(int veh)
        {
            Debug.WriteLine("7");
            if (User.IsJobGroupSix() && IsPedInVehicle(GetPlayerPed(-1), NetToVeh(veh), false))
            {
                Debug.WriteLine("8");
                Random random = new Random();
                Debug.WriteLine("9");
                var r = random.Next(84);
                Debug.WriteLine("10");
                var pos = new Vector3((float) Pickups[r, 0], (float) Pickups[r, 1], (float) Pickups[r, 2] - 1);
                Debug.WriteLine("11");
                Managers.Checkpoint.CreateWithMarker(pos, 1f, 1f, Marker.Red.R, Marker.Red.G, Marker.Red.B,
                    Marker.Red.A, "job:GrSix:Work");
                Debug.WriteLine("12");
                Managers.Blip.Create(pos);
                Debug.WriteLine("13");
                User.SetWaypoint(pos.X, pos.Y);
                Debug.WriteLine("14");
            }
        }*/

        public static async void Start()
        {
            //55 checkpoints
            var player = GetPlayerPed(-1);
            var ppos = GetEntityCoords(GetPlayerPed(-1), true);
            int veh = GetVehiclePedIsIn(PlayerPedId(), false);
            if (IsProcess)
            {
                Notification.SendWithTime("~r~Задание уже получено");
                return;
            }
            Random random = new Random();
            var r = random.Next(0, 53);
            var pos = new Vector3((float) Pickups[r, 0], (float) Pickups[r, 1], (float) Pickups[r, 2] - 1);
            
            //var pos = new Vector3((float) Pickups[Temp, 0], (float) Pickups[Temp, 1], (float) Pickups[Temp, 2] - 1);
            //Temp += 1;
            Managers.Checkpoint.CreateWithMarker(pos, 1f, 1f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "job:GrSix:Work");
            Notification.SendPicture(Lang.GetTextToPlayer("_lang_129"), Lang.GetTextToPlayer("_lang_130"), "323-777-6666", "CHAR_ANDREAS", Notification.TypeChatbox);
            Price = (int) (Main.GetDistanceToSquared(pos, ppos))/4;
            if (Price > 1200)
                Price = 1200;
            if (Price < 180)
                Price = 180;
            Managers.Blip.Create(pos);
            User.SetWaypoint(pos.X, pos.Y);
            IsProcess = true;
        }
        
        public static async void WorkProcess()
        {
            Notification.SendWithTime("~y~Вы проводите инкассацию");
            Managers.Blip.Delete();
            
            IsProcess = false;

            var rand = new Random();
            MoneyBag = (Price + rand.Next(50, 100)) * 100;
            Sync.Data.SetLocally(User.GetServerId(), "GrSix:MoneyBag", true);
            SetPedComponentVariation(GetPlayerPed(-1), 5, 45, 0, 2);
            Notification.SendWithTime($"В сумке: \"~g~${MoneyBag:#,#}\"");
            Notification.SendWithTime("~g~Отлично. Теперь езжай дальше по заданию");
        }

        public static async void PutMoneyInCar()
        {
            var veh = GetVehiclePedIsUsing(PlayerPedId());

            MoneyInCar = MoneyInCar + MoneyBag;
            MoneyBag = 0;
            Notification.SendWithTime("~g~Вы положили деньги в автомобиль");
            Sync.Data.ResetLocally(User.GetServerId(), "GrSix:MoneyBag");
            TriggerServerEvent("ARP:GrSix:DropMoney",VehToNet(veh), MoneyInCar);
            SetPedComponentVariation(GetPlayerPed(-1), 5, 44, 0, 2);
        }
        
        public static void UniformSet()
        {
            if (User.Data.money < 1000)
            {
                Notification.SendWithTime("~r~У вас недостаточно денег");
                return;
            }
            int Armor = 50;
            int r = 150;
            var coords = GetEntityCoords(GetPlayerPed(-1), true);
            Shared.TriggerEventToAllPlayers("ARP:GiveArmorMp", coords.X, coords.Y, coords.Z, r, Armor);
            Notification.SendWithTime("~b~Готово");
            if (User.Skin.SEX == 1)
            {
                SetPedComponentVariation(GetPlayerPed(-1), 3, 117, 0, 2);
                SetPedComponentVariation(GetPlayerPed(-1), 4, 32, 0, 2);
                SetPedComponentVariation(GetPlayerPed(-1), 5, 44, 0, 2);
                SetPedComponentVariation(GetPlayerPed(-1), 6, 25, 0, 2);
                SetPedComponentVariation(GetPlayerPed(-1), 8, 154, 1, 2);
                SetPedComponentVariation(GetPlayerPed(-1), 11, 46, 0, 2);
                SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
            }
            else
            {
                SetPedComponentVariation(GetPlayerPed(-1), 3, 107, 0, 2);
                SetPedComponentVariation(GetPlayerPed(-1), 4, 31, 0, 2);
                SetPedComponentVariation(GetPlayerPed(-1), 5, 44, 0, 2);
                SetPedComponentVariation(GetPlayerPed(-1), 6, 25, 0, 2);
                SetPedComponentVariation(GetPlayerPed(-1), 8, 124, 1, 2);
                SetPedComponentVariation(GetPlayerPed(-1), 11, 53, 0, 2);
                SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                
            }
            Sync.Data.SetLocally(User.GetServerId(), "GrSix:Uniform", true);
            User.RemoveMoney(1000);
            Coffer.AddMoney(1000);
        }

        public static void Equip()
        {
            if (User.Data.money < 2250)
            {
                Notification.SendWithTime("~r~ Недостаточно денег");
                return;
            }
            User.GiveWeapon((uint) WeaponHash.SMG, 180, false, false);
            User.GiveWeapon((uint) WeaponHash.PistolMk2, 60, false, false);
            Notification.SendWithTime("~g~Вы взяли оружие");
            Sync.Data.SetLocally(User.GetServerId(), "GrSix:Equip", true);
            User.RemoveMoney(2250);
            Coffer.AddMoney(2250);
        }

        public static void DeleteVeh(int money, int vehicle)
        {
            var veh = new CitizenFX.Core.Vehicle(NetToVeh(vehicle));
            var vehId = Managers.Vehicle.GetVehicleIdByNumber(GetVehicleNumberPlateText(veh.Handle));
            if(Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), GrSixMenuPos) > 50)
            {
                Notification.SendWithTime("~y~Транспорт можно сдать только на базе");
                User.SetWaypoint(-42, -664);
                return;
            }
            if (User.CanOpenVehicle(vehId, veh.Handle))
            {
                Notification.SendWithTime("~g~Вы сдали автомобиль ");
                Coffer.RemoveMoney(4500);

                Notification.SendWithTime($"~q~Вы окончили маршрут день и заработали: ${money}");
                
                User.AddMoney(money + 4500);
                
                Main.FindNearestVehicle().Delete();
                Characher.UpdateCloth(false);
                TriggerServerEvent("ARP:GrSix:ResetMoneyInCar", veh.NetworkId);
                MoneyInCar = 0;
                
            }
            else
            {
                Notification.SendWithTime("Не вы арендовали, не вам сдавать.");
                return;
            }
        }

        public static async void Grab(int money)
        {
            var veh = Main.FindNearestVehicle();
            int vehicle = NetToVeh(veh.NetworkId);
            veh.StartAlarm();
            Vector3 pickupPos = new Vector3(veh.Position.X - Sin(veh.Rotation.Z) * (-4),veh.Position.Y + Cos(veh.Rotation.Z) * (-4) , veh.Position.Z);
            SetEntityCoords(GetPlayerPed(-1), pickupPos.X, pickupPos.Y, pickupPos.Z, true, false, false, true);
            User.PedRotation(veh.Rotation.Z);
            User.PlayScenario("WORLD_HUMAN_WELDING");
            FreezeEntityPosition(vehicle, true);

            await Delay(120000);

            if (Main.GetDistanceToSquared(pickupPos, GetEntityCoords(GetPlayerPed(-1), true)) > 2f && Main.GetDistanceToSquared(veh.Position, GetEntityCoords(GetPlayerPed(-1), true)) > 5f)
            {
                Notification.SendWithTime("~r~Вы находитесь слишком далеко");
                return;
            }
    
            if (Client.Sync.Data.HasLocally(User.GetServerId(), "GrabCash"))
                Client.Sync.Data.SetLocally(User.GetServerId(), "GrabCash", (int) Client.Sync.Data.GetLocally(User.GetServerId(), "GrabCash") + money);
            else
                Client.Sync.Data.SetLocally(User.GetServerId(), "GrabCash", money);

            
            SetVehicleDoorBroken(vehicle, 3, false);
            SetVehicleDoorBroken(vehicle, 2, false);
            
            User.PlayScenario("forcestop");

            Notification.SendWithTime("~g~Вы вскрыли кузов и схватили мешок с наличкой");
            MoneyInCar = 0;
            TriggerServerEvent("ARP:GrSix:ResetMoneyInCar", veh.NetworkId);
            await Delay(90000);
            veh.Delete();
        }
        

        public static void Dequip()
        {
            if (Sync.Data.HasLocally(User.GetServerId(), "GrSix:Uniform") &&
                Sync.Data.HasLocally(User.GetServerId(), "GrSix:Equip"))
            {
                if(GetAmmoInPedWeapon(GetPlayerPed(-1), (uint) WeaponHash.SMG) >60 && GetAmmoInPedWeapon(GetPlayerPed(-1), (uint) WeaponHash.PistolMk2) > 20)
                { 
                    Notification.SendWithTime("~g~Вы сдали оружие и форму");
                    RemoveWeaponFromPed(GetPlayerPed(-1), (uint) WeaponHash.SMG);
                    SetPedAmmo(GetPlayerPed(-1), (uint) WeaponHash.SMG, 0);
                    RemoveWeaponFromPed(GetPlayerPed(-1), (uint) WeaponHash.PistolMk2);
                    SetPedAmmo(GetPlayerPed(-1), (uint) WeaponHash.PistolMk2, 0);
                    Sync.Data.SetLocally(User.GetServerId(), "GrSix:Equip", false);
                    
                    Sync.Data.ResetLocally(User.GetServerId(), "hasMask");
                    SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                    SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                    SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                    ClearPedProp(GetPlayerPed(-1), 0);
                    Characher.UpdateCloth(false);
                    Characher.UpdateFace(false);
                    Sync.Data.ResetLocally(User.GetServerId(), "GrSix:Uniform");
                    Sync.Data.ResetLocally(User.GetServerId(), "GrSix:Equip");
                    
                        User.AddMoney(1000);
                }
                if(GetAmmoInPedWeapon(GetPlayerPed(-1), (uint) WeaponHash.SMG) < 60 && GetAmmoInPedWeapon(GetPlayerPed(-1), (uint) WeaponHash.PistolMk2) < 20)
                { 
                    Notification.SendWithTime("~g~Вы сдали оружие и форму");
                    RemoveWeaponFromPed(GetPlayerPed(-1), (uint) WeaponHash.SMG);
                    SetPedAmmo(GetPlayerPed(-1), (uint) WeaponHash.SMG, 0);
                    RemoveWeaponFromPed(GetPlayerPed(-1), (uint) WeaponHash.PistolMk2);
                    SetPedAmmo(GetPlayerPed(-1), (uint) WeaponHash.PistolMk2, 0);
                    Sync.Data.SetLocally(User.GetServerId(), "GrSix:Equip", false);
                    
                    Sync.Data.ResetLocally(User.GetServerId(), "hasMask");
                    SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                    SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                    SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                    ClearPedProp(GetPlayerPed(-1), 0);
                    Characher.UpdateCloth(false);
                    Characher.UpdateFace(false);
                    Sync.Data.ResetLocally(User.GetServerId(), "GrSix:Uniform");
                    Sync.Data.ResetLocally(User.GetServerId(), "GrSix:Equip");
                    
                    User.AddMoney(600);
                }
            }
            else
            {
                Notification.SendWithTime("~r~На вас нет формы. Что вы собрались сдавать?");
            }
        }
        
        public GroupSix()
        {
            Tick += OnTick;
        }

        private async Task OnTick()
        {
            
            if(User.Data.job == "GrSix")
            {
                int vehicle = GetVehiclePedIsIn(PlayerPedId(), false);
                var veh = new CitizenFX.Core.Vehicle(vehicle);
                if (veh.Model.Hash == 1747439474 && IsPedInVehicle(GetPlayerPed(-1), vehicle, true) && Sync.Data.HasLocally(User.GetServerId(), "GrSix:MoneyBag"))
                {
                    PutMoneyInCar();
                }
            }
            

        }
    }
        /*public GroupSix()
        {
            Tick += OnTick;
        }
        
        private async Task OnTick()
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            int veh = GetVehiclePedIsIn(PlayerPedId(), false);
            if (veh != 0)
            {
                var created = false;
                var speed = GetEntitySpeed(veh);
                var mph = speed * 2.236936;
                if (mph == 0 && created == false)
                {
                    var Range = 5.5f;
                    var Rot = GetEntityRotation(veh, 2);
                    var Pos = GetEntityCoords(veh, false);
                    var pos = new Vector3(Range * Sin(Rot.X) + Pos.X, Range * Cos(Rot.Y) + Pos.Y, Pos.Z-1);
                    Marker.Create(pos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
                    Managers.Checkpoint.Create(pos, 1.4f, "show:menu");

                    if (Main.GetDistanceToSquared(pos, playerPos) < Managers.Pickup.DistanceCheck)
                        MenuList.GrSixOgrabOrInCarMenu();
                    await Delay(100);
                }

                if (mph != 0 && created == true)
                {
                    
                }
            }
            else
            {
                await Delay(100);
                //Notification.SendWithTime("Not in veh");
            }
        }*/
    }