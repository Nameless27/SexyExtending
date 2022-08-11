using SexyExtending.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using static SexyExtending.PInvoke;
using Drawing = System.Drawing;

namespace SexyExtending
{
    public class GameWindow
    {
        public static readonly GameWindow Instance = new GameWindow();

        internal GameWindow()
        {
            EnumWindows(FindGameWindowProc, IntPtr.Zero);
            Dwm = new DwmApi();
            Dwm.handle = handle;
        }

        internal IntPtr handle = IntPtr.Zero;

        #region WindowPosition
        internal int x;
        internal int minX;
        internal int maxX;
        internal int y;
        internal int minY;
        internal int maxY;
        internal int width;
        internal int height;

        #region Position

        #region Normal
        public int X
        {
            get
            {
                WindowPosition.GetNormalPosition(handle, out x, out _);
                return x;
            }
            set
            {
                x = value;
                WindowPosition.SetNormalPosition(handle, x, y);
            }
        }

        public int Y
        {
            get
            {
                WindowPosition.GetNormalPosition(handle, out _, out y);
                return y;
            }
            set
            {
                y = value;
                WindowPosition.SetNormalPosition(handle, x, y);
            }
        }
        #endregion

        #region Minimized
        public int MinX
        {
            get
            {
                WindowPosition.GetNormalPosition(handle, out minX, out _);
                return minX;
            }
            set
            {
                minX = value;
                WindowPosition.SetNormalPosition(handle, minX, minY);
            }
        }

        public int MinY
        {
            get
            {
                WindowPosition.GetNormalPosition(handle, out _, out minY);
                return minY;
            }
            set
            {
                minY = value;
                WindowPosition.SetNormalPosition(handle, minX, minY);
            }
        }
        #endregion

        #region Maximized
        public int MaxX
        {
            get
            {
                WindowPosition.GetNormalPosition(handle, out maxX, out _);
                return maxX;
            }
            set
            {
                maxX = value;
                WindowPosition.SetNormalPosition(handle, maxX, maxY);
            }
        }

        public int MaxY
        {
            get
            {
                WindowPosition.GetNormalPosition(handle, out _, out maxY);
                return maxY;
            }
            set
            {
                y = value;
                WindowPosition.SetNormalPosition(handle, maxY, maxY);
            }
        }
        #endregion

        public void GetPosition(out int x, out int y)
        {
            WindowPosition.GetRectPosition(handle, out x, out y);
        }

        public void SetPosition(int x, int y)
        {
            WindowPosition.SetNormalPosition(handle, x, y);
        }

        public void GetMaximizedPosition(out int x, out int y)
        {
            WindowPosition.GetMaximizedPosition(handle, out x, out y);
        }

        public void SetMaximizedPosition(int x, int y)
        {
            WindowPosition.SetMaximizedPosition(handle, x, y);
        }

        public void GetMinimizedPosition(out int x, out int y)
        {
            WindowPosition.GetMinimizedPosition(handle, out x, out y);
        }

        public void SetMinimizedPosition(int x, int y)
        {
            WindowPosition.SetMinimizedPosition(handle, x, y);
        }
        #endregion

        #region Size
        public int Width
        {
            get
            {
                WindowPosition.GetSize(handle, out width, out _);
                return width;
            }
            set
            {
                width = value;
                WindowPosition.SetSize(handle, value, height);
            }
        }

        public int Height
        {
            get
            {
                WindowPosition.GetSize(handle, out _, out height);
                return height;
            }
            set
            {
                height = value;
                WindowPosition.SetSize(handle, width, value);
            }
        }

        public void GetSize(out int width, out int height)
        {
            WindowPosition.GetSize(handle, out width, out height);
        }

        public void SetSize(int width, int height)
        {
            WindowPosition.SetSize(handle, width, height);
        }
        #endregion

        #region Move
        public void Move(int x, int y, bool rePaint = false)
        {
            WindowPosition.Move(handle, x, y, rePaint);
        }

        public void Resize(int width, int height, bool rePaint = false)
        {
            WindowPosition.Resize(handle, width, height, rePaint);
        }
        #endregion
        #endregion

        #region FindGameWindowProc
        internal bool FindGameWindowProc(IntPtr hWnd, IntPtr lParam)
        {
            GetWindowThreadProcessId(hWnd, out var pid);
            if (pid == GameProcess.Instance.pid)
            {
                var capacity = GetWindowTextLength(hWnd) * 2;
                var builder = new StringBuilder(capacity);
                GetWindowText(hWnd, builder, builder.Capacity);
                if (builder.ToString() == Application.productName)
                {
                    handle = hWnd;
                }
            }
            return true;
        }
        #endregion

        #region Event
        public event Action OnLoaded
        {
            add => GameProcess.Instance.onLoaded += value;
            remove => GameProcess.Instance.onLoaded -= value;
        }
        #endregion

