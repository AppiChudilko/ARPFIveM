using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class Spawn : BaseScript
    {
        private static bool _spawnLock = false;
        public static readonly Vector3 HospSpawn = new Vector3(353.8642f, -580.8f, 42.28162f);

        public Spawn()
        {
            EventHandlers.Add("ARP:SpawnPlayer", new Action<string, float, float, float, float, bool>(SpawnEventPlayer));
            EventHandlers.Add("ARP:SpawnPlayerAuto", new Action(SpawnPlayerAuto));
            EventHandlers.Add("ARP:SpawnPlayerHospital", new Action(SpawnPlayerHospital));
            
            Tick += RespawnTimer;
        }
        
        private static async Task RespawnTimer()
        {
            await Delay(5000);
            if (User.IsDead())
            {
                var ped = GetPlayerPed(-1);
                
                //User.Freeze(true);
                
                /*var playerPos = GetEntityCoords(ped, true);
                NetworkResurrectLocalPlayer(playerPos.X, playerPos.Y, playerPos.Z, GetEntityHeading(ped), true, true);
                SetEntityHealth(ped, 101);*/
                        
                StartScreenEffect("DeathFailOut", 0, true);
                NetworkSetTalkerProximity(1f);

                if (User.IsAdmin())
                    User.Respawn(HospSpawn, 90);
                else
                {
                    var distance = Main.GetDistanceToSquared(GetEntityCoords(ped, true), new Vector3(342.6589f, -1397.471f, 32.50927f));
                    int distanceMin = 3;
                        
                    if (distance >= 6000)
                        distanceMin = 8;
                    if (distance >= 4000 && distance < 6000)
                        distanceMin = 7;
                    else if (distance >= 2000 && distance < 4000)
                        distanceMin = 6;
                    else if (distance > 200 && distance < 2000)
                        distanceMin = 5;
                    
                    //if (Main.ServerName == "SunFlower")
                    //    distanceMin = 3;
                        
                    for (int i = distanceMin; i > 0; i--)
                    {
                        if (!User.IsDead()) continue;
                        Notification.SendWithTime($"Осталось до респавна: {i} мин.");
                        //SetPedToRagdoll(ped, 60000, 60000, 0, false, false, false);
                        await Delay(1000 * 60);
                    }
                }
                StopAllScreenEffects();
                
                if (User.IsDead())
                {
                    User.Respawn(HospSpawn, 90);
                    SetEntityHealth(GetPlayerPed(-1), GetEntityHealth(GetPlayerPed(-1)) - 80); // эксперимент
                    if (User.Data.wanted_level > 0)
                        Jail.JailPlayerScene(User.Data.wanted_level * 600);
                }
            }
        }

        public static void SpawnPlayerAuto()
        {
            /*if (User.IsLogin()) {
            
                return;
            }
            SpawnPlayerAuto();*/
        }

        public static void SpawnPlayerHospital()
        {
            //SpawnPlayer(skin, x, y, z, heading, checkIsLogin);
        }

        public static void SpawnEventPlayer(string skin, float x, float y, float z, float heading, bool checkIsLogin = false)
        {
            SpawnPlayer(skin, x, y, z, heading, checkIsLogin);
        }
        
        public static async void SpawnPlayer(string skin, float x, float y, float z, float heading, bool checkIsLogin = false, bool isFreeze = true, bool isInvisible = true)
        {
            if (_spawnLock)
                return;

            _spawnLock = true;

            DoScreenFadeOut(500);

            while (IsScreenFadingOut())
                await Delay(1);

            if (isFreeze)
                User.Freeze(PlayerId(), true);
            
            if (isInvisible)
                User.Invisible(PlayerId(), true);
            
            uint spawnModel = (uint) GetHashKey(skin);
            //NetworkFadeOutEntity(GetPlayerPed(-1), true, true);
            
            RequestModel(spawnModel);
            while (!HasModelLoaded(spawnModel))
            {
                RequestModel(spawnModel);
                await Delay(1);
            }

            SetPlayerModel(PlayerId(), spawnModel);
            SetModelAsNoLongerNeeded(spawnModel);

            RequestCollisionAtCoord(x, y, z);

            var ped = GetPlayerPed(-1);

            SetEntityCoordsNoOffset(ped, x, y, z, false, false, false);
            NetworkResurrectLocalPlayer(x, y, z, heading, true, true);
            ClearPedTasksImmediately(ped);
            RemoveAllPedWeapons(ped, false);
            User.RemoveWeapons();
            ClearPlayerWantedLevel(PlayerId());
            
            while (!HasCollisionLoadedAroundEntity(ped))
                await Delay(1);

            if (checkIsLogin)
            {
                await User.GetAllData();
                await User.GetAllSkin();
                
                User.IsAuth = User.Data.is_auth;
                
                while (!User.IsLogin()) {
                    await User.GetAllData();
                    User.IsAuth = User.Data.is_auth;
                    await Delay(500);
                }
                
                User.Money = User.Data.money;
                
                //if (User.IsGang())
                Grab.LoadBlips();
                
                Characher.UpdateFace(false);
                Characher.UpdateCloth(false);
            }

            if (!User.Data.s_is_characher)
                await Delay(10000);

            if (User.GetPlayerVirtualWorld() == -433 || User.GetPlayerVirtualWorld() == -535)
                await Delay(20000);

            if (User.IsLogin() && !User.Data.s_is_characher && User.Data.age == 18 && User.Data.exp_age < 2)
            {
                var rand = new Random();
                User.SetVirtualWorld(rand.Next(10000));
                User.SetSkin("mp_m_freemode_01");

                RequestCollisionAtCoord(9.653649f, 528.3086f, 169.635f);
                await Delay(1000);
                
                SetEntityCoordsNoOffset(ped, 9.653649f, 528.3086f, 169.635f, false, false, false);
                NetworkResurrectLocalPlayer(9.653649f, 528.3086f, 169.635f, 120.0613f, true, true);
                
                User.PedRotation(120.0613f);
                    
                User.Freeze(PlayerId(), true);
                MenuList.ShowCharacherCustomMenu();
                /*Camera.Position = new Vector3(-7.973644f, 513.0889f, 174.6281f);
                Camera.PointAt(new Vector3(-10.10947f, 508.5056f, 174.6281f));*/
                    
                Characher.UpdateFace();
                Characher.UpdateCloth();
                    
                MenuList.Camera = new CitizenFX.Core.Camera(CreateCam("DEFAULT_SCRIPTED_CAMERA", true));
                MenuList.Camera.IsActive = true;
                MenuList.Camera.Position = new Vector3(8.243752f, 527.4373f, 171.6173f);
                MenuList.Camera.PointAt(new Vector3(9.653649f, 528.3086f, 171.335f));
                RenderScriptCams(true, false, MenuList.Camera.Handle, false, false);
            }
            //NetworkFadeInEntity(GetPlayerPed(-1), false);
            
            if(GetIsLoadingScreenActive())
                ShutdownLoadingScreen();
            
            DoScreenFadeIn(500);
            
            while (IsScreenFadingIn())
                await Delay(1);
            
            if (isFreeze)
                User.Freeze(PlayerId(), false);
            
            if (isInvisible)
                User.Invisible(PlayerId(), false);

            _spawnLock = false;
            
            if (User.IsLogin() && !User.Data.s_is_characher && (User.Data.age == 18 && User.Data.exp_age >= 2 || User.Data.age > 18))
                MenuList.ShowAskCharacherCustomMenu();
        }
    }
}