using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json.Linq;
using static CitizenFX.Core.Native.API;

namespace Server.Managers
{
    public class Event : BaseScript
    {
        public Event()
        {
            EventHandlers.Add("playerConnecting", new Action<Player, string, CallbackDelegate, ExpandoObject>(OnPlayerConnecting));
            EventHandlers.Add("playerDropped", new Action<Player, string, CallbackDelegate>(OnPlayerDropped));

            EventHandlers.Add("ARP:KickPlayer", new Action<Player, int, string>(KickPlayer));
            EventHandlers.Add("ARP:KickPlayerServerId", new Action<Player, int, string>(KickPlayerServerId));
            EventHandlers.Add("ARP:BanPlayerServerId", new Action<Player, int, int, string>(BanPlayerServerId));
            EventHandlers.Add("ARP:BanPlayerByServer", new Action<Player>(BanPlayerByServer));
            EventHandlers.Add("ARP:BlackListPlayerServerId", new Action<Player, int, string>(BlackListPlayerServerId));

            EventHandlers.Add("ARP:LoginPlayer", new Action<Player, string, string>(Login));
            EventHandlers.Add("ARP:RegPlayer", new Action<Player, string, string, string, string, string, bool>(Register));
            EventHandlers.Add("ARP:SendLog", new Action<string, string>(SendLog));
            EventHandlers.Add("ARP:SetVirtualWorld", new Action<Player, int>(SetVirtualWorld));
            EventHandlers.Add("ARP:PlayerFinishLoad", new Action<Player>(PlayerFinishLoad));

            EventHandlers.Add("ARP:UpdateVehNetId", new Action<int, int>(UpdateVehNetId));
            EventHandlers.Add("ARP:UpdateVehInfo", new Action<int, float, float, float, float, float>(UpdateVehInfo));
            EventHandlers.Add("ARP:UpdateVehPark", new Action<int, float, float, float, float>(UpdateVehPark));

            EventHandlers.Add("ARP:SetUserCashMoney", new Action<Player, int>(OnSetCashMoney));
            EventHandlers.Add("ARP:SetUserBankMoney", new Action<Player, int>(OnSetBankMoney));
            EventHandlers.Add("ARP:SetUserPayDayMoney", new Action<Player, int>(OnSetPayDayMoney));

            EventHandlers.Add("ARP:SetCofferMoney", new Action<int>(Coffer.SetMoney));
            EventHandlers.Add("ARP:SetCofferBizzNalog", new Action<int>(Coffer.SetBizzNalog));
            EventHandlers.Add("ARP:SetCofferMoneyOld", new Action<int>(Coffer.SetMoneyOld));
            EventHandlers.Add("ARP:SetCofferNalog", new Action<int>(Coffer.SetNalog));
            EventHandlers.Add("ARP:SetCofferPosob", new Action<int>(Coffer.SetPosob));

            EventHandlers.Add("ARP:SpawnServerVehicle", new Action<Player, int, int>(SpawnServerVehicle));

            EventHandlers.Add("ARP:UpdateHousePin", new Action<int, int>(House.UpdateHousePin));
            EventHandlers.Add("ARP:UpdateHouseInfo", new Action<Player, string, int, int, int>(UpdateHouseInfo));
            EventHandlers.Add("ARP:UpdateHouseInfoHookup", new Action<Player, int, int, string>(UpdateHouseInfoHookup));
            EventHandlers.Add("ARP:UpdateHouseInfoAntiHookup", new Action<Player, int, int>(UpdateHouseInfoAntiHookup));
            EventHandlers.Add("ARP:UpdateCondoInfo", new Action<Player, string, int, int>(UpdateCondoInfo));
            EventHandlers.Add("ARP:UpdateStockInfo", new Action<Player, string, int, int>(UpdateStockInfo));
            EventHandlers.Add("ARP:UpdateApartmentInfo", new Action<Player, string, int, int>(UpdateApartmentInfo));
            EventHandlers.Add("ARP:UpdateSellCarInfo", new Action<Player, string, int, int>(UpdateSellCarInfo));

            EventHandlers.Add("ARP:SaveUserAccount", new Action<Player>(SaveUserAccount));
            EventHandlers.Add("ARP:SaveVehicle", new Action<int>(SaveVehicle));
            EventHandlers.Add("ARP:AutoSpawnPlayer", new Action<Player>(AutoSpawnPlayer));

            EventHandlers.Add("ARP:FromJson", new Action<Player, string>(FromJson));
            EventHandlers.Add("ARP:ToJson", new Action<Player, dynamic>(ToJson));

            EventHandlers.Add("ARP:SendPlayerNotificationPictureToRadio", new Action<string, string, string, string>(SendPlayerNotificationPictureToRadio));
            EventHandlers.Add("ARP:SendPlayerNotificationPictureToFraction", new Action<string, string, string, string, int, int>(SendPlayerNotificationPictureToFraction));
            EventHandlers.Add("ARP:SendPlayerNotificationPictureToJob", new Action<string, string, string, string, int, string>(SendPlayerNotificationPictureToJob));
            EventHandlers.Add("ARP:SendPlayerNotificationPictureToDep", new Action<string, string, string, string, int>(SendPlayerNotificationPictureToDep));
            EventHandlers.Add("ARP:SendPlayerNotificationToDep", new Action<string>(SendPlayerNotificationToDep));
            EventHandlers.Add("ARP:SendPlayerNotificationToFraction", new Action<string, int>(SendPlayerNotificationToFraction));

            EventHandlers.Add("ARP:SendServerToPlayerSubTitle", new Action<string, int>(SendPlayerSubTitle));
            EventHandlers.Add("ARP:SendServerToPlayerTooltip", new Action<string, int>(SendPlayerTooltip));
            EventHandlers.Add("ARP:SendPlayerShowPassByHacker", new Action<Player, int>(SendPlayerShowPassByHacker));
            EventHandlers.Add("ARP:SendPlayerShowPassByHackerByPhone", new Action<Player, int, int>(SendPlayerShowPassByHackerByPhone));
            EventHandlers.Add("ARP:SendPlayerShowPass", new Action<Player, int>(SendPlayerShowPass));
            EventHandlers.Add("ARP:SendPlayerShowDoc", new Action<Player, int>(SendPlayerShowDoc));
            EventHandlers.Add("ARP:SendPlayerShowGovDoc", new Action<Player, int>(SendPlayerShowGovDoc));
            EventHandlers.Add("ARP:UpdatePlayerCashDisplay", new Action<int>(UpdateCashDisplay));
            EventHandlers.Add("ARP:SendSms", new Action<Player, string, string>(SendSms));
            EventHandlers.Add("ARP:OpenSmsListMenu", new Action<Player, string>(OpenSmsListMenu));
            EventHandlers.Add("ARP:OpenContacntListMenu", new Action<Player, string>(OpenContacntListMenu));
            EventHandlers.Add("ARP:OpenSmsInfoMenu", new Action<Player, int>(OpenSmsInfoMenu));
            EventHandlers.Add("ARP:OpenContInfoMenu", new Action<Player, int>(OpenContInfoMenu));
            EventHandlers.Add("ARP:DeleteSms", new Action<Player, int>(DeleteSms));
            EventHandlers.Add("ARP:AddContact", new Action<Player, string, string, string>(AddContact));
            EventHandlers.Add("ARP:DeleteContact", new Action<Player, int>(DeleteContact));
            EventHandlers.Add("ARP:SendPlayerNotificationPictureToAll", new Action<string, string, string, string, int>(SendPlayerNotificationPictureToAll));
            EventHandlers.Add("ARP:SendServerToPlayerJail", new Action<string, int, int>(SendServerToPlayerJail));
            EventHandlers.Add("ARP:SendPlayerMembers", new Action<Player>(SendPlayerMembers));
            EventHandlers.Add("ARP:SendPlayerMembers2", new Action<Player>(SendPlayerMembers2));
            EventHandlers.Add("ARP:SendPlayersList", new Action<Player>(SendPlayersList));
            EventHandlers.Add("ARP:SendPlayerBankClientInfo", new Action<Player, int>(SendPlayerBankClientInfo));
            EventHandlers.Add("ARP:SendPlayerVehicleLog", new Action<Player>(SendPlayerVehicleLog));
            EventHandlers.Add("ARP:SendPlayerGunLog", new Action<Player>(SendPlayerGunLog));
            EventHandlers.Add("ARP:SendPlayerStockLog", new Action<Player, int>(SendPlayerStockLog));
            EventHandlers.Add("ARP:AddFractionGunLog", new Action<Player, string, string, int>(AddFractionGunLog));
            EventHandlers.Add("ARP:AddFractionVehicleLog", new Action<Player, string, string, int>(AddFractionVehicleLog));
            EventHandlers.Add("ARP:AddStockLog", new Action<Player, string, string, int>(AddStockLog));
            EventHandlers.Add("ARP:OpenBusinnesListMenu", new Action<Player, int>(OpenBusinnesListMenu));
            EventHandlers.Add("ARP:OpenApartamentListMenu", new Action<Player, int, int>(OpenApartamentListMenu));
            EventHandlers.Add("ARP:SaveBusiness", new Action<int>(Save.Business));

            EventHandlers.Add("ARP:UnDuty", new Action<Player>(UnDuty));
            //EventHandlers.Add("ARP:PartnerCheck", Action<Players>(PartnerCheck));

            EventHandlers.Add("ARP:ChangeNumberPhone", new Action<Player, int, int>(ChangeNumberPhone));
            EventHandlers.Add("ARP:ChangeNumberCard", new Action<Player, int, int>(ChangeNumberCard));
            EventHandlers.Add("ARP:TransferMoneyBank", new Action<Player, int, int, int>(TransferMoneyBank));

            EventHandlers.Add("ARP:SendRadiusMessage", new Action<Player, string, float, float, float>(SendRadiusMessage));
            EventHandlers.Add("ARP:GivePlayerRank", new Action<Player, string, int>(GivePlayerRank));
            EventHandlers.Add("ARP:GivePlayerRank2", new Action<Player, string, int>(GivePlayerRank2));
            EventHandlers.Add("ARP:Uninvite", new Action<Player, string>(Uninvite));
            EventHandlers.Add("ARP:Uninvite2", new Action<Player, string>(Uninvite2));

            EventHandlers.Add("ARP:ResetLoto", new Action(Sync.ResetLoto));
            EventHandlers.Add("ARP:WinLotoLoto", new Action<string>(Sync.WinLotoLoto));

            EventHandlers.Add("ARP:CheckSoloSession", new Action<Player, int>(CheckSoloSession));
            EventHandlers.Add("ARP:SendReferrer", new Action<Player>(SendReferrer));

            EventHandlers.Add("ARP:AddAd", new Action<string, string, string, string>(AddAd));
            EventHandlers.Add("ARP:AddNewVehicle", new Action<Player, int, string, string, int, int, int, int, float, float, float, float, int, int>(AddNewVehicle));
            EventHandlers.Add("ARP:SetCarNumber", new Action<Player, int, int, int, string, int, int>(SetCarNumber));
            EventHandlers.Add("ARP:PayTax", new Action<Player, int, int, int>(Tax.PayTax));

            EventHandlers.Add("ARP:OnVip", new Action<Player>(OnVip));

            EventHandlers.Add("ARP:CreateUnofFraction", new Action<Player, string>(FractionUnoff.Create));
            EventHandlers.Add("ARP:RenameUnofFraction", new Action<Player, int, string>(FractionUnoff.Rename));
            EventHandlers.Add("ARP:DeleteUnofFraction", new Action<Player, int>(FractionUnoff.Delete));

            EventHandlers.Add("ARP:Promocode", new Action<Player, string>(Promocode));

            //EventHandlers.Add("ARP:GrSix:Partner", new Action<Player, Player, int>(GrSix.PartnerSet));
            EventHandlers.Add("ARP:GrSix:DropMoney", new Action<int, int>(GrSixDropMoney));
            EventHandlers.Add("ARP:GrSix:MoneyInCarCheck", new Action<Player, int>(CheckMoneyGrSix));
            EventHandlers.Add("ARP:GrSix:Grab", new Action<Player, int>(GrabGrSix));
            EventHandlers.Add("ARP:GrSix:Payment", new Action<Player, int>(Payment));
            EventHandlers.Add("ARP:GrSix:ResetMoneyInCar", new Action<int>(ResetMoneyInCar));

            Tick += DataBaseSync;
        }

