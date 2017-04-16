using ProjectPiBoy.Common.Utilities;
using ProjectPiBoy.SDLApp.Extensions.ColorExtensions;
using ProjectPiBoy.SDLApp.Screens;
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

        public UiText(Screen screen) : base(screen)
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

        /// <summary>The width of the text, in pixels</summary>
        public int TextWidth { get; protected set; }

        /// <summary>The height of the text, in pixels</summary>
        public int TextHeight { get; protected set; }

        private bool TextNeedsRerendered { get; set; }

        public void RerenderTexture(IntPtr renderer, Assets assets, Vector2 screenDimensions)
        {
            //Clean up the old texture
            this.DisposeTexture();

            int textWidth = 0;
            int textHeight = 0;

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

                //Query the text width and height in pixels
                TTF_SizeText(assets.MainFont, this.Text, out textWidth, out textHeight);
            }

            //Update the cached text dimensions
            this.TextWidth = textWidth;
            this.TextHeight = textHeight;

            UiObjectPlacement placement = this.Placement;

            //Convert the text dimensions to screen percentage
            Vector2 textDimensions = new Vector2(textWidth, textHeight);
            Vector2 textDimensionsPercent = ScreenSpaceUtil.GlobalToPercentage(textDimensions, screenDimensions);

            //Update the placement's width and height
            placement.Width = (float)textDimensionsPercent.X;
            placement.Height = (float)textDimensionsPercent.Y;
            this.Placement = placement;

            //It just got rerendered, or doesn't need to be
            this.TextNeedsRerendered = false;
        }

        public override void Render(IntPtr renderer, Vector2 screenDimensions, Assets assets, bool showDebugBorders)
        {
            base.Render(renderer, screenDimensions, assets, showDebugBorders);

            if (this.TextNeedsRerendered)
                this.RerenderTexture(renderer, assets, screenDimensions);

            //Only render the text if the texture is allocated
            if (this.TextureAllocated)
            {
                //Calculate the global position of the center of the texture
                Vector2 globalPos = ScreenSpaceUtil.PercentageToGlobal(this.GlobalPlacement.PosVec, screenDimensions);

                //Create the texture rectangle
                SDL_Rect textureRect = new SDL_Rect()
                {
                    x = (int)globalPos.X - this.TextWidth / 2,
                    y = (int)globalPos.Y - this.TextHeight / 2
                };

                //Query the texture dimensions into the rectangle
                SDL_QueryTexture(this.Texture, out uint format, out int access, out textureRect.w, out textureRect.h);

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
