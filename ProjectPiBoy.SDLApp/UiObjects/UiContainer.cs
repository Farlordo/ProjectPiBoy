using ProjectPiBoy.Common.Utilities;
using ProjectPiBoy.SDLApp.Input;
using ProjectPiBoy.SDLApp.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPiBoy.SDLApp.UiObjects
{
    /// <summary>
    /// Represents a <see cref="UiObject"/> that contains another single <see cref="UiObject"/>.
    /// </summary>
    public abstract class UiContainer : UiObject
    {
        public UiContainer(Screen screen, UiObjectPlacement placement) : base(screen, placement)
        {

        }

        public override Vector2 PlacementOffset
        {
            get { return base.PlacementOffset; }
            set
            {
                base.PlacementOffset = value;

                //Update the content's placement offset with this new placement offset (WARNING: This will not update if the this placement's PosVec updates! This might be considered a bug.)
                if (this.Content != null)
                    this.Content.PlacementOffset = this.PlacementOffset + this.Placement.PosVec;
            }
        }

        private UiObject _Content;
        /// <summary>The content of this <see cref="UiContainer"/></summary>
        public UiObject Content
        {
            get { return this._Content; }
            set
            {
                //Set the content's placement offset relative to this object's placement and placement offset
                value.PlacementOffset = this.PlacementOffset + this.Placement.PosVec;

                //Set the content's placement relative to this object's placement
                //value.Placement = new UiObjectPlacement(value.Placement) { PosVec = value.Placement.PosVec + this.Placement.PosVec };
                this._Content = value;
            }
        }

        public override void Render(IntPtr renderer, Vector2 screenDimensions, Assets assets, bool showDebugBorders)
        {
            base.Render(renderer, screenDimensions, assets, showDebugBorders);

            this.Content?.Render(renderer, screenDimensions, assets, showDebugBorders);
        }

        public override void Dispose()
        {
            base.Dispose();

            this.Content?.Dispose();
        }

        public override bool HandleTouch(TouchInputEventArgs e)
        {
            return HandleChildrenTouchHelper(e, new[] { this.Content }, base.HandleTouch,
                (e1, c1) => c1.ContainsGlobalPoint(e1.Pos));

            //bool handled = false;

            ////Try to have the content handle the touch event
            //if (this.Content != null && this.Content.Placement.ContainsPoint(e.Pos))
            //    handled = this.Content.HandleTouch(e);

            ////If the touch event wasn't handled by the content, then handle it
            //if (!handled)
            //    handled = base.HandleTouch(e);

            //return handled;
        }
    }
}
