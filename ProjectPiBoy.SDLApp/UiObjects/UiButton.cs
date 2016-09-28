using ProjectPiBoy.Common.Utilities;
using ProjectPiBoy.SDLApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.SDL;

namespace ProjectPiBoy.SDLApp.UiObjects
{
    /// <summary>
    /// Represents a button that can be pressed.
    /// </summary>
    public class UiButton : UiContainer
    {
        //TODO: Add more button stuff

        public UiButton(UiObjectPlacement placement) : base(placement)
        {
            
        }

        public override void Render(IntPtr renderer, Vector2 screenDimensions, Assets assets, bool showDebugBorders)
        {
            UiObjectPlacement globalPlacement = this.GetGlobalPlacement();
            Vector2 topLeft = ScreenSpaceUtil.PercentageToGlobal(globalPlacement.TopLeft, screenDimensions);
            Vector2 bottomRight = ScreenSpaceUtil.PercentageToGlobal(globalPlacement.BottomRight, screenDimensions);

            SDL_Rect buttonRect = ScreenSpaceUtil.GetGlobalBorderRectangle(globalPlacement, screenDimensions);

            //Background
            SDLUtil.SetSDLRenderDrawColor(renderer, assets.Theme.SecondaryColor);
            SDL_RenderFillRect(renderer, ref buttonRect);

            //Outline
            SDLUtil.SetSDLRenderDrawColor(renderer, assets.Theme.PrimaryColor);
            //SDL_RenderSetScale(renderer, 2, 2);
            SDL_RenderDrawRect(renderer, ref buttonRect);
            //SDL_RenderSetScale(renderer, 1, 1);
            //TODO: Use scaling to draw thicker lines. This requires scaling the drawing coordinates as well. Maybe a using() thing could be used to manage the scale.

            //Render the base button, and contents
            base.Render(renderer, screenDimensions, assets, showDebugBorders);
        }
    }
}
