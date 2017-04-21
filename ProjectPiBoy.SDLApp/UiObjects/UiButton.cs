using ProjectPiBoy.Common.Utilities;
using ProjectPiBoy.SDLApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.SDL;
using ProjectPiBoy.SDLApp.Input;
using ProjectPiBoy.SDLApp.Screens;
using ProjectPiBoy.SDLApp.Events;

namespace ProjectPiBoy.SDLApp.UiObjects
{
    /// <summary>
    /// Represents a button that can be pressed.
    /// </summary>
    public class UiButton : UiObject
    {
        /// <summary>Whether the button is pressed or not</summary>
        public bool Pressed { get; protected set; }

        //TODO: Add more button stuff

        public UiButton(Screen screen, UiObjectPlacement placement) : base(screen, placement)
        {
            
        }

        public override void Render(IntPtr renderer, Vector2 screenDimensions, Assets assets, bool showDebugBorders)
        {
            Vector2 topLeft = ScreenSpaceUtil.PercentageToGlobal(this.GlobalPlacement.TopLeft, screenDimensions);
            Vector2 bottomRight = ScreenSpaceUtil.PercentageToGlobal(this.GlobalPlacement.BottomRight, screenDimensions);

            SDL_Rect buttonRect = ScreenSpaceUtil.GetGlobalBorderRectangle(this.GlobalPlacement, screenDimensions);

            Color backgroundColor;

            if (this.Pressed)
                backgroundColor = assets.Theme.SecondaryActiveColor;
            else if (this.Hovered)
                backgroundColor = assets.Theme.SecondaryHighlightColor;
            else
                backgroundColor = assets.Theme.SecondaryColor;

            //Background
            SDLUtil.SetSDLRenderDrawColor(renderer, backgroundColor);
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

        public override void OnTouchDown(TouchInputEventArgs e)
        {
            base.OnTouchDown(e);

            e.Handled = true;
            this.Pressed = true;
            this.Click?.Invoke();
            //Console.WriteLine($"{nameof(UiButton)}(\"{this.Content}\"): OnTouchDown called and handled!");
        }

        public override void OnTouchUp(TouchInputEventArgs e)
        {
            base.OnTouchUp(e);

            e.Handled = true;
            this.Pressed = false;
        }

        /// <summary>Fired when the button is clicked</summary>
        public event Action Click;
    }
}
