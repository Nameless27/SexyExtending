using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SexyExtending.Win32
{
    public struct Win32Rect
    {
        public static readonly Win32Rect Zero = new Win32Rect();

        public void Set(int left, int right, int top, int bottom)
        {
            this.left = left;
            this.right = right;
            this.top = top;
            this.bottom = bottom;
        }

        public Win32Rect(int left, int right, int top, int bottom)
        {
            this.left = left;
            this.right = right;
            this.top = top;
            this.bottom = bottom;
        }

        internal Win32Rect(RECT rect)
        {
            left = rect.Left;
            right = rect.Right;
            top = rect.Top;
            bottom = rect.Bottom;
        }

        public int left;
        public int right;
        public int top;
        public int bottom;
    }
}
