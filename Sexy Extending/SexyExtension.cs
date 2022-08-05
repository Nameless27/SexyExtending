using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SexyExtending
{
    public abstract class SexyExtension : IExtensionInfo
    {
        internal static readonly Type Type = typeof(SexyExtension);

        public abstract string Id { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string Author { get; }
        public abstract string Version { get; }
        public abstract string Link { get; }

        public virtual void OnExtensionLoaded()
        {
            
        }

        public virtual void OnSceneChanged(Scene scene)
        {

        }
    }

    public abstract class SexyExtension<T> : SexyExtension where T : MonoBehaviour
    {
        internal T behaviourInstance;
        public T BehaviourInstance => behaviourInstance;

        internal GameObject behaviourGameObject;
        public GameObject BehaviourGameObject => behaviourGameObject;

        public abstract Scenes BehaviourScenes { get; }

        public override void OnSceneChanged(Scene scene)
        {
            if (BehaviourScenes.HasFlag(GameParameters.currentSceneEnum))
                CreateBehaviour();
        }

        internal void CreateBehaviour()
        {
            if (behaviourInstance != null)
                return;
            var name = string.Format("{0}({1}).Behaviour", Name, Id);
            var gameObject = UnityEngine.Object.Instantiate(new GameObject(name));
            var behaviour = gameObject.AddComponent<T>();
            behaviourInstance = behaviour;
            behaviourGameObject = gameObject;
        }
    }
}
