using ProjectPiBoy.Common.Utilities;
using ProjectPiBoy.SDLApp.UiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.SDL;

namespace ProjectPiBoy.SDLApp.Utilities
{
    /// <summary>
    /// A class that contains utility functions for working with screen space coordinates.
    /// </summary>
    public static class ScreenSpaceUtil
    {
        /// <summary>
        /// Converts from percentage coordinates to global coordinates.
        /// </summary>
        /// <param name="percentageCoordinates">The percentage coordinates</param>
        /// <param name="diminsions">The dimensions of the object that the coordinates are in</param>
        /// <returns>The global coordinates</returns>
        public static Vector2 PercentageToGlobal(Vector2 percentageCoordinates, Vector2 diminsions) => percentageCoordinates * diminsions;

        /// <summary>
        /// Converts from global coordinates to percentage coordinates.
        /// </summary>
        /// <param name="globalCoordinates">The global coordinates</param>
        /// <param name="dimensions">The dimensions of the object that the coordinates are in</param>
        /// <returns>The percentage coordinates</returns>
        public static Vector2 GlobalToPercentage(Vector2 globalCoordinates, Vector2 dimensions) => globalCoordinates / dimensions;

        /// <summary>
        /// Gets an <see cref="SDL_Rect"/> representing the <see cref="UiObjectPlacement"/>'s bounds in global screen coordinates.
        /// </summary>
        /// <param name="uiObjectPlacement">The <see cref="UiObjectPlacement"/></param>
        /// <param name="screenDimensions">A <see cref="Vector2"/> containing the screen dimensions</param>
        /// <returns>a <see cref="SDL_Rect"/> representing the <see cref="UiObjectPlacement"/>'s bounds</returns>
        public static SDL_Rect GetGlobalBorderRectangle(UiObjectPlacement uiObjectPlacement, Vector2 screenDimensions)
        {
            Vector2 topLeft = PercentageToGlobal(uiObjectPlacement.TopLeft, screenDimensions);
            Vector2 bottomRight = PercentageToGlobal(uiObjectPlacement.BottomRight, screenDimensions);

            return new SDL_Rect()
            {
                x = (int) (topLeft.X),
                y = (int) (topLeft.Y),
                w = (int) (bottomRight.X - topLeft.X),
                h = (int) (bottomRight.Y - topLeft.Y)
            };
        }
    }
}
