using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using Client.Vehicle;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class Voice : BaseScript
    {
        private static bool _pushToTalkRadio = false;
        private static bool _pushToTalk = false;
        private static string _voicestate = "loading";
        private static int _volume = 0;
        
        private static List<Player> _peers = new List<Player>();
        private static Dictionary<int, string> _radioList = new Dictionary<int, string>();
        
        public Voice()
        {
            Tick += TickTimer;
            Tick += Tick500Timer;
            Tick += SecTimer;
            Tick += Sec10Timer;
            
            EventHandlers.Add("ARP:UpdateRadioList", new Action<dynamic>(UpdateRadioList));
            EventHandlers.Add("ARP:PeerRadioMute", new Action<int, string, string>(PeerRadioMute));
            EventHandlers.Add("ARP:PeerRadioUnmute", new Action<int, string, string>(PeerRadioUnmute));
            EventHandlers.Add("ARP:SetVoiceState", new Action<string>(SetVoiceState));
            EventHandlers.Add("ARP:RequestPeerFailed", new Action<string>(RequestPeerFailed));
            EventHandlers.Add("ARP:RequestPeerSuccess", new Action<string>(RequestPeerSuccess));
        }

        public static async void Restart()
        {
            if (!User.IsLogin()) return;
            TriggerEvent("ARPVoice:Quit");
            Notification.SendWithTime("~b~Начинаю перезагрузку");
            await Delay(2000);
            Notification.SendWithTime("~b~Голосовой чат был перезагружен");
            var name = $"{Main.ServerName}_pl_{User.GetServerId()}";
            TriggerEvent("ARPVoice:Init", name);
            await Delay(2000);
            _peers.Clear();
        }

        public static void UpdateRadioList(dynamic data)
        {
            if (!User.IsLogin()) return;
            //Debug.WriteLine("[ARP] UPDATE RADIO LIST");
            _radioList.Clear();
            foreach (var item in (IDictionary<String, Object>) data)
                _radioList.Add(Convert.ToInt32(item.Key), item.Value.ToString());
        }

        public static void PeerRadioMute(int id, string peer, string walkieNum)
        {
            if (!User.IsLogin()) return;
            if (id == User.Data.id) return;
            Debug.WriteLine($"[ARP] PeerRadioMute {User.Data.s_radio_vol} | {User.Data.s_radio_balance}");
            TriggerEvent("ARPVoice:ChangeVolumeConsumers", $@"[{{""name"":""{peer}"", ""volume"":0, ""balance"":{User.Data.s_radio_balance}}}]");
        }

        public static void PeerRadioUnmute(int id, string peer, string walkieNum)
        {
            if (!User.IsLogin()) return;
            if (id == User.Data.id) return;
            if (walkieNum != User.Data.walkietalkie_num) return;
            if (User.Data.jail_time > 0) return;
            Debug.WriteLine($"[ARP] PeerRadioUnmute {User.Data.s_radio_vol} | {User.Data.s_radio_balance}");

            if (string.IsNullOrEmpty(peer))
            {
                Notification.SendWithTime("~r~Ошибка получения информации в рации");
                return;
            }
            
            var peerId = peer.Replace(Main.ServerName + "_pl_", "");
            int serverId = Convert.ToInt32(peerId);
            foreach (var p in new PlayerList())
                if (serverId == p.ServerId)
                    Debug.WriteLine($"[ARP] HAS PEER {serverId}");
            
            TriggerEvent("ARPVoice:ChangeVolumeConsumers", $@"[{{""name"":""{peer}"", ""volume"":{User.Data.s_radio_vol}, ""balance"":{User.Data.s_radio_balance}}}]");
            if (User.Data.s_radio_vol > 0)
                TriggerEvent("ARPSound:RadioPeer");
        }

        public static void RequestPeerFailed(string peer)
        {
            RemovePeer(peer);
        }

        public static void RequestPeerSuccess(string peer)
        {
            AddPeer(peer);
        }

        public static void AddPeer(string peer)
        {
            if (string.IsNullOrEmpty(peer)) return;
            peer = peer.Replace(Main.ServerName + "_pl_", "");
            int serverId = Convert.ToInt32(peer);
            foreach (var p in new PlayerList())
            {
                if (serverId != p.ServerId) continue;
                AddPeer(p);
                return;
            }
        }

        public static void AddPeer(Player p)
        {
            if (!_peers.Contains(p))
                _peers.Add(p);
        }

        public static void RemovePeer(string peer)
        {
            foreach (var p in _peers)
            {
                if ($"{Main.ServerName}_pl_{p.ServerId}" != peer) continue;
                RemovePeer(p);
                return;
            }
        }

        public static void RemovePeer(Player p)
        {
            if (_peers.Contains(p))
                _peers.Remove(p);
        }

        public static void AddStream(string peer)
        {
            TriggerEvent("ARPVoice:StreamIn", peer);
            AddPeer(peer);
        }

        public static void RemoveStream(string peer)
        {
            TriggerEvent("ARPVoice:StreamOut", peer);
            RemovePeer(peer);
        }

        public static void SetMicroEnable(bool enable)
        {
            _pushToTalk = enable;
            TriggerEvent(enable ? "ARPVoice:UnmuteMic" : "ARPVoice:MuteMic");
            SetPlayerTalkingOverride(PlayerId(), enable);
        }

        public static void SetRadioEnable(bool enable)
        {
            _pushToTalkRadio = enable;
            SetMicroEnable(enable);
            var name = $"{Main.ServerName}_pl_{User.GetServerId()}";
            Shared.TriggerEventToAllPlayers(enable ? "ARP:PeerRadioUnmute" : "ARP:PeerRadioMute", User.Data.id, name, User.Data.walkietalkie_num);
            TriggerEvent(enable ? "ARPSound:RadioOn" : "ARPSound:RadioOff");
            if (IsPedInAnyVehicle(GetPlayerPed(-1), true)) return;
            if (enable)
                User.PlayAnimation("random@arrests", "generic_radio_chatter");
            else
                User.StopAnimation();
        }

        public static bool IsMicroEnable()
        {
            return _pushToTalk;
        }

        public static bool IsRadioEnable()
        {
            return _pushToTalkRadio;
        }

        public static int GetVolume()
        {
            return _volume;
        }

        public static void SetVoiceState(string state)
        {
            _voicestate = state;
        }

        public static string GetVoiceState()
        {
            return _voicestate;
        }

        public static float GenerateVolume(Vector3 pos1, Vector3 pos2, Player p2)
        {
            int distance = 200;
            if (User.Voice == 0)
                distance = 50;
            if (User.Voice == 2)
                distance = 400;
            //int distance = 200;
            
            var s = GetDistanceBetweenCoords(pos1.X, pos1.Y, pos1.Z, pos2.X, pos2.Y, pos2.Z, true);
            var volume = -(s * s - distance) / (s * s * s * s + distance);

            var plPed = GetPlayerPed(-1);
            var plPed2 = GetPlayerPed(p2.Handle);
            
            if (volume < 0.0f)
                volume = 0.0f;
            else if (volume > User.Data.s_voice_vol)
                volume = User.Data.s_voice_vol;

            if (volume > 1)
                volume = 1;

            if (volume > 0)
            {
                if (!HasEntityClearLosToEntity(plPed, plPed2, 17))
                    volume = volume / 6;
            
                if (IsPedInAnyVehicle(plPed, true) && IsPedInAnyVehicle(plPed2, true))
                {
                    var v1 = new CitizenFX.Core.Vehicle(GetVehiclePedIsUsing(plPed));
                    var v2 = new CitizenFX.Core.Vehicle(GetVehiclePedIsUsing(plPed2));
    
                    bool isOpenVeh1 = VehInfo.GetClassName(v1.Model.Hash) != "Boats" &&
                                     VehInfo.GetClassName(v1.Model.Hash) != "Cycles" &&
                                     VehInfo.GetClassName(v1.Model.Hash) != "Motorcycles";
                    bool isOpenVeh2 = VehInfo.GetClassName(v2.Model.Hash) != "Boats" &&
                                     VehInfo.GetClassName(v2.Model.Hash) != "Cycles" &&
                                     VehInfo.GetClassName(v2.Model.Hash) != "Motorcycles";
                    
                    
                    bool v1Doors = true;
                    bool v2Doors = true;
                    foreach (var item in v1.Doors)
                        v1Doors = !item.IsBroken && !item.IsOpen && !item.IsFullyOpen;
                    foreach (var item in v2.Doors)
                        v2Doors = !item.IsBroken && !item.IsOpen && !item.IsFullyOpen;
                    
                    bool isAllUp1 = IsVehicleWindowIntact(v1.Handle, 0) && IsVehicleWindowIntact(v1.Handle, 1) && v1.Windows.AreAllWindowsIntact && v1.RoofState == VehicleRoofState.Closed && v1Doors && isOpenVeh1;
                    bool isAllUp2 = IsVehicleWindowIntact(v2.Handle, 0) && IsVehicleWindowIntact(v2.Handle, 1) && v2.Windows.AreAllWindowsIntact && v2.RoofState == VehicleRoofState.Closed && v2Doors && isOpenVeh2;
                    
                    if (v1.Handle != v2.Handle)
                    {
                        if (isAllUp1)
                            volume = volume / 5;
                        if (isAllUp2)
                            volume = volume / 5;
                    }
                }
                else if (IsPedInAnyVehicle(plPed, true))
                {
                    var v1 = new CitizenFX.Core.Vehicle(GetVehiclePedIsUsing(plPed));
                    bool isOpenVeh1 = VehInfo.GetClassName(v1.Model.Hash) != "Boats" &&
                                      VehInfo.GetClassName(v1.Model.Hash) != "Cycles" &&
                                      VehInfo.GetClassName(v1.Model.Hash) != "Motorcycles";
                    bool v1Doors = true;
                    foreach (var item in v1.Doors)
                        v1Doors = !item.IsBroken && !item.IsOpen && !item.IsFullyOpen;
                    bool isAllUp1 = IsVehicleWindowIntact(v1.Handle, 0) && IsVehicleWindowIntact(v1.Handle, 1) && v1.Windows.AreAllWindowsIntact && v1.RoofState == VehicleRoofState.Closed && v1Doors && isOpenVeh1;
                    if (isAllUp1)
                        volume = volume / 6;
                }
                else if (IsPedInAnyVehicle(plPed2, true))
                {
                    var v1 = new CitizenFX.Core.Vehicle(GetVehiclePedIsUsing(plPed2));
                    bool isOpenVeh1 = VehInfo.GetClassName(v1.Model.Hash) != "Boats" &&
                                      VehInfo.GetClassName(v1.Model.Hash) != "Cycles" &&
                                      VehInfo.GetClassName(v1.Model.Hash) != "Motorcycles";
                    bool v1Doors = true;
                    foreach (var item in v1.Doors)
                        v1Doors = !item.IsBroken && !item.IsOpen && !item.IsFullyOpen;
                    bool isAllUp1 = IsVehicleWindowIntact(v1.Handle, 0) && IsVehicleWindowIntact(v1.Handle, 1) && v1.Windows.AreAllWindowsIntact && v1.RoofState == VehicleRoofState.Closed && v1Doors && isOpenVeh1;
                    if (isAllUp1)
                        volume = volume / 6;
                }
            }
            if (volume > 1)
                volume = 1;
            return volume;
        }

        public static float GenerateBalance(float x1, float y1, float x2, float y2)
        {
            if (!User.Data.s_voice_balance)
                return 0;
            
            var yaw = GetGameplayCamRot(2).Z;
            var nx = -Math.Sin(yaw * Math.PI / 180);
            var ny = Math.Cos(yaw * Math.PI / 180);

            var x = x2 - x1;
            var y = y2 - y1;
            var s = (float) Math.Sqrt(x * x + y * y);

            x = x / s;
            y = y / s;

            if (x * ny - nx * y > 0)
                return  (float) Math.Sqrt(1 - (x * nx + y * ny) * (x * nx + y * ny));
            if (x * ny - nx * y < 0)
                return (float) -Math.Sqrt(1 - (x * nx + y * ny) * (x * nx + y * ny));
            return 0;
        }
        
        private static async Task TickTimer()
        {
            if (User.IsLogin() && !User.IsDead())
            {
                if (Game.IsControlJustPressed(0, (Control) 249))
                    SetMicroEnable(true);
                if (Game.IsControlJustReleased(0, (Control) 249))
                    SetMicroEnable(false);

                //TODO если связан и если в наручниках
                if (!Menu.IsShowInput)
                {
                    if (User.Data.walkietalkie_num != "0" && !string.IsNullOrEmpty(User.Data.walkietalkie_num) && User.Data.jail_time == 0)
                    {
                        if (Game.IsControlJustPressed(0, (Control) 137))
                            SetRadioEnable(true);
                        if (Game.IsControlJustReleased(0, (Control) 137))
                            SetRadioEnable(false);
                    }
                }
            }
        }
        
        private static async Task SecTimer()
        {
            await Delay(1000);
            NetworkSetVoiceChannel(User.GetServerId());
            if (GetVoiceState() == "connected")
            {
                var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
                foreach (var p in new PlayerList())
                {
                    if (User.GetServerId() == p.ServerId) continue;
                    if (!NetworkIsPlayerActive(p.Handle)) continue;
                    var name = $"{Main.ServerName}_pl_{p.ServerId}";

                    if (_radioList.ContainsKey(p.ServerId) && _radioList[p.ServerId] == User.Data.walkietalkie_num)
                    {
                        if (!_peers.Contains(p))
                            AddStream(name);
                        continue;
                    }
                    
                    var peerPos = GetEntityCoords(GetPlayerPed(p.Handle), true);
                    if (Main.GetDistanceToSquared(peerPos, playerPos) < 75 && !_peers.Contains(p))
                        AddStream(name);
                    if (Main.GetDistanceToSquared(peerPos, playerPos) > 100 && _peers.Contains(p))
                        RemoveStream(name);
                }
            }
        }
        
        private static async Task Sec10Timer()
        {
            await Delay(10000);
            if (User.IsLogin()) 
            {
                /*foreach (var item in User.PlayerIdList)
                    Debug.WriteLine($"[ARP] SERVER_ID:{item.Key}, USER_ID:{item.Value} ");*/
                
                if (GetVoiceState() == "closed" || GetVoiceState() == "loading")
                {
                    Debug.WriteLine($"[ARP] UserVolume {User.Data.s_voice_vol} | {User.Data.s_voice_balance}");
                    var name = $"{Main.ServerName}_pl_{User.GetServerId()}";
                    Debug.WriteLine($"[ARP] Connecting to voice as {name}");
                    TriggerEvent("ARPVoice:Init", name);
                    await Delay(5000);
                    TriggerEvent("ARPVoice:ChangeProducerIsUsb");
                }
            }
        }
        
        private static async Task Tick500Timer()
        {
            await Delay(500);
            if (GetVoiceState() == "connected")
            {
                bool shouldSend = false;
                string data = "[";
                var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
                
                foreach (var p in _peers)
                {
                    var name = $"{Main.ServerName}_pl_{p.ServerId}";
                    var peerPos = GetEntityCoords(GetPlayerPed(p.Handle), true);
                    var volume = GenerateVolume(playerPos, peerPos, p);
                    
                    if (volume == 0 && _radioList.ContainsKey(p.ServerId) && _radioList[p.ServerId] == User.Data.walkietalkie_num)
                        continue;
                    
                    var balance = GenerateBalance(playerPos.X, playerPos.Y, peerPos.X, peerPos.Y);
                    if (balance < -0.95f)
                        balance = -0.95f;
                    else if (balance > 0.95f)
                        balance = 0.95f;
                    
                    data += $@"{{""name"":""{name}"", ""volume"":{volume}, ""balance"":{balance}}},";
                    shouldSend = true;
                }

                if (shouldSend)
                    data = data.Remove(data.Length - 1);
                data += "]";
                if (shouldSend)
                    TriggerEvent("ARPVoice:ChangeVolumeConsumers", data);
            }
        }
    }
}

