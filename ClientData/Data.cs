using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;

namespace Client.Sync
{
    public class Data : BaseScript
    {
        public static bool Debug = false;
        public static bool ShowSyncMessage = false;
        
        /*private static dynamic _dataGet = null;
        private static dynamic _dataGetAll = null;
        private static bool _dataHas = false;*/
        
        private static readonly Dictionary<string, dynamic> _dataGet = new Dictionary<string, dynamic>();
        private static readonly Dictionary<string, dynamic> _dataGetAll = new Dictionary<string, dynamic>();
        private static readonly Dictionary<string, bool> _dataHas = new Dictionary<string, bool>();
        
        private static readonly Dictionary<string, bool> _isServerGetCallBack = new Dictionary<string, bool>();
        private static readonly Dictionary<string, bool> _isServerGetAllCallBack = new Dictionary<string, bool>();
        private static readonly Dictionary<string, bool> _isServerHasCallBack = new Dictionary<string, bool>();
        
        /*private static bool _isServerGetCallBack = false;
        private static bool _isServerGetAllCallBack = false;
        private static bool _isServerHasCallBack = false;*/
        
        private static readonly Dictionary<int, Dictionary<string, object>> _data = new Dictionary<int, Dictionary<string, object>>();
        
        public Data()
        {
            EventHandlers.Add("Sync:Client:Data:Get", new Action<int, dynamic>(GetServer));
            EventHandlers.Add("Sync:Client:Data:GetAll", new Action<int, dynamic>(GetAllServer));
            EventHandlers.Add("Sync:Client:Data:Has", new Action<int, bool>(HasServer));
        }
        
