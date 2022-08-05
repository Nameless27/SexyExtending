using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace SexyExtending
{
    public class GameProcess
    {
        internal GameProcess()
        {
            process = Process.GetCurrentProcess();
            pid = process.Id;
        }

        public static readonly GameProcess Instance = new GameProcess();

        internal Process process;

        internal int pid;
        public int PID => pid;
    }
}
