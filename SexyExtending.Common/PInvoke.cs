using SexyExtending.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SexyExtending
{
    internal class PInvoke
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        internal delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);


        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);


        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);


        internal class Dwm
        {
            [DllImport("dwmapi.dll", PreserveSig = true)]
            internal static extern int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttribute attr, ref int attrValue, int attrSize = sizeof(int));

            [DllImport("dwmapi.dll", PreserveSig = true)]
            internal static extern int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttribute attr, ref bool attrValue, int attrSize = sizeof(bool));

            [DllImport("dwmapi.dll")]
            internal static extern int DwmGetWindowAttribute(IntPtr hwnd, DwmWindowAttribute dwAttribute, out bool pvAttribute, int cbAttribute);

            [DllImport("dwmapi.dll")]
            internal static extern int DwmGetWindowAttribute(IntPtr hwnd, DwmWindowAttribute dwAttribute, out RECT pvAttribute, int cbAttribute);

            [DllImport("dwmapi.dll", ExactSpelling = true)]
            internal static extern unsafe int DwmGetWindowAttribute(IntPtr hwnd, DwmWindowAttribute dwAttribute, void* pvAttribute, int cbAttribute);

            internal static unsafe int DwmGetWindowAttributeInternal(IntPtr hwnd, DwmWindowAttribute attribute, ref int value)
            {
                fixed (void* pointer = &value)
                {
                    var result = DwmGetWindowAttribute(hwnd, attribute, pointer, sizeof(int));
                    return result;
                }
            }

            internal static int DwmGetWindowAttribute(IntPtr hwnd, DwmWindowAttribute attribute, out int value)
            {
                value = 0;
                return DwmGetWindowAttributeInternal(hwnd, attribute, ref value);
            }

            [DllImport("dwmapi.dll")]
            internal static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

            [DllImport("user32.dll")]
            internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        }

        internal class WindowVisible
        {
            [DllImport("user32.dll")]
            internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            internal static void ShowWindow(IntPtr hwnd)
            {
                ShowWindow(hwnd, SW_SHOW);
            }

            internal static void HideWindow(IntPtr hwnd)
            {
                ShowWindow(hwnd, SW_HIDE);
            }

            /// <summary>
            /// Hides the window and activates another window.
            /// </summary>
            internal const int SW_HIDE = 0;

            /// <summary>
            /// Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.
            /// </summary>
            internal const int SW_SHOWNORMAL = 1;
            /// <summary>
            /// Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.
            /// </summary>
            internal const int SW_NORMAL = 1;

            /// <summary>
            /// Activates the window and displays it as a minimized window.
            /// </summary>
            internal const int SW_SHOWMINIMIZED = 2;

            /// <summary>
            /// Activates the window and displays it as a maximized window.
            /// </summary>
            internal const int SW_SHOWMAXIMIZED = 3;
            /// <summary>
            /// Activates the window and displays it as a maximized window.
            /// </summary>
            internal const int SW_MAXIMIZE = 3;

            /// <summary>
            /// Displays a window in its most recent size and position. This value is similar to <seealso cref="SW_SHOWNORMAL"/>, except that the window is not activated.
            /// </summary>
            internal const int SW_SHOWNOACTIVATE = 4;

            /// <summary>
            /// Activates the window and displays it in its current size and position.
            /// </summary>
            internal const int SW_SHOW = 5;

            /// <summary>
            /// Minimizes the specified window and activates the next top-level window in the Z order.
            /// </summary>
            internal const int SW_MINIMIZE = 6;

            /// <summary>
            /// Displays the window as a minimized window. This value is similar to <seealso cref="SW_SHOWMINIMIZED"/>, except the window is not activated.
            /// </summary>
            internal const int SW_SHOWMINNOACTIVE = 7;

            /// <summary>
            /// Displays the window in its current size and position. This value is similar to <seealso cref="SW_SHOW"/>, except that the window is not activated.
            /// </summary>
            internal const int SW_SHOWNA = 8;

            /// <summary>
            /// Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when restoring a minimized window.
            /// </summary>
            internal const int SW_RESTORE = 9;

            /// <summary>
            /// Sets the show state based on the SW_ value specified in the STARTUPINFO structure passed to the CreateProcess function by the program that started the application.
            /// </summary>
            internal const int SW_SHOWDEFAULT = 10;

            /// <summary>
            /// Minimizes a window, even if the thread that owns the window is not responding.<para/> 
            /// This flag should only be used when minimizing windows from a different thread.
            /// </summary>
            internal const int SW_FORCEMINIMIZE = 11;
        }

        internal class WindowPosition
        {
            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl);

            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

            [DllImport("user32.dll", SetLastError = true)]
            internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

            #region Rect

            #region Get
            internal static void GetRectPosition(IntPtr hwnd, out int x, out int y)
            {
                x = 0;
                y = 0;
                if (GetWindowRect(hwnd, out var rect))
                {
                    x = rect.Left;
                    y = rect.Top;
                }
            }

            internal static void GetRectSize(IntPtr hwnd, out int width, out int height)
            {
                width = 0;
                height = 0;
                if (GetWindowRect(hwnd, out var rect))
                {
                    width = rect.Width;
                    height = rect.Height;
                }
            }
            #endregion

            #endregion
            #region Placement

            #region Get

            #region Position
            internal static void GetMaximizedPosition(IntPtr hwnd, out int x, out int y)
            {
                var placement = empty;
                x = 0;
                y = 0;
                if (GetWindowPlacement(hwnd, ref placement))
                {
                    x = placement.MaxPosition.X;
                    y = placement.MaxPosition.Y;
                }
            }

            internal static void GetNormalPosition(IntPtr hwnd, out int x, out int y)
            {
                var placement = empty;
                x = 0;
                y = 0;
                if (GetWindowPlacement(hwnd, ref placement))
                {
                    x = placement.NormalPosition.X;
                    y = placement.NormalPosition.Y;
                }
            }

            internal static void GetMinimizedPosition(IntPtr hwnd, out int x, out int y)
            {
                var placement = empty;
                x = 0;
                y = 0;
                if (GetWindowPlacement(hwnd, ref placement))
                {
                    x = placement.MinPosition.X;
                    y = placement.MinPosition.Y;
                }
            }
            #endregion

            #region Size
            internal static void GetSize(IntPtr hwnd, out int width, out int height)
            {
                var placement = empty;
                width = 0;
                height = 0;
                if (GetWindowPlacement(hwnd, ref placement))
                {
                    width = placement.NormalPosition.Width;
                    height = placement.NormalPosition.Height;
                }
            }
            #endregion

            #endregion

            #region Set

            #region Position
            internal static void SetMaximizedPosition(IntPtr hwnd, int x, int y)
            {
                var placement = empty;
                if (GetWindowPlacement(hwnd, ref placement))
                {
                    placement.MaxPosition.X = x;
                    placement.MaxPosition.X = y;
                    SetWindowPlacement(hwnd, ref placement);
                }
            }

            internal static void SetNormalPosition(IntPtr hwnd, int x, int y)
            {
                var placement = empty;
                if (GetWindowPlacement(hwnd, ref placement))
                {
                    placement.NormalPosition.X = x;
                    placement.NormalPosition.Y = y;
                    SetWindowPlacement(hwnd, ref placement);
                }
            }

            internal static void SetMinimizedPosition(IntPtr hwnd, int x, int y)
            {
                var placement = empty;
                if (GetWindowPlacement(hwnd, ref placement))
                {
                    placement.MinPosition.X = x;
                    placement.MinPosition.Y = y;
                    SetWindowPlacement(hwnd, ref placement);
                }
            }
            #endregion

            #region Size
            internal static void SetSize(IntPtr hwnd, int width, int height)
            {
                var placement = empty;
                if (GetWindowPlacement(hwnd, ref placement))
                {
                    placement.NormalPosition.Width = width;
                    placement.NormalPosition.Height = height;
                    SetWindowPlacement(hwnd, ref placement);
                }
            }
            #endregion

            #endregion

            #endregion

            internal const int WPF_ASYNCWINDOWPLACEMENT = 0x0004;
            internal const int WPF_RESTORETOMAXIMIZED = 0x0002;
            internal const int WPF_SETMINPOSITION = 0x0001;

            internal static readonly WINDOWPLACEMENT empty = WINDOWPLACEMENT.Default;
        }
    }
}
