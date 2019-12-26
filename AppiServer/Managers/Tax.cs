using System;
using System.Data;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace Server.Managers
{
    public class Tax : BaseScript
    {
        private static int Bonus = 1;
        private const double CurrentTax = 0.0002;
        private const int TaxMin = 10;
        private const int TaxDays = 21 * 7;
        private const int TaxDays2 = 21;

        public Tax()
        {
            if (Main.ServerName == "Andromeda")
                Bonus = 2;
            Tick += TimeSync;

            UpdateTax();
        }
        
        private static async Task TimeSync()
        {
            Appi.MySql.ExecuteQuery("UPDATE houses SET score_tax = (RAND(90000000) * 10000000) + 50000000");
            Appi.MySql.ExecuteQuery("UPDATE condo SET score_tax = (RAND(90000000) * 10000000) + 40000000");
            Appi.MySql.ExecuteQuery("UPDATE apartment SET score_tax = (RAND(90000000) * 10000000) + 70000000");
            Appi.MySql.ExecuteQuery("UPDATE business SET score_tax = (RAND(90000000) * 10000000) + 10000000");
            Appi.MySql.ExecuteQuery("UPDATE stocks SET score_tax = (RAND(90000000) * 10000000) + 90000000");
            Appi.MySql.ExecuteQuery("UPDATE cars SET score_tax = (RAND(90000000) * 10000000) + 30000000");
            
            
            await Delay(Convert.ToInt32(60000 * 60 * 3.4));
            RemoveTax();
            await Delay(5000);
            Sell();
        }

        public static void BankNotification(Player p, int prefix, int sum)
        {
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
        }

        public static void AdWithNotification(string text)
        {
            Appi.MySql.ExecuteQuery("INSERT INTO rp_invader_ad (datetime, name, phone, title, text) " +
                                    $"VALUES ('{Weather.GetRpDateTime()}', 'Государство', 'gov.sa', 'Продажа', '{text}')");
            
            text = (text.Length > 51) ? text.Remove(51) + "..." : text;
            
            TriggerClientEvent("ARP:SendPlayerNotificationPicture", text, "~g~Реклама", "Государство", "CHAR_LIFEINVADER", 2);
        }

        public static void Sell()
        {
            //=============================
            //============Склады=============
            //=============================
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM stocks WHERE money_tax <= (round(price * '" + CurrentTax * Bonus + "' + '10', 0) * '" + TaxDays2 + "') * '-1' AND user_id > '0'").Rows)
            {
                int price = Convert.ToInt32((int) row["price"] / 2);
                if ((int) row["money_tax"] < -100000)
                    price = Convert.ToInt32(price * 2 * 1.3);
                
                bool isPlayerOnline = false;
                foreach (var player in new PlayerList())
                {
                    if (!Server.Sync.Data.Has(User.GetServerId(player), "id")) continue;
                    if ((int) Server.Sync.Data.Get(User.GetServerId(player), "id") == (int) row["user_id"])
                    {
                        isPlayerOnline = true;
                        Server.Sync.Data.Set(User.GetServerId(player), "stock_id", 0);
                        User.AddBankMoney(player, price);
                        
                        TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Ваш склад был изъят государством за неуплату");
                        BankNotification(player, (int) Server.Sync.Data.Get(User.GetServerId(player), "bank_prefix"), price);
                        
                        User.UpdateAllData(player);
                    }
                }

                if (!isPlayerOnline)
                    Appi.MySql.ExecuteQuery("UPDATE users SET money_bank = money_bank + '" + price + "', stock_id = '0', money_tax = '0' WHERE id = '" + (int) row["user_id"] + "'");
                
                Stock.Save((int) row["id"], "", 0);
                AdWithNotification($"Склад {(string) row["address"]} №{(int) row["id"]} поступил в продажу");
                
                Main.SaveLog("SELL_UNACTIVE", $"USER: {(int) row["user_id"]} STOCK {(int) row["id"]}");
            }
            
            //=============================
            //============Дома=============
            //=============================
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM houses WHERE money_tax <= (round(price * '" + CurrentTax * Bonus + "' + '10', 0) * '" + TaxDays2 + "') * '-1' AND id_user > '0'").Rows)
            {
                int price = Convert.ToInt32((int) row["price"] / 2);
                if ((int) row["money_tax"] < -100000)
                    price = Convert.ToInt32(price * 2 * 1.3);
                
                bool isPlayerOnline = false;
                foreach (var player in new PlayerList())
                {
                    if (!Server.Sync.Data.Has(User.GetServerId(player), "id")) continue;
                    if ((int) Server.Sync.Data.Get(User.GetServerId(player), "id") == (int) row["id_user"])
                    {
                        isPlayerOnline = true;
                        Server.Sync.Data.Set(User.GetServerId(player), "id_house", 0);
                        User.AddBankMoney(player, price);
                        
                        TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Ваш дом был изъят государством за неуплату");
                        BankNotification(player, (int) Server.Sync.Data.Get(User.GetServerId(player), "bank_prefix"), price);
                        
                        User.UpdateAllData(player);
                    }
                }

                if (!isPlayerOnline)
                    Appi.MySql.ExecuteQuery("UPDATE users SET money_bank = money_bank + '" + price + "', id_house = '0', money_tax = '0' WHERE id = '" + (int) row["id_user"] + "'");
                
                House.SaveHouse((int) row["id"], 0, "", 0);
                AdWithNotification($"Дом {(string) row["address"]} №{(int) row["id"]} поступил в продажу");
                
                Main.SaveLog("SELL_UNACTIVE", $"USER: {(int) row["id_user"]} HOUSE {(int) row["id"]}");
            }
            
            //=============================
            //============Квартиры=============
            //=============================
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM condo WHERE money_tax <= (round(price * '" + CurrentTax * Bonus + "' + '10', 0) * '" + TaxDays2 + "') * '-1' AND id_user > '0'").Rows)
            {
                int price = Convert.ToInt32((int) row["price"] / 2);
                if ((int) row["money_tax"] < -100000)
                    price = Convert.ToInt32(price * 2 * 1.3);
                
                bool isPlayerOnline = false;
                foreach (var player in new PlayerList())
                {
                    if (!Server.Sync.Data.Has(User.GetServerId(player), "id")) continue;
                    if ((int) Server.Sync.Data.Get(User.GetServerId(player), "id") == (int) row["id_user"])
                    {
                        isPlayerOnline = true;
                        Server.Sync.Data.Set(User.GetServerId(player), "condo_id", 0);
                        User.AddBankMoney(player, price);
                        
                        TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Ваш дом был изъят государством за неуплату");
                        BankNotification(player, (int) Server.Sync.Data.Get(User.GetServerId(player), "bank_prefix"), price);
                        
                        User.UpdateAllData(player);
                    }
                }

                if (!isPlayerOnline)
                    Appi.MySql.ExecuteQuery("UPDATE users SET money_bank = money_bank + '" + price + "', condo_id = '0', money_tax = '0', pin = '0' WHERE id = '" + (int) row["id_user"] + "'");
                
                Condo.SaveHouse((int) row["id"], "", 0);
                AdWithNotification($"Квартира {(string) row["address"]} №{(int) row["id"]} поступил в продажу");
                
                Main.SaveLog("SELL_UNACTIVE", $"USER: {(int) row["id_user"]} HOUSE {(int) row["id"]}");
            }
            
            //=============================
            //=========Бизнесы=============
            //=============================
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM business WHERE money_tax <= (round(price * '" + CurrentTax * Bonus + "' + '10', 0) * '" + TaxDays2 + "') * '-1' AND user_id > '0'").Rows)
            {
                int price = Convert.ToInt32((int) row["price"] / 2);
                if ((int) row["money_tax"] < -100000)
                    price = Convert.ToInt32(price * 2 * 1.3);
                
                bool isPlayerOnline = false;
                foreach (var player in new PlayerList())
                {
                    if (!Server.Sync.Data.Has(User.GetServerId(player), "id")) continue;
                    if ((int) Server.Sync.Data.Get(User.GetServerId(player), "id") == (int) row["user_id"])
                    {
                        isPlayerOnline = true;
                        Server.Sync.Data.Set(User.GetServerId(player), "business_id", 0);
                        User.AddBankMoney(player, price);
                        
                        TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Ваш бизнес был изъят государством за неуплату");
                        BankNotification(player, (int) Server.Sync.Data.Get(User.GetServerId(player), "bank_prefix"), price);
                        
                        User.UpdateAllData(player);
                    }
                }

                if (!isPlayerOnline)
                    Appi.MySql.ExecuteQuery("UPDATE users SET money_bank = money_bank + '" + price + "', business_id = '0', money_tax = '0' WHERE id = '" + (int) row["user_id"] + "'");
                
                Server.Sync.Data.Set(-20000 + (int) row["id"], "user_id", 0);
                Server.Sync.Data.Set(-20000 + (int) row["id"], "user_name", "");
                Server.Sync.Data.Set(-20000 + (int) row["id"], "money_tax", 0);
                
                Appi.MySql.ExecuteQuery("UPDATE business SET money_tax = '0' WHERE id = '" + (int) row["id"] + "'");
                
                AdWithNotification($"Бизнес {(string) row["name"]} поступил в продажу");
                
                Main.SaveLog("SELL_UNACTIVE", $"USER: {(int) row["user_id"]} BUSINESS {(int) row["id"]}");
            }
            
            //=============================
            //============Авто=============
            //=============================
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM cars WHERE money_tax <= (round(price * '" + CurrentTax * Bonus + "' + '10', 0) * '" + TaxDays2 + "') * '-1' AND id_user > '0'").Rows)
            {
                int price = Convert.ToInt32((int) row["price"] / 2);
                if ((int) row["money_tax"] < -100000)
                    price = Convert.ToInt32(price * 2 * 1.3);
                
                string carId = "1";

                foreach (DataRow rowUser in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM users WHERE car_id1 = '" + (int) row["id"] + "' OR car_id2 = '" + (int) row["id"] + "' OR car_id3 = '" + (int) row["id"] + "' OR car_id4 = '" + (int) row["id"] + "' OR car_id5 = '" + (int) row["id"] + "' OR car_id6 = '" + (int) row["id"] + "' OR car_id7 = '" + (int) row["id"] + "' OR car_id8 = '" + (int) row["id"] + "'").Rows)
                {
                    if ((int) rowUser["car_id2"] == (int) row["id"])
                        carId = "2";
                    if ((int) rowUser["car_id3"] == (int) row["id"])
                        carId = "3";
                    if ((int) rowUser["car_id4"] == (int) row["id"])
                        carId = "4";
                    if ((int) rowUser["car_id5"] == (int) row["id"])
                        carId = "5";
                    if ((int) rowUser["car_id6"] == (int) row["id"])
                        carId = "6";
                    if ((int) rowUser["car_id7"] == (int) row["id"])
                        carId = "7";
                    if ((int) rowUser["car_id8"] == (int) row["id"])
                        carId = "8";
                }

                bool isPlayerOnline = false;
                foreach (var player in new PlayerList())
                {
                    if (!Server.Sync.Data.Has(User.GetServerId(player), "id")) continue;
                    if ((int) Server.Sync.Data.Get(User.GetServerId(player), "id") == (int) row["id_user"])
                    {
                        isPlayerOnline = true;
                        Server.Sync.Data.Set(User.GetServerId(player), "car_id" + carId, 0);
                        User.AddBankMoney(player, price);
                        
                        TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Ваш авто '" + (string) row["name"] +  "' был изъят государством за неуплату");
                        BankNotification(player, (int) Server.Sync.Data.Get(User.GetServerId(player), "bank_prefix"), price);
                        
                        User.UpdateAllData(player);
                    }
                }

                if (!isPlayerOnline)
                    Appi.MySql.ExecuteQuery("UPDATE users SET money_bank = money_bank + '" + price + "', car_id" + carId + " = '0', money_tax = '0' WHERE id = '" + (int) row["id_user"] + "'");
                
                Vehicle.BuyAndSellCar((int) row["id"], "", 0);
                
                AdWithNotification($"Транспорт {(string) row["name"]} поступил в продажу");
                
                Main.SaveLog("SELL_UNACTIVE", $"USER: {(int) row["id_user"]} CAR {(int) row["id"]}");
            }
            
            //=============================
            //==========Апарты=============
            //=============================
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM apartment WHERE money_tax <= (round(price * '" + CurrentTax * Bonus + "' + '10', 0) * '" + TaxDays2 + "') * '-1' AND user_id > '0'").Rows)
            {
                int price = Convert.ToInt32((int) row["price"] / 2);
                if ((int) row["money_tax"] < -100000)
                    price = Convert.ToInt32(price * 2 * 1.3);
                
                bool isPlayerOnline = false;
                foreach (var player in new PlayerList())
                {
                    if (!Server.Sync.Data.Has(User.GetServerId(player), "id")) continue;
                    if ((int) Server.Sync.Data.Get(User.GetServerId(player), "id") == (int) row["user_id"])
                    {
                        isPlayerOnline = true;
                        Server.Sync.Data.Set(User.GetServerId(player), "apartment_id", 0);
                        User.AddBankMoney(player, price);
                        
                        TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Ваши апартаменты были изъяты государством за неуплату");
                        BankNotification(player, (int) Server.Sync.Data.Get(User.GetServerId(player), "bank_prefix"), price);
                        
                        User.UpdateAllData(player);
                    }
                }

                if (!isPlayerOnline)
                    Appi.MySql.ExecuteQuery("UPDATE users SET money_bank = money_bank + '" + price + "', apartment_id = '0', money_tax = '0' WHERE id = '" + (int) row["user_id"] + "'");
                
                Apartment.Save((int) row["id"], "", 0);
                
                AdWithNotification($"Апартаменты №{(int) row["id"]} поступила в продажу");
                
                Main.SaveLog("SELL_UNACTIVE", $"USER: {(int) row["user_id"]} APRT {(int) row["id"]}");
            }
        }

        public static void RemoveTax()
        {
            Appi.MySql.ExecuteQuery("UPDATE houses SET money_tax = money_tax - (round((price * '" + CurrentTax * Bonus + "' + '" + TaxMin +  "') / '7', 0)) WHERE id_user > 0");
            Appi.MySql.ExecuteQuery("UPDATE condo SET money_tax = money_tax - (round((price * '" + CurrentTax * Bonus + "' + '" + TaxMin +  "') / '7', 0)) WHERE id_user > 0");
            Appi.MySql.ExecuteQuery("UPDATE apartment SET money_tax = money_tax - (round((price * '" + CurrentTax * Bonus + "' + '" + TaxMin +  "') / '7', 0)) WHERE user_id > 0");
            Appi.MySql.ExecuteQuery("UPDATE business SET money_tax = money_tax - (round((price * '" + CurrentTax * Bonus + "' + '" + TaxMin +  "') / '7', 0)) WHERE user_id > 0");
            Appi.MySql.ExecuteQuery("UPDATE stocks SET money_tax = money_tax - (round((price * '" + CurrentTax * Bonus + "' + '" + TaxMin +  "') / '7', 0)) WHERE user_id > 0");
            Appi.MySql.ExecuteQuery("UPDATE cars SET money_tax = money_tax - (round((price * '" + CurrentTax * Bonus + "' + '" + TaxMin +  "') / '7', 0)) WHERE id_user > 0");
            
            TriggerClientEvent("ARP:SendPlayerNotification", "~y~Не забудьте оплатить налог за ваше имущество");
            
            UpdateTax();
        }

        public static void PayTax([FromSource] Player player, int type, int sum, int score)
        {
            if (sum < 1)
            {
                TriggerClientEvent(player, "ARP:SendPlayerNotification", "Сумма должна быть больше нуля");
                return;
            }
            if (score.ToString()[0] == '1')
            {
                //=============================
                //=========Бизнесы=============
                //=============================
                foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM business WHERE score_tax = '" + score + "'").Rows)
                {
                    if (sum > (int) row["money_tax"] * -1)
                    {
                        TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Сумма оплаты не должна привышать суммы долга (#1)");
                        TriggerClientEvent(player, "ARP:SendPlayerNotification", $"~r~Ваш долг равен: ${(int) row["money_tax"]:#,#}");
                        return;
                    }
                    
                    Appi.MySql.ExecuteQuery("UPDATE business SET money_tax = '" + ((int) row["money_tax"] + sum) + "' WHERE score_tax = '" + score + "'");
                    
                    if (type == 0)
                        User.RemoveCashMoney(player, sum);
                    else
                        User.RemoveBankMoney(player, sum);
                    
                    TriggerClientEvent(player, "ARP:SendPlayerNotification", $"~g~Счёт {score} был оплачен на сумму ${sum:#,#}");
                    User.UpdateAllData(player);
                    
                    Server.Sync.Data.Set(-20000 + (int) row["id"], "money_tax", (int) row["money_tax"] + sum);
                    return;
                }
            }
            else if (score.ToString()[0] == '3')
            {
                //=============================
                //============Авто=============
                //=============================
                foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM cars WHERE score_tax = '" + score + "'").Rows)
                {
                    if (sum > (int) row["money_tax"] * -1)
                    {
                        TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Сумма оплаты не должна привышать суммы долга (#2)");
                        TriggerClientEvent(player, "ARP:SendPlayerNotification", $"~r~Ваш долг равен: ${(int) row["money_tax"]:#,#}");
                        return;
                    }
                    
                    Appi.MySql.ExecuteQuery("UPDATE cars SET money_tax = '" + ((int) row["money_tax"] + sum) + "' WHERE score_tax = '" + score + "'");
                    
                    if (type == 0)
                        User.RemoveCashMoney(player, sum);
                    else
                        User.RemoveBankMoney(player, sum);
                    
                    TriggerClientEvent(player, "ARP:SendPlayerNotification", $"~g~Счёт {score} был оплачен на сумму ${sum:#,#}");
                    User.UpdateAllData(player);
                    
                    Server.Sync.Data.Set(110000 + (int) row["id"], "money_tax", (int) row["money_tax"] + sum);
                    return;
                }
            }
            else if (score.ToString()[0] == '4')
            {
                //=============================
                //============Квартиры=============
                //=============================
                foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM condo WHERE score_tax = '" + score + "'").Rows)
                {
                    if (sum > (int) row["money_tax"] * -1)
                    {
                        TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Сумма оплаты не должна привышать суммы долга (#3)");
                        TriggerClientEvent(player, "ARP:SendPlayerNotification", $"~r~Ваш долг равен: ${(int) row["money_tax"]:#,#}");
                        return;
                    }
                    
                    Appi.MySql.ExecuteQuery("UPDATE condo SET money_tax = '" + ((int) row["money_tax"] + sum) + "' WHERE score_tax = '" + score + "'");
                    
                    if (type == 0)
                        User.RemoveCashMoney(player, sum);
                    else
                        User.RemoveBankMoney(player, sum);
                    
                    TriggerClientEvent(player, "ARP:SendPlayerNotification", $"~g~Счёт {score} был оплачен на сумму ${sum:#,#}");
                    User.UpdateAllData(player);
                    
                    Server.Sync.Data.Set(300000 + (int) row["id"], "money_tax", (int) row["money_tax"] + sum);
                    return;
                }
            }
            else if (score.ToString()[0] == '5')
            {
                //=============================
                //============Дома=============
                //=============================
                foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM houses WHERE score_tax = '" + score + "'").Rows)
                {
                    if (sum > (int) row["money_tax"] * -1)
                    {
                        TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Сумма оплаты не должна привышать суммы долга (#3)");
                        TriggerClientEvent(player, "ARP:SendPlayerNotification", $"~r~Ваш долг равен: ${(int) row["money_tax"]:#,#}");
                        return;
                    }
                    
                    Appi.MySql.ExecuteQuery("UPDATE houses SET money_tax = '" + ((int) row["money_tax"] + sum) + "' WHERE score_tax = '" + score + "'");
                    
                    if (type == 0)
                        User.RemoveCashMoney(player, sum);
                    else
                        User.RemoveBankMoney(player, sum);
                    
                    TriggerClientEvent(player, "ARP:SendPlayerNotification", $"~g~Счёт {score} был оплачен на сумму ${sum:#,#}");
                    User.UpdateAllData(player);
                    
                    Server.Sync.Data.Set(100000 + (int) row["id"], "money_tax", (int) row["money_tax"] + sum);
                    return;
                }
            }
            else if (score.ToString()[0] == '7')
            {
                //=============================
                //==========Апарты=============
                //=============================
                foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM apartment WHERE score_tax = '" + score + "'").Rows)
                {
                    if (sum > (int) row["money_tax"] * -1)
                    {
                        TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Сумма оплаты не должна привышать суммы долга (#4)");
                        TriggerClientEvent(player, "ARP:SendPlayerNotification", $"~r~Ваш долг равен: ${(int) row["money_tax"]:#,#}");
                        return;
                    }
                    
                    Appi.MySql.ExecuteQuery("UPDATE apartment SET money_tax = '" + ((int) row["money_tax"] + sum) + "' WHERE score_tax = '" + score + "'");
                    
                    if (type == 0)
                        User.RemoveCashMoney(player, sum);
                    else
                        User.RemoveBankMoney(player, sum);
                    
                    TriggerClientEvent(player, "ARP:SendPlayerNotification", $"~g~Счёт {score} был оплачен на сумму ${sum:#,#}");
                    User.UpdateAllData(player);
                    
                    Server.Sync.Data.Set(-100000 + (int) row["id"], "money_tax", (int) row["money_tax"] + sum);
                    return;
                }
            }
            else if (score.ToString()[0] == '9')
            {
                //=============================
                //==========Склады=============
                //=============================
                foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM stocks WHERE score_tax = '" + score + "'").Rows)
                {
                    if (sum > (int) row["money_tax"] * -1)
                    {
                        TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Сумма оплаты не должна привышать суммы долга (#4)");
                        TriggerClientEvent(player, "ARP:SendPlayerNotification", $"~r~Ваш долг равен: ${(int) row["money_tax"]:#,#}");
                        return;
                    }
                    
                    Appi.MySql.ExecuteQuery("UPDATE stocks SET money_tax = '" + ((int) row["money_tax"] + sum) + "' WHERE score_tax = '" + score + "'");
                    
                    if (type == 0)
                        User.RemoveCashMoney(player, sum);
                    else
                        User.RemoveBankMoney(player, sum);
                    
                    TriggerClientEvent(player, "ARP:SendPlayerNotification", $"~g~Счёт {score} был оплачен на сумму ${sum:#,#}");
                    User.UpdateAllData(player);
                    
                    Server.Sync.Data.Set(200000 + (int) row["id"], "money_tax", (int) row["money_tax"] + sum);
                    return;
                }
            }
                
            TriggerClientEvent(player, "ARP:SendPlayerNotification", "~r~Номер счёта не найден");
        }

        public static async void UpdateTax()
        {
            await Delay(10000);
            
            //=============================
            //============Квартиры=============
            //=============================
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM condo WHERE id_user > '0'").Rows)
            {
                Server.Sync.Data.Set(300000 + (int) row["id"], "money_tax", (int) row["money_tax"]);
                Server.Sync.Data.Set(300000 + (int) row["id"], "score_tax", (int) row["score_tax"]);
            }
            
            //=============================
            //============Дома=============
            //=============================
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM houses WHERE id_user > '0'").Rows)
            {
                Server.Sync.Data.Set(100000 + (int) row["id"], "money_tax", (int) row["money_tax"]);
                Server.Sync.Data.Set(100000 + (int) row["id"], "score_tax", (int) row["score_tax"]);
            }
            
            //=============================
            //=========Бизнесы=============
            //=============================
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM business WHERE user_id > '0'").Rows)
            {
                Server.Sync.Data.Set(-20000 + (int) row["id"], "money_tax", (int) row["money_tax"]);
                Server.Sync.Data.Set(-20000 + (int) row["id"], "score_tax", (int) row["score_tax"]);
            }
            
            //=============================
            //============Авто=============
            //=============================
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM cars WHERE id_user > '0'").Rows)
            {
                Server.Sync.Data.Set(110000 + (int) row["id"], "money_tax", (int) row["money_tax"]);
                Server.Sync.Data.Set(110000 + (int) row["id"], "score_tax", (int) row["score_tax"]);
            }
            
            //=============================
            //==========Апарты=============
            //=============================
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM apartment WHERE user_id > '0'").Rows)
            {
                Server.Sync.Data.Set(-100000 + (int) row["id"], "money_tax", (int) row["money_tax"]);
                Server.Sync.Data.Set(-100000 + (int) row["id"], "score_tax", (int) row["score_tax"]);
            }
            
            //=============================
            //==========Склады=============
            //=============================
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM stocks WHERE user_id > '0'").Rows)
            {
                Server.Sync.Data.Set(200000 + (int) row["id"], "money_tax", (int) row["money_tax"]);
                Server.Sync.Data.Set(200000 + (int) row["id"], "score_tax", (int) row["score_tax"]);
            }
        }
    }
}