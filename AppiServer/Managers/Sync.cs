using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace Server.Managers
{
    public class Sync : BaseScript
    {
        public static int Loto = 10;
        
        
        /*
        
        
        Цена бизнеса на кол-во дней
        1750000 / 180
        
        
        */
        
        public Sync()
        {
            //Tick += WeatherSync;
            Tick += SecTimer;
            Tick += TimeSync;
            Tick += Min5Timer;
            Tick += Min30Timer;
            Tick += Min60Timer;

            ResetLoto();
        }
        
        public static void ResetLoto()
        {
            var rand = new Random();
            Loto = rand.Next(200) + 200;
        }

        public static void UpdateLoto()
        {
            var rand = new Random();
            Loto = Loto + rand.Next(200) + 100;
            TriggerClientEvent("ARP:SendPlayerNotificationPicture", $"Призовой куш вырос до: ${Loto:#,#}", "Life Invader", "Лото", "CHAR_LIFEINVADER", 2);
            TriggerClientEvent("ARP:UpdateLoto", rand.Next(50), Loto);
        }

        public static async void WinLotoLoto(string name)
        {
            TriggerClientEvent("ARP:SendPlayerNotificationPicture", $"Победитель лото: {name}. Поздравляем!", "Life Invader", "Лото - Победитель", "CHAR_LIFEINVADER", 2);
            await Delay(10000);
            ResetLoto();
        }

        public static void VirtualWorldToClient()
        {
            var newList = new PlayerList().Where(p => User.PlayerVirtualWorldList.ContainsKey(User.GetServerId(p).ToString())).ToDictionary(p => User.GetServerId(p).ToString(), p => User.PlayerVirtualWorldList[User.GetServerId(p).ToString()]);
            User.PlayerVirtualWorldList = newList;
            TriggerClientEvent("ARP:SyncVirtualWorldToClient", User.PlayerVirtualWorldList);
        }

        public static void PlayerIdListToClient()
        {            
            TriggerClientEvent("ARP:SyncPlayerIdListToClient", User.PlayerIdList);
        }

        public static void UpdateWalkietalkie()
        {            
            var data = new PlayerList().Select(User.GetServerId).Where(serverId => Server.Sync.Data.Has(serverId, "walkietalkie_num") && (string) Server.Sync.Data.Get(serverId, "walkietalkie_num") != "0").ToDictionary(serverId => serverId.ToString(), serverId => (string) Server.Sync.Data.Get(serverId, "walkietalkie_num"));
            if (data.Count > 0)
                TriggerClientEvent("ARP:UpdateRadioList", data);
        }

        private static async Task SecTimer()
        {
            await Delay(1000);     
            UpdateWalkietalkie();
        }

        private static async Task TimeSync()
        {
            await Delay(8500);

            var plList = new PlayerList();
            Appi.MySql.ExecuteQuery("UPDATE monitoring SET online = '" + plList.Count() + "', last_update='" + Main.GetTimeStamp() + "' where id = '1'");

            if (DateTime.Now.Hour == 11 && DateTime.Now.Minute == 23)
                Server.Sync.Data.Reset(-9999, "IsBlackoutTimeOut");
            
            if (DateTime.Now.Hour == 3 && DateTime.Now.Minute == 50)
                TriggerClientEvent("ARP:SendPlayerNotification", "Рестарт сервера через 15 минут");
            if (DateTime.Now.Hour == 3 && DateTime.Now.Minute == 59)
                TriggerClientEvent("ARP:SendPlayerNotification", "Рестарт сервера через 5 минут");
            if (DateTime.Now.Hour == 4 && DateTime.Now.Minute == 4)
            {
                Appi.MySql.ExecuteQuery("DELETE FROM items WHERE owner_type = 0 AND timestamp_update < '" + (Main.GetTimeStamp() - (60 * 60 * 24 * 7)) + "'");
                Appi.MySql.ExecuteQuery("DELETE FROM items WHERE owner_type = 9 AND owner_id = 2 AND timestamp_update < '" + (Main.GetTimeStamp() - (60 * 60 * 24 * 7)) + "'");
                
                if (Main.ServerName != "Venus")
                {
                    foreach (var pl in new PlayerList())
                        pl.Drop("RESTART. Wait 5 min.");
                }
                
                Appi.MySql.ExecuteQuery("UPDATE users SET is_online='0'");
                Appi.MySql.ExecuteQuery("UPDATE monitoring SET online = '0', last_update='0' where id = '1'");
            }
            
            VirtualWorldToClient();
        }
        
        private static async Task Min5Timer()
        {
            await Delay(1000 * 60 * 5);
            Vehicle.SpawnVehiclePriority.Clear();

            var plList = new PlayerList();

            foreach (var player in plList)
            {
                if (Server.Sync.Data.Has(User.GetServerId(player), "id"))
                    Save.UserAccount(player);
            }
        
            Debug.WriteLine("Saved time and user accounts");
            Main.UpdateDiscordStatus();

            Debug.WriteLine("Saved all stock");
            for (int i = 0; i <= Business.Count+1; i++)
                Save.Business(i);
            Debug.WriteLine("Saved all businnes");
            
            /*Timer grab*/

            for (int i = 0; i < 100; i++)
            {
                if (Server.Sync.Data.Has(1000000, "TimerGrab" + i))
                {
                    if ((int) Server.Sync.Data.Get(1000000, "TimerGrab" + i) < 0)
                    {
                        Server.Sync.Data.Reset(1000000, "TimerGrab" + i);
                        Server.Sync.Data.Reset(1000000, "Grab" + i);
                        continue;
                    }
                    
                    Server.Sync.Data.Set(1000000, "TimerGrab" + i, Convert.ToInt32(Server.Sync.Data.Get(1000000, "TimerGrab" + i)) - 1);
                }
                
                if (Server.Sync.Data.Has(1100000, "TimerGrabShop" + i))
                {
                    if ((int) Server.Sync.Data.Get(1100000, "TimerGrabShop" + i) < 0)
                    {
                        Server.Sync.Data.Reset(1100000, "TimerGrabShop" + i);
                        Server.Sync.Data.Reset(1100000, "GrabShop" + i);
                        continue;
                    }
                    
                    Server.Sync.Data.Set(1100000, "TimerGrabShop" + i, Convert.ToInt32(Server.Sync.Data.Get(1000000, "TimerGrabShop" + i)) - 1);
                }
            }
        }
        
        private static async Task Min30Timer()
        {
            await Delay(1000 * 60 * 30);
            foreach (var item in Vehicle.VehicleInfoGlobalDataList)
                Save.UserVehicle(item);
            
            Debug.WriteLine("Saved all vehicle");

            for (int i = 0; i < 90; i++)
                Server.Sync.Data.Reset(-1, "DisableNetwork" + i);
            
            UpdateLoto();
        }
        
        private static async Task Min60Timer()
        {
            await Delay(1000 * 60 * 180);
            
            var rand = new Random();
            
            DataTable tbl = Appi.MySql.ExecuteQueryWithResult("SELECT * FROM business WHERE type = 0 OR type = 1 OR type = 4 OR type = 5 OR type = 7 OR type = 9 OR type = 12 OR type = 8 OR type = 13");
            foreach (DataRow row in tbl.Rows)
            {
                if (!Server.Sync.Data.Has(-20000 + (int) row["id"], "bizzPayDay")) continue;
                if ((int) row["id"] == 91) continue;
                int payPay = (int) Server.Sync.Data.Get(-20000 + (int) row["id"], "bizzPayDay");
                if (payPay <= 1) continue;

                int sum = rand.Next(1, payPay);
                Server.Sync.Data.Set(-20000 + (int) row["id"], "bizzPayDay", payPay - sum);
                Business.AddMoney((int) row["id"], sum);
            }
        }

        /*private async Task WeatherSync()
        {
            await Delay(5000);
            if (DynamicWeather)
            {
                _dynamicWeatherTimeLeft -= 10;
                if (_dynamicWeatherTimeLeft < 10)
                {
                    _dynamicWeatherTimeLeft = 5 * 12 * 10;
                    RefreshWeather();
                }
            }
            else
            {
                _dynamicWeatherTimeLeft = 5 * 12 * 10;
            }
            TriggerClientEvent("vMenu:SetWeather", _currentWeather, Blackout, DynamicWeather);
        }*/
    }
}