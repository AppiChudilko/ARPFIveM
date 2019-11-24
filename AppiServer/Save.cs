using System;
using System.Collections.Generic;
using CitizenFX.Core;

namespace Server
{
    public class Save : BaseScript
    {
        public static void UserAccount(Player player)
        {
            var plId = User.GetServerId(player);
            if (!Sync.Data.Has(plId, "id"))
                return;

            PlayerSkin skin = new PlayerSkin
            {
                GTAO_SHAPE_FIRST_ID = Sync.Data.Get(plId, "GTAO_SHAPE_FIRST_ID"),
                GTAO_SHAPE_SECOND_ID = Sync.Data.Get(plId, "GTAO_SHAPE_SECOND_ID"),
                GTAO_SHAPE_THRID_ID = Sync.Data.Get(plId, "GTAO_SHAPE_THRID_ID"),
                GTAO_SKIN_FIRST_ID = Sync.Data.Get(plId, "GTAO_SKIN_FIRST_ID"),
                GTAO_SKIN_SECOND_ID = Sync.Data.Get(plId, "GTAO_SKIN_SECOND_ID"),
                GTAO_SKIN_THRID_ID = Sync.Data.Get(plId, "GTAO_SKIN_THRID_ID"),
                GTAO_SHAPE_MIX = Sync.Data.Get(plId, "GTAO_SHAPE_MIX"),
                GTAO_SKIN_MIX = Sync.Data.Get(plId, "GTAO_SKIN_MIX"),
                GTAO_THRID_MIX = Sync.Data.Get(plId, "GTAO_THRID_MIX"),
                GTAO_HAIR = Sync.Data.Get(plId, "GTAO_HAIR"),
                GTAO_HAIR_COLOR = Sync.Data.Get(plId, "GTAO_HAIR_COLOR"),
                GTAO_HAIR_COLOR2 = Sync.Data.Get(plId, "GTAO_HAIR_COLOR2"),
                GTAO_EYE_COLOR = Sync.Data.Get(plId, "GTAO_EYE_COLOR"),
                GTAO_EYEBROWS = Sync.Data.Get(plId, "GTAO_EYEBROWS"),
                GTAO_EYEBROWS_COLOR = Sync.Data.Get(plId, "GTAO_EYEBROWS_COLOR"),
                GTAO_OVERLAY = Sync.Data.Get(plId, "GTAO_OVERLAY"),
                GTAO_OVERLAY_COLOR = Sync.Data.Get(plId, "GTAO_OVERLAY_COLOR"),
                GTAO_OVERLAY4 = Sync.Data.Get(plId, "GTAO_OVERLAY4"),
                GTAO_OVERLAY4_COLOR = Sync.Data.Get(plId, "GTAO_OVERLAY4_COLOR"),
                GTAO_OVERLAY5 = Sync.Data.Get(plId, "GTAO_OVERLAY5"),
                GTAO_OVERLAY5_COLOR = Sync.Data.Get(plId, "GTAO_OVERLAY5_COLOR"),
                GTAO_OVERLAY6 = Sync.Data.Get(plId, "GTAO_OVERLAY6"),
                GTAO_OVERLAY6_COLOR = Sync.Data.Get(plId, "GTAO_OVERLAY6_COLOR"),
                GTAO_OVERLAY7 = Sync.Data.Get(plId, "GTAO_OVERLAY7"),
                GTAO_OVERLAY7_COLOR = Sync.Data.Get(plId, "GTAO_OVERLAY7_COLOR"),
                GTAO_OVERLAY8 = Sync.Data.Get(plId, "GTAO_OVERLAY8"),
                GTAO_OVERLAY8_COLOR = Sync.Data.Get(plId, "GTAO_OVERLAY8_COLOR"),
                GTAO_OVERLAY9 = Sync.Data.Get(plId, "GTAO_OVERLAY9"),
                GTAO_OVERLAY9_COLOR = Sync.Data.Get(plId, "GTAO_OVERLAY9_COLOR"),
                GTAO_OVERLAY10 = Sync.Data.Get(plId, "GTAO_OVERLAY10"),
                GTAO_OVERLAY10_COLOR = Sync.Data.Get(plId, "GTAO_OVERLAY10_COLOR"),
                SEX = Sync.Data.Get(plId, "SEX")
            };


            Sync.Data.Set(plId, "skin", Main.ToJson(skin));
            
            string sql1 = "UPDATE users SET empty_col = 'null'";
            foreach (var property in typeof(PlayerData).GetProperties())
            {
                if (property.Name == "id" || property.Name == "password" || property.Name == "rp_name" || property.Name == "is_auth") continue;
                if (property.Name == "car_id1_key" || property.Name == "car_id2_key" || property.Name == "car_id3_key" || property.Name == "car_id4_key" || property.Name == "car_id5_key" || property.Name == "car_id6_key" || property.Name == "car_id7_key" || property.Name == "car_id8_key") continue;
                if (!Sync.Data.Has(plId, property.Name)) continue;
                
                var propertyValue = Sync.Data.Get(plId, property.Name);
                if (propertyValue is bool)
                    propertyValue = propertyValue == true ? 1 : 0;

                sql1 = sql1 + ", " + property.Name + " = '" + propertyValue + "'";
            }
            sql1 = sql1 + " where id = '" + (int) Sync.Data.Get(plId, "id") + "'";
            
            Appi.MySql.ExecuteQuery(sql1);
            /*return;

            string sql;
            
            sql = "UPDATE users SET";
            
            sql = sql + " car_id5 = '" + data.car_id5 + "'";
            sql = sql + ", car_id4 = '" + data.car_id4 + "'";
            sql = sql + ", car_id3 = '" + data.car_id3 + "'";
            sql = sql + ", car_id2 = '" + data.car_id2 + "'";
            sql = sql + ", car_id1 = '" + data.car_id1 + "'";
            sql = sql + ", business_id = '" + data.business_id + "'";
            sql = sql + ", id_house = '" + data.id_house + "'";
            sql = sql + ", id_house = '" + data.id_house + "'";
            sql = sql + ", posob = '" + (data.posob ? "1" : "0") + "'";
            sql = sql + ", money_payday = '" + data.money_payday + "'";
            sql = sql + ", money_bank = '" + data.money_bank + "'";
            sql = sql + ", money = '" + data.money + "'";
            sql = sql + ", health = '" + data.health + "'";
            sql = sql + ", wanted_reason = '" + data.wanted_reason + "'";
            sql = sql + ", wanted_level = '" + data.wanted_level + "'";
            sql = sql + ", exp_age = '" + data.exp_age + "'";
            sql = sql + ", age = '" + data.age + "'";
            sql = sql + ", reg_time = '" + data.reg_time + "'";
            sql = sql + ", reg_status = '" + data.reg_status + "'";
            sql = sql + ", exp = '" + data.exp + "'";
            sql = sql + ", job = '" + data.job + "'";
            sql = sql + ", skin = '" + data.skin + "'";
            sql = sql + ", med_vitamin = '" + data.med_vitamin + "'";
            sql = sql + ", med_antibiotic = '" + data.med_antibiotic + "'";
            sql = sql + ", med_antipyretic = '" + data.med_antipyretic + "'";
            sql = sql + ", med_tablet_cough = '" + data.med_tablet_cough + "'";
            sql = sql + ", med_vasoconstrictive = '" + data.med_vasoconstrictive + "'";
            sql = sql + ", med_syrup = '" + data.med_syrup + "'";
            sql = sql + ", med_carbon = '" + data.med_carbon + "'";
            sql = sql + ", water_redbull = '" + data.water_redbull + "'";
            sql = sql + ", water_cola = '" + data.water_cola + "'";
            sql = sql + ", water_limonad = '" + data.water_limonad + "'";
            sql = sql + ", water_tea = '" + data.water_tea + "'";
            sql = sql + ", water_coffie = '" + data.water_coffie + "'";
            sql = sql + ", water = '" + data.water + "'";
            sql = sql + ", eat_veg = '" + data.eat_veg + "'";
            sql = sql + ", eat_mre = '" + data.eat_mre + "'";
            sql = sql + ", eat_rabbit = '" + data.eat_rabbit + "'";
            sql = sql + ", eat_quesadilla = '" + data.eat_quesadilla + "'";
            sql = sql + ", eat_roast = '" + data.eat_roast + "'";
            sql = sql + ", eat_pizza = '" + data.eat_pizza + "'";
            sql = sql + ", eat_hamburger = '" + data.eat_hamburger + "'";
            sql = sql + ", eat_cesar = '" + data.eat_cesar + "'";
            sql = sql + ", eat_roll = '" + data.eat_roll + "'";
            sql = sql + ", eat_appi_pot = '" + data.eat_appi_pot + "'";
            sql = sql + ", eat_appi_nuts = '" + data.eat_appi_nuts + "'";
            sql = sql + ", eat = '" + data.eat + "'";
            sql = sql + ", fuel_item = '" + data.fuel_item + "'";
            sql = sql + ", jail_time = '" + data.jail_time + "'";
            sql = sql + ", jailed = '" + (data.jailed ? "1" : "0") + "'";
            
            sql = sql + " where id = '" + data.id + "'";
            
            //Main.SaveLog("SQL", sql);
            Appi.MySql.ExecuteQuery(sql);
            
            
            sql = "UPDATE users SET";
            
            sql = sql + " toolskit = '" + data.toolskit + "'";
            sql = sql + ", caroil = '" + data.caroil + "'";
            sql = sql + ", picklock = '" + data.picklock + "'";
            sql = sql + ", is_old_money = '" + (data.is_old_money ? "1" : "0") + "'";
            sql = sql + ", is_buy_lic_sp = '" + (data.is_buy_lic_sp ? "1" : "0") + "'";
            sql = sql + ", is_buy_mob_sp = '" + (data.is_buy_mob_sp ? "1" : "0") + "'";
            sql = sql + ", is_buy_walkietalkie = '" + (data.is_buy_walkietalkie ? "1" : "0") + "'";
            sql = sql + ", is_buy_mob = '" + (data.is_buy_mob ? "1" : "0") + "'";
            sql = sql + ", walkietalkie_num = '" + data.walkietalkie_num + "'";
            sql = sql + ", phone = '" + data.phone + "'";
            sql = sql + ", phone_code = '" + data.phone_code + "'";
            sql = sql + ", bank_number = '" + data.bank_number + "'";
            sql = sql + ", bank_prefix = '" + data.bank_prefix + "'";
            sql = sql + ", helper_level = '" + data.helper_level + "'";
            sql = sql + ", admin_level = '" + data.admin_level + "'";
            sql = sql + ", tag = '" + data.tag + "'";
            sql = sql + ", rank = '" + data.rank + "'";
            sql = sql + ", fraction_id = '" + data.fraction_id + "'";
            sql = sql + ", warn = '" + data.warn + "'";
            sql = sql + ", date_mute = '" + data.date_mute + "'";
            sql = sql + ", date_ban = '" + data.date_ban + "'";
            sql = sql + ", last_login = '" + data.last_login + "'";
            sql = sql + ", sick_poisoning = '" + data.sick_poisoning + "'";
            sql = sql + ", sick_cold = '" + data.sick_cold + "'";
            sql = sql + ", temp_level = '" + data.temp_level + "'";
            sql = sql + ", health_level = '" + data.health_level + "'";
            sql = sql + ", water_level = '" + data.water_level + "'";
            sql = sql + ", eat_level = '" + data.eat_level + "'";            
            sql = sql + ", animal_name = '" + data.animal_name + "'";
            sql = sql + ", animal = '" + data.animal + "'";
            sql = sql + ", vip_time = '" + data.vip_time + "'";
            sql = sql + ", vip_status = '" + data.vip_status + "'";
            sql = sql + ", weapons = '" + data.weapons + "'";
            sql = sql + ", guns = '" + data.guns + "'";
            sql = sql + ", drug_marg = '" + data.drug_marg + "'";
            sql = sql + ", drugs = '" + data.drugs + "'";
            sql = sql + ", foot_color = '" + data.foot_color + "'";
            sql = sql + ", foot = '" + data.foot + "'";
            sql = sql + ", parachute_color = '" + data.parachute_color + "'";
            sql = sql + ", parachute = '" + data.parachute + "'";
            sql = sql + ", hand_color = '" + data.hand_color + "'";
            sql = sql + ", hand = '" + data.hand + "'";
            sql = sql + ", leg_color = '" + data.leg_color + "'";
            sql = sql + ", leg = '" + data.leg + "'";
            sql = sql + ", torso_color = '" + data.torso_color + "'";
            sql = sql + ", torso = '" + data.torso + "'";
            sql = sql + ", body_color = '" + data.body_color + "'";
            sql = sql + ", body = '" + data.body + "'";
            sql = sql + ", head_color = '" + data.head_color + "'";
            sql = sql + ", head = '" + data.head + "'";
            sql = sql + ", story_1 = '" + data.story_1 + "'";
            sql = sql + ", story_timeout_1 = '" + data.story_timeout_1 + "'";
            sql = sql + ", sell_car_time = '" + data.sell_car_time + "'";
            sql = sql + ", sell_car = '" + (data.sell_car ? "1" : "0") + "'";
            
            sql = sql + " where id = '" + data.id + "'";
            
            //Main.SaveLog("SQL", sql);
            Appi.MySql.ExecuteQuery(sql);
            
            
            sql = "UPDATE users SET";
            
            sql = sql + " count_aask = '" + data.count_aask + "'";
            sql = sql + ", count_hask = '" + data.count_hask + "'";
            sql = sql + ", skill_shop = '" + data.skill_shop + "'";
            sql = sql + ", skill_scrap = '" + data.skill_scrap + "'";
            sql = sql + ", skill_builder = '" + data.skill_builder + "'";
            sql = sql + ", house_grab = '" + data.house_grab + "'";
            sql = sql + ", referer = '" + data.referer + "'";
            sql = sql + ", mailhouses = '" + data.mailhouses + "'";
            sql = sql + ", ip_last = '" + data.ip_last + "'";
            sql = sql + ", s_lang = '" + data.s_lang + "'";
            sql = sql + ", s_is_characher = '" + (data.s_is_characher ? "1" : "0") + "'";
            sql = sql + ", s_is_load_blip_house = '" + (data.s_is_load_blip_house ? "1" : "0") + "'";
            sql = sql + ", s_is_pay_type_bank = '" + (data.s_is_pay_type_bank ? "1" : "0") + "'";
            sql = sql + ", law_lic = '" + (data.law_lic ? "1" : "0") + "'";
            sql = sql + ", gun_lic = '" + (data.gun_lic ? "1" : "0") + "'";
            sql = sql + ", psy_lic = '" + (data.psy_lic ? "1" : "0") + "'";
            sql = sql + ", ship_lic = '" + (data.ship_lic ? "1" : "0") + "'";
            sql = sql + ", taxi_lic = '" + (data.taxi_lic ? "1" : "0") + "'";
            sql = sql + ", air_lic = '" + (data.air_lic ? "1" : "0") + "'";
            sql = sql + ", heli_lic = '" + (data.heli_lic ? "1" : "0") + "'";
            sql = sql + ", c_lic = '" + (data.c_lic ? "1" : "0") + "'";
            sql = sql + ", b_lic = '" + (data.b_lic ? "1" : "0") + "'";
            sql = sql + ", a_lic = '" + (data.a_lic ? "1" : "0") + "'";
            sql = sql + ", allow_marg = '" + (data.allow_marg ? "1" : "0") + "'";
            
            sql = sql + " where id = '" + data.id + "'";
            
            //Main.SaveLog("SQL", sql);
            Appi.MySql.ExecuteQuery(sql);*/
        }
        
