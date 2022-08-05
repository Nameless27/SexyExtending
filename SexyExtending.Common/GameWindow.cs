using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace SexyExtending
{
    public class GameWindow
    {
        public static readonly GameWindow Instance = new GameWindow();

        internal GameWindow()
        {
            PInvoke.EnumWindows(FindGameWindowProc, IntPtr.Zero);
        }

        internal IntPtr handle = IntPtr.Zero;


        internal bool FindGameWindowProc(IntPtr hWnd, IntPtr lParam)
        {
            PInvoke.GetWindowThreadProcessId(hWnd, out var pid);
            if (pid == GameProcess.Instance.pid)
            {
                var capacity = PInvoke.GetWindowTextLength(hWnd) * 2;
                var builder = new StringBuilder(capacity);
                PInvoke.GetWindowText(hWnd, builder, builder.Capacity);
                if (builder.ToString() == Application.productName)
                {
                    handle = hWnd;
                }
            }
            return true;
        }
    }
}
