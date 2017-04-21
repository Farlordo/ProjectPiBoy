using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPiBoy.SDLApp.Input
{
    /// <summary>
    /// An interface for a touch listener
    /// </summary>
    public interface ITouchListener
    {
        /// <summary>
        /// Called when a touch is hovering
        /// </summary>
        /// <param name="e">The touch event</param>
        void OnTouchHover(TouchInputEventArgs e);

        /// <summary>
        /// Called when a touch is down
        /// </summary>
        /// <param name="e">The touch event</param>
        void OnTouchDown(TouchInputEventArgs e);

        /// <summary>
        /// Called when a touch is up
        /// </summary>
        /// <param name="e">The touch event</param>
        void OnTouchUp(TouchInputEventArgs e);

        /// <summary>
        /// Called when a touch is moved
        /// </summary>
        /// <param name="e">The touch event</param>
        void OnTouchMove(TouchInputEventArgs e);
    }
}
