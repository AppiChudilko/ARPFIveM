using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Fractions
{
    public class Government : BaseScript
    {
        public static async void BuyTaxiLic()
        {
            if (User.Data.age == 18 && User.GetMonth() < 2)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_86"));
                return;
            }

            if (User.GetMoneyWithoutSync() < 50)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_87"));
                return;
            }

            if (User.Data.reg_status == 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_67"));
                return;
            }

            if (User.Data.taxi_lic)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_70"));
                return;
            }

            User.RemoveMoney(50);
            User.Data.taxi_lic = true;
            Sync.Data.Set(User.GetServerId(), "taxi_lic", true);
            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_88"));
        }
        
        public static void GiveRegStatus()
        {
            if (User.Data.reg_status > 1)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_89"));
                return;
            }

            User.Data.reg_status = 1;
            User.Data.reg_time = 186;
            Sync.Data.Set(User.GetServerId(), "reg_status", 1);
            Sync.Data.Set(User.GetServerId(), "reg_time", 186);
            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_90"));
        }
        
        public static void GivePosob()
        {
            if (User.Data.job == "" || User.Data.fraction_id > 7  && User.Data.fraction_id < 15 || User.Data.fraction_id == 0)
            {
                User.Data.posob = true;
                Sync.Data.Set(User.GetServerId(), "posob", true);
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_91"));
            }
            else
            {
                User.Data.posob = false;
                Sync.Data.Set(User.GetServerId(), "posob", false);
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_92"));
            }
        }
        
        public static void GivePension()
        {
            if (User.Data.age > 49)
            {
                User.Data.is_old_money = true;
                Sync.Data.Set(User.GetServerId(), "is_old_money", true);
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_93"));
            }
            else
            {
                User.Data.is_old_money = false;
                Sync.Data.Set(User.GetServerId(), "is_old_money", false);
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_94"));
            }
        }
        
        public static async void GetJob(string job)
        {
            await User.GetAllData();
            
            if (User.Data.reg_status == 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_67"));
                return;
            }

            if (!User.Data.b_lic)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_78"));
                return;
            }

            if (User.Data.age == 18 && User.GetMonth() < 6 && (job == "bus1" || job == "bus2" || job == "bus3" || job == "meh"))
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_95"));
                return;
            }

            if (User.Data.age == 18 && User.GetMonth() < 2 && !User.Data.taxi_lic  && (job == "taxi1" || job == "taxi2"))
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_96"));
                return;
            }
            
            if (User.Data.age == 18 && User.GetMonth() < 1 && (job == "trash" || job == "photo" || job == "mail" || job == "scrap" || job == "three"))
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_97"));
                return;
            }
            
            if (User.Data.age < 21 && job == "mail2")
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_98"));
                return;
            }
            
            if (User.Data.age < 21 && (job == "swater" || job == "sground") && User.Data.reg_status < 3)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_99"));
                return;
            }
            
            if (User.Data.age < 25 && job == "lawyer1" && !User.Data.law_lic && User.Data.reg_status < 3)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_100"));
                return;
            }
            
            if (User.Data.age < 30 && job == "lawyer2" && !User.Data.law_lic && User.Data.reg_status < 3)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_100"));
                return;
            }
            
            if (User.Data.age < 35 && job == "lawyer3" && !User.Data.law_lic && User.Data.reg_status < 3)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_100"));
                return;
            }

            User.Data.posob = false;
            Sync.Data.Set(User.GetServerId(), "posob", false);

            User.Data.job = job;
            Sync.Data.Set(User.GetServerId(), "job", job);
            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_101"));
        }
        
        public static void ResetJob()
        {
            User.Data.posob = false;
            Sync.Data.Set(User.GetServerId(), "posob", false);

            User.Data.job = "";
            Sync.Data.Set(User.GetServerId(), "job", "");
            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_102"));
        }
        
        public static void SetPosob(int money)
        {
            if (!User.IsGov()) return;
            if (money < 3 || money > 43)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_103", 3, 43));
                return;
            }

            Coffer.SetPosob(money);  
            TriggerServerEvent("ARP:SendPlayerNotificationPictureToAll", Lang.GetTextToPlayer("_lang_105", money), "~r~Maze~s~ Bank", Lang.GetTextToPlayer("_lang_104"), "CHAR_BANK_MAZE", Notification.TypeChatbox);
        }
        
        public static void SetNalog(int number)
        {
            if (!User.IsGov()) return;
            if (number < 1 || number > 20)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_103", 1, 20));
                return;
            }

            Coffer.SetNalog(number);  
            TriggerServerEvent("ARP:SendPlayerNotificationPictureToAll", Lang.GetTextToPlayer("_lang_107", number), "~r~Maze~s~ Bank", Lang.GetTextToPlayer("_lang_104"), "CHAR_BANK_MAZE", Notification.TypeChatbox);
        }
        
        public static void SetNalogBusiness(int number)
        {
            if (!User.IsGov()) return;
            if (number < 5 || number > 25)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_103", 5, 25));
                return;
            }

            Coffer.SetBizzNalog(number);  
            TriggerServerEvent("ARP:SendPlayerNotificationPictureToAll", Lang.GetTextToPlayer("_lang_108", number), "~r~Maze~s~ Bank", Lang.GetTextToPlayer("_lang_104"), "CHAR_BANK_MAZE", Notification.TypeChatbox);
        }
        
        public static void SetPension(int number)
        {
            if (!User.IsGov()) return;
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_103", 30, 140));
                return;
            }

            Coffer.SetMoneyOld(number);  
            TriggerServerEvent("ARP:SendPlayerNotificationPictureToAll", Lang.GetTextToPlayer("_lang_106", number), "~r~Maze~s~ Bank", Lang.GetTextToPlayer("_lang_104"), "CHAR_BANK_MAZE", Notification.TypeChatbox);
        }
    }
}