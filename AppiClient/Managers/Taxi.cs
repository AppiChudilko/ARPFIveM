using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;


namespace Client.Managers
{
    public class Taxi : BaseScript
    {
        
        public static uint GroupHash = 0;
        public static int WaitAnswerId = -1;
        
        public Taxi()
        {
            Tick += SecTimer;
            Tick += TickTimer;
        }
        
        public static async void CreateTest2()
        {
            var spawnTaxi = GetEntityCoords(GetPlayerPed(-1), true);
            string[] skins = {"s_m_m_cntrybar_01", "a_m_m_fatlatin_01", "csb_imran", "a_m_m_mexlabor_01", "a_m_y_vindouche_01", "a_m_m_fatlatin_01", "s_m_m_cntrybar_01"};
        
            var rand = new Random();
            uint vehicleHash = (uint) GetHashKey("taxi");
            uint pHash = (uint) GetHashKey(skins[rand.Next(0, 6)]);
            if (!await Main.LoadModel(vehicleHash))
                return;
            if (!await Main.LoadModel(pHash))
                return;
            
            var veh = CreateVehicle(vehicleHash, spawnTaxi.X, spawnTaxi.Y, spawnTaxi.Z + 1f, 0, true, false); 
            CitizenFX.Core.Vehicle vehicle = new CitizenFX.Core.Vehicle(veh);
        
            await Delay(200);
            
            CitizenFX.Core.Ped driver = new CitizenFX.Core.Ped(CreatePedInsideVehicle(vehicle.Handle, 26, pHash, -1, true, false));
        
            var groupHandle = GetPlayerGroup(PlayerId());
            SetGroupSeparationRange(groupHandle, 10.0f);
            SetPedNeverLeavesGroup(driver.Handle, true);
            SetPedAsGroupMember(driver.Handle, groupHandle);
        
            
            //TaskEnterVehicle(PlayerPedId(), vehicle.Handle, -1, 2, 1.0f, 1, 0);     
            SetPedIntoVehicle(PlayerPedId(), veh, 2);
            SetPedCanBeDraggedOut(PlayerPedId(), false);
            
            ClearPedTasks(driver.Handle);
            SetVehicleEngineOn(vehicle.Handle, true, false, false);
            TaskVehiclePark(driver.Handle, vehicle.Handle, -1467.7531738282f, -477.02423095704f, 34.681056976318f, 0, 0, 20.0f, true);
        }
        
        public static async void CreateTest()
        {
            var spawnTaxi = GetEntityCoords(GetPlayerPed(-1), true);
            string[] skins = {"s_m_m_cntrybar_01", "a_m_m_fatlatin_01", "csb_imran", "a_m_m_mexlabor_01", "a_m_y_vindouche_01", "a_m_m_fatlatin_01", "s_m_m_cntrybar_01"};
        
            var rand = new Random();
            uint vehicleHash = (uint) GetHashKey("taxi");
            uint pHash = (uint) GetHashKey(skins[rand.Next(0, 6)]);
            if (!await Main.LoadModel(vehicleHash))
                return;
            if (!await Main.LoadModel(pHash))
                return;
                
            var veh = CreateVehicle(vehicleHash, spawnTaxi.X, spawnTaxi.Y, spawnTaxi.Z + 1f, 0, true, false); 
            CitizenFX.Core.Vehicle vehicle = new CitizenFX.Core.Vehicle(veh)
            {
                IsEngineRunning = true
            };
        
            SetEntityAsMissionEntity(vehicle.Handle, true, true);
            CitizenFX.Core.Ped driver = new CitizenFX.Core.Ped(CreatePedInsideVehicle(vehicle.Handle, 26, pHash, -1, true, false));
        
            SetBlockingOfNonTemporaryEvents(driver.Handle, true);
            
            /*var GroupHandle = GetPlayerGroup(PlayerId());
            SetGroupSeparationRange(GroupHandle, 10.0f);
            SetPedNeverLeavesGroup(driver.Handle, true);
            SetPedAsGroupMember(driver.Handle, GroupHandle);*/
            
            AddRelationshipGroup("TAXI", ref GroupHash);
            driver.RelationshipGroup = GroupHash;
            
            SetRelationshipBetweenGroups(RelationshipTypes.Companion, (uint) GetHashKey("PLAYER"), (uint) GetHashKey("TAXI"));
            SetRelationshipBetweenGroups(RelationshipTypes.Companion, (uint) GetHashKey("TAXI"), (uint) GetHashKey("PLAYER"));
            SetPedRelationshipGroupHash(GetPlayerPed(-1), (uint) GetPedRelationshipGroupHash(driver.Handle));
        
            driver.CanBeTargetted = true;
            driver.BlockPermanentEvents = true;
        
            vehicle.IsPositionFrozen = true;
            
            vehicle.LockStatus = VehicleLockStatus.Unlocked;    
            await Delay(5000);
            
            TaskEnterVehicle(PlayerPedId(), vehicle.Handle, 10000, 2, 1.0f, 1, 0);
            SetPedCanBeDraggedOut(PlayerPedId(), false);
            
            //driver.MarkAsNoLongerNeeded();
            //vehicle.MarkAsNoLongerNeeded();
            await Delay(10000);
            
            vehicle.IsPositionFrozen = false;
            TaskVehicleDriveToCoordLongrange(driver.Handle, vehicle.Handle, -1526.977f, -466.6963f, 34.90295f, 25.0f, 411, 30.0f);
            SetPedKeepTask(driver.Handle, true);
        }
        
