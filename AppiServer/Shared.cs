using System;
using CitizenFX.Core;

namespace Server
{
    public class Shared : BaseScript 
    {
        public Shared()
        {
            EventHandlers.Add("ARP:TriggerEventToAllPlayers", new Action<string, int, object, object, object, object, object, object, object, object, object, object>(TriggerEventToAllPlayers));
            EventHandlers.Add("ARP:TriggerEventToPlayer", new Action<int, string, int, object, object, object, object, object, object, object, object, object, object>(TriggerEventToPlayer));
            EventHandlers.Add("ARP:SharedServer:Cuff", new Action<int>(Cuff));
            EventHandlers.Add("ARP:SharedServer:SetWaypoint", new Action<int, float, float>(SetWaypoint));
            EventHandlers.Add("ARP:SharedServer:SetWaypointToDep", new Action<float, float>(SetWaypointToDep));
            EventHandlers.Add("ARP:SharedServer:SetWaypointToFraction", new Action<int, float, float>(SetWaypointToFraction));
        }

        public static void TriggerEventToAllPlayers(string eventName, int countArgs = 0, object args1 = null, object args2 = null, object args3 = null, object args4 = null, object args5 = null, object args6 = null, object args7 = null, object args8 = null, object args9 = null, object args10 = null)
        {
            switch (countArgs)
            {
                case 0:
                    TriggerClientEvent(eventName);
                    break;
                case 1:
                    TriggerClientEvent(eventName, args1);
                    break;
                case 2:
                    TriggerClientEvent(eventName, args1, args2);
                    break;
                case 3:
                    TriggerClientEvent(eventName, args1, args2, args3);
                    break;
                case 4:
                    TriggerClientEvent(eventName, args1, args2, args3, args4);
                    break;
                case 5:
                    TriggerClientEvent(eventName, args1, args2, args3, args4, args5);
                    break;
                case 6:
                    TriggerClientEvent(eventName, args1, args2, args3, args4, args5, args6);
                    break;
                case 7:
                    TriggerClientEvent(eventName, args1, args2, args3, args4, args5, args6, args7);
                    break;
                case 8:
                    TriggerClientEvent(eventName, args1, args2, args3, args4, args5, args6, args7, args8);
                    break;
                case 9:
                    TriggerClientEvent(eventName, args1, args2, args3, args4, args5, args6, args7, args8, args9);
                    break;
                case 10:
                    TriggerClientEvent(eventName, args1, args2, args3, args4, args5, args6, args7, args8, args9, args10);
                    break;
            }
        }

        public static void TriggerEventToPlayer(int serverId, string eventName, int countArgs = 0, object args1 = null, object args2 = null, object args3 = null, object args4 = null, object args5 = null, object args6 = null, object args7 = null, object args8 = null, object args9 = null, object args10 = null)
        {
            foreach (var pl in new PlayerList())
            {
                if (User.GetServerId(pl) != serverId) continue;

                switch (countArgs)
                {
                    case 0:
                        TriggerClientEvent(pl, eventName);
                        break;
                    case 1:
                        TriggerClientEvent(pl, eventName, args1);
                        break;
                    case 2:
                        TriggerClientEvent(pl, eventName, args1, args2);
                        break;
                    case 3:
                        TriggerClientEvent(pl, eventName, args1, args2, args3);
                        break;
                    case 4:
                        TriggerClientEvent(pl, eventName, args1, args2, args3, args4);
                        break;
                    case 5:
                        TriggerClientEvent(pl, eventName, args1, args2, args3, args4, args5);
                        break;
                    case 6:
                        TriggerClientEvent(pl, eventName, args1, args2, args3, args4, args5, args6);
                        break;
                    case 7:
                        TriggerClientEvent(pl, eventName, args1, args2, args3, args4, args5, args6, args7);
                        break;
                    case 8:
                        TriggerClientEvent(pl, eventName, args1, args2, args3, args4, args5, args6, args7, args8);
                        break;
                    case 9:
                        TriggerClientEvent(pl, eventName, args1, args2, args3, args4, args5, args6, args7, args8, args9);
                        break;
                    case 10:
                        TriggerClientEvent(pl, eventName, args1, args2, args3, args4, args5, args6, args7, args8, args9, args10);
                        break;
                }

                return;
            }
        }

        public static void Cuff(int serverId)
        {
            foreach (var pl in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(pl))) continue;
                if (User.GetServerId(pl) != serverId) continue;
                TriggerClientEvent(pl, "ARP:SharedClient:Cuff");
            }
        }

        public static void SetWaypoint(int serverId, float x, float y)
        {
            foreach (var pl in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(pl))) continue;
                if (User.GetServerId(pl) != serverId) continue;
                TriggerClientEvent(pl, "ARP:SharedClient:SetWaypoint", x, y);
            }
        }

        public static void SetWaypointToDep(float x, float y)
        {
            foreach (var pl in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(pl))) continue;
                int fractionId = (int) Sync.Data.Get(User.GetServerId(pl), "fraction_id");
                if (fractionId == 1 || fractionId == 2 || fractionId == 3 || fractionId == 7 || fractionId == 16)
                    TriggerClientEvent(pl, "ARP:SharedClient:SetWaypoint", x, y);
            }
        }

        public static void SetWaypointToFraction(int fractionId, float x, float y)
        {
            foreach (var pl in new PlayerList())
            {
                if (!User.IsLogin(User.GetServerId(pl))) continue;
                if ((int) Sync.Data.Get(User.GetServerId(pl), "fraction_id") != fractionId) continue;
                TriggerClientEvent(pl, "ARP:SharedClient:SetWaypoint", x, y);
            }
        }
    }
}