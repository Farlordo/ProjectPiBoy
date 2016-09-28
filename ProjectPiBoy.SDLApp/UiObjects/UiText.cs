using ProjectPiBoy.Common.Utilities;
using ProjectPiBoy.SDLApp.Extensions.ColorExtensions;
using ProjectPiBoy.SDLApp.Utilities;
using System;
using static SDL2.SDL;
using static SDL2.SDL_ttf;

namespace ProjectPiBoy.SDLApp.UiObjects
{
    /// <summary>
    /// A <see cref="UiObject"/> that displays text.
    /// </summary>
    public class UiText : UiObject
    {
        /// <summary>A pointer to the texture that the text will be / is rendered to.</summary>
        private IntPtr Texture;

        /// <summary>Whether the texture has been allocated or not.</summary>
        private bool TextureAllocated;

        public UiText() : base()
        {

        }

        private string _Text;
        /// <summary>The text to display</summary>
        public string Text
        {
            get { return this._Text; }
            set
            {
                this._Text = value;
                //TODO: Resize?
                this.TextNeedsRerendered = true;
            }
        }

        private Color? _Color;
        /// <summary>The color of the text</summary>
        public Color? Color
        {
            get { return this._Color; }
            set
            {
                this._Color = value;
                this.TextNeedsRerendered = true;
            }
        }


        private bool TextNeedsRerendered { get; set; }

        public void RerenderTexture(IntPtr renderer, Assets assets)
        {
            //Clean up the old texture
            this.DisposeTexture();

            if (!string.IsNullOrWhiteSpace(this.Text))
            {
                //Create a new surface with the text drawn on it
                IntPtr surface = TTF_RenderText_Solid(assets.MainFont, this.Text, this.Color?.ToSDLColor() ?? assets.Theme.TextColor.ToSDLColor());

                if (surface == IntPtr.Zero)
                    throw new ApplicationException($"TTF_RenderText_Solid() error: \"{SDL_GetError()}\"");

                //Create a texture out of the surface
                this.Texture = SDL_CreateTextureFromSurface(renderer, surface);

                //Deallocate the surface, we're done with it
                SDL_FreeSurface(surface);

                if (this.Texture == IntPtr.Zero)
                    throw new ApplicationException($"SDL_CreateTextureFromSurface() error: \"{SDL_GetError()}\"");

                //Keep track of this
                this.TextureAllocated = true;
            }

            //It just got rerendered, or doesn't need to be
            this.TextNeedsRerendered = false;
        }

        public override void Render(IntPtr renderer, Vector2 screenDimensions, Assets assets, bool showDebugBorders)
        {
            base.Render(renderer, screenDimensions, assets, showDebugBorders);

            if (this.TextNeedsRerendered)
                this.RerenderTexture(renderer, assets);

            //Only render the text if the texture is allocated
            if (this.TextureAllocated)
            {
                //Query the text width and height in pixels, so we can center it. TODO: Should this only be calculated when the text is rerendered?
                int textWidth, textHeight;
                TTF_SizeText(assets.MainFont, this.Text, out textWidth, out textHeight);

                //Calculate the global position of the center of the texture
                Vector2 globalPos = ScreenSpaceUtil.PercentageToGlobal(this.GetGlobalPlacement().PosVec, screenDimensions);
                SDL_Rect textureRect = new SDL_Rect()
                {
                    x = (int)globalPos.X - textWidth / 2,
                    y = (int)globalPos.Y - textHeight / 2
                };

                //Unused, these only exist so the out parameters can be satisfied
                uint format;
                int access;

                //Query the texture dimensions into the rectangle
                SDL_QueryTexture(this.Texture, out format, out access, out textureRect.w, out textureRect.h);

                //Render the texture
                SDL_RenderCopy(renderer, this.Texture, IntPtr.Zero, ref textureRect);
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            this.DisposeTexture();
        }

        /// <summary>
        /// Disposes the texture and surface, if they have been allocated.
        /// </summary>
        private void DisposeTexture()
        {
            if (this.TextureAllocated)
            {
                SDL_DestroyTexture(this.Texture);
                this.TextureAllocated = false;
            }
        }
    }
}
