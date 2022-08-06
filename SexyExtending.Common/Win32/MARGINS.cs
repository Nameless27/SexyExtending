using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SexyExtending.Win32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MARGINS
    {
        internal static readonly MARGINS Zero = new MARGINS();

        public MARGINS(int left, int right, int top, int bottom)
        {
            leftWidth = left;
            rightWidth = right;
            topHeight = top;
            bottomHeight = bottom;
        }

        public int leftWidth;
        public int rightWidth;
        public int topHeight;
        public int bottomHeight;
    }
}
