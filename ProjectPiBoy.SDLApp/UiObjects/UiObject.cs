using ProjectPiBoy.Common.Utilities;
using ProjectPiBoy.SDLApp.Input;
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
        public UiObject() : this(default(UiObjectPlacement)) { }

        public UiObject(UiObjectPlacement placement, Vector2 placementOffset = new Vector2())
        {
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

        /// <summary>Whether the <see cref="UiObject"/> is being hovered over</summary>
        public bool Hovered { get; protected set; }

        public virtual void Render(IntPtr renderer, Vector2 screenDimensions, Assets assets, bool showDebugBorders)
        {
            //TODO: This needs replaced, see OnTouchHover comments
            this.Hovered = false;

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

        public override bool OnTouchHover(TouchInputEventArgs e)
        {
            //TODO: This needs to be controlled in a better way. This is only called when the mouse moves!
            this.Hovered = true;
            return true;
        }
    }
}
