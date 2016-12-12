using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPiBoy.SDLApp.Input
{
    /// <summary>
    /// Represents a touch type for <see cref="TouchInputEventArgs"/> objects
    /// </summary>
    public enum EnumTouchType
    {
        /// <summary>The touch just moved</summary>
        Motion = 0,
        /// <summary>The touch was just a hover</summary>
        Hover = 1,
        /// <summary>The touch just started</summary>
        Down = 2,
        /// <summary>The touch just ended</summary>
        Up = 3
    }
}
