using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;


namespace Client.Managers
{
    public class Newbie : BaseScript
    {
        public Newbie()
        {
            Tick += TickTimer;
        }

        public static async void FlyScene()
        {   
            DoScreenFadeOut(500);
            while (IsScreenFadingOut())
                await Delay(1);
                
            SetCinematicModeActive(true);
            
            await Delay(500);

            Client.Sync.Data.SetLocally(User.GetServerId(), "isFly", true);
            CitizenFX.Core.UI.Screen.Hud.IsVisible = false;
            
            var model1 = (uint)GetHashKey("jet");
            var model2 = (uint)GetHashKey("s_m_m_pilot_01");
            
            if (IsModelInCdimage(model1))
            {
                RequestModel(model1);
                while (!HasModelLoaded(model1))
                    await Delay(1);
            }
            if (IsModelInCdimage(model2))
            {
                RequestModel(model2);
                while (!HasModelLoaded(model2))
                    await Delay(1);
            }

            var random = new Random();
            
            var taxiCar = CreateVehicle(model1, 5000f, -8000f, 300f, 50f, true, false); //40
            var taxiDriver = CreatePedInsideVehicle(taxiCar, 26, model2, -1, true, false);
            
            SetEntityInvincible(taxiCar, true);
            SetEntityCollision(taxiDriver, false, true);
            SetEntityCollision(taxiCar, false, true);
            SetEntityCollision(GetPlayerPed(-1), false, true);
            
            var groupHandle = GetPlayerGroup(PlayerPedId());
            SetGroupSeparationRange(groupHandle, 10f);
            SetPedNeverLeavesGroup(taxiDriver, true);
            SetPedIntoVehicle(PlayerPedId(), taxiCar, 0);
            
            SetVehicleDoorsLocked(taxiCar, 4);
            
            SetPedCanBeTargetted(taxiDriver, true);
            SetPedCanBeTargettedByPlayer(taxiDriver, GetPlayerPed(-1), true);
            SetCanAttackFriendly(taxiDriver, false, false);
            TaskSetBlockingOfNonTemporaryEvents(taxiDriver, true);
            SetBlockingOfNonTemporaryEvents(taxiDriver, true);

            //TaskPlaneMission(taxiDriver, taxiCar, taxiCar, taxiDriver, -107.2212f, 2717.5534f, 61.9673f, 4, 0, 0, 0.0f, 2500.0f, 300f);
            TaskPlaneMission(taxiDriver, taxiCar, 0, 0, -1039.481f, -2740.733f, 19.16927f, 4, 1000f, 0, 0.0f, 2500.0f, 200f);

            await Delay(1000);
            
            SetVehicleForwardSpeed(taxiCar, 80f);
            SetVehicleLandingGear(taxiCar, 1);

            //await Delay(20000);
                
            DoScreenFadeIn(500);
            while (IsScreenFadingIn())
                await Delay(1);

            PlaneDialog();

            while (Main.GetDistanceToSquared2D(GetEntityCoords(taxiCar, true), new Vector3(-1039.481f, -2740.733f, 19.16927f)) > 1500f)
                await Delay(1000);
                        
            DoScreenFadeOut(500);
            while (IsScreenFadingOut())
                await Delay(1);
                
            await Delay(500);

            var spawnPos = new Vector3(-1042.389f, -2745.814f, 20.3594f);
            
            SetEntityCollision(GetPlayerPed(-1), true, true);
            RequestCollisionAtCoord(spawnPos.X, spawnPos.Y, spawnPos.Z);
            Client.Sync.Data.ResetLocally(User.GetServerId(), "isTaxi");
            CitizenFX.Core.UI.Screen.Hud.IsVisible = true;
            
            await Delay(500);
            
            SetEntityCollision(GetPlayerPed(-1), false, true);
            SetEntityCoords(GetPlayerPed(-1), spawnPos.X, spawnPos.Y, spawnPos.Z, true, false, false, true);
            NetworkResurrectLocalPlayer(spawnPos.X, spawnPos.Y, spawnPos.Z, 0, true, false);
            User.PedRotation(328.8829f);
            Client.Sync.Data.ResetLocally(User.GetServerId(), "isFly");

            new CitizenFX.Core.Ped(taxiDriver).Delete();
            new CitizenFX.Core.Vehicle(taxiCar).Delete();
            
            SetCinematicModeActive(false);
            
            await Delay(500);
            
            DoScreenFadeIn(500);
            while (IsScreenFadingIn())
                await Delay(1);
            
            Notification.Send("~g~Вы прибыли в г. Лос-Сантос.");
            await Delay(1500);
            Notification.Send("~g~Чтобы открыть главное меню, намите ~b~M~s~");
            await Delay(1500);
            if (User.GetVipStatus() == "none" && User.Data.last_login < User.Data.date_reg + 604800)
                Notification.SendWithTime("~b~У Ваc активирован VIP Light на 1 неделю");
            await Delay(1500);
            Notification.Send("~g~Приятной игры на Appi RolePlay :3");
        }
        public static async void PlaneDialog()
        {
            await Delay(5000);
            UI.ShowToolTip("Привет! Добро пожаловать на проект ~b~Alamo RolePlay ~s~. Сейчас в этом окне мы в краце расскажем о этом сервере.");
            await Delay(20000);
            UI.ShowToolTip("Это ~b~RolePlay~s~ сервер, где необходимо отыгрывать свою роль. Советуем Вам ознакомиться с правилами сервера и посетить наш дискорд ~b~dscrd.in/appi~s~ и сайт ~b~alamo-rp.com~s~ на котором есть вся информация по серверу.");
            await Delay(20000);
            UI.ShowToolTip("~b~RolePlay~s~ - это режим который моделирует реальную жизнь, у вашего персонажа есть потребности, возможность иметь различное имущество, вы можете быть как преступником, так и полицейским, а может и обычным гражданским. Всё зависит только от вас.");
            await Delay(25000);
            UI.ShowToolTip("Самолет скоро приземлится.\nЖелаем вам приятной игры и хорошего настроения на ~b~Alamo RolePlay");
        }
        
