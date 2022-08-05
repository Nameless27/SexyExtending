using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SexyExtending
{
    public class ExtensionsManager
    {
        internal static ExtensionCollection extensions = new ExtensionCollection();
        public static ExtensionCollection Extensions => extensions;

        public static void Append(IEnumerable<SexyExtension> extensions)
        {
            foreach (var extension in extensions)
            {
                Add(extension);
            }
        }

        public static void Add(SexyExtension extension)
        {
            if (extension == null)
                return;
            if (extensions.Contains(extension))
                return;
            extensions.Add(extension);
        }

        #region FindGenerics
        /// <summary>
        /// 寻找所有同类泛型的拓展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>所有寻找到的拓展, 如果没有找到则返回空列表</returns>
        public IEnumerable<SexyExtension<T>> FindGenericsExtenions<T>() where T : MonoBehaviour
        {
            foreach (var extension in extensions)
            {
                if (extension is SexyExtension<T> generics)
                    yield return generics;
            }
            yield break;
        }

        /// <summary>
        /// 通过泛型寻找泛型拓展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>如果寻找到了拓展则返回拓展, 否则返回null</returns>
        public SexyExtension<T> FindGenerics<T>() where T : MonoBehaviour => FindGenericsExtenion<T>();

        /// <summary>
        /// 通过泛型寻找泛型拓展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>如果寻找到了拓展则返回拓展, 否则返回null</returns>
        public SexyExtension<T> FindGenericsExtenion<T>() where T : MonoBehaviour
        {
            foreach (var extension in extensions)
            {
                if (extension is SexyExtension<T> generics)
                    return generics;
            }
            return null;
        }
        #endregion
        #region FindName
        /// <summary>
        /// 通过名称寻找拓展
        /// </summary>
        /// <param name="name">拓展名称</param>
        /// <returns>如果寻找到了拓展则返回拓展, 否则返回null</returns>
        public SexyExtension FindName(string name)
        {
            foreach (var extension in extensions)
            {
                if (extension.Name == name)
                    return extension;
            }
            return null;
        }

        /// <summary>
        /// 通过名称寻找多个拓展
        /// </summary>
        /// <param name="name">拓展名称</param>
        /// <returns>所有寻找到的拓展, 如果没有找到则返回空列表</returns>
        public IEnumerable<SexyExtension> FindNames(string name)
        {
            foreach (var extension in extensions)
            {
                if (extension.Name == name)
                    yield return extension;
            }
            yield break;
        }

        /// <summary>
        /// 通过名称寻找泛型拓展
        /// </summary>
        /// <param name="name">拓展名称</param>
        /// <returns>如果寻找到了拓展则返回拓展, 否则返回null</returns>
        public SexyExtension<MonoBehaviour> FindGenericsByName(string name) => FindGenericsExtensionByName(name);

        /// <summary>
        /// 通过名称寻找泛型拓展
        /// </summary>
        /// <param name="name">拓展名称</param>
        /// <returns>如果寻找到了拓展则返回拓展, 否则返回null</returns>
        public SexyExtension<MonoBehaviour> FindGenericsExtensionByName(string name)
        {
            foreach (var extension in extensions)
            {
                if (extension.Name != name)
                    continue;
                if (extension is SexyExtension<MonoBehaviour> generics)
                    return generics;
            }
            return null;
        }

        /// <summary>
        /// 通过名称寻找泛型拓展
        /// </summary>
        /// <param name="name">拓展名称</param>
        /// <returns>如果寻找到了拓展则返回拓展, 否则返回null</returns>
        public IEnumerable<SexyExtension<MonoBehaviour>> FindGenericsExtensionsByName(string name)
        {
            foreach (var extension in extensions)
            {
                if (extension.Name != name)
                    continue;
                if (extension is SexyExtension<MonoBehaviour> generics)
                    yield return generics;
            }
            yield break;
        }
        #endregion
        #region FindID
        /// <summary>
        /// 通过名称寻找拓展
        /// </summary>
        /// <param name="id">拓展名称</param>
        /// <returns>如果寻找到了拓展则返回拓展, 否则返回null</returns>
        public SexyExtension FindID(string id)
        {
            foreach (var extension in extensions)
            {
                if (extension.Id == id)
                    return extension;
            }
            return null;
        }

        /// <summary>
        /// 通过名称寻找多个拓展
        /// </summary>
        /// <param name="id">拓展名称</param>
        /// <returns>所有寻找到的拓展, 如果没有找到则返回空列表</returns>
        public IEnumerable<SexyExtension> FindIDs(string id)
        {
            foreach (var extension in extensions)
            {
                if (extension.Id == id)
                    yield return extension;
            }
            yield break;
        }

        /// <summary>
        /// 通过名称寻找泛型拓展
        /// </summary>
        /// <param name="id">拓展名称</param>
        /// <returns>如果寻找到了拓展则返回拓展, 否则返回null</returns>
        public SexyExtension<MonoBehaviour> FindGenericsByID(string id) => FindGenericsExtensionByID(id);

        /// <summary>
        /// 通过名称寻找泛型拓展
        /// </summary>
        /// <param name="id">拓展名称</param>
        /// <returns>如果寻找到了拓展则返回拓展, 否则返回null</returns>
        public SexyExtension<MonoBehaviour> FindGenericsExtensionByID(string id)
        {
            foreach (var extension in extensions)
            {
                if (extension.Id != id)
                    continue;
                if (extension is SexyExtension<MonoBehaviour> generics)
                    return generics;
            }
            return null;
        }

        /// <summary>
        /// 通过名称寻找泛型拓展
        /// </summary>
        /// <param name="id">拓展名称</param>
        /// <returns>如果寻找到了拓展则返回拓展, 否则返回null</returns>
        public IEnumerable<SexyExtension<MonoBehaviour>> FindGenericsExtensionsByID(string id)
        {
            foreach (var extension in extensions)
            {
                if (extension.Id != id)
                    continue;
                if (extension is SexyExtension<MonoBehaviour> generics)
                    yield return generics;
            }
            yield break;
        }
        #endregion

        public static readonly ExtensionsManager Instance = new ExtensionsManager();

        public SexyExtension this[string name] => FindName(name);

        public SexyExtension this[int index] => extensions[index];

        public class ExtensionCollection : ObservableCollection<SexyExtension>
        {
            internal new void Add(SexyExtension extension)
            {
                base.Add(extension);
                onAdded?.Invoke(extension);
            }

            internal new void Clear()
            {
                base.Clear();
            }

            public new void CopyTo(SexyExtension[] array, int index)
            {
                base.CopyTo(array, index);
            }

            public new bool Contains(SexyExtension item)
            {
                return base.Contains(item);
            }

            public new IEnumerator<SexyExtension> GetEnumerator()
            {
                return base.GetEnumerator();
            }

            public new int IndexOf(SexyExtension item)
            {
                return base.IndexOf(item);
            }

            internal new void Insert(int index, SexyExtension item)
            {
                base.Insert(index, item);
            }

            internal new void Remove(SexyExtension extension)
            {
                base.Remove(extension);
            }

            internal new void RemoveAt(int index)
            {
                base.RemoveAt(index);
            }

            public new SexyExtension this[int index]
            {
                get
                {
                    if (extensions.Count > index)
                    {
                        return extensions[index];
                    }
                    return null;
                }
                internal set => base[index] = value;
            }


            internal event Action<SexyExtension> onAdded;
        }
    }
}
