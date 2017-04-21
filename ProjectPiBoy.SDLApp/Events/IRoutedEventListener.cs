using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectPiBoy.SDLApp.UiObjects;

namespace ProjectPiBoy.SDLApp.Events
{
    /// <summary>
    /// Allows a <see cref="UiObject"/> to receive <see cref="RoutedEventArgs"/>
    /// </summary>
    public interface IRoutedEventListener
    {
        /// <summary>
        /// Called before the event is routed into the object. DO NOT HANDLE THE EVENT!
        /// </summary>
        /// <remarks>
        /// Useful for passively reacting to events
        /// </remarks>
        /// <param name="e">The event</param>
        void OnPreRoute(RoutedEventArgs e);

        /// <summary>
        /// Called when the <see cref="UiObject"/> can receive an event. To handle it,
        /// set its <see cref="RoutedEventArgs.Handled"/> property to true.
        /// </summary>
        /// <param name="e">The event</param>
        void OnEvent(RoutedEventArgs e);
    }
}
