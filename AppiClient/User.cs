using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client
{
    class MyEntity : Entity
    {
        public MyEntity(int handle) : base(handle)
        {
            
        }
    }
    
    public class User : BaseScript
    {
        public static PlayerData Data = new PlayerData();
        public static PlayerSkin Skin = new PlayerSkin();
        
        public static Dictionary<string, int> PlayerVirtualWorldList = new Dictionary<string, int>();
        public static Dictionary<string, int> PlayerIdList = new Dictionary<string, int>();
        public static Dictionary<string, bool> VehicleKeyList = new Dictionary<string, bool>();
        
        public static Dictionary<string, string> SmsList = new Dictionary<string, string>();
        
        public static int Bonus = 1;
        public static bool EnableRagdoll = false;

        public static int VirtWorld = 0;
        public static int Voice = 1;
        public static string VoiceString = "Нормально";

        public static int TimerAbduction = -1;
        
        public static int Amount = 50000;
        
        public static int Money = 0;
        public static int HealthLevel = 0;
        public static int EatLevel = 0;
        public static int WaterLevel = 0;
        public static float TempLevel = 0;
        public static bool IsAuth = false;
        public static bool IsBlockAnimation = false;
        public static bool IsRpAnim = false;
        public static bool healing = false;
        
        private static string _currentScenario = "";
        
        public User()
        {
            Tick += CheckInfo;
            Tick += HealNaKoike;
            EventHandlers.Add("ARP:PayDay", new Action(PayDay));
            EventHandlers.Add("ARP:AuthSuccess", new Action(AuthSuccess));
            EventHandlers.Add("ARP:SendPlayerSubTitle", new Action<string>(SendPlayerSubTitle));
            EventHandlers.Add("ARP:SendPlayerTooltip", new Action<string>(SendPlayerTooltip));
            EventHandlers.Add("ARP:SendToPlayerMenu", new Action<string, string, dynamic>(SendToPlayerMenu));
            EventHandlers.Add("ARP:SendToPlayerMembersMenu", new Action<dynamic>(SendToPlayerMembersMenu));
            EventHandlers.Add("ARP:SendToPlayerMembersMenu2", new Action<dynamic>(SendToPlayerMembersMenu2));
            EventHandlers.Add("ARP:SendToPlayersListMenu", new Action<dynamic>(SendToPlayersListMenu));
            EventHandlers.Add("ARP:SendToPlayerBankInfoMenu", new Action<dynamic>(SendToPlayerBankInfoMenu));
            EventHandlers.Add("ARP:SendToPlayerLogVehicleMenu", new Action<dynamic>(SendToPlayerLogVehicleMenu));
            EventHandlers.Add("ARP:SendToPlayerLogGunMenu", new Action<dynamic>(SendToPlayerLogGunMenu));
            EventHandlers.Add("ARP:SendToPlayerBusinessListMenu", new Action<dynamic, dynamic>(SendToPlayerBusinessListMenu));
            EventHandlers.Add("ARP:SendToPlayerSmsListMenu", new Action<dynamic, dynamic, dynamic, dynamic, string>(SendToPlayerSmsListMenu));
            EventHandlers.Add("ARP:SendToPlayerContacntListMenu", new Action<dynamic, dynamic, string>(SendToPlayerContacntListMenu));
            EventHandlers.Add("ARP:SendToPlayerSmsInfoMenu", new Action<int, string, string, string, string>(SendToPlayerSmsInfoMenu));
            EventHandlers.Add("ARP:SendToPlayerContInfoMenu", new Action<int, string, string>(SendToPlayerContInfoMenu));
            EventHandlers.Add("ARP:SendToPlayerApartmentListMenu", new Action<dynamic>(SendToPlayerApartmentListMenu));
            EventHandlers.Add("ARP:SendToPlayerItemListMenu", new Action<dynamic, dynamic, int, int>(SendToPlayerItemListMenu));
            EventHandlers.Add("ARP:SendToPlayerItemWorldListMenu", new Action<dynamic, dynamic>(SendToPlayerItemWorldListMenu));
            EventHandlers.Add("ARP:SendToPlayerItemListUpdateAmountMenu", new Action<dynamic, int, int>(SendToPlayerItemListUpdateAmountMenu));
            EventHandlers.Add("ARP:UpdateCashDisplay", new Action<int>(UpdateCashDisplay));
            EventHandlers.Add("ARP:AddNewSms", new Action<string, string>(AddNewSms));
            EventHandlers.Add("ARP:UpdateServerName", new Action<string>(UpdateServerName));
            EventHandlers.Add("ARP:UpdateLastName", new Action<string>(UpdateLastName));
            //EventHandlers.Add("ARP:UserPlayAnimation", new Action<string, string, int>(PlayAnimation));
            EventHandlers.Add("ARP:UserPlayAnimationToAll", new Action<int, string, string, int>(PlayAnimationToAll));
            EventHandlers.Add("ARP:UserPlayAnimationToPlayer", new Action<string, string, int>(PlayAnimationToPlayer));
            
            EventHandlers.Add("ARP:SendAskMessage", new Action<string, int, string>(SendAskMessage));
            EventHandlers.Add("ARP:SendReportMessage", new Action<string, int, string>(SendReportMessage));
            
            EventHandlers.Add("ARP:SendAskToPlayerMessage", new Action<string, int, int, string>(SendAskToPlayerMessage));
            EventHandlers.Add("ARP:SendReportToPlayerMessage", new Action<string, int, int, string>(SendReportToPlayerMessage));
            EventHandlers.Add("ARP:HightSapd", new Action<int>(HightSapd));
        }
   
        public static void HightSapd(int type)
        {
            switch (type)
            {
                case 0:
                    Sync.Data.SetLocally(GetServerId(), "hightSapd", true);
                    break;
                case 1:
                    Sync.Data.SetLocally(GetServerId(), "hightSapd", true);
                    break;
                case 2:
                    Sync.Data.SetLocally(GetServerId(), "hightSapd", true);
                    break;
                default:
                    Sync.Data.ResetLocally(GetServerId(), "hightSapd");
                    break;
            }
            Sync.Data.SetLocally(GetServerId(), "hightSapdType", type);
        }
   
        public static void SendAskMessage(string msg, int playerId, string rpName)
        {
            if (!IsHelper()) return;
            Chat.SendChatInfoMessage($"Вопрос от {rpName} ({playerId})", msg, "FFC107");
        }
   
        public static void SendReportMessage(string msg, int playerId, string rpName)
        {
            if (!IsAdmin()) return;
            Chat.SendChatInfoMessage($"Жалоба от {rpName} ({playerId})", msg, "f44336");
        }
   
        public static void SendAskToPlayerMessage(string msg, int id, int playerId, string rpName)
        {
            if (IsHelper() || IsAdmin())
                Chat.SendChatInfoMessage($"Ответ от хелпера {rpName} ({playerId}) игроку {id}", msg, "FFC107");
            if (Data.id != id) return;
            Chat.SendChatInfoMessage($"Ответ от хелпера {rpName} ({playerId})", msg, "FFC107");
        }
   
        public static void SendReportToPlayerMessage(string msg, int id, int playerId, string rpName)
        {
            if (IsAdmin())
                Chat.SendChatInfoMessage($"Ответ от администратора {rpName} ({playerId}) игроку {id}", msg, "f44336");
            if (Data.id != id) return;
            Chat.SendChatInfoMessage($"Ответ от администратора {rpName} ({playerId})", msg, "f44336");
        }
   
        public static void UpdateServerName(string serverName)
        {
            Main.ServerName = serverName;
            Debug.WriteLine("UpdateServerName: " + serverName);

            if (Main.ServerName == "Andromeda")
                Bonus = 2;
            if (Main.ServerName == "SunFlower")
                Bonus = 3;
            
            Main.MaxPlayers = 128;
            
            Objects.LoadAllObjects();
        }
   
        public static void UpdateLastName(string lastName)
        {
            Main.LastName = lastName;
            Debug.WriteLine("UpdateLastName: " + lastName);
        }
   
        public static void Kick(string reason = "You have been kicked")
        {
            TriggerServerEvent("ARP:KickPlayer", PlayerId(), reason);
        }
   
        public static void Kick(int playerId, string reason = "You have been kicked")
        {
            TriggerServerEvent("ARP:KickPlayer", playerId, reason);
        }
        
        public static void SetVirtualWorld(int id)
        {
            TriggerServerEvent("ARP:SetVirtualWorld", id);
        }
        
        public static void AddNewSms(string numberFrom, string text)
        {
            if (GetNetwork() < 1) return;
            text = (text.Length > 49) ? text.Remove(51) + "..." : text;
            Notification.SendPicture(text, "Входящее СМС", numberFrom, "CHAR_ARTHUR", Notification.TypeChatbox);    
        }
        
        public static void AuthSuccess()
        {
            TriggerEvent("chatCanOpen", true);
            
            /*if (Main.ServerName == "Andromeda")
                NetworkSetVoiceActive(false);*/
            
            var players = new PlayerList();
            TriggerServerEvent("ARP:CheckSoloSession", players.Count());
            
            if (GetVipStatus() == "none" && Data.last_login < Data.date_reg + 604800 && Data.age == 18 && Data.exp_age < 50)
                Notification.SendWithTime("~b~У Ваc активирован VIP Light на 1 неделю");
            
            Sync.Data.Set(GetServerId(), "deathReason", -1);
            
            Managers.Pickup.CreateTeleportPickups();

            /*await GetAllData();
            IsAuth = Data.is_auth;*/
            //Money = (int) await Sync.Data.Get(GetServerId(), "money");
            //Characher.UpdateFace();
        }
        
        public static void UpdateCashDisplay(int money)
        {
            Data.money = money;
        }
        
        public static void SendPlayerSubTitle(string text)
        {
            Notification.SendSubtitle(text);
        }
        
        public static void SendPlayerTooltip(string text)
        {
            UI.ShowToolTip(text);
        }
        
        public static void SendToPlayerMenu(string title, string desc, dynamic data)
        {
            MenuList.ShowToPlayerMenu(title, "~b~" + desc, data);
        }
        
        public static void SendToPlayerMembersMenu(dynamic data)
        {
            MenuList.ShowToPlayerMembersMenu(data);
        }
        
        public static void SendToPlayerMembersMenu2(dynamic data)
        {
            MenuList.ShowToPlayerMembersMenu2(data);
        }
        
        public static void SendToPlayersListMenu(dynamic data)
        {
            MenuList.ShowToPlayersListMenu(data);
        }
        
        public static void SendToPlayerBankInfoMenu(dynamic data)
        {
            MenuList.ShowToPlayerBankInfoMenu(data);
        }
        
        public static void SendToPlayerLogVehicleMenu(dynamic data)
        {
            MenuList.ShowToPlayerLogVehicleMenu(data);
        }
        
        public static void SendToPlayerLogGunMenu(dynamic data)
        {
            MenuList.ShowToPlayerLogGunMenu(data);
        }
        
        public static void SendToPlayerBusinessListMenu(dynamic data, dynamic data2)
        {
            MenuList.ShowToPlayerBusinessListMenu(data, data2);
        }
        
        public static void SendToPlayerSmsListMenu(dynamic data, dynamic data2, dynamic data3, dynamic data4, string phone)
        {
            MenuList.ShowPlayerPhoneSmsMenu(data, data2, data3, data4, phone);
        }
        
        public static void SendToPlayerContacntListMenu(dynamic data, dynamic data2, string phone)
        {
            MenuList.ShowPlayerPhoneBookMenu(data, data2, phone);
        }
        
        public static void SendToPlayerSmsInfoMenu(int id, string numberFrom, string numberTo, string text, string dateTime)
        {
            MenuList.ShowPlayerPhoneSmsInfoMenu(id, numberFrom, numberTo, text, dateTime);
        }
        
        public static void SendToPlayerContInfoMenu(int id, string title, string number)
        {
            MenuList.ShowPlayerPhoneContInfoMenu(id, title, number);
        }
        
        public static void SendToPlayerApartmentListMenu(dynamic data)
        {
            MenuList.ShowToPlayerApartmentListMenu(data);
        }
        
        public static void SendToPlayerItemListMenu(dynamic data, dynamic data2, int ownerId, int ownerType)
        {
            MenuList.ShowToPlayerItemListMenu(data, data2, ownerId, ownerType);
        }
        
        public static void SendToPlayerItemWorldListMenu(dynamic data, dynamic data2)
        {
            MenuList.ShowToPlayerItemWorldListMenu(data, data2);
        }
        
        public static void SendToPlayerItemListUpdateAmountMenu(dynamic data, int ownerId, int ownerType)
        {
            var tempData2 = (IDictionary<String, Object>) data;
            int sum = tempData2.Sum(property => Inventory.GetItemAmountById(Convert.ToInt32(property.Value)));
            Managers.Inventory.SetInvAmount(ownerId, ownerType, sum);
        }
        
        public static void UpdateVirtualWorld()
        {
            int playerWv = GetPlayerVirtualWorld();
            
            //NetworkSetVoiceChannel(playerWv);
            
            foreach (var player in new PlayerList())
            {
                if (GetPlayerServerId(PlayerId()) == player.ServerId || !PlayerVirtualWorldList.ContainsKey(player.ServerId.ToString())) continue;
                if (playerWv == PlayerVirtualWorldList[player.ServerId.ToString()]) continue;
                if (
                    playerWv == -27 ||
                    playerWv == -29 ||
                    playerWv == -80 ||
                    playerWv == -133 ||
                    playerWv == -135 ||
                    playerWv == -170 ||
                    playerWv == -202 ||
                    playerWv == -204 ||
                    playerWv == -208 ||
                    playerWv == -399 ||
                    playerWv == -417 ||
                    playerWv == -422 ||
                    playerWv == -433 ||
                    playerWv == -535
                    )
                    continue;
                
                Invisible(player.Handle, true);   
                SetEntityCoords(GetPlayerPed(player.Handle), 0, 0, 0, true, false, false, true);
            }
        }
        
        /*public static void GetPlayersIdWithRadius(Vector3 pos, float radius)
        {
            var list = new List<int>();
            
            foreach (var player in new PlayerList())
            {
                if (GetPlayerServerId(PlayerId()) == player.ServerId || !PlayerIdList.ContainsKey(player.ServerId.ToString())) continue;                
                if (playerWv != PlayerIdList[player.ServerId.ToString()])
                    Invisible(player.Handle, true);
            }
        }*/

        public static int GetPlayerVirtualWorld()
        {
            int playerWv = 0;
            if (PlayerVirtualWorldList.ContainsKey(GetPlayerServerId(PlayerId()).ToString()))
                playerWv = PlayerVirtualWorldList[GetPlayerServerId(PlayerId()).ToString()];
            return playerWv;
        }
        
        public static void Freeze(bool freeze)
        {
            Freeze(PlayerId(), freeze);
        }
        
        public static void Freeze(int playerId, bool freeze)
        {
            var ped = GetPlayerPed(playerId);
            
            SetPlayerControl(playerId, !freeze, 0);
            if (!freeze)
                FreezeEntityPosition(ped, false);
            else
            {
                FreezeEntityPosition(ped, true);
                
                if (IsPedFatallyInjured(ped))
                    ClearPedTasksImmediately(ped);
            }
        }
        
        public static void Invisible(int playerId, bool invisible)
        {
            var ped = GetPlayerPed(playerId);
            
            if (!invisible)
            {
                if (!IsEntityVisible(ped))
                    SetEntityVisible(ped, true, false);
                
                if (!IsPedInAnyVehicle(ped, true))
                    SetEntityCollision(ped, true, true);
        
                SetPlayerInvincible(playerId, false);
            } 
            else 
            {
                if (IsEntityVisible(ped))
                    SetEntityVisible(ped, false, false);
        
                SetEntityCollision(ped, false, true);
                SetPlayerInvincible(playerId, true);
            }
        }
        
        public static int GetMonth()
        {
            return Convert.ToInt32(Data.exp_age / 31);
        }
        
        public static string GetRegStatusName()
        {
            switch (Data.reg_status)
            {
                case 1:
                    return "временная";
                case 2:
                    return "получение гражданства";
                case 3:
                    return "гражданство США";
                default:
                    return "~r~Нет";
            }
        }
        
        public static string GetFractionName()
        {
            return Main.GetFractionName(Data.fraction_id);
        }

        public static string GetRankName()
        {
            return Main.GetRankName(Data.fraction_id, Data.rank);
        }

        public static string GetWorkName()
        {
            switch (Data.job)
            {
                case "swater":
                    return "Учёный - Гидролог";
                case "sground":
                    return "Учёный - Биолог";
                case "water":
                    return "Мехатроник";
                case "sunb":
                    return "Уборщик квартир";
                case "bgstar":
                    return "Дезинсектор";
                case "lawyer1":
                    return "Адвокат в UnitSA";
                case "lawyer2":
                    return "Адвокат в Planet-E";
                case "lawyer3":
                    return "Адвокат в Pearson Specter";
                case "trash":
                    return "Водитель мусоровоза";
                case "scrap":
                    return "Развозчик металлолома";
                case "mail":
                    return "Почтальон в PostOp";
                case "mail2":
                    return "Почтальон в GoPostal";
                case "three":
                    return "Садовник";
                case "GrSix":
                    return "Инкассатор";
                case "photo":
                    return "Фотограф";
                case "taxi1":
                    return "Водитель такси";
                case "taxi2":
                    return "Водитель маршрутного такси";
                case "bus1":
                    return "Городской автобус";
                case "bus2":
                    return "Трансферный автобус";
                case "bus3":
                    return "Рейсовый автобус";
                case "meh":
                    return "Механик";
                default:
                    return "~r~Нет";
            }
        }
        
        public static bool IsMuted()
        {
            return Data.date_mute > Main.GetTimeStamp();
        }

        public static bool IsGang()
        {
            return Data.fraction_id > 9 && Data.fraction_id < 15;
        }

        public static bool IsLeader2()
        {
            return Data.rank2 == 11;
        }

        public static bool IsSubLeader2()
        {
            return Data.rank2 >= 9;
        }

        public static bool IsLeader()
        {
            return Data.rank == 14;
        }

        public static bool IsSubLeader()
        {
            switch (Data.fraction_id)
            {
                case 1:
                    return Data.rank == 13;
                case 2:
                    return Data.rank >= 12;
                case 3:
                    return Data.rank >= 7;
                case 4:
                    return Data.rank >= 13;
                case 5:
                    return Data.rank >= 14;
                case 6:
                    return Data.rank >= 14;
                case 7:
                    return Data.rank >= 14;
                case 8:
                    return Data.rank >= 10;
                case 9:
                    return Data.rank >= 10;
                case 10:
                    return Data.rank >= 10;
                case 11:
                    return Data.rank >= 10;
                case 12:
                    return Data.rank >= 10;
                case 13:
                    return Data.rank >= 10;
                case 14:
                    return Data.rank >= 10;
                case 15:
                    return Data.rank >= 11;
                case 16:
                    return Data.rank >= 13;
                default:
                    return false;
            }
        }

        public static bool IsGos()
        {
            return IsLogin() && (Data.fraction_id == 1 || Data.fraction_id == 2 || Data.fraction_id == 3 || Data.fraction_id == 4 || Data.fraction_id == 7 || Data.fraction_id == 16);
        }

        public static bool IsGov()
        {
            return IsLogin() && Data.fraction_id == 1;
        }

        public static bool IsSapd()
        {
            return IsLogin() && Data.fraction_id == 2;
        }

        public static bool IsEms()
        {
            return IsLogin() && Data.fraction_id == 16;
        }

        public static bool IsSheriff()
        {
            return IsLogin() && Data.fraction_id == 7;
        }

        public static bool IsFib()
        {
            return IsLogin() && (Data.fraction_id == 3 || Data.fraction_id == 2 && Data.rank >= 5);
        }

        public static bool IsCartel()
        {
            return IsLogin() && Data.fraction_id == 8;
        }

        public static bool IsGrove()
        {
            return IsLogin() && Data.fraction_id == 10;
        }

        public static bool IsBallas()
        {
            return IsLogin() && Data.fraction_id == 11;
        }

        public static bool IsMara()
        {
            return IsLogin() && Data.fraction_id == 14;
        }

        public static bool IsJobTrash()
        {
            return IsLogin() && Data.job == "trash";
        }

        public static bool IsJobBus1()
        {
            return IsLogin() && Data.job == "bus1";
        }

        public static bool IsJobBus2()
        {
            return IsLogin() && Data.job == "bus2";
        }

        public static bool IsJobBus3()
        {
            return IsLogin() && Data.job == "bus3";
        }

        public static bool IsJobSunb()
        {
            return IsLogin() && Data.job == "sunb";
        }

        public static bool IsJobConnor()
        {
            return IsLogin() && Data.job == "three";
        }

        public static bool IsJobBgstar()
        {
            return IsLogin() && Data.job == "bgstar";
        }

        public static bool IsJobWap()
        {
            return IsLogin() && Data.job == "water";
        }

        public static bool IsJobScrap()
        {
            return IsLogin() && Data.job == "scrap";
        }

        public static bool IsJobPhoto()
        {
            return IsLogin() && Data.job == "photo";
        }

        public static bool IsJobMail()
        {
            return IsLogin() && (Data.job == "mail" || Data.job == "mail2");
        }

        public static bool IsJobLab()
        {
            return IsLogin() && (IsJobScienceGround() || IsJobScienceWater());
        }

        public static bool IsJobScienceGround()
        {
            return Data.job == "sground";
        }

        public static bool IsJobGroupSix()
        {
            return Data.job == "GrSix";
        }

        public static bool IsJobScienceWater()
        {
            return Data.job == "swater";
        }

        public static bool IsDuty()
        {
            return IsLogin() && Sync.Data.HasLocally(GetServerId(), "duty");
        }

        public static bool IsAdmin(int adminLevel = 1)
        {
            return IsLogin() && Data.admin_level >= adminLevel;
        }

        public static bool IsHelper(int helperLevel = 1)
        {
            return IsLogin() && Data.helper_level >= helperLevel;
        }

        public static bool IsLogin()
        {
            return IsAuth;
        }

        public static bool IsAnimal(int hash)
        {
            switch ((PedHash) hash)
            {
                case PedHash.Boar:
                case PedHash.Cat:
                case PedHash.ChickenHawk:
                case PedHash.Chimp:
                case PedHash.Chop:
                case PedHash.Cormorant:
                case PedHash.Cow:
                case PedHash.Coyote:
                case PedHash.Crow:
                case PedHash.Deer:
                case PedHash.Dolphin:
                case PedHash.Fish:
                case PedHash.HammerShark:
                case PedHash.Hen:
                case PedHash.Humpback:
                case PedHash.Husky:
                case PedHash.KillerWhale:
                case PedHash.MountainLion:
                case PedHash.Orleans:
                case PedHash.OrleansCutscene:
                case PedHash.Pig:
                case PedHash.Pigeon:
                case PedHash.Poodle:
                case PedHash.Pug:
                case PedHash.Rabbit:
                case PedHash.Rat:
                case PedHash.Retriever:
                case PedHash.Rhesus:
                case PedHash.Rottweiler:
                case PedHash.Seagull:
                case PedHash.Shepherd:
                case PedHash.Stingray:
                case PedHash.TigerShark:
                case PedHash.Westy:
                    return true;
            }
            return false;
        }

        public static bool IsDriver()
        {
            if (!IsPedInAnyVehicle(PlayerPedId(), true)) return false;
            var veh = GetVehiclePedIsUsing(PlayerPedId());
            return GetPedInVehicleSeat(veh, -1) == PlayerPedId();
        }

        public static int GetVehicleIsDriver()
        {
            if (!IsPedInAnyVehicle(PlayerPedId(), true)) return -1;
            var veh = GetVehiclePedIsUsing(PlayerPedId());
            return GetPedInVehicleSeat(veh, -1) == PlayerPedId() ? veh : -1;
        }

        public static bool AddVehicleKey(string key)
        {
            /*if (VehicleKeyList.Count > 10)
                return false;*/
            
            VehicleKeyList.Add(key.Replace(" ", string.Empty), true);
            return true;
        }

        public static void RemoveVehicleKey(string key)
        {
            VehicleKeyList.Remove(key);
        }

        public static bool HasVehicleKey(string key)
        {
            return VehicleKeyList.ContainsKey(key);
        }
        
        public static void GiveWeapon(uint hash, int pt, bool p1 = false, bool p2 = true)
        {
            Client.Sync.Data.SetLocally(0, hash.ToString(), true);
            Client.Sync.Data.Set(User.GetServerId(), hash.ToString(), pt);
            GiveWeaponToPed(GetPlayerPed(-1), hash, pt, p1, p2);
        }

        public static void SlapMp(float x, float y, float z, int r, int h)
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            if (!(Main.GetDistanceToSquared(playerPos, new Vector3(x, y, z)) < r)) return;
            GiveWeapon((uint) WeaponHash.Parachute, 1, false, false);
            Teleport(playerPos + new Vector3(0, 0, h));
        }

        public static void GiveGunMp(float x, float y, float z, int r, string gun, int count)
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            if (!(Main.GetDistanceToSquared(playerPos, new Vector3(x, y, z)) < r)) return;
            
            foreach(uint hash in Enum.GetValues(typeof(WeaponHash)))
            {
                string name = Enum.GetName(typeof(WeaponHash), hash);
                if (!String.Equals(name, gun, StringComparison.CurrentCultureIgnoreCase)) continue;
                GiveWeapon((uint) hash, (int) count, false, true);
            }
        }

        public static void GiveSkinMp(float x, float y, float z, int r, string skin)
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            if (!(Main.GetDistanceToSquared(playerPos, new Vector3(x, y, z)) < r)) return;
            SetSkin(skin);
        }

        public static void GiveArmorMp(float x, float y, float z, int r, int count)
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            if (!(Main.GetDistanceToSquared(playerPos, new Vector3(x, y, z)) < r)) return;
            SetPedArmour(GetPlayerPed(-1), count);
        }

        public static void GiveHealthMp(float x, float y, float z, int r, int count)
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            if (!(Main.GetDistanceToSquared(playerPos, new Vector3(x, y, z)) < r)) return;
            SetEntityHealth(GetPlayerPed(-1), 100 + count);
        }

        public static void AdminJailPlayer(int id, int count, string reason)
        {
            if (Data.id != id) return;
            if (count == 0)
            {
                Jail.JailFreePlayer();   
                Notification.SendWithTime("~r~Админ вас выпустил из тюрьмы");
                return;
            }
            Jail.JailPlayer(count * 60);
            Notification.SendWithTime("~r~Админ вас посадил в тюрьму");
            Notification.SendWithTime("~r~" + reason);
        }

        public static async void GiveCashMoney(int serverId, int count)
        {
            if (Data.money < count)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                return;
            }

            int money = Convert.ToInt32(await Sync.Data.Get(serverId, "money"));
            if (money < 0) return;
            if (count < 0) return;
            
            Main.SaveLog("GiveMoney", $"ID FROM {Data.rp_name} TO {await Sync.Data.Get(serverId, "rp_name")} ${count}");
            
            RemoveCashMoney(count);
            Sync.Data.Set(serverId, "money", money + count);
            
            Shared.TriggerEventToPlayer(serverId, "ARP:UserPlayAnimationToPlayer", "mp_common","givetake2_a", 8);
            PlayAnimation("mp_common","givetake1_a", 8);
                
            TriggerServerEvent("ARP:UpdatePlayerCashDisplay", serverId);
            TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "Вам передали ~g~$" + count, serverId);
            Notification.SendSubtitle("Вы передали ~g~$" + count);
        }
        
        public static void CheckTime()
        {
            Notification.Send($"~g~Ваш ID:~s~ {Data.id}\n~g~Время:~s~ {DateTime.Now:dd/MM/yyyy} {DateTime.Now:HH:mm}");
            if (Data.jail_time > 0)
                Notification.Send($"~g~Время в тюрьме:~s~ {Data.jail_time} сек.");
            if (IsMuted())
                Notification.Send($"~g~Время окончания мута:~s~ {Main.UnixTimeStampToDateTime(Data.date_mute)}");
        }

        public static int GetServerIdById(int id)
        {
            foreach (var item in PlayerIdList)
                if (item.Value == id)
                    return Convert.ToInt32(item.Key);
            return -1;
        }

        public static Player GetPlayerById(int id)
        {
            int serverId = GetServerIdById(id);
            return new PlayerList().FirstOrDefault(p => p.ServerId == serverId);
        }
        
        /*Eat Level*/
        
        public static bool AddEatLevel(int level)
        {
            if(level > 900) { SetEatLevel(1100); return true; }
            if(GetEatLevel() + level > 1000) { SetEatLevel(1000); return true; }
            SetEatLevel(GetEatLevel() + level);
            return true;
        }

        public static bool RemoveEatLevel(int level)
        {
            if(GetEatLevel() - level < 0) { SetEatLevel(0); return true; }
            SetEatLevel(GetEatLevel() - level);
            return true;
        }

        public static bool SetEatLevel(int level)
        {
            Data.eat_level = level;
            Sync.Data.Set(GetServerId(), "eat_level", level);
            return true;
        }

        public static int GetEatLevel()
        {
            return Data.eat_level;
        }
        
        /*Drunk Level*/

        public static void AddDrunkLevel(int level)
        {
            SetDrunkLevel(GetDrunkLevel() + level);
        }

        public static void RemoveDrunkLevel(int level)
        {
            if(GetDrunkLevel() - level < 0) { SetDrunkLevel(0); return; }
            SetDrunkLevel(GetDrunkLevel() - level);
        }

        public static void SetDrunkLevel(int level)
        {
            Sync.Data.SetLocally(GetServerId(), "drunk_level", level);
            Sync.Data.Set(GetServerId(), "drunk_level", level);
        }

        public static int GetDrunkLevel()
        {
            return (int) Sync.Data.GetLocally(GetServerId(), "drunk_level");
        }
        
        /*Drug Level*/

        public static void AddDrugLsdLevel(int level)
        {
            SetDrugLsdLevel(GetDrugLsdLevel() + level);
        }

        public static void RemoveDrugLsdLevel(int level)
        {
            if(GetDrugLsdLevel() - level < 0) { SetDrugLsdLevel(0); return; }
            SetDrugLsdLevel(GetDrugLsdLevel() - level);
        }

        public static void SetDrugLsdLevel(int level)
        {
            Sync.Data.SetLocally(GetServerId(), "drug_lsd_level", level);
            Sync.Data.Set(GetServerId(), "drug_lsd_level", level);
        }

        public static int GetDrugLsdLevel()
        {
            return (int) Sync.Data.GetLocally(GetServerId(), "drug_lsd_level");
        }

        public static void AddDrugDmtLevel(int level)
        {
            SetDrugDmtLevel(GetDrugDmtLevel() + level);
        }

        public static void RemoveDrugDmtLevel(int level)
        {
            if(GetDrugDmtLevel() - level < 0) { SetDrugDmtLevel(0); return; }
            SetDrugDmtLevel(GetDrugDmtLevel() - level);
        }

        public static void SetDrugDmtLevel(int level)
        {
            Sync.Data.SetLocally(GetServerId(), "drug_dmt_level", level);
            Sync.Data.Set(GetServerId(), "drug_dmt_level", level);
        }

        public static int GetDrugDmtLevel()
        {
            return (int) Sync.Data.GetLocally(GetServerId(), "drug_dmt_level");
        }

        public static void AddDrugCocaLevel(int level)
        {
            SetDrugCocaLevel(GetDrugCocaLevel() + level);
        }

        public static void RemoveDrugCocaLevel(int level)
        {
            if(GetDrugCocaLevel() - level < 0) { SetDrugCocaLevel(0); return; }
            SetDrugCocaLevel(GetDrugCocaLevel() - level);
        }

        public static void SetDrugCocaLevel(int level)
        {
            Sync.Data.SetLocally(GetServerId(), "drug_coca_level", level);
            Sync.Data.Set(GetServerId(), "drug_coca_level", level);
        }

        public static int GetDrugCocaLevel()
        {
            return (int) Sync.Data.GetLocally(GetServerId(), "drug_coca_level");
        }

        public static void AddDrugAmfLevel(int level)
        {
            SetDrugAmfLevel(GetDrugAmfLevel() + level);
        }

        public static void RemoveDrugAmfLevel(int level)
        {
            if(GetDrugAmfLevel() - level < 0) { SetDrugAmfLevel(0); return; }
            SetDrugAmfLevel(GetDrugAmfLevel() - level);
        }

        public static void SetDrugAmfLevel(int level)
        {
            Sync.Data.SetLocally(GetServerId(), "drug_amf_level", level);
            Sync.Data.Set(GetServerId(), "drug_amf_level", level);
        }

        public static int GetDrugAmfLevel()
        {
            return (int) Sync.Data.GetLocally(GetServerId(), "drug_amf_level");
        }

        //Мефедрон
        public static void AddDrugMefLevel(int level)
        {
            SetDrugMefLevel(GetDrugMefLevel() + level);
        }

        public static void RemoveDrugMefLevel(int level)
        {
            if(GetDrugMefLevel() - level < 0) { SetDrugMefLevel(0); return; }
            SetDrugMefLevel(GetDrugMefLevel() - level);
        }

        public static void SetDrugMefLevel(int level)
        {
            Sync.Data.SetLocally(GetServerId(), "drug_mef_level", level);
            Sync.Data.Set(GetServerId(), "drug_mef_level", level);
        }

        public static int GetDrugMefLevel()
        {
            return (int) Sync.Data.GetLocally(GetServerId(), "drug_mef_level");
        }

        //Кетамин
        public static void AddDrugKetLevel(int level)
        {
            SetDrugKetLevel(GetDrugKetLevel() + level);
        }

        public static void RemoveDrugKetLevel(int level)
        {
            if(GetDrugKetLevel() - level < 0) { SetDrugKetLevel(0); return; }
            SetDrugKetLevel(GetDrugKetLevel() - level);
        }

        public static void SetDrugKetLevel(int level)
        {
            Sync.Data.SetLocally(GetServerId(), "drug_ket_level", level);
            Sync.Data.Set(GetServerId(), "drug_ket_level", level);
        }

        public static int GetDrugKetLevel()
        {
            return (int) Sync.Data.GetLocally(GetServerId(), "drug_ket_level");
        }

        public static void AddDrugMargLevel(int level)
        {
            SetDrugMargLevel(GetDrugMargLevel() + level);
        }

        public static void RemoveDrugMargLevel(int level)
        {
            if(GetDrugMargLevel() - level < 0) { SetDrugMargLevel(0); return; }
            SetDrugMargLevel(GetDrugMargLevel() - level);
        }

        public static void SetDrugMargLevel(int level)
        {
            Sync.Data.SetLocally(GetServerId(), "drug_marg_level", level);
            Sync.Data.Set(GetServerId(), "drug_marg_level", level);
        }

        public static int GetDrugMargLevel()
        {
            return (int) Sync.Data.GetLocally(GetServerId(), "drug_marg_level");
        }

        public static bool IsDrugDrunk()
        {
            return GetDrugAmfLevel() > 0 || GetDrugMefLevel() > 0 || GetDrugLsdLevel() > 0 || GetDrugKetLevel() > 0 || GetDrugCocaLevel() > 0 || GetDrugDmtLevel() > 0;
        }

        public static int GetDrugDrunkLevel()
        {
            return GetDrugAmfLevel() + GetDrugMefLevel() + GetDrugLsdLevel() + GetDrugKetLevel() + GetDrugCocaLevel() + GetDrugDmtLevel();
        }
        
        /*Water Level*/

        public static bool AddWaterLevel(int level)
        {
            if(level > 100) { SetWaterLevel(110); return true; }
            if(GetWaterLevel() + level > 100) { SetWaterLevel(100); return true; }
            SetWaterLevel(GetWaterLevel() + level);
            return true;
        }

        public static bool RemoveWaterLevel(int level)
        {
            if(GetWaterLevel() - level < 0) { SetWaterLevel(0); return true; }
            SetWaterLevel(GetWaterLevel() - level);
            return true;
        }

        public static bool SetWaterLevel(int level)
        {
            Data.water_level = level;
            Sync.Data.Set(GetServerId(), "water_level", level);
            return true;
        }

        public static int GetWaterLevel()
        {
            return Data.water_level;
        }
        
        /*Health Level*/

        public static bool AddHealthLevel(int level)
        {
            if (GetHealthLevel() + level > 100) SetHealthLevel(100);
            else SetHealthLevel(GetHealthLevel() + level);
            return true;
        }

        public static bool RemoveHealthLevel(int level)
        {
            if (GetHealthLevel() - level < 0) SetHealthLevel(0);
            else SetHealthLevel(GetHealthLevel() - level);
            return true;
        }

        public static bool SetHealthLevel(int level)
        {
            Data.health_level = level;
            Sync.Data.Set(GetServerId(), "health_level", level);
            return true;
        }

        public static int GetHealthLevel()
        {
            return Data.health_level;
        }
        
        /*Temp Level*/
        
        public static bool AddTempLevel(float level)
        {
            if(GetTempLevel() + level > 100) { SetTempLevel(100); return true; }
            SetTempLevel(GetTempLevel() + level);
            return true;
        }

        public static bool RemoveTempLevel(float level)
        {
            if(GetTempLevel() - level < 0) { SetTempLevel(0); return true; }
            SetTempLevel(GetTempLevel() - level);
            return true;
        }

        public static bool SetTempLevel(float level)
        {
            Data.temp_level = level;
            Sync.Data.Set(GetServerId(), "temp_level", level);
            return true;
        }

        public static float GetTempLevel()
        {
            return Data.temp_level;
        }

        public static void UpdateLevel()
        {
            // < 35.2 - холодно
            // Ниже 32 не может быть
            // > 36.9 заболел
            //
            
            if (Data.jail_time > 1 || IsDuty())
                return;
            
            dynamic[,] cloth = User.Skin.SEX == 1 ? Business.Cloth.ClothF : Business.Cloth.ClothM;
            
            for (int i = 0; i < cloth.Length / 12; i++)
            {
                if ((int) cloth[i, 1] != 11) continue;
                if ((int) cloth[i, 2] != User.Data.body) continue;
                if ((int) cloth[i, 10] > Managers.Weather.Temp)
                {
                    Notification.SendWithTime("~y~У Вас замерз торс");
                    SetEntityHealth(GetPlayerPed(-1), GetEntityHealth(GetPlayerPed(-1)) - 3);
                    break;
                }
                if ((int) cloth[i, 11] < Managers.Weather.Temp)
                {
                    Notification.SendWithTime("~y~Вашему торсу жарко");
                    break;
                }
            }
            
            for (int i = 0; i < cloth.Length / 12; i++)
            {
                if ((int) cloth[i, 1] != 4) continue;
                if ((int) cloth[i, 2] != User.Data.leg) continue;
                if ((int) cloth[i, 10] > Managers.Weather.Temp)
                {
                    Notification.SendWithTime("~y~У Вас замерзли ноги");
                    SetEntityHealth(GetPlayerPed(-1), GetEntityHealth(GetPlayerPed(-1)) - 3);
                    break;
                }
                if ((int) cloth[i, 11] < Managers.Weather.Temp)
                {
                    Notification.SendWithTime("~y~Вашим ногам жарко");
                    break;
                }
            }
            
            for (int i = 0; i < cloth.Length / 12; i++)
            {
                if ((int) cloth[i, 1] != 6) continue;
                if ((int) cloth[i, 2] != User.Data.foot) continue;
                if ((int) cloth[i, 10] > Managers.Weather.Temp)
                {
                    Notification.SendWithTime("~y~У Вас замерзли ступни");
                    SetEntityHealth(GetPlayerPed(-1), GetEntityHealth(GetPlayerPed(-1)) - 3);
                    break;
                }
                if ((int) cloth[i, 11] < Managers.Weather.Temp)
                {
                    Notification.SendWithTime("~y~Вашим ступням жарко");
                    break;
                }
            }

            if (Client.Sync.Data.HasLocally(User.GetServerId(), "is_torso") && Managers.Weather.Temp < 20)
            {
                SetEntityHealth(GetPlayerPed(-1), GetEntityHealth(GetPlayerPed(-1)) - 3);
                Notification.SendWithTime("~y~У Вас замерз торс");
            }

            if (Client.Sync.Data.HasLocally(User.GetServerId(), "is_leg") && Managers.Weather.Temp < 20)
            {
                SetEntityHealth(GetPlayerPed(-1), GetEntityHealth(GetPlayerPed(-1)) - 3);
                Notification.SendWithTime("~y~У Вас замерзли ноги");
            }

            if (Client.Sync.Data.HasLocally(User.GetServerId(), "is_foot") && Managers.Weather.Temp < 20)
            {
                SetEntityHealth(GetPlayerPed(-1), GetEntityHealth(GetPlayerPed(-1)) - 3);
                Notification.SendWithTime("~y~У Вас замерзли ступни");
            }
            
        }

        public static async Task HealNaKoike()
        {
            
            if (healing == true)
            {
                await Delay(3000);
                SetEntityHealth(GetPlayerPed(-1), GetEntityHealth(GetPlayerPed(-1)) + 2);
            }
        }
        
        /*Wanted Level*/
        
        public static bool AddWantedLevel(int level, string reason)
        {
            if(GetWantedLevel() + level > 10) { SetWantedLevel(10, reason); return true; }
            SetWantedLevel(GetWantedLevel() + level, reason);
            return true;
        }

        public static bool RemoveWantedLevel(int level, string reason)
        {
            if(GetWantedLevel() - level < 0) { SetWantedLevel(0, reason); return true; }
            SetWantedLevel(GetWantedLevel() - level, reason);
            return true;
        }

        public static bool SetWantedLevel(int level, string reason)
        {
            Data.wanted_level = level;
            Data.wanted_reason = reason;
            Sync.Data.Set(GetServerId(), "wanted_level", level);
            Sync.Data.Set(GetServerId(), "wanted_reason", reason);
            Notification.Send("~r~Вас розыскивает полиция\n~y~" + reason);
            return true;
        }

        public static int GetWantedLevel()
        {
            return Data.wanted_level;
        }
        
        /*Money*/

        public static async void AddMoney(int money)
        {
            int moneyNow = await GetMoney();
            Main.SaveLog("money", $"[ADD] {Data.rp_name} {moneyNow} + {money}");
            SetMoney(moneyNow + money);
        }

        public static async void RemoveMoney(int money)
        {
            int moneyNow = await GetMoney();
            Main.SaveLog("money", $"[REMOVE] {Data.rp_name} {moneyNow} - {money}");
            SetMoney(moneyNow - money);
        }

        public static void SetMoney(int money)
        {
            SetCashMoney(money);
        }

        public static async Task<int> GetMoney()
        {
            return await GetCashMoney();
        }

        public static int GetMoneyWithoutSync()
        {
            return Data.money;
        }
        
        /*Money Cash*/

        public static async void AddCashMoney(int money)
        {
            int moneyNow = await GetCashMoney();
            Main.SaveLog("money", $"[ADD-CASH] {Data.rp_name} {moneyNow} + {money}");
            SetCashMoney(moneyNow + money);
        }

        public static async void RemoveCashMoney(int money)
        {
            int moneyNow = await GetCashMoney();
            Main.SaveLog("money", $"[REMOVE-CASH] {Data.rp_name} {moneyNow} - {money}");
            SetCashMoney(moneyNow - money);
        }

        public static void SetCashMoney(int money)
        {
            Data.money = money;
            Sync.Data.Set(GetServerId(), "money", money);
        }

        public static async Task<int> GetCashMoney()
        {
            return Data.money = (int) await Sync.Data.Get(GetServerId(), "money");
        }
        
        /*Money Bank*/

        public static async void AddBankMoney(int money)
        {
            int moneyNow = await GetBankMoney();
            Main.SaveLog("money", $"[ADD-BANK] {Data.rp_name} {moneyNow} + {money}");
            SetBankMoney(moneyNow + money);
        }

        public static async void RemoveBankMoney(int money)
        {
            int moneyNow = await GetBankMoney();
            Main.SaveLog("money", $"[REMOVE-BANK] {Data.rp_name} {moneyNow} - {money}");
            SetBankMoney(moneyNow - money);
        }

        public static void SetBankMoney(int money)
        {
            Data.money_bank = money;
            Sync.Data.Set(GetServerId(), "money_bank", money);
        }

        public static async Task<int> GetBankMoney()
        {
            return Data.money_bank = (int) await Sync.Data.Get(GetServerId(), "money_bank");
        }
        
        /*Money Payday*/

        public static void AddPayDayMoney(int money)
        {
            SetPayDayMoney(GetPayDayMoney() + money);
        }

        public static void RemovePayDayMoney(int money)
        {
            SetPayDayMoney(GetPayDayMoney() - money);
        }

        public static void SetPayDayMoney(int money)
        {
            Data.money_payday = money;
            Sync.Data.Set(GetServerId(), "money_payday", money);
        }

        public static int GetPayDayMoney()
        {
            return Data.money_payday;
        }

        public static void GiveJobMoney(int money)
        {
            money = money * User.Bonus;
            if (Data.bank_prefix == 0)
            {
                AddCashMoney(money);
                Business.Bank.SendSmsBankOperation("~y~Оформите банковскую карту");
            }
            else
            {
                AddBankMoney(money);
                Business.Bank.SendSmsBankOperation($"Зачисление средств: ~g~${money}");
            }
            
            Coffer.RemoveMoney(money);

            switch (Data.job)
            {
                case "mail":
                    Business.Business.AddMoney(115, money / 2);
                    break;
                case "mail2":
                    Business.Business.AddMoney(119, money / 2);
                    break;
                case "bgstar":
                    Business.Business.AddMoney(116, money / 2);
                    break;
                case "sunb":
                    Business.Business.AddMoney(117, money / 2);
                    break;
                case "three":
                    Business.Business.AddMoney(118, money / 2);
                    break;
                case "photo":
                    Business.Business.AddMoney(92, money / 2);
                    break;
            }
        }

        public static async void SaveAccount()
        {
            Managers.Sync.ToServer();
            await Delay(500);
            TriggerServerEvent("ARP:SaveUserAccount");
        }

        public static bool CanOpenVehicle(int vehId, int vHandle)
        {
            if (HasVehicleKey(Managers.Vehicle.GetVehicleNumber(vHandle)))
                return true;
            
            if (!Managers.Vehicle.HasVehicleId(vehId))
                return false;
            //if (!Managers.Vehicle.VehicleInfoGlobalDataList.Contains(veh)) return false;
            
            return IsAdmin() ||
                   Managers.Vehicle.VehicleInfoGlobalDataList[vehId].IsUserOwner &&
                   (
                       Managers.Vehicle.VehicleInfoGlobalDataList[vehId].id == Data.car_id1 ||
                       Managers.Vehicle.VehicleInfoGlobalDataList[vehId].id == Data.car_id2 ||
                       Managers.Vehicle.VehicleInfoGlobalDataList[vehId].id == Data.car_id3 ||
                       Managers.Vehicle.VehicleInfoGlobalDataList[vehId].id == Data.car_id4 ||
                       Managers.Vehicle.VehicleInfoGlobalDataList[vehId].id == Data.car_id5 ||
                       Managers.Vehicle.VehicleInfoGlobalDataList[vehId].id == Data.car_id6 ||
                       Managers.Vehicle.VehicleInfoGlobalDataList[vehId].id == Data.car_id7 ||
                       Managers.Vehicle.VehicleInfoGlobalDataList[vehId].id == Data.car_id8
                   ) ||
                   Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Job != "" && 
                   Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Job == Data.job ||
                   HasVehicleKey(Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Number);
        }

        public static async void GetAllDataEvent()
        {
            await GetAllData();
        }

        public static async Task<bool> GetAllData()
        {
            try
            {
                dynamic data = await Sync.Data.GetAll(GetServerId(), 1000);    
                if (data == null) return false;
            
                var localData = (IDictionary<String, Object>) data;
                foreach (var property in typeof(PlayerData).GetProperties())
                    property.SetValue(Data, localData[property.Name], null);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Ex{e}");
                throw;
            }
            return false;
        }

        public static async Task<PlayerData> GetAllDataByServerId(int serverId)
        {
            try
            {
                if (!await Sync.Data.Has(serverId, "id")) return new PlayerData();

                dynamic data = await Sync.Data.GetAll(serverId, 1000);
                if (data == null) return new PlayerData();

                var userData = new PlayerData();
                var localData = (IDictionary<String, Object>) data;
                foreach (var property in typeof(PlayerData).GetProperties())
                    property.SetValue(userData, localData[property.Name], null);
                return userData;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Ex{e}");
                throw;
            }
            return new PlayerData();
        }

        public static async Task<bool> GetAllSkin()
        {
            try
            {
                dynamic data = await Sync.Data.GetAll(GetServerId(), 1000);
                if (data == null) return false;
            
                var localData = (IDictionary<String, Object>) data;
                foreach (var property in typeof(PlayerSkin).GetProperties())
                    property.SetValue(Skin, localData[property.Name], null);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Ex{e}");
                throw;
            }
            return false;
        }

        public static int GetNetwork()
        {
            return Convert.ToInt32(Ctos.UserNetwork * 100);
        }

        public static int GetServerId()
        {
            return GetPlayerServerId(PlayerId());
        }

        public static void VTeleport(float x, float y, float z, float rot = 0)
        {
            VTeleport(new Vector3(x, y, z), rot);
        }

        public static async void VTeleport(Vector3 pos, float rot = 0)
        {
            if (!IsPedInAnyVehicle(PlayerPedId(), true))
            {
                Teleport(pos);
                return;
            }
            
            var veh = GetVehiclePedIsUsing(PlayerPedId());
            RequestCollisionAtCoord(pos.X, pos.Y, pos.Z);
            DoScreenFadeOut(500);
            while (IsScreenFadingOut())
                await Delay(1);
            await Delay(1000);
            
            Freeze(PlayerId(), true);
            SetEntityCoords(veh, pos.X, pos.Y, pos.Z, true, false, false, true);
            SetEntityHeading(veh, rot);
            
            await Delay(1200);

            Freeze(PlayerId(), false);
            DoScreenFadeIn(500);
            while (IsScreenFadingIn())
                await Delay(1);
        }

        public static async void Teleport(Vector3 pos, int delay = 1)
        {
            RequestCollisionAtCoord(pos.X, pos.Y, pos.Z);
            DoScreenFadeOut(500);
            while (IsScreenFadingOut())
                await Delay(1);
            
            NetworkFadeOutEntity(GetPlayerPed(-1), true, true);
            Freeze(PlayerId(), true);
            
            SetEntityCoords(GetPlayerPed(-1), pos.X, pos.Y, pos.Z, true, false, false, true);
            //NetworkResurrectLocalPlayer(pos.X, pos.Y, pos.Z, 0, true, false);

            await Delay(500 + delay);

            Freeze(PlayerId(), false);
            
            await Delay(500);
            NetworkFadeInEntity(GetPlayerPed(-1), false);
            
            DoScreenFadeIn(500);
            while (IsScreenFadingIn())
                await Delay(1);
        }

        public static async void HealthCheck()
        {
            if (GetEntityHealth(GetPlayerPed(-1)) < 130)
            {
                User.SetPlayerNonStaticClipset("move_heist_lester");
            }
        }

        public static async void Respawn(Vector3 pos, float rot)
        {
            if (GetEntityHealth(GetPlayerPed(-1)) > 110)
                return;
            
            DoScreenFadeOut(500);

            while (IsScreenFadingOut())
                await Delay(1);

            NetworkFadeOutEntity(GetPlayerPed(-1), true, true);
            Freeze(PlayerId(), true);

            if (Data.jail_time > 0)
                Jail.JailPlayer(Data.jail_time);
            else
            {
                SetEntityCoords(GetPlayerPed(-1), pos.X, pos.Y, pos.Z, true, false, false, true);
                NetworkResurrectLocalPlayer(pos.X, pos.Y, pos.Z, rot, true, false);
            }
                        
            SetDrugAmfLevel(0);
            SetDrugCocaLevel(0);
            SetDrugDmtLevel(0);
            SetDrugKetLevel(0);
            SetDrugLsdLevel(0);
            SetDrugMargLevel(0);
            SetDrugMefLevel(0);
            SetDrunkLevel(0);
            
            Sync.Data.Set(GetServerId(), "deathReason", -1);
            
            NetworkSetTalkerProximity(5f);
            PlayScenario("forcestop");
            ClearPedBloodDamage(GetPlayerPed(-1));
            StopAllScreenEffects();
            RemoveAllPedWeapons(GetPlayerPed(-1), false);
            RemoveWeapons();
            IsBlockAnimation = false;
            Sync.Data.ResetLocally(GetServerId(), "GrabCash");
            UnTie();
            UnTieBandage();
            User.TimerAbduction = -1;
			SetVirtualWorld(0);
            
            if (User.IsEms())
            {
                
                    if (User.IsDuty())
                    {
                        Fractions.Ems.Garderob(0);
                        Notification.SendWithTime("~b~Вы ушли с дежурства");
                        Client.Sync.Data.ResetLocally(User.GetServerId(), "duty");
                    }
            }
            if (User.IsFib())
            {

                if (User.IsDuty())
                    {
                        Notification.SendWithTime("~b~Вы ушли с дежурства");
                        Client.Sync.Data.ResetLocally(User.GetServerId(), "duty");
                    }
            }
            
            if (User.IsSapd())
            {
                if (User.IsDuty())
                    {
                        Fractions.Sapd.Garderob(0);
                        Notification.SendWithTime("~b~Вы ушли с дежурства");
                        Client.Sync.Data.ResetLocally(User.GetServerId(), "duty");
                    }
            }
            


            if (Data.med_lic)
            {
                RemoveCashMoney(10);
                Coffer.AddMoney(10);
                //Notification.SendWithTime("~g~Стоимость лечения со страховкой $10");
                Notification.SendPicture("Стоимость лечения со страховкой ~g~$10", "Mors Mutual Insurance", "Оповещение", "CHAR_MP_MORS_MUTUAL", Notification.TypeChatbox);
                SetEntityHealth(GetPlayerPed(-1), 120);
            }
            else
            {
                RemoveCashMoney(150);
                Coffer.AddMoney(150);
                //Notification.SendWithTime("~g~Стоимость лечения $150");
                Notification.SendPicture("Стоимость лечения ~g~$150", "Mors Mutual Insurance", "Оповещение", "CHAR_MP_MORS_MUTUAL", Notification.TypeChatbox);
                SetEntityHealth(GetPlayerPed(-1), 120);
            }
            
            Sync.Data.Reset(GetServerId(), "isCuff");
            Sync.Data.ResetLocally(GetServerId(), "isCuff");

            await Delay(500);

            Freeze(PlayerId(), false);
            
            if (Data.jail_time > 0)
                Jail.JailPlayer(Data.jail_time);
            
            await Delay(500);
            NetworkFadeInEntity(GetPlayerPed(-1), false);
            
            DoScreenFadeIn(500);
            
            if (Data.age == 18)
                Notification.Send("~g~[HELP] Чтобы открыть меню, нажмите ~y~M");
            
            while (IsScreenFadingIn())
                await Delay(1);
        }

        public static void Revive()
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            NetworkResurrectLocalPlayer(playerPos.X, playerPos.Y, playerPos.Z, GetEntityHeading(GetPlayerPed(-1)), true, true);
            Freeze(PlayerId(), false);
        }

        public static void PedRotation(float rotation)
        {
            SetEntityHeading(GetPlayerPed(-1), rotation);
        }

        public static async void SetSkin(string skin)
        {
            uint spawnModel = (uint) GetHashKey(skin);
            if (!await Main.LoadModel(spawnModel))
                return;
            SetPlayerModel(PlayerId(), spawnModel);
            SetModelAsNoLongerNeeded(spawnModel);
        }
        
        public static string GetVipStatus()
        {
            return Data.vip_status == "" ? "none" : Data.vip_status;
        }

        public static void UpdateValues()
        {
            Characher.UpdateCloth();
            Characher.UpdateFace();
        }

        public static void SetWaypoint(float x, float y)
        {
            World.RemoveWaypoint();
            World.WaypointPosition = new Vector3(x, y, 0);
            Notification.SendSubtitle("~g~Метка в GPS была установлена");
        }
        
        public static void BlockAnimation(int time = 10000)
        {
            IsBlockAnimation = true;
            Task.Delay(time).ContinueWith(_ => { IsBlockAnimation = false; } ).Start();
        }

        public static void PlayAnimationToAll(int id, string name, string name2, int flag = 49)
        {
            if (id != Data.id) return;
            PlayAnimation(name, name2, flag);
        }

        public static void PlayAnimationToPlayer(string name, string name2, int flag = 49)
        {
            PlayAnimation(name, name2, flag);
        }

        public static async void PlayArrestAnimation()
        {
            RequestAnimDict("random@arrests");
            while (!HasAnimDictLoaded("random@arrests"))
                await Delay(1);
            
            RequestAnimDict("random@arrests@busted");
            while (!HasAnimDictLoaded("random@arrests@busted"))
                await Delay(1);

            var player = GetPlayerPed(-1);

            if (IsEntityPlayingAnim(player, "random@arrests@busted", "idle_a", 3))
            {
                TaskPlayAnim(player, "random@arrests@busted", "exit", 8.0f, 1.0f, -1, 2, 0, false, false, false);
                await Delay(3000);
                TaskPlayAnim(player, "random@arrests", "kneeling_arrest_get_up", 8.0f, 1.0f, -1, 128, 0, false, false, false);
            }
            else
            {
                TaskPlayAnim(player, "random@arrests", "idle_2_hands_up", 8.0f, 1.0f, -1, 2, 0, false, false, false);
                await Delay(4000);
                TaskPlayAnim(player, "random@arrests", "kneeling_arrest_idle", 8.0f, 1.0f, -1, 2, 0, false, false, false);
                await Delay(500);
                TaskPlayAnim(player, "random@arrests@busted", "enter", 8.0f, 1.0f, -1, 2, 0, false, false, false);
                await Delay(1000);
                TaskPlayAnim(player, "random@arrests@busted", "idle_a", 8.0f, 1.0f, -1, 9, 0, false, false, false);
            }
        }

        public static void PlayEatAnimation()
        {
            PlayAnimation("mp_player_inteat@burger", "mp_player_int_eat_burger", 48);
            /*if (IsBlockAnimation) return;
            
            User.PlayAnimation("mp_player_inteat@burger", "mp_player_int_eat_burger_enter", 48);
            
            await Delay(50);
            
            while (IsEntityPlayingAnim(GetPlayerPed(-1), "mp_player_inteat@burger", "mp_player_int_eat_burger_enter", 3))
                await Delay(1);
                
            User.PlayAnimation("mp_player_inteat@burger", "mp_player_int_eat_burger", 48);
            await Delay(50);
            while (IsEntityPlayingAnim(GetPlayerPed(-1), "mp_player_inteat@burger", "mp_player_int_eat_burger", 3))
                await Delay(1);
                
            User.PlayAnimation("mp_player_inteat@burger", "mp_player_int_eat_burger_fp", 48);
            await Delay(50);
            while (IsEntityPlayingAnim(GetPlayerPed(-1), "mp_player_inteat@burger", "mp_player_int_eat_burger_fp", 3))
                await Delay(1);
                
            User.PlayAnimation("mp_player_inteat@burger", "mp_player_int_eat_exit_burger", 48);*/
        }

        public static void PlayDrinkAnimation()
        {
            PlayAnimation("mp_player_intdrink", "loop_bottle", 48);
            /*if (IsBlockAnimation) return;
            User.PlayAnimation("mp_player_intdrink", "intro_bottle", 48);
            await Delay(50);
            while (IsEntityPlayingAnim(GetPlayerPed(-1), "mp_player_intdrink", "intro_bottle", 3))
                await Delay(1);
                
            User.PlayAnimation("mp_player_intdrink", "loop_bottle", 48);
            await Delay(50);
            while (IsEntityPlayingAnim(GetPlayerPed(-1), "mp_player_intdrink", "loop_bottle", 3))
                await Delay(1);
                
            User.PlayAnimation("mp_player_intdrink", "outro_bottle", 48);*/
        }

        public static void AddWatchDogs(int skill = 10, int random = 4)
        {
            var rand = new Random();
            if (Data.mp0_watchdogs >= skill || rand.Next(random) != 0) return;
            Data.mp0_watchdogs++;
            Sync.Data.Set(GetServerId(), "mp0_watchdogs", Data.mp0_watchdogs);
        }

        public static void PlayDrugAnimation()
        {
            PlayAnimation("move_m@drunk@transitions", "slightly_to_idle", 8);
        }

        public static void PlayPhoneAnimation()
        {
            if (!IsPedUsingScenario(GetPlayerPed(-1), "WORLD_HUMAN_STAND_MOBILE"))
                PlayScenario("WORLD_HUMAN_STAND_MOBILE");
        }

        public static async void PlayAnimation(string name, string name2, int flag = 49)
        {
            if (IsBlockAnimation) return;
            /*
                8 = нормально играть
                9 = цикл
                48 = нормально играть только верхнюю часть тела
                49 = только верхняя часть тела
            */
            
            RequestAnimDict(name);
            while (!HasAnimDictLoaded(name))
                await Delay(1);
        
            if (IsPedInAnyVehicle(PlayerPedId(), true))
                return;
            if (User.IsDead())
                return;
            
            TaskPlayAnim(GetPlayerPed(-1), name, name2, 8f, -8, -1, flag, 0, false, false, false);
        }

        public static async void PlayPedAnimation(CitizenFX.Core.Ped ped, string name, string name2, int flag = 49)
        {
            /*
                8 = нормально играть
                9 = цикл
                48 = нормально играть только верхнюю часть тела
                49 = только верхняя часть тела
            */
            
            RequestAnimDict(name);
            while (!HasAnimDictLoaded(name))
                await Delay(1);

            if (ped.IsInVehicle())
                return;
            if (ped.IsDead)
                return;
            
            TaskPlayAnim(ped.Handle, name, name2, 8f, -8, -1, flag, 0, false, false, false);
        }

        public static async void SetPlayerCurrentClipset()
        {   
            if (Data.s_clipset == "")
                ResetPedMovementClipset(GetPlayerPed(-1), 0);
            else
            {
                int attpempt = 10;
                RequestClipSet(Data.s_clipset);
                if (!HasClipSetLoaded(Data.s_clipset) && attpempt > 0)
                {
                    attpempt--;
                    await Delay(100);
                }
                
                if (HasClipSetLoaded(Data.s_clipset))
                    SetPedMovementClipset(GetPlayerPed(-1), Data.s_clipset, 0);
            }
        }

        public static async void SetPlayerNonStaticClipset(string clipset)
        {   
            if (clipset == "")
                ResetPedMovementClipset(GetPlayerPed(-1), 0);
            else
            {
                int attpempt = 10;
                RequestClipSet(clipset);
                if (!HasClipSetLoaded(clipset) && attpempt > 0)
                {
                    attpempt--;
                    await Delay(100);
                }
                
                if (HasClipSetLoaded(clipset))
                    SetPedMovementClipset(GetPlayerPed(-1), clipset, 0);
            }
        }

        public static void SetPlayerNewClipset(string clipset)
        {
            Data.s_clipset = clipset;
            Sync.Data.Set(GetServerId(), "s_clipset", Data.s_clipset);
            SetPlayerCurrentClipset();
        }

        public static void StopAnimation()
        {
            ClearPedSecondaryTask(GetPlayerPed(-1));
        }

        public static async void Cuff()
        {
            if (await Sync.Data.Has(GetServerId(), "isCuff"))
            {
                SetEnableHandcuffs(GetPlayerPed(-1), false);
                Sync.Data.Reset(GetServerId(), "isCuff");
                Sync.Data.ResetLocally(GetServerId(), "isCuff");
                StopAnimation();
                IsBlockAnimation = false;
                //Freeze(PlayerId(), false);
            }
            else
            {
                PlayAnimation("mp_arresting", "idle");
                SetEnableHandcuffs(GetPlayerPed(-1), true);
                Sync.Data.Set(GetServerId(), "isCuff", true);
                Sync.Data.SetLocally(GetServerId(), "isCuff", true);
                Sync.Data.ResetLocally(GetServerId(), "GrabCash");
                IsBlockAnimation = true;
                //Freeze(PlayerId(), true);
            }
        }

        public static async void Knockout()
        {
            //PlayAnimation("amb@world_human_bum_slumped@male@laying_on_right_side@base", "base", 9);
            //SetEnableHandcuffs(GetPlayerPed(-1), true);
            Sync.Data.Set(GetServerId(), "isKnockout", true);
            Sync.Data.SetLocally(GetServerId(), "isKnockout", true);
            //IsBlockAnimation = true;
            //Freeze(PlayerId(), true);

            SetPedToRagdoll(GetPlayerPed(-1), 30000, 30000, 0, false, false, false); 
            SetEntityHealth(GetPlayerPed(-1), GetEntityHealth(GetPlayerPed(-1)) - 20);
            
            Notification.SendWithTime("~r~Вы в нокауте");

            await Delay(2000);
            UI.ShowLoadDisplay();
            await Delay(30000);
            UI.HideLoadDisplay();
            await Delay(500);
            
            //IsBlockAnimation = false;
            Sync.Data.Reset(GetServerId(), "isKnockout");
            Sync.Data.ResetLocally(GetServerId(), "isKnockout");
            //StopAnimation();

            ResetPedRagdollTimer(GetPlayerPed(-1));
            
            if (!await Sync.Data.Has(GetServerId(), "isTie"))
            {
                Freeze(PlayerId(), false);
                //SetEnableHandcuffs(GetPlayerPed(-1), false);
            }
        }

        public static async void SellPlayer()
        {
            TieBandage();
            UnTie();

            await Delay(1000);
            var random = new Random();
            var r = random.Next(45);
            var pos = new Vector3((float) Jobs.HumanLab.GroundCheckPos[r, 0], (float) Jobs.HumanLab.GroundCheckPos[r, 1], (float) Jobs.HumanLab.GroundCheckPos[r, 2] - 1);
            SetEntityCoords(GetPlayerPed(-1), pos.X, pos.Y, pos.Z, true, false, false, true);
            
            SetPedComponentVariation(GetPlayerPed(-1), 3, Data.torso, Data.torso_color, 2);

            if (Skin.SEX == 0)
            {
                Sync.Data.Set(GetServerId(), "torso", 15);
                Sync.Data.Set(GetServerId(), "torso_color", 0);
                Sync.Data.Set(GetServerId(), "parachute", 0);
                Sync.Data.Set(GetServerId(), "parachute_color", 240);
                Sync.Data.Set(GetServerId(), "leg", 61);
                Sync.Data.Set(GetServerId(), "leg_color", 13);
                Sync.Data.Set(GetServerId(), "foot", 34);
                Sync.Data.Set(GetServerId(), "foot_color", 0);
                Sync.Data.Set(GetServerId(), "body", 0);
                Sync.Data.Set(GetServerId(), "body_color", 240);
            }
            else
            {
                Sync.Data.Set(GetServerId(), "torso", 15);
                Sync.Data.Set(GetServerId(), "torso_color", 0);
                Sync.Data.Set(GetServerId(), "parachute", 0);
                Sync.Data.Set(GetServerId(), "parachute_color", 240);
                Sync.Data.Set(GetServerId(), "leg", 15);
                Sync.Data.Set(GetServerId(), "leg_color", 0);
                Sync.Data.Set(GetServerId(), "foot", 35);
                Sync.Data.Set(GetServerId(), "foot_color", 0);
                Sync.Data.Set(GetServerId(), "body", 15);
                Sync.Data.Set(GetServerId(), "body_color", 0);   
            }
            
            /*while (moneyFull > 0)
            {
                if (moneyFull == 1)
                {
                    Managers.Inventory.AddItemServer(138, 1, InventoryTypes.StockGang, 8, moneyFull, -1, -1, -1);
                    moneyFull = 0;
                }
                else if (moneyFull == 100)
                {
                    Managers.Inventory.AddItemServer(139, 1, InventoryTypes.StockGang, 8, moneyFull, -1, -1, -1);
                    moneyFull = 0;
                }
                else if (moneyFull <= 4000)
                {
                    Managers.Inventory.AddItemServer(140, 1, InventoryTypes.StockGang, 8, moneyFull, -1, -1, -1);
                    moneyFull = 0;
                }
                else if (moneyFull <= 8000)
                {
                    Managers.Inventory.AddItemServer(141, 1, InventoryTypes.StockGang, 8, moneyFull, -1, -1, -1);
                    moneyFull = 0;
                }
                else
                {
                    Managers.Inventory.AddItemServer(141, 1, InventoryTypes.StockGang, 8, 8000, -1, -1, -1);
                    moneyFull = moneyFull - 8000;
                }
            }*/
            
            Characher.UpdateCloth();
            await Delay(10000);
            
            Main.SaveLog("Cartel", $"[SELL_BITCH] {Data.rp_name} - {Data.money}");
            
            var rand = new Random();
            
            SetCashMoney(0);
            SetPedToRagdoll(GetPlayerPed(-1), 20000, 20000, 0, false, false, false); 
            SetEntityHealth(GetPlayerPed(-1), 100 + rand.Next(60));
            
            await Delay(10000);
            
            UnTieBandage();
        }

        public static void EjectCar()
        {
            TaskLeaveVehicle(GetPlayerPed(-1), GetVehiclePedIsUsing(PlayerPedId()), 1); //16
        }

        public static void UseAdrenalin()
        {
            /*var ped = new CitizenFX.Core.Ped(GetPlayerPed(-1));
            if (ped.IsAlive)
                return;*/
            if (GetEntityHealth(GetPlayerPed(-1)) < 101)

            {
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
                PlayScenario("forcestop");
                NetworkSetTalkerProximity(5f);
                SetEntityCoords(GetPlayerPed(-1), pos.X, pos.Y, pos.Z, true, false, false, true);
                NetworkResurrectLocalPlayer(pos.X, pos.Y, pos.Z, 0, true, false);
                StopAllScreenEffects();
                SetEntityHealth(GetPlayerPed(-1), 105);
                Freeze(PlayerId(), false);
            
                Dispatcher.SendEms("Код 4", "Человек в сознании");
            }
            else
            {
                Notification.SendWithTime("Человек в сознании");
            }
        }
        
        public static void UseDef()
        {
            if (GetEntityHealth(GetPlayerPed(-1)) < 101)
            {
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
                PlayScenario("forcestop");
                NetworkSetTalkerProximity(5f);
                SetEntityCoords(GetPlayerPed(-1), pos.X, pos.Y, pos.Z, true, false, false, true);
                NetworkResurrectLocalPlayer(pos.X, pos.Y, pos.Z, 0, true, false);
                StopAllScreenEffects();
                SetEntityHealth(GetPlayerPed(-1), 105);
                Freeze(PlayerId(), false);
                
                Dispatcher.SendEms("Код 4", "Человек в сознании");
            }
            else
            {
                Notification.SendWithTime("Человек в сознании");
            }
    
        }
        public static void EmsHeal()
        {
            var pos = GetEntityCoords(GetPlayerPed(-1), true);
            //PlayScenario("forcestop");
            NetworkSetTalkerProximity(5f);
            SetEntityCoords(GetPlayerPed(-1), pos.X, pos.Y, pos.Z, true, false, false, true);
            NetworkResurrectLocalPlayer(pos.X, pos.Y, pos.Z, 0, true, false);
            StopAllScreenEffects();
            SetEntityHealth(GetPlayerPed(-1), 200);
            Freeze(PlayerId(), false);
        }
        
        public static void UseFirstAidKit()
        {
            /*var ped = new CitizenFX.Core.Ped(GetPlayerPed(-1));
            if (ped.IsAlive)
                return;*/
            
            //var pos = GetEntityCoords(GetPlayerPed(-1), true);
            //PlayScenario("forcestop");
            NetworkSetTalkerProximity(5f);
            //SetEntityCoords(GetPlayerPed(-1), pos.X, pos.Y, pos.Z, true, false, false, true);
            //NetworkResurrectLocalPlayer(pos.X, pos.Y, pos.Z, 0, true, false);
            StopAllScreenEffects();
            if (GetEntityHealth(GetPlayerPed(-1)) < 120 && GetEntityHealth(GetPlayerPed(-1)) > 110)
            {
                SetEntityHealth(GetPlayerPed(-1), GetEntityHealth(GetPlayerPed(-1)) + 20);
            }
            else if (GetEntityHealth(GetPlayerPed(-1)) < 110 && GetEntityHealth(GetPlayerPed(-1)) > 100)
            {
                SetEntityHealth(GetPlayerPed(-1), GetEntityHealth(GetPlayerPed(-1)) + 30);
            }
        }

        public static void UseTie()
        {
            if (Sync.Data.HasLocally(GetServerId(), "isTie"))
                UnTie();
            else
                Tie();
        }

        public static void Tie()
        {
            PlayAnimation("mp_arresting", "idle");
            SetEnableHandcuffs(GetPlayerPed(-1), true);
            Sync.Data.Set(GetServerId(), "isTie", true);
            Sync.Data.SetLocally(GetServerId(), "isTie", true);
            IsBlockAnimation = true;
            //Freeze(PlayerId(), true);
            
            Notification.SendWithTime("~r~Вас связали");
        }

        public static void UnTie()
        {
            SetEnableHandcuffs(GetPlayerPed(-1), false);
            Sync.Data.Reset(GetServerId(), "isTie");
            Sync.Data.ResetLocally(GetServerId(), "isTie");
            IsBlockAnimation = false;
            Freeze(PlayerId(), false);
            StopAnimation();
        }

        public static void UseTieBandage()
        {
            if (Sync.Data.HasLocally(GetServerId(), "isTieBandage"))
                UnTieBandage();
            else
                TieBandage();
        }

        public static async void TieBandage()
        {
            Sync.Data.Set(GetServerId(), "isTieBandage", true);
            Sync.Data.SetLocally(GetServerId(), "isTieBandage", true);
            Notification.SendWithTime("~r~На Вас надели повязку");
            //await Delay(2000);
            //UI.ShowLoadDisplay();
        }
        
        public static void UnTieBandage()
        {
            //UI.HideLoadDisplay();
            Sync.Data.Reset(GetServerId(), "isTieBandage");
            Sync.Data.ResetLocally(GetServerId(), "isTieBandage");
        }
        
        public static void Incar()
        {
            var veh = Main.FindNearestVehicle();
            if (veh == null)
                return;
            new CitizenFX.Core.Ped(PlayerPedId()).SetIntoVehicle(veh, VehicleSeat.Any);
        }

        public static async void AdminInCar()
        {
            var veh = Main.FindNearestVehicle();
            if (veh == null)
                return;
            new CitizenFX.Core.Ped(PlayerPedId()).SetIntoVehicle(veh, VehicleSeat.Any);
            
            foreach (CitizenFX.Core.Ped p in Main.GetPedListOnRadius(Convert.ToInt32(1)))
                p.Delete();
            await Delay(300);
            foreach (CitizenFX.Core.Vehicle v in Main.GetVehicleListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), Convert.ToInt32(1)))
                v.Delete();
        }
        
        public static async void UnTieKnife()
        {
            var pPos = GetEntityCoords(GetPlayerPed(-1), true);
            var player = Main.GetPlayerOnRadius(pPos, 1.5f);
            if (player == null)
            {
                Notification.SendWithTime("~r~Рядом с вами никого нет");
                return;
            }

            if (await Client.Sync.Data.Has(player.ServerId, "isTie"))
            {
                Shared.TriggerEventToPlayer(player.ServerId, "ARP:UnTie");
                Notification.SendWithTime("~y~Вы разрезали веревки");
                Chat.SendMeCommand("освободил человека рядом");
                return;
            }
            Notification.SendWithTime("~y~Игрок не связан");
        }
        
        
        public static void UnDuty()
        {
            //UI.HideLoadDisplay();
            Sync.Data.Reset(GetServerId(), "duty");
            Sync.Data.ResetLocally(GetServerId(), "duty");
        }
        
        public static void PlayScenario(string scenarioName)
        {
            if (IsBlockAnimation) return;
            
            if (_currentScenario == "" || _currentScenario != scenarioName)
            {
                _currentScenario = scenarioName;
                ClearPedTasks(PlayerPedId());

                if (IsPedInAnyVehicle(PlayerPedId(), true))
                    return;
                if (IsPedRunning(PlayerPedId()))
                {
                    Notification.SendWithTime("Вы не можете включить анимацию пока вы бежите.", true, false);
                    return;
                }
                if (User.IsDead())
                {
                    Notification.SendWithTime("Вы в коме.", true, false);
                    return;
                }
                if (IsPlayerInCutscene(PlayerPedId()))
                {
                    Notification.SendWithTime("Вы в катсцене.", true, false);
                    return;
                }
                if (IsPedFalling(PlayerPedId()))
                {
                    Notification.SendWithTime("Вы падаете.", true, false);
                    return;
                }
                if (IsPedRagdoll(PlayerPedId()))
                    return;
                if (!IsPedOnFoot(PlayerPedId()))
                {
                    Notification.SendWithTime("Вы должны идти пешком.", true, false);
                    return;
                }
                if (NetworkIsInSpectatorMode())
                {
                    Notification.SendWithTime("Нужно выйти из спектатора.", true, false);
                    return;
                }
                if (GetEntitySpeed(PlayerPedId()) > 5.0f)
                {
                    Notification.SendWithTime("Вы двигаетесь слишком быстро.", true, false);
                    return;
                }

                if (PedScenarios.PositionBasedScenarios.Contains(scenarioName))
                {
                    var pos = GetOffsetFromEntityInWorldCoords(PlayerPedId(), 0f, -0.5f, -0.5f);
                    var heading = GetEntityHeading(PlayerPedId());
                    TaskStartScenarioAtPosition(PlayerPedId(), scenarioName, pos.X, pos.Y, pos.Z, heading, -1, true, false);
                }
                else
                {
                    TaskStartScenarioInPlace(PlayerPedId(), scenarioName, 0, true);
                }
            }
            else
            {
                _currentScenario = "";
                ClearPedTasks(PlayerPedId());
                ClearPedSecondaryTask(PlayerPedId());
            }

            if (scenarioName != "forcestop") return;
            _currentScenario = "";
            ClearPedTasks(PlayerPedId());
            //ClearPedTasksImmediately(PlayerPedId());
        }

        public static void StopScenario()
        {
            _currentScenario = "";
            ClearPedTasks(PlayerPedId());
        }

        public static string GetDeathReason(uint reason)
        {
            switch (reason)
            {
                case (uint) WeaponHash.Knife:
                case (uint) WeaponHash.Dagger:
                case (uint) WeaponHash.Machete:
                case (uint) WeaponHash.SwitchBlade:
                case (uint) WeaponHash.Bottle:
                    return "Человек без сознания - ножевое ранение";
                case (uint) WeaponHash.Unarmed:
                    return "Драка";
                case (uint) WeaponHash.Hatchet:
                case (uint) WeaponHash.BattleAxe:
                    return "Человек без сознания - удар топором";
                case (uint) WeaponHash.Hammer:
                case (uint) WeaponHash.Nightstick:
                case (uint) WeaponHash.KnuckleDuster:
                case (uint) WeaponHash.Bat:
                case (uint) WeaponHash.Crowbar:
                case (uint) WeaponHash.GolfClub:
                case (uint) WeaponHash.Flashlight:
                case (uint) WeaponHash.PoolCue:
                case (uint) WeaponHash.Wrench:
                case (uint) WeaponHash.Snowball:
                case (uint) WeaponHash.Ball:
                    return "Человек без сознания - удар тупым предметом";
                case 133987706:
                    return "Человек выпал из транспорта";
                case 2461879995:
                    return "Человека сбил транспорт";
                case 3452007600:
                    return "Человек упал с высоты";
                case 4194021054:
                    return "Нападание животного";
                case 2339582971:
                    return "Кровотечение";
                case 148160082:
                    return "Нападение Пумы";
                case 1223143800:
                    return "Раны от колючей проволки";
                case 4284007675:
                case 1936677264:
                    return "Человек тонет";
                case 539292904:
                case (uint) WeaponHash.Grenade:
                case (uint) WeaponHash.StickyBomb:
                case (uint) WeaponHash.ProximityMine:
                case (uint) WeaponHash.PipeBomb:
                case (uint) WeaponHash.RPG:
                case (uint) WeaponHash.GrenadeLauncher:
                case (uint) WeaponHash.Railgun:
                case (uint) WeaponHash.GrenadeLauncherSmoke:
                    return "Взрыв";
                case 910830060:
                    return "Человек истощенен";
                case 3750660587:
                case (uint) WeaponHash.Molotov:
                case (uint) WeaponHash.PetrolCan:
                    return "Возгорание";
                case (uint) WeaponHash.BZGas:
                case (uint) WeaponHash.FireExtinguisher:
                case (uint) WeaponHash.Flare:
                case (uint) WeaponHash.SmokeGrenade:
                    return "Человек без сознания - недостаток кислорода";
                case 341774354:
                    return "Крушение вертолёта";
            }
            
            return Enum.GetValues(typeof(WeaponHash)).Cast<uint>().Any(hash => reason == hash) ? "Огнестрельное ранение" : "Человек без сознания";
        }

        public static void PayDay()
        {
            if (!IsLogin()) return;

            int fullMoney = 0;
            int plId = GetServerId();
            
            /*if (Main.ServerName != "Andromeda")
                Data.exp_age = Data.exp_age + Bonus;
            else*/
            Data.exp_age++;
            
            Sync.Data.Set(plId, "exp_age", Data.exp_age);

            if (GetVipStatus() != "none" && Data.exp_age % 4 == 0)
                Sync.Data.Set(plId, "exp_age", ++Data.exp_age);

            if (Data.reg_time == 0 && Data.reg_status == 1)
                Sync.Data.Set(plId, "reg_status", 0);

            if (Data.reg_time == 0 && Data.reg_status == 2)
            {
                Sync.Data.Set(plId, "reg_status", 3);
                Notification.SendPicture("Поздравляю, Вы получили гражданство США.", "Адвокат", "323-555-0001", "CHAR_BARRY", Notification.TypeChatbox);
            }
            
            if (Data.reg_time > 0)
                Sync.Data.Set(plId, "reg_time", --Data.reg_time);

            if (Data.exp_age >= 372)
            {
                Data.exp_age = Data.exp_age - 372;
                Sync.Data.Set(plId, "age", ++Data.age);
                Sync.Data.Set(plId, "exp_age", Data.exp_age);
                
                Notification.SendPicture("В честь вашего праздника, мы дарим Вам ~g~$50", "Facebook", "С днём рождения!", "CHAR_FACEBOOK", Notification.TypeChatbox);

                if (Data.age == 21 && !IsStringNullOrEmpty(Data.referer))
                {
                    Notification.SendWithTime($"~g~Пригласивший {Data.referer} получил 100ac на личный счёт");
                    TriggerServerEvent("ARP:SendReferrer");
                }

                fullMoney = fullMoney + 50;
            }

            if (Data.bank_prefix != 0)
            {
                int money;
                switch (Data.fraction_id)
                {
                    case 1:
                        money = 71 + Data.rank * 7;
                        AddPayDayMoney(money);
                        //fullMoney = fullMoney + money;
                        break;
                    case 2:
                        money = 43 + Data.rank * 7;
                        AddPayDayMoney(money);
                        //fullMoney = fullMoney + money;
                        break;
                    case 3:
                        money = 64 + Data.rank * 7;
                        AddPayDayMoney(money);
                        //fullMoney = fullMoney + money;
                        break;
                    case 4:
                        money = 36 + Data.rank * 7;
                        AddPayDayMoney(money);
                        //fullMoney = fullMoney + money;
                        break;
                    case 5:
                        money = 29 + Data.rank * 7;
                        AddPayDayMoney(money);
                        //fullMoney = fullMoney + money;
                        break;
                    case 6:
                        money = 29 + Data.rank * 7;
                        AddPayDayMoney(money);
                        //fullMoney = fullMoney + money;
                        break;
                    case 7:
                        money = 36 + Data.rank * 7;
                        AddPayDayMoney(money);
                        //fullMoney = fullMoney + money;
                        break;
                    case 16:
                        money = 50 + Data.rank * 7;
                        AddPayDayMoney(money);
                        //fullMoney = fullMoney + money;
                        break;
                }
            }

            if (Data.is_old_money)
            {
                AddPayDayMoney(Coffer.GetMoneyOld());
                //fullMoney = fullMoney + Coffer.GetMoneyOld();
            }
            
            if (GetPayDayMoney() > 0)
            {
                if (GetPayDayMoney() > 280)
                    Main.SaveLog("Payday", Data.rp_name + " add money " + GetPayDayMoney());
                
                int nalog = GetPayDayMoney() * Bonus * (100 - Coffer.GetNalog()) / 100;

                switch (Data.bank_prefix)
                {
                    case 1111:
                        //RemoveBankMoney(2);
                        //Coffer.AddMoney(2);
                        //Notification.SendPicture($"~y~Обслуживание банка:~s~ -$2\n~b~Сумма: ~w~${nalog}", "~r~Maze~s~ Bank", "Зачисление средств", "CHAR_BANK_MAZE", Notification.TypeChatbox);
                        Notification.SendPicture($"~b~Сумма: ~w~${nalog}", "~r~Maze~s~ Bank", "Зачисление средств", "CHAR_BANK_MAZE", Notification.TypeChatbox);
                        break;
                    case 2222:
                        //int bankPrice1 = await Business.Business.GetPrice(1);
                        //RemoveBankMoney(bankPrice1);
                        //Business.Business.AddMoney(1, bankPrice1);
                        //Notification.SendPicture($"~y~Обслуживание банка:~s~ -${bankPrice1}\n~b~Сумма: ~w~${nalog}", "~g~Flecca~s~ Bank", "Зачисление средств", "CHAR_BANK_FLEECA", Notification.TypeChatbox);
                        Notification.SendPicture($"~b~Сумма: ~w~${nalog}", "~g~Fleeca~s~ Bank", "Зачисление средств", "CHAR_BANK_FLEECA", Notification.TypeChatbox);
                        break;
                    case 3333:
                        //int bankPrice2 = await Business.Business.GetPrice(2);
                        //RemoveBankMoney(bankPrice2);
                        //Business.Business.AddMoney(1, bankPrice2);
                        //Notification.SendPicture($"~y~Обслуживание банка:~s~ -${bankPrice2}\n~b~Сумма: ~w~${nalog}", "~b~Blaine~s~ Bank", "Зачисление средств", "CHAR_STEVE_TREV_CONF", Notification.TypeChatbox);
                        Notification.SendPicture($"~b~Сумма: ~w~${nalog}", "~b~Blaine~s~ Bank", "Зачисление средств", "CHAR_STEVE_TREV_CONF", Notification.TypeChatbox);
                        break;
                    case 4444:
                        //int bankPrice2 = await Business.Business.GetPrice(2);
                        //RemoveBankMoney(bankPrice2);
                        //Business.Business.AddMoney(1, bankPrice2);
                        //Notification.SendPicture($"~y~Обслуживание банка:~s~ -${bankPrice2}\n~b~Сумма: ~w~${nalog}", "~b~Blaine~s~ Bank", "Зачисление средств", "CHAR_STEVE_TREV_CONF", Notification.TypeChatbox);
                        Notification.SendPicture($"~b~Сумма: ~w~${nalog}", "~o~Pacific~s~ Bank", "Зачисление средств", "CHAR_STEVE_MIKE_CONF", Notification.TypeChatbox);
                        break;
                }
                
                SetPayDayMoney(0);
                
                fullMoney = fullMoney + nalog;
                
                Notification.Send($"~y~Налог: ~w~{Coffer.GetNalog()}%");
            }
            
            if (Data.posob)
            {
                fullMoney = fullMoney + Coffer.GetPosob();
                Notification.Send("~b~Пособие: ~w~$" + Coffer.GetPosob());
            }
            
            if (Data.is_old_money)
                Notification.Send("~b~Пенсия: ~w~$" + Coffer.GetMoneyOld());
            
            int moneyH = 0;
            
            if (Data.admin_level > 0)
                moneyH = moneyH + Data.admin_level * 20;
            
            if (Data.helper_level > 0)
                moneyH = moneyH + Data.helper_level * 15;
            
            switch (GetVipStatus())
            {
                case "Hard":
                {
                    moneyH = moneyH + 28;
                    break;
                }
                case "Light":
                {
                    moneyH = moneyH + 14;
                    break;
                }
                case "YouTube":
                {
                    moneyH = moneyH + 20;
                    break;
                }
            }

            if (moneyH > 0)
            {
                fullMoney = fullMoney + moneyH;
                //AddBankMoney(moneyH);
                Notification.Send("~b~Бонус к зарплате: ~w~$" + moneyH);
            }
            
            if (Data.bank_prefix == 0)
            {
                Notification.Send("~y~Оформите банковскую карту");
                AddCashMoney(fullMoney);
            }
            else
                AddBankMoney(fullMoney);
            
            Coffer.RemoveMoney(fullMoney);
        }
        
        public static async void TakeAllGuns(int id)
        {
            for (int n = 54; n < 138; n++)
            {
                foreach(uint hash in Enum.GetValues(typeof(WeaponHash)))
                {
                    string name = Enum.GetName(typeof(WeaponHash), hash);
                    if (!String.Equals(name, Client.Inventory.GetItemNameHashById(n), StringComparison.CurrentCultureIgnoreCase)) continue;
                    if (!HasPedGotWeapon(GetPlayerPed(-1), hash, false)) continue;
                    
                    var n1 = n;
                    int ammoItem = Managers.Inventory.AmmoTypeToAmmo(GetPedAmmoType(GetPlayerPed(-1), hash));
                    if (ammoItem != -1)
                    {
                        Managers.Inventory.UnEquipItem(Managers.Inventory.AmmoTypeToAmmo(GetPedAmmoType(GetPlayerPed(-1), hash)), GetAmmoInPedWeapon(GetPlayerPed(-1), hash), 9999, id);
                        await Delay(100);
                    }
                    Managers.Inventory.UnEquipItem(n1, 0, 9999, id);

                    Chat.SendDoCommand($"Было найдено \"{Inventory.GetItemNameById(n)}\"");
                }
            }

            if (Sync.Data.HasLocally(GetServerId(), "GrabCash"))
            {
                var money = (int) Client.Sync.Data.GetLocally(User.GetServerId(), "GrabCash");
                Managers.Inventory.AddItemServer(141, 1, InventoryTypes.Player, id, Convert.ToInt32(money * 0.25), -1, -1, -1);
                Chat.SendDoCommand($"была найдена пачка награбленных денег, суммой ${money:#,#}");
            }
            
            //RemoveAllPedWeapons(GetPlayerPed(-1), false);
        }
        
        public static async void TakeAllGunSAPD(int id)
        {
            for (int n = 54; n < 138; n++)
            {
                foreach(uint hash in Enum.GetValues(typeof(WeaponHash)))
                {
                    string name = Enum.GetName(typeof(WeaponHash), hash);
                    if (!String.Equals(name, Client.Inventory.GetItemNameHashById(n), StringComparison.CurrentCultureIgnoreCase)) continue;
                    if (!HasPedGotWeapon(GetPlayerPed(-1), hash, false)) continue;
                    
                    var n1 = n;
                    int ammoItem = Managers.Inventory.AmmoTypeToAmmo(GetPedAmmoType(GetPlayerPed(-1), hash));
                    if (ammoItem != -1)
                    {
                        Managers.Inventory.UnEquipItem(Managers.Inventory.AmmoTypeToAmmo(GetPedAmmoType(GetPlayerPed(-1), hash)), GetAmmoInPedWeapon(GetPlayerPed(-1), hash), 9999, id);
                        await Delay(100);
                        
                    }

                    Chat.SendDoCommand($"Сдал в арсенал \"{Inventory.GetItemNameById(n)}\"");
                    Main.AddFractionGunLog(User.Data.rp_name, $"DROP: {Inventory.GetItemNameById(n)}", User.Data.fraction_id);
                }
            }
            RemoveWeapons();
        }
        
        
        public static void RemoveWeapons()
        {
            RemoveAllPedWeapons(GetPlayerPed(-1), false);
        }
        
        public static bool IsDead()
        {
            return GetEntityHealth(GetPlayerPed(-1)) <= 102;
        }
        
        private static async Task CheckInfo()
        {
            if (IsLogin() && !Sync.Data.HasLocally(GetServerId(), "GrabCash"))
            {

                if (!Sync.Data.HasLocally(GetServerId(), "GrabCash"))
                {
                    if (GetPlayerWantedLevel(PlayerId()) > 0)
                        ClearPlayerWantedLevel(PlayerId());

                    SetPoliceIgnorePlayer(PlayerId(), true);

                    if (!IsGos())
                    {
                        if (IsPedInAnyVehicle(PlayerPedId(), true))
                        {
                            var veh = new CitizenFX.Core.Vehicle(GetVehiclePedIsUsing(PlayerPedId()));
                            if (GetPedInVehicleSeat(veh.Handle, -1) == PlayerPedId() &&
                                Vehicle.VehInfo.GetClassName(veh.Model.Hash) == "Emergency")
                            {
                                Dispatcher.SendEms("Код 0", $"Угон служебного ТС\nНомера: ~y~{GetVehicleNumberPlateText(veh.Handle)}");
                                //Sync.Data.SetLocally(GetServerId(), "GrabCash", 1);
                                
                                //SetPlayerWantedLevel(Game.Player.Handle, 5, false);
                                //SetPlayerWantedLevelNow(Game.Player.Handle, false);
                                
                                //PedAi.SendCode(1, false, 400, UnitTypes.Standart);
                        
                                Sync.Data.Set(GetServerId(), "wanted_level", 10);
                                Sync.Data.Set(GetServerId(), "wanted_reason", "Угон служебного транспорта");
                            }
                        }
                    }
                }
                else
                {
                    SetPoliceIgnorePlayer(PlayerId(), false);

                    if (!IsGos())
                    {
                        if (IsPedInAnyVehicle(PlayerPedId(), true))
                        {
                            var veh = new CitizenFX.Core.Vehicle(GetVehiclePedIsUsing(PlayerPedId()));
                            if (GetPedInVehicleSeat(veh.Handle, -1) == PlayerPedId() &&
                                Vehicle.VehInfo.GetClassName(veh.Model.Hash) == "Emergency")
                            {
                                SetPlayerWantedLevel(Game.Player.Handle, 5, false);
                                SetPlayerWantedLevelNow(Game.Player.Handle, false);
                            }
                        }
                    }
                }
            }

            await Delay(500);
        }
    }
}

