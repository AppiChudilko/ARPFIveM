using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CitizenFX.Core;

namespace Server.Sync
{
    public class Data : BaseScript
    {
        public static bool Debug = false;
        private static readonly Dictionary<int, Dictionary<string, object>> _data = new Dictionary<int, Dictionary<string, object>>();
        
        public Data()
        {
            EventHandlers.Add("Sync:Server:Data:Set", new Action<int, string, object>(SetClient));
            EventHandlers.Add("Sync:Server:Data:Reset", new Action<int, string>(Reset));
            EventHandlers.Add("Sync:Server:Data:Get", new Action<Player, int, string>(GetClient));
            EventHandlers.Add("Sync:Server:Data:GetAll", new Action<Player, int>(GetAllClient));
            EventHandlers.Add("Sync:Server:Data:Has", new Action<Player, int, string>(HasClient));
            EventHandlers.Add("Sync:Server:Data:Debug", new Action<string>(SaveLog));
        }
        
        public static void SetClient(int id, string key, object value)
        {
            if (Debug)
                SaveLog($"[SET-CLIENT-SRV] ID: {id}, KEY: {key}, OBJECT: {value}");
            Set(id, key, value);
        }
        
        public static void Set(int id, string key, object value)
        {
            lock (_data)
            {
                if (_data.ContainsKey(id))
                {
                    _data[id].Set(key, value);
                }
                else
                {
                    _data.Add(id, new Dictionary<string, object>());
                    _data[id].Set(key, value);
                }
                
                if (Debug)
                    SaveLog($"[SET] ID: {id}, KEY: {key}, OBJECT: {value}");
            }
        }
        
        public static void Reset(int id, string key)
        {
            lock (_data)
            {
                if (!_data.ContainsKey(id) || !_data[id].ContainsKey(key)) return;
                
                _data[id].Remove(key);
                
                if (Debug)
                    SaveLog($"[RESET] ID: {id}, KEY: {key}");
            }
        }
        
        public static dynamic Get(int id, string key)
        {
            lock (_data)
            {
                if (Debug)
                    SaveLog($"[GET] ID: {id}, KEY: {key}");
                
                return _data.ContainsKey(id) ? _data[id].Get(key) : null;
            }
        }
        
        public static bool Has(int id, string key)
        {
            lock (_data)
            {
                if (Debug)
                    SaveLog($"[HAS] ID: {id}, KEY: {key}");
                
                return _data.ContainsKey(id) && _data[id].ContainsKey(key);
            }
        }
        
        public static string[] GetKeyAll(int id)
        {
            lock (_data)
            {
                return _data.ContainsKey(id) ? _data[id].Select(pair => pair.Key).ToArray() : new string[0];
            }
        }
        
        public static Dictionary<string, object> GetAll(int id)
        {
            lock (_data)
            {
                return _data.ContainsKey(id) ? _data[id] : null;
            }
        }
        
        public static void SaveLog(string log)
        {
            try
            {
                File.AppendAllText($"sync-debug.log", $"[{DateTime.Now:dd/MM/yyyy}] [{DateTime.Now:HH:mm:ss tt}] {log}\n");
            }
            catch (Exception e)
            {
                SaveLog($"ServerLog TRY-CATCH {log} | {e}");
            }
        }
        
        private static void GetAllClient([FromSource] Player player, int id)
        {
            if (Debug)
                SaveLog($"[GET_ALL_CLIENT] ID: {id}");
            
            TriggerClientEvent(player, "Sync:Client:Data:GetAll", id, GetAll(id));
        }
        
        private static void GetClient([FromSource] Player player, int id, string key)
        {
            if (Debug)
                SaveLog($"[GETCLIENT] ID: {id}, KEY: {key}");
            
            TriggerClientEvent(player, "Sync:Client:Data:Get", id, Get(id, key));
        }
        
        private static void HasClient([FromSource] Player player, int id, string key)
        {
            if (Debug)
                SaveLog($"[HASCLIENT] ID: {id}, KEY: {key}");
            
            TriggerClientEvent(player, "Sync:Client:Data:Has", id, Has(id, key));
        }
    }
}