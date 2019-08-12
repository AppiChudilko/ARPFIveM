using CitizenFX.Core;
using Client.Managers;

namespace Client.Business
{
    public class Lic : BaseScript
    {
        public static readonly int A = 0;
        public static readonly int B = 1;
        public static readonly int C = 2;
        public static readonly int Ship = 3;
        public static readonly int Air = 4;

        public static async void BuyLic(int type)
        {
            if (User.Data.reg_status == 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_67"));
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_68"));
                return;
            }
            
            switch (type)
            {
                case 0:
                    if (!User.Data.a_lic)
                    {
                        if (User.GetMoneyWithoutSync() < 75)
                        {
                            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                            return;
                        }

                        User.Data.a_lic = true;
                        Sync.Data.Set(User.GetServerId(), "a_lic", true);
                        
                        User.RemoveMoney(75);
                        Coffer.AddMoney(75);
                        
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_69"));
                        break;
                    }
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_70"));
                    break;
                case 1:
                    if (!User.Data.b_lic)
                    {
                        if (User.GetMoneyWithoutSync() < 200)
                        {
                            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                            return;
                        }

                        User.Data.b_lic = true;
                        Sync.Data.Set(User.GetServerId(), "b_lic", true);
                        
                        User.RemoveMoney(200);
                        Coffer.AddMoney(200);
                        
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_69"));
                        break;
                    }
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_70"));
                    break;
                case 2:
                    if (!User.Data.c_lic)
                    {
                        if (User.GetMoneyWithoutSync() < 600)
                        {
                            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                            return;
                        }

                        User.Data.c_lic = true;
                        Sync.Data.Set(User.GetServerId(), "c_lic", true);
                        
                        User.RemoveMoney(600);
                        Coffer.AddMoney(600);
                        
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_69"));
                        break;
                    }
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_70"));
                    break;
                case 3:
                    if (!User.Data.air_lic)
                    {
                        if (User.GetMoneyWithoutSync() < 1200)
                        {
                            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                            return;
                        }

                        User.Data.air_lic = true;
                        Sync.Data.Set(User.GetServerId(), "air_lic", true);
                        
                        User.RemoveMoney(1200);
                        Coffer.AddMoney(1200);
                        
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_69"));
                        break;
                    }
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_70"));
                    break;
                case 4:
                    if (!User.Data.ship_lic)
                    {
                        if (User.GetMoneyWithoutSync() < 920)
                        {
                            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                            return;
                        }

                        User.Data.ship_lic = true;
                        Sync.Data.Set(User.GetServerId(), "ship_lic", true);
                        
                        User.RemoveMoney(920);
                        Coffer.AddMoney(920);
                        
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_69"));
                        break;
                    }
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_70"));
                    break;
            }
        }
    }
}