        public static async void CreateForNewPlayer(Vector3 spawnTaxi, float rot, Vector3 posTo, float rotTo, Vector3 posTo2, float rotTo2)
        {
            string[] skins = {"s_m_m_cntrybar_01", "a_m_m_fatlatin_01", "csb_imran", "a_m_m_mexlabor_01", "a_m_y_vindouche_01", "a_m_m_fatlatin_01", "s_m_m_cntrybar_01"};
        
            var rand = new Random();
            uint vehicleHash = (uint) GetHashKey("taxi");
            uint pHash = (uint) GetHashKey(skins[rand.Next(0, 6)]);
            if (!await Main.LoadModel(vehicleHash))
                return;
            if (!await Main.LoadModel(pHash))
                return;
            
            Debug.WriteLine("DEBUG: Model was loaded");
            
            var taxiCar = CreateVehicle(vehicleHash, spawnTaxi.X, spawnTaxi.Y, spawnTaxi.Z, rot, true, false);
            var taxiDriver = CreatePedInsideVehicle(taxiCar, 26, pHash, -1, true, false);
            
            Debug.WriteLine($"DEBUG: Ped {taxiDriver} Veh {taxiCar}");
            
            var groupHandle = GetPlayerGroup(PlayerPedId());
            SetGroupSeparationRange(groupHandle, 10f);
            SetPedNeverLeavesGroup(taxiDriver, true);
            
            SetPedCanBeTargetted(taxiDriver, true);
            SetPedCanBeTargettedByPlayer(taxiDriver, GetPlayerPed(-1), true);
            SetCanAttackFriendly(taxiDriver, false, false);
            TaskSetBlockingOfNonTemporaryEvents(taxiDriver, true);
            SetBlockingOfNonTemporaryEvents(taxiDriver, true);
            
            TaskVehiclePark(taxiDriver, taxiCar, posTo.X, posTo.Y, posTo.Z, rotTo, 0, 20f, false);
                     
            Client.Sync.Data.SetLocally(taxiDriver, "isTaxi", true);
            Client.Sync.Data.SetLocally(User.GetServerId(), "isTaxi", true);
            
            Client.Sync.Data.SetLocally(User.GetServerId(), "taxiToX", posTo.X);
            Client.Sync.Data.SetLocally(User.GetServerId(), "taxiToY", posTo.Y);
            Client.Sync.Data.SetLocally(User.GetServerId(), "taxiToZ", posTo.Z);
            Client.Sync.Data.SetLocally(User.GetServerId(), "taxiToRot", rotTo);
            Client.Sync.Data.SetLocally(User.GetServerId(), "taxiTo", false);
            
            Client.Sync.Data.SetLocally(User.GetServerId(), "taxiTo2X", posTo2.X);
            Client.Sync.Data.SetLocally(User.GetServerId(), "taxiTo2Y", posTo2.Y);
            Client.Sync.Data.SetLocally(User.GetServerId(), "taxiTo2Z", posTo2.Z);
            Client.Sync.Data.SetLocally(User.GetServerId(), "taxiTo2Rot", rotTo2);
            Client.Sync.Data.SetLocally(User.GetServerId(), "taxiTo2", false);
            
            Client.Sync.Data.SetLocally(User.GetServerId(), "taxiDriver", taxiDriver);
            Client.Sync.Data.SetLocally(User.GetServerId(), "taxiVehicle", taxiCar);
            
            Notification.SendWithTime($"~y~Такси скоро приедет.\nНомера: {Vehicle.GetVehicleNumber(taxiCar)}");
        }
        
