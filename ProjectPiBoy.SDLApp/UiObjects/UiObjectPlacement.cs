using ProjectPiBoy.Common.Utilities;

namespace ProjectPiBoy.SDLApp.UiObjects
{
    /// <summary>
    /// Represents a <see cref="UiObject"/>'s position and size.
    /// </summary>
    public struct UiObjectPlacement
    {
        public UiObjectPlacement(float xPos, float yPos, float width, float height, int depth)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.Width = width;
            this.Height = height;
            this.Depth = depth;
        }

        public UiObjectPlacement(UiObjectPlacement other) : this(other.XPos, other.YPos, other.Width, other.Height, other.Depth) { }

        /// <summary>The X position of the center of the UiObject, in a percentage of the screen. 0.0 - 1.0</summary>
        public float XPos { get; set; }

        /// <summary>The Y position of the center of the UiObject, in a percentage of the screen. 0.0 - 1.0</summary>
        public float YPos { get; set; }

        /// <summary>The width of the UiObject, in a percentage of the screen. 0.0 - 1.0</summary>
        public float Width { get; set; }

        /// <summary>The height of the UiObject, in a percentage of the screen. 0.0 - 1.0</summary>
        public float Height { get; set; }

        /// <summary>The depth of the UiObject. Larger numbers = greater depth</summary>
        public int Depth { get; }

        /// <summary>A <see cref="Vector2"/> representation of the UiObject's position.</summary>
        public Vector2 PosVec
        {
            get => new Vector2(this.XPos, this.YPos);
            set
            {
                this.XPos = (float) value.X;
                this.YPos = (float) value.Y;
            }
        }

        /// <summary>Calculates and returns the left edge of the UiObject, in a percentage of the screen. 0.0 - 1.0</summary>
        public float LeftEdge => this.XPos - (this.Width / 2);

        /// <summary>Calculates and returns the right edge of the UiObject, in a percentage of the screen. 0.0 - 1.0</summary>
        public float RightEdge => this.XPos + (this.Width / 2);

        /// <summary>Calculates and returns the top edge of the UiObject, in a percentage of the screen. 0.0 - 1.0</summary>
        public float TopEdge => this.YPos - (this.Height / 2);

        /// <summary>Calculates and returns the bottom edge of the UiObject, in a percentage of the screen. 0.0 - 1.0</summary>
        public float BottomEdge => this.YPos + (this.Height / 2);

        /// <summary>Gets the top right corner of the UiObject as a <see cref="Vector2"/>.</summary>
        public Vector2 TopLeft => new Vector2(this.LeftEdge, this.TopEdge);

        /// <summary>Gets the bottom right corner of the UiObject as a <see cref="Vector2"/>.</summary>
        public Vector2 BottomRight => new Vector2(this.RightEdge, this.BottomEdge);

        /// <summary>
        /// Determines whether the specified point is contained within the UiObject's bounds
        /// </summary>
        /// <param name="x">The X position of the point, in a percentage of the screen. 0.0 - 1.0</param>
        /// <param name="y">The Y position of the point, in a percentage of the screen. 0.0 - 1.0</param>
        /// <returns></returns>
        public bool ContainsPoint(float x, float y) =>
            x >= this.LeftEdge && x <= this.RightEdge &&
            y >= this.TopEdge && y <= this.BottomEdge;

        /// <summary>
        /// Determines whether the specified point is contained within the UiObject's bounds
        /// </summary>
        /// <param name="point">The point, with coordinates as a percentage of the screen. 0.0 - 1.0</param>
        /// <returns></returns>
        public bool ContainsPoint(Vector2 point) => this.ContainsPoint((float)point.X, (float)point.Y);

        public override string ToString() => $"{nameof(UiObjectPlacement)}({nameof(this.XPos)}: {this.XPos}, {nameof(this.YPos)}: {this.YPos}, {nameof(this.Width)}: {this.Width}, {nameof(this.Height)}: {this.Height}, {nameof(this.Depth)}: {this.Depth})";
    }
}
