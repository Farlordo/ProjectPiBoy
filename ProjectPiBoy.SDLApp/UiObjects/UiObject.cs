using ProjectPiBoy.Common.Utilities;
using ProjectPiBoy.SDLApp.Input;
using ProjectPiBoy.SDLApp.Screens;
using ProjectPiBoy.SDLApp.Utilities;
using System;
using static SDL2.SDL;

namespace ProjectPiBoy.SDLApp.UiObjects
{
    /// <summary>
    /// Represents a UI Object that can be displayed on a screen.
    /// </summary>
    public abstract class UiObject : TouchListener, IDisposable, IRenderable
    {
        public UiObject(Screen screen) : this(screen, default(UiObjectPlacement)) { }

        public UiObject(Screen screen, UiObjectPlacement placement, Vector2 placementOffset = new Vector2())
        {
            this.Screen = screen;
            this.Placement = placement;
            this.PlacementOffset = placementOffset;
        }

        /// <summary>The placement of this <see cref="UiObject"/></summary>
        public virtual UiObjectPlacement Placement { get; set; }

        /// <summary>The offset of the placement</summary>
        public virtual Vector2 PlacementOffset { get; set; }

        /// <summary>
        /// Calculates and returns the global placement of this <see cref="UiObject"/>
        /// </summary>
        /// <returns>The global placement of this <see cref="UiObject"/></returns>
        public virtual UiObjectPlacement GetGlobalPlacement()
        {
            //Create a copy of this UiObject's placement
            UiObjectPlacement globalPlacement = new UiObjectPlacement(this.Placement);

            //Add the placement offset to the placement
            globalPlacement.PosVec += this.PlacementOffset;

            //Return the new, global placement
            return globalPlacement;
        }

        /// <summary>
        /// Determines whether this <see cref="UiObject"/> contains the specified point.
        /// </summary>
        /// <remarks>
        /// Override this if your <see cref="UiObject"/> uses its placement differently
        /// </remarks>
        /// <param name="point">The point to check</param>
        /// <returns>Whether this <see cref="UiObject"/> contains the specified point</returns>
        public virtual bool ContainsGlobalPoint(Vector2 point)
        {
            return this.GetGlobalPlacement().ContainsPoint(point);
        }

        /// <summary>The screen that this <see cref="UiObject"/> is on</summary>
        public Screen Screen { get; }

        /// <summary>Whether the <see cref="UiObject"/> is being hovered over</summary>
        public bool Hovered { get; protected set; }

        public virtual void Render(IntPtr renderer, Vector2 screenDimensions, Assets assets, bool showDebugBorders)
        {
            bool hovered = false;

            //If there are any touches over this object, then it is being hovered over
            foreach (var touch in this.Screen.Touches)
                if (this.ContainsGlobalPoint(touch.Value))
                {
                    hovered = true;
                    break;
                }

            //Update hovered state
            this.Hovered = hovered;

            if (showDebugBorders)
            {
                //Get a SDL_Rect representing the border
                SDL_Rect border = ScreenSpaceUtil.GetGlobalBorderRectangle(this.GetGlobalPlacement(), screenDimensions);

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
