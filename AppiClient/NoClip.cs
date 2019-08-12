using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Client
{
    public class NoClip : BaseScript
    {
        public static bool NoClipEnabled = false;
        public static int CurrentSpeed = 1;

        public static readonly List<string> Speeds = new List<string>()
        {
            "Slow",
            "Medium",
            "Fast",
            "Very Fast",
            "Extremely Fast",
            "Snail Speed!"
        };

        public NoClip()
        {
            Tick += OnTick;
        }

        private async Task OnTick()
        {
            if (NoClipEnabled)
            {
                var noclipEntity = IsPedInAnyVehicle(PlayerPedId(), false) ? GetVehiclePedIsUsing(PlayerPedId()) : PlayerPedId();

                FreezeEntityPosition(noclipEntity, true);
                SetEntityInvincible(noclipEntity, true);

                Game.DisableControlThisFrame(0, Control.MoveUpOnly);
                Game.DisableControlThisFrame(0, Control.MoveUp);
                Game.DisableControlThisFrame(0, Control.MoveUpDown);
                Game.DisableControlThisFrame(0, Control.MoveDown);
                Game.DisableControlThisFrame(0, Control.MoveDownOnly);
                Game.DisableControlThisFrame(0, Control.MoveLeft);
                Game.DisableControlThisFrame(0, Control.MoveLeftOnly);
                Game.DisableControlThisFrame(0, Control.MoveLeftRight);
                Game.DisableControlThisFrame(0, Control.MoveRight);
                Game.DisableControlThisFrame(0, Control.MoveRightOnly);
                Game.DisableControlThisFrame(0, Control.Cover);
                Game.DisableControlThisFrame(0, Control.MultiplayerInfo);
                Game.DisableControlThisFrame(0, (Control) 47);

                //var xoff = 0.0f;
                var yoff = 0.0f;
                var zoff = 0.0f;

                if (Game.CurrentInputMode == InputMode.MouseAndKeyboard)
                {
                    if (Game.IsControlJustPressed(0, Control.Sprint))
                    {
                        CurrentSpeed++;
                        if (CurrentSpeed == Speeds.Count)
                            CurrentSpeed = 0;
                    }

                    if (Game.IsDisabledControlPressed(0, Control.MoveUpOnly))
                    {
                        yoff = 0.5f;
                    }
                    if (Game.IsDisabledControlPressed(0, Control.MoveDownOnly))
                    {
                        yoff = -0.5f;
                    }
                    if (Game.IsDisabledControlPressed(0, Control.MoveLeftOnly))
                    {
                        SetEntityHeading(PlayerPedId(), GetEntityHeading(PlayerPedId()) + 3f);
                    }
                    if (Game.IsDisabledControlPressed(0, Control.MoveRightOnly))
                    {
                        SetEntityHeading(PlayerPedId(), GetEntityHeading(PlayerPedId()) - 3f);
                    }
                    if (Game.IsDisabledControlPressed(0, Control.Cover))
                    {
                        zoff = 0.21f;
                    }
                    if (Game.IsDisabledControlPressed(0, Control.MultiplayerInfo))
                    {
                        zoff = -0.21f;
                    }
                    if (Game.IsDisabledControlPressed(0, (Control) 47))
                    {
                        NoClipEnabled = false;
                    }
                }

                var newPos = GetOffsetFromEntityInWorldCoords(noclipEntity, 0f, yoff * (CurrentSpeed + 0.5f),
                    zoff * (CurrentSpeed + 0.5f));

                var heading = GetEntityHeading(noclipEntity);
                SetEntityVelocity(noclipEntity, 0f, 0f, 0f);
                SetEntityRotation(noclipEntity, 0f, 0f, 0f, 0, false);
                SetEntityHeading(noclipEntity, heading);

                //if (!((yoff > -0.01f && yoff < 0.01f) && (zoff > -0.01f && zoff < 0.01f)))
                {
                    SetEntityCollision(noclipEntity, false, false);
                    SetEntityCoordsNoOffset(noclipEntity, newPos.X, newPos.Y, newPos.Z, true, true, true);
                }

                FreezeEntityPosition(noclipEntity, false);
                SetEntityInvincible(noclipEntity, false);
                SetEntityCollision(noclipEntity, true, true);
            }
            else
            {
                await Delay(100);
            }
        }
    }
}