using SexyExtending.ExDebug;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SexyExtending.GameParameters;

namespace SexyExtending
{
    public class SexyExtentionLoader
    {
        #region LoadFile
        public static SexyExtension LoadFile(string fileName)
        {
            if (!File.Exists(fileName))
                return null;
            var assembly = Assembly.LoadFrom(fileName);
            if (assembly == null)
                return null;
            return Load(assembly);
        }

        public static IEnumerable<SexyExtension> LoadExtensionsFromFile(string fileName)
        {
            if (!File.Exists(fileName))
                yield break;
            var assembly = Assembly.LoadFrom(fileName);
            if (assembly == null)
                yield break;
            foreach (var extension in LoadExtensions(assembly))
            {
                yield return extension;
            }
            yield break;
        }
        #endregion

        #region LoadDirectory
        /// <summary>
        /// 加载路径下的所有程序集<para/>
        /// 只会尝试从一个程序集中获取一个SexyExtension
        /// </summary>
        /// <param name="directory">路径</param>
        /// <param name="toponly">如果为false的话会读取该路径下的所有子路径</param>
        /// <returns>读取到的所有拓展</returns>
        public static IEnumerable<SexyExtension> LoadDirectory(string directory, bool toponly = true)
        {
            var option = toponly ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories;
            if (!Directory.Exists(directory))
            {
                try { Directory.CreateDirectory(directory); }
                catch (Exception) { yield break; }
            }
            var files = Directory.GetFiles(directory, "*.dll", option);
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFrom(file);
                if (assembly == null)
                    continue;
                var extension = Load(assembly);
                if (extension == null)
                    continue;
                yield return extension;
            }
            yield break;
        }

        /// <summary>
        /// 加载路径下的所有程序集<para/>
        /// 会尝试从一个程序集中获取多个SexyExtension
        /// </summary>
        /// <param name="directory">路径</param>
        /// <param name="toponly">如果为false的话会读取该路径下的所有子路径</param>
        /// <returns>读取到的所有拓展</returns>
        public static IEnumerable<SexyExtension> LoadExtensionsFromDirectory(string directory, bool toponly = true)
        {
            var option = toponly ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories;
            if (!Directory.Exists(directory))
            {
                try { Directory.CreateDirectory(directory); }
                catch (Exception) { yield break; }
            }
            var files = Directory.GetFiles(directory, "*.dll", option);
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFrom(file);
                if (assembly == null)
                    continue;
                foreach (var extension in LoadExtensions(assembly))
                {
                    yield return extension;
                }
            }
            yield break;
        }
        #endregion

        #region LoadAssembly
        public static SexyExtension Load(Assembly assembly)
        {
            try
            {
                var alltypes = assembly.GetTypes();
                var maintype = FindType(alltypes, SexyExtension.Type);
                if (maintype == null) return null;
                var ins = Activator.CreateInstance(maintype);
                if (ins == null)
                    return null;
                var instance = (SexyExtension)ins;

                instance.OnExtensionLoaded();
                if (DebugEx.IsDebugEnabled)
                {
                    Debug.Log($"[{DateTime.UtcNow}][ExtensionLoaded]{instance.Name}{instance.Version} by {instance.Author}");
                    foreach (var descriptionLine in instance.Description.Split('\n'))
                    {
                        Debug.Log(descriptionLine.PadLeft(4, ' '));
                    }
                    Debug.Log("");
                }
                return instance;
            }
            catch (Exception ex)
            {
                if (DebugEx.IsDebugEnabled)
                {
                    Debug.Log(ex.Message);
                }
                return null;
            }
        }

        public static IEnumerable<SexyExtension> LoadExtensions(Assembly assembly)
        {
            Type[] alltypes;
            IEnumerable<Type> maintypes;
            try
            {
                alltypes = assembly.GetTypes();
                maintypes = FindTypes(alltypes, SexyExtension.Type);
            }
            catch (Exception ex)
            {
                if (DebugEx.IsDebugEnabled)
                {
                    Debug.Log(ex.Message);
                }
                yield break;
            }
            foreach (var maintype in maintypes)
            {
                SexyExtension instance = null;
                try
                {
                    var ins = Activator.CreateInstance(maintype);
                    if (ins == null)
                        continue;
                    instance = (SexyExtension)ins;

                    instance.OnExtensionLoaded();
                    if (DebugEx.IsDebugEnabled)
                    {
                        Debug.Log($"[{DateTime.UtcNow}][ExtensionLoaded]{instance.Name}{instance.Version} by {instance.Author}");
                        foreach (var descriptionLine in instance.Description.Split('\n'))
                        {
                            Debug.Log(descriptionLine.PadLeft(4, ' '));
                        }
                        Debug.Log("");
                    }
                }
                catch (Exception ex)
                {
                    if (DebugEx.IsDebugEnabled)
                    {
                        Debug.Log(ex.Message);
                    }
                    yield break;
                }
                if (instance == null)
                    continue;
                yield return instance;
            }
            yield break;
        }

        private static Type FindType(IEnumerable<Type> types, Type basetype)
        {
            foreach (var type in types)
            {
                if (type.IsSubclassOf(basetype))
                    return type;
            }
            return null;
        }

        private static IEnumerable<Type> FindTypes(IEnumerable<Type> types, Type basetype)
        {
            return types.Where(t => t.IsSubclassOf(basetype));
        }
        #endregion

        #region LoadSexyExtending
        public static void LoadSexyExtending(bool debug = false)
        {
            SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
            var active = SceneManager.GetActiveScene();
            DebugEx.IsDebugEnabled = debug;
            SceneManager_activeSceneChanged(active, active);
            
        }

        private static void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
        {
            if (DebugEx.IsDebugEnabled)
            {
                DebugListener.AddEvent();
            }
            player = GameObject.Find(PLAYER_NAME) ?? player;
            handle = GameObject.Find(HANDLE_NAME) ?? handle;
            hub = GameObject.Find(HUB_NAME) ?? hub;
            tip = GameObject.Find(TIP_NAME) ?? tip;
            cursor = GameObject.Find(CURSOR_NAME) ?? cursor;
            currentScene = arg1;
            switch (arg1.name)
            {
                case LOADER_NAME:
                    currentSceneEnum = Scenes.Loader;
                    break;
                case CREDITS_NAME:
                    currentSceneEnum = Scenes.Credits;
                    break;
                case MIAN_NAME:
                    currentSceneEnum = Scenes.Mian;
                    break;
                case REWARDLOADER_NAME:
                    currentSceneEnum = Scenes.RewardLoader;
                    break;
                default:
                    break;
            }

            foreach (var extension in ExtensionsManager.extensions)
            {
                extension.OnSceneChanged(arg1);
            }
            //Debug.Debug.Log($"{DateTime.UtcNow}SceneChanged:{arg1.name}");
            Debug.Log($"SceneChanged:{arg1.name}");
        }
        #endregion
    }
}
