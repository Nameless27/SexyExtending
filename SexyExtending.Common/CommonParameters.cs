using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SexyExtending
{
    public class CommonParameters : GameParameters
    {
        internal static CameraControl mainCamera;
        public static CameraControl MainCamera
        {
            get
            {
                if (mainCamera == null)
                {
                    mainCamera = UnityEngine.Object.FindObjectOfType<CameraControl>();
                }
                return mainCamera;
            }
        }
    }
}