        public static async void GoGoTaxi()
        {
            var model1 = (uint)GetHashKey("taxi");
            var model2 = (uint)GetHashKey("a_m_y_epsilon_02");
            if (!await Main.LoadModel(model1))
                return;
            if (!await Main.LoadModel(model2))
                return;
            var coords = GetEntityCoords(PlayerPedId(),true);
            var taxiCar = CreateVehicle(model1, coords.X + 2f, coords.Y + 2f, coords.Z, 175.716f, true, false);
            var taxiDriver = CreatePedInsideVehicle(taxiCar, 26, model2, -1, true, false);
            
            SetEntityAsMissionEntity(taxiCar, true, true);
            SetEntityAsMissionEntity(taxiDriver, true, true);
            
            var groupHandle = GetPlayerGroup(PlayerPedId());
            SetGroupSeparationRange(groupHandle, 10f);
            SetPedNeverLeavesGroup(taxiDriver, true);
            TaskVehiclePark(taxiDriver, taxiCar, coords.X + 1f, coords.Y + 1f, coords.Z, 0f, 0, 20f, false);
            SetGameplayEntityHint(taxiCar, 0f, 0f, 0f, true, 1500, 1000, 1000, 0);
            SetPedIntoVehicle(PlayerPedId(), taxiCar, 2);
            
            while (!IsPedInVehicle(GetPlayerPed(-1), taxiCar, true))
                await Delay(1000);
            
            ClearPedTasks(taxiDriver);
            SetVehicleEngineOn(taxiCar, true, false, false);
            TaskVehiclePark(taxiDriver, taxiCar, -1467.7531738282f, -477.02423095704f, 34.681056976318f, 0f, 0, 20f, false);
            
            Client.Sync.Data.SetLocally(User.GetServerId(), "hasTaxi", true);
        }
        
        public static async void GoGoTaxi2()
        {
            var model1 = (uint)GetHashKey("taxi");
            var model2 = (uint)GetHashKey("a_m_y_epsilon_02");
            if (!await Main.LoadModel(model1))
                return;
            if (!await Main.LoadModel(model2))
                return;
            var coords = GetEntityCoords(PlayerPedId(),true);
            var taxiCar = CreateVehicle(model1, coords.X + 2f, coords.Y + 2f, coords.Z, 175.716f, true, false);
            var taxiDriver = CreatePedInsideVehicle(taxiCar, 26, model2, -1, true, false);

            SetEntityAsMissionEntity(taxiCar, true, true);
            SetEntityAsMissionEntity(taxiDriver, true, true);
            
            var groupHandle = GetPlayerGroup(PlayerPedId());
            SetGroupSeparationRange(groupHandle, 10f);
            SetPedNeverLeavesGroup(taxiDriver, true);
            TaskVehiclePark(taxiDriver, taxiCar, coords.X + 1f, coords.Y + 1f, coords.Z, 0f, 0, 20f, false);
            SetGameplayEntityHint(taxiCar, 0f, 0f, 0f, true, 1500, 1000, 1000, 0);
            TaskEnterVehicle(GetPlayerPed(-1), taxiCar, 10000, 2, 1.0f, 1, 0);
            
            while (!IsPedInVehicle(GetPlayerPed(-1), taxiCar, true))
            {
                await Delay(1000);
            }
            
            ClearPedTasks(taxiDriver);
            SetVehicleEngineOn(taxiCar, true, false, false);
            TaskVehiclePark(taxiDriver, taxiCar, -1467.7531738282f, -477.02423095704f, 34.681056976318f, 0f, 0, 20f, false);
        }
        
