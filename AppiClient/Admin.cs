using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Client
{
    public class Admin : BaseScript
    {
        protected static CitizenFX.Core.Blip BlipMp = null;
        protected static Dictionary<int, CitizenFX.Core.Blip> PlayerBlips = new Dictionary<int, CitizenFX.Core.Blip>();
        
        public Admin()
        {
            EventHandlers.Add("ARP:InviteMp", new Action<float, float, float>(MenuList.ShowAskInviteMpMenu));
            EventHandlers.Add("ARP:SlapMp", new Action<float, float, float, int, int>(User.SlapMp));
            EventHandlers.Add("ARP:GiveArmorMp", new Action<float, float, float, int, int>(User.GiveArmorMp));
            EventHandlers.Add("ARP:GiveHealthMp", new Action<float, float, float, int, int>(User.GiveHealthMp));
            EventHandlers.Add("ARP:GiveGunMp", new Action<float, float, float, int, string, int>(User.GiveGunMp));
            EventHandlers.Add("ARP:GiveSkinMp", new Action<float, float, float, int, string>(User.GiveSkinMp));
            EventHandlers.Add("ARP:Admin:JailPlayer", new Action<int, int, string>(User.AdminJailPlayer));
            
            EventHandlers.Add("ARP:CreateZoneMp", new Action<float, float, float, int>(CreateZoneMp));
            EventHandlers.Add("ARP:DeleteZoneMp", new Action(DeleteZoneMp));
            
            //Tick += TickTimer;
            Tick += Sec60Timer;
        }
        
        public static void CreateZoneMp(float x, float y, float z, int radius)
        {
            DeleteZoneMp();
            BlipMp = World.CreateBlip(new Vector3(x, y, z));
            BlipMp.Sprite = (BlipSprite) 10;
            BlipMp.Color = (BlipColor) 57;
            BlipMp.Name = "Мероприятие";
            BlipMp.IsShortRange = true;
            BlipMp.Scale = radius / 10f;
        }

        public static void DeleteZoneMp()
        {
            if (BlipMp == null) return;
            BlipMp.Delete();
            BlipMp = null;
        }
        
        private static async Task Sec60Timer()
        {
            await Delay(1000 * 60);

            if (User.IsAdmin())
            {
                foreach (var player in new PlayerList())
                {
                    if (player.ServerId == User.GetServerId()) continue; 
                    var entity = new MyEntity(GetPlayerPed(player.Handle));
                    if (!PlayerBlips.ContainsKey(player.ServerId))
                    {
                        var b = AddBlipForEntity(entity.Handle);
                        var blip = new Blip(b)
                        {
                            Sprite = (BlipSprite) 1,
                            Name = "Игрок",
                            IsShortRange = true
                        };
                        PlayerBlips.Add(player.ServerId, blip);
                    }
                }
            }
        }
    }
}