using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SexyExtending.ExDebug
{
    public class DebugEx
    {
        internal static ConsoleEx Console = ConsoleEx.Instance;


        private static bool isDebugEnabled;

        public static bool IsDebugEnabled
        {
            get => isDebugEnabled;
            set => SetIsEnabled(value);
        }

        private static void SetIsEnabled(bool value)
        {
            if (value == isDebugEnabled) return;
            if (value)
            {
                ConsoleEx.Create();
                Console = ConsoleEx.Instance;
            }
            else
            {
                ConsoleEx.Destory();
                Console = null;
            }
            isDebugEnabled = value;
        }
    }
}
