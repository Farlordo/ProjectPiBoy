using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPiBoy.Common.Utilities
{
    /// <summary>
    /// Represents a color theme for the program.
    /// </summary>
    public class Theme
    {
        public Color PrimaryColor { get; set; }

        public Color SecondaryColor { get; set; }

        public Color BackgroundColor { get; set; }

        public Color TextColor { get; set; }

        public Color DisabledTextColor { get; set; }

        public Color DebugOutlineColor { get; set; }
    }
}