public class PlayerData
{
    public string name { get; set; }
    public string password { get; set; }
    public string lic { get; set; }

    public int id { get; set; }
    public string rp_name { get; set; }
    public string skin { get; set; }
    public string job { get; set; }
    public int reg_status { get; set; }
    public int reg_time { get; set; }
    public int age { get; set; }
    public int exp_age { get; set; }
    public int wanted_level { get; set; }
    public string wanted_reason { get; set; }
    
    public bool is_auth { get; set; }
    public int health { get; set; }

    public int money { get; set; }
    public int money_bank { get; set; }
    public int money_payday { get; set; }
    public int money_tax { get; set; }
    public bool posob { get; set; }
    public int id_house { get; set; }
    public int apartment_id { get; set; }
    public int business_id { get; set; }
    public int stock_id { get; set; }
    public int condo_id { get; set; }
    public int car_id1 { get; set; }
    public int car_id2 { get; set; }
    public int car_id3 { get; set; }
    public int car_id4 { get; set; }
    public int car_id5 { get; set; }
    public int car_id6 { get; set; }
    public int car_id7 { get; set; }
    public int car_id8 { get; set; }
    
    public int car_id1_key { get; set; }
    public int car_id2_key { get; set; }
    public int car_id3_key { get; set; }
    public int car_id4_key { get; set; }
    public int car_id5_key { get; set; }
    public int car_id6_key { get; set; }
    public int car_id7_key { get; set; }
    public int car_id8_key { get; set; }
    
