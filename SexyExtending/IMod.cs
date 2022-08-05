using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SexyExtending
{
    public interface IMod
	{
		bool Load();

		bool UnLoad();

		string name { get; }

		string version { get; }

		string author { get; }

		string web { get; }

		string Update(float deltaTime, GameObject player);

		List<string> options { get; }

		int option { get; set; }

		bool enabled { get; set; }

		string optionsTitle { get; set; }

		bool enableSplits { get; set; }

		string FixedUpdate(float deltaTime, GameObject player);

		string onGUI(float deltaTime, GameObject player);
	}
}
