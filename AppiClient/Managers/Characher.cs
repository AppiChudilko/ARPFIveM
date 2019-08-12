using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class Characher : BaseScript
    {
        public static async void UpdateFace(bool withSync = true)
        {
            if (withSync)
                await User.GetAllSkin();
            
            if (!Client.Sync.Data.HasLocally(User.GetServerId(), "hasMask") && !Client.Sync.Data.HasLocally(User.GetServerId(), "hasBuyMask"))
                SetPedHeadBlendData(
                    GetPlayerPed(-1),
                    User.Skin.GTAO_SHAPE_THRID_ID,    
                    User.Skin.GTAO_SHAPE_SECOND_ID,    
                    User.Skin.GTAO_SHAPE_FIRST_ID,    
                    User.Skin.GTAO_SKIN_THRID_ID,    
                    User.Skin.GTAO_SKIN_SECOND_ID,    
                    User.Skin.GTAO_SKIN_FIRST_ID,    
                    User.Skin.GTAO_SHAPE_MIX,    
                    User.Skin.GTAO_SKIN_MIX,    
                    User.Skin.GTAO_THRID_MIX,    
                    false    
                );
            
            SetPedHairColor(GetPlayerPed(-1), User.Skin.GTAO_HAIR_COLOR, User.Skin.GTAO_HAIR_COLOR2);
            SetPedComponentVariation(GetPlayerPed(-1), 2, User.Skin.GTAO_HAIR, 0, 0);
            SetPedEyeColor(GetPlayerPed(-1), User.Skin.GTAO_EYE_COLOR);
            
            SetPedHeadOverlay(GetPlayerPed(-1), 2, User.Skin.GTAO_EYEBROWS, 1f);
            SetPedHeadOverlayColor(GetPlayerPed(-1), 2, 1, User.Skin.GTAO_EYEBROWS_COLOR, 0);
            
            /*
            
            4 Макияж 0 - 74, 255
            5 Румяна 0 - 6, 255
            6 Цвет лица 0 - 11, 255
            7 Sun Damage 0 - 10, 255
            8 Губная помада 0 - 9, 255
            9 Веснушки 0 - 17, 255
            10 Грудь Волосы 0 - 16, 255
            */

            if (User.Data.age > 72)
            {
                SetPedHeadOverlay(GetPlayerPed(-1), 3, 14, 1f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 3, 0, 1, 1);
            }
            else if (User.Data.age > 69)
            {
                SetPedHeadOverlay(GetPlayerPed(-1), 3, 16, 1f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 3, 0, 1, 1);
            }
            else if (User.Data.age > 66)
            {
                SetPedHeadOverlay(GetPlayerPed(-1), 3, 12, 1f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 3, 0, 1, 1);
            }
            else if (User.Data.age > 63)
            {
                SetPedHeadOverlay(GetPlayerPed(-1), 3, 11, 0.9f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 3, 0, 1, 1);
            }
            else if (User.Data.age > 60)
            {
                SetPedHeadOverlay(GetPlayerPed(-1), 3, 10, 0.9f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 3, 0, 1, 1);
            }
            else if (User.Data.age > 57)
            {
                SetPedHeadOverlay(GetPlayerPed(-1), 3, 9, 0.8f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 3, 0, 1, 1);
            }
            else if (User.Data.age > 54)
            {
                SetPedHeadOverlay(GetPlayerPed(-1), 3, 8, 0.8f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 3, 0, 1, 1);
            }
            else if (User.Data.age > 51)
            {
                SetPedHeadOverlay(GetPlayerPed(-1), 3, 7, 0.7f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 3, 0, 1, 1);
            }
            else if (User.Data.age > 48)
            {
                SetPedHeadOverlay(GetPlayerPed(-1), 3, 6, 0.7f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 3, 0, 1, 1);
            }
            else if (User.Data.age > 45)
            {
                SetPedHeadOverlay(GetPlayerPed(-1), 3, 5, 0.6f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 3, 0, 1, 1);
            }
            else if (User.Data.age > 42)
            {
                SetPedHeadOverlay(GetPlayerPed(-1), 3, 4, 0.5f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 3, 0, 1, 1);
            }
            else if (User.Data.age > 39)
            {
                SetPedHeadOverlay(GetPlayerPed(-1), 3, 3, 0.4f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 3, 0, 1, 1);
            }
            else if (User.Data.age > 36)
            {
                SetPedHeadOverlay(GetPlayerPed(-1), 3, 2, 0.3f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 3, 0, 1, 1);
            }
            else if (User.Data.age > 33)
            {
                SetPedHeadOverlay(GetPlayerPed(-1), 3, 1, 0.2f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 3, 0, 1, 1);
            }
            else if (User.Data.age > 30)
            {
                SetPedHeadOverlay(GetPlayerPed(-1), 3, 0, 0.2f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 3, 0, 1, 1);
            }
            
            if (User.Skin.GTAO_OVERLAY9 != -1)
            {
                SetPedHeadOverlay(GetPlayerPed(-1), 9, User.Skin.GTAO_OVERLAY9, 1f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 9, 0, User.Skin.GTAO_OVERLAY9_COLOR, User.Skin.GTAO_OVERLAY9_COLOR);
            }

            if (User.Skin.SEX == 0)
            {
                /*if (User.Skin.GTAO_OVERLAY10 != -1)
                {
                    SetPedHeadOverlay(GetPlayerPed(-1), 10, User.Skin.GTAO_OVERLAY10, 1f);
                    SetPedHeadOverlayColor(GetPlayerPed(-1), 10, 1, User.Skin.GTAO_OVERLAY10_COLOR, User.Skin.GTAO_OVERLAY10_COLOR);
                }*/
                
                SetPedHeadOverlay(GetPlayerPed(-1), 10, User.Skin.GTAO_OVERLAY10, 1f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 10, 1, User.Skin.GTAO_OVERLAY10_COLOR, User.Skin.GTAO_OVERLAY10_COLOR);
                
                //if (User.Skin.GTAO_OVERLAY == -1) return;
                SetPedHeadOverlay(GetPlayerPed(-1), 1, User.Skin.GTAO_OVERLAY, 1f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 1, 1, User.Skin.GTAO_OVERLAY_COLOR, 0);
            }
            else if (User.Skin.SEX == 1)
            {
                SetPedHeadOverlay(GetPlayerPed(-1), 4, User.Skin.GTAO_OVERLAY4, 1f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 4, 0, User.Skin.GTAO_OVERLAY4_COLOR, User.Skin.GTAO_OVERLAY4_COLOR);
           
                SetPedHeadOverlay(GetPlayerPed(-1), 5, User.Skin.GTAO_OVERLAY5, 0.4f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 5, 2, User.Skin.GTAO_OVERLAY5_COLOR, User.Skin.GTAO_OVERLAY5_COLOR);
            
                SetPedHeadOverlay(GetPlayerPed(-1), 8, User.Skin.GTAO_OVERLAY8, 1f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 8, 2, User.Skin.GTAO_OVERLAY8_COLOR, User.Skin.GTAO_OVERLAY8_COLOR);
            
                /*SetPedHeadOverlay(GetPlayerPed(-1), 6, User.Skin.GTAO_OVERLAY6, 1f);
                SetPedHeadOverlayColor(GetPlayerPed(-1), 6, 0, User.Skin.GTAO_OVERLAY6_COLOR, User.Skin.GTAO_OVERLAY6_COLOR);*/
                
                /*if (User.Skin.GTAO_OVERLAY4 != -1)
                {
                    SetPedHeadOverlay(GetPlayerPed(-1), 4, User.Skin.GTAO_OVERLAY4, 1f);
                    SetPedHeadOverlayColor(GetPlayerPed(-1), 4, 0, User.Skin.GTAO_OVERLAY4_COLOR, User.Skin.GTAO_OVERLAY4_COLOR);
                }

                if (User.Skin.GTAO_OVERLAY5 != -1)
                {
                    SetPedHeadOverlay(GetPlayerPed(-1), 5, User.Skin.GTAO_OVERLAY5, 1f);
                    SetPedHeadOverlayColor(GetPlayerPed(-1), 5, 2, User.Skin.GTAO_OVERLAY5_COLOR, User.Skin.GTAO_OVERLAY5_COLOR);
                }

                if (User.Skin.GTAO_OVERLAY6 != -1)
                {
                    SetPedHeadOverlay(GetPlayerPed(-1), 6, User.Skin.GTAO_OVERLAY6, 1f);
                    SetPedHeadOverlayColor(GetPlayerPed(-1), 6, 0, User.Skin.GTAO_OVERLAY6_COLOR, User.Skin.GTAO_OVERLAY6_COLOR);
                }

                if (User.Skin.GTAO_OVERLAY8 != -1)
                {
                    SetPedHeadOverlay(GetPlayerPed(-1), 8, User.Skin.GTAO_OVERLAY8, 1f);
                    SetPedHeadOverlayColor(GetPlayerPed(-1), 8, 2, User.Skin.GTAO_OVERLAY8_COLOR, User.Skin.GTAO_OVERLAY8_COLOR);
                }*/
            }
        }
        
        public static async void UpdateCloth(bool withSync = true)
        {
            if (withSync)
                await User.GetAllData();
            
            if (Client.Sync.Data.HasLocally(User.GetServerId(), "hasBuyMask"))
                SetPedComponentVariation(GetPlayerPed(-1), 1, User.Data.mask, User.Data.mask_color, 2);
            
            ClearPedProp(GetPlayerPed(-1), 0);
            ClearPedProp(GetPlayerPed(-1), 1);
            ClearPedProp(GetPlayerPed(-1), 2);
            ClearPedProp(GetPlayerPed(-1), 6);
            ClearPedProp(GetPlayerPed(-1), 7);
            
            SetPedComponentVariation(GetPlayerPed(-1), 3, User.Data.torso, User.Data.torso_color, 2);
            SetPedComponentVariation(GetPlayerPed(-1), 4, User.Data.leg, User.Data.leg_color, 2);
            SetPedComponentVariation(GetPlayerPed(-1), 5, User.Data.hand, User.Data.hand_color, 2);
            SetPedComponentVariation(GetPlayerPed(-1), 6, User.Data.foot, User.Data.foot_color, 2);
            SetPedComponentVariation(GetPlayerPed(-1), 7, User.Data.accessorie, User.Data.accessorie_color, 2);
            SetPedComponentVariation(GetPlayerPed(-1), 8, User.Data.parachute, User.Data.parachute_color, 2);
            SetPedComponentVariation(GetPlayerPed(-1), 9, User.Data.armor, User.Data.armor_color, 2);
            SetPedComponentVariation(GetPlayerPed(-1), 10, User.Data.decal, User.Data.decal_color, 2);
            SetPedComponentVariation(GetPlayerPed(-1), 11, User.Data.body, User.Data.body_color, 2);
            //SetPedComponentVariation(GetPlayerPed(-1), 2, User.Data.leg, User.Data.leg_color, 2);
            
            if (User.Data.hat >= 0)
                SetPedPropIndex(GetPlayerPed(-1), 0, User.Data.hat, User.Data.hat_color, true);
            if (User.Data.glasses >= 0)
                SetPedPropIndex(GetPlayerPed(-1), 1, User.Data.glasses, User.Data.glasses_color, true);
            if (User.Data.ear >= 0)
                SetPedPropIndex(GetPlayerPed(-1), 2, User.Data.ear, User.Data.ear_color, true);
            if (User.Data.watch >= 0)
                SetPedPropIndex(GetPlayerPed(-1), 6, User.Data.watch, User.Data.watch_color, true);
            if (User.Data.bracelet >= 0)
                SetPedPropIndex(GetPlayerPed(-1), 7, User.Data.bracelet, User.Data.bracelet_color, true);

            UpdateTattoo(false);
        }
        
        public static async void UpdateTattoo(bool withSync = true)
        {
            if (withSync)
                await User.GetAllData();
            
            ClearPedDecorations(GetPlayerPed(-1));
            
            ApplyPedOverlay(GetPlayerPed(-1), (uint) GetHashKey(User.Data.tattoo_head_c), (uint) GetHashKey(User.Data.tattoo_head_o));
            ApplyPedOverlay(GetPlayerPed(-1), (uint) GetHashKey(User.Data.tattoo_torso_c), (uint) GetHashKey(User.Data.tattoo_torso_o));
            ApplyPedOverlay(GetPlayerPed(-1), (uint) GetHashKey(User.Data.tattoo_left_arm_c), (uint) GetHashKey(User.Data.tattoo_left_arm_o));
            ApplyPedOverlay(GetPlayerPed(-1), (uint) GetHashKey(User.Data.tattoo_right_arm_c), (uint) GetHashKey(User.Data.tattoo_right_arm_o));
            ApplyPedOverlay(GetPlayerPed(-1), (uint) GetHashKey(User.Data.tattoo_left_leg_c), (uint) GetHashKey(User.Data.tattoo_left_leg_o));
            ApplyPedOverlay(GetPlayerPed(-1), (uint) GetHashKey(User.Data.tattoo_right_leg_c), (uint) GetHashKey(User.Data.tattoo_right_leg_o));
        }
    }
}