    public bool jailed { get; set; }
    public int jail_time { get; set; }
    
    public int eat_level { get; set; }
    public int water_level { get; set; }
    public int health_level { get; set; }
    public float temp_level { get; set; }
    
    public int sick_cold { get; set; }
    public int sick_poisoning { get; set; }

    public int date_reg { get; set; }
    public int last_login { get; set; }
    public int date_ban { get; set; }
    public int date_mute { get; set; }
    public int warn { get; set; }

    public int fraction_id { get; set; }
    public int rank { get; set; }
    public int fraction_id2 { get; set; }
    public int rank2 { get; set; }
    public string tag { get; set; }

    public int admin_level { get; set; }
    public int helper_level { get; set; }

    public int bank_prefix { get; set; }
    public int bank_number { get; set; }
    
    public int phone_code { get; set; }
    public int phone { get; set; }
    public bool item_clock { get; set; }
    public bool is_buy_walkietalkie { get; set; }
    public string walkietalkie_num { get; set; }
    public bool is_old_money { get; set; }
    public bool sell_car { get; set; }
    public int sell_car_time { get; set; }
    
    public int story_1 { get; set; }
    public int story_timeout_1 { get; set; }

    public int mask { get; set; } //1
    public int mask_color { get; set; }
    public int torso { get; set; } //3
    public int torso_color { get; set; }
    public int leg { get; set; } //4
    public int leg_color { get; set; }
    public int hand { get; set; } //5
    public int hand_color { get; set; }
    public int foot { get; set; } //6
    public int foot_color { get; set; }
    public int accessorie { get; set; } //7
    public int accessorie_color { get; set; }
    public int parachute { get; set; } //8
    public int parachute_color { get; set; }
    public int armor { get; set; } //9
    public int armor_color { get; set; }
    public int decal { get; set; } //10
    public int decal_color { get; set; }
    public int body { get; set; } //11
    public int body_color { get; set; }
    
