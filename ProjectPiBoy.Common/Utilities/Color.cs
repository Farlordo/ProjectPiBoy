
namespace ProjectPiBoy.Common.Utilities
{
    /// <summary>
    /// Represents an RGB color, with an additional alpha channel.
    /// </summary>
    /// <remarks>
    /// Based on Color in Dalton's ShockLib Minecraft mod.
    /// </remarks>
    public struct Color
    {
        private float _Red;
        /// <summary>The red component of the color, 0.0 - 1.0</summary>
        public float Red
        {
            get { return this._Red; }
            set
            {
                this._Red = MathUtil.Clamp(value, 0, 1);
                this.Red8Bit = (byte)(this._Red * 255);
            }
        }

        private float _Green;
        /// <summary>The green component of the color, 0.0 - 1.0</summary>
        public float Green
        {
            get { return this._Green; }
            set
            {
                this._Green = MathUtil.Clamp(value, 0, 1);
                this.Green8Bit = (byte)(this._Green * 255);
            }
        }

        private float _Blue;
        /// <summary>The blue component of the color, 0.0 - 1.0</summary>
        public float Blue
        {
            get { return this._Blue; }
            set
            {
                this._Blue = MathUtil.Clamp(value, 0, 1);
                this.Blue8Bit = (byte)(this._Blue * 255);
            }
        }

        private float _Alpha;
        /// <summary>The alpha component of the color, 0.0 - 1.0</summary>
        public float Alpha
        {
            get { return this._Alpha; }
            set
            {
                this._Alpha = MathUtil.Clamp(value, 0, 1);
                this.Alpha8Bit = (byte)(this._Alpha * 255);
            }
        }

        /// <summary>The read only 8 bit red component of the color, 0 - 255</summary>
        public byte Red8Bit { get; private set; }

        /// <summary>The read only 8 bit green component of the color, 0 - 255</summary>
        public byte Green8Bit { get; private set; }

        /// <summary>The read only 8 bit blue component of the color, 0 - 255</summary>
        public byte Blue8Bit { get; private set; }

        /// <summary>The read only 8 bit alpha component of the color, 0 - 255</summary>
        public byte Alpha8Bit { get; private set; }

        /// <summary>
        /// Creates a new color with the specified components.
        /// </summary>
        /// <param name="red">The red component of the color, 0.0 - 1.0</param>
        /// <param name="green">The green component of the color, 0.0 - 1.0</param>
        /// <param name="blue">The blue component of the color, 0.0 - 1.0</param>
        /// <param name="alpha">The alpha component of the color, 0.0 - 1.0</param>
        public Color(float red, float green, float blue, float alpha = 1F) : this()
        {
            this.Red = MathUtil.Clamp(red, 0, 1);
            this.Green = MathUtil.Clamp(green, 0, 1);
            this.Blue = MathUtil.Clamp(blue, 0, 1);
            this.Alpha = MathUtil.Clamp(alpha, 0, 1);
        }

        /// <summary>
        /// Creates a new color with the same values as another color.
        /// </summary>
        /// <param name="other">The other color</param>
        public Color(Color other) : this(other.Red, other.Green, other.Blue, other.Alpha) { }

        /// <summary>
        /// Creates a new color from an ARGB hexadecimal code.
        /// </summary>
        /// <param name="argb">The ARGB hexadecimal code</param>
        public Color(uint argb) : this()
        {
            this.SetARGB(argb);
        }

        /// <summary>
        /// Sets this color's values to another color's values.
        /// </summary>
        /// <param name="other">The other color</param>
        public void Set(Color other)
        {
            this.Red = other.Red;
            this.Green = other.Green;
            this.Blue = other.Blue;
            this.Alpha = other.Alpha;
        }

        /// <summary>
        /// Calculates and returns the ARGB hexadecimal representation of this color.
        /// </summary>
        /// <returns>The ARGB hexadecimal representation of this color</returns>
        public uint GetARGB()
        {
            uint hex = 0;

            //Add each color component to the hex
            hex |= (uint)(this.Alpha8Bit << 24);
            hex |= (uint)(this.Red8Bit   << 16);
            hex |= (uint)(this.Green8Bit << 8);
            hex |= (uint)(this.Blue8Bit  << 0);

            return hex;
        }

        /// <summary>
        /// Sets the color's components based on an ARGB hexadecimal representation.
        /// </summary>
        /// <param name="hex">The ARGB hexadecimal representation of the color</param>
        public void SetARGB(uint hex)
        {
            this.Alpha = ((hex >> 24) & 0xFF) / 255F;
            this.Red   = ((hex >> 16) & 0xFF) / 255F;
            this.Green = ((hex >> 8 ) & 0xFF) / 255F;
            this.Blue  = ((hex >> 0 ) & 0xFF) / 255F;
        }

        public override string ToString() => $"Color(A: {this.Alpha}, R: {this.Red}, G: {this.Green}, B: {this.Blue}, ARGB: {this.GetARGB()})";

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Color))
                return false;

            Color other = (Color)obj;

            return this.Red == other.Red
                && this.Green == other.Green
                && this.Blue == other.Blue
                && this.Alpha == other.Alpha;
        }

        public override int GetHashCode()
        {
            unchecked //Don't care about overflow, it just wraps
            {
                int hash = 17;

                hash = hash * 23 + this.Red.GetHashCode();
                hash = hash * 23 + this.Green.GetHashCode();
                hash = hash * 23 + this.Blue.GetHashCode();
                hash = hash * 23 + this.Alpha.GetHashCode();

                return hash;
            }
        }
    }
}
