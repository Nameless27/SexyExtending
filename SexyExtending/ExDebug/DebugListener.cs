using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SexyExtending._Debug
{
    public static class DebugListener
    {
        public static void AddEvent()
        {
            Application.logMessageReceived -= HandleLog;
            Application.logMessageReceived += HandleLog;
        }

        public static void HandleLog(string logString, string stackTrace, LogType type)
        {
            ConsoleEx.WriteLine($"[{DateTime.UtcNow}][{type}]{logString}");
        }
    }
}
