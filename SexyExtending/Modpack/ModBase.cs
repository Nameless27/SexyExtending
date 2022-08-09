using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SexyExtending.Modpack
{
    public abstract class ModBase : SexyExtension, IMod
    {
        public void LoadMod()
        {
            enabled = true;
        }

        public void UnloadMod()
        {
            enabled = false;
        }

        #region IMod
        public abstract string name { get; }

        public abstract string version { get; }

        public abstract string author { get; }

        public abstract string web { get; }

        public abstract List<string> options { get; }

        public abstract int option { get; set; }

        public abstract bool enabled { get; set; }

        public abstract string optionsTitle { get; set; }

        public abstract bool enableSplits { get; set; }

        public abstract string FixedUpdate(float deltaTime, GameObject player);

        public abstract bool Load();

        public abstract string onGUI(float deltaTime, GameObject player);

        public abstract bool UnLoad();

        public abstract string Update(float deltaTime, GameObject player);
        #endregion
        #region SexyExtension
        #region IExtensionInfo
        public override string Id => string.Format("{0}.{1}", author, name);

        public override string Name => name;

        public abstract override string Description { get; }

        public override string Author => author;

        public override string Version => version;

        public override string Link => web;
        #endregion

        public override void OnSceneChanged(Scene scene)
        {
            if (enabled)
            {
                var name = string.Format("{0}({1}).Behaviour", Name, Id);
                var gameObject = UnityEngine.Object.Instantiate(new GameObject(name));
                var behaviour = gameObject.AddComponent<ModpackBehaviour>();
                behaviour.mod = this;
                BehaviourObject = gameObject;
                Behaviour = behaviour;
            }
            else
            {
                UnityEngine.Object.Destroy(Behaviour);
                UnityEngine.Object.Destroy(BehaviourObject);
            }
        }
        #endregion

        protected GameObject BehaviourObject { get; private set; }
        protected ModpackBehaviour Behaviour { get; private set; }
    }
}
