using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SexyExtending
{
    public class GameParameters
    {
        public const string PLAYER_NAME = "Player";
        public const string HANDLE_NAME = "handle";
        public const string HUB_NAME = "Hub";
        public const string TIP_NAME = "Tip";
        public const string CURSOR_NAME = "Cursor";

        public const string LOADER_NAME = "Loader";
        public const string MIAN_NAME = "Mian";
        public const string CREDITS_NAME = "Credits";
        public const string REWARDLOADER_NAME = "Reward Loader";

        static GameParameters()
        {
            var hasExe = Directory.EnumerateFiles(gamePath).Where(f => f.EndsWith("GettingOverIt.exe")).Count() > 0;
            var hasDll = File.Exists(assemblyCSharp);
            valid = hasExe && hasDll;
        }

        internal static GameObject player;
        public static GameObject Player => player;

        internal static GameObject handle;
        public static GameObject Handle => handle;

        internal static GameObject hub;
        public static GameObject Hub => hub;

        internal static GameObject tip;
        public static GameObject Tip => tip;

        internal static GameObject cursor;
        public static GameObject Cursor => cursor;


        internal static Scenes currentSceneEnum;
        public static Scenes CurrentSceneEnum => currentSceneEnum;

        internal static Scene currentScene;
        public static Scene CurrentScene => currentScene;


        internal static bool valid = true;
        public static bool Invalid => !valid;
        public static bool Valid => valid;

        internal static string gamePath = Directory.GetCurrentDirectory() + "\\";
        public static string GamePath => gamePath;

        internal static string assemblyCSharp = string.Format("{0}GettingOverIt_Data\\Managed\\Assembly-CSharp.dll", gamePath);
        public static string AssemblyCSharp => assemblyCSharp;
    }
}
