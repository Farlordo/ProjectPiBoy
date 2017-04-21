using ProjectPiBoy.Common.Utilities;
using ProjectPiBoy.SDLApp.UiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPiBoy.SDLApp.Events
{
    /// <summary>
    /// Base class for events that are routed throughout the visual tree
    /// </summary>
    /// <remarks>
    /// Based on <see cref="System.Windows.RoutedEventArgs"/>
    /// </remarks>
    public class RoutedEventArgs : EventArgs
    {
        private bool _Handled;
        /// <summary>Whether the event has been handled or not</summary>
        public bool Handled
        {
            get => this._Handled;
            set
            {
                if (this.Handled)
                    throw new InvalidOperationException("Event has already been handled!");
                else
                    this._Handled = value;
            }
        }

        /// <summary>The lowest <see cref="UiObject"/> that the event hit</summary>
        public UiObject LowestHit { get; set; }

        /// <summary>The screen position of the routed event</summary>
        /// <remarks>This has a setter so coordinates can be adjusted</remarks>
        public Vector2 Pos { get; set; }
    }
}
