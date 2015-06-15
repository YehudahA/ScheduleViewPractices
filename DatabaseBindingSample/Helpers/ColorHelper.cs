using System.Windows.Media;

namespace DatabaseBindingSample.Helpers
{
    public static class ColorHelper
    {
        /// <summary>
        /// Converts the string representation of a color to its System.Windows.Media.Color equivalent
        /// A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="s">The string representation of the color to convert.</param>
        /// <param name="color"></param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParse(string s, out Color? color)
        {
            bool parsed = false;
            object obj = null;
            
            color = null;

            try
            {
                obj = ColorConverter.ConvertFromString(s);
            }
            catch
            {
            }

            if (obj is Color)
            {
                color = (Color)obj;
                parsed = true;
            }

            return parsed;
        }

        /// <summary>
        /// Try parse string to color. If the conversion failed, return the defaultColor.
        /// </summary>
        /// <param name="s">The string representation of the color to convert.</param>
        /// <param name="defaultColor">The color that return if conversion failed</param>
        /// <returns>Color</returns>
        public static Color ParseOrDefault(string s, Color defaultColor)
        {
            Color? color;
            bool parsed = TryParse(s, out color);

            if (parsed)
                return color.Value;

            return defaultColor;
        }
    }
}
