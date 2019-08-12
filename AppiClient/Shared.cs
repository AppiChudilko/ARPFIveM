using System;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Client
{
    public class Shared : BaseScript 
    {
        public Shared()
        {
            EventHandlers.Add("ARP:SharedClient:Cuff", new Action(User.Cuff));
            EventHandlers.Add("ARP:SharedClient:SetWaypoint", new Action<float, float>(User.SetWaypoint));
        }
        
        public static void TriggerEventToPlayer(int serverId, string eventName)
        {
            TriggerServerEvent("ARP:TriggerEventToPlayer", serverId, eventName, 0);
        }

        public static void TriggerEventToPlayer(int serverId, string eventName, object args1)
        {
            TriggerServerEvent("ARP:TriggerEventToPlayer", serverId, eventName, 1, args1);
        }

        public static void TriggerEventToPlayer(int serverId, string eventName, object args1, object args2)
        {
            TriggerServerEvent("ARP:TriggerEventToPlayer", serverId, eventName, 2, args1, args2);
        }

        public static void TriggerEventToPlayer(int serverId, string eventName, object args1, object args2, object args3)
        {
            TriggerServerEvent("ARP:TriggerEventToPlayer", serverId, eventName, 3, args1, args2, args3);
        }

        public static void TriggerEventToPlayer(int serverId, string eventName, object args1, object args2, object args3, object args4)
        {
            TriggerServerEvent("ARP:TriggerEventToPlayer", serverId, eventName, 4, args1, args2, args3, args4);
        }

        public static void TriggerEventToPlayer(int serverId, string eventName, object args1, object args2, object args3, object args4, object args5)
        {
            TriggerServerEvent("ARP:TriggerEventToPlayer", serverId, eventName, 5, args1, args2, args3, args4, args5);
        }

        public static void TriggerEventToPlayer(int serverId, string eventName, object args1, object args2, object args3, object args4, object args5, object args6)
        {
            TriggerServerEvent("ARP:TriggerEventToPlayer", serverId, eventName, 6, args1, args2, args3, args4, args5, args6);
        }

        public static void TriggerEventToPlayer(int serverId, string eventName, object args1, object args2, object args3, object args4, object args5, object args6, object args7)
        {
            TriggerServerEvent("ARP:TriggerEventToPlayer", serverId, eventName, 7, args1, args2, args3, args4, args5, args6, args7);
        }

        public static void TriggerEventToPlayer(int serverId, string eventName, object args1, object args2, object args3, object args4, object args5, object args6, object args7, object args8)
        {
            TriggerServerEvent("ARP:TriggerEventToPlayer", serverId, eventName, 8, args1, args2, args3, args4, args5, args6, args7, args8);
        }

        public static void TriggerEventToPlayer(int serverId, string eventName, object args1, object args2, object args3, object args4, object args5, object args6, object args7, object args8, object args9)
        {
            TriggerServerEvent("ARP:TriggerEventToPlayer", serverId, eventName, 9, args1, args2, args3, args4, args5, args6, args7, args8, args9);
        }

        public static void TriggerEventToPlayer(int serverId, string eventName, object args1, object args2, object args3, object args4, object args5, object args6, object args7, object args8, object args9, object args10)
        {
            TriggerServerEvent("ARP:TriggerEventToPlayer", serverId, eventName, 10, args1, args2, args3, args4, args5, args6, args7, args8, args9, args10);
        }

        public static void TriggerEventToAllPlayers(string eventName)
        {
            TriggerServerEvent("ARP:TriggerEventToAllPlayers", eventName, 0);
        }

        public static void TriggerEventToAllPlayers(string eventName, object args1)
        {
            TriggerServerEvent("ARP:TriggerEventToAllPlayers", eventName, 1, args1);
        }

        public static void TriggerEventToAllPlayers(string eventName, object args1, object args2)
        {
            TriggerServerEvent("ARP:TriggerEventToAllPlayers", eventName, 2, args1, args2);
        }

        public static void TriggerEventToAllPlayers(string eventName, object args1, object args2, object args3)
        {
            TriggerServerEvent("ARP:TriggerEventToAllPlayers", eventName, 3, args1, args2, args3);
        }

        public static void TriggerEventToAllPlayers(string eventName, object args1, object args2, object args3, object args4)
        {
            TriggerServerEvent("ARP:TriggerEventToAllPlayers", eventName, 4, args1, args2, args3, args4);
        }

        public static void TriggerEventToAllPlayers(string eventName, object args1, object args2, object args3, object args4, object args5)
        {
            TriggerServerEvent("ARP:TriggerEventToAllPlayers", eventName, 5, args1, args2, args3, args4, args5);
        }

        public static void TriggerEventToAllPlayers(string eventName, object args1, object args2, object args3, object args4, object args5, object args6)
        {
            TriggerServerEvent("ARP:TriggerEventToAllPlayers", eventName, 6, args1, args2, args3, args4, args5, args6);
        }

        public static void TriggerEventToAllPlayers(string eventName, object args1, object args2, object args3, object args4, object args5, object args6, object args7)
        {
            TriggerServerEvent("ARP:TriggerEventToAllPlayers", eventName, 7, args1, args2, args3, args4, args5, args6, args7);
        }

        public static void TriggerEventToAllPlayers(string eventName, object args1, object args2, object args3, object args4, object args5, object args6, object args7, object args8)
        {
            TriggerServerEvent("ARP:TriggerEventToAllPlayers", eventName, 8, args1, args2, args3, args4, args5, args6, args7, args8);
        }

        public static void TriggerEventToAllPlayers(string eventName, object args1, object args2, object args3, object args4, object args5, object args6, object args7, object args8, object args9)
        {
            TriggerServerEvent("ARP:TriggerEventToAllPlayers", eventName, 9, args1, args2, args3, args4, args5, args6, args7, args8, args9);
        }

        public static void TriggerEventToAllPlayers(string eventName, object args1, object args2, object args3, object args4, object args5, object args6, object args7, object args8, object args9, object args10)
        {
            TriggerServerEvent("ARP:TriggerEventToAllPlayers", eventName, 10, args1, args2, args3, args4, args5, args6, args7, args8, args9, args10);
        }

        public static void CallNative(string nativeName, params InputArgument[] args)
        {
            foreach(uint hash in Enum.GetValues(typeof(Hash)))
            {
                string name = Enum.GetName(typeof(Hash), hash);
                if (nativeName != name) continue;

                switch (args.GetLength(0))
                {
                    case 1:
                        Function.Call((Hash) hash, args[0]);
                        break;
                    case 2:
                        Function.Call((Hash) hash, args[0], args[1]);
                        break;
                    case 3:
                        Function.Call((Hash) hash, args[0], args[1], args[2]);
                        break;
                    case 4:
                        Function.Call((Hash) hash, args[0], args[1], args[2], args[3]);
                        break;
                    case 5:
                        Function.Call((Hash) hash, args[0], args[1], args[2], args[3], args[4]);
                        break;
                    case 6:
                        Function.Call((Hash) hash, args[0], args[1], args[2], args[3], args[4], args[5]);
                        break;
                    case 7:
                        Function.Call((Hash) hash, args[0], args[1], args[2], args[3], args[4], args[5], args[6]);
                        break;
                    case 8:
                        Function.Call((Hash) hash, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]);
                        break;
                    case 9:
                        Function.Call((Hash) hash, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]);
                        break;
                    case 10:
                        Function.Call((Hash) hash, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8], args[9]);
                        break;
                
                }
            }
        }

        public static void Cuff(int serverId)
        {
            TriggerServerEvent("ARP:SharedServer:Cuff", serverId);
        }

        public static void SetWaypoint(int serverId, float x, float y)
        {
            TriggerServerEvent("ARP:SharedServer:SetWaypoint", serverId, x, y);
        }

        public static void SetWaypointToDep(float x, float y)
        {
            TriggerServerEvent("ARP:SharedServer:SetWaypointToDep", x, y);
        }

        public static void SetWaypointToFraction(int fractionId, float x, float y)
        {
            TriggerServerEvent("ARP:SharedServer:SetWaypointToFraction", fractionId, x, y);
        }
    }
}