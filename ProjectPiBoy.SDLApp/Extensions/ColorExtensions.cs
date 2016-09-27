using ProjectPiBoy.Common.Utilities;
using static SDL2.SDL;

namespace ProjectPiBoy.SDLApp.Extensions.ColorExtensions
{
    public static class ColorExtensions
    {
        /// <summary>
        /// An extension method that converts a <see cref="Color"/> to an <see cref="SDL_Color"/>.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> to convert</param>
        /// <returns>The new <see cref="SDL_Color"/></returns>
        public static SDL_Color ToSDLColor(this Color color) => new SDL_Color()
        {
            r = color.Red8Bit,
            g = color.Green8Bit,
            b = color.Blue8Bit,
            a = color.Alpha8Bit
        };
    }
}
