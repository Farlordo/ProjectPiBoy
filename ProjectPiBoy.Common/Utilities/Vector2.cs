using System;

namespace ProjectPiBoy.Common.Utilities
{
    /// <summary>
    /// Represents a two dimensional vector
    /// </summary>
    /// <remarks>
    /// Based on Vector3
    /// </remarks>
    public struct Vector2 : IFormattable
    {
        /// <summary>The X component of the vector</summary>
        public double X { get; set; }

        /// <summary>The Y component of the vector</summary>
        public double Y { get; set; }

        //Static vectors, DO NOT MODIFY
        public static readonly Vector2 One   = new Vector2(1, 1);
        public static readonly Vector2 UnitX = new Vector2(1, 0);
        public static readonly Vector2 UnitY = new Vector2(0, 1);
        public static readonly Vector2 Zero  = new Vector2(0, 0);

        /// <summary>
        /// Constructs a new <see cref="Vector2"/>
        /// </summary>
        /// <param name="x">The X component of this vector</param>
        /// <param name="y">The Y component of this vector</param>
        public Vector2(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Constructs a new <see cref="Vector2"/> based on another <see cref="Vector2"/>
        /// </summary>
        /// <param name="other"></param>
        public Vector2(Vector2 other) : this(other.X, other.Y) { }

        /// <summary>
        /// Sets the values of this vector
        /// </summary>
        /// <param name="x">The X component of this vector</param>
        /// <param name="y">The Y component of this vector</param>
        /// <returns>This vector for chaining</returns>
        public Vector2 Set(double x, double y)
        {
            this.X = x;
            this.Y = y;
            return this;
        }

        /// <summary>
        /// Sets the components of this vector to the components of another vector
        /// </summary>
        /// <param name="other">The other vector</param>
        /// <returns>This vector for chaining</returns>
        public Vector2 Set(Vector2 other) => this.Set(other.X, other.Y);

        /// <summary>
        /// Adds two vectors together
        /// </summary>
        /// <param name="vec1">The first vector to add</param>
        /// <param name="vec2">The second vector to add</param>
        /// <returns>A new vector, the addition of the other vectors</returns>
        public static Vector2 operator +(Vector2 vec1, Vector2 vec2) => new Vector2(vec1.X + vec2.X, vec1.Y + vec2.Y);

        /// <summary>
        /// Subtracts a vector from another vector
        /// </summary>
        /// <param name="vec1">The first vector</param>
        /// <param name="vec2">The vector to subtract</param>
        /// <returns>A new vector, the difference of the other vectors</returns>
        public static Vector2 operator -(Vector2 vec1, Vector2 vec2) => new Vector2(vec1.X - vec2.X, vec1.Y - vec2.Y);

        /// <summary>
        /// Checks if two vectors are equal
        /// </summary>
        /// <param name="vec1">The first vector</param>
        /// <param name="vec2">The second vector</param>
        /// <returns>Whether the vectors are equal</returns>
        public static bool operator ==(Vector2 vec1, Vector2 vec2) => vec1.X == vec2.X && vec1.Y == vec2.Y;

        /// <summary>
        /// Checks if two vectors are not equal
        /// </summary>
        /// <param name="vec1">The first vector</param>
        /// <param name="vec2">The second vector</param>
        /// <returns>Whether the vectors are not equal</returns>
        public static bool operator !=(Vector2 vec1, Vector2 vec2) => !(vec1 == vec2);

        /// <summary>
        /// Scales the vector with a scalar value
        /// </summary>
        /// <param name="vec">The vector to scale</param>
        /// <param name="scalar">The scalar value</param>
        /// <returns>A new scaled vector</returns>
        public static Vector2 operator *(Vector2 vec, double scalar) => new Vector2(vec.X * scalar, vec.Y * scalar);

        /// <summary>
        /// Multiplies the two vectors together, component wise.
        /// </summary>
        /// <param name="vec1">The first vector</param>
        /// <param name="vec2">The second vector</param>
        /// <returns>The result of the multiplication</returns>
        public static Vector2 operator *(Vector2 vec1, Vector2 vec2) => new Vector2(vec1.X * vec2.X, vec1.Y * vec2.Y);

        /// <summary>
        /// Divides the left vector by the right, component wise.
        /// </summary>
        /// <param name="vec1">The first vector</param>
        /// <param name="vec2">The second vector</param>
        /// <returns>The result of the division</returns>
        public static Vector2 operator /(Vector2 vec1, Vector2 vec2) => new Vector2(vec1.X / vec2.X, vec1.Y / vec2.Y);

        /// <summary>
        /// Checks if this vector equals the specified object
        /// </summary>
        /// <param name="obj">The object</param>
        /// <returns>Whether this vector equals the specified object</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Vector2))
                return false;

            Vector2 other = (Vector2)obj;

            return this == other;
        }

