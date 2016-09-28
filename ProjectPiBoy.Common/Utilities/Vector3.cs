using System;

namespace ProjectPiBoy.Common.Utilities
{
    /// <summary>
    /// Represents a three dimensional vector
    /// </summary>
    /// <remarks>
    /// Based on Vector3 in Dalton's ShockLib Minecraft mod.
    /// </remarks>
    public struct Vector3 : IFormattable
    {
        /// <summary>The X component of the vector</summary>
        public double X { get; set; }

        /// <summary>The Y component of the vector</summary>
        public double Y { get; set; }

        /// <summary>The Z component of the vector</summary>
        public double Z { get; set; }

        //Static vectors, DO NOT MODIFY
        public static readonly Vector3 One   = new Vector3(1, 1, 1);
        public static readonly Vector3 UnitX = new Vector3(1, 0, 0);
        public static readonly Vector3 UnitY = new Vector3(0, 1, 0);
        public static readonly Vector3 UnitZ = new Vector3(0, 0, 1);
        public static readonly Vector3 Zero  = new Vector3(0, 0, 0);

        /// <summary>
        /// Constructs a new <see cref="Vector3"/>
        /// </summary>
        /// <param name="x">The X component of this vector</param>
        /// <param name="y">The Y component of this vector</param>
        /// <param name="z">The Z component of this vector</param>
        public Vector3(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Constructs a new <see cref="Vector3"/> based on another <see cref="Vector3"/>
        /// </summary>
        /// <param name="other"></param>
        public Vector3(Vector3 other) : this(other.X, other.Y, other.Z) { }

        /// <summary>
        /// Sets the values of this vector
        /// </summary>
        /// <param name="x">The X component of this vector</param>
        /// <param name="y">The Y component of this vector</param>
        /// <param name="z">The Z component of this vector</param>
        /// <returns>This vector for chaining</returns>
        public Vector3 Set(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            return this;
        }

        /// <summary>
        /// Sets the components of this vector to the components of another vector
        /// </summary>
        /// <param name="other">The other vector</param>
        /// <returns>This vector for chaining</returns>
        public Vector3 Set(Vector3 other) => this.Set(other.X, other.Y, other.Z);

        /// <summary>
        /// Adds two vectors together
        /// </summary>
        /// <param name="vec1">The first vector to add</param>
        /// <param name="vec2">The second vector to add</param>
        /// <returns>A new vector, the addition of the other vectors</returns>
        public static Vector3 operator +(Vector3 vec1, Vector3 vec2) => new Vector3(vec1.X + vec2.X, vec1.Y + vec2.Y, vec1.Z + vec2.Z);

        /// <summary>
        /// Subtracts a vector from another vector
        /// </summary>
        /// <param name="vec1">The first vector</param>
        /// <param name="vec2">The vector to subtract</param>
        /// <returns>A new vector, the difference of the other vectors</returns>
        public static Vector3 operator -(Vector3 vec1, Vector3 vec2) => new Vector3(vec1.X - vec2.X, vec1.Y - vec2.Y, vec1.Z - vec2.Z);

        /// <summary>
        /// Checks if two vectors are equal
        /// </summary>
        /// <param name="vec1">The first vector</param>
        /// <param name="vec2">The second vector</param>
        /// <returns>Whether the vectors are equal</returns>
        public static bool operator ==(Vector3 vec1, Vector3 vec2) => vec1.X == vec2.X && vec1.Y == vec2.Y && vec1.Z == vec2.Z;

        /// <summary>
        /// Checks if two vectors are not equal
        /// </summary>
        /// <param name="vec1">The first vector</param>
        /// <param name="vec2">The second vector</param>
        /// <returns>Whether the vectors are not equal</returns>
        public static bool operator !=(Vector3 vec1, Vector3 vec2) => !(vec1 == vec2);

        /// <summary>
        /// Scales the vector with a scalar value
        /// </summary>
        /// <param name="vec">The vector to scale</param>
        /// <param name="scalar">The scalar value</param>
        /// <returns>A new scaled vector</returns>
        public static Vector3 operator *(Vector3 vec, double scalar) => new Vector3(vec.X * scalar, vec.Y * scalar, vec.Z * scalar);

        /// <summary>
        /// Multiplies the two vectors together, component wise.
        /// </summary>
        /// <param name="vec1">The first vector</param>
        /// <param name="vec2">The second vector</param>
        /// <returns>The result of the multiplication</returns>
        public static Vector3 operator *(Vector3 vec1, Vector3 vec2) => new Vector3(vec1.X * vec2.X, vec1.Y * vec2.Y, vec1.Z * vec2.Z);

        /// <summary>
        /// Divides the left vector by the right, component wise.
        /// </summary>
        /// <param name="vec1">The first vector</param>
        /// <param name="vec2">The second vector</param>
        /// <returns>The result of the division</returns>
        public static Vector3 operator /(Vector3 vec1, Vector3 vec2) => new Vector3(vec1.X / vec2.X, vec1.Y / vec2.Y, vec1.Z / vec2.Z);

        /// <summary>
        /// Checks if this vector equals the specified object
        /// </summary>
        /// <param name="obj">The object</param>
        /// <returns>Whether this vector equals the specified object</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Vector3))
                return false;

            Vector3 other = (Vector3)obj;

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
                hash = hash * 23 + this.Z.GetHashCode();

                return hash;
            }
        }

