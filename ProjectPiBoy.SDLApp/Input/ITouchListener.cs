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
        /// Handles all touch events. Delegates them to the other methods in this interface.
        /// </summary>
        /// <param name="e">The touch event</param>
        /// <returns>Whether the touch was handled or not</returns>
        bool HandleTouch(TouchInputEventArgs e);

        /// <summary>
        /// Called when a touch is hovering
        /// </summary>
        /// <param name="e">The touch event</param>
        /// <returns>Whether the touch was handled or not</returns>
        bool OnTouchHover(TouchInputEventArgs e);

        /// <summary>
        /// Called when a touch is down
        /// </summary>
        /// <param name="e">The touch event</param>
        /// <returns>Whether the touch was handled or not</returns>
        bool OnTouchDown(TouchInputEventArgs e);

        /// <summary>
        /// Called when a touch is up
        /// </summary>
        /// <param name="e">The touch event</param>
        /// <returns>Whether the touch was handled or not</returns>
        bool OnTouchUp(TouchInputEventArgs e);

        /// <summary>
        /// Called when a touch is moved
        /// </summary>
        /// <param name="e">The touch event</param>
        /// <returns>Whether the touch was handled or not</returns>
        bool OnTouchMove(TouchInputEventArgs e);
    }
}
