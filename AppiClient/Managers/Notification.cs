using System;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class Notification : BaseScript
    {
        public static readonly int TypeChatbox = 1;
        public static readonly int TypeEmail = 2;
        public static readonly int TypeAddFriendRequest = 3;
        public static readonly int TypeNothing = 3;
        public static readonly int TypeRightJumpingArrow = 7;
        public static readonly int TypeRpIcon = 8;
        public static readonly int TypeMoneyIcon = 9;
        
        public Notification()
        {
            EventHandlers.Add("ARP:SendPlayerNotification", new Action<string, bool, bool>(Send));
            EventHandlers.Add("ARP:SendPlayerNotificationPicture", new Action<string, string, string, string, int>(SendPicture));
            EventHandlers.Add("ARP:SendPlayerSubTitle", new Action<string, int, bool>(SendSubtitle));
        }

        public static void Send(string message, bool blink = true, bool saveToBrief = true)
        {
            /*var rand = new Random();
            int idx = rand.Next(0, 50000);
            AddTextEntry($"textNotif{idx}", message);
            SetTextComponentFormat($"textNotif{idx}");*/
            
            SetNotificationTextEntry("THREESTRINGS");
            foreach (string msg in Main.StringToArray(message))
                if (msg != null)
                    AddTextComponentSubstringPlayerName(msg);
            DrawNotification(blink, saveToBrief);
        }
        
        public static void SendWithTime(string message, bool blink = true, bool saveToBrief = true)
        {
            Send($"[{Weather.Hour:D2}:{Weather.Min:D2}] {message}", blink, saveToBrief);
        }
        
        /*
        
        Title: Facebook / Fleeca / Адвокат / LSPD / EMS и прочее
        Subtitle: Номер или тема сообщения
        Text: Тема сообщения
        
        */
        
        public static async void SendPicture(string text, string title, string subtitle, string icon, int type)
        {
            if (icon == "WEB_LOSSANTOSPOLICEDEPT" || icon == "DIA_TANNOY")
            {
                RequestStreamedTextureDict(icon, true);
                if (!HasStreamedTextureDictLoaded(icon))
                    await Delay(10);
            }
            
            SetNotificationTextEntry("STRING");
            AddTextComponentString(text);
            SetNotificationMessage(icon, icon, true, type, title, subtitle);
            DrawNotification(false, true);
        }
        
        public static void SendPictureToAll(string text, string title, string subtitle, string icon, int type)
        {
            TriggerServerEvent("ARP:SendPlayerNotificationPictureToAll", text, title, subtitle, icon, type);
        }
        
        public static void SendPictureToDep(string text, string title, string subtitle, string icon, int type)
        {
            TriggerServerEvent("ARP:SendPlayerNotificationPictureToDep", text, title, subtitle, icon, type);
        }
        
        public static void SendPictureToFraction(string text, string title, string subtitle, string icon, int type, int fractionId)
        {
            TriggerServerEvent("ARP:SendPlayerNotificationPictureToFraction", text, title, subtitle, icon, type, fractionId);
        }
        
        public static void SendToFraction(string text, int fractionId)
        {
            TriggerServerEvent("ARP:SendPlayerNotificationToFraction", text, fractionId);
        }
        
        public static void SendToDep(string text)
        {
            TriggerServerEvent("ARP:SendPlayerNotificationToDep", text);
        }
        
        public static void SendPictureToJob(string text, string title, string subtitle, string icon, int type, string job)
        {
            TriggerServerEvent("ARP:SendPlayerNotificationPictureToJob", text, title, subtitle, icon, type, job);
        }
        
        public static void SendRadio(string text, string title, string subtitle, string num)
        {
            TriggerServerEvent("ARP:SendPlayerNotificationPictureToRadio", text, title, subtitle, num);
        }
        
        public static void SendSubtitle(string message, int duration = 5000, bool drawImmediately = true)
        {
            BeginTextCommandPrint("THREESTRINGS");
            foreach (var msg in Main.StringToArray(message))
                AddTextComponentSubstringPlayerName(msg);
            EndTextCommandPrint(duration, drawImmediately);
        }
    }
}