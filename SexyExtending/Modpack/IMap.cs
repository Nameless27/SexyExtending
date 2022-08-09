using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SexyExtending.Modpack
{
    public interface IMap
    {
		bool Load();

		bool UnLoad();

		string name { get; }

		string version { get; }

		string author { get; }

		string web { get; }

		string Update(float deltaTime, GameObject player);

		Dictionary<string, Rect> splits { get; }

		Dictionary<string, Rect> splitsSideways { get; }

		Dictionary<string, SaveState> teleportSaveStates { get; }

		string FixedUpdate(float deltaTime, GameObject player);

		string onGUI(float deltaTime, GameObject player);
	}
}