    public int hat { get; set; } //0
    public int hat_color { get; set; }
    public int glasses { get; set; } //1
    public int glasses_color { get; set; }
    public int ear { get; set; } //2
    public int ear_color { get; set; }
    public int watch { get; set; } //6
    public int watch_color { get; set; }
    public int bracelet { get; set; } //7
    public int bracelet_color { get; set; }
    
    public string tattoo_head_c { get; set; }
    public string tattoo_head_o { get; set; }
    public string tattoo_torso_c { get; set; }
    public string tattoo_torso_o { get; set; }
    public string tattoo_left_arm_c { get; set; }
    public string tattoo_left_arm_o { get; set; }
    public string tattoo_right_arm_c { get; set; }
    public string tattoo_right_arm_o { get; set; }
    public string tattoo_left_leg_c { get; set; }
    public string tattoo_left_leg_o { get; set; }
    public string tattoo_right_leg_c { get; set; }
    public string tattoo_right_leg_o { get; set; }

    public bool allow_marg { get; set; }

    public string vip_status { get; set; }
    public int vip_time { get; set; }
    public string animal { get; set; }
    public string animal_name { get; set; }

    public bool a_lic { get; set; }
    public bool b_lic { get; set; }
    public bool c_lic { get; set; }
    public bool air_lic { get; set; }
    public bool taxi_lic { get; set; }
    public bool ship_lic { get; set; }
    public bool gun_lic { get; set; }
    public bool law_lic { get; set; }
    public bool med_lic { get; set; }
    public bool biz_lic { get; set; }
    public bool animal_lic { get; set; }
    public bool fish_lic { get; set; }