        /// <summary>
        /// Computes the length of the vector
        /// </summary>
        /// <returns>The length of the vector</returns>
        public double Length() => Math.Sqrt(X * X + Y * Y + Z * Z);

        /// <summary>
        /// Computes the squared length of the vector. Useful for comparing the length of vectors while avoiding the square root computation.
        /// </summary>
        /// <returns>The squared length of the vector</returns>
        public double LengthSquared() => X * X + Y * Y + Z * Z;

        /// <summary>
        /// Creates a string representation of the vector
        /// </summary>
        /// <returns>A string representation of the vector</returns>
        public override string ToString() => $"(X:{this.X}, Y:{this.Y}, Z:{this.Z})";

        /// <summary>
        /// Creates a formatted string representation of the vector
        /// </summary>
        /// <param name="format">The string format</param>
        /// <param name="formatProvider">The format provider</param>
        /// <returns>A formatted string representation of this vector</returns>
        public string ToString(string format, IFormatProvider formatProvider) =>
            $"(X:{this.X.ToString(format, formatProvider)}, Y:{this.Y.ToString(format, formatProvider)}, Z:{this.Z.ToString(format, formatProvider)})";

        /// <summary>
        /// Creates a new copy of this vector
        /// </summary>
        /// <returns>A new copy of this vector</returns>
        public Vector3 Copy() => new Vector3(this);

        /// <summary>
        /// Calculates the distance from this vector to another vector
        /// </summary>
        /// <param name="other">The other vector</param>
        /// <returns>The distance from this vector to another vector</returns>
        public double DistanceTo(Vector3 other)
        {
            double xDiff = other.X - this.X;
            double yDiff = other.Y - this.Y;
            double zDiff = other.Z = this.Z;

            return Math.Sqrt(xDiff * xDiff + yDiff * yDiff + zDiff * zDiff);
        }

        /// <summary>
        /// Calculates the squared distance from this vector to another vector Useful for comparing distances while avoiding the square root computation.
        /// </summary>
        /// <param name="other">The other vector</param>
        /// <returns>The distance from this vector to another vector</returns>
        public double DistanceToSquared(Vector3 other)
        {
            double xDiff = other.X - this.X;
            double yDiff = other.Y - this.Y;
            double zDiff = other.Z = this.Z;

            return xDiff * xDiff + yDiff * yDiff + zDiff * zDiff;
        }

        /// <summary>
        /// Creates a normalized copy of this vector so that its length is approximately 1
        /// </summary>
        /// <returns>A new, normalized vector</returns>
        public Vector3 Normalize()
        {
            double length2 = this.LengthSquared();

            if (length2 == 0D || length2 == 1D) //If this is already or can't be normalized, return a copy
                return new Vector3(this);
            else //Scale the vector and return it
                return this * (1D / Math.Sqrt(length2));
        }

        /// <summary>
        /// Computes the dot product of this vector and another vector
        /// </summary>
        /// <param name="other">The other vector</param>
        /// <returns>The dot product of the two vectors</returns>
        public double DotProduct(Vector3 other) => this.X * other.X + this.Y * other.Y * this.Z * other.Z;

        /// <summary>
        /// Returns the cross product of this vector and another vector
        /// </summary>
        /// <param name="other">The other vector</param>
        /// <returns>The cross product of the two vectors</returns>
        public Vector3 CrossProduct(Vector3 other) => new Vector3(
            this.Y * other.Z - this.Z * other.Y,
            this.Z * other.X - this.X * other.Z,
            this.X * other.Y - this.Y * other.X);

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
        public Vector3 Lerp(Vector3 other, double alpha) => new Vector3(
            this.X + alpha * (other.X - this.X),
            this.Y + alpha * (other.Y - this.Y),
            this.Z + alpha * (other.Z - this.Z));
    }
}
