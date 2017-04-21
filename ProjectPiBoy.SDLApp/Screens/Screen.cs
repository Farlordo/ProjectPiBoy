using ProjectPiBoy.SDLApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectPiBoy.SDLApp.UiObjects;
using ProjectPiBoy.Common.Utilities;
using ProjectPiBoy.SDLApp.Input;
using System.Collections;
using ProjectPiBoy.SDLApp.Events;

namespace ProjectPiBoy.SDLApp.Screens
{
    /// <summary>
    /// Represents a screen that can be displayed by the program.
    /// </summary>
    public abstract class Screen : TouchListener, IRenderable, IDisposable, IEnumerable<UiObject>, IRoutedEventListener
    {
        public Screen()
        {
            this.UiObjects = new List<UiObject>();
            this.Touches = new Dictionary<long, Vector2>();
        }

        /// <summary>A list of <see cref="UiObject"/>s on the screen</summary>
        protected List<UiObject> UiObjects { get; }

        /// <summary>A list of touch points currently active on the screen, indexed by finger ID</summary>
        public Dictionary<long, Vector2> Touches { get; }

        public void Dispose()
        {
            foreach (UiObject uiObject in this)
                uiObject.Dispose();
        }

        public virtual void Render(IntPtr renderer, Vector2 screenDimensions, Assets assets, bool showDebugBorders)
        {
            foreach (UiObject uiObject in this)
                uiObject.Render(renderer, screenDimensions, assets, showDebugBorders);
        }

        public IEnumerator<UiObject> GetEnumerator() => this.UiObjects.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.UiObjects.GetEnumerator();

        public override void OnEvent(RoutedEventArgs e)
        {
            base.OnEvent(e);

            //This can be overriden to handle events, but make sure to call base!
        }

        public override void OnPreRoute(RoutedEventArgs e)
        {
            base.OnPreRoute(e);

            //If the event is a touch event, update the touches dictionary
            if (e is TouchInputEventArgs te)
                this.Touches[te.FingerID] = te.Pos;
        }
    }
}
