using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleBase = System.Console;
using System.Runtime.InteropServices;

namespace SexyExtending.Debug
{
    public sealed class Console
    {
        public static void Show()
        {

        }

        public static void Hide()
        {

        }

        public static void Create()
        {
            if (Instance != null)
                return;
            if (AllocConsole())
            {
                instance = new Console();
                instance.hwnd = GetConsoleWindow();
                instance.stream = ConsoleBase.OpenStandardOutput();
                instance.writer = new StreamWriter(Instance.Stream);
                instance.writer.AutoFlush = true;
                instance.Title = "Sexy Extending - Console";
            }
        }

        public static void Destory()
        {
            CloseHandle(instance.hwnd);
            instance.Writer.Close();
            instance.stream = null;
            instance = null;
        }

        public static void Write(string text)
        {
            if (instance != null)
            {
                ConsoleBase.Write(text);
                instance.writer.Write(text);
            }
        }

        public static void WriteLine(string text)
        {
            if (instance != null)
            {
                ConsoleBase.WriteLine(text);
                instance.writer.WriteLine(text);
            }
        }

        public static void SetTitle(string title)
        {
            if (SetConsoleTitle(title))
            {
                instance.title = title;
            }
        }

        internal string title = Directory.GetCurrentDirectory() + "GettingOverIt.exe";
        public string Title
        {
            get => title;
            set => SetTitle(value);
        }

        internal IntPtr hwnd;
        public IntPtr Hwnd => hwnd;

        internal StreamWriter writer;
        internal StreamWriter Writer => writer;

        internal Stream stream;
        public Stream Stream => stream;

        internal static System.Diagnostics.Process parent;
        internal static int parentPID = -1;
        public static int ParentPID
        {
            get => parentPID;
            set
            {
                var process = System.Diagnostics.Process.GetProcessById(value);
                if (process == null)
                    return;
                parent = process;
                parentPID = value;
            }
        }

        internal static Console instance;
        public static Console Instance => instance;


        #region P/Invoke
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CreateFile(string fileName,
                                                uint desiredAccess,
                                                int shareMode,
                                                IntPtr securityAttributes,
                                                int creationDisposition,
                                                int flagsAndAttributes,
                                                IntPtr templateFile);

        [DllImport("kernel32.dll", SetLastError = false)]
        private static extern bool FreeConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetStdHandle(int nStdHandle, IntPtr hConsoleOutput);

        [DllImport("kernel32.dll", BestFitMapping = true, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SetConsoleTitle(string title);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibraryEx(string lpLibFileName, IntPtr hFile, uint dwFlags);
        #endregion
    }
}
