using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SexyExtending.Win32
{
    public class ColorConverter
    {
        public static int ToAttributeValue(Color? color, bool prefereddwm = true)
        {
            if (color.HasValue && prefereddwm)
            {
                return (color.Value.R) +
                       (color.Value.G << 8) +
                       (color.Value.B << 16);
            }
            return -2;
        }
        public static Color ToDrawingColor(int attributeValue)
        {
            if (attributeValue > 0xFFFFFF) return Color.Transparent;
            var r = attributeValue >> 16;
            attributeValue -= r << 16;
            var g = attributeValue >> 8;
            attributeValue -= g << 8;
            var b = attributeValue;
            return Color.FromArgb((byte)r, (byte)g, (byte)b);
        }
    }
}
