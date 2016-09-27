using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPiBoy.Common.Utilities
{
    public static class MathUtil
    {
        #region Clamp Function

        /// <summary>
        /// Clamps an integer the the inclusive range specified by a minimum and maximum integer.
        /// </summary>
        /// <param name="value">The value to clamp, passed by reference, so it can be modified</param>
        /// <param name="min">The minimum value</param>
        /// <param name="max">The maximum value</param>
        public static void Clamp(ref int value, int min, int max) => value = Math.Max(min, Math.Min(max, value));

        /// <summary>
        /// Clamps an integer the the inclusive range specified by a minimum and maximum integer.
        /// </summary>
        /// <param name="value">The value to clamp</param>
        /// <param name="min">The minimum value</param>
        /// <param name="max">The maximum value</param>
        /// <returns>The clamped value</returns>
        public static int Clamp(int value, int min, int max) => Math.Max(min, Math.Min(max, value));

        /// <summary>
        /// Clamps a float the the inclusive range specified by a minimum and maximum float.
        /// </summary>
        /// <param name="value">The value to clamp, passed by reference, so it can be modified</param>
        /// <param name="min">The minimum value</param>
        /// <param name="max">The maximum value</param>
        public static void Clamp(ref float value, float min, float max) => value = Math.Max(min, Math.Min(max, value));

        /// <summary>
        /// Clamps a float the the inclusive range specified by a minimum and maximum float.
        /// </summary>
        /// <param name="value">The value to clamp</param>
        /// <param name="min">The minimum value</param>
        /// <param name="max">The maximum value</param>
        /// <returns>The clamped value</returns>
        public static float Clamp(float value, float min, float max) => Math.Max(min, Math.Min(max, value));

        /// <summary>
        /// Clamps a double the the inclusive range specified by a minimum and maximum double.
        /// </summary>
        /// <param name="value">The value to clamp, passed by reference, so it can be modified</param>
        /// <param name="min">The minimum value</param>
        /// <param name="max">The maximum value</param>
        public static void Clamp(ref double value, double min, double max) => value = Math.Max(min, Math.Min(max, value));

        /// <summary>
        /// Clamps a double the the inclusive range specified by a minimum and maximum double.
        /// </summary>
        /// <param name="value">The value to clamp</param>
        /// <param name="min">The minimum value</param>
        /// <param name="max">The maximum value</param>
        /// <returns>The clamped value</returns>
        public static double Clamp(double value, double min, double max) => Math.Max(min, Math.Min(max, value));

        #endregion

        /// <summary>
        /// Linearly interpolates between two colors based on a parcentage value from 0 to 1.
        /// </summary>
        /// <param name="percent">The percent between 0 and 1</param>
        /// <param name="beginColor">The beginning color</param>
        /// <param name="endColor">The ending color</param>
        /// <returns>The interpolated color</returns>
        public static Color LinearlyInterpolateColors(float percent, Color beginColor, Color endColor)
        {
            Color result = new Color();

            result.Set(beginColor);

            result.Red   += percent * (endColor.Red   - beginColor.Red);
            result.Green += percent * (endColor.Green - beginColor.Green);
            result.Blue  += percent * (endColor.Blue  - beginColor.Blue);
            result.Alpha += percent * (endColor.Alpha - beginColor.Alpha);

            return result;
        }
    }
}