        private static async Task TickTimer()
        {
            if (Client.Sync.Data.HasLocally(User.GetServerId(), "isFly"))
                SetPedInVehicleContext(PlayerPedId(), (uint) GetHashKey("MINI_PROSTITUTE_LOW_PASSENGER"));
        }
        
        public static async void GoGoTaxi2()
        {
            var model1 = (uint)GetHashKey("jet");
            var model2 = (uint)GetHashKey("s_m_m_pilot_01");
            if (IsModelInCdimage(model1))
            {
                RequestModel(model1);
                while (!HasModelLoaded(model1))
                    await Delay(1);
            }
            if (IsModelInCdimage(model2))
            {
                RequestModel(model2);
                while (!HasModelLoaded(model2))
                    await Delay(1);
            }
            var coords = GetEntityCoords(PlayerPedId(),true);
            var taxiCar = CreateVehicle(model1, coords.X + 2f, coords.Y + 2f, coords.Z, 175.716f, false, false);
            var taxiDriver = CreatePedInsideVehicle(taxiCar, 26, model2, -1, false, false);
            
            //CreatePedInsideVehicle(taxiCar, 26, model2, 0, false, false);

            var groupHandle = GetPlayerGroup(PlayerPedId());
            SetGroupSeparationRange(groupHandle, 10f);
            SetPedNeverLeavesGroup(taxiDriver, true);
            SetPedIntoVehicle(PlayerPedId(), taxiCar, 0);

            TaskPlaneMission(taxiDriver, taxiCar, 0, 0, -107.2212f, 2717.5534f, 61.9673f, 0, 1000f, 0, 0.0f, 2500.0f, 100f);

            await Delay(1000);
            
            SetVehicleLandingGear(taxiCar, 1);

            //TaskPlaneLand(taxiDriver, taxiCar, coords.X + 2f, coords.Y + 2f, coords.Z, -1467.7531738282f, -477.02423095704f, 34.681056976318f);
        }
    }
}