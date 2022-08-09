using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace SexyExtending
{
	[XmlRoot("SaveState")]
	[Serializable]
	public class SaveState
	{
		[XmlElement("hingePos")]
		public float hingePos { get; set; }

		[XmlElement("hingeVel")]
		public float hingeVel { get; set; }

		[XmlElement("sliderPos")]
		public float sliderPos { get; set; }

		[XmlElement("sliderVel")]
		public float sliderVel { get; set; }

		[XmlElement("camPos")]
		public Vector3 camPos { get; set; }

		[XmlElement("playerPos")]
		public Vector3 playerPos { get; set; }

		[XmlElement("playerRot")]
		public Quaternion playerRot { get; set; }

		[XmlArray("rbLinearVelocities")]
		public Vector2[] rbLinearVelocities { get; set; }

		[XmlArray("rbAngularVelocities")]
		public float[] rbAngularVelocities { get; set; }

		[XmlArray("rbPositions")]
		public Vector2[] rbPositions { get; set; }

		[XmlArray("rbAngles")]
		public float[] rbAngles { get; set; }

		[XmlElement("saveNum")]
		public int saveNum { get; set; }

		[XmlElement("currentCondolence")]
		public int currentCondolence { get; set; }

		[XmlElement("currentObservation")]
		public int currentObservation { get; set; }

		[XmlElement("keyDialogDone")]
		public string keyDialogDone { get; set; }

		[XmlElement("condolenceDialogDone")]
		public string condolenceDialogDone { get; set; }

		[XmlElement("observationDialogDone")]
		public string observationDialogDone { get; set; }

		[XmlElement("timePlayed")]
		public float timePlayed { get; set; }

		[XmlElement("version")]
		public string version { get; set; }

		[XmlElement("speedrun")]
		public bool speedrun { get; set; }

		public SaveState()
		{
			hingePos = 0f;
			hingeVel = 0f;
			sliderPos = 0f;
			sliderVel = 0f;
			camPos = new Vector3(-43.57285f, -1.2893757f, -20f);
			playerPos = Vector3.zero;
			playerRot = Quaternion.identity;
			rbLinearVelocities = new Vector2[1];
			rbPositions = new Vector2[1];
			rbAngularVelocities = new float[1];
			rbAngles = new float[1];
			saveNum = -1;
			currentCondolence = 0;
			currentObservation = 0;
			keyDialogDone = "";
			condolenceDialogDone = "";
			observationDialogDone = "";
			timePlayed = 0f;
			version = "";
			speedrun = false;
			sidewaysEnabled = false;
			sidewaysAngle = 0f;
		}

		[XmlElement("sidewaysEnabled")]
		public bool sidewaysEnabled { get; set; }

		[XmlElement("sidewaysAngle")]
		public float sidewaysAngle { get; set; }
	}
}