        public DwmApi Dwm;

        public class DwmApi
        {
            internal IntPtr handle;
            #region Windows Vista +
            /// <summary>
            /// Windows Vista +
            /// </summary>
            public void SetGlassFrameThickness(int thickness)
            {
                var margins = MARGINS.Zero;
                var value = Math.Max(0, thickness);
                margins.leftWidth = value;
                margins.rightWidth = value;
                margins.topHeight = value;
                margins.bottomHeight = value;
                PInvoke.Dwm.DwmExtendFrameIntoClientArea(handle, ref margins);
            }

            /// <summary>
            /// Windows Vista +
            /// </summary>
            public void SetGlassFrameThickness(int left, int top, int right, int bottom)
            {
                var margins = MARGINS.Zero;
                margins.leftWidth = Math.Max(0, left);
                margins.rightWidth = Math.Max(0, right);
                margins.topHeight = Math.Max(0, top);
                margins.bottomHeight = Math.Max(0, bottom);
                PInvoke.Dwm.DwmExtendFrameIntoClientArea(handle, ref margins);
            }
            #endregion
            #region Windows11 Build 22000
            /// <summary>
            /// Windows11 Build 22000
            /// </summary>
            public void SetCornerRadius(WindowsCornerRadius cornerRadius)
            {
                int attrValue = (int)cornerRadius;
                PInvoke.Dwm.DwmSetWindowAttribute(handle, DwmWindowAttribute.WindowCornerPreference, ref attrValue);
            }

            /// <summary>
            /// Windows11 Build 22000
            /// </summary>
            public void SetBorder(Drawing.Color color)
            {
                int attrValue = ColorConverter.ToAttributeValue(color);
                PInvoke.Dwm.DwmSetWindowAttribute(handle, DwmWindowAttribute.BorderColor, ref attrValue);
            }

            /// <summary>
            /// Windows11 Build 22000
            /// </summary>
            public void SetCaption(Drawing.Color color)
            {
                int attrValue = ColorConverter.ToAttributeValue(color);
                PInvoke.Dwm.DwmSetWindowAttribute(handle, DwmWindowAttribute.CaptionColor, ref attrValue);
            }

            /// <summary>
            /// Windows11 Build 22000
            /// </summary>
            public void SetText(Drawing.Color color)
            {
                int attrValue = ColorConverter.ToAttributeValue(color);
                PInvoke.Dwm.DwmSetWindowAttribute(handle, DwmWindowAttribute.TextColor, ref attrValue);
            }
            #endregion
            #region Windows 11 Build 22621
            public void SetBackdrop(DWM_SYSTEMBACKDROP_TYPE backdrop)
            {
                int attrValue = (int)backdrop;
                PInvoke.Dwm.DwmSetWindowAttribute(handle, DwmWindowAttribute.SystemBackdropType, ref attrValue);
            }

            public DWM_SYSTEMBACKDROP_TYPE GetBackdrop()
            {
                PInvoke.Dwm.DwmGetWindowAttribute(handle, DwmWindowAttribute.SystemBackdropType, out var attrValue);
                return (DWM_SYSTEMBACKDROP_TYPE)attrValue;
            }
            #endregion

            public void SetAccent(AccentState state)
            {
                var accent = new AccentPolicy()
                {
                    AccentState = state,
                };

                var accentSize = Marshal.SizeOf(accent);
                var accentPointer = Marshal.AllocHGlobal(accentSize);
                Marshal.StructureToPtr(accent, accentPointer, false);

                var data = new WindowCompositionAttributeData
                {
                    Attribute = WindowCompositionAttribute.AccentPolicy,
                    SizeOfData = accentSize,
                    Data = accentPointer
                };
                PInvoke.Dwm.SetWindowCompositionAttribute(handle, ref data);
                Marshal.FreeHGlobal(accentPointer);
            }

            public void SetBlurBehind(AccentState state, Drawing.Color? color = null)
            {
                var accent = new AccentPolicy()
                {
                    AccentState = state,
                };
                if (color != null)
                {
                    var attrColor = ColorConverter.ToAttributeValue(color);
                    accent.GradientColor = attrColor;
                }

                var accentSize = Marshal.SizeOf(accent);
                var accentPointer = Marshal.AllocHGlobal(accentSize);
                Marshal.StructureToPtr(accent, accentPointer, false);

                var data = new WindowCompositionAttributeData
                {
                    Attribute = WindowCompositionAttribute.AccentPolicy,
                    SizeOfData = accentSize,
                    Data = accentPointer
                };

                PInvoke.Dwm.SetWindowCompositionAttribute(handle, ref data);
                Marshal.FreeHGlobal(accentPointer);
            }
        }
    }
}
