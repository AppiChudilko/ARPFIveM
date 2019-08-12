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
            
            if (!Client.Sync.Data.HasLocally(User.GetServerId(), "hasBuyMask"))
            {
                Notification.Send("~r~Вас заметила камера наблюдения");
                User.AddWantedLevel(2, "Оргабление магазина");
            }
                
            Dispatcher.SendEms("Код 3", "Всем патрулям, ограбление магазина");

            await Delay(120000);

            Client.Sync.Data.Set(1100000, "GrabShop" + shopId, true);
            Client.Sync.Data.Set(1100000, "TimerGrabShop" + shopId, 96);
                
            User.IsBlockAnimation = false;
            User.Freeze(PlayerId(), false);
            User.StopAnimation();

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
    }
}