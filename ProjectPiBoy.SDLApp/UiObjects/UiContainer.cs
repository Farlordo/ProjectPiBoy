using ProjectPiBoy.Common.Utilities;
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
        public UiContainer(UiObjectPlacement placement) : base(placement)
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
    }
}
