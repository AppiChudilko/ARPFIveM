using System;
using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Jobs
{
    public class Mail : BaseScript
    {
        public static void SendMail(int hId)
        {
            if (Client.Sync.Data.HasLocally(User.GetServerId(), "mail"))
            {
                if ((int) Client.Sync.Data.GetLocally(User.GetServerId(), "mail") > 0)
                {
                    Client.Sync.Data.Set(hId, "isMail", true);
                    Client.Sync.Data.SetLocally(User.GetServerId(), "mail", (int) Client.Sync.Data.GetLocally(User.GetServerId(), "mail") - 1);
                
                    Notification.SendWithTime($"~g~Вы отнесли почту ({(int) Client.Sync.Data.GetLocally(User.GetServerId(), "mail")}/10)");
                    User.GiveJobMoney(User.Data.job == "mail2" ? 12 : 6);
                    return;
                }
            }
            Notification.SendWithTime("~r~У Вас нет почты, возьмите из авто");
        }
        
        public static void TakeMail()
        {
            Client.Sync.Data.SetLocally(User.GetServerId(), "mail", 10); 
            Notification.SendWithTime("~g~Вы взяли почту из транспорта");
        }
    }
}