        protected static int rand = 0;

        protected static void Promocode([FromSource] Player player, string code)
        {
            bool isValid = false;

            switch (code)
            {
                case "VK":
                case "MYRKA":
                case "BROTHERS":
                case "VMP":
                case "REDAGE":
                case "UMBRELLA":
                case "VINIPUX":
                    isValid = true;
                    break;
            }

            int userId = User.GetId(player);
            if (Appi.MySql.ExecuteQueryWithResult("SELECT * FROM promocode_using WHERE promocode_name = '" + code + "' AND user_id = '" + userId + "'").Rows.Count > 0)
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Промокод уже был использован");
                return;
            }

            if (isValid)
            {
                Appi.MySql.ExecuteQuery($"INSERT INTO promocode_using (promocode_name, user_id) VALUES ('{code}', '{userId}')");
                TriggerClientEvent(player, "ARP:PromocodeActivate", code);
                return;
            }

            if (Appi.MySql.ExecuteQueryWithResult("SELECT * FROM promocode_list WHERE code = '" + code + "'").Rows.Count == 0)
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Промокод не найден");
                return;
            }

            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM promocode_list WHERE code = '" + code + "'").Rows)
            {
                User.AddCashMoney(player, (int) row["bonus"]);
                Appi.MySql.ExecuteQuery($"INSERT INTO promocode_using (promocode_name, user_id) VALUES ('{code}', '{userId}')");
                TriggerClientEvent(player, "ARP:SendPlayerNotification", "~g~Вы активировали промокод: $" + (int) row["bonus"]);
                return;
            }

            TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Промокод не найден");
        }

        protected static void CheckSoloSession([FromSource] Player player, int players)
        {
            var plList = new PlayerList();
            if (plList.Count() <= 4) return;
            if (players == 1)
                player.Drop("[KICK] solo session");
        }

        protected static void AddAd(string text, string rpName, string phone, string type)
        {
            Appi.MySql.ExecuteQuery("INSERT INTO rp_invader_ad (datetime, name, phone, title, text) " + $"VALUES ('{Weather.GetRpDateTime()}', '{rpName}', '{phone}', '{type}', '{text}')");
        }

        protected static void SetCarNumber([FromSource] Player player, int vehId, int lscId, int vehHandle,
            string newNumber, int hashNewNumber, int hashOldNumber)
        {
            if (Appi.MySql.ExecuteQueryWithResult("SELECT * FROM cars WHERE number = '" + newNumber + "'").Rows.Count >
                0)
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Номер ТС уже существует");
                return;
            }

            Server.Sync.Data.Set(110000 + vehId, "Number", newNumber);

            User.RemoveCashMoney(player, 40000);
            Business.AddMoney(lscId, 40000);

            Save.UserVehicleById(vehId);

            User.UpdateAllData(player);

            Appi.MySql.ExecuteQuery($"UPDATE items SET owner_id = '{hashNewNumber}' where owner_id = '{hashOldNumber}' and (owner_type = '2' or owner_type = '3' or owner_type = '4')");
            Main.SaveLog("ChangeNumberLSC", $"{Server.Sync.Data.Get(User.GetServerId(player), "rp_name")} | vehId: {vehId} | newNumber: {newNumber}");
            TriggerClientEvent(player, "ARP:SendPlayerNotification", "~g~Вы изменили номер транспорта");
            TriggerClientEvent("ARP:UpdateVehicleNumber", User.GetServerId(player), vehId, vehHandle, newNumber);
        }

        protected static void ToJson([FromSource] Player player, dynamic data)
        {
            TriggerClientEvent(player, "ARP:ToJsonServer", Main.ToJson(data));
        }

        protected static void FromJson([FromSource] Player player, string json)
        {
            Main.SaveLog("ServerLSC", player.Name + " | " + json);
            TriggerClientEvent(player, "ARP:FromJsonServer",
                Main.FromJson(json).ToObject<Dictionary<string, object>>());
        }

        protected static void AddNewVehicle([FromSource] Player player, int hash, string displayName, string className,
            int fullFuel, int fuelMin, int stockFull, int stock, float x, float y, float z, float rot, int price,
            int count)
        {
            if (!User.IsAdmin(User.GetServerId(player))) return;

            var rand = new Random();

            for (int i = 0; i < count; i++)
            {

                int color = rand.Next(156);
                const string chars = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var number = "";
                for (int j = 0; j < 8; j++)
                    number += chars[rand.Next(0, chars.Length - 1)];
                Appi.MySql.ExecuteQuery(
                    "INSERT INTO cars (hash, name, class_type, full_fuel, fuel, fuel_minute, color1, color2, number, stock, stock_full, price, x, y, z, rot) " + 
                    $"VALUES ('{hash}', '{displayName}', '{className}', '{fullFuel}', '{fullFuel}', '{fuelMin}', '{color}', '{color}', '{number}', '{stock}', '{stockFull}', " +
                    $"'{price}', '{x}', '{y}', '{z}', '{rot}')");
            }
        }

        protected static void OnVip([FromSource] Player player)
        {
            if (User.GetVipStatus(User.GetServerId(player)) == "Hard")
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotificationPicture", $"Вам были начислены: 2 AC!", "Онлайн кошелек", "Начисление", "CHAR_ACTING_UP", 2);
                Appi.MySql.ExecuteQuery("UPDATE users SET money_donate = money_donate + '2' WHERE id = '" + User.GetId(player) + "'");
            }
            if (User.GetVipStatus(User.GetServerId(player)) == "Light")
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotificationPicture", $"Вам был начислен: ~g~1 AC!", "Онлайн кошелек", "Начисление", "CHAR_ACTING_UP", 2);
                Appi.MySql.ExecuteQuery("UPDATE users SET money_donate = money_donate + '1' WHERE id = '" + User.GetId(player) + "'");
            }
        }

        protected static void GivePlayerRank([FromSource] Player player, string name, int rank)
        {
            if (rank == 14) return;

            Main.SaveLog("fractionLog",
                $"[GIVERANK] {Server.Sync.Data.Get(User.GetServerId(player), "rp_name")} ({Server.Sync.Data.Get(User.GetServerId(player), "fraction_id")}) - {name}");

            foreach (var pl in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(pl))) continue;
                if ((string) Server.Sync.Data.Get(User.GetServerId(pl), "rp_name") != name) continue;
                Server.Sync.Data.Set(User.GetServerId(pl), "rank", rank);
                User.SendPlayerSubTitle(player, $"~g~Вы выдали ранг {name} - {rank}");
                User.SendPlayerSubTitle(pl, $"~g~Вам выдали ранг {rank}");
                User.UpdateAllData(pl);
                return;
            }

            if ((int) Server.Sync.Data.Get(User.GetServerId(player), "rank") != 14)
            {
                User.SendPlayerSubTitle(player, "~r~Вы не лидер чтобы уволнять оффлайн");
                return;
            }

            Appi.MySql.ExecuteQuery("UPDATE users SET rank = '" + rank + "' where rp_name = '" + name +
                                    "' AND rank < 14");
            User.SendPlayerSubTitle(player, $"~g~Вы выдали ранг {name} - {rank}");
        }

        protected static void GivePlayerRank2([FromSource] Player player, string name, int rank)
        {
            if (rank == 11) return;

            Main.SaveLog("fractionLog2",
                $"[GIVERANK] {Server.Sync.Data.Get(User.GetServerId(player), "rp_name")} ({Server.Sync.Data.Get(User.GetServerId(player), "fraction_id2")}) - {name}");

            foreach (var pl in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(pl))) continue;
                if ((string) Server.Sync.Data.Get(User.GetServerId(pl), "rp_name") != name) continue;
                Server.Sync.Data.Set(User.GetServerId(pl), "rank2", rank);
                User.SendPlayerSubTitle(player, $"~g~Вы выдали ранг {name} - {rank}");
                User.SendPlayerSubTitle(pl, $"~g~Вам выдали ранг {rank}");
                User.UpdateAllData(pl);
                return;
            }

            if ((int) Server.Sync.Data.Get(User.GetServerId(player), "rank2") != 11)
            {
                User.SendPlayerSubTitle(player, "~r~Вы не лидер чтобы уволнять оффлайн");
                return;
            }

            Appi.MySql.ExecuteQuery("UPDATE users SET rank2 = '" + rank + "' where rp_name = '" + name +
                                    "' AND rank2 < 11");
            User.SendPlayerSubTitle(player, $"~g~Вы выдали ранг {name} - {rank}");
        }

        protected static void UnDuty([FromSource] Player player)
        {
            foreach (var pl in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(pl))) continue;
                Server.Sync.Data.Set(User.GetServerId(pl), "duty", 0);
                TriggerClientEvent("ARP:UpdateAllData");
                return;
            }
        }

        protected static void GrSixDropMoney(int veh, int money)
        {
            Server.Sync.Data.Set(veh, "GrSix:MoneyInCar", money);
        }

        protected static void CheckMoneyGrSix([FromSource] Player player,  int veh)
        {
            if (Server.Sync.Data.Has(veh, "GrSix:MoneyInCar"))
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotification", $"В машине: $\"{Server.Sync.Data.Get(veh, "GrSix:MoneyInCar")}\"");
            }
            else
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotification", "Вы еще ничего не собрали");
            }
        }
        protected static void GrabGrSix([FromSource] Player player, int veh)
        {
            if (Server.Sync.Data.Has(veh, "HasGrab"))
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotification", "~g~Транспорт уже ограбили");
                return;
            }
            if (Server.Sync.Data.Get(veh, "GrSix:MoneyInCar") == 0)
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotification", "~g~Нечего грабить, транспорт пуст");
                return;
            }
            Server.Sync.Data.Set(veh, "HasGrab", true);
            int cash = Server.Sync.Data.Get(veh, "GrSix:MoneyInCar") / 130;
            TriggerEvent("ARP:TriggerEventToPlayer", User.GetServerId(player), "ARP:GrSix:Grab", 1, cash);
        }

        protected static void Payment([FromSource] Player player, int veh)
        {
            if (Server.Sync.Data.Has(veh, "GrSix:MoneyInCar"))
            {
                int cash = Server.Sync.Data.Get(veh, "GrSix:MoneyInCar") / 130;
                TriggerEvent("ARP:TriggerEventToPlayer", User.GetServerId(player), "ARP:GrSix:Pay", 2, cash, veh);
            }
            else
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotification", "~y~Вы ничего не заработали");
                TriggerEvent("ARP:TriggerEventToPlayer", User.GetServerId(player), "ARP:GrSix:Pay", 2, 0, veh);
            }
        }

        protected static void ResetMoneyInCar(int veh)
        {
            Server.Sync.Data.Reset(veh, "GrSix:MoneyInCar");
            Server.Sync.Data.Reset(veh, "HasGrab");
        }
        protected static void Uninvite([FromSource] Player player, string name)
        {
            Main.SaveLog("fractionLog", $"[UNINVITE] {Server.Sync.Data.Get(User.GetServerId(player), "rp_name")} ({Server.Sync.Data.Get(User.GetServerId(player), "fraction_id")}) - {name}");
            
            foreach (var pl in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(pl))) continue;
                if ((string) Server.Sync.Data.Get(User.GetServerId(pl), "rp_name") != name) continue;
                Server.Sync.Data.Set(User.GetServerId(pl), "rank", 0);
                Server.Sync.Data.Set(User.GetServerId(pl), "fraction_id", 0);
                User.SendPlayerSubTitle(player, $"~g~Вы уволили {name}");
                User.SendPlayerSubTitle(pl, $"~g~Ваc уволили из организации");
                TriggerClientEvent("ARP:UpdateAllData");
                return;
            }

            if ((int) Server.Sync.Data.Get(User.GetServerId(player), "rank") != 14)
            {
                User.SendPlayerSubTitle(player, "~r~Вы не лидер чтобы уволнять оффлайн");
                return;
            }
            
            Appi.MySql.ExecuteQuery("UPDATE users SET rank = '0', fraction_id = '0' where rp_name = '" + name + "' AND rank < 14");
            User.SendPlayerSubTitle(player, $"~g~Вы уволили {name}");
        }

        protected static void Uninvite2([FromSource] Player player, string name)
        {
            Main.SaveLog("fractionLog2", $"[UNINVITE] {Server.Sync.Data.Get(User.GetServerId(player), "rp_name")} ({Server.Sync.Data.Get(User.GetServerId(player), "fraction_id2")}) - {name}");
            
            foreach (var pl in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(pl))) continue;
                if ((string) Server.Sync.Data.Get(User.GetServerId(pl), "rp_name") != name) continue;
                Server.Sync.Data.Set(User.GetServerId(pl), "rank2", 0);
                Server.Sync.Data.Set(User.GetServerId(pl), "fraction_id2", 0);
                User.SendPlayerSubTitle(player, $"~g~Вы уволили {name}");
                User.SendPlayerSubTitle(pl, $"~g~Ваc уволили из организации");
                TriggerClientEvent("ARP:UpdateAllData");
                return;
            }

            if ((int) Server.Sync.Data.Get(User.GetServerId(player), "rank") != 11)
            {
                User.SendPlayerSubTitle(player, "~r~Вы не лидер чтобы уволнять оффлайн");
                return;
            }
            
            Appi.MySql.ExecuteQuery("UPDATE users SET rank2 = '0', fraction_id2 = '0' where rp_name = '" + name + "' AND rank2 < 11");
            User.SendPlayerSubTitle(player, $"~g~Вы уволили {name}");
        }

        protected static void SendRadiusMessage([FromSource] Player player, string msg, float x, float y, float z)
        {
            if (!Server.Sync.Data.Has(User.GetServerId(player), "id")) return;
            string name = (int) Server.Sync.Data.Get(User.GetServerId(player), "id") + "";
            
            if (msg.Contains("/me "))
            {
                msg = msg.Remove(0, 4);
                TriggerClientEvent("ARP:SendRadiusMessageToClient", name, $"<span style=\"color: #C2A2DA\">{name} {msg}</span>", x, y, z, true); 
                    
            }
            else if (msg.Contains("/do "))
            {
                msg = msg.Remove(0, 4);
                TriggerClientEvent("ARP:SendRadiusMessageToClient", name, $"<span style=\"color: #C2A2DA\">(( {msg} )) {name}</span>", x, y, z, true); 
                    
            }
            else if (msg.Contains("/try "))
            {
                msg = msg.Remove(0, 5);
                Random rand = new Random();
                TriggerClientEvent("ARP:SendRadiusMessageToClient", name, $"<span style=\"color: #C2A2DA\">{(rand.Next(2) == 0 ? "Удачно" : "Не удачно")} {name} {msg}</span>", x, y, z, true); 
            }
            else if (msg.Contains("/diceHASH"))
            {
                Random rand = new Random();
                TriggerClientEvent("ARP:SendRadiusMessageToClient", name, $"<span style=\"color: #C2A2DA\">{name} бросил кости </span><span style=\"color: #FF9800\">(( Выпало: {(rand.Next(6) + 1)} ))</span>", x, y, z, true); 
            }
            else if (msg.Contains("/b "))
            {
                msg = msg.Remove(0, 3);
                TriggerClientEvent("ARP:SendRadiusMessageToClient", name, $"(( {msg} ))", x, y, z, false);
            }

            else
                TriggerClientEvent("ARP:SendRadiusMessageToClient", name, msg, x, y, z, false);
        }

        protected static void SendPlayerNotificationPictureToAll(string text, string title, string subtitle, string icon, int type)
        {
            TriggerClientEvent("ARP:SendPlayerNotificationPicture", text, title, subtitle, icon, type);
        }

        protected static void SendPlayerNotificationPictureToRadio(string text, string title, string subtitle, string num)
        {
            foreach (var pl in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(pl))) continue;
                if ((string) Server.Sync.Data.Get(User.GetServerId(pl), "walkietalkie_num") != num) continue;
                TriggerClientEvent(pl, "ARP:SendPlayerNotificationPicture", text, title, subtitle, "CHAR_DEFAULT", 1);
            }
        }

        protected static void SendPlayerNotificationPictureToFraction(string text, string title, string subtitle, string icon, int type, int fraction)
        {
            foreach (var pl in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(pl))) continue;
                if ((int) Server.Sync.Data.Get(User.GetServerId(pl), "fraction_id") != fraction) continue;
                TriggerClientEvent(pl, "ARP:SendPlayerNotificationPicture", text, title, subtitle, icon, type);
            }
        }

        protected static void SendPlayerNotificationToFraction(string text, int fraction)
        {
            foreach (var pl in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(pl))) continue;
                if ((int) Server.Sync.Data.Get(User.GetServerId(pl), "fraction_id") != fraction) continue;
                TriggerClientEvent(pl, "ARP:SendPlayerNotification", text);
            }
        }

        protected static void SendPlayerNotificationPictureToJob(string text, string title, string subtitle, string icon, int type, string job)
        {
            foreach (var pl in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(pl))) continue;
                if ((string) Server.Sync.Data.Get(User.GetServerId(pl), "job") != job) continue;
                TriggerClientEvent(pl, "ARP:SendPlayerNotificationPicture", text, title, subtitle, icon, type);
            }
        }

        protected static void SendPlayerNotificationPictureToDep(string text, string title, string subtitle, string icon, int type)
        {
            foreach (var pl in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(pl))) continue;
                int fractionId = (int) Server.Sync.Data.Get(User.GetServerId(pl), "fraction_id");
                if (fractionId == 1 || fractionId == 2 || fractionId == 3 || fractionId == 7 || fractionId == 16)
                    TriggerClientEvent(pl, "ARP:SendPlayerNotificationPicture", text, title, subtitle, icon, type);
            }
        }

        protected static void SendPlayerNotificationToDep(string text)
        {
            foreach (var pl in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(pl))) continue;
                int fractionId = (int) Server.Sync.Data.Get(User.GetServerId(pl), "fraction_id");
                if (fractionId == 1 || fractionId == 2 || fractionId == 3 || fractionId == 7 || fractionId == 16)
                    TriggerClientEvent(pl, "ARP:SendPlayerNotification", text);
            }
        }

        protected static void OnSetCashMoney([FromSource] Player player, int money)
        {
            User.SetCashMoney(player, money);
        }

        protected static void OnSetBankMoney([FromSource] Player player, int money)
        {
            User.SetBankMoney(player, money);
        }

        protected static void OnSetPayDayMoney([FromSource] Player player, int money)
        {
            User.SetPayDayMoney(player, money);
        }
    
        protected static async void OnPlayerConnecting([FromSource]Player player, string playerName, CallbackDelegate kickCallback, dynamic deferrals)
        {
            try
            {
                deferrals.defer();
                deferrals.update("Хм...");
                await Delay(500);

                string guid = GetPlayerGuid(player.Handle);
                string license = player.Identifiers["license"];
                string live = player.Identifiers["live"];
                string xbl = player.Identifiers["xbl"];

                if (!string.IsNullOrEmpty(guid))
                {
                    if (Appi.MySql.ExecuteQueryWithResult("SELECT * FROM black_list WHERE guid = '" + guid + "'").Rows.Cast<DataRow>().Any())
                    {
                        deferrals.done("Вы находитесь в черном списке ;c"); 
                        return;
                    }
                }
                
                if (!string.IsNullOrEmpty(license))
                {
                    if (Appi.MySql.ExecuteQueryWithResult("SELECT * FROM black_list WHERE lic = '" + license + "'").Rows.Cast<DataRow>().Any())
                    {
                        deferrals.done("Вы находитесь в черном списке ;c"); 
                        return;
                    }
                }
                
                if (!string.IsNullOrEmpty(live))
                {
                    if (Appi.MySql.ExecuteQueryWithResult("SELECT * FROM black_list WHERE live = '" + live + "'").Rows.Cast<DataRow>().Any())
                    {
                        deferrals.done("Вы находитесь в черном списке ;c"); 
                        return;
                    }
                }
                
                if (!string.IsNullOrEmpty(xbl))
                {
                    if (Appi.MySql.ExecuteQueryWithResult("SELECT * FROM black_list WHERE xbl = '" + xbl + "'").Rows.Cast<DataRow>().Any())
                    {
                        deferrals.done("Вы находитесь в черном списке ;c"); 
                        return;
                    }
                }
                
                if (Main.ServerName == "Earth")
                {
                    if (!string.IsNullOrEmpty(license))
                    {
                        if (!Appi.MySql.ExecuteQueryWithResult("SELECT * FROM white_list WHERE lic = '" + license + "'").Rows.Cast<DataRow>().Any())
                        {
                            deferrals.done("Вы не находитесь в вайтлисте. HASH: " + license);
                            return;
                        }
                    }
                    else
                    {
                        deferrals.done("Вы не находитесь в вайтлисте. HASH: " + license);
                        return;
                    }
                }

                if (new PlayerList().Count() >= Main.MaxPlayers)
                {
                    deferrals.done("Сервер переполнен :c");
                    return;
                }
				
                var rand = new Random();
                User.SetVirtualWorld(player, rand.Next(1, 10000));
                Main.SaveLog("Connect", $"[{GetPlayerEndpoint(player.Handle)}] [Connect] {player.Name} | {User.GetServerId(player)} | {license} | {guid}");
                
                deferrals.update("Привет! Добро пожаловать на Alamo RolePlay :3");
                await Delay(2500);
                deferrals.update("Желаем тебе приятной игры c:");
                await Delay(2500);
                deferrals.done();
            }
            catch(Exception)
            {
                deferrals.done("Неизвестная ошибка, попробуйте зайти еще раз"); 
                return;
            }
        }
    
        protected static void OnPlayerDropped([FromSource]Player player, string playerName, CallbackDelegate kickReason)
        {
            //User.UnloadAccount(User.GetServerId(player));

            if (User.IsLogin(player))
            {
                foreach (uint hash in Enum.GetValues(typeof(WeaponHash)))
                {
                    if (Server.Sync.Data.Has(User.GetServerId(player), hash.ToString()))
                    {
                        for (int itemId = 50; itemId < 150; itemId++)
                        {
                            string name = Enum.GetName(typeof(WeaponHash), hash);
                            if (Inventory.ItemList[itemId, 1] == name)
                            {
                                Inventory.AddItem(player, itemId, 1, InventoryTypes.Player, User.GetId(player), 1, -1, -1, -1);
                                Server.Sync.Data.Reset(User.GetServerId(player), hash.ToString());   
                            }
                        }
                    }
                }
            }
            
            Appi.MySql.ExecuteQuery("UPDATE users SET is_online='0' WHERE id = '" + Convert.ToInt32(Server.Sync.Data.Get(User.GetServerId(player), "id")) + "'");
            Save.UserAccount(player);
            Main.SaveLog("Connect", $"[{GetPlayerEndpoint(player.Handle)}] [Disconnect] " + player.Name + " " + kickReason);
        }
    
        protected static async void PlayerFinishLoad([FromSource]Player player)
        {
            string license = player.Identifiers["license"];
            string guid = GetPlayerGuid(player.Handle);
            
            string lastName = "";
            if (!string.IsNullOrEmpty(license))
            {
                foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM log_auth WHERE lic = '" + license + "' ORDER BY id DESC LIMIT 1").Rows)
                {
                    lastName = (string) row["nick"];
                    Debug.WriteLine("LAST NICK " + lastName);
                }
            }
            TriggerClientEvent(player, "ARP:UpdateLastName", lastName);
            
            /*if (!string.IsNullOrEmpty(guid))
            {
                var tbl = Appi.MySql.ExecuteQueryWithResult("SELECT * FROM black_list WHERE guid = '" + guid + "'");
                if (tbl.Rows.Cast<DataRow>().Any()) { player.Drop("You are in blacklist"); return; }
            }*/
            
            /*string steam = player.Identifiers["steam"];
            if (!string.IsNullOrEmpty(steam))
            {
                var tbl = Appi.MySql.ExecuteQueryWithResult("SELECT * FROM black_list WHERE steam = '" + steam + "'");
                if (tbl.Rows.Cast<DataRow>().Any()) { player.Drop("You are in blacklist"); return; }
            }*/
            
            Weather.SetPlayerCurrentWeather(player);
            Inventory.LoadAllItems(player);

            await Delay(100);
            
            //TriggerClientEvent(player, "ARP:AddVehicleInfoGlobalDataList", Vehicle.VehicleInfoGlobalDataList);            
            TriggerClientEvent(player, "ARP:AddHouseGlobalDataList", House.HouseGlobalDataList);
            TriggerClientEvent(player, "ARP:AddCondoGlobalDataList", Condo.CondoGlobalDataList);
            TriggerClientEvent(player, "ARP:AddStockGlobalDataList", Stock.StockGlobalDataList);
            TriggerClientEvent(player, "ARP:UpdateServerName", Main.ServerName);
            Main.UpdateDiscordStatus();
        }
        
        protected static void AutoSpawnPlayer([FromSource]Player player)
        {
            User.SpawnAuto(player);
        }
        
        protected static void UpdateSellCarInfo([FromSource]Player player, string userName, int playerId, int vehicleId)
        {
            Vehicle.BuyAndSellCar(vehicleId, userName, playerId);
        }
        
        protected static void UpdateHouseInfo([FromSource]Player player, string userName, int playerId, int isBuy, int houseId)
        {
            House.SaveHouse(houseId, isBuy, userName, playerId);
        }
        
        protected static void UpdateHouseInfoHookup([FromSource]Player player, int houseId, int playerId, string pidn)
        {
            House.SaveHouseHookup(houseId, playerId, pidn);
        }
        
        protected static void UpdateHouseInfoAntiHookup([FromSource]Player player, int houseId, int playerId)
        {
            House.SaveHouseAntiHookup(houseId, playerId);
        }
        protected static void UpdateCondoInfo([FromSource]Player player, string userName, int playerId, int houseId)
        {
            Condo.SaveHouse(houseId, userName, playerId);
        }
        
        protected static void UpdateStockInfo([FromSource]Player player, string userName, int playerId, int houseId)
        {
            Stock.Save(houseId, userName, playerId);
        }
        
        protected static void UpdateApartmentInfo([FromSource]Player player, string userName, int playerId, int houseId)
        {
            Apartment.Save(houseId, userName, playerId);
        }
        
        protected static void SpawnServerVehicle([FromSource]Player player, int vehId, int playerId)
        {
            Vehicle.SpawnServerVehicle(player, vehId, playerId);
        }
        
        protected static void UpdateVehNetId(int id, int netid)
        {
            Vehicle.UpdateVehNetId(id, netid);
        }
        
        protected static void UpdateVehInfo(int id, float fuel, float x, float y, float z, float rot)
        {
            Vehicle.UpdateVehInfo(id, fuel, x, y, z, rot);
        }
        
        protected static void UpdateVehPark(int id, float x, float y, float z, float rot)
        {
            Vehicle.ParkCar(id, x, y, z, rot);
        }
        
        protected static void SetVirtualWorld([FromSource] Player player, int id)
        {
            User.SetVirtualWorld(player, id);
        }
        
        protected static void KickPlayer([FromSource] Player player, int id, string kickReason = "You have been kicked")
        {
            DropPlayer(player.Handle, kickReason);
        }
        
        protected static void KickPlayerServerId([FromSource] Player pl, int id, string kickReason = "You have been kicked")
        {
            if (!User.IsAdmin(User.GetServerId(pl))) return;
            
            foreach (var player in new PlayerList())
            {
                if (User.GetServerId(player) != id) continue;
                DropPlayer(player.Handle, kickReason);
                Main.SaveLog("kick", $"{Server.Sync.Data.Get(User.GetServerId(pl), "rp_name")} kick {Server.Sync.Data.Get(User.GetServerId(player), "rp_name")} {kickReason}" );
                return;
            }
        }
        
        protected static async void BanPlayerServerId([FromSource] Player pl, int id, int idx, string kickReason = "You have been kicked")
        {
            if (!User.IsAdmin(User.GetServerId(pl))) return;
            
            foreach (var player in new PlayerList())
            {
                if (User.GetServerId(player) != id) continue;
                
                int timeFormat = 1;
                
                switch (idx)
                {
                    case 0: 
                        timeFormat = 1 * 60 * 60; 
                        break;
                    case 1: 
                        timeFormat = 12 * 60 * 60; 
                        break;
                    case 2: 
                        timeFormat = 1 * 60 * 60 * 24; 
                        break;
                    case 3: 
                        timeFormat = 3 * 60 * 60 * 24; 
                        break;
                    case 4: 
                        timeFormat = 7 * 60 * 60 * 24; 
                        break;
                    case 5: 
                        timeFormat = 14 * 60 * 60 * 24; 
                        break;
                    case 6: 
                        timeFormat = 30 * 60 * 60 * 24; 
                        break;
                    case 7: 
                        timeFormat = 90 * 60 * 60 * 24; 
                        break;
                }
                
                Server.Sync.Data.Set(User.GetServerId(player), "date_ban", Main.GetTimeStamp() + timeFormat);

                await Delay(500);
                
                Save.UserAccount(player);
                
                //string sql1 = "UPDATE users SET date_ban = '" + (Main.GetTimeStamp() + timeFormat) + "' where id = '" + (int) Server.Sync.Data.Get(User.GetServerId(pl), "id") + "'";
                //Appi.MySql.ExecuteQuery(sql1);
                
                var list = new List<string> {"1", "12", "1", "3", "7", "14", "30", "90"};
                var listType = new List<string> {"h", "h", "d", "d", "d", "d", "d", "d"};

                string admin = (string) Server.Sync.Data.Get(User.GetServerId(pl), "rp_name");
                string plban = (string) Server.Sync.Data.Get(User.GetServerId(player), "rp_name");
                
                string sql = "INSERT INTO ban_list (ban_from, ban_to, count, datetime, format, reason) VALUES ('" + admin + "','" + plban + "','" + list[idx] + "','" + Main.GetTimeStamp() + "','" + listType[idx] + ".','" + kickReason + "')";
                Appi.MySql.ExecuteQuery(sql);
                
                DropPlayer(player.Handle, $"[BAN] {kickReason}");
                Main.SaveLog("ban", $"{admin} ban {plban} | {kickReason} {list[idx]}{listType[idx]} " );
                return;
            }
        }
        
        protected static async void BanPlayerByServer([FromSource] Player pl)
        {
            int timeFormat = 5 * 365 * 60 * 60 * 24;
            
            Server.Sync.Data.Set(User.GetServerId(pl), "date_ban", Main.GetTimeStamp() + timeFormat);

            await Delay(500);
            
            Save.UserAccount(pl);
            
            string plban = (string) Server.Sync.Data.Get(User.GetServerId(pl), "rp_name");
            
            string sql = "INSERT INTO ban_list (ban_from, ban_to, count, datetime, format, reason) VALUES ('Server','" + plban + "','10','" + Main.GetTimeStamp() + "','y.','До выяснения (Обратитесь к Appi)')";
            Appi.MySql.ExecuteQuery(sql);
            
            DropPlayer(pl.Handle, $"[BAN] Check banlist: alamo-rp.com");
            Main.SaveLog("ban", $"Server ban {plban} | до выяснения 10y." );
        }
        
        protected static async void BlackListPlayerServerId([FromSource] Player pl, int id, string kickReason = "You have been kicked")
        {
            if (!User.IsAdmin(User.GetServerId(pl))) return;
            
            foreach (var player in new PlayerList())
            {
                if (User.GetServerId(player) != id) continue;
                
                int timeFormat = 5 * 365 * 60 * 60 * 24;
                
                Server.Sync.Data.Set(User.GetServerId(player), "date_ban", Main.GetTimeStamp() + timeFormat);

                await Delay(500);
                
                Save.UserAccount(player);
                
                //string sql1 = "UPDATE users SET date_ban = '" + (Main.GetTimeStamp() + timeFormat) + "' where id = '" + (int) Server.Sync.Data.Get(User.GetServerId(pl), "id") + "'";
                //Appi.MySql.ExecuteQuery(sql1);

                string admin = (string) Server.Sync.Data.Get(User.GetServerId(pl), "rp_name");
                string plban = (string) Server.Sync.Data.Get(User.GetServerId(player), "rp_name");
                
                string sql = "INSERT INTO ban_list (ban_from, ban_to, count, datetime, format, reason) VALUES ('" + admin + "','" + plban + "','10','" + Main.GetTimeStamp() + "','y.','" + kickReason + " + BlackList')";
                Appi.MySql.ExecuteQuery(sql);
                
                sql = "INSERT INTO black_list (steam, lic, live, xbl, guid, reason) VALUES ('" + player.Identifiers["steam"] + "','" + player.Identifiers["license"] + "','" + player.Identifiers["live"] + "','" + player.Identifiers["xbl"] + "','" + GetPlayerGuid(player.Handle) + "','" + kickReason + "')";
                Appi.MySql.ExecuteQuery(sql);
                
                DropPlayer(player.Handle, $"[BLACK LIST] {kickReason}");
                Main.SaveLog("blacklist", $"{admin} ban {plban} | {kickReason} " );
                return;
            }
        }
        
        protected static void SendLog(string file, string log)
        {
            Main.SaveLog(file, log);
        }
    
        protected static void SaveUserAccount([FromSource] Player player)
        {
            Save.UserAccount(player);
        }
    
        protected static void SaveVehicle(int vehid)
        {
            Save.UserVehicleById(vehid);
        }
    
        protected static void SendPlayerSubTitle(string text, int serverId)
        {
            foreach (var player in new PlayerList())
            {
                if (User.GetServerId(player) != serverId) continue;
                
                User.SendPlayerSubTitle(player, text);
                return;
            }
        }
    
        protected static void SendPlayerShowPass([FromSource] Player player, int serverId)
        {
            foreach (var pl in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(pl))) continue;
                if (User.GetServerId(pl) != serverId) continue;

                var data = new Dictionary<string, string>();
                
                int years = (int) Server.Sync.Data.Get(User.GetServerId(player), "age") - 18;
                int month = (int) Server.Sync.Data.Get(User.GetServerId(player), "exp_age") / 31;
                
                data.Add("CardID", Server.Sync.Data.Get(User.GetServerId(player), "id").ToString());
                data.Add("Имя", Server.Sync.Data.Get(User.GetServerId(player), "rp_name").ToString());
                data.Add("Проживает в штате", $"{years}г. {month}мес.");
                data.Add("Регистрация", User.GetRegStatusName(User.GetServerId(player)));
                
                if (User.GetVipStatus(User.GetServerId(player)) == "YouTube")
                    data.Add("Особый статус", "блогер");
                    
                User.SendToPlayerMenu(pl, Server.Sync.Data.Get(User.GetServerId(player), "rp_name"), "Документы", data);
                return;
            }
        }
    
        protected static void SendPlayerShowPassByHacker([FromSource] Player player, int id)
        {
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM users WHERE id = '" + id + "'").Rows)
            {
                var data = new Dictionary<string, string>();
                
                int years = (int) row["age"] - 18;
                int month = (int) row["exp_age"] / 31;
                
                data.Add("ID", row["id"].ToString());
                data.Add("Имя", row["rp_name"].ToString());
                data.Add("Проживает в штате", $"{years}г. {month}мес.");
                data.Add("Номер телефона", $"{row["phone_code"]}-{row["phone"]}");
                data.Add("Банковская карта", $"{row["bank_prefix"]}-{row["bank_number"]}");

                if ((int) Server.Sync.Data.Get(User.GetServerId(player), "mp0_watchdogs") > 95 || User.IsSapd(User.GetServerId(player)))
                {
                    data.Add("Права категории А", (bool) row["a_lic"] ? "есть" : "~r~нет");
                    data.Add("Права категории B", (bool) row["b_lic"] ? "есть" : "~r~нет");
                    data.Add("Права категории C", (bool) row["c_lic"] ? "есть" : "~r~нет");
                    data.Add("Авиа лицензия", (bool) row["air_lic"] ? "есть" : "~r~нет");
                    data.Add("Лицензия на водный транспорт", (bool) row["ship_lic"] ? "есть" : "~r~нет");
                    data.Add("Лицензия на оружие", (bool) row["gun_lic"] ? "есть" : "~r~нет");
                    data.Add("Лицензия таксиста", (bool) row["taxi_lic"] ? "есть" : "~r~нет");
                    data.Add("Лицензия адвоката", (bool) row["law_lic"] ? "есть" : "~r~нет");
                    data.Add("Лицензия бизнес", (bool) row["biz_lic"] ? "есть" : "~r~нет");
                    data.Add("Разрешение на охоту", (bool) row["animal_lic"] ? "есть" : "~r~нет");
                    data.Add("Разрешение на рыболовство", (bool) row["fish_lic"] ? "есть" : "~r~нет");
                    data.Add("Мед. страховка", (bool) row["med_lic"] ? "есть" : "~r~нет");
                    data.Add("Рецепт марихуаны", (bool) row["allow_marg"] ? "есть" : "~r~нет");
                    
                    if ((int) Server.Sync.Data.Get(User.GetServerId(player), "mp0_watchdogs") > 98 || User.IsSapd(User.GetServerId(player)))
                    {
                        data.Add("Денег в банке", $"{row["money_bank"]}");
                        data.Add("Розыск", $"{((int) row["wanted_level"] > 0 ? "Да" : "Нет")}");
                        if ((int) row["wanted_level"] > 0)
                            data.Add("Причина розыска", $"{row["wanted_reason"]}");
                    }
                }
                    
                User.SendToPlayerMenu(player, (string) row["rp_name"], "Информация", data);
                return;
            }
        }
    
        protected static void SendPlayerShowPassByHackerByPhone([FromSource] Player player, int phoneCode, int phoneNumber)
        {
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM users WHERE phone_code = '" + phoneCode + "' AND phone = '" + phoneNumber + "'").Rows)
            {
                var data = new Dictionary<string, string>();
                
                int years = (int) row["age"] - 18;
                int month = (int) row["exp_age"] / 31;
                
                data.Add("ID", row["id"].ToString());
                data.Add("Имя", row["rp_name"].ToString());
                data.Add("Проживает в штате", $"{years}г. {month}мес.");
                data.Add("Номер телефона", $"{row["phone_code"]}-{row["phone"]}");
                data.Add("Банковская карта", $"{row["bank_prefix"]}-{row["bank_number"]}");

                if ((int) Server.Sync.Data.Get(User.GetServerId(player), "mp0_watchdogs") > 95 || (User.IsSapd(User.GetServerId(player)) || User.IsSheriff(User.GetServerId(player))))
                {
                    data.Add("Права категории А", (bool) row["a_lic"] ? "есть" : "~r~нет");
                    data.Add("Права категории B", (bool) row["b_lic"] ? "есть" : "~r~нет");
                    data.Add("Права категории C", (bool) row["c_lic"] ? "есть" : "~r~нет");
                    data.Add("Авиа лицензия", (bool) row["air_lic"] ? "есть" : "~r~нет");
                    data.Add("Лицензия на водный транспорт", (bool) row["ship_lic"] ? "есть" : "~r~нет");
                    data.Add("Лицензия на оружие", (bool) row["gun_lic"] ? "есть" : "~r~нет");
                    data.Add("Лицензия таксиста", (bool) row["taxi_lic"] ? "есть" : "~r~нет");
                    data.Add("Лицензия адвоката", (bool) row["law_lic"] ? "есть" : "~r~нет");
                    data.Add("Мед. страховка", (bool) row["med_lic"] ? "есть" : "~r~нет");
                    data.Add("Рецепт марихуаны", (bool) row["allow_marg"] ? "есть" : "~r~нет");
                    
                    if ((int) Server.Sync.Data.Get(User.GetServerId(player), "mp0_watchdogs") > 98 || (User.IsSapd(User.GetServerId(player)) || User.IsSheriff(User.GetServerId(player))))
                    {
                        data.Add("Денег в банке", $"{row["money_bank"]}");
                        data.Add("Розыск", $"{((int) row["wanted_level"] > 0 ? "Да" : "Нет")}");
                        if ((int) row["wanted_level"] > 0)
                            data.Add("Причина розыска", $"{row["wanted_reason"]}");
                    }
                }
                    
                User.SendToPlayerMenu(player, (string) row["rp_name"], "Информация", data);
                return;
            }
        }
    
        protected static void SendPlayerMembers([FromSource] Player player)
        {
            int fractionId = Server.Sync.Data.Get(User.GetServerId(player), "fraction_id");
            if (fractionId == 0)
                return;
            
            var data = new Dictionary<string, string>();

            foreach (DataRow row in Appi.MySql
                .ExecuteQueryWithResult("SELECT rp_name, rank, is_online, last_login, tag FROM users WHERE fraction_id = " + fractionId +
                                        " ORDER BY is_online DESC, rank DESC").Rows)
            {
                if ((bool) row["is_online"])
                    data.Add("~g~*~s~ " + (string) row["rp_name"], Main.GetRankName(fractionId, (int) row["rank"]) + "\n" + (string) row["tag"]);
                else
                    data.Add("~r~*~s~ " + (string) row["rp_name"], Main.GetRankName(fractionId, (int) row["rank"]) + "\n" + Main.UnixTimeStampToDateTime((int) row["last_login"]) + " (msk)");
            }
            
            User.SendToPlayerMembersMenu(player, data);
        }
    
        protected static void SendPlayersList([FromSource] Player player)
        {
            var data = new Dictionary<string, string>();
            data.Add("~b~======[Лидеры]=========", "");

            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT id, rp_name, fraction_id, rank, is_online, last_login, phone, phone_code FROM users WHERE rank = 14 ORDER BY is_online DESC, fraction_id DESC").Rows)
            {
                string fractionName = Main.GetFractionName((int) row["fraction_id"]);
                string phone = (int) row["phone"] + "-" + (int) row["phone_code"];
                if ((bool) row["is_online"])
                    data.Add("~g~*~s~ " + (string) row["rp_name"] + " (" + (int) row["id"] + ")", fractionName + "\n" + phone);
                else
                    data.Add("~r~*~s~ " + (string) row["rp_name"] + " (" + (int) row["id"] + ")", fractionName + "\n" + phone);
            }

            data.Add("~b~======[Хелперы]=========", "");
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT id, rp_name, is_online, phone, phone_code FROM users WHERE helper_level > 0 AND admin_level = 0 ORDER BY is_online DESC, helper_level DESC").Rows)
            {
                string phone = (int) row["phone"] + "-" + (int) row["phone_code"];
                if ((bool) row["is_online"])
                    data.Add("~g~*~s~ " + (string) row["rp_name"] + " (" + (int) row["id"] + ")", phone);
                else
                    data.Add("~r~*~s~ " + (string) row["rp_name"] + " (" + (int) row["id"] + ")", phone);
            }

            data.Add("~b~======[Админы]=========", "");
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT id, rp_name, is_online, phone, phone_code FROM users WHERE admin_level > 0 AND admin_level < 5  ORDER BY is_online DESC, admin_level DESC").Rows)
            {
                string phone = (int) row["phone"] + "-" + (int) row["phone_code"];
                if ((bool) row["is_online"])
                    data.Add("~g~*~s~ " + (string) row["rp_name"] + " (" + (int) row["id"] + ")", phone);
                else
                    data.Add("~r~*~s~ " + (string) row["rp_name"] + " (" + (int) row["id"] + ")", phone);
            }
            
            User.SendToPlayersListMenu(player, data);
        }
    
        protected static void SendPlayerMembers2([FromSource] Player player)
        {
            int fractionId = Server.Sync.Data.Get(User.GetServerId(player), "fraction_id2");
            if (fractionId == 0)
                return;
            
            var data = new Dictionary<string, string>();

            foreach (DataRow row in Appi.MySql
                .ExecuteQueryWithResult("SELECT rp_name, rank2, is_online, last_login, tag FROM users WHERE fraction_id2 = " + fractionId +
                                        " ORDER BY is_online DESC, rank DESC").Rows)
            {
                string rankName = (int) row["rank2"] >= 11 ? "Основатель" : $"Ранг {row["rank2"]}";
                if ((bool) row["is_online"])
                    data.Add("~g~*~s~ " + (string) row["rp_name"], rankName + "\n" + (string) row["tag"]);
                else
                    data.Add("~r~*~s~ " + (string) row["rp_name"], rankName + "\n" + Main.UnixTimeStampToDateTime((int) row["last_login"]) + " (msk)");
            }
            
            User.SendToPlayerMembersMenu2(player, data);
        }
    
        protected static void SendPlayerBankClientInfo([FromSource] Player player, int prefix)
        {
            var data = new Dictionary<string, string>();
            
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT COUNT(*) as clients, SUM(money_bank) as money FROM users WHERE bank_prefix = " + prefix).Rows)
            {
                data.Add("Клиентов", row["clients"].ToString());
                data.Add("Капитал", $"${Convert.ToInt32(row["money"]):#,#}");
            }
            
            User.SendToPlayerBankInfoMenu(player, data);
        }
    
        protected static void SendPlayerVehicleLog([FromSource] Player player)
        {
            int fractionId = Server.Sync.Data.Get(User.GetServerId(player), "fraction_id");
            if (fractionId == 0)
                return;
   
            int i = 0;
            var data = Appi.MySql.ExecuteQueryWithResult("SELECT * FROM log_fraction_vehicle WHERE fraction_id = " + fractionId + " ORDER BY id DESC LIMIT 100").Rows.Cast<DataRow>().ToDictionary(row => ++i + ") " + (string) row["name"] + "|" + Main.UnixTimeStampToDateTimeShort((int) row["timestamp"]), row => (string) row["do"]);
            User.SendToPlayerLogVehicleMenu(player, data);
        }
    
        protected static void SendPlayerGunLog([FromSource] Player player)
        {
            int fractionId = (int) Server.Sync.Data.Get(User.GetServerId(player), "fraction_id");
            if (fractionId == 0)
                return;

            int i = 0;
            var data = Appi.MySql.ExecuteQueryWithResult("SELECT * FROM log_fraction_gun WHERE fraction_id = " + fractionId + " ORDER BY id DESC LIMIT 100").Rows.Cast<DataRow>().ToDictionary(row => ++i + ") " + (string) row["name"] + "|" + Main.UnixTimeStampToDateTimeShort((int) row["timestamp"]), row => (string) row["do"]);
            User.SendToPlayerLogGunMenu(player, data);
        }
    
        protected static void SendPlayerStockLog([FromSource] Player player, int stockId)
        {
            int i = 0;
            var data = Appi.MySql.ExecuteQueryWithResult("SELECT * FROM log_stock WHERE stock_id = " + stockId + " ORDER BY id DESC LIMIT 100").Rows.Cast<DataRow>().ToDictionary(row => ++i + ") " + (string) row["name"] + "|" + Main.UnixTimeStampToDateTimeShort((int) row["timestamp"]), row => (string) row["do"]);
            User.SendToPlayerLogGunMenu(player, data);
        }
    
        protected static void TransferMoneyBank([FromSource] Player player, int prefix, int number, int sum)
        {
            var thisPlayerNumber = (int) Server.Sync.Data.Get(User.GetServerId(player), "bank_number");
            var thisPlayerPrefix = (int) Server.Sync.Data.Get(User.GetServerId(player), "bank_prefix");
            int sumForBiz = Convert.ToInt32(sum * 0.005);
            
            Main.SaveLog("BizzTransfer", $"{prefix}-{number}-{sum}-" + sumForBiz);

            foreach (var p in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(p))) continue;
                if ((int) Server.Sync.Data.Get(User.GetServerId(p), "bank_number") == number &&
                    (int) Server.Sync.Data.Get(User.GetServerId(p), "bank_prefix") == prefix)
                {
                    User.RemoveBankMoney(player, sum);
                    
                    switch (prefix)
                    {
                        case 2222:
                            Business.AddMoney(1, sumForBiz);
                            break;
                        case 3333:
                            Business.AddMoney(2, sumForBiz);
                            break;
                        case 4444:
                            Business.AddMoney(108, sumForBiz);
                            break;
                        default:
                            Coffer.AddMoney(sumForBiz);
                            break;
                    }
                    switch (thisPlayerPrefix)
                    {
                        case 2222:
                            Business.AddMoney(1, sumForBiz);
                            break;
                        case 3333:
                            Business.AddMoney(2, sumForBiz);
                            break;
                        case 4444:
                            Business.AddMoney(108, sumForBiz);
                            break;
                        default:
                            Coffer.AddMoney(sumForBiz);
                            break;
                    }
                    
                    sum = Convert.ToInt32(sum * 0.99); 
                    
                    User.AddBankMoney(p, sum);
                    
                    Main.SaveLog("GiveBank", $"ID FROM {Server.Sync.Data.Get(User.GetServerId(player), "rp_name")} TO {Server.Sync.Data.Get(User.GetServerId(p), "rp_name")} ${sum}");
                    User.UpdateAllData(player);
                    User.UpdateAllData(p);
                    
                    switch (prefix)
                    {
                        case 2222:
                            TriggerClientEvent(p, "ARP:SendPlayerNotificationPicture", $"Зачисление: ${sum:#,#}", "~g~Fleeca~s~ Bank", "Операция со счётом", "CHAR_BANK_FLEECA", 2);
                            break;
                        case 3333:
                            TriggerClientEvent(p, "ARP:SendPlayerNotificationPicture", $"Зачисление: ${sum:#,#}", "~b~Blaine~s~ Bank", "Операция со счётом", "CHAR_STEVE_TREV_CONF", 2);
                            break;
                        case 4444:
                            TriggerClientEvent(p, "ARP:SendPlayerNotificationPicture", $"Зачисление: ${sum:#,#}", "~o~Pacific~s~ Bank", "Операция со счётом", "CHAR_STEVE_MIKE_CONF", 2);
                            break;
                        default:
                            TriggerClientEvent(p, "ARP:SendPlayerNotificationPicture", $"Зачисление: ${sum:#,#}", "~r~Maze~s~ Bank", "Операция со счётом", "CHAR_BANK_MAZE", 2);
                            break;
                    }
                    
                    switch (thisPlayerPrefix)
                    {
                        case 2222:
                            TriggerClientEvent(player, "ARP:SendPlayerNotificationPicture", $"Переведено: ${sum:#,#}", "~g~Fleeca~s~ Bank", "Операция со счётом", "CHAR_BANK_FLEECA", 2);
                            break;
                        case 3333:
                            TriggerClientEvent(player, "ARP:SendPlayerNotificationPicture", $"Переведено: ${sum:#,#}", "~b~Blaine~s~ Bank", "Операция со счётом", "CHAR_STEVE_TREV_CONF", 2);
                            break;
                        case 4444:
                            TriggerClientEvent(player, "ARP:SendPlayerNotificationPicture", $"Переведено: ${sum:#,#}", "~o~Pacific~s~ Bank", "Операция со счётом", "CHAR_STEVE_MIKE_CONF", 2);
                            break;
                        default:
                            TriggerClientEvent(player, "ARP:SendPlayerNotificationPicture", $"Переведено: ${sum:#,#}", "~r~Maze~s~ Bank", "Операция со счётом", "CHAR_BANK_MAZE", 2);
                            break;
                    }
                    return;
                }
            }

            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM users WHERE bank_number = " + number + " AND bank_prefix = " + prefix).Rows)
            {
                User.RemoveBankMoney(player, sum);
                
                switch (prefix)
                {
                    case 2222:
                        Business.AddMoney(1, sumForBiz);
                        break;
                    case 3333:
                        Business.AddMoney(2, sumForBiz);
                        break;
                    case 4444:
                        Business.AddMoney(108, sumForBiz);
                        break;
                    default:
                        Coffer.AddMoney(sumForBiz);
                        break;
                }
                switch (thisPlayerPrefix)
                {
                    case 2222:
                        Business.AddMoney(1, sumForBiz);
                        break;
                    case 3333:
                        Business.AddMoney(2, sumForBiz);
                        break;
                    case 4444:
                        Business.AddMoney(108, sumForBiz);
                        break;
                    default:
                        Coffer.AddMoney(sumForBiz);
                        break;
                }
                
                sum = Convert.ToInt32(sum * 0.99); 
                
                switch (thisPlayerPrefix)
                {
                    case 2222:
                        TriggerClientEvent(player, "ARP:SendPlayerNotificationPicture", $"Списано: ${sum:#,#}", "~g~Fleeca~s~ Bank", "Операция со счётом", "CHAR_BANK_FLEECA", 2);
                        break;
                    case 3333:
                        TriggerClientEvent(player, "ARP:SendPlayerNotificationPicture", $"Списано: ${sum:#,#}", "~b~Blaine~s~ Bank", "Операция со счётом", "CHAR_STEVE_TREV_CONF", 2);
                        break;
                    case 4444:
                        TriggerClientEvent(player, "ARP:SendPlayerNotificationPicture", $"Списано: ${sum:#,#}", "~o~Pacific~s~ Bank", "Операция со счётом", "CHAR_STEVE_MIKE_CONF", 2);
                        break;
                    default:
                        TriggerClientEvent(player, "ARP:SendPlayerNotificationPicture", $"Списано: ${sum:#,#}", "~r~Maze~s~ Bank", "Операция со счётом", "CHAR_BANK_MAZE", 2);
                        break;
                }
                
                Main.SaveLog("GiveBankOffline", $"ID FROM {Server.Sync.Data.Get(User.GetServerId(player), "rp_name")} TO {(string) row["rp_name"]} ${sum}");
                
                Appi.MySql.ExecuteQuery("UPDATE users SET money_bank = '" + ((int) row["money_bank"] + sum) + "' where id = '" + (int) row["id"] + "'");
                User.UpdateAllData(player);
                return;
            }
            
            TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Счёт не найден");
        }
    
        protected static void SendReferrer([FromSource] Player player)
        {
            string referer = (string) Server.Sync.Data.Get(User.GetServerId(player), "referer");
            string rpName = (string) Server.Sync.Data.Get(User.GetServerId(player), "rp_name");
            if (String.IsNullOrEmpty(referer)) return;
            
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult($"SELECT * FROM users WHERE rp_name = '{referer}'").Rows)
            {
                int money = (int) row["money_donate"] + 100;
                Appi.MySql.ExecuteQuery($"UPDATE users SET money_donate = '{money}' WHERE rp_name ='{referer}'");
                Appi.MySql.ExecuteQuery($"INSERT INTO log_referrer (name, referrer, money, timestamp) VALUES ('{rpName}', '{referer}', '100', '{Main.GetTimeStamp()}')");
            }
        }
    
        protected static void ChangeNumberPhone([FromSource] Player player, int prefix, int number)
        {   
            if (Appi.MySql.ExecuteQueryWithResult("SELECT * FROM users WHERE phone = " + number + " AND phone_code = " + prefix).Rows.Count > 0)
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Номер телефона уже существует");
                return;
            }
            
            if (Appi.MySql.ExecuteQueryWithResult("SELECT * FROM items WHERE number = " + number + " AND prefix = " + prefix).Rows.Count > 0)
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Номер телефона уже существует");
                return;
            }

            if (User.GetCashMoney(player) < 100000)
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotification", Lang.GetTextToPlayer("_lang_15"));
                return;
            }
            
            User.RemoveCashMoney(player, 100000);

            switch (prefix)
            {
                case 777:
                    Business.AddMoney(92, 100000);
                    break;
                default:
                    Coffer.AddMoney(100000);
                    break;
            }
            
            Server.Sync.Data.Set(User.GetServerId(player), "phone", number);
            TriggerClientEvent(player, "ARP:SendPlayerNotification", "~g~Вы изменили номер телефона");
            Save.UserAccount(player);
            
            User.UpdateAllData(player);
        }
    
        protected static void ChangeNumberCard([FromSource] Player player, int prefix, int number)
        {   
            if (Appi.MySql.ExecuteQueryWithResult("SELECT * FROM users WHERE bank_number = " + number + " AND bank_prefix = " + prefix).Rows.Count > 0)
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Номер карты уже существует");
                return;
            }
            
            if (Appi.MySql.ExecuteQueryWithResult("SELECT * FROM items WHERE number = " + number + " AND prefix = " + prefix).Rows.Count > 0)
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Номер карты уже существует");
                return;
            }

            if (User.GetCashMoney(player) < 100000)
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotification", Lang.GetTextToPlayer("_lang_15"));
                return;
            }
            
            User.RemoveCashMoney(player, 100000);

            switch (prefix)
            {
                case 2222:
                    Business.AddMoney(1, 100000);
                    break;
                case 3333:
                    Business.AddMoney(2, 100000);
                    break;
                case 4444:
                    Business.AddMoney(108, 100000);
                    break;
                default:
                    Coffer.AddMoney(100000);
                    break;
            }
            
            Server.Sync.Data.Set(User.GetServerId(player), "bank_number", number);
            TriggerClientEvent(player, "ARP:SendPlayerNotification", "~g~Вы изменили номер карты");
            Save.UserAccount(player);
            
            User.UpdateAllData(player);
        }
    
        protected static void AddFractionGunLog([FromSource] Player player, string name, string text, int fractionId)
        {            
            Appi.MySql.ExecuteQuery($"INSERT INTO log_fraction_gun (name, do, fraction_id, timestamp) VALUES ('{name}', '{text}', '{fractionId}', '{Main.GetTimeStamp()}')");
        }
    
        protected static void AddFractionVehicleLog([FromSource] Player player, string name, string text, int fractionId)
        {            
            Appi.MySql.ExecuteQuery($"INSERT INTO log_fraction_vehicle (name, do, fraction_id, timestamp) VALUES ('{name}', '{text}', '{fractionId}', '{Main.GetTimeStamp()}')");
        }
    
        protected static void AddStockLog([FromSource] Player player, string name, string text, int stockId)
        {            
            Appi.MySql.ExecuteQuery($"INSERT INTO log_stock (name, do, stock_id, timestamp) VALUES ('{name}', '{text}', '{stockId}', '{Main.GetTimeStamp()}')");
        }
    
        protected static void OpenBusinnesListMenu([FromSource] Player player, int typeId)
        {
            var data = new Dictionary<string, string>();
            var data2 = new Dictionary<string, string>();

            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT name, user_name, id, interior FROM business WHERE type = " + typeId).Rows)
            {
                data.Add((string) row["id"].ToString(), (string) row["user_name"]);
                data2.Add((string) row["id"].ToString(), (string) row["name"]);
            }
            
            User.SendToPlayerBusinessListMenu(player, data, data2);
        }
    
        protected static void OpenApartamentListMenu([FromSource] Player player, int floor, int buildId)
        {
            
            var data = Appi.MySql.ExecuteQueryWithResult("SELECT user_name, id FROM apartment WHERE floor = " + floor + " AND build_id = " + buildId).Rows.Cast<DataRow>().ToDictionary(row => (int) row["id"], row => (string) row["user_name"]);
            User.SendToPlayerApartmentListMenu(player, data);
        }
    
        protected static void SendServerToPlayerJail(string reason, int wantedLevel, int serverId)
        {
            foreach (var pl in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(pl))) continue;
                if (User.GetServerId(pl) != serverId) continue;
                TriggerClientEvent(pl, "ARP:SendPlayerNotification", "~y~Вас арестовали~s~\n" + reason);
                TriggerClientEvent(pl, "ARP:JailPlayer", wantedLevel);
                return;
            }
        }
    
        protected static void SendPlayerShowDoc([FromSource] Player player, int serverId)
        {
            foreach (var pl in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(pl))) continue;
                if (User.GetServerId(pl) != serverId) continue;

                int plId = User.GetServerId(player);

                var data = new Dictionary<string, string>();
                
                data.Add("Права категории А", (bool) Server.Sync.Data.Get(plId, "a_lic") ? "есть" : "~r~нет");
                data.Add("Права категории B", (bool) Server.Sync.Data.Get(plId, "b_lic") ? "есть" : "~r~нет");
                data.Add("Права категории C", (bool) Server.Sync.Data.Get(plId, "c_lic") ? "есть" : "~r~нет");
                data.Add("Авиа лицензия", (bool) Server.Sync.Data.Get(plId, "air_lic") ? "есть" : "~r~нет");
                data.Add("Лицензия на водный транспорт", (bool) Server.Sync.Data.Get(plId, "ship_lic") ? "есть" : "~r~нет");
                data.Add("Лицензия на оружие", (bool) Server.Sync.Data.Get(plId, "gun_lic") ? "есть" : "~r~нет");
                data.Add("Лицензия таксиста", (bool) Server.Sync.Data.Get(plId, "taxi_lic") ? "есть" : "~r~нет");
                data.Add("Лицензия адвоката", (bool) Server.Sync.Data.Get(plId, "law_lic") ? "есть" : "~r~нет");
                data.Add("Мед. страховка", (bool) Server.Sync.Data.Get(plId, "med_lic") ? "есть" : "~r~нет");
                data.Add("Рецепт марихуаны", (bool) Server.Sync.Data.Get(plId, "allow_marg") ? "есть" : "~r~нет");
                    
                User.SendToPlayerMenu(pl, Server.Sync.Data.Get(User.GetServerId(player), "rp_name"), "Документы", data);
                return;
            }
        }
    
        protected static void SendPlayerShowGovDoc([FromSource] Player player, int serverId)
        {
            foreach (var pl in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(pl))) continue;
                if (User.GetServerId(pl) != serverId) continue;

                int plId = User.GetServerId(player);

                var data = new Dictionary<string, string>();
                
                data.Add("ID", Convert.ToString((int) Server.Sync.Data.Get(plId, "id")));
                data.Add("Имя", (string) Server.Sync.Data.Get(plId, "rp_name"));
                data.Add("Организация", Main.GetFractionName((int) Server.Sync.Data.Get(plId, "fraction_id")));
                data.Add("Должность", Main.GetRankName((int) Server.Sync.Data.Get(plId, "fraction_id"), (int) Server.Sync.Data.Get(plId, "rank")));
                data.Add("Отдел", ((string) Server.Sync.Data.Get(plId, "tag") == "" ? "~r~Нет" : (string) Server.Sync.Data.Get(plId, "tag")));
                    
                User.SendToPlayerMenu(pl, Server.Sync.Data.Get(User.GetServerId(player), "rp_name"), "Документы", data);
                return;
            }
        }
    
        protected static void SendPlayerTooltip(string text, int serverId)
        {
            foreach (var player in new PlayerList())
            {
                if (User.GetServerId(player) != serverId) continue;
                
                User.SendPlayerTooltip(player, text);
                return;
            }
        }
    
        protected static void UpdateCashDisplay(int serverId)
        {
            foreach (var player in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(player))) continue;
                if (User.GetServerId(player) != serverId) continue;
                TriggerClientEvent(player, "ARP:UpdateCashDisplay", (int) Server.Sync.Data.Get(serverId, "money"));
                return;
            }
        }
    
        protected static void SendSms([FromSource] Player player, string number, string text)
        {
            string numberFrom = Server.Sync.Data.Get(User.GetServerId(player), "phone_code") + "-" +
                                Server.Sync.Data.Get(User.GetServerId(player), "phone");
            
            foreach (var pl in new PlayerList())
            {
                string phoneNumber = Server.Sync.Data.Get(User.GetServerId(pl), "phone_code") + "-" +
                                     Server.Sync.Data.Get(User.GetServerId(pl), "phone");
                if (phoneNumber != number) continue;
                
                TriggerClientEvent(pl, "ARP:AddNewSms", numberFrom, text);
            }

            Appi.MySql.ExecuteQuery("INSERT INTO phone_sms (number_from, number_to, text, datetime) " +
                                    $"VALUES ('{numberFrom}', '{number}', '{text}', '{Weather.GetRpDateTime()}')");
            
            TriggerClientEvent(player, "ARP:SendPlayerNotification", "~g~Отправлено~s~\nСМС была отправлена получателю");
        }
    
        protected static void OpenSmsListMenu([FromSource] Player player, string number)
        {
            var data = new Dictionary<string, string>();
            var data2 = new Dictionary<string, string>();
            var data3 = new Dictionary<string, string>();
            var data4 = new Dictionary<string, string>();

            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult($"SELECT * FROM phone_sms WHERE number_from = '{number}' OR number_to = '{number}' ORDER BY id DESC LIMIT 100").Rows)
            {
                data.Add(((int) row["id"]).ToString(), (string) row["number_from"]);
                data2.Add(((int) row["id"]).ToString(), (string) row["number_to"]);
                data3.Add(((int) row["id"]).ToString(), (string) row["datetime"]);
                data4.Add(((int) row["id"]).ToString(), (string) row["text"]);
            }
            
            User.SendToPlayerSmsListMenu(player, data, data2, data3, data4, number);
        }
    
        protected static void OpenContacntListMenu([FromSource] Player player, string number)
        {
            var data = new Dictionary<string, string>();
            var data2 = new Dictionary<string, string>();

            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult($"SELECT * FROM phone_contact WHERE number = '{number}' ORDER BY title ASC LIMIT 100").Rows)
            {
                data.Add(((int) row["id"]).ToString(), (string) row["title"]);
                data2.Add(((int) row["id"]).ToString(), (string) row["text_number"]);
            }
            
            User.SendToPlayerContacntListMenu(player, data, data2, number);
        }
    
        protected static void AddContact([FromSource] Player player, string phone, string title, string num)
        {
            Appi.MySql.ExecuteQuery($"INSERT INTO phone_contact (number, title, text_number) VALUES ('{phone}', '{title}', '{num}');");
            TriggerClientEvent(player, "ARP:SendPlayerNotification", "~g~Контакт был добавлен");
        }
    
        protected static void DeleteContact([FromSource] Player player, int id)
        {
            Appi.MySql.ExecuteQuery($"DELETE FROM phone_contact WHERE id = '{id}'");
            TriggerClientEvent(player, "ARP:SendPlayerNotification", "~g~Контакт был удален");
        }
    
        protected static void OpenSmsInfoMenu([FromSource] Player player, int id)
        {
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult($"SELECT * FROM phone_sms WHERE id = '{id}'").Rows)
            {            
                TriggerClientEvent(player, "ARP:SendToPlayerSmsInfoMenu", (int) row["id"], (string) row["number_from"], (string) row["number_to"], (string) row["text"], (string) row["datetime"]);
                return;
            }
        }
    
        protected static void OpenContInfoMenu([FromSource] Player player, int id)
        {
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult($"SELECT * FROM phone_contact WHERE id = '{id}'").Rows)
            {            
                TriggerClientEvent(player, "ARP:SendToPlayerContInfoMenu", (int) row["id"], (string) row["title"], (string) row["text_number"]);
                return;
            }
        }
    
        protected static void DeleteSms([FromSource] Player player, int id)
        {
            Appi.MySql.ExecuteQuery($"DELETE FROM phone_sms WHERE id = '{id}'");
            TriggerClientEvent(player, "ARP:SendPlayerNotification", "~g~Смс была удалена");
        }
        
        protected static async void Register([FromSource] Player player, string name, string surname, string password, string email, string referer, bool acceptRules)
        {
            if (User.IsLogin(User.GetServerId(player)))
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~ОШИБКА~s~\nВы уже вошли в систему");
                return;
            }

            if (!acceptRules)
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~ОШИБКА~s~\nВы не согласились с правилами проекта");
                return;
            }

            if (User.DoesNameAccountExist(name + " " + surname))
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~ОШИБКА~s~\nАккаунт уже с таким RP именем уже существует");
                return;
            }

            if (User.Does3AccountExist(player))
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~ОШИБКА~s~\nНельзя иметь более 3 аккаунтов");
                return;
            }

            User.CreatePlayerAccount(player, password, name + " " + surname, email, referer);

            await Delay(1500);
            
            User.LoadAccount(player, name + " " + surname);
        }
        
        protected static void Login([FromSource] Player player, string name, string pass)
        {
            int id = User.GetServerId(player);
            
            if (User.IsLogin(id))
                return;

            var tryLogin = User.TryLogin(name, pass);

            if (tryLogin == -1)
            {
                string guid = GetPlayerGuid(player.Handle);
                string license = player.Identifiers["license"];
                Main.SaveLog("PasswordFailed", $"[{player.EndPoint}] [{license}] [{guid}] {name}");
                TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~ОШИБКА~s~\nОшибка пароля или аккаунт еще не был создан");
                return;
            }

            if (tryLogin > 10)
            {
                DropPlayer(player.Handle, "Ban: " + Main.UnixTimeStampToDateTime(tryLogin) + " (msk)");
                return;
            }

            try
            {
                User.LoadAccount(player, name);
            }
            catch (Exception e)
            {
                string guid = GetPlayerGuid(player.Handle);
                string license = player.Identifiers["license"];
                Main.SaveLog("ExceptionAuth", $"[{player.EndPoint}] [{license}] [{guid}] {name}");
            }

        }
        
        private static async Task DataBaseSync()
        {
            await Delay(10000);
            try
            {
                foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM event_to_server").Rows)
                {
                    int id = (int) row["id"];
                    var param = (JObject) Main.FromJson(row["params"].ToString().Replace("&quot;", "\""));
                    int type = (int) row["type"];
                    int itemId = (int) row["item_id"];
                    string action = (string) row["action"];
    
                    if (type == EventTypes.Other)
                    {
                        /*switch (action)
                        {
                            case "ExecuteCommand":
                                ExecuteCommand((string) GetParam(param, "msg"));
                                break;
                            case "StopResoruce":
                                StopResource((string) GetParam(param, "msg"));
                                break;
                            case "StartResoruce":
                                StartResource((string) GetParam(param, "msg"));
                                break;
                            case "RestartResoruce":
                                StopResource((string) GetParam(param, "msg"));
                                await Delay(5000);
                                StartResource((string) GetParam(param, "msg"));
                                break;
                        }*/
                    }
                    else if (type == EventTypes.Business)
                    {
                        switch (action)
                        {
                            case "AddMoney":
                                int bId = (int) GetParam(param, "bId");
                                int money = (int) GetParam(param, "money");
                                
                                if (bId == 0)
                                    break;
                                if (money == 0)
                                    break;
                                
                                Business.AddMoney(bId, money);
                                break;
                        }
                    }
                    else if (type == EventTypes.Player)
                    {
                        var player = User.GetPlayerById(itemId);
                        
                        switch (action)
                        {
                            case "Car":
                            {
                                TriggerClientEvent("ARP:GetCar", (string) GetParam(param, "car"));
                                break;
                            }
                            case "GiveAllWeapon":
                            {
                                TriggerClientEvent("ARP:GiveAllWeapon");
                                break;
                            }
                            case "KickAll":
                            {
                                foreach (var p in new PlayerList())
                                    p.Drop("Restart");
                                break;
                            }
                            case "AddMoney":
                            {
                                if (player == default(Player)) continue;
                                int money = (int) GetParam(param, "money");
                                if (money == 0)
                                    break;
                                User.AddBankMoney(player, money);
                                break;
                            }
                            case "SellMotorVehicle":
                            {
                                string uName = (string) GetParam(param, "uName");
                                int uId = (int) GetParam(param, "uId");
                                int vId = (int) GetParam(param, "vId");
                                
                                int price = (int) GetParam(param, "price");
                                
                                if (uId == 0)
                                    break;
                                if (vId == 0)
                                    break;
                                if (price == 0)
                                    break;
                                
                                UpdateSellCarInfo(player, uName, uId, vId);
                                
                                UpdateSellCarInfo(player, uName, uId, vId);
                                Main.SaveLog("BuyMotorCar", $"ID: {vId}, SELL USER: {itemId}, BUY USER: {uId}, PRICE: {price}");
    
                                if (player != default(Player))
                                {
                                    int plServerId = User.GetServerId(player);
                                    
                                    for (var i = 1; i < 9; i++)
                                    {
                                        if ((int) Server.Sync.Data.Get(plServerId, "car_id" + i) == vId)
                                            Server.Sync.Data.Set(plServerId, "car_id" + i, 0);
                                    }
                                    User.AddBankMoney(player, Convert.ToInt32(price * 0.95));
                                    User.UpdateAllData(player);
                                    Save.UserAccount(player);
                                    TriggerClientEvent(player, "ARP:SendPlayerNotificationPicture", $"Вы продали своё ТС по цене: ~g~${price:#,#}", "Premium Deluxe Motorsport", "Уведомление", "CHAR_SIMEON", 2);
                                }
                                else
                                {
                                    Appi.MySql.ExecuteQuery("UPDATE users SET money_bank = money_bank + '" + Convert.ToInt32(price * 0.95) + "', car_id1 = '0' WHERE car_id1 = '" + vId + "' AND id = '" + itemId + "'");
                                    Appi.MySql.ExecuteQuery("UPDATE users SET money_bank = money_bank + '" + Convert.ToInt32(price * 0.95) + "', car_id2 = '0' WHERE car_id2 = '" + vId + "' AND id = '" + itemId + "'");
                                    Appi.MySql.ExecuteQuery("UPDATE users SET money_bank = money_bank + '" + Convert.ToInt32(price * 0.95) + "', car_id3 = '0' WHERE car_id3 = '" + vId + "' AND id = '" + itemId + "'");
                                    Appi.MySql.ExecuteQuery("UPDATE users SET money_bank = money_bank + '" + Convert.ToInt32(price * 0.95) + "', car_id4 = '0' WHERE car_id4 = '" + vId + "' AND id = '" + itemId + "'");
                                    Appi.MySql.ExecuteQuery("UPDATE users SET money_bank = money_bank + '" + Convert.ToInt32(price * 0.95) + "', car_id5 = '0' WHERE car_id5 = '" + vId + "' AND id = '" + itemId + "'");
                                    Appi.MySql.ExecuteQuery("UPDATE users SET money_bank = money_bank + '" + Convert.ToInt32(price * 0.95) + "', car_id6 = '0' WHERE car_id6 = '" + vId + "' AND id = '" + itemId + "'");
                                    Appi.MySql.ExecuteQuery("UPDATE users SET money_bank = money_bank + '" + Convert.ToInt32(price * 0.95) + "', car_id7 = '0' WHERE car_id7 = '" + vId + "' AND id = '" + itemId + "'");
                                    Appi.MySql.ExecuteQuery("UPDATE users SET money_bank = money_bank + '" + Convert.ToInt32(price * 0.95) + "', car_id8 = '0' WHERE car_id8 = '" + vId + "' AND id = '" + itemId + "'");
                                }

                                Appi.MySql.ExecuteQuery("DELETE FROM event_to_server WHERE id = '" + id + "'");
                                Business.AddMoney(86, Convert.ToInt32(price * 0.05));
                                break;
                            }
                            case "Kick":
                                if (player == default(Player)) continue;
                                player.Drop((string) GetParam(param, "msg"));
                                break;
                            case "BuyVehicle":
                            {
                                Appi.MySql.ExecuteQuery("DELETE FROM event_to_server WHERE id = '" + id + "'");
                                int vId = (int) GetParam(param, "vId");
                                int price = (int) GetParam(param, "price");
                                int slot = (int) GetParam(param, "slot");
                               
                                int plServerId = User.GetServerId(player);
                                if (player == default(Player)) 
                                {
                                    foreach(var p in new PlayerList())
                                    {
                                        var i = User.GetServerId(p);
                                        if (Server.Sync.Data.Has(i, "id") && Server.Sync.Data.Get(i, "id") == itemId)
                                        {
                                            Server.Sync.Data.Set(i, "car_id" + slot, vId);
                                            foreach (DataRow carRow in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM cars WHERE id = " + vId).Rows)
                                                Managers.Vehicle.AddUserVehicle(carRow);
                                            UpdateSellCarInfo(p, (string) Server.Sync.Data.Get(i, "rp_name"), itemId, vId);

                                            return;
                                        }
                                    }
                                    break;
                                }
                                try
                                {
                                    if (User.GetBankMoney(player) < price)
                                    {
                                        Appi.MySql.ExecuteQuery("UPDATE cars SET user_name = '', id_user = '0' WHERE id = '" + vId + "'");
                                        User.SendPlayerTooltip(player,"~r~У вас не достаточно денег на банковском счету");
                                        return;
                                    }
                                    
                                    TriggerClientEvent(player, "ARP:HideMenu");
                                    User.RemoveBankMoney(player, price);
                                    Coffer.AddMoney(price);
                                    User.UpdateAllData(player);
                                    Server.Sync.Data.Set(User.GetServerId(player), "car_id" + slot, vId);
                                    foreach (DataRow carRow in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM cars WHERE id = " + vId).Rows)
                                        Managers.Vehicle.AddUserVehicle(carRow);
                                    UpdateSellCarInfo(player, (string) Server.Sync.Data.Get(plServerId, "rp_name"), itemId, vId);
                                   
                                    User.SendPlayerTooltip(player, "~g~Поздравляем с покупкой транспорта");
                                    
                                    Save.UserAccount(player);
                                    Main.SaveLog("BuyCar",$"ID: {vId}, BUY NAME: {(string) Server.Sync.Data.Get(plServerId, "rp_name")}, PRICE: {price}");

                                    break;
                                }
                                catch (Exception e)
                                {
                                    Main.SaveLog("BuyVehicleEx", e.ToString());
                                    break;
                                }
                            }
                        }
                    }
                    DeleteEventItem(id);
                }
            }
            catch (Exception e)
            {
                Main.SaveLog("Ex", e.ToString());
            }
        }
        
        private static void DeleteEventItem(int id)
        {
            Appi.MySql.ExecuteQuery($"DELETE FROM event_to_server WHERE id = '{id}'");
        }
        
        private static dynamic GetParam(JObject param, string key)
        {
            return param.ContainsKey(key) ? param[key] : null;
        }
    }
}
public class EventTypes
{
    public static int Other => 0;
    public static int Player => 1;
    public static int Vehicle => 2;
    public static int Business => 3;
    public static int House => 4;
    public static int Apartment => 5;
    public static int Bag => 6;
    public static int Stock => 7;
}