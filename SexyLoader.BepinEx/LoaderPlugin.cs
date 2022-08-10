using System;
using System.Collections.Generic;
using System.Text;
using BepInEx;
using UnityEngine;
using SexyExtending;
using SexyExtending.Debug;
using ExConsole = SexyExtending.Debug.Console;
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
                //ExConsole.ParentPID = GameProcess.Instance.PID;
                ExConsole.Create();
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

            internal const string Version = "1.22.8.10";
        }
    }
}
