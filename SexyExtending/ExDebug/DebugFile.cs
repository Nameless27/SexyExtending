using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SexyExtending.ExDebug
{
    public class DebugFile
    {
        #region .cctor
        public DebugFile(string fileName)
        {
            info = new FileInfo(fileName);
            try { info.Create().Close(); }
            catch (Exception) { throw; }
            writer = new StreamWriter(info.FullName);
            writer.AutoFlush = true;
            append = false;
            opened = true;
        }

        internal DebugFile(string fileName, bool XXX)
        {
            info = new FileInfo(fileName);
            if (!info.Exists)
            {
                try { info.Create().Close(); }
                catch (Exception) { throw; }
            }
            append = true;
            opened = true;
            writer = new StreamWriter(info.FullName, true);
            writer.AutoFlush = true;
        }
        #endregion

        #region Create
        public static DebugFile TryCreate(string fileName)
        {
            try
            {
                var file = new DebugFile(fileName);
                return file;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DebugFile Create(string fileName)
        {
            var file = new DebugFile(fileName);
            return file;
        }
        #endregion

        #region Read
        public static DebugFile TryRead(string fileName)
        {
            try
            {
                var file = new DebugFile(fileName, false);
                return file;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DebugFile Read(string fileName)
        {
            var file = new DebugFile(fileName, false);
            return file;
        }
        #endregion

        #region Write
        public void Write(string text)
        {
            if (!info.Exists)
                return;
            if (writer == null)
                writer = new StreamWriter(info.FullName, append);
            writer.Write(text);
            append = true;
        }

        public void WriteLine(string text)
        {
            if (!info.Exists)
                return;
            if (writer == null)
                writer = new StreamWriter(info.FullName, append);
            writer.WriteLine(text);
            append = true;
        }
        #endregion

        internal FileInfo info;

        internal StreamWriter writer;

        internal bool append;

        internal bool isEnabled;
        public bool IsEnabled
        {
            get => isEnabled;
            set => SetEnabled(value);
        }
        void SetEnabled(bool value)
        {
            if (!info.Exists)
                return;
            if (value)
            {
                writer = new StreamWriter(info.FullName);
                writer.AutoFlush = true;
            }
            else
            {
                writer.Close();
                writer = null;
            }
            isEnabled = value;
        }

        internal static bool opened = false;
    }
}
