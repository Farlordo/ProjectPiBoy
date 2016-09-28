﻿using ProjectPiBoy.Common.Utilities;
using ProjectPiBoy.SDLApp.Utilities;
using System;
using static SDL2.SDL;

namespace ProjectPiBoy.SDLApp.UiObjects
{
    /// <summary>
    /// Represents a UI Object that can be displayed on a screen.
    /// </summary>
    public abstract class UiObject : IDisposable, IRenderable
    {
        public UiObject() : this(default(UiObjectPlacement)) { }

        public UiObject(UiObjectPlacement placement)
        {
            this.Placement = placement;
        }

        /// <summary>The placement of this <see cref="UiObject"/></summary>
        public UiObjectPlacement Placement { get; set; }

        public virtual void Render(IntPtr renderer, Vector2 screenDimensions, Assets assets, bool showDebugBorders)
        {
            if (showDebugBorders)
            {
                //Get a SDL_Rect representing the border
                SDL_Rect border = ScreenSpaceUtil.GetGlobalBorderRectangle(this.Placement, screenDimensions);

                //Set the color to green, and render the border rectangle
                SDLUtil.SetSDLRenderDrawColor(renderer, assets.Theme.DebugOutlineColor);
                SDL_RenderDrawRect(renderer, ref border);
            }
        }

        public virtual void Dispose()
        {
            //Nothing to dispose
        }
    }
}
