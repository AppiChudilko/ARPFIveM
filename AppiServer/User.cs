using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CitizenFX.Core;
using Server.Managers;
using static CitizenFX.Core.Native.API;

namespace Server
{
    public class User : BaseScript
    {
        public static Dictionary<string, int> PlayerVirtualWorldList = new Dictionary<string, int>();
        public static Dictionary<string, int> PlayerIdList = new Dictionary<string, int>();

        public static void LoadAccount(Player player, string rpName)
        {
            string selectSql = "id, is_online";
            foreach (var property in typeof(PlayerData).GetProperties())
            {
                if (property.Name == "car_id1_key") continue;
                if (property.Name == "car_id2_key") continue;
                if (property.Name == "car_id3_key") continue;
                if (property.Name == "car_id4_key") continue;
                if (property.Name == "car_id5_key") continue;
                if (property.Name == "car_id6_key") continue;
                if (property.Name == "car_id7_key") continue;
                if (property.Name == "car_id8_key") continue;
                if (property.Name == "is_auth") continue;
                if (property.Name == "id") continue;
                selectSql += ", " + property.Name;
            }
            
            
            rpName = Main.DeleteSqlHack(rpName);
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT " + selectSql + " FROM users WHERE rp_name = '" + rpName + "'").Rows)
            {
                PlayerSkin playerSkin = Main.FromJson((string)row["skin"]).ToObject<PlayerSkin>();

                if ((bool) row["is_online"])
                {
                    player.Drop("Account already authorized");
                    return;
                }

                var playerObj = new PlayerData()
                {
                    id = (int) row["id"],
                    name = (string) row["name"],
                    lic = (string) player.Identifiers["license"],
                    password = (string) row["password"],
                    skin = (string) row["skin"],
                    money = (int) row["money"],
                    money_bank = (int) row["money_bank"],
                    money_payday = (int) row["money_payday"],
                    money_tax = (int) row["money_tax"],
                    posob = (bool) row["posob"],
                    id_house = (int) row["id_house"],
                    apartment_id = (int) row["apartment_id"],
                    business_id = (int) row["business_id"],
                    condo_id = (int) row["condo_id"],
                    stock_id = (int) row["stock_id"],
                    
                    car_id1 = (int) row["car_id1"],
                    car_id2 = (int) row["car_id2"],
                    car_id3 = (int) row["car_id3"],
                    car_id4 = (int) row["car_id4"],
                    car_id5 = (int) row["car_id5"],
                    car_id6 = (int) row["car_id6"],
                    car_id7 = (int) row["car_id7"],
                    car_id8 = (int) row["car_id8"],
                    
                    car_id1_key = (int) row["car_id1"],
                    car_id2_key = (int) row["car_id2"],
                    car_id3_key = (int) row["car_id3"],
                    car_id4_key = (int) row["car_id4"],
                    car_id5_key = (int) row["car_id5"],
                    car_id6_key = (int) row["car_id6"],
                    car_id7_key = (int) row["car_id7"],
                    car_id8_key = (int) row["car_id8"],
                    
                    reg_status = (int) row["reg_status"],
                    reg_time = (int) row["reg_time"],
                    age = (int) row["age"],
                    exp_age = (int) row["exp_age"],
                    rp_name = (string) row["rp_name"],
                    job = (string) row["job"],
                    wanted_level = (int) row["wanted_level"],
                    wanted_reason = (string) row["wanted_reason"],
                    
                    is_auth = true,
                    health = (int) row["health"],

                    date_reg = (int) row["date_reg"],
                    jailed = (bool) row["jailed"],
                    last_login = Main.GetTimeStamp(),
                    jail_time = (int) row["jail_time"],
                    
                    story_1 = (int) row["story_1"],
                    story_timeout_1 = (int) row["story_timeout_1"],
                    
                    water_level = (int) row["water_level"],
                    eat_level = (int) row["eat_level"],
                    health_level = (int) row["health_level"],
                    temp_level = (float) row["temp_level"],
                    
                    sick_cold = (int) row["sick_cold"],
                    sick_poisoning = (int) row["sick_poisoning"],

                    date_ban = (int) row["date_ban"],
                    date_mute = (int) row["date_mute"],
                    warn = (int) row["warn"],

                    fraction_id = (int) row["fraction_id"],
                    rank = (int) row["rank"],
                    fraction_id2 = (int) row["fraction_id2"],
                    rank2 = (int) row["rank2"],
                    tag = (string) row["tag"],

                    admin_level = (int) row["admin_level"],
                    helper_level = (int) row["helper_level"],

                    bank_prefix = (int) row["bank_prefix"],
                    bank_number = (int) row["bank_number"],

                    item_clock = (bool) row["item_clock"],
                    phone_code = (int) row["phone_code"],
                    phone = (int) row["phone"],
                    is_buy_walkietalkie = (bool) row["is_buy_walkietalkie"],
                    walkietalkie_num = (string) row["walkietalkie_num"],
                    is_old_money = (bool) row["is_old_money"],

                    mask = (int) row["mask"],
                    mask_color = (int) row["mask_color"],
                    torso = (int) row["torso"],
                    torso_color = (int) row["torso_color"],
                    leg = (int) row["leg"],
                    leg_color = (int) row["leg_color"],
                    hand = (int) row["hand"],
                    hand_color = (int) row["hand_color"],
                    foot = (int) row["foot"],
                    foot_color = (int) row["foot_color"],
                    accessorie = (int) row["accessorie"],
                    accessorie_color = (int) row["accessorie_color"],
                    parachute = (int) row["parachute"],
                    parachute_color = (int) row["parachute_color"],
                    armor = (int) row["armor"],
                    armor_color = (int) row["armor_color"],
                    decal = (int) row["decal"],
                    decal_color = (int) row["decal_color"],
                    body = (int) row["body"],
                    body_color = (int) row["body_color"],
                    
                    hat = (int) row["hat"],
                    hat_color = (int) row["hat_color"],
                    glasses = (int) row["glasses"],
                    glasses_color = (int) row["glasses_color"],
                    ear = (int) row["ear"],
                    ear_color = (int) row["ear_color"],
                    watch = (int) row["watch"],
                    watch_color = (int) row["watch_color"],
                    bracelet = (int) row["bracelet"],
                    bracelet_color = (int) row["bracelet_color"],
                                       
                    tattoo_head_c = (string) row["tattoo_head_c"],
                    tattoo_head_o = (string) row["tattoo_head_o"],
                    tattoo_torso_c = (string) row["tattoo_torso_c"],
                    tattoo_torso_o = (string) row["tattoo_torso_o"],
                    tattoo_left_arm_c = (string) row["tattoo_left_arm_c"],
                    tattoo_left_arm_o = (string) row["tattoo_left_arm_o"],
                    tattoo_right_arm_c = (string) row["tattoo_right_arm_c"],
                    tattoo_right_arm_o = (string) row["tattoo_right_arm_o"],
                    tattoo_left_leg_c = (string) row["tattoo_left_leg_c"],
                    tattoo_left_leg_o = (string) row["tattoo_left_leg_o"],
                    tattoo_right_leg_c = (string) row["tattoo_right_leg_c"],
                    tattoo_right_leg_o = (string) row["tattoo_right_leg_o"],
                    
                    allow_marg = (bool) row["allow_marg"],

                    vip_status = (string) row["vip_status"],
                    vip_time = (int) row["vip_time"],
                    animal = (string) row["animal"],
                    animal_name = (string) row["animal_name"],

                    a_lic = (bool) row["a_lic"],
                    b_lic = (bool) row["b_lic"],
                    c_lic = (bool) row["c_lic"],
                    air_lic = (bool) row["air_lic"],
                    taxi_lic = (bool) row["taxi_lic"],
                    ship_lic = (bool) row["ship_lic"],
                    gun_lic = (bool) row["gun_lic"],
                    law_lic = (bool) row["law_lic"],
                    med_lic = (bool) row["med_lic"],
                    animal_lic = (bool) row["animal_lic"],
                    fish_lic = (bool) row["fish_lic"],
                    biz_lic = (bool) row["biz_lic"],

                    s_radio_balance = (int) row["s_radio_balance"],
                    s_radio_vol = (float) row["s_radio_vol"],
                    s_voice_vol = (float) row["s_voice_vol"],
                    s_voice_balance = (bool) row["s_voice_balance"],
                    
                    s_is_pay_type_bank = (bool) row["s_is_pay_type_bank"],
                    s_is_load_blip_house = (bool) row["s_is_load_blip_house"],
                    s_is_characher = (bool) row["s_is_characher"],
                    s_is_spawn_aprt = (bool) row["s_is_spawn_aprt"],
                    s_is_usehackerphone = (bool) row["s_is_usehackerphone"],
                    s_lang = (string) row["s_lang"],
                    s_clipset = (string) row["s_clipset"],

                    sell_car = (bool) row["sell_car"],
                    sell_car_time = (int) row["sell_car_time"],
                    
                    referer = (string) row["referer"],
                    ip_last = GetPlayerEndpoint(player.Handle),
                    
                    mp0_stamina = (int) row["mp0_stamina"],
                    mp0_strength = (int) row["mp0_strength"],
                    mp0_lung_capacity = (int) row["mp0_lung_capacity"],
                    mp0_wheelie_ability = (int) row["mp0_wheelie_ability"],
                    mp0_flying_ability = (int) row["mp0_flying_ability"],
                    mp0_shooting_ability = (int) row["mp0_shooting_ability"],
                    mp0_stealth_ability = (int) row["mp0_stealth_ability"],
                    mp0_watchdogs = (int) row["mp0_watchdogs"],

                    skill_builder = (int) row["skill_builder"],
                    skill_scrap = (int) row["skill_scrap"],
                    skill_shop = (int) row["skill_shop"],

                    count_hask = (int) row["count_hask"],
                    count_aask = (int) row["count_aask"]
                };
                
                var playerId = GetServerId(player);
                Main.SaveLog("Auth", $"[{GetPlayerEndpoint(player.Handle)}] {player.Identifiers["license"]} | {GetPlayerGuid(player.Handle)} | {player.Name} - {playerObj.id}");
                
                foreach (var property in typeof(PlayerData).GetProperties())
                {
                    Sync.Data.Reset(playerId, property.Name);
                    Sync.Data.Set(playerId, property.Name, property.GetValue(playerObj, null));
                }
                
                foreach (var property in typeof(PlayerSkin).GetProperties())
                {
                    Sync.Data.Reset(playerId, property.Name);
                    Sync.Data.Set(playerId, property.Name, property.GetValue(playerSkin, null));
                }
                
                TriggerClientEvent(player, "ARP:AddVehicleInfoGlobalDataList", Managers.Vehicle.VehicleInfoGlobalDataList);
                TriggerClientEvent("ARP:UpdateCoffer", Coffer.Money, Coffer.MoneyBomj, Coffer.Nalog, Coffer.BizzNalog, Coffer.MoneyLimit, Coffer.MoneyOld);
                //TriggerClientEvent(player, "ARP:AddHouseGlobalDataList", House.HouseGlobalDataList);
                
                TriggerClientEvent(player, "ARP:CloseMenu");
                SetVirtualWorld(player, 0);
                SpawnAuto(player);
                
                Appi.MySql.ExecuteQuery("UPDATE users SET is_online='1' WHERE id = '" + playerObj.id + "'");
                
                string sql = "INSERT INTO log_auth (nick, lic, datetime) VALUES ('" + playerObj.rp_name + "','" + player.Identifiers["license"] + "','" + Main.GetTimeStamp() + "')";
                Appi.MySql.ExecuteQuery(sql);
                
                PlayerIdList[GetServerId(player).ToString()] = playerObj.id;
                Managers.Sync.PlayerIdListToClient();

                foreach (DataRow carRow in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM cars WHERE id_user = " + playerObj.id).Rows)
                    Managers.Vehicle.AddUserVehicle(carRow);

                TriggerClientEvent(player, "ARP:AuthSuccess");

                Managers.Sync.UpdateWalkietalkie();
                return;
            }
        }
        
        public static void CreatePlayerAccount(Player player, string password, string rpName, string email, string referer)
        {
            Random rand = new Random();
            
            rpName = Main.DeleteSqlHack(rpName);
            email = Main.DeleteSqlHack(email);
            referer = Main.DeleteSqlHack(referer);

            int money = rand.Next(100) + 150;
            if (Main.ServerName == "Andromeda")
                money = money * 3;
            if (Main.ServerName == "Earth")
                money = money + 700;

            password = Main.Sha256(password);
            string skin = "{\"SEX\":0,\"GTAO_SHAPE_FIRST_ID\":0,\"GTAO_SHAPE_SECOND_ID\":0,\"GTAO_SKIN_FIRST_ID\":0,\"GTAO_HAIR\":1,\"GTAO_HAIR_COLOR\":0,\"GTAO_EYE_COLOR\":0,\"GTAO_EYEBROWS\":0,\"GTAO_EYEBROWS_COLOR\":0}";
            string sql = "INSERT INTO users (name, rp_name, password, rp_biography, money, parachute, parachute_color, body_color, leg_color, foot_color, body, leg, foot, skin, date_reg, ip_reg, email, referer) VALUES ('" + GetPlayerGuid(player.Handle) +
                         "', '" + rpName + "', '" + password + "', 'Нет', '" + money + "', '0', '44', '" + rand.Next(5) + "', '" + rand.Next(15) + "', '" + rand.Next(15) + "', '0', '1', '1', '" + skin + "', '" + Main.GetTimeStamp() + "', '" + player.EndPoint + "', '" + email + "', '" + referer + "')";

            Appi.MySql.ExecuteQuery(sql);
        }

        public static bool DoesNameAccountExist(string name)
        {
            name = Main.DeleteSqlHack(name);
            
            bool isAccount = false;
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT rp_name FROM users WHERE rp_name = '" + name + "'").Rows)
                if (!string.IsNullOrEmpty((string)row["rp_name"])) isAccount = true;
            return isAccount;
        }

        public static bool Does3AccountExist(Player player)
        {
            var lic = player.Identifiers["license"] == "" ? "none" : player.Identifiers["license"];
            return Appi.MySql.ExecuteQueryWithResult("SELECT lic FROM users WHERE lic = '" + lic + "'").Rows.Count >= 3;
        }

        public static void SpawnAuto(Player player)
        {
            try
            {
                string skin = (int) Sync.Data.Get(GetServerId(player), "SEX") == 1
                    ? "mp_f_freemode_01"
                    : "mp_m_freemode_01";

                if (Sync.Data.Has(GetId(player), "qposX"))
                {
                    SetVirtualWorld(player, (int) Sync.Data.Get(GetId(player), "qvw"));
                    TriggerClientEvent(player, "ARP:SpawnPlayer", skin, (float) Sync.Data.Get(GetId(player), "qposX"), (float) Sync.Data.Get(GetId(player), "qposY"), (float) Sync.Data.Get(GetId(player), "qposZ"), (float) Sync.Data.Get(GetId(player), "qrot"), true);
                    return;
                }

                if ((int) Sync.Data.Get(GetServerId(player), "id_house") == 0)
                    Sync.Data.Set(GetServerId(player), "s_is_spawn_aprt", true);

                if ((bool) Sync.Data.Get(GetServerId(player), "s_is_spawn_aprt") && (int) Sync.Data.Get(GetServerId(player), "apartment_id") != 0)
                {
                    foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM apartment WHERE id = '" + (int) Sync.Data.Get(GetServerId(player), "apartment_id") + "'").Rows)
                    {
                        var id = (int) row["id"];
                        var intId = (int) row["interior_id"];
                        var isEx = (bool) row["is_exterior"];
                        var pos = isEx ? new Vector3((float) Apartment.IntList[intId, 0], (float) Apartment.IntList[intId, 1], (float) Apartment.IntList[intId, 2]) : new Vector3((float) Apartment.HouseIntList[intId, 0], (float) Apartment.HouseIntList[intId, 1], (float) Apartment.HouseIntList[intId, 2]);
                        
                        SetVirtualWorld(player, id * -1);
                        TriggerClientEvent(player, "ARP:SpawnPlayer", skin, pos.X, pos.Y, pos.Z, 0f, true);
                        break;
                    }
                }
                else if ((int) Sync.Data.Get(GetServerId(player), "id_house") != 0)
                {
                    foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM houses WHERE id = '" + (int) Sync.Data.Get(GetServerId(player), "id_house") + "'").Rows)
                    {
                        TriggerClientEvent(player, "ARP:SpawnPlayer", skin, (float) row["x"], (float) row["y"], (float) row["z"], 0f, true);
                        break;
                    }
                }
                /*else if ((int) Sync.Data.Get(GetServerId(player), "id_house") == 0)
                {
                    foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM houses WHERE pid2 = '" + (int) Sync.Data.Get(GetServerId(player), "id") + "'").Rows)
                    {
                        TriggerClientEvent(player, "ARP:SpawnPlayer", skin, (float) row["x"], (float) row["y"], (float) row["z"], 0f, true);
                        break;
                    }
                }*/
                else if ((int) Sync.Data.Get(GetServerId(player), "condo_id") != 0)
                {
                    foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM condo WHERE id = '" + (int) Sync.Data.Get(GetServerId(player), "condo_id") + "'").Rows)
                    {
                        TriggerClientEvent(player, "ARP:SpawnPlayer", skin, (float) row["x"], (float) row["y"], (float) row["z"], 0f, true);
                        break;
                    }
                }
                else
                {
                    if ((int) Sync.Data.Get(GetServerId(player), "age") == 18 &&
                        (int) Sync.Data.Get(GetServerId(player), "exp_age") < 2)
                        TriggerClientEvent(player, "ARP:SpawnPlayer", skin, -1037.133f, -2737.67f, 13.78236f, 0f, true);
                    else
                    {
                        var rand = new Random();
                        
                        foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM houses WHERE pid2 = '" + (int) Sync.Data.Get(GetServerId(player), "id") + "'").Rows)
                    {
                        TriggerClientEvent(player, "ARP:SpawnPlayer", skin, (float) row["x"], (float) row["y"], (float) row["z"], 0f, true);
                        break;
                    }
                    foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM houses WHERE pid3 = '" + (int) Sync.Data.Get(GetServerId(player), "id") + "'").Rows)
                    {
                        TriggerClientEvent(player, "ARP:SpawnPlayer", skin, (float) row["x"], (float) row["y"], (float) row["z"], 0f, true);
                        break;
                    }
                    foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM houses WHERE pid4 = '" + (int) Sync.Data.Get(GetServerId(player), "id") + "'").Rows)
                    {
                        TriggerClientEvent(player, "ARP:SpawnPlayer", skin, (float) row["x"], (float) row["y"], (float) row["z"], 0f, true);
                        break;
                    }
                    foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM houses WHERE pid5 = '" + (int) Sync.Data.Get(GetServerId(player), "id") + "'").Rows)
                    {
                        TriggerClientEvent(player, "ARP:SpawnPlayer", skin, (float) row["x"], (float) row["y"], (float) row["z"], 0f, true);
                        break;
                    }
                    foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM houses WHERE pid6 = '" + (int) Sync.Data.Get(GetServerId(player), "id") + "'").Rows)
                    {
                        TriggerClientEvent(player, "ARP:SpawnPlayer", skin, (float) row["x"], (float) row["y"], (float) row["z"], 0f, true);
                        break;
                    }
                    foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM houses WHERE pid7 = '" + (int) Sync.Data.Get(GetServerId(player), "id") + "'").Rows)
                    {
                        TriggerClientEvent(player, "ARP:SpawnPlayer", skin, (float) row["x"], (float) row["y"], (float) row["z"], 0f, true);
                        break;
                    }
                    foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM houses WHERE pid8 = '" + (int) Sync.Data.Get(GetServerId(player), "id") + "'").Rows)
                    {
                        TriggerClientEvent(player, "ARP:SpawnPlayer", skin, (float) row["x"], (float) row["y"], (float) row["z"], 0f, true);
                        break;
                    }
                    foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM houses WHERE pid9 = '" + (int) Sync.Data.Get(GetServerId(player), "id") + "'").Rows)
                    {
                        TriggerClientEvent(player, "ARP:SpawnPlayer", skin, (float) row["x"], (float) row["y"], (float) row["z"], 0f, true);
                        break;
                    }
                    foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM houses WHERE pid10 = '" + (int) Sync.Data.Get(GetServerId(player), "id") + "'").Rows)
                    {
                        TriggerClientEvent(player, "ARP:SpawnPlayer", skin, (float) row["x"], (float) row["y"], (float) row["z"], 0f, true);
                        break;
                    }
                        switch (rand.Next(3))
                        {
                             case 0:
                                 TriggerClientEvent(player, "ARP:SpawnPlayer", skin, 462.8509f, -850.47f, 26.12981f, 0f, true);
                                 break;
                             case 1:
                                 TriggerClientEvent(player, "ARP:SpawnPlayer", skin, 1.66987f, -1225.569f, 28.29525f, 0f, true);
                                 break;
                             default:
                                 TriggerClientEvent(player, "ARP:SpawnPlayer", skin, 124.8076f, -1215.845f, 28.33152f, 0f, true);
                                 break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Main.SaveLog("SPAWN_EX", $"{(int) Sync.Data.Get(GetServerId(player), "id")} | {player.Name} | {e}");
                throw;
            }
        }

        public static void SetVirtualWorld(Player player, int id)
        {
            PlayerVirtualWorldList[GetServerId(player).ToString()] = id;
            Managers.Sync.VirtualWorldToClient();
        }

        public static void UnloadAccount(int serverId)
        {
            PlayerVirtualWorldList.Remove(serverId.ToString());
            PlayerIdList.Remove(serverId.ToString());
        }

        public static void SendPlayerSubTitle(Player player, string text)
        {
            TriggerClientEvent(player, "ARP:SendPlayerSubTitle", text);
        }

        public static void SendPlayerTooltip(Player player, string text)
        {
            TriggerClientEvent(player, "ARP:SendPlayerTooltip", text);
        }

        public static void SendToPlayerMenu(Player player, string text, string desc, Dictionary<string, string> data)
        {
            TriggerClientEvent(player, "ARP:SendToPlayerMenu", text, desc, data);
        }

        public static void SendToPlayerMembersMenu(Player player, Dictionary<string, string> data)
        {
            TriggerClientEvent(player, "ARP:SendToPlayerMembersMenu", data);
        }

        public static void SendToPlayerMembersMenu2(Player player, Dictionary<string, string> data)
        {
            TriggerClientEvent(player, "ARP:SendToPlayerMembersMenu2", data);
        }

        public static void SendToPlayersListMenu(Player player, Dictionary<string, string> data)
        {
            TriggerClientEvent(player, "ARP:SendToPlayersListMenu", data);
        }

        public static void SendToPlayerBankInfoMenu(Player player, Dictionary<string, string> data)
        {
            TriggerClientEvent(player, "ARP:SendToPlayerBankInfoMenu", data);
        }

        public static void SendToPlayerLogVehicleMenu(Player player, Dictionary<string, string> data)
        {
            TriggerClientEvent(player, "ARP:SendToPlayerLogVehicleMenu", data);
        }

        public static void SendToPlayerLogGunMenu(Player player, Dictionary<string, string> data)
        {
            TriggerClientEvent(player, "ARP:SendToPlayerLogGunMenu", data);
        }

        public static void SendToPlayerBusinessListMenu(Player player, Dictionary<string, string> data, Dictionary<string, string> data2)
        {
            TriggerClientEvent(player, "ARP:SendToPlayerBusinessListMenu", data, data2);
        }

        public static void SendToPlayerSmsListMenu(Player player, Dictionary<string, string> data, Dictionary<string, string> data2, Dictionary<string, string> data3, Dictionary<string, string> data4, string phone)
        {
            TriggerClientEvent(player, "ARP:SendToPlayerSmsListMenu", data, data2, data3, data4, phone);
        }

        public static void SendToPlayerContacntListMenu(Player player, Dictionary<string, string> data, Dictionary<string, string> data2, string phone)
        {
            TriggerClientEvent(player, "ARP:SendToPlayerContacntListMenu", data, data2, phone);
        }

        public static void SendToPlayerItemListMenu(Player player, Dictionary<string, string> data, Dictionary<string, int> data2, int ownerId, int ownerType)
        {
            TriggerClientEvent(player, "ARP:SendToPlayerItemListMenu", data, data2, ownerId, ownerType);
        }

        public static void SendToPlayerItemWorldListMenu(Player player, Dictionary<string, string> data, Dictionary<string, int> data2)
        {
            TriggerClientEvent(player, "ARP:SendToPlayerItemWorldListMenu", data, data2);
        }

        public static void SendToPlayerItemListUpdateAmountMenu(Player player, Dictionary<string, int> data, int ownerId, int ownerType)
        {
            TriggerClientEvent(player, "ARP:SendToPlayerItemListUpdateAmountMenu", data, ownerId, ownerType);
        }

        public static void SendToPlayerApartmentListMenu(Player player, Dictionary<int, string> data)
        {
            TriggerClientEvent(player, "ARP:SendToPlayerApartmentListMenu", data);
        }
        
        public static int TryLogin(string name, string password)
        {
            /*Error here*/
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT date_ban, password FROM users WHERE rp_name = '" + name + "'").Rows)
            {
                if (Main.Sha256(password) != (string) row["password"]) continue;
                if (Main.GetTimeStamp() < (int) row["date_ban"])
                    return (int) row["date_ban"];
                return 1;
            }
            return -1;
        }

        public static bool IsLogin(Player player)
        {
            return IsLogin(GetServerId(player));
        }

        public static bool IsLogin(int serverId)
        {
            return Sync.Data.Has(serverId, "is_auth") && (bool) Sync.Data.Get(serverId, "is_auth");
        }

        public static bool IsSapd(int serverId)
        {
            return IsLogin(serverId) && (int) Sync.Data.Get(serverId, "fraction_id") == 2;
        }

        public static bool IsSheriff(int serverId)
        {
            return IsLogin(serverId) && (int) Sync.Data.Get(serverId, "fraction_id") == 7;
        }

        public static bool IsAdmin(int serverId)
        {
            return IsLogin(serverId) && (int) Sync.Data.Get(serverId, "admin_level") > 0;
        }

        public static bool IsLeader(int serverId)
        {
            return IsLogin(serverId) && (int) Sync.Data.Get(serverId, "rank") == 14;
        }

        public static int GetServerId(Player player)
        {
            return Convert.ToInt32(player.Handle) <= 65535 ? Convert.ToInt32(player.Handle) : Convert.ToInt32(player.Handle) - 65535;
        }
        
        public static string GetRegStatusName(int serverId)
        {
            switch ((int) Sync.Data.Get(serverId, "reg_status"))
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
        
        public static string GetVipStatus(int serverId)
        {
            return Sync.Data.Has(serverId, "vip_status") && (string) Sync.Data.Get(serverId, "vip_status") == "" ? "none" : (string) Sync.Data.Get(serverId, "vip_status");
        }
        
        public static int GetId(Player player)
        {
            int serverId = GetServerId(player);
            return !Sync.Data.Has(serverId, "id") ? -1 : (int) Sync.Data.Get(serverId, "id");
        }
        
        public static Player GetPlayerById(int id)
        {
            return new PlayerList().FirstOrDefault(player => GetId(player) == id);
        }
        
        /*Money Cash*/

        public static void AddCashMoney(Player player, int money)
        {
            //Main.SaveLog("money", $"[ADD-CASH] {Data.rp_name} {moneyNow} + {money}");
            SetCashMoney(player, GetCashMoney(player) + money);
        }

        public static void RemoveCashMoney(Player player, int money)
        {
            SetCashMoney(player, GetCashMoney(player) - money);
        }

        public static void SetCashMoney(Player player, int money)
        {
            Sync.Data.Set(GetServerId(player), "money", money);
        }

        public static int GetCashMoney(Player player)
        {
            return Sync.Data.Get(GetServerId(player), "money");
        }
        
        /*Money Bank*/

        public static void AddBankMoney(Player player, int money)
        {
            SetBankMoney(player, GetBankMoney(player) + money);
        }

        public static void RemoveBankMoney(Player player, int money)
        {
            SetBankMoney(player, GetBankMoney(player) - money);
        }

        public static void SetBankMoney(Player player, int money)
        {
            Sync.Data.Set(GetServerId(player), "money_bank", money);
        }

        public static int GetBankMoney(Player player)
        {
            return Sync.Data.Get(GetServerId(player), "money_bank");
        }
        
        /*Money Payday*/

        public static void AddPayDayMoney(Player player, int money)
        {
            SetPayDayMoney(player, GetPayDayMoney(player) + money);
        }

        public static void RemovePayDayMoney(Player player, int money)
        {
            SetPayDayMoney(player, GetPayDayMoney(player) - money);
        }

        public static void SetPayDayMoney(Player player, int money)
        {
            Sync.Data.Set(GetServerId(player), "money_payday", money);
        }

        public static int GetPayDayMoney(Player player)
        {
            return Sync.Data.Get(GetServerId(player), "money_payday");
        }

        public static void UpdateAllData(Player player)
        {
            TriggerClientEvent("ARP:UpdateAllData");
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

public enum WeaponHash : uint
{
    SniperRifle = 100416529,
    FireExtinguisher = 101631238,
    CompactGrenadeLauncher = 125959754,
    Snowball = 126349499,
    VintagePistol = 137902532,
    CombatPDW = 171789620,
    HeavySniperMk2 = 177293209,
    HeavySniper = 205991906,
    SweeperShotgun = 317205821,
    MicroSMG = 324215364,
    Wrench = 419712736,
    Pistol = 453432689,
    PumpShotgun = 487013001,
    APPistol = 584646201,
    Ball = 600439132,
    Molotov = 615608432,
    SMG = 736523883,
    StickyBomb = 741814745,
    PetrolCan = 883325847,
    StunGun = 911657153,
    StoneHatchet = 940833800,
    AssaultRifleMk2 = 961495388,
    HeavyShotgun = 984333226,
    Minigun = 1119849093,
    GolfClub = 1141786504,
    RayCarbine = 1198256469,
    FlareGun = 1198879012,
    Flare = 1233104067,
    GrenadeLauncherSmoke = 1305664598,
    Hammer = 1317494643,
    PumpShotgunMk2 = 1432025498,
    CombatPistol = 1593441988,
    Gusenberg = 1627465347,
    CompactRifle = 1649403952,
    HomingLauncher = 1672152130,
    Nightstick = 1737195953,
    MarksmanRifleMk2 = 1785463520,
    Railgun = 1834241177,
    SawnOffShotgun = 2017895192,
    SMGMk2 = 2024373456,
    BullpupRifle = 2132975508,
    Firework = 2138347493,
    CombatMG = 2144741730,
    CarbineRifle = 2210333304,
    Crowbar = 2227010557,
    BullpupRifleMk2 = 2228681469,
    SNSPistolMk2 = 2285322324,
    Flashlight = 2343591895,
    Dagger = 2460120199,
    Grenade = 2481070269,
    PoolCue = 2484171525,
    Bat = 2508868239,
    SpecialCarbineMk2 = 2526821735,
    DoubleAction = 2548703416,
    Pistol50 = 2578377531,
    Knife = 2578778090,
    MG = 2634544996,
    BullpupShotgun = 2640438543,
    BZGas = 2694266206,
    Unarmed = 2725352035,
    GrenadeLauncher = 2726580491,
    NightVision = 2803906140,
    Musket = 2828843422,
    ProximityMine = 2874559379,
    AdvancedRifle = 2937143193,
    RayPistol = 2939590305,
    RPG = 2982836145,
    RayMinigun = 3056410471,
    PipeBomb = 3125143736,
    MiniSMG = 3173288789,
    SNSPistol = 3218215474,
    PistolMk2 = 3219281620,
    AssaultRifle = 3220176749,
    SpecialCarbine = 3231910285,
    Revolver = 3249783761,
    MarksmanRifle = 3342088282,
    RevolverMk2 = 3415619887,
    BattleAxe = 3441901897,
    HeavyPistol = 3523564046,
    KnuckleDuster = 3638508604,
    MachinePistol = 3675956304,
    CombatMGMk2 = 3686625920,
    MarksmanPistol = 3696079510,
    Machete = 3713923289,
    SwitchBlade = 3756226112,
    AssaultShotgun = 3800352039,
    DoubleBarrelShotgun = 4019527611,
    AssaultSMG = 4024951519,
    Hatchet = 4191993645,
    Bottle = 4192643659,
    CarbineRifleMk2 = 4208062921,
    Parachute = 4222310262,
    SmokeGrenade = 4256991824,
}