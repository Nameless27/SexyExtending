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
                DwmExtendFrameIntoClientArea(handle, ref margins);
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
                DwmExtendFrameIntoClientArea(handle, ref margins);
            }
            #endregion
            #region Windows11 Build 22000
            /// <summary>
            /// Windows11 Build 22000
            /// </summary>
            public void SetCornerRadius(WindowsCornerRadius cornerRadius)
            {
                int attrValue = (int)cornerRadius;
                DwmSetWindowAttribute(handle, DwmWindowAttribute.WindowCornerPreference, ref attrValue);
            }

            /// <summary>
            /// Windows11 Build 22000
            /// </summary>
            public void SetBorder(Drawing.Color color)
            {
                int attrValue = ColorConverter.ToAttributeValue(color);
                DwmSetWindowAttribute(handle, DwmWindowAttribute.BorderColor, ref attrValue);
            }

            /// <summary>
            /// Windows11 Build 22000
            /// </summary>
            public void SetCaption(Drawing.Color color)
            {
                int attrValue = ColorConverter.ToAttributeValue(color);
                DwmSetWindowAttribute(handle, DwmWindowAttribute.CaptionColor, ref attrValue);
            }

            /// <summary>
            /// Windows11 Build 22000
            /// </summary>
            public void SetText(Drawing.Color color)
            {
                int attrValue = ColorConverter.ToAttributeValue(color);
                DwmSetWindowAttribute(handle, DwmWindowAttribute.TextColor, ref attrValue);
            }
            #endregion
            #region Windows 11 Build 22621
            public void SetBackdrop(DWM_SYSTEMBACKDROP_TYPE backdrop)
            {
                int attrValue = (int)backdrop;
                DwmSetWindowAttribute(handle, DwmWindowAttribute.SystemBackdropType, ref attrValue);
            }

            public DWM_SYSTEMBACKDROP_TYPE GetBackdrop()
            {
                DwmGetWindowAttribute(handle, DwmWindowAttribute.SystemBackdropType, out var attrValue);
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
                SetWindowCompositionAttribute(handle, ref data);
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

                SetWindowCompositionAttribute(handle, ref data);
                Marshal.FreeHGlobal(accentPointer);
            }
        }
    }
}