        private static async Task SecTimer()
        {
            await Delay(1000);
        
            if (Client.Sync.Data.HasLocally(User.GetServerId(), "taxiDriver"))
            {
                var v = (int) Client.Sync.Data.GetLocally(User.GetServerId(), "taxiVehicle");
                var p = (int) Client.Sync.Data.GetLocally(User.GetServerId(), "taxiDriver");
                Vector3 pos1 = new Vector3((float) Client.Sync.Data.GetLocally(User.GetServerId(), "taxiToX"), (float) Client.Sync.Data.GetLocally(User.GetServerId(), "taxiToY"), (float) Client.Sync.Data.GetLocally(User.GetServerId(), "taxiToZ"));
                Vector3 pos2 = new Vector3((float) Client.Sync.Data.GetLocally(User.GetServerId(), "taxiTo2X"), (float) Client.Sync.Data.GetLocally(User.GetServerId(), "taxiTo2Y"), (float) Client.Sync.Data.GetLocally(User.GetServerId(), "taxiTo2Z"));
                var rot1 = (float) Client.Sync.Data.GetLocally(User.GetServerId(), "taxiToRot");
                var rot2 = (float) Client.Sync.Data.GetLocally(User.GetServerId(), "taxiTo2Rot");
                
                if (Main.GetDistanceToSquared(GetEntityCoords(v, true), pos1) < 7f &&
                    (bool) Client.Sync.Data.GetLocally(User.GetServerId(), "taxiTo") == false)
                {
                    //TaskVehicleTempAction(p.Handle, v.Handle, 27, 60000);
                    //TaskVehiclePark(p.Handle, v.Handle, pos1.X, pos1.Y, pos1.Z, 0, 0, 20.0f, true);
                    SetGameplayEntityHint(v, 0f, 0f, 0f, true, 1500, 1000, 1000, 0);
        
                    int attempt = 6;
                    Notification.SendWithTime($"~y~Транспорт на месте.\nНомера: {Vehicle.GetVehicleNumber(v)}");
                    Notification.SendWithTime($"~y~Сядьте в такси");
                    
                    //while (GetEntitySpeed(v) * 2.236936 < 2f)
                    while (!IsVehicleStopped(v))
                        await Delay(100);
                    
                    FreezeEntityPosition(v, true);

                    while (!IsPedInVehicle(GetPlayerPed(-1), v, true) && attempt > 0)
                    {
                        attempt--;
                        await Delay(10000);
                    }
                    
                    FreezeEntityPosition(v, false);
                    
                    ClearPedTasks(p);
                    SetVehicleEngineOn(v, true, false, false);
                    
                    if (!IsPedInVehicle(GetPlayerPed(-1), v, true))
                    {
                        TaskVehicleDriveWander(p, v, 17.0f, DriveTypes.Normal);
                        new CitizenFX.Core.Ped(p).MarkAsNoLongerNeeded();
                        new CitizenFX.Core.Vehicle(v).MarkAsNoLongerNeeded();
                        
                        Client.Sync.Data.SetLocally(User.GetServerId(), "taxiTo", true);
                        Client.Sync.Data.SetLocally(User.GetServerId(), "taxiTo2", true);
                    }
                    else
                    {
                        PlayAmbientSpeech1(p, "TAXID_WHERE_TO", "SPEECH_PARAMS_FORCE_NORMAL");
                            
                        User.SetWaypoint(pos2.X, pos2.Y);
                        TaskVehicleDriveToCoordLongrange(p, v, pos2.X, pos2.Y, pos2.Z, 17f, DriveTypes.Normal, 3.0f);
                        Client.Sync.Data.SetLocally(User.GetServerId(), "taxiTo", true);
        
                        TaxiDialog(v);
                    }
        
                }
                else if (Main.GetDistanceToSquared(GetEntityCoords(v, true), pos2) < 30f &&
                    (bool) Client.Sync.Data.GetLocally(User.GetServerId(), "taxiTo") == true && (bool) Client.Sync.Data.GetLocally(User.GetServerId(), "taxiTo2") == false)
                {
                    ClearPedTasks(p);
                    
                    TaskVehiclePark(p, v, pos2.X, pos2.Y, pos2.Z, rot2, 0, 20f, false);
                    
                    while (!IsVehicleStopped(v))
                        await Delay(100);
                    
                    FreezeEntityPosition(v, true);

                    Notification.SendWithTime($"~y~Вы достигли конечной точки маршрута");
                    Notification.SendWithTime($"~y~Стоимость поездки $10");
                    
                    User.RemoveCashMoney(10);
                    
                    PlayAmbientSpeech1(p, "THANKS", "SPEECH_PARAMS_FORCE_NORMAL");

                    await Delay(10000);
                    
                    if (IsPedInVehicle(GetPlayerPed(-1), v, true))
                        TaskLeaveVehicle(PlayerPedId(), v, 1);
                    
                    while (IsPedInVehicle(GetPlayerPed(-1), v, true))
                        await Delay(10000);
                    
                    FreezeEntityPosition(v, false);
                    
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "isTaxi");
                    TaskVehicleDriveWander(p, v, 17.0f, DriveTypes.Normal);
                    
                    new CitizenFX.Core.Ped(p).MarkAsNoLongerNeeded();
                    new CitizenFX.Core.Vehicle(v).MarkAsNoLongerNeeded();
                    
                    Client.Sync.Data.SetLocally(User.GetServerId(), "taxiTo2", true);
                    
                    ResetPedInVehicleContext(GetPlayerPed(-1));
                }
                else if ((bool) Client.Sync.Data.GetLocally(User.GetServerId(), "taxiTo2") == true)
                {
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "taxiToX");
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "taxiToY");
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "taxiToZ");
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "taxiToRot");
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "taxiTo");
            
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "taxiTo2X");
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "taxiTo2Y");
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "taxiTo2Z");
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "taxiToRot2");
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "taxiTo2");
            
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "taxiDriver");
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "taxiVehicle");
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "isTaxi");
                }
            }
        }
        
        private static async Task TickTimer()
        {
            if (Client.Sync.Data.HasLocally(User.GetServerId(), "isTaxi"))
                SetPedInVehicleContext(PlayerPedId(), (uint)GetHashKey("MINI_PROSTITUTE_LOW_PASSENGER"));
        }

        public static async void TaxiDialog(int v)
        {
            for (int i = 0; i < 4; i++)
            {
                if (!IsPedInVehicle(GetPlayerPed(-1), v, true))
                    return;

                switch (i)
                {
                    case 0:
                    {
                        await Delay(15000);
                        Chat.SendChatInfoMessage("таксист", "Привет, какими судьбами в нашем штате?");
                        await Delay(5000);
                        MenuList.ShowTaxiAsk0Menu();
                        while (WaitAnswerId == -1)
                            await Delay(1000);
                        break;
                    }
                    case 1:
                    {
                        switch (WaitAnswerId)
                        {
                            case 0:
                            case 1:
                                Chat.SendChatInfoMessage("Таксист", "Да.. Страна возможностей, я могу тебе подсказать где и с чего тебе начать, чтобы быстро освоиться, хочешь?");
                                break;
                            case 2:
                                Chat.SendChatInfoMessage("Таксист", "Ну, не хочешь конечно не отвечай, но я могу подсказать с чего начать, чтобы быстро освоиться, хочешь?");
                                break;
                        }
                        WaitAnswerId = -1;
                        await Delay(10000);
                        MenuList.ShowTaxiAsk1Menu();
                        while (WaitAnswerId == -1)
                            await Delay(1000);
                        break;
                    }
                    case 2:
                    {
                        switch (WaitAnswerId)
                        {
                            case 0:
                                Chat.SendChatInfoMessage("Таксист", "Хорошо, тогда первым делом тебе лучше всего доехать до здания правительства, получить пособие и временную регистрацию, а то могут быть проблемы с законом");
                                break;
                            case 1:
                                Chat.SendChatInfoMessage("Таксист", "Хорошо, поедем молча");
                                return;
                        }
                        WaitAnswerId = -1;
                        await Delay(5000);
                        MenuList.ShowTaxiAsk2Menu();
                        while (WaitAnswerId == -1)
                            await Delay(1000);
                        break;
                    }
                    case 3:
                    {
                        Chat.SendChatInfoMessage("Таксист", "На счёт заработка, неподалёку можно витрины за деньги помыть, как раз купишь себе права категории B");
                        Chat.SendChatInfoMessage("СПРАВКА", "(( M - GPS - Работы - Мойщик Окон ))", "BDBDBD");
                        await Delay(15000);
                        Chat.SendChatInfoMessage("Таксист", "После того как купишь себе права категории B, ты можешь в здании правительства в трудовой бирже устроиться например уборщиком квартир, это то здание где ты пособие и регистрацию оформляешь");
                        await Delay(15000);
                        Chat.SendChatInfoMessage("Таксист", "Только внимательней будь в трудовой бирже, там написаны компании и тебе надо запомнить куда устроишься, чтобы ты смог ее найти в GPS");
                        Chat.SendChatInfoMessage("СПРАВКА", "(( M - GPS - Компании ))", "BDBDBD");
                        await Delay(15000);
                        Chat.SendChatInfoMessage("Таксист", "Ну, а там всё просто, приезжаешь и работаешь. И да, не забудь купить телефон и часы \"Appi Watch\" в магазине 24/7. Учти что в каждом магазине разные цены, дешевле $200 ты не найдешь");
                        Chat.SendChatInfoMessage("СПРАВКА", "(( M - GPS - Магазины и прочее - Ближайший 24/7 ))", "BDBDBD");
                        await Delay(15000);
                        Chat.SendChatInfoMessage("Таксист", "Ох... Не знаю что еще и добавить");
                        Chat.SendChatInfoMessage("СПРАВКА", "(( Чтобы воспользоваться справкой нажмите: M - Помощь ))", "BDBDBD");
                        Chat.SendChatInfoMessage("СПРАВКА", "(( Если есть вопросы по игре пишите в дискорд http://dscrd.in/appi в канал #ask ))", "BDBDBD");
                        break;
                    }
                }
            }
        }
    }
}