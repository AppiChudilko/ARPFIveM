using System;
using System.Linq;
using CitizenFX.Core;
using Client.Vehicle;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class Grab : BaseScript
    {
        public static dynamic[,] GrabPosList =
        {
            { "Grab0", 887.3554, -953.2086, 38.21319 },
            { "Grab1", -101.6633, -2475.251, 5.020355 },
            { "Grab2", -210.2604, -2666.596, 10.76917 },
            { "Grab3", 118.4109, -3290.957, 5.019972 },
            { "Grab4", 1240, -3179.401, 6.104862 },
            { "Grab5", 938.0486, -2984.562, 4.900765 },
            { "Grab6", 819.4758, -2348.912, 29.3346 },
            { "Grab7", 939.7209, -1490.461, 29.09275 },
            { "Grab8", -1267.961, -812.321, 16.10697 },
            { "Grab9", 53.43676, 160.6483, 103.7036 }
        };

        public static Vector3 GrabPos = new Vector3(-193.1882f, -2707.356f, 5.010916f);
        public static Vector3 GrabVehPos = new Vector3(-195.071f, -2707.035f, 5.595252f);
        
        
        public static  Vector3 RobBank101 = new Vector3(146.7091f, -1045.741f, 28.36805f);
        public static  Vector3 RobBank102 = new Vector3(148.3756f, -1050.235f, 28.346f);
        public static Vector3 VehRobBank1 = new Vector3(116.1841f, -1081.145f, 29.2f);
        
        public static  Vector3 RobBank201 = new Vector3(310.79f, -283.753f, 53.17f);
        public static  Vector3 RobBank202 = new Vector3(312.86f, -288.57f, 53.14f);
        public static  Vector3 VehRobBank2 = new Vector3(298.658f, -333f, 44.9f);
        
        public static  Vector3 RobBank301 = new Vector3(-354.03f, -54.77f, 48.03f);
        public static  Vector3 RobBank302 = new Vector3(-352.302f, -59.302f, 48f);
        public static  Vector3 VehRobBank3 = new Vector3(-316.14f, -72.67f, 54.42f);
        
        public static  Vector3 RobBank401 = new Vector3(-1211.52f, -335.81f, 36.7f);
        public static  Vector3 RobBank402 = new Vector3(-1206.952f, -338.24f, 36.75f);
        public static  Vector3 VehRobBank4 = new Vector3(-1212.73f, -352f, 37.3f);
        
        public static  Vector3 RobBank501 = new Vector3(253.4969f, 228.44f, 100.68f);
        public static  Vector3 RobBank502 = new Vector3(265.65f, 213.98f, 100.67f);
        public static  Vector3 VehRobBank5 = new Vector3(259.49f, 276.66f, 105.62f);
        
        public static  Vector3 RobBank601 = new Vector3(1176.08f, 2712.275f, 37.08f);
        public static  Vector3 RobBank602 = new Vector3(1172.801f, 2716.4f, 37.06f);
        public static  Vector3 VehRobBank6 = new Vector3(1210.2f, 2711.508f, 38f);
        
        public static  Vector3 RobBank701 = new Vector3(-2957f, 481.23f, 14.7f);
        public static  Vector3 RobBank702 = new Vector3(-2953.1f, 484.56f, 14.6f);
        public static  Vector3 VehRobBank7 = new Vector3(-2944.53f, 491.32f, 45.28f);
        
        public static  Vector3 RobBank801 = new Vector3(-105.1297f, 6471f, 30.62f);
        public static  Vector3 RobBank802 = new Vector3(-104.12f, 6477.8f, 30.62f);
        public static  Vector3 VehRobBank8 = new Vector3(-89.45374f, 6474.44f, 31.259f);
        
        
        

        public static void LoadBlips()
        {
            var blip = World.CreateBlip(GrabPos);
            blip.Sprite = (BlipSprite) 523;
            blip.Name = "Место для сбыта ТС";
            blip.IsShortRange = true;
            blip.Scale = 0.8f; //86
            
            /*blip = World.CreateBlip(Pickup.OtmDengPos);
            blip.Sprite = (BlipSprite) 207;
            blip.Name = "Отмыть деньги";
            blip.IsShortRange = true;
            blip.Scale = 0.8f; //86*/
                
            Marker.Create(blip.Position, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(blip.Position, 1.4f, "grab:sell:car");
            
            /*for (int i = 0; i < GrabPosList.Length / 4; i++)
            {
                blip = World.CreateBlip(new Vector3((float) GrabPosList[i, 1], (float) GrabPosList[i, 2], (float) GrabPosList[i, 3]));
                blip.Sprite = (BlipSprite) 362;
                blip.Name = "Место для ограбления";
                blip.IsShortRange = true;
                blip.Scale = 0.4f; //86
                
                Marker.Create(blip.Position, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
                Checkpoint.Create(blip.Position, 1.4f, "grab:start");
            }*/
        }
        

        public static void GetGrabRandomVehicle()
        {
            if (Client.Sync.Data.HasLocally(User.GetServerId(), "sellCarTaskName"))
            {
                Notification.SendWithTime("~g~Вы уже получили задание, угнать: " + Client.Sync.Data.GetLocally(User.GetServerId(), "sellCarTaskName"));     
                return;
            }
            
            var pos = GetEntityCoords(GetPlayerPed(-1), true);
            var vehicle = GetRandomVehicleInSphere(pos.X, pos.Y, pos.Z, 300f, 0, 0);
            var v = new CitizenFX.Core.Vehicle(vehicle);

            switch (VehInfo.GetClassName(v.Model.Hash))
            {
                case "Emergency":
                case "Boats":
                case "Helicopters":
                case "Planes":
                    GetGrabRandomVehicle();
                    return;
            }

            Notification.SendWithTime("~g~Вам нужно угнать: " + v.DisplayName);           
            Client.Sync.Data.SetLocally(User.GetServerId(), "sellCarTaskHash", v.Model.Hash);
            Client.Sync.Data.SetLocally(User.GetServerId(), "sellCarTaskName", v.DisplayName);
        }

        public static void OpenSellCarMenuList()
        {
            if (Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), GrabPos) < 2f)
                MenuList.ShowSellCarListMenu(Main.GetVehicleListOnRadius(GrabVehPos, 10f));
        }

        public static void SellVehicle(CitizenFX.Core.Vehicle veh)
        {
            if (Weather.Hour < 22 && Weather.Hour > 4)
            {
                Notification.SendWithTime("~r~Доступно только с 22 до 4 утра");
                return;
            }
            
            if (User.Data.sell_car_time > 0)
            {
                Notification.SendWithTime("~r~Вы не можете сейчас сбыть транспорт");
                Debug.WriteLine("sell_car_time: " + User.Data.sell_car_time);
                return;
            }

            int money = 100;
            
            if (Client.Sync.Data.HasLocally(User.GetServerId(), "sellCarTaskName"))
            {
                if ((string) Client.Sync.Data.GetLocally(User.GetServerId(), "sellCarTaskName") == veh.DisplayName)
                {
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "sellCarTaskHash");
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "sellCarTaskName");
                    
                    money += 250;
                    
                    switch (VehInfo.GetClassName(veh.Model.Hash))
                    {
                        case "Sports Classics":
                            money += 450;
                            break;
                        case "Sports":
                            money += 320;
                            break;
                        case "Super":
                            money += 280;
                            break;
                        case "SUVs":
                        case "Muscle":
                        case "Off-Road":
                            money += 250;
                            break;
                    }
            
                    veh.Delete();
            
                    User.AddCashMoney(money);
                    Notification.SendWithTime("~g~Вы заработали: $" + money);

                    User.Data.sell_car_time = 15;
                    Client.Sync.Data.Set(User.GetServerId(), "sell_car_time", User.Data.sell_car_time);
                    return;
                }
            }

            switch (VehInfo.GetClassName(veh.Model.Hash))
            {
                case "Emergency":
                case "Boats":
                case "Helicopters":
                case "Planes":
                    Notification.SendWithTime("~r~Мы такое не принимаем");
                    return;
                case "Sports Classics":
                    money += 250;
                    break;
                case "Sports":
                    money += 220;
                    break;
                case "Super":
                    money += 180;
                    break;
                case "SUVs":
                case "Muscle":
                case "Off-Road":
                    money += 150;
                    break;
            }
            
            veh.Delete();
            
            User.AddCashMoney(money);
            Notification.SendWithTime("~g~Вы заработали: $" + money);
            
            var pPos = GetEntityCoords(GetPlayerPed(-1), true);
            Main.SaveLog("Grab", $"[GRAB_CAR] {User.Data.rp_name} | {pPos.X} {pPos.Y} {pPos.Z} | ${money} | {VehInfo.GetDisplayName(veh.Model.Hash)} | {VehInfo.GetClassName(veh.Model.Hash)}");

            User.Data.sell_car_time = 30;
            Client.Sync.Data.Set(User.GetServerId(), "sell_car_time", User.Data.sell_car_time);
        }

        public static async void GrabStock()
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
                
            for (int i = 0; i < GrabPosList.Length / 4; i++)
            {
                var pos = new Vector3((float) GrabPosList[i, 1], (float) GrabPosList[i, 2], (float) GrabPosList[i, 3]);
                if (Main.GetDistanceToSquared(playerPos, pos) > 5f) continue;
                
                if (Client.Sync.Data.HasLocally(User.GetServerId(), "HasGrab"))
                    return;
                
                if (new PlayerList().Count() < 10)
                {
                    Notification.SendWithTime("~r~Онлайн на сервере должен быть не менее 10 человек");
                    return;
                }
                
                if (Main.GetPlayerListOnRadius(6).Count < 2)
                {
                    Notification.SendWithTime("~r~Для ограбления склада нужно 2 и более человек");
                    return;
                }
            
                if (Weather.Hour < 22 && Weather.Hour > 6)
                {
                    Notification.SendWithTime("~r~Можно грабить с 22 до 6 утра.");
                    return;
                }
                
                if (await Client.Sync.Data.Has(1000000, (string) GrabPosList[i, 0]))
                {
                    Notification.SendWithTime("~r~Склад недавно грабили");
                    return;
                }

                Client.Sync.Data.SetLocally(User.GetServerId(), "HasGrab", true);

                User.Freeze(PlayerId(), true);
                User.PlayAnimation("anim@heists@money_grab@duffel", "loop", 9);
                User.IsBlockAnimation = true;

                await Delay(5000);

                if (!Client.Sync.Data.HasLocally(User.GetServerId(), "hasBuyMask"))
                {
                    Notification.Send("~r~Вас заметила камера наблюдения");
                    User.AddWantedLevel(2, "Оргабление склада");
                }
                
                Dispatcher.SendEms("Код 3", "Всем свободным патрулям, сработала сигнализация на складе");
                
                await Delay(120000);
                
                User.IsBlockAnimation = false;
                User.Freeze(PlayerId(), false);
                User.StopAnimation();

                Client.Sync.Data.Set(1000000, (string) GrabPosList[i, 0], true);
                Client.Sync.Data.Set(1000000, "Timer" + (string) GrabPosList[i, 0], 24);

                if (Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), pos) > 10f)
                {
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "HasGrab");
                    Notification.Send("~r~Вы слишком далеко от места ограбления");
                    return;
                }

                var rand = new Random();
                int money = rand.Next(800, 1100);

                if (Client.Sync.Data.HasLocally(User.GetServerId(), "GrabCash"))
                    Client.Sync.Data.SetLocally(User.GetServerId(), "GrabCash", (int) Client.Sync.Data.GetLocally(User.GetServerId(), "GrabCash") + money);
                else
                    Client.Sync.Data.SetLocally(User.GetServerId(), "GrabCash", money);
                
                if (!await Ctos.IsBlackout())
                    PedAi.SendCode(3, false);
                
                await Delay(500);
                Client.Sync.Data.ResetLocally(User.GetServerId(), "HasGrab");
                SetPlayerWantedLevel(PlayerId(), 2, false);
                Notification.Send("~y~Вы ограбили склад");
            }
        }
        
        public static async void GrabShop(int shopId)
        {
            if (Main.GetPlayerListOnRadius(6).Count < 2)
            {
                Notification.SendWithTime("~r~Для ограбления магазина нужно 2 и более человек");
                return;
            }
            
            if (await Client.Sync.Data.Has(1100000, "GrabShop" + shopId))
            {
                Notification.SendWithTime("~r~Магазин недавно грабили");
                return;
            }
            if (Weather.Hour > 5 && Weather.Hour < 23)
            {
                Notification.SendWithTime("~r~Грабить можно с 23:00 до 5:00");
                return;
            }
            if (new PlayerList().Count() < 10)
            {
                Notification.SendWithTime("~r~Онлайн на сервере должен быть не менее 10 человек");
                return;
            }
            if (User.GetPlayerVirtualWorld() != 0)
            {
                Notification.SendWithTime("~r~[ОШИБКА] Пожалуйста зайдите в любой дом/квартиру и выйдете");
                return;
            }

            if (Client.Sync.Data.HasLocally(User.GetServerId(), "HasGrab"))
                return;

            Client.Sync.Data.SetLocally(User.GetServerId(), "HasGrab", true);
            Client.Sync.Data.SetLocally(User.GetServerId(), "GrabPos", GetEntityCoords(GetPlayerPed(-1), true));

            User.Freeze(PlayerId(), true);
            User.PlayAnimation("anim@heists@money_grab@duffel", "loop", 9);
            User.IsBlockAnimation = true;

            await Delay(5000);
            
            SetPedComponentVariation(GetPlayerPed(-1), 5, 45, 0, 2);
            
            if (!Client.Sync.Data.HasLocally(User.GetServerId(), "hasBuyMask"))
            {
                Notification.Send("~r~Вас заметила камера наблюдения");
                User.AddWantedLevel(2, "Оргабление магазина");
            }
            
            var random = new Random();
            if (random.Next(0,2) == 0 || random.Next(0,2) == 2)
            {
                Dispatcher.SendEms("Код 3", "Всем патрулям, ограбление магазина");
            }

            await Delay(120000);

            Client.Sync.Data.Set(1100000, "GrabShop" + shopId, true);
            Client.Sync.Data.Set(1100000, "TimerGrabShop" + shopId, 96);
                
            User.IsBlockAnimation = false;
            User.Freeze(PlayerId(), false);
            User.PlayScenario("forcestop");

            if (Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), (Vector3) Client.Sync.Data.GetLocally(User.GetServerId(), "GrabPos")) > 10f)
            {
                Client.Sync.Data.ResetLocally(User.GetServerId(), "HasGrab");
                Notification.Send("~r~Вы слишком далеко от места ограбления");
                return;
            }

            var rand = new Random();
            int money = rand.Next(600, 900) * User.Bonus;
            
            if (Client.Sync.Data.HasLocally(User.GetServerId(), "GrabCash"))
                Client.Sync.Data.SetLocally(User.GetServerId(), "GrabCash", (int) Client.Sync.Data.GetLocally(User.GetServerId(), "GrabCash") + money);
            else
                Client.Sync.Data.SetLocally(User.GetServerId(), "GrabCash", money);
            
            var pPos = GetEntityCoords(GetPlayerPed(-1), true);
            Main.SaveLog("Grab", $"[GRAB] {User.Data.rp_name} | {pPos.X} {pPos.Y} {pPos.Z} | ${money}");
            
            //if (!await Ctos.IsBlackout())
            //    PedAi.SendCode(2, false);
            
            await Delay(500);
            Client.Sync.Data.ResetLocally(User.GetServerId(), "HasGrab");
            SetPlayerWantedLevel(PlayerId(), 2, false);
            Notification.Send("~y~Вы ограбили магазин");
        }
        
        public static async void GrabBank()
        {
            var distance1 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(146.7091f, -1045.741f, 28.36805f));
            var distance12 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(148.3756f, -1050.235f, 28.346f));
            var distance13 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(116.1841f, -1081.145f, 29.2f));
        
            var distance2 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(310.79f, -283.753f, 53.17f));
            var distance21 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(312.86f, -288.57f, 53.14f));
            var distance23 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(298.658f, -333f, 44.9f));
        
            var distance3 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(-354.03f, -54.77f, 48.03f));
            var distance31 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(-352.302f, -59.302f, 48f));
            var distance32 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(-316.14f, -72.67f, 54.42f));
        
            var distance4 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(-1211.52f, -335.81f, 36.7f));
            var distance41 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(-1206.952f, -338.24f, 36.75f));
            var distance42 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(-1212.73f, -352f, 37.3f));
        
            var distance5 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(253.4969f, 228.44f, 100.68f));
            var distance51 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(265.65f, 213.98f, 100.67f));
            var distance52= Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(259.49f, 276.66f, 105.62f));
        
            var distance6 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(1176.08f, 2712.275f, 37.08f));
            var distance61 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(1172.801f, 2716.4f, 37.06f));
            var distance62 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(1210.2f, 2711.508f, 38f));
        
            var distance7 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(-2957f, 481.23f, 14.7f));
            var distance71 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(-2953.1f, 484.56f, 14.6f));
            var distance72 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(-2944.53f, 491.32f, 45.28f));
        
            var distance8 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(-105.1297f, 6471f, 30.62f));
            var distance81 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(-104.12f, 6477.8f, 30.62f));
            var distance82 = Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(-89.45374f, 6474.44f, 31.259f));
            
            var pos1 = new Vector3(146.7091f, -1045.741f, 28.36805f);
            var pos2 = new Vector3(148.3756f, -1050.235f, 28.346f);
            var carpos = new Vector3(116.1841f, -1081.145f, 29.2f);

            if (distance1 <= distance2 && distance1 <= distance3 && distance1 <= distance4 && distance1 <= distance5 &&
                distance1 <= distance6 && distance1 <= distance7 && distance1 <= distance8)
            {
                pos1 = new Vector3(146.7091f, -1045.741f, 28.36805f);
                pos2 = new Vector3(148.3756f, -1050.235f, 28.346f);
                carpos = new Vector3(116.1841f, -1081.145f, 29.2f);
            }
            
            else if (distance2 <= distance1 && distance2 <= distance2 && distance2 <= distance3 && distance2 <= distance4 && distance2 <= distance5 &&
                distance2 <= distance6 && distance2 <= distance7 && distance2 <= distance8)
            {
                pos1 = new Vector3(310.79f, -283.753f, 53.17f);
                pos2 = new Vector3(312.86f, -288.57f, 53.14f);
                carpos = new Vector3(298.658f, -333f, 44.9f);
            }
            else if (distance3 <= distance1 && distance3 <= distance2 && distance3 <= distance3 && distance3 <= distance4 && distance3 <= distance5 &&
                distance3 <= distance6 && distance3 <= distance7 && distance3 <= distance8)
            {
                pos1 = new Vector3(-354.03f, -54.77f, 48.03f);
                pos2 = new Vector3(-352.302f, -59.302f, 48f);
                carpos = new Vector3(-316.14f, -72.67f, 54.42f);
            }
            else if (distance4 <= distance1 && distance4 <= distance2 && distance4 <= distance3 && distance4 <= distance4 && distance4 <= distance5 &&
                     distance4 <= distance6 && distance4 <= distance7 && distance4 <= distance8)
            {
                pos1 = new Vector3(-1211.52f, -335.81f, 36.7f);
                pos2 = new Vector3(-1206.952f, -338.24f, 36.75f);
                carpos = new Vector3(-1212.73f, -352f, 37.3f);
            }
            else if (distance5 <= distance1 && distance5 <= distance2 && distance5 <= distance3 && distance5 <= distance4 && distance5 <= distance5 &&
                     distance5 <= distance6 && distance5 <= distance7 && distance5 <= distance8)
            {
                pos1 = new Vector3(253.4969f, 228.44f, 100.68f);
                pos2 = new Vector3(265.65f, 213.98f, 100.67f);
                carpos = new Vector3(259.49f, 276.66f, 105.62f);
            }
            else if (distance6 <= distance1 && distance6 <= distance2 && distance6 <= distance3 && distance6 <= distance4 && distance6 <= distance5 &&
                     distance6 <= distance6 && distance6 <= distance7 && distance6 <= distance8)
            {
                pos1 = new Vector3(1176.08f, 2712.275f, 37.08f);
                pos2 = new Vector3(1172.801f, 2716.4f, 37.06f);
                carpos = new Vector3(1210.2f, 2711.508f, 38f);
            }
            else if (distance7 <= distance1 && distance7 <= distance2 && distance7 <= distance3 && distance7 <= distance4 && distance7 <= distance5 &&
                     distance7 <= distance6 && distance7 <= distance7 && distance7 <= distance8)
            {
                pos1 = new Vector3(-2957f, 481.23f, 14.7f);
                pos2 = new Vector3(-2953.1f, 484.56f, 14.6f);
                carpos = new Vector3(-2944.53f, 491.32f, 14.28f);
            }
            else if (distance8 <= distance1 && distance8 <= distance2 && distance8 <= distance3 && distance8 <= distance4 && distance8 <= distance5 &&
                     distance8 <= distance6 && distance8 <= distance7 && distance8 <= distance8)
            {
                pos1 = new Vector3(-105.1297f, 6471f, 30.62f);
                pos2 = new Vector3(-104.12f, 6477.8f, 30.62f);
                carpos = new Vector3(-89.45374f, 6474.44f, 31.259f);
            }
            
            

            int moneygot = 0;
            if (Main.GetPlayerListOnRadius(8).Count < 4)
            {
                Notification.SendWithTime("~r~Для ограбления банка нужно 4 и более человек");
                //return;
            }

            if (User.IsBlockAnimation == true)
            {
                return;
            }
            /*
            if (await Client.Sync.Data.Has(1100000, "GrabShop" + shopId))
            {
                Notification.SendWithTime("~r~Банк недавно грабили");
                return;
            }
            if (Weather.Hour > 1 && Weather.Hour < 23)
            {
                Notification.SendWithTime("~r~Грабить можно с 23:00 до 5:00");
                return;
            }
            if (new PlayerList().Count() < 20)
            {
                Notification.SendWithTime("~r~Онлайн на сервере должен быть не менее 20 человек");
                return;
            }
            if (User.GetPlayerVirtualWorld() != 0)
            {
                Notification.SendWithTime("~r~[ОШИБКА] Пожалуйста зайдите в любой дом/квартиру и выйдете");
                return;
            }*/
            if (Client.Sync.Data.HasLocally(User.GetServerId(), "HasGrab"))
            {
                Notification.Send("~r~Вы еще не отмыли предыдущие деньги");
                return;
            }

            if (User.Data.mp0_watchdogs < 50)
            {
                Notification.Send("~r~Слишком низкий навык взлома");
                return;
            }
            Chat.SendMeCommand("начинает взлом системы бесопасности..");
            User.PlayScenario("forcestop");
            User.Freeze(PlayerId(), true);
            User.IsBlockAnimation = true;
            Notification.SendPicture("Подключаюсь..", "Shon", "Дело", "CHAR_HUMANDEFAULT", Notification.TypeChatbox);

            Chat.SendDoCommand("Connecting...");
            User.PlayAnimation("amb@prop_human_movie_studio_light@idle_a", "idle_a", 8);
            Chat.SendDoCommand("REQUEST RECEIVED");
            Chat.SendDoCommand("awaiting for 'V4ult.py'..");
            await Delay(10000);
            Chat.SendDoCommand("Status 21%..");
            await Delay(1000);
            Chat.SendDoCommand("Status 41%..");
            User.PlayAnimation("amb@prop_human_movie_studio_light@idle_a", "idle_a", 8);
            await Delay(10000);
            Chat.SendDoCommand("Status 54%..");
            User.PlayAnimation("amb@prop_human_movie_studio_light@idle_a", "idle_a", 8);
            Chat.SendDoCommand("Status 89%..");
            await Delay(10000);
            
            var ran = new Random();
            if (ran.Next(0,2) == 0 || ran.Next(0,2) == 2)
            {
                Chat.SendMeCommand("ACCESS DENIED, try again later..");
                Notification.SendPicture("Черт! Попробуйте еще раз, что-то пошло не так.", "Shon", "Дело", "CHAR_HUMANDEFAULT", Notification.TypeChatbox);
                Dispatcher.SendEms("Код 3", "SUSPICIOUS ACTIVITY, REQUESTING ALL PATROLS");
                User.IsBlockAnimation = false;
                User.Freeze(PlayerId(), false);
                User.PlayScenario("forcestop");
                return;
            }
            Chat.SendDoCommand("Success! script by SH05");
            Notification.SendPicture("Отлично! Набивайте сумки!", "Shon", "Дело", "CHAR_HUMANDEFAULT", Notification.TypeChatbox);


            Client.Sync.Data.SetLocally(User.GetServerId(), "HasGrab", true);
            Client.Sync.Data.SetLocally(User.GetServerId(), "GrabPos", GetEntityCoords(GetPlayerPed(-1), true));
            User.Teleport(pos2);
            await Delay(1000);
            User.PlayAnimation("anim@heists@money_grab@duffel", "loop", 9);
            await Delay(5000);
            
            SetPedComponentVariation(GetPlayerPed(-1), 5, 45, 0, 2);
            
            if (!Client.Sync.Data.HasLocally(User.GetServerId(), "hasBuyMask"))
            {
                string rpname = User.Data.rp_name;
                Notification.Send("~r~Вас заметила камера наблюдения");
                User.AddWantedLevel(8, "Оргабление банка");
                Dispatcher.SendEms("Код 3", "Ограбление банка, подозреваемый: " + rpname);
            }
            
            var random = new Random();
            Dispatcher.SendEms("Код 3", "Всем патрулям, ограбление банка");
            Notification.SendPicture("Черт, копы уже едут к вам, обороняйтесь!", "Shon", "Дело", "CHAR_HUMANDEFAULT", Notification.TypeChatbox);

            
            User.Freeze(PlayerId(), true);
            User.IsBlockAnimation = true;
            Notification.SendPicture("Сваливайте когда посчитаете нужным", "Shon", "Дело", "CHAR_HUMANDEFAULT", Notification.TypeChatbox);
            await Delay(1000);
            Notification.SendPicture("Приготовьтесь к обороне!", "Shon", "Дело", "CHAR_HUMANDEFAULT", Notification.TypeChatbox);
            if (User.IsBlockAnimation == true)
            {
                for (int i = 1; i <= 40000; i++)
                {
                    if (i == 100)
                    {
                        MenuList.ShowStopGrab();
                    }
                    if (i == 5000)
                    {
                        Notification.Send("~b~Куш достиг 5.000$");
                    }
                    if (i == 10000)
                    {
                        Notification.Send("~b~Куш достиг 10.000$");
                    }
                    if (i == 15000)
                    {
                        Notification.Send("~g~Куш достиг 15.000$");
                    }
                    if (i == 20000)
                    {
                        Notification.Send("~y~Куш достиг 20.000$");
                    }
                    if (i == 35000)
                    {
                        Notification.Send("~o~Куш достиг 35.000$");
                    }
                    if (i == 40000)
                    {
                        Notification.Send("~r~Куш достиг 40.000$");
                    }

                    if (!User.IsBlockAnimation == true)
                    {
                        break;
                    }

                    moneygot = i;
                    await Delay(2);

                }
            }
            //await Delay(120);

            //Client.Sync.Data.Set(1100000, "GrabShop" + shopId, true);
            //Client.Sync.Data.Set(1100000, "TimerGrabShop" + shopId, 96);
            
            User.IsBlockAnimation = false;
            User.Freeze(PlayerId(), false);
            User.PlayScenario("forcestop");

            if (Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), (Vector3) Client.Sync.Data.GetLocally(User.GetServerId(), "GrabPos")) > 10f)
            {
                Client.Sync.Data.ResetLocally(User.GetServerId(), "HasGrab");
                Notification.Send("~r~Вы слишком далеко от места ограбления");
                Notification.SendPicture("Вы куда?!", "Shon", "Дело", "CHAR_HUMANDEFAULT", Notification.TypeChatbox);

                return;
            }

            var rand = new Random();
            int money = moneygot;
            
            if (Client.Sync.Data.HasLocally(User.GetServerId(), "GrabCash"))
                Client.Sync.Data.SetLocally(User.GetServerId(), "GrabCash", (int) Client.Sync.Data.GetLocally(User.GetServerId(), "GrabCash") + money);
            else
                Client.Sync.Data.SetLocally(User.GetServerId(), "GrabCash", money);
            
            var pPos = GetEntityCoords(GetPlayerPed(-1), true);
            Main.SaveLog("Grab", $"[GRAB] {User.Data.rp_name} | {pPos.X} {pPos.Y} {pPos.Z} | ${money}");
            
            //if (!await Ctos.IsBlackout())
            //    PedAi.SendCode(2, false);
            
            await Delay(500);
            Client.Sync.Data.ResetLocally(User.GetServerId(), "HasGrab");
            SetPlayerWantedLevel(PlayerId(), 7, false);
            Notification.Send("~y~Вы ограбили банк");
            User.Teleport(pos1);
            
            await Managers.Vehicle.SpawnByName("rumpo3", carpos, 0f);
            //Notification.SendPicture("Теперь скройтесь от полиции!", "Shon", "Дело", "CHAR_HUMANDEFAULT", Notification.TypeChatbox);

            Notification.SendPicture("Ищите авто для побега на задней стоянке", "Shon", "Дело", "CHAR_HUMANDEFAULT", Notification.TypeChatbox);
            User.Teleport(pos1);
            await Delay(5000);
            Notification.SendPicture("Скройтесь от копов и затем отмойте деньги", "Shon", "Дело", "CHAR_HUMANDEFAULT", Notification.TypeChatbox);

        }
        
        public static async void GrabGrSix()
        {
            var veh = Main.FindNearestVehicle();
            var vehId = Managers.Vehicle.GetVehicleIdByNumber(GetVehicleNumberPlateText(veh.Handle));
            var vehItem = await Managers.Vehicle.GetAllData(vehId);
            int vehicle = NetToVeh(veh.NetworkId);
            if (User.Data.job == "GrSix")
            {
                Main.SaveLog("GrSixGrabGrSix", $"USER: {User.GetServerId()} COORDS: {NetworkGetPlayerCoords(User.GetServerId())}");
                Notification.SendWithTime("Вот ето тебе сейчас блокировка будет чучело.");
                return;
            }
            if (vehItem.Hash != 1747439474)
            {
                Notification.SendWithTime("~r~Грабить можно автомобили типа STOCKADE");
                return;
            }

            if (User.IsSapd() || User.IsSheriff() || User.IsFib() || User.IsEms())
            {
                Notification.SendWithTime("Ну зачем, тебе что, работа не дорога?");
                return;
            }
            var plData = await User.GetAllDataByServerId(User.GetServerId());
            if (plData.fraction_id2 > 0 || User.IsBallas() || User.IsCartel() || User.IsMara())
            {
                if (Weather.Hour > 8 && Weather.Hour < 22)
                {
                    Notification.SendWithTime("~r~Ограбить можно с 22:00 до 8:00");
                    return;
                }
                if (new PlayerList().Count() < 10)
                {
                    Notification.SendWithTime("~r~Онлайн на сервере должен быть не менее 10 человек");
                    return;
                }
                TriggerServerEvent("ARP:GrSix:Grab", VehToNet(vehicle));
                Dispatcher.SendEms("Код 0", "Всем патрулям, нападение на экипаж инкассаторского автомобиля");
            }
            else
            {
                Notification.SendWithTime("~r~Нужно состоять в неофициальной организации");
            }
        }
    }
}