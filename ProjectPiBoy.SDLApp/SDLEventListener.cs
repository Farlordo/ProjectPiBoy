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

                //TODO: More events
            }
        }

        /// <summary>An event that gets fired for every SDL event</summary>
        public event EventHandler<SDL_Event> SDLEvent;

        /// <summary>An event that gets fired when the application is requested to quit</summary>
        public event EventHandler<SDL_Event> QuitRequested;
    }
}
