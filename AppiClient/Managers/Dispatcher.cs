using System;
using System.Collections.Generic;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class Dispatcher : BaseScript
    {
        public static List<EmsItem> EmsList = new List<EmsItem>();
        
        public Dispatcher()
        {
            EventHandlers.Add("ARP:AddDispatcherEms", new Action<string, string, string, string, string, float, float, float, bool>(AddDispatcherEms));
            EventHandlers.Add("ARP:AcceptDispatch", new Action<string>(AcceptDispatch));
        }

        public static void AcceptDispatch(string phone)
        {
            if (User.Data.phone_code + "-" + User.Data.phone == phone)
                Notification.SendPicture("Ваш вызов был принят, ожидайте", "Диспетчер", "911", "CHAR_CALL911", Notification.TypeChatbox);
        }

        public static async void AddDispatcherEms(string title, string desc, string street1, string street2, string time, float x, float y, float z, bool withCoord)
        {
            if (!User.IsSapd() && !User.IsEms() && !User.IsFib() && !User.IsSheriff()) return;
            
            EmsItem item = new EmsItem
            {
                Title = title,
                Desc = desc,
                Street1 = street1,
                Street2 = street2,
                Time = time,
                X = x,
                Y = y,
                Z = z,
                WithCoord = withCoord
            };

            EmsList.Add(item);
            
            Notification.SendPicture(desc, "Диспетчер", title, "CHAR_CALL911", Notification.TypeChatbox);
            await Delay(100);
            Notification.Send($"~y~Время:~s~ {time}");

            if (!withCoord) return;
            
            await Delay(50);
            Notification.Send($"~y~Район:~s~ {street1}");
            await Delay(50);
            Notification.Send($"~y~Улица:~s~ {street2}");
        }

        public static async void SendEms(string title, string desc, Vector3 pos, bool withCoord = true, bool isPlayer = false)
        {
            if (await Ctos.IsBlackout())
                return;

            var zone = UI.GetPlayerZoneName();
            var street = UI.GetPlayerStreetName();

            if (isPlayer)
            {
                //var pos = GetEntityCoords(GetPlayerPed(-1), true);
                if (User.GetPlayerVirtualWorld() > 50000)
                {
                    int vw = User.GetPlayerVirtualWorld() - 50000;
                    foreach (var item in Stock.StockGlobalDataList)
                    {
                        if (item.id != vw) continue;
                        pos = new Vector3(item.x, item.y, item.z);
                        street = World.GetStreetName(pos);
                        zone = World.GetZoneLocalizedName(pos);
                    }
                }
                else if (User.GetPlayerVirtualWorld() > 0)
                {
                    int vw = User.GetPlayerVirtualWorld();
                    foreach (var item in House.HouseGlobalDataList)
                    {
                        if (item.id != vw) continue;
                        pos = new Vector3(item.x, item.y, item.z);
                        street = World.GetStreetName(pos);
                        zone = World.GetZoneLocalizedName(pos);
                    }
                }
                else if (User.GetPlayerVirtualWorld() < 0)
                {
                    var currentData = await Apartment.GetAllData(User.GetPlayerVirtualWorld() * -1);
                    if (currentData.id > 0)
                    {
                        int i = currentData.build_id;
                        pos = new Vector3((float) Apartment.BuildList[i, 0], (float) Apartment.BuildList[i, 1], (float) Apartment.BuildList[i, 2]);
                        street = World.GetStreetName(pos);
                        zone = World.GetZoneLocalizedName(pos);
                    }
                }
            }
            
            string time = World.CurrentDayTime.Hours.ToString("D2") + ":" + World.CurrentDayTime.Minutes.ToString("D2");
            Shared.TriggerEventToAllPlayers("ARP:AddDispatcherEms", title, desc, zone, street, time, pos.X, pos.Y, pos.Z, withCoord);
        }

        public static void SendEms(string title, string desc, bool withCoord = true)
        {
            SendEms(title, desc, GetEntityCoords(GetPlayerPed(-1), true), withCoord, true);
        }
        
        public static async void SendNotification(string title, string desc, string desc2 = "", string desc3 = "")
        {
            if (await Ctos.IsBlackout())
                return;
            
            Notification.SendPictureToFraction(desc, "Диспетчер", title, "CHAR_CALL911", Notification.TypeChatbox, 2);
            Notification.SendPictureToFraction(desc, "Диспетчер", title, "CHAR_CALL911", Notification.TypeChatbox, 3);
            Notification.SendPictureToFraction(desc, "Диспетчер", title, "CHAR_CALL911", Notification.TypeChatbox, 16);
            Notification.SendPictureToFraction(desc, "Диспетчер", title, "CHAR_CALL911", Notification.TypeChatbox, 7);

            if (desc2 != "")
            {
                await Delay(100);
                Notification.SendToFraction(desc2, 2);
                Notification.SendToFraction(desc2, 3);
                Notification.SendToFraction(desc2, 16);
                Notification.SendToFraction(desc2, 7);
            }
            
            if (desc3 != "")
            {
                await Delay(100);
                Notification.SendToFraction(desc3, 2);
                Notification.SendToFraction(desc3, 3);
                Notification.SendToFraction(desc3, 16);
                Notification.SendToFraction(desc3, 7);
            }
        }
    }
}

public class EmsItem
{
    public string Title { get;set; }
    public string Desc { get;set; }
    public string Street1 { get;set; }
    public string Street2 { get;set; }
    public string Time { get;set; }
    public float X { get;set; }
    public float Y { get;set; }
    public float Z { get;set; }
    public bool WithCoord { get;set; }
}