        public static void Business(int id)
        {   
            if (id <= 0) return;
        
            id = -20000 + id;
        
            if (!Sync.Data.Has(id, "id")) return;
            
            string sql = "UPDATE business SET";
            
            sql = sql + " name = '" + Main.RemoveQuotes((string) Sync.Data.Get(id, "name")) + "'";
            //sql = sql + ", price = '" + (int) Sync.Data.Get(id, "price") + "'";
            sql = sql + ", user_name = '" + (string) Sync.Data.Get(id, "user_name") + "'";
            sql = sql + ", user_id = '" + (int) Sync.Data.Get(id, "user_id") + "'";
            sql = sql + ", bank = '" + (int) Sync.Data.Get(id, "bank") + "'";
            //sql = sql + ", type = '" + (int) Sync.Data.Get(id, "type") + "'";
            sql = sql + ", price_product = '" + (int) Sync.Data.Get(id, "price_product") + "'";
            sql = sql + ", price_card1 = '" + (int) Sync.Data.Get(id, "price_card1") + "'";
            sql = sql + ", price_card2 = '" + (int) Sync.Data.Get(id, "price_card2") + "'";
            sql = sql + ", tarif = '" + (int) Sync.Data.Get(id, "tarif") + "'";
            sql = sql + ", interior = '" + (int) Sync.Data.Get(id, "interior") + "'";
            
            sql = sql + " where id = '" + (int) Sync.Data.Get(id, "id") + "'";
            
            //Main.SaveLog("SQL", sql);
            Appi.MySql.ExecuteQuery(sql);
        }
        