        /// <summary>
        /// Computes the hash code for this vector
        /// </summary>
        /// <returns>The hash code for this vector</returns>
        public override int GetHashCode()
        {
            unchecked //Don't care about overflow, it just wraps
            {
                int hash = 17;

                hash = hash * 23 + this.X.GetHashCode();
                hash = hash * 23 + this.Y.GetHashCode();

                return hash;
            }
        }

        /// <summary>
        /// Computes the length of the vector
        /// </summary>
        /// <returns>The length of the vector</returns>
        public double Length() => Math.Sqrt(X * X + Y * Y);

        /// <summary>
        /// Computes the squared length of the vector. Useful for comparing the length of vectors while avoiding the square root computation.
        /// </summary>
        /// <returns>The squared length of the vector</returns>
        public double LengthSquared() => X * X + Y * Y;

        /// <summary>
        /// Creates a string representation of the vector
        /// </summary>
        /// <returns>A string representation of the vector</returns>
        public override string ToString() => $"(X:{this.X}, Y:{this.Y})";

        /// <summary>
        /// Creates a formatted string representation of the vector
        /// </summary>
        /// <param name="format">The string format</param>
        /// <param name="formatProvider">The format provider</param>
        /// <returns>A formatted string representation of this vector</returns>
        public string ToString(string format, IFormatProvider formatProvider) =>
            $"(X:{this.X.ToString(format, formatProvider)}, Y:{this.Y.ToString(format, formatProvider)})";

        /// <summary>
        /// Creates a new copy of this vector
        /// </summary>
        /// <returns>A new copy of this vector</returns>
        public Vector2 Copy() => new Vector2(this);

        /// <summary>
        /// Calculates the distance from this vector to another vector
        /// </summary>
        /// <param name="other">The other vector</param>
        /// <returns>The distance from this vector to another vector</returns>
        public double DistanceTo(Vector2 other)
        {
            double xDiff = other.X - this.X;
            double yDiff = other.Y - this.Y;

            return Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
        }

        /// <summary>
        /// Calculates the squared distance from this vector to another vector Useful for comparing distances while avoiding the square root computation.
        /// </summary>
        /// <param name="other">The other vector</param>
        /// <returns>The distance from this vector to another vector</returns>
        public double DistanceToSquared(Vector2 other)
        {
            double xDiff = other.X - this.X;
            double yDiff = other.Y - this.Y;

            return xDiff * xDiff + yDiff * yDiff;
        }

        /// <summary>
        /// Creates a normalized copy of this vector so that its length is approximately 1
        /// </summary>
        /// <returns>A new, normalized vector</returns>
        public Vector2 Normalize()
        {
            double length2 = this.LengthSquared();

            if (length2 == 0D || length2 == 1D) //If this is already or can't be normalized, return a copy
                return new Vector2(this);
            else //Scale the vector and return it
                return this * (1D / Math.Sqrt(length2));
        }

        /// <summary>
        /// Computes the dot product of this vector and another vector
        /// </summary>
        /// <param name="other">The other vector</param>
        /// <returns>The dot product of the two vectors</returns>
        public double DotProduct(Vector2 other) => this.X * other.X + this.Y * other.Y;

        /// <summary>
        /// Returns whether this is a unit vector, with the optionally specified error margin
        /// </summary>
        /// <param name="margin">The error margin for the squared length</param>
        /// <returns>Whether this is a unit vector</returns>
        public bool IsUnitVector(double margin = 0.000000001D) => Math.Abs(this.LengthSquared() - 1D) < margin;

        /// <summary>
        /// Returns whether this is the zero vector
        /// </summary>
        /// <returns>Whether this is the zero vector</returns>
        public bool IsZero() => this == Zero;

        /// <summary>
        /// Returns whether this is the zero vector, with the specified error margin
        /// </summary>
        /// <param name="margin">The error margin</param>
        /// <returns>Whether this is the zero vector</returns>
        public bool IsZero(double margin) => this.LengthSquared() < margin;

        /// <summary>
        /// Linearly interpolates between this vector and another vector based on an alpha value
        /// </summary>
        /// <param name="other">The other vector</param>
        /// <param name="alpha">The alpha value, 0.0 - 1.0</param>
        /// <returns>The interpolated vector</returns>
        public Vector2 Lerp(Vector2 other, double alpha) => new Vector2(
            this.X + alpha * (other.X - this.X),
            this.Y + alpha * (other.Y - this.Y));
    }
}
