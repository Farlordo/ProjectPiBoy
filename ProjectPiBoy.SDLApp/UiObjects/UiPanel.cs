using ProjectPiBoy.SDLApp.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectPiBoy.Common.Utilities;
using static SDL2.SDL;
using ProjectPiBoy.SDLApp.Utilities;

namespace ProjectPiBoy.SDLApp.UiObjects
{
    /// <summary>
    /// Represents a UI panel
    /// </summary>
    public class UiPanel : UiObject
    {
        /// <summary>The panel's background color</summary>
        public Color BackgroundColor { get; set; }

        public UiPanel(Screen screen, UiObjectPlacement placement) : base(screen, placement)
        {

        }

        public override void Render(IntPtr renderer, Vector2 screenDimensions, Assets assets, bool showDebugBorders)
        {
            Vector2 topLeft = ScreenSpaceUtil.PercentageToGlobal(this.GlobalPlacement.TopLeft, screenDimensions);
            Vector2 bottomRight = ScreenSpaceUtil.PercentageToGlobal(this.GlobalPlacement.BottomRight, screenDimensions);

            SDL_Rect panelRect = ScreenSpaceUtil.GetGlobalBorderRectangle(this.GlobalPlacement, screenDimensions);

            //Background
            SDLUtil.SetSDLRenderDrawColor(renderer, this.BackgroundColor);
            SDL_RenderFillRect(renderer, ref panelRect);

            //Outline
            SDLUtil.SetSDLRenderDrawColor(renderer, assets.Theme.PrimaryColor);
            SDL_RenderDrawRect(renderer, ref panelRect);

            //Render the base object, and contents
            base.Render(renderer, screenDimensions, assets, showDebugBorders);
        }
    }
}
