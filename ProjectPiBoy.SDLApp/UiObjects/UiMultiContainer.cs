using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectPiBoy.Common.Utilities;
using System.ComponentModel;
using System.Collections.Specialized;
using ProjectPiBoy.SDLApp.Input;
using ProjectPiBoy.SDLApp.Screens;

namespace ProjectPiBoy.SDLApp.UiObjects
{
    public abstract class UiMultiContainer : UiObject
    {
        public UiMultiContainer(Screen screen, UiObjectPlacement placement) : base(screen, placement)
        {

        }

        public override Vector2 PlacementOffset
        {
            get { return base.PlacementOffset; }
            set
            {
                base.PlacementOffset = value;

                //Update the content's placement offset with this new placement offset (WARNING: This will not update if the this placement's PosVec updates! This might be considered a bug.)
                if (this.ContentList != null)
                    foreach (UiObject uiObj in this.ContentList)
                        uiObj.PlacementOffset = this.PlacementOffset + this.Placement.PosVec;
            }
        }

        private ObservableCollection<UiObject> _ContentList;
        /// <summary>The content list of this <see cref="UiMultiContainer"/></summary>
        public virtual ObservableCollection<UiObject> ContentList
        {
            get { return this._ContentList; }
            set
            {
                if (this.ContentList != null)
                    this.ContentList.CollectionChanged -= this.ContentListChanged;

                this._ContentList = value;

                //This is a new list, treat all of the items as newly added
                foreach (UiObject uiObj in this.ContentList)
                    this.OnUiObjectAdded(uiObj);

                if (this.ContentList != null)
                    this.ContentList.CollectionChanged += this.ContentListChanged;
            }
        }

        /// <summary>
        /// Event handler for when content in the content list is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ContentListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Call this.OnUiObjectAdded() for each new item
            foreach (object obj in e.NewItems)
                if (obj is UiObject)
                    this.OnUiObjectAdded((UiObject)obj);
        }

        /// <summary>
        /// Called whenever a <see cref="UiObject"/> is added to the content list
        /// </summary>
        /// <param name="uiObj">The <see cref="UiObject"/> being added to the list</param>
        protected virtual void OnUiObjectAdded(UiObject uiObj)
        {

        }

        public override void Render(IntPtr renderer, Vector2 screenDimensions, Assets assets, bool showDebugBorders)
        {
            base.Render(renderer, screenDimensions, assets, showDebugBorders);

            foreach (UiObject uiObj in this.ContentList)
                uiObj?.Render(renderer, screenDimensions, assets, showDebugBorders);
        }

        public override void Dispose()
        {
            base.Dispose();

            foreach (UiObject uiObj in this.ContentList)
                uiObj?.Dispose();
        }

        public override bool HandleTouch(TouchInputEventArgs e)
        {
            return HandleChildrenTouchHelper(e, this.ContentList, base.HandleTouch,
                (e1, c1) => c1.ContainsGlobalPoint(e1.Pos));

            //bool handled = false;

            ////Try to have the content handle the touch event
            //foreach (UiObject uiObj in this.ContentList)
            //{
            //    //Once one child handles it, we don't need to try the others
            //    if (handled)
            //        break;

            //    if (uiObj.Placement.ContainsPoint(e.Pos))
            //        handled = uiObj.HandleTouch(e);
            //}

            ////If the touch event wasn't handled by the content, then handle it
            //if (!handled)
            //    handled = base.HandleTouch(e);

            //return handled;
        }
    }
}
