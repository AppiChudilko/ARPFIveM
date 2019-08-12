using System;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class Chat : BaseScript
    {
        public static bool IsChatOpen = false;

        public Chat()
        {
            //Tick += ChatTimer;
            //OnLoad();
            EventHandlers.Add("chatMessageCallback", new Action<string, string>(ChatMessageCallback));
            EventHandlers.Add("ARP:SendRadiusMessageToClient", new Action<string, string, float, float, float, bool>(SendRadiusMessageToClient));
        }
        
        public static void ChatMessageCallback(string msg, string p0)
        {
            if (IsStringNullOrEmpty(msg)) return;
            Main.SaveLog("Chat", $"{User.Data.rp_name}: {msg}");
            
            if (msg.Contains("/vipuninvite") && User.GetVipStatus() == "Hard")
            {
                User.Data.fraction_id = 0;
                User.Data.rank = 0;
                Client.Sync.Data.Set(User.GetServerId(), "rank", 0);
                Client.Sync.Data.Set(User.GetServerId(), "fraction_id", 0);
                Notification.SendWithTime("~g~Вы вышли из организации");
                return;
            }
            if (msg.Contains("/vipuninvite2") && (User.GetVipStatus() == "Hard" || User.GetVipStatus() == "Light"))
            {
                User.Data.fraction_id2 = 0;
                User.Data.rank2 = 0;
                Client.Sync.Data.Set(User.GetServerId(), "rank2", 0);
                Client.Sync.Data.Set(User.GetServerId(), "fraction_id2", 0);
                Notification.SendWithTime("~g~Вы вышли из организации");
                return;
            }
            if (msg.Contains("/rasform"))
            {
                if (User.Data.fraction_id2 > 0 && User.Data.rank2 == 11)
                {
                    TriggerServerEvent("ARP:DeleteUnofFraction", User.Data.fraction_id2);
                    User.Data.fraction_id2 = 0;
                    User.Data.rank2 = 0;
                }
                return;
            }
            
            Vector3 pos = GetEntityCoords(GetPlayerPed(-1), true);
            TriggerServerEvent("ARP:SendRadiusMessage", msg, pos.X, pos.Y, pos.Z);
        }
        
        public static void SendRadiusMessageToClient(string name, string message, float x, float y, float z, bool withCommand = false)
        {
            var pos = new Vector3(x, y, z);
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            if (Main.GetDistanceToSquared(playerPos, pos) < 7f)
            {
                if (withCommand)
                    SendChatMessageWithCommand(message); 
                else
                    SendChatMessage(name, message);   
            }
        }

        public static void SendChatMessage(string name, string message)
        {
            TriggerEvent("chatSendMessage", $"[{DateTime.Now:HH:mm:ss}] <span style=\"color: #03A9F4\">Незнакомец ({name}) говорит</span>: {message}");
        }

        public static void SendChatInfoMessage(string name, string message, string color = "03A9F4")
        {
            TriggerEvent("chatSendMessage", $"[{DateTime.Now:HH:mm:ss}] <span style=\"color: #{color}\">{name}</span>: {message}");
        }

        public static void SendChatMessageWithCommand(string message)
        {
            TriggerEvent("chatSendMessage", $"[{DateTime.Now:HH:mm:ss}] {message}");
        }

        public static void SendCommand(string message)
        {
            ChatMessageCallback(message, "");
        }

        public static void SendMeCommand(string message)
        {
            ChatMessageCallback($"/me {message}", "");
        }

        public static void SendDoCommand(string message)
        {
            ChatMessageCallback($"/do {message}", "");
        }

        public static void SendTryCommand(string message)
        {
            ChatMessageCallback($"/try {message}", "");
        }
    }
}