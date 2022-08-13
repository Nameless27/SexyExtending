using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SexyExtending.Debug
{
    public class Debug
    {
        public static void LogConsole(string message)
        {
            if (!isDebugEnabled)
                return;
            var time = DateTime.Now;
            var text = string.Format("[{0}]{1}", time.ToString("T"), message);
            Console.WriteLine(text);
        }

        public static void LogConsole()
        {
            if (!isDebugEnabled)
                return;
            
            Console.WriteLine(" ");
        }

        public static void LogFile(string message)
        {
            if (!isDebugEnabled)
                return;
            var time = DateTime.Now;
            var text = string.Format("[{0}]{1}", time.ToString("T"), message);
            File.WriteLine(text);
        }

        public static void LogFile()
        {
            if (!isDebugEnabled)
                return;

            File.WriteLine(" ");
        }

        public static void Log(string message)
        {
            if (!isDebugEnabled)
                return;
            var time = DateTime.Now;
            var text = string.Format("[{0}]{1}", time.ToString("T"), message);
            Console.WriteLine(text);
            File.WriteLine(text);
        }

        public static void Log()
        {
            if (!isDebugEnabled)
                return;
            LogConsole();
            LogFile();
        }


        internal static Console Console = Console.Instance;

        internal static DebugFile File;


        private static bool isDebugEnabled;

        public static bool IsDebugEnabled
        {
            get => isDebugEnabled;
            set => SetIsEnabled(value);
        }

        private static void SetIsEnabled(bool value)
        {
            if (value)
            {
                Console.Create();
                Console = Console.Instance;
                if (DebugFile.opened)
                    File = DebugFile.TryRead("sexy_extending_debug.log");
                else
                    File = DebugFile.TryCreate("sexy_extending_debug.log");
            }
            else
            {
                Console.Destory();
                Console = null;
                File = null;
            }
            isDebugEnabled = value;
        }
    }
}
