using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class Sync : BaseScript
    {
        public static int Afk = 0;
        public static Vector3 AfkLastPos = new Vector3();
        
        /*public static int Day = 0;
        public static int Month = 0;
        public static int Year = 0;
        public static int Hour = 0;
        public static int Min = 0;
        public static int Sec = 0;
        public static float Temp = 27;
        public static string DayName = "Понедельник";*/
        
        public static int PedFind = -1;
        
        /*public static int RealHour = 0;
        public static string FullRealDateTime = "";
        
        private static readonly string[] DayNames = {"Воскресенье", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота"};*/
        
        public Sync()
        {
            EventHandlers.Add("ARP:SyncToClient", new Action<dynamic, dynamic>(ToClient));
            EventHandlers.Add("ARP:SyncVirtualWorldToClient", new Action<dynamic>(VirtualWorldToClient));
            EventHandlers.Add("ARP:SyncPlayerIdListToClient", new Action<dynamic>(PlayerListIdToClient));
            
            EventHandlers.Add("ARP:UpdateLoto", new Action<int, int>(UpdateLoto));
            
            Tick += TimerSync;
            Tick += VirtualWorldSync;
            Tick += TimerAfk;
        }

        public static void VirtualWorldToClient(dynamic players)
        {
            User.PlayerVirtualWorldList.Clear();
            foreach (var property in (IDictionary<String, Object>) players)
                User.PlayerVirtualWorldList.Add(property.Key, Convert.ToInt32(property.Value));
            User.UpdateVirtualWorld();
        }

        public static void PlayerListIdToClient(dynamic players)
        {
            User.PlayerIdList.Clear();
            foreach (var property in (IDictionary<String, Object>) players)
                User.PlayerIdList.Add(property.Key, Convert.ToInt32(property.Value));
        }

        public static void ToClient(dynamic data, dynamic skin)
        {
            var localData = (IDictionary<String, Object>) data;
            var localSkin = (IDictionary<String, Object>) skin;
            
            foreach (var property in typeof(PlayerData).GetProperties())
                property.SetValue(User.Data, localData[property.Name], null);
            
            foreach (var property in typeof(PlayerSkin).GetProperties())
                property.SetValue(User.Skin, localSkin[property.Name], null);
        }

        public static void ToServer()
        {
            TriggerServerEvent("ARP:SyncToServer", User.Data, User.Skin, GetPlayerServerId(PlayerId()));
        }

        public static void UpdateLoto(int loto, int money)
        {
            if (!Client.Sync.Data.HasLocally(User.GetServerId(), "loto")) return;
            
            if ((int) Client.Sync.Data.GetLocally(User.GetServerId(), "loto") == loto)
            {
                User.AddBankMoney(money);
                TriggerServerEvent("ARP:WinLotoLoto", User.Data.rp_name);
                Notification.SendPicture("Вы выиграли в лото $" + money, "Life Invader", "УРА!!!! Число: " + loto, "CHAR_LIFEINVADER", Notification.TypeChatbox);
            }
            else
                Notification.SendPicture("Ваш лотерейный билетик не выиграл :c", "Life Invader", "Эх... Число: " + loto, "CHAR_LIFEINVADER", Notification.TypeChatbox);
        }
        
        private static async Task VirtualWorldSync()
        {
            User.UpdateVirtualWorld();
        }
        
        private static async Task TimerSync()
        {
            await Delay(1000 * 60 * 3);
            if (User.IsLogin())
            {
                //Characher.UpdateCloth(false);
                Characher.UpdateFace();

                if (!User.IsRpAnim)
                {
                    User.RemoveEatLevel(16);
                    User.RemoveWaterLevel(2);
                }

                if (User.TimerAbduction > 0)
                {
                    User.TimerAbduction--;

                    if (User.TimerAbduction == 0)
                    {
                        var rand = new Random();
                        var index = rand.Next(5);
                        var pos = new Vector3((float) Main.CartelMiserkPos[index, 0], (float) Main.CartelMiserkPos[index, 1], (float) Main.CartelMiserkPos[index, 2]);
                        
                        Notification.SendPictureToFraction("Мистер К скинул координаты, едем туда", "Мистер К", "Дело", "CHAR_HUMANDEFAULT", Notification.TypeChatbox, 8);
                        Shared.SetWaypointToFraction(8, pos.X, pos.Y);
                        Chat.SendChatMessageWithCommand("По приезду откройте меню организации и нажмите \"Совершить сделку\", нужно находиться рядом с человеком");
                        Checkpoint.CreateWithMarker(pos, 5f, 5f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "checkpoint:withdelete");
                    }
                }
                
                if (User.GetEatLevel() < 100) {
                    SetEntityHealth(GetPlayerPed(-1), GetEntityHealth(GetPlayerPed(-1)) - 2);
                    Notification.SendWithTime("~y~Вы голодны.");
                    
                    if (User.Data.age == 18)
                        Notification.Send("~g~[HELP] Купите еду в магазине (M -> GPS)");
                }
                if (User.GetWaterLevel() < 5) {
                    SetEntityHealth(GetPlayerPed(-1), GetEntityHealth(GetPlayerPed(-1)) - 2);
                    Notification.SendWithTime("~y~Вы хотите пить.");
                    
                    if (User.Data.age == 18)
                        Notification.Send("~g~[HELP] Купите воду в магазине (M -> GPS)");
                }

                if (Client.Sync.Data.HasLocally(User.GetServerId(), "isTie") && User.GetNetwork() > 1)
                {
                    foreach (var p in Main.GetPedListOnRadius(100f))
                    {
                        if (User.IsAnimal(p.Model.Hash)) continue;
                        var rand = new Random();
                        if (rand.Next(2) != 0) continue;
                        p.Task.StartScenario("WORLD_HUMAN_STAND_MOBILE", p.Position);
                        await Delay(10000);
                        if (p.IsDead) break;
                        Dispatcher.SendEms("Код 3", "Свидетели сообщают о подозрительной активности");
                        p.Task.ClearAll();
                        break;
                    }
                }

                if (User.IsDead() && User.GetNetwork() > 1)
                {
                    foreach (var p in Main.GetPedListOnRadius(100f))
                    {
                        if (User.IsAnimal(p.Model.Hash)) continue;
                        p.Task.StartScenario("WORLD_HUMAN_STAND_MOBILE", p.Position);
                        await Delay(10000);
                        if (p.IsDead) break;
                        if (await Client.Sync.Data.Has(User.GetServerId(), "deathReason"))
                            Dispatcher.SendEms("Код 3", User.GetDeathReason((uint) await Client.Sync.Data.Get(User.GetServerId(), "deathReason")));
                       else
                            Dispatcher.SendEms("Код 2", User.GetDeathReason(0));
                        p.Task.ClearAll();
                        break;
                    }
                }
            }
        }
        
        private static async Task TimerAfk()
        {
            await Delay(1000 * 60 * 1);

            Main.UpdateDiscordStatus(User.GetPlayerVirtualWorld() != 0 ? "В помещении" : UI.GetPlayerZoneName());

            if (User.Data.sell_car_time > 0)
            {
                User.Data.sell_car_time--;
                Client.Sync.Data.Set(User.GetServerId(), "sell_car_time", User.Data.sell_car_time);
            }

            if (AfkLastPos == GetEntityCoords(GetPlayerPed(-1), true))
            {
                if (!User.IsRpAnim && new PlayerList().Count() > (Main.MaxPlayers - 5) && !User.IsDead())
                {
                    Afk++;
                
                    if (User.GetVipStatus() == "Hard")
                    {
                        switch (Afk)
                        {
                            case 25:
                                Notification.SendWithTime("~r~Вас кикнет через 5 минут за AFK");
                                break;
                            case 28:
                                Notification.SendWithTime("~r~Вас кикнет через 2 минуты за AFK");
                                break;
                            case 29:
                                Notification.SendWithTime("~r~Вас кикнет через 1 минуту за AFK");
                                break;
                            case 30:
                                User.Kick(PlayerId(), "AFK 30m");
                                break;
                        }
                    }
                    else if (User.GetVipStatus() == "Light" || User.GetVipStatus() == "YouTube")
                    {
                        switch (Afk)
                        {
                            case 15:
                                Notification.SendWithTime("~r~Вас кикнет через 5 минут за AFK");
                                break;
                            case 18:
                                Notification.SendWithTime("~r~Вас кикнет через 2 минуты за AFK");
                                break;
                            case 19:
                                Notification.SendWithTime("~r~Вас кикнет через 1 минуту за AFK");
                                break;
                            case 20:
                                User.Kick(PlayerId(), "AFK 20m");
                                break;
                        }
                    }
                    else
                    {
                        switch (Afk)
                        {
                            case 5:
                                Notification.SendWithTime("~r~Вас кикнет через 5 минут за AFK");
                                break;
                            case 8:
                                Notification.SendWithTime("~r~Вас кикнет через 2 минуты за AFK");
                                break;
                            case 9:
                                Notification.SendWithTime("~r~Вас кикнет через 1 минуту за AFK");
                                break;
                            case 10:
                                User.Kick(PlayerId(), "AFK 10m");
                                break;
                        }
                    }   
                }
            }
            else
                Afk = 0;
            
            AfkLastPos = GetEntityCoords(GetPlayerPed(-1), true);
        }
    }
}