using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using Client.Vehicle;
using NativeUI;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    /*class LoadingPrompt : Screen.LoadingPrompt
    {
        
    }*/
    
    public class UI : BaseScript
    {
        public static SizeF Res = GetScreenResolutionMaintainRatio();
        private static float Height = Res.Height;
        private static readonly float Width = Res.Width;

        private static string _zone = "Подключение к сети GPS";
        private static string _street = "...";
        private static bool _bandage = false;
        private static bool _bankCard = false;
        
        private static float _screenX = 0f;
        private static float _screenY = 0f;
        

        public static Color ColorTransparent = Color.FromArgb(0);
        public static Color ColorRed = Color.FromArgb(244,67,54);
        public static Color ColorRed900 = Color.FromArgb(183,28,28);
        public static Color ColorWhite = Color.FromArgb(255,255,255);
        public static Color ColorBlue = Color.FromArgb(33,150,243);
        public static Color ColorGreen = Color.FromArgb(76,175,80);
        public static Color ColorAmber = Color.FromArgb(255,193,7);
        public static Color ColorDeepOrange = Color.FromArgb(255,87,34);
        
        private static string _rpg = "RolePlay";

        //private Scaleform sf = new Scaleform("PLAYER_SWITCH_STATS_PANEL");
            
        public UI()
        {
            Tick += TimeSync;
            Tick += SapdRadar;
            Tick += TimeBandageSync;
            Tick += UpdateZone;
            Tick += UpdateItemHud;
            Tick += UpdateItemHudVehicle;
            
            Debug.WriteLine($"Height: {Res.Height}");
            Debug.WriteLine($"Width: {Res.Width}");
            Debug.WriteLine($"Height2: {Height}");
            Debug.WriteLine($"Width2: {Width}");
            Debug.WriteLine($"Height3: {Screen.Height}");
            Debug.WriteLine($"Width3: {Screen.Width}");
            Debug.WriteLine($"Height4: {Screen.Resolution.Height}");
            Debug.WriteLine($"Width4: {Screen.Resolution.Width}");
            Debug.WriteLine($"GetScreenResolutionMaintainRatio: {GetScreenResolutionMaintainRatio()}");
        }
        
        /*public void SetLabels() {
            PushScaleformMovieFunction(sf.Handle, "SET_STATS_LABELS");
            PushScaleformMovieFunctionParameterInt(116);
            PushScaleformMovieFunctionParameterBool(true);
            
            PushScaleformMovieFunctionParameterInt(Stats.RANK.value);
            BeginTextCommandScaleformString("TR_RANKNUM");
            EndTextCommandScaleformString();
            
            
            PushScaleformMovieFunctionParameterInt(Stats.STAMINA.value);
            BeginTextCommandScaleformString("PS_STAMINA");
            EndTextCommandScaleformString();
            
            PushScaleformMovieFunctionParameterInt(Stats.LUNG.value);
            BeginTextCommandScaleformString("PS_LUNG");
            EndTextCommandScaleformString();
            
            PushScaleformMovieFunctionParameterInt(Stats.STRENGTH.value);
            BeginTextCommandScaleformString("PS_STRENGTH");
            EndTextCommandScaleformString();
            
            PushScaleformMovieFunctionParameterInt(Stats.DRIVING.value);
            BeginTextCommandScaleformString("PS_DRIVING");
            EndTextCommandScaleformString();
            
            PushScaleformMovieFunctionParameterInt(Stats.FLYING.value);
            BeginTextCommandScaleformString("PS_FLYING");
            EndTextCommandScaleformString();
            
            PushScaleformMovieFunctionParameterInt(Stats.SHOOTING.value);
            BeginTextCommandScaleformString("PS_SHOOTING");
            EndTextCommandScaleformString();
            
            PushScaleformMovieFunctionParameterInt(Stats.STEALTH.value);
            BeginTextCommandScaleformString("PS_STEALTH");
            EndTextCommandScaleformString();
            
            PushScaleformMovieFunctionParameterInt(0);
            BeginTextCommandScaleformString("PCARD_MENTAL_STATE");
            EndTextCommandScaleformString();
            
            PopScaleformMovieFunctionVoid();
        }
        
        public void Render() {
            ScreenDrawPositionBegin(82, 67);
            ScreenDrawPositionRatio(0f, 0f, 0f, 0f);
            DrawScaleformMovie(sf.Handle, 
                (0.042f + (((0.140625f * 1) * 1.3333f) * 0.5f)), 0.006f, 
                ((0.120625f * 1) * 1.3333f), (0.3875f * 1), 
                255, 255, 255, 255, 0);
            ScreenDrawPositionEnd();
        }*/
        
        public static SizeF GetScreenResolutionMaintainRatio()
        {
            return new SizeF(Screen.Resolution.Height * ((float) Screen.Resolution.Width / (float) Screen.Resolution.Height), Screen.Resolution.Height);
        }

        private static string UpdateDirectionText()
        {
            var dgr = GetEntityHeading(GetPlayerPed(-1));
            
            if (dgr >= 22.5 && dgr < 67.5)
            {
                return "NW";
            }
            if (dgr >= 67.5 && dgr < 112.5)
            {
                return "W";
            }
            if (dgr >= 112.5 && dgr < 157.5)
            {
                return "SW";
            }
            if (dgr >= 157.5 && dgr < 202.5)
            {
                return "S";
            }
            if (dgr >= 202.53 && dgr < 247.5)
            {
                return "SE";
            }
            if (dgr >= 247.5 && dgr < 292.5)
            {
                return "E";
            }
            if (dgr >= 292.5 && dgr < 337.5)
            {
                return "NE";
            }
            
            return "N";
        }

        private static void UpdateZoneName()
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            _street = World.GetStreetName(playerPos);
            _zone = World.GetZoneLocalizedName(playerPos);
            //_zone = World.GetZoneDisplayName(playerPos); //OCEANA
        }
        
        private static async Task UpdateItemHud()
        {
            if (User.IsLogin())
            {
                if (Client.Sync.Data.HasLocally(User.GetServerId(), "isTieBandage"))
                {
                    _bandage = true;
                }
                else
                {
                    _bandage = false;
                }

                if (User.Data.bank_prefix > 0)
                {
                    _bankCard = true;
                }
                else
                {
                    _bankCard = false;
                }
                    
            }
            TriggerEvent("ARPHUD:UpdateData:item_clock", _bandage ? "?" : UpdateDirectionText(), _bandage ? "Неизвестно" : GetPlayerZoneName(), _bandage ? "Неизвестно" : GetPlayerStreetName(),
                $"{Weather.Day.ToString("D2")}/{Weather.Month.ToString("D2")}/{Weather.Year.ToString("D2")}",
                $"{World.CurrentDayTime.Hours.ToString("D2")}:{World.CurrentDayTime.Minutes.ToString("D2")}",
                $"{Weather.Temp}°");
            TriggerEvent("ARPHUD:UpdateData:money", $"${User.Data.money.ToString("#,#")}"
                /*, _bankCard ? $"${User.Data.money_bank.ToString("#,#")}" : "Нет банковской карты"*/);
            if (User.Data.item_clock)
            {
                TriggerEvent("ARPHUD:UpdateData:showWatch",  true);
            }
            else
            {
                TriggerEvent("ARPHUD:UpdateData:showWatch",  false);
            }
            if (IsPedInAnyVehicle(GetPlayerPed(-1), true))
            {
                TriggerEvent("ARPHUD:UpdateData:showSpeed",  true);
            }
            else
            {
                TriggerEvent("ARPHUD:UpdateData:showSpeed",  false);
            }
            var _eatLevel = Convert.ToInt32(User.GetEatLevel()/10);
            var _waterLevel = Convert.ToInt32(User.GetWaterLevel());
            TriggerEvent("ARPHUD:UpdateData:food", _eatLevel > 100 ? "100%" : _eatLevel + "%", _waterLevel > 100 ? "100%" : _waterLevel + "%");
            await Delay(1000);
        }

        private static async Task UpdateItemHudVehicle()
        {
            if (IsPedInAnyVehicle(PlayerPedId(), true))
            {
                TriggerEvent("ARPHUD:UpdateData:vehicle", GetCurrentSpeed(), GetCurrentFuel());
            }
        }

        public static bool IsPlayerInOcean()
        {
            return World.GetZoneDisplayName(GetEntityCoords(GetPlayerPed(-1), true)) == "OCEANA";
        }

        public static string GetPlayerZoneName()
        {
            return _zone;
        }

        public static string GetPlayerStreetName()
        {
            return _street;
        }
        
        private static async Task UpdateZone()
        {
            if (User.IsLogin())
                UpdateZoneName();
            
            await Delay(2000);
        }

        public static bool IsShowRadar = false;
        private static bool _isFreezeRadar = false;
        private static string _radarInfoFront = "В ожидании...";
        private static string _radarInfoBack = "В ожидании...";
        private static int _speed = 0;
        private static string _fuel = "0";

        private static bool _hit1;
        private static Vector3 _endCoords1;
        private static Vector3 _surfaceNormal1;
        private static int _entityHit1;

        private static bool _hit2;
        private static Vector3 _endCoords2;
        private static Vector3 _surfaceNormal2;
        private static int _entityHit2;

        public static int GetCurrentSpeed()
        {
            return _speed;
        }
        
        public static string GetCurrentFuel()
        {
            return _fuel;
        }

        private static async Task SapdRadar()
        {
            if (User.IsLogin() && (IsHudPreferenceSwitchedOn() || Screen.Hud.IsVisible))
            {
                if (IsShowRadar)
                {
                    var veh = GetVehiclePedIsUsing(PlayerPedId());
                    var v = new CitizenFX.Core.Vehicle(veh);

                    if (!_isFreezeRadar)
                    {
                        var coordA = GetOffsetFromEntityInWorldCoords(veh, 0.0f, 1.0f, 1.0f);
                        var coordB = GetOffsetFromEntityInWorldCoords(veh, 0.0f, 105.0f, 0.0f);
                        var frontcar = StartShapeTestCapsule(coordA.X, coordA.Y, coordA.Z, coordB.X, coordB.Y, coordB.Z, 3.0f, 10, veh, 7);
                        GetShapeTestResult(frontcar, ref _hit1, ref _endCoords1, ref _surfaceNormal1, ref _entityHit1);

                        if (IsEntityAVehicle(_entityHit1))
                            _radarInfoFront = $"{Vehicle.GetVehicleNumber(_entityHit1)} | {GetDisplayNameFromVehicleModel((uint) GetEntityModel(_entityHit1))} | {Math.Round(GetEntitySpeed(_entityHit1) * 2.23693629, 0)} MP/H";

                        var bcoordB = GetOffsetFromEntityInWorldCoords(veh, 0.0f, -105.0f, 0.0f);
                        var rearcar = StartShapeTestCapsule(coordA.X, coordA.Y, coordA.Z, bcoordB.X, bcoordB.Y, bcoordB.Z, 3.0f, 10, veh, 7);
                        GetShapeTestResult(rearcar, ref _hit2, ref _endCoords2, ref _surfaceNormal2, ref _entityHit2);
                    
                        if (IsEntityAVehicle(_entityHit2))
                            _radarInfoBack = $"{Vehicle.GetVehicleNumber(_entityHit2)} | {GetDisplayNameFromVehicleModel((uint) GetEntityModel(_entityHit2))} | {Math.Round(GetEntitySpeed(_entityHit2) * 2.23693629, 0)} MP/H";
                    }
                    
                    var tag = User.Data.tag != "" && User.IsGos() ? User.Data.tag.ToUpper() + " | " : "";
                    DrawRectangle(371, 317, 350, 200, 0, 0, 0, 150, 2, 2);
                    DrawText($"{tag}№{User.Data.id}", 363, 313, 0.6f, 255, 255, 255, 255, 4, 0, false, false, 0, 2, 2);
                    DrawRectangle(371, 317, 350, 50, 3, 169, 244, 150, 2, 2);
                    DrawText($"{VehInfo.GetDisplayName(v.Model.Hash).ToUpper()} | {Vehicle.GetVehicleNumber(veh)} | СИРЕНА: {(IsVehicleSirenOn(veh) ? "ВКЛ" : "ВЫКЛ")} | {_speed} MP/H", 363, 267, 0.4f, 3, 155, 229, 255, 4, 0, false, false, 0, 2, 2);
                    DrawRectangle(371, 267, 350, 30, 2, 119, 189, 150, 2, 2);
                    DrawText("ПЕРЕДНИЙ РАДАР", 363, 234, 0.3f, 255, 255, 255, 150, 4, 0, false, false, 0, 2, 2);
                    DrawText("ЗАДНИЙ РАДАР", 362, 175, 0.3f, 255, 255, 255, 150, 4, 0, false, false, 0, 2, 2);
                    DrawText(_radarInfoFront, 362, 217, 0.5f, 255, 255, 255, 255, 4, 0, false, false, 0, 2, 2);
                    DrawText(_radarInfoBack, 361, 158, 0.5f, 255, 255, 255, 255, 4, 0, false, false, 0, 2, 2);
                    DrawRectangle(361, 180, 330, 1, 255, 255, 255, 255, 2, 2);
                }

                if (!IsPedInAnyVehicle(GetPlayerPed(-1), true) && IsShowRadar)
                {
                    _radarInfoFront = "В ожидании...";
                    _radarInfoBack = "В ожидании...";
                    IsShowRadar = false;
                }
            }
        }

        protected static TimerBarPool TimerBarPool = new TimerBarPool();
        public static async void DrawAdditionalHud()
        {
            if (!IsShowRadar && Screen.Hud.IsVisible)
            {
                BarTimerBar network = new BarTimerBar("Сеть");
                network.BackgroundColor = UnknownColors.Black;
                network.Height = 5;

                if (await Ctos.IsBlackout())
                {
                    network.Percentage = 0;
                    network.ForegroundColor = ColorRed900;
                }
                else
                {
                    if (Ctos.UserNetwork * 100 > 100)
                        network.ForegroundColor = ColorBlue;
                    else if (Ctos.UserNetwork * 100 > 60)
                        network.ForegroundColor = ColorWhite;
                    else if (Ctos.UserNetwork * 100 > 40)
                        network.ForegroundColor = ColorAmber;
                    else if (Ctos.UserNetwork * 100 > 20)
                        network.ForegroundColor = ColorDeepOrange;
                    else if (Ctos.UserNetwork * 100 > 0)
                        network.ForegroundColor = ColorRed;
                    else if (Ctos.UserNetwork * 100 <= 0)
                        network.ForegroundColor = ColorRed900;

                    network.Percentage = Ctos.UserNetwork;
                    //network.ForegroundColor = ColorGreen;
                }

                /*if (User.Data.water_level > 60)
                    network.ForegroundColor = UnknownColors.Green;
                else if (User.Data.water_level > 40)
                    network.ForegroundColor = UnknownColors.Yellow;
                else if (User.Data.water_level > 20)
                    network.ForegroundColor = UnknownColors.Orange;
                else if (User.Data.water_level > 0)
                    network.ForegroundColor = UnknownColors.Red;
                else if (User.Data.water_level <= 0)
                    network.ForegroundColor = UnknownColors.DarkRed;*/

                /*BarTimerBar eat = new BarTimerBar("Сытость");
                eat.BackgroundColor = UnknownColors.Black;
                eat.Percentage = User.Data.eat_level / 1000f;
                eat.Height = 5;

                if (eat.Percentage >= 1)
                    eat.Percentage = 1;

                if (User.Data.eat_level > 1000)
                    eat.ForegroundColor = ColorBlue;
                else if (User.Data.eat_level > 600)
                    eat.ForegroundColor = ColorWhite;
                else if (User.Data.eat_level > 400)
                    eat.ForegroundColor = ColorAmber;
                else if (User.Data.eat_level > 200)
                    eat.ForegroundColor = ColorDeepOrange;
                else if (User.Data.eat_level > 0)
                    eat.ForegroundColor = ColorRed;
                else if (User.Data.eat_level <= 0)
                    eat.ForegroundColor = ColorRed900;

                BarTimerBar drink = new BarTimerBar("Жажда");
                drink.BackgroundColor = UnknownColors.Black;
                drink.Percentage = User.Data.water_level / 100f;
                drink.Height = 5;

                if (drink.Percentage >= 1)
                    drink.Percentage = 1;

                if (User.Data.water_level > 100)
                    drink.ForegroundColor = ColorBlue;
                else if (User.Data.water_level > 60)
                    drink.ForegroundColor = ColorWhite;
                else if (User.Data.water_level > 40)
                    drink.ForegroundColor = ColorAmber;
                else if (User.Data.water_level > 20)
                    drink.ForegroundColor = ColorDeepOrange;
                else if (User.Data.water_level > 0)
                    drink.ForegroundColor = ColorRed;
                else if (User.Data.water_level <= 0)
                    drink.ForegroundColor = ColorRed900;*/
                
                /*
                BarTimerBar fuel = new BarTimerBar("Топливо");
                fuel.BackgroundColor = UnknownColors.Black;
                fuel.ForegroundColor = ColorWhite;
                fuel.Percentage = 1;
                fuel.Height = 10;

                if (fuel.Percentage >= 1)
                    fuel.Percentage = 1;
                    
                var vehId = Vehicle.GetVehicleIdByNumber(Vehicle.GetVehicleNumber(GetVehiclePedIsUsing(PlayerPedId())));
                if (vehId != -1)
                {
                    var fuelIndicator = Convert.ToInt32((Vehicle.VehicleInfoGlobalDataList[vehId].Fuel / Vehicle.VehicleInfoGlobalDataList[vehId].FullFuel) * 100);
                    if (fuelIndicator > 100)
                        fuel.ForegroundColor = ColorBlue;
                    else if (fuelIndicator > 60)
                        fuel.ForegroundColor = ColorWhite;
                    else if (fuelIndicator > 40)
                        fuel.ForegroundColor = ColorAmber;
                    else if (fuelIndicator > 20)
                        fuel.ForegroundColor = ColorDeepOrange;
                    else if (fuelIndicator > 0)
                        fuel.ForegroundColor = ColorRed;
                    else if (fuelIndicator <= 0)
                        fuel.ForegroundColor = ColorRed900;
                    
                    fuel.Percentage = fuelIndicator / 100f;
                }
                */
/*
                TimerBarPool = new TimerBarPool();
                TimerBarPool.Add(drink);
                TimerBarPool.Add(eat);*/
                /*if (User.Data.phone_code > 0)
                    TimerBarPool.Add(network);*/
                /*TimerBarPool.Add(fuel);
                TimerBarPool.Add(new TextTimerBar("Скорость", $"{GetCurrentSpeed()} MP/H"));*/
                //TimerBarPool.Add(new TextTimerBar("Громкость", User.VoiceString));
                
                if (Voice.IsRadioEnable())
                    TimerBarPool.Add(new TextTimerBar("", "~b~Вы говорите в рацию"));
                if (Business.Taxi.IsFindNpc)
                    TimerBarPool.Add(new TextTimerBar("", "~b~Идёт поиск клиентов"));
            }
            else
            {
                TimerBarPool =  new TimerBarPool();
            }
        }

        private static async Task TimeSync()
        {        
            if (User.IsLogin() && (IsHudPreferenceSwitchedOn() || Screen.Hud.IsVisible))
            {
                if (IsPedInAnyVehicle(PlayerPedId(), true))
                {
                    var velocity = GetEntityVelocity(GetVehiclePedIsUsing(PlayerPedId()));
                    var speed = Math.Sqrt(velocity.X * velocity.X +
                                          velocity.Y * velocity.Y +
                                          velocity.Z * velocity.Z);
                    
                    /*var vehId = Vehicle.GetVehicleIdByNetId(VehToNet(GetVehiclePedIsUsing(PlayerPedId())));
                    
                    var indicator = "~g~";

                    if (vehId != -1)
                    {
                        var fuelIndicator = Convert.ToInt32((Vehicle.VehicleInfoGlobalDataList[vehId].Fuel / Vehicle.VehicleInfoGlobalDataList[vehId].FullFuel) * 100);
                        if (fuelIndicator < 61 && fuelIndicator > 40)
                            indicator = "~y~";
                        else if(fuelIndicator < 41 && fuelIndicator > 20)
                            indicator = "~o~";
                        else if(fuelIndicator < 21 && fuelIndicator > 1)
                            indicator = "~r~";
                        else if(fuelIndicator < 2)
                            indicator = "~u~";
                    }*/

                    _speed = (int) Math.Round(speed * 2.23693629, 0);
                    
                    var vehId = Vehicle.GetVehicleIdByNumber(Vehicle.GetVehicleNumber(GetVehiclePedIsUsing(PlayerPedId())));
                    if (vehId != -1)
                    {
                        _fuel = Convert.ToInt32(Vehicle.VehicleInfoGlobalDataList[vehId].Fuel) + " Л";
                    }
                    else if (VehInfo.Get(GetEntityModel(GetVehiclePedIsUsing(PlayerPedId()))).FullFuel  == 1)
                    {
                        _fuel = "ЭЛЕКТРО";
                    }
                    else
                    {
                        _fuel = VehInfo.Get(GetEntityModel(GetVehiclePedIsUsing(PlayerPedId()))).FullFuel + " Л";
                    }
                    /*DrawRectangle(319, 166, 180, 1, 255, 255, 255, 255, 2);
                    DrawText($"{indicator}*", 319, 205, 0.9f, 255, 255, 255, 255, 4, 0, false, true, 0, 2);
                    DrawText($"I {_speed} MP/H", 339, 208, 0.6f, 255, 255, 255, 255, 4, 0, false, true, 0, 2);*/
                }
                
                //DrawText("Appi ~y~" + _rpg, 0, 10, 0.5f, 255, 255, 255, 255, 1, 1, true, true, 0, 0, 1);

                var rightOffset = 0;
                if (Main.MaxPlayers > 32)
                    rightOffset = 150;
                
                if (User.Data.jail_time > 0)
                    DrawText(User.Data.jail_time + "сек. | " + Weather.FullRealDateTime + " | " + Main.ServerName, 130 + rightOffset, 8, 0.3f, 255, 255, 255, 180, 0, 2, false, false, 0, 0, 2);
                else if (NoClip.NoClipEnabled)
                    DrawText(NoClip.Speeds[NoClip.CurrentSpeed] + " | " + Weather.FullRealDateTime + " | " + Main.ServerName, 130 + rightOffset, 8, 0.3f, 255, 255, 255, 180, 0, 2, false, false, 0, 0, 2);
                else
                    DrawText(Weather.FullRealDateTime + " | " + Main.ServerName, 130 + rightOffset, 8, 0.3f, 255, 255, 255, 180 + rightOffset, 0, 2, false, false, 0, 0, 2);
                /*
                if (User.Data.money < 0)
                    DrawText("$" + User.Data.money.ToString("#,#"), 15, 50, 0.6f, 244, 67, 54, 255, 7, 2, false, true, 0, 0, 2);
                else if (User.Data.money == 0)
                    DrawText("$0", 15, 50, 0.6f, 244, 67, 54, 255, 7, 2, false, true, 0, 0, 2);
                else
                    DrawText("$" + User.Data.money.ToString("#,#"), 15, 50, 0.6f, 115, 186, 131, 255, 7, 2, false, true, 0, 0, 2);
                if (User.Data.item_clock)
                {
                    //DrawText(UpdateDirectionText(), 347, 72, 1, 255, 255, 255, 255, 4, 1, false, true, 0, 2);
                    DrawText("|", 374, 72, 1, 255, 255, 255, 255, 4, 0, false, true, 0, 2);
                    DrawText(_zone, 391, 65, 0.4f, 241, 196, 15, 255, 4, 0, false, true, 0, 2);
                    DrawText(_street, 390, 42, 0.4f, 255, 255, 255, 255, 4, 0, false, true, 0, 2);
                    DrawRectangle(319, 67, 180, 1, 255, 255, 255, 255, 2);
                    DrawText("Температура воздуха: ~w~" + Weather.Temp + '°', 319, 100, 0.4f, 241, 196, 15, 255, 4, 0, false, true, 0, 2);
                    DrawRectangle(319, 102, 180, 1, 255, 255, 255, 255, 2);
                    DrawText(World.CurrentDayTime.Hours.ToString("D2") + ":" + World.CurrentDayTime.Minutes.ToString("D2") + " | " + Weather.Day.ToString("D2") + "/" + Weather.Month.ToString("D2") + "/" + Weather.Year.ToString("D2"), 319, 142, 0.6f, 255, 255, 255, 255, 4, 0, false, true, 0, 2);
                    DrawText(Weather.DayName, 319, 165, 0.4f, 241, 196, 15, 255, 4, 0, false, true, 0, 2);
                    
                    //DrawText("Сытость: ~g~" + User.Data.eat_level / 1000 + "%", 10, 545, 0.40f, 255, 255, 255, 255, 4, 0, true, true, 0);
                    //DrawText("Жажда: ~g~" + User.Data.water_level / 100 + "%", 10, 575, 0.40f, 255, 255, 255, 255, 4, 0, true, true, 0);    
                }
                */
                TimerBarPool.Draw();
                
                //DrawText("Громкость: ~g~" + User.VoiceString, 10, 605, 0.40f, 255, 255, 255, 255, 4, 0, true, true, 0);
                //DrawText("Иммунитет: ~g~" + User.Data.health_level + "%", 10, 605, 0.40f, 255, 255, 255, 255, 4, 0, true, true, 0);
                //DrawText("Температура тела: ~g~" + Math.Round(User.Data.temp_level, 1) + "°", 10, 635, 0.40f, 255, 255, 255, 255, 4, 0, true, true, 0);
            }
        }
        
        private static async Task TimeBandageSync()
        {        
            if (User.IsLogin())
            {
                if (Client.Sync.Data.HasLocally(User.GetServerId(), "isTieBandage"))
                    DrawRectangle(0, 0, Res.Width, Res.Height, 0, 0, 0, 255);
            }
        }
        
        /*public static void DxDrawTexture(int idx, string filename, float xPos, float yPos, float txdWidth, float txdHeight, float rot, int r, int g, int b, int a, bool centered = false)
        {
            if (!IsHudPreferenceSwitchedOn() || !Screen.Hud.IsVisible) return;

            float reduceX = xPos / Width;
            float reduceY = yPos / Height;

            float scaleX = txdWidth/Width;
            float scaleY = txdHeight/Height;

            var cF = GetFrameCount();

            if (cF != _lastframe)
                _lastframe = cF;

            var test = new NativeUI.Sprite("path");
            
            var customSprite = new GTA.UI.CustomSprite(path,
                new SizeF(scaleX, scaleY / Ratio),
                new PointF(reduceX, reduceY),
                Color.FromArgb(a, r, g, b),
                rot,
                centered);
            
            customSprite.Draw();
        }*/
        
        public static void DrawText3D(Vector3 pos, string text) {
            
            pos.Z += .5f;
            SetDrawOrigin(pos.X, pos.Y, pos.Z, 0);
            var camPos = GetGameplayCamCoords();
            var dist = World.GetDistance(pos, camPos);
            float scale = 1 / dist * 2f;
            float fov = 1 / GetGameplayCamFov() * 100f;
            scale *= fov;
            if (scale < 0.4)
                scale = 0.4f;
        
            SetTextScale(0.1f * scale, 0.55f * scale);
            SetTextFont(0);
            SetTextProportional(true);
            SetTextColour(255, 255, 255, 255);
            SetTextDropshadow(0, 0, 0, 0, 255);
            SetTextOutline();
            SetTextEdge(2, 0, 0, 0, 150);
            SetTextDropShadow();
            SetTextEntry("STRING");
            SetTextCentre(true);
            AddTextComponentString(text);
            CitizenFX.Core.Native.API.DrawText(0, 0);
            ClearDrawOrigin();
        }

        public static void DrawMarker(int type, Vector3 pos, Vector3 dir, Vector3 rot, Vector3 scale, int r, int g, int b, int a)
        {
            CitizenFX.Core.Native.API.DrawMarker(type, pos.X, pos.Y, pos.Z, dir.X, dir.Y, dir.Z, rot.X, rot.Y, rot.Z, scale.X, scale.Y, scale.Z, r, g, b, a, false, true, 1, false, null, null, false);
        }

        public static void Draw3DText(string text, Vector3 pos, float size, int r, int g, int b, int a)
        {
            /*if (World3dToScreen2d(pos.X, pos.Y, pos.Z, ref _screenX, ref _screenY))
                DrawText(text, Res.Width * _screenX, Res.Height * _screenY, size, r, g, b, a, 4, 0, true, true, 0);*/
        }
        
        public static void DrawSprite(string dict, string txtName, float xPos, float yPos, float width, float height, float heading, int r, int g, int b, int alpha, int vAlig = 0, int hAlig = 0)
        {
            if (!IsHudPreferenceSwitchedOn() || !CitizenFX.Core.UI.Screen.Hud.IsVisible) return;
            
            if (!HasStreamedTextureDictLoaded(dict))
                RequestStreamedTextureDict(dict, true);
            
            if (hAlig == 2)
                xPos = Res.Width - xPos;
            else if (hAlig == 1)
                xPos = Res.Width / 2 + xPos;
            
            if (vAlig == 2)
                yPos = Res.Height - yPos;
            else if (vAlig == 1)
                yPos = Res.Height / 2 + yPos;

            float w = width / Width;
            float h = height / Height;
            float x = xPos / Width + w * 0.5f;
            float y = yPos / Height + h * 0.5f;
            
            CitizenFX.Core.Native.API.DrawSprite(dict, txtName, x, y, w, h, heading, r, g, b, alpha);
        }

        public static void DrawRectangle(float xPos, float yPos, float wSize, float hSize, int r, int g, int b, int alpha, int vAlig = 0, int hAlig = 0)
        {
            if (!IsHudPreferenceSwitchedOn() || !CitizenFX.Core.UI.Screen.Hud.IsVisible) return;
            
            if (hAlig == 2)
                xPos = Res.Width - xPos;
            else if (hAlig == 1)
                xPos = Res.Width / 2 + xPos;
            
            if (vAlig == 2)
                yPos = Res.Height - yPos;
            else if (vAlig == 1)
                yPos = Res.Height / 2 + yPos;
            
            float w = wSize / Width;
            float h = hSize / Height;
            float x = xPos / Width + w * 0.5f;
            float y = yPos / Height + h * 0.5f;
        
            DrawRect(x, y, w, h, r, g, b, alpha);
        }
        
        public static void DrawText(string caption, float xPos, float yPos, float scale, int r, int g, int b, int alpha, int font, int justify, bool shadow, bool outline, int wordWrap, int vAlig = 0, int hAlig = 0)
        {
            if (!IsHudPreferenceSwitchedOn() || !CitizenFX.Core.UI.Screen.Hud.IsVisible) return;
        
            if (hAlig == 2)
                xPos = Res.Width - xPos;
            else if (hAlig == 1)
                xPos = Res.Width / 2 + xPos;
            
            if (vAlig == 2)
                yPos = Res.Height - yPos;
            else if (vAlig == 1)
                yPos = Res.Height / 2 + yPos;
            
            float x = xPos / Width;
            float y = yPos / Height;
            
            SetTextFont(font);
            SetTextScale(1f, scale);
            SetTextColour(r, g, b, alpha);
            
            if (shadow) SetTextDropShadow();
            if (outline) SetTextOutline();
            switch (justify)
            {
                case 1:
                    SetTextCentre(true);
                    break;
                case 2:
                    SetTextRightJustify(true);
                    SetTextWrap(0, x);
                    break;
            }
        
            if (wordWrap != 0)
                SetTextWrap(x, (xPos + wordWrap) / Width);
        
            BeginTextCommandDisplayText("STRING");
        
            const int maxStringLength = 99;
            for (int i = 0; i < caption.Length; i += maxStringLength)
                AddTextComponentSubstringPlayerName(caption.Substring(i, System.Math.Min(maxStringLength, caption.Length - i)));
        
            EndTextCommandDisplayText(x, y);
        }

        /*public static void DrawSprite(string dict, string txtName, float x, float y, float width, float height, float heading, int r, int g, int b, int alpha)
        {
            if (!IsHudPreferenceSwitchedOn() || !Screen.Hud.IsVisible) return;
            
            if (!HasStreamedTextureDictLoaded(dict))
                RequestStreamedTextureDict(dict, true);

            float w = width / Width;
            float h = height / Height;
            float xx = x / Width + w * 0.5f;
            float yy = y / Height + h * 0.5f;
            
            CitizenFX.Core.Native.API.DrawSprite(dict, txtName, xx, yy, w, h, heading, r, g, b, alpha);
        }

        public static void DrawRectangle(float xPos, float yPos, float wSize, float hSize, int r, int g, int b, int alpha)
        {
            if (!IsHudPreferenceSwitchedOn() || !Screen.Hud.IsVisible) return;
            
            float w = wSize / Width;
            float h = hSize / Height;
            float x = xPos / Width + w * 0.5f;
            float y = yPos / Height + h * 0.5f;

            DrawRect(x, y, w, h, r, g, b, alpha);
        }
        
        public static void DrawText(string caption, float xPos, float yPos, float scale, int r, int g, int b, int alpha, int font, int justify, bool shadow, bool outline, int wordWrap)
        {
            if (!IsHudPreferenceSwitchedOn() || !Screen.Hud.IsVisible) return;
        
            float x = xPos / Width;
            float y = yPos / Height;
            
            SetTextFont(font);
            SetTextScale(1f, scale);
            SetTextColour(r, g, b, alpha);
            
            if (shadow) SetTextDropShadow();
            if (outline) SetTextOutline();
            switch (justify)
            {
                case 1:
                    SetTextCentre(true);
                    break;
                case 2:
                    SetTextRightJustify(true);
                    SetTextWrap(0, x);
                    break;
            }
        
            if (wordWrap != 0)
                SetTextWrap(x, (xPos + wordWrap) / Width);
        
            BeginTextCommandDisplayText("STRING");
        
            const int maxStringLength = 99;
            for (int i = 0; i < caption.Length; i += maxStringLength)
            {
                AddTextComponentSubstringPlayerName(caption.Substring(i, System.Math.Min(maxStringLength, caption.Length - i)));
            }
        
            EndTextCommandDisplayText(x, y);
        }*/
        
        public static void DrawTextOnScreen(string text, float xPosition, float yPosition, float size, int r, int g, int b, int a, Alignment justification, int font = 0, bool isTextOutline = false)
        {
            if (!IsHudPreferenceSwitchedOn() || !Screen.Hud.IsVisible) return;
            
            SetTextFont(font);
            SetTextScale(1.0f, size);
            SetTextColour(r, g, b, a);
            if (justification == Alignment.Right) SetTextWrap(0f, xPosition);
            SetTextJustification((int)justification);
            if (isTextOutline) SetTextOutline();
            BeginTextCommandDisplayText("STRING");
            AddTextComponentSubstringPlayerName(text);
            EndTextCommandDisplayText(xPosition, yPosition);
        }
        
        public static void ShowToolTip(string text)
        {
            var rand = new Random();
            int idx = rand.Next(0, 50000);
            AddTextEntry($"toolTip{idx}", text);
            SetTextComponentFormat($"toolTip{idx}");
            EndTextCommandDisplayHelp(0, false, true, -1);
        }
        
        public static void ShowToolTip1(string text)
        {            
            BeginTextCommandDisplayHelp("STRING");
            AddTextComponentSubstringPlayerName(text);
            EndTextCommandDisplayHelp(0, false, true, -1);
        }

        public static void ShowSimpleShard(string text, string desc ="", int time = 5000)
        {
            new BigMessageHandler().ShowSimpleShard(text, desc, time);
        }

        public static void ShowMissionPassedMessage(string text, int time = 5000)
        {
            new BigMessageHandler().ShowMissionPassedMessage(text, time);
        }

        public static async void LoadingPrompt(string text, int time = 10000, LoadingSpinnerType type = LoadingSpinnerType.RegularClockwise)
        {
            Screen.LoadingPrompt.Show(text, type);
            await Delay(time);
            Screen.LoadingPrompt.Hide();
        }

        public static async void ShowLoadDisplay()
        {
            DoScreenFadeOut(500);
            while (IsScreenFadingOut())
                await Delay(1);
        }

        public static async void HideLoadDisplay()
        {
            DoScreenFadeIn(500);
            while (IsScreenFadingIn())
                await Delay(1);
        }

        public static async Task<bool> ShowLoadDisplayAwait()
        {
            DoScreenFadeOut(500);
            while (IsScreenFadingOut())
                await Delay(1);
            return true;
        }

        public static async Task<bool> HideLoadDisplayAwait()
        {
            DoScreenFadeIn(500);
            while (IsScreenFadingIn())
                await Delay(1);
            return true;
        }
    }
}