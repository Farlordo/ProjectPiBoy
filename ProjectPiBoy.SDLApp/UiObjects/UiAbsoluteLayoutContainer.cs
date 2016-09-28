using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPiBoy.SDLApp.UiObjects
{
    public class UiAbsoluteLayoutContainer : UiMultiContainer
    {
        public UiAbsoluteLayoutContainer(UiObjectPlacement placement = new UiObjectPlacement()) : base(placement)
        {

        }

        protected override void OnUiObjectAdded(UiObject uiObj)
        {
            base.OnUiObjectAdded(uiObj);

            //Set the content's placement relative to this object's placement
            uiObj.PlacementOffset = this.PlacementOffset + this.Placement.PosVec;
        }
    }
}
