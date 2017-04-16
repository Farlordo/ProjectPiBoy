using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectPiBoy.Common.Utilities;
using ProjectPiBoy.SDLApp.Screens;

namespace ProjectPiBoy.SDLApp.UiObjects
{
    [Obsolete(message: "Functionality moved to UiObject", error: true)]
    public class UiAbsoluteLayoutContainer : UiMultiContainer
    {
        public UiAbsoluteLayoutContainer(Screen screen, UiObjectPlacement placement = new UiObjectPlacement()) : base(screen, placement)
        {

        }

        protected override void OnUiObjectAdded(UiObject uiObj)
        {
            base.OnUiObjectAdded(uiObj);

            //Set the content's placement relative to this object's placement
            uiObj.PlacementOffset = this.PlacementOffset + this.Placement.PosVec;
        }

        public override bool ContainsGlobalPoint(Vector2 point)
        {
            return true; //This container does not have bounds
        }
    }
}
