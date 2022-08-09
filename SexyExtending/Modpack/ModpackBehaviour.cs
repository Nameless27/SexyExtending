using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SexyExtending.Modpack
{
    public class ModpackBehaviour : MonoBehaviour
    {
        internal static readonly Type Type = typeof(ModpackBehaviour);

        public IMod mod;

		void OnEnable()
        {
            if (mod == null)
                return;
            mod.Load();
        }

		void OnDisable()
        {
            if (mod == null)
                return;
            mod.UnLoad();
        }

		void Update()
        {
            if (mod == null)
                return;
            mod.Update(Time.deltaTime, GameParameters.player);
        }

		void FixedUpdate()
        {
            if (mod == null)
                return;
            mod.FixedUpdate(Time.deltaTime, GameParameters.player);
        }

		void OnGUI()
        {
            if (mod == null)
                return;
            mod.onGUI(Time.deltaTime, GameParameters.player);
        }
	}
}