    public bool s_is_pay_type_bank { get; set; }
    public bool s_is_load_blip_house { get; set; }
    public bool s_is_characher { get; set; }
    public bool s_is_spawn_aprt { get; set; }
    public bool s_is_usehackerphone { get; set; }
    public string s_lang { get; set; }
    public string s_clipset { get; set; }
    
    public int s_radio_balance { get; set; }
    public float s_radio_vol { get; set; }
    public float s_voice_vol { get; set; }
    public bool s_voice_balance { get; set; }

    public string ip_last { get; set; }
    
    public string referer { get; set; }
    
    public int mp0_stamina { get; set; }
    public int mp0_strength { get; set; }
    public int mp0_lung_capacity { get; set; }
    public int mp0_wheelie_ability { get; set; }
    public int mp0_flying_ability { get; set; }
    public int mp0_shooting_ability { get; set; }
    public int mp0_stealth_ability { get; set; }
    public int mp0_watchdogs { get; set; }

    public int skill_builder { get; set; }
    public int skill_scrap { get; set; }
    public int skill_shop { get; set; }

    public int count_hask { get; set; }
    public int count_aask { get; set; }
}

public class PlayerSkin
{
    public int SEX {get;set;}
    public int GTAO_SHAPE_FIRST_ID {get;set;}
    public int GTAO_SHAPE_SECOND_ID {get;set;}
    public int GTAO_SHAPE_THRID_ID {get;set;}
    public int GTAO_SKIN_FIRST_ID {get;set;}
    public int GTAO_SKIN_SECOND_ID {get;set;}
    public int GTAO_SKIN_THRID_ID {get;set;}
    public float GTAO_SHAPE_MIX {get;set;}
    public float GTAO_SKIN_MIX {get;set;}
    public float GTAO_THRID_MIX {get;set;}
    public int GTAO_HAIR {get;set;}
    public int GTAO_HAIR_COLOR {get;set;}
    public int GTAO_HAIR_COLOR2 {get;set;}
    public int GTAO_EYE_COLOR {get;set;}
    public int GTAO_EYEBROWS {get;set;}
    public int GTAO_EYEBROWS_COLOR {get;set;}
    public int GTAO_OVERLAY {get;set;}
    public int GTAO_OVERLAY_COLOR {get;set;}
    public int GTAO_OVERLAY4 {get;set;}
    public int GTAO_OVERLAY4_COLOR {get;set;}
    public int GTAO_OVERLAY5 {get;set;}
    public int GTAO_OVERLAY5_COLOR {get;set;}
    public int GTAO_OVERLAY6 {get;set;}
    public int GTAO_OVERLAY6_COLOR {get;set;}
    public int GTAO_OVERLAY7 {get;set;}
    public int GTAO_OVERLAY7_COLOR {get;set;}
    public int GTAO_OVERLAY8 {get;set;}
    public int GTAO_OVERLAY8_COLOR {get;set;}
    public int GTAO_OVERLAY9 {get;set;}
    public int GTAO_OVERLAY9_COLOR {get;set;}
    public int GTAO_OVERLAY10 {get;set;}
    public int GTAO_OVERLAY10_COLOR {get;set;}
}

