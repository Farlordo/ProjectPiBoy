using ProjectPiBoy.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPiBoy.SDLApp.Utilities
{
    /// <summary>
    /// An interface for things that need to be rendered.
    /// </summary>
    public interface IRenderable
    {
        /// <summary>
        /// Renders the object.
        /// </summary>
        /// <param name="renderer">A pointer to the SDL Renderer</param>
        /// <param name="screenDimensions">The dimensions of the screen</param>
        /// <param name="assets">The assets to use</param>
        /// <param name="showDebugBorders">Whether to render debug borders or not</param>
        void Render(IntPtr renderer, Vector2 screenDimensions, Assets assets, bool showDebugBorders);
    }
}
