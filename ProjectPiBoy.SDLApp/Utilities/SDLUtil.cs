using System;
using ProjectPiBoy.Common.Utilities;
using static SDL2.SDL;

namespace ProjectPiBoy.SDLApp.Utilities
{
    public static class SDLUtil
    {
        /// <summary>
        /// Sets the SDL render draw color from a <see cref="Color"/>.
        /// </summary>
        /// <param name="renderer">The renderer to set the color for</param>
        /// <param name="color">The color to set</param>
        public static void SetSDLRenderDrawColor(IntPtr renderer, Color color) =>
            SDL_SetRenderDrawColor(renderer, color.Red8Bit, color.Green8Bit, color.Blue8Bit, color.Alpha8Bit);
    }
}