        public static void SetLocally(int id, string key, object value)
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
                    TriggerServerEvent("Sync:Server:Data:Debug", $"[SETLOCAL] ID: {id}, KEY: {key}, OBJECT: {value}");
            }
        }
        
        public static void ResetLocally(int id, string key)
        {
            lock (_data)
            {
                if (!_data.ContainsKey(id) || !_data[id].ContainsKey(key)) return;
                
                _data[id].Remove(key);
                
                if (Debug)
                    TriggerServerEvent("Sync:Server:Data:Debug", $"[RESETLOCAL] ID: {id}, KEY: {key}");
            }
        }
        
        public static dynamic GetLocally(int id, string key)
        {
            lock (_data)
            {
                if (Debug)
                    TriggerServerEvent("Sync:Server:Data:Debug", $"[GETLOCAL] ID: {id}, KEY: {key}");
                
                return _data.ContainsKey(id) ? _data[id].Get(key) : null;
            }
        }
        
        public static bool HasLocally(int id, string key)
        {
            lock (_data)
            {
                if (Debug)
                    TriggerServerEvent("Sync:Server:Data:Debug", $"[HASLOCAL] ID: {id}, KEY: {key}");
                
                return _data.ContainsKey(id) && _data[id].ContainsKey(key);
            }
        }
        
        public static string[] GetKeyAllLocally(int id)
        {
            lock (_data)
            {
                return _data.ContainsKey(id) ? _data[id].Select(pair => pair.Key).ToArray() : new string[0];
            }
        }
        
        public static Dictionary<string, object> GetAllLocally(int id)
        {
            lock (_data)
            {
                return _data.ContainsKey(id) ? _data[id] : null;
            }
        }
        
        public static void Set(int id, string key, object value)
        {
            TriggerServerEvent("Sync:Server:Data:Set", id, key, value);
                
            if (Debug)
                TriggerServerEvent("Sync:Server:Data:Debug", $"[SET-CLIENT] ID: {id}, KEY: {key}, OBJECT: {value}", "");
        }
        
        public static void Reset(int id, string key)
        {
            TriggerServerEvent("Sync:Server:Data:Reset", id, key);
                
            if (Debug)
                TriggerServerEvent("Sync:Server:Data:Debug", $"[RESET] ID: {id}, KEY: {key}", "");
        }
        
        public static async Task<dynamic> Get(int id, string key, int waitMs = 500)
        {
            TriggerServerEvent("Sync:Server:Data:Get", id, key);
            
            if (!_isServerGetCallBack.ContainsKey(id.ToString()))
                _isServerGetCallBack.Add(id.ToString(), false);
            
            while (!_isServerGetCallBack[id.ToString()] && waitMs > 0)
            {
                waitMs--;
                await Delay(1);
            }
                
            if (Debug)
                TriggerServerEvent("Sync:Server:Data:Debug", $"[GET] ID: {id}, KEY: {key}", "");

            dynamic returnData = null;
            if (_dataGet.ContainsKey(id.ToString()))
                returnData = _dataGet[id.ToString()];
            
            ResetGetCallback(id);
            return returnData;
        }
        
        public static async Task<dynamic> GetAll(int id, int waitMs = 500)
        {
            try
            {
                if (ShowSyncMessage)
                    Screen.LoadingPrompt.Show("Синхронизация данных", LoadingSpinnerType.SocialClubSaving);
                TriggerServerEvent("Sync:Server:Data:GetAll", id);
                
                if (!_isServerGetAllCallBack.ContainsKey(id.ToString()))
                    _isServerGetAllCallBack.Add(id.ToString(), false);
                
                while (!_isServerGetAllCallBack[id.ToString()] && waitMs > 0)
                {
                    waitMs--;
                    await Delay(1);
                }
                    
                if (Debug)
                    TriggerServerEvent("Sync:Server:Data:Debug", $"[GETALL] ID: {id}", "");
    
                dynamic returnData = null;
                if (_dataGetAll.ContainsKey(id.ToString()))
                    returnData = _dataGetAll[id.ToString()];
                
                ResetGetAllCallback(id);
                return returnData;
            }
            catch (Exception e)
            {
                CitizenFX.Core.Debug.WriteLine($"GETALL {e}");
                Screen.LoadingPrompt.Hide();
                ResetGetAllCallback(id);
                throw;
            }
        }
        
        public static async Task<bool> Has(int id, string key, int waitMs = 500)
        {
            TriggerServerEvent("Sync:Server:Data:Has", id, key);
            
            if (!_isServerHasCallBack.ContainsKey(id.ToString()))
                _isServerHasCallBack.Add(id.ToString(), false);
            
            while (!_isServerHasCallBack[id.ToString()] && waitMs > 0)
            {
                waitMs--;
                await Delay(1);
            }
                
            if (Debug)
                TriggerServerEvent("Sync:Server:Data:Debug", $"[HAS] ID: {id}, KEY: {key}", "");

            bool returnData = false;
            if (_dataHas.ContainsKey(id.ToString()))
                returnData = _dataHas[id.ToString()];
            
            ResetHasCallback(id);
            return returnData;
        }
        
        private static async void GetServer(int id, dynamic callback)
        {
            int waitMs = 10000;
            while (_isServerGetCallBack.ContainsKey(id.ToString()) && _isServerGetCallBack[id.ToString()] && waitMs > 0)
            {
                waitMs--;
                await Delay(1);
            }
            
            if (_dataGet.ContainsKey(id.ToString()))
                _dataGet[id.ToString()] = callback;
            else
                _dataGet.Add(id.ToString(), callback);

            if (_isServerGetCallBack.ContainsKey(id.ToString()))
                _isServerGetCallBack[id.ToString()] = true;
            else
                _isServerGetCallBack.Add(id.ToString(), false);
        }
        
        private static async void GetAllServer(int id, dynamic callback)
        {
            int waitMs = 10000;
            while (_isServerGetAllCallBack.ContainsKey(id.ToString()) && _isServerGetAllCallBack[id.ToString()] && waitMs > 0)
            {
                waitMs--;
                await Delay(1);
            }
            
            if (_dataGetAll.ContainsKey(id.ToString()))
                _dataGetAll[id.ToString()] = callback;
            else
                _dataGetAll.Add(id.ToString(), callback);
            
            if (_isServerGetAllCallBack.ContainsKey(id.ToString()))
                _isServerGetAllCallBack[id.ToString()] = true;
            else
                _isServerGetAllCallBack.Add(id.ToString(), false);
        }
        
        private static async void HasServer(int id, bool callback)
        {
            int waitMs = 10000;
            while (_isServerHasCallBack.ContainsKey(id.ToString()) && _isServerHasCallBack[id.ToString()] && waitMs > 0)
            {
                waitMs--;
                await Delay(1);
            }
            
            if (_dataHas.ContainsKey(id.ToString()))
                _dataHas[id.ToString()] = callback;
            else
                _dataHas.Add(id.ToString(), callback);
            
            if (_isServerHasCallBack.ContainsKey(id.ToString()))
                _isServerHasCallBack[id.ToString()] = true;
            else
                _isServerHasCallBack.Add(id.ToString(), false);
        }
        
        private static void ResetHasCallback(int id)
        {
            if (_dataHas.ContainsKey(id.ToString()))
                _dataHas[id.ToString()] = false;
            else
                _dataHas.Add(id.ToString(), false);
            
            if (_isServerHasCallBack.ContainsKey(id.ToString()))
                _isServerHasCallBack[id.ToString()] = false;
            else
                _isServerHasCallBack.Add(id.ToString(), false);
        }
        
        private static void ResetGetCallback(int id)
        {
            if (_dataGet.ContainsKey(id.ToString()))
                _dataGet[id.ToString()] = null;
            else
                _dataGet.Add(id.ToString(), null);
            
            if (_isServerGetCallBack.ContainsKey(id.ToString()))
                _isServerGetCallBack[id.ToString()] = false;
            else
                _isServerGetCallBack.Add(id.ToString(), false);
        }
        
        private static void ResetGetAllCallback(int id)
        {
            if (_dataGetAll.ContainsKey(id.ToString()))
                _dataGetAll[id.ToString()] = null;
            else
                _dataGetAll.Add(id.ToString(), null);
            
            if (_isServerGetAllCallBack.ContainsKey(id.ToString()))
                _isServerGetAllCallBack[id.ToString()] = false;
            else
                _isServerGetAllCallBack.Add(id.ToString(), false);
            
            if (ShowSyncMessage)
                Screen.LoadingPrompt.Hide();
        }
    }
}