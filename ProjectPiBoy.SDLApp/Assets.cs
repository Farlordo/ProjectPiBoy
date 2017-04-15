using ProjectPiBoy.Common.Utilities;
using ProjectPiBoy.SDLApp.Utilities;
using System;
using static SDL2.SDL;
using static SDL2.SDL_ttf;

namespace ProjectPiBoy.SDLApp
{
    /// <summary>
    /// Contains references to all of the assets used by the program.
    /// </summary>
    public class Assets : IDisposable
    {
        public IntPtr MainFont;

        public Theme Theme { get; private set; }

        public bool Init()
        {
            this.Theme = new Theme()
            {
                PrimaryColor = new Color(0xFF00FF00),
                SecondaryColor = new Color(0xFF002200),
                SecondaryHighlightColor = new Color(0xFF004400),
                BackgroundColor = new Color(0xFF001100),
                TextColor = new Color(0xFF44FF44),
                DisabledTextColor = new Color(0xFF88AA88),
                DebugOutlineColor = new Color(0xFFFFFF00)
            };

            if (!LoadFont("DroidSansMono", 24, out this.MainFont))
                return false;

            return true;
        }

        public void Dispose()
        {
            TTF_CloseFont(this.MainFont);
        }

        /// <summary>
        /// Loads a font.
        /// </summary>
        /// <param name="fontName">The name of the font, without the file extension. The font must be in the /Fonts/ folder.</param>
        /// <param name="pointSize">The point size of the font. Example: 12pt</param>
        /// <param name="fontPtr">A reference to the pointer to store the location of the font</param>
        /// <returns>Whether the font was successfully loaded or not</returns>
        private static bool LoadFont(string fontName, int pointSize, out IntPtr fontPtr)
        {
            string fontPath = $"{Environment.CurrentDirectory}/Fonts/{fontName}.ttf";

            fontPtr = TTF_OpenFont(fontPath, pointSize);

            if (fontPtr == IntPtr.Zero)
            {
                Console.WriteLine($"TTF_OpenFont() error: \"{SDL_GetError()}\", Font path: {fontPath}");
                return false;
            }

            return true;
        }
    }
}
