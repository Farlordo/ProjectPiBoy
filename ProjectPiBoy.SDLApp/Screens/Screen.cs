using ProjectPiBoy.SDLApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectPiBoy.SDLApp.UiObjects;
using ProjectPiBoy.Common.Utilities;

namespace ProjectPiBoy.SDLApp.Screens
{
    /// <summary>
    /// Represents a screen that can be displayed by the program.
    /// </summary>
    public abstract class Screen : IRenderable, IDisposable
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
    }
}
