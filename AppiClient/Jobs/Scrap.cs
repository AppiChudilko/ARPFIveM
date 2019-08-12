using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Jobs
{
    public class Scrap : BaseScript
    {
        public static readonly Vector3 LoadPos = new Vector3(-470.6644f, -1693.163f, 17.50471f);
        public static readonly Vector3 UnLoadPos = new Vector3(1078.731f, -1967.789f, 30.0631f);
        public static CitizenFX.Core.Blip Blip = null;
        
        public static void Load()
        {
            if (!User.IsDriver())
            {
                Notification.SendWithTime("~r~Вы должны быть за рулём");
                return;
            }

            var veh = new CitizenFX.Core.Vehicle(User.GetVehicleIsDriver());

            if (veh.Model.Hash != -1700801569)
            {
                Notification.SendWithTime("~r~Нужно находиться в рабочем транспорте");
                return;
            }
            
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            if (Main.GetDistanceToSquared(LoadPos, playerPos) < 5f)
            {
                Sync.Data.SetLocally(VehToNet(veh.Handle), "veh:scrapLoad", true);
                Notification.SendWithTime("~g~Вы загрузили металлолом");
                Managers.Blip.Create(UnLoadPos);
                Managers.Checkpoint.CreateWithMarker(UnLoadPos, 5f, 5f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "checkpoint:withdelete");
                User.SetWaypoint(UnLoadPos.X, UnLoadPos.Y);
            }
            else
            {
                Notification.SendWithTime("~r~Вы слишком далеко");
                Managers.Checkpoint.CreateWithMarker(LoadPos, 5f, 5f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "checkpoint:withdelete");
                Managers.Blip.Create(LoadPos);
                User.SetWaypoint(LoadPos.X, LoadPos.Y);
            }
        }
        
        public static void UnLoad()
        {
            if (!User.IsDriver())
            {
                Notification.SendWithTime("~r~Вы должны быть за рулём");
                return;
            }

            var veh = new CitizenFX.Core.Vehicle(User.GetVehicleIsDriver());

            if (veh.Model.Hash != -1700801569)
            {
                Notification.SendWithTime("~r~Нужно находиться в рабочем транспорте");
                return;
            }
            if (!Sync.Data.HasLocally(VehToNet(veh.Handle), "veh:scrapLoad"))
            {
                Notification.SendWithTime("~r~В транспорте нет груза");
                return;
            }
            
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            if (Main.GetDistanceToSquared(UnLoadPos, playerPos) < 5f)
            {
                Sync.Data.ResetLocally(VehToNet(veh.Handle), "veh:scrapLoad");
                Notification.SendWithTime("~g~Вы разагрузили металлолом");
                User.GiveJobMoney(50);
                Managers.Blip.Delete();
            }
            else
            {
                Notification.SendWithTime("~r~Вы слишком далеко");
                Managers.Checkpoint.CreateWithMarker(UnLoadPos, 5f, 5f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "checkpoint:withdelete");
                Managers.Blip.Create(UnLoadPos);
                User.SetWaypoint(UnLoadPos.X, UnLoadPos.Y);
            }
        }
    }
}