using ProjectPiBoy.SDLApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
            base.Render(renderer, screenDimensions, assets, showDebugBorders);

            Vector2 topLeft = ScreenSpaceUtils.PercentageToGlobal(this.Placement.TopLeft, screenDimensions);
            Vector2 bottomRight = ScreenSpaceUtils.PercentageToGlobal(this.Placement.BottomRight, screenDimensions);

            SDL_RenderDrawLine(renderer, (int) topLeft.X, (int)topLeft.Y, (int)bottomRight.X, (int)bottomRight.Y);
        }
    }
}