public class PlayerGun
{
    /*Melee*/
    public int Knife {get;set;}
    public int Nightstick {get;set;}
    public int Hammer {get;set;}
    public int Bat {get;set;}
    public int Crowbar {get;set;}
    public int Golfclub {get;set;}
    public int Bottle {get;set;}
    public int Dagger {get;set;}
    public int Hatchet {get;set;}
    public int KnuckleDuster {get;set;}
    public int Machete {get;set;}
    public int Flashlight {get;set;}
    public int SwitchBlade {get;set;}
    public int Poolcue {get;set;}
    public int Wrench {get;set;}
    public int Battleaxe {get;set;}
    
    /*Handguns*/
    public int Pistol {get;set;}
    public int CombatPistol {get;set;}
    public int Pistol50 {get;set;}
    public int SNSPistol {get;set;}
    public int HeavyPistol {get;set;}
    public int VintagePistol {get;set;}
    public int MarksmanPistol {get;set;}
    public int Revolver {get;set;}
    public int APPistol {get;set;}
    public int StunGun {get;set;}
    public int FlareGun {get;set;}
    
    /*Machine Guns*/
    public int MicroSMG {get;set;}
    public int MachinePistol {get;set;}
    public int SMG {get;set;}
    public int AssaultSMG {get;set;}
    public int CombatPDW {get;set;}
    public int MG {get;set;}
    public int CombatMG {get;set;}
    public int Gusenberg {get;set;}
    public int MiniSMG {get;set;}
    