        public static void UserVehicle(VehicleInfoGlobalData vehData)
        {   
            if (vehData.id <= 0) return;
            
            var vdata = Sync.Data.GetAll(110000 + vehData.VehId);
            if (vdata == null) return;

            var data = new VehicleInfoGlobalData();
            var localData = (IDictionary<String, Object>) vdata;
            foreach (var property in typeof(VehicleInfoGlobalData).GetProperties())
                property.SetValue(data, localData[property.Name], null);

            if (data.id_user == 0) return;
            if (!data.IsVisible) return;

            foreach (var item in Managers.Vehicle.VehicleInfoGlobalDataList)
            {
                if (item.VehId == data.VehId)
                {
                    Managers.Vehicle.VehicleInfoGlobalDataList[item.VehId].Fuel = data.Fuel;
                    Managers.Vehicle.VehicleInfoGlobalDataList[item.VehId].SBody = data.SBody;
                    Managers.Vehicle.VehicleInfoGlobalDataList[item.VehId].SCandle = data.Fuel;
                    Managers.Vehicle.VehicleInfoGlobalDataList[item.VehId].SEngine = data.SEngine;
                    Managers.Vehicle.VehicleInfoGlobalDataList[item.VehId].SMp = data.Fuel;
                    Managers.Vehicle.VehicleInfoGlobalDataList[item.VehId].SOil = data.Fuel;
                    Managers.Vehicle.VehicleInfoGlobalDataList[item.VehId].SSuspension = data.SSuspension;
                    Managers.Vehicle.VehicleInfoGlobalDataList[item.VehId].SWhBkl = data.SWhBkl;
                    Managers.Vehicle.VehicleInfoGlobalDataList[item.VehId].IsUserOwner = data.IsUserOwner;
                    Managers.Vehicle.VehicleInfoGlobalDataList[item.VehId].user_name = data.user_name;
                    Managers.Vehicle.VehicleInfoGlobalDataList[item.VehId].id_user = data.id_user;
                    Managers.Vehicle.VehicleInfoGlobalDataList[item.VehId].upgrade = data.upgrade;
                }
            }
               
            string sql = "UPDATE cars SET";
            
            sql = sql + " cop_park_name = '" + data.cop_park_name + "'";
            sql = sql + ", upgrade = '" + data.upgrade + "'";
            sql = sql + ", color1 = '" + data.color1 + "'";
            sql = sql + ", color2 = '" + data.color2 + "'";
            sql = sql + ", neon_type = '" + data.neon_type + "'";
            sql = sql + ", neon_r = '" + data.neon_r + "'";
            sql = sql + ", neon_g = '" + data.neon_g + "'";
            sql = sql + ", neon_b = '" + data.neon_b + "'";
            sql = sql + ", wanted_level = '" + data.wanted_level + "'";
            sql = sql + ", fuel = '" + data.Fuel + "'";
            sql = sql + ", stock_item = '" + data.StockItem + "'";
            sql = sql + ", class_type = '" + data.ClassName + "'";
            sql = sql + ", number = '" + data.Number + "'";
            sql = sql + ", s_mp = '" + data.SMp + "'";
            sql = sql + ", s_wh_bk_l = '" + data.SWhBkl + "'";
            sql = sql + ", s_wh_b_l = '" + data.SWhBl + "'";
            sql = sql + ", s_wh_bk_r = '" + data.SWhBkr + "'";
            sql = sql + ", s_wh_b_r = '" + data.SWhBr + "'";
            sql = sql + ", s_engine = '" + data.SEngine + "'";
            sql = sql + ", s_suspension = '" + data.SSuspension + "'";
            sql = sql + ", s_body = '" + data.SBody + "'";
            sql = sql + ", s_candle = '" + data.SCandle + "'";
            sql = sql + ", s_oil = '" + data.SOil + "'";
            sql = sql + ", livery = '" + data.Livery + "'";
            sql = sql + ", is_cop_park = '" + (data.is_cop_park ? "1" : "0") + "'";
            sql = sql + ", lock_status = '" + (data.lock_status ? "1" : "0") + "'";
            
            sql = sql + " where id = '" + data.id + "'";
            
            //Main.SaveLog("SQL", sql);
            Appi.MySql.ExecuteQuery(sql);
        }
        
        public static void UserVehicleById(int vehId)
        {
            foreach (var item in Managers.Vehicle.VehicleInfoGlobalDataList)
                if (vehId == item.VehId)
                    UserVehicle(item);
        }
    }
}