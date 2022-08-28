using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BepInEx;
using UnityEngine;
using SexyExtending;
using SexyExtending._Debug;
using System.Reflection;

namespace SexyLoader.BepinEx
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class LoaderPlugin : BaseUnityPlugin
    {
        void Awake()
        {
            if (gameWindow == null)
            {
                SexyExtentionLoader.LoadSexyExtending();
                var extensions = SexyExtentionLoader.LoadExtensionsFromDirectory(".\\Mods\\", true);
                ExtensionsManager.Append(extensions);
                gameWindow = GameWindow.Instance;
                var args = Environment.GetCommandLineArgs();
                var debug = args.Where(a => a.ToLower() == "debug");
                if (debug.Count() > 0)
                    DebugEx.IsDebugEnabled = true;
                else
                    DebugEx.IsDebugEnabled = false;
            }
        }

        void Start()
        {
            if (!started)
            {
                started = true;
                var type = typeof(GameProcess);
                var method = type.GetMethod("InvokeOnLoaded", BindingFlags.Instance | BindingFlags.NonPublic);
                method.Invoke(GameProcess.Instance, null);
            }
        }

        internal static GameWindow gameWindow;

        internal static bool started;

        internal class PluginInfo
        {
            internal const string GUID = "com.Nameless27.SexyExtending";

            internal const string Name = "SexyExtending";

            internal const string Version = "1.22.8.11";
        }
    }
}