    /*Assault Rifles*/
    public int AssaultRifle {get;set;}
    public int CarbineRifle {get;set;}
    public int AdvancedRifle {get;set;}
    public int SpecialCarbine {get;set;}
    public int BullpupRifle {get;set;}
    public int CompactRifle {get;set;}
    
    /*Sniper Rifles*/
    public int SniperRifle {get;set;}
    public int HeavySniper {get;set;}
    public int MarksmanRifle {get;set;}
    
    /*Shotguns*/
    public int PumpShotgun {get;set;}
    public int SawnoffShotgun {get;set;}
    public int BullpupShotgun {get;set;}
    public int AssaultShotgun {get;set;}
    public int Musket {get;set;}
    public int HeavyShotgun {get;set;}
    public int DoubleBarrelShotgun {get;set;}
    public int SweeperShotgun {get;set;}
    
    /*Heavy Weapons*/
    public int GrenadeLauncher {get;set;}
    public int RPG {get;set;}
    public int Firework {get;set;}
    public int Railgun {get;set;}
    public int HomingLauncher {get;set;}
    public int GrenadeLauncherSmoke {get;set;}
    public int CompactLauncher {get;set;}
    
    /*Thrown Weapons*/
    public int Grenade {get;set;}
    public int StickyBomb {get;set;}
    public int ProximityMine {get;set;}
    public int BZGas {get;set;}
    public int Molotov {get;set;}
    public int FireExtinguisher {get;set;}
    public int PetrolCan {get;set;}
    public int Flare {get;set;}
    public int Ball {get;set;}
    public int Snowball {get;set;}
    public int SmokeGrenade {get;set;}
    public int Pipebomb {get;set;}
    
    /*Parachute*/
    public int Parachute {get;set;}
}