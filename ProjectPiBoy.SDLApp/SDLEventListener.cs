using ProjectPiBoy.SDLApp.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.SDL;

namespace ProjectPiBoy.SDLApp
{
    public class SDLEventListener
    {
        /// <summary>
        /// Called whenever an <see cref="SDL_Event"/> of any kind occurs.
        /// </summary>
        /// <param name="e">The <see cref="SDL_Event"/></param>
        public void OnSDLEvent(SDL_Event e)
        {
            this.SDLEvent?.Invoke(this, e);

            switch (e.type)
            {
                case SDL_EventType.SDL_QUIT:
                    this.QuitRequested?.Invoke(this, e);
                    break;

                case SDL_EventType.SDL_FINGERDOWN:
                case SDL_EventType.SDL_FINGERUP:
                case SDL_EventType.SDL_FINGERMOTION:
                    this.TouchEvent?.Invoke(this, TouchInputEventArgs.FromTouch(e.tfinger));
                    break;

                case SDL_EventType.SDL_MOUSEBUTTONDOWN:
                case SDL_EventType.SDL_MOUSEBUTTONUP:
                    this.TouchEvent?.Invoke(this, TouchInputEventArgs.FromMouseInput(e.button));
                    break;

                case SDL_EventType.SDL_MOUSEMOTION:
                    this.TouchEvent?.Invoke(this, TouchInputEventArgs.FromMouseMotion(e.motion));
                    break;

                //TODO: More events
            }
        }

        /// <summary>An event that gets fired for every SDL event</summary>
        public event EventHandler<SDL_Event> SDLEvent;

        /// <summary>An event that gets fired when the application is requested to quit</summary>
        public event EventHandler<SDL_Event> QuitRequested;

        /// <summary>An event that gets fired by touch or mouse input</summary>
        public event EventHandler<TouchInputEventArgs> TouchEvent;
    }
}