/*
-- Client => NUI (SendNUIMessage)
SendNUIMessage({type="init", args={"pl_".. PlayerId(), "key"}}) -- Подключится к войс чату.
SendNUIMessage({type="muteMic", args={}}) -- Мут.
SendNUIMessage({type="unmuteMic", args={}}) -- Анмут.
SendNUIMessage({type="restartIce", args={}}) -- Перезагрузить подключение к ICE серверам.
SendNUIMessage({type="changeProducerIsUsb", args={true}}) -- Включение/выключение автогейна.
SendNUIMessage({type="changeVolumeConsumers", args={senddata}}) -- Массив новых данных для пиров. [{name, volume, balance}], есть так же changeVolumeConsumer для одного пира
SendNUIMessage({type="streamIn", args={"peername"}}) -- Подключится к пиру.
SendNUIMessage({type="streamOut", args={"peername"}}) -- Отключится от пира.
SendNUIMessage({type="changeProducer", args={deviceId, producerVolume}}) -- Смена микрофона используемый в войсе.
SendNUIMessage({type="changeMainVolume", args={1.0}}) -- Смена общей громкости.
SendNUIMessage({type="quit", args={}}) -- Дисконнект

-- CEF => Client (RegisterNUICallback)
"requestMediaPeerResponse" -- Откликается после попытки подключится к пиру, отдает обьект. {peerName, status}
"requestCloseMediaPeerResponse" -- Откликается при попытке отключится от пира. Тоже самое что и выше.
"changeConsumersVolume" -- Данные об изменении громкости разговора пиров. Т.е. отдает массив из данных громкости разговора игроков, от 0 до 7. Отдает массив из обьектов, на один пункт ниже.
"changeConsumerVolume" -- Тоже самое, только не массив, а по 1 пиру. {peerName, status}
"changeProducerVolume" -- Отдает изменение громкости твоего разговора.
"toggleMicrophone" -- Ивент включение/выключения микрофона.
"changeStateConnection" -- Изменение состояния подключения к медиасерверу. ("connected", "connecting", "disconnected", "closed", "failed")
*/