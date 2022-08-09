using System;
using System.Collections.Generic;
using System.Text;
using BepInEx;
using UnityEngine;
using SexyExtending;

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
            }
        }

        internal static GameWindow gameWindow;

        internal class PluginInfo
        {
            internal const string GUID = "com.Nameless27.SexyExtending";

            internal const string Name = "SexyExtending";

            internal const string Version = "0.22.8.9";
        }
    }
}
