using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Business
{
    public class CarWash : BaseScript
    {
        public static void LoadAll()
        {
            var blip = World.CreateBlip(new Vector3(-700.0402f, -932.4921f, 18.34011f));
            blip.Sprite = (BlipSprite) 100;
            //blip.Color = (BlipColor) 26;
            blip.Name = Lang.GetTextToPlayer("_lang_46");
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            Managers.Checkpoint.Create(blip.Position, 4f, "vehicle:wash");
            
            blip = World.CreateBlip(new Vector3(22.56987f, -1391.852f, 28.91351f));
            blip.Sprite = (BlipSprite) 100;
            //blip.Color = (BlipColor) 26;
            blip.Name = Lang.GetTextToPlayer("_lang_46");
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            Managers.Checkpoint.Create(blip.Position, 4f, "vehicle:wash");
            
            blip = World.CreateBlip(new Vector3(170.6151f, -1718.647f, 28.88343f));
            blip.Sprite = (BlipSprite) 100;
            //blip.Color = (BlipColor) 26;
            blip.Name = Lang.GetTextToPlayer("_lang_46");
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            Managers.Checkpoint.Create(blip.Position, 4f, "vehicle:wash");
        }

        public static async void Wash(CitizenFX.Core.Vehicle veh)
        {
            DoScreenFadeOut(500);

            while (IsScreenFadingOut())
                await Delay(1);
            
            await Delay(500);
            
            veh.Wash();
            veh.DirtLevel = 0;
            User.RemoveCashMoney(21);
            Business.AddMoney(113, 21);
            
            await Delay(500);
            
            DoScreenFadeIn(500);
            
            while (IsScreenFadingIn())
                await Delay(1);
            
            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_47"));
        }
    }
}