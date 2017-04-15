using ProjectPiBoy.SDLApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectPiBoy.SDLApp.UiObjects;
using ProjectPiBoy.Common.Utilities;
using ProjectPiBoy.SDLApp.Input;

namespace ProjectPiBoy.SDLApp.Screens
{
    /// <summary>
    /// Represents a screen that can be displayed by the program.
    /// </summary>
    public abstract class Screen : TouchListener, IRenderable, IDisposable
    {
        public Screen()
        {
            this.UiObjects = new List<UiObject>();
        }

        protected List<UiObject> UiObjects { get; }

        public void Dispose()
        {
            foreach (UiObject uiObject in this.UiObjects)
                uiObject.Dispose();
        }

        public virtual void Render(IntPtr renderer, Vector2 screenDimensions, Assets assets, bool showDebugBorders)
        {
            foreach (UiObject uiObject in this.UiObjects)
                uiObject.Render(renderer, screenDimensions, assets, showDebugBorders);
        }

        public override bool HandleTouch(TouchInputEventArgs e)
        {
            return HandleChildrenTouchHelper(e, this.UiObjects, base.HandleTouch,
                (e1, c1) => c1.ContainsGlobalPoint(e1.Pos));

            //bool handled = false;

            ////Delegate the touch event to the UI objects that the touch is within
            //foreach (UiObject uiObject in this.UiObjects)
            //{
            //    //Once one child handles it, we don't need to try the others
            //    if (handled)
            //        break;

            //    if (uiObject.Placement.ContainsPoint(e.Pos))
            //        handled = uiObject.HandleTouch(e);
            //}

            ////If the touch event wasn't handled by the content, then handle it
            //if (!handled)
            //    handled = base.HandleTouch(e);

            //return handled;
        }

        //public override void OnTouchHover(TouchInputEventArgs e)
        //{
        //    base.OnTouchHover(e);
        //}

        //public override void OnTouchDown(TouchInputEventArgs e)
        //{
        //    base.OnTouchDown(e);
        //}

        //public override void OnTouchUp(TouchInputEventArgs e)
        //{
        //    base.OnTouchUp(e);
        //}

        //public override void OnTouchMove(TouchInputEventArgs e)
        //{
        //    base.OnTouchMove(e);
        //}
    }
}
