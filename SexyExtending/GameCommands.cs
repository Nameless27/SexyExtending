using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SexyExtending.GameParameters;

namespace SexyExtending
{
    public class GameCommands
    {
        public static void Restart()
        {
            PlayerPrefs.DeleteKey("NumSaves");
            PlayerPrefs.DeleteKey("SaveGame0");
            PlayerPrefs.DeleteKey("SaveGame1");
            PlayerPrefs.DeleteKey("targetScene");
            PlayerPrefs.Save();
            SceneManager.LoadScene("Mian");
        }

        public static void LoadScene(string name, LoadSceneMode mode = LoadSceneMode.Single)
        {
            PlayerPrefs.DeleteKey("NumSaves");
            PlayerPrefs.DeleteKey("SaveGame0");
            PlayerPrefs.DeleteKey("SaveGame1");
            PlayerPrefs.DeleteKey("targetScene");
            PlayerPrefs.Save();
            SceneManager.LoadScene(name, mode);
        }

        public static void Teleport(Vector3 position) => Teleport(player, position);

        public static void Teleport(Vector3 position, Quaternion rotation) => Teleport(player, position, rotation);

        public static void Teleport(GameObject gameObject, Vector3 position) => Teleport(gameObject, position, gameObject.transform.rotation);

        public static void Teleport(GameObject gameObject, Vector3 position, Quaternion rotation)
        {
            if (gameObject == player)
            {
                Vector3 b = Camera.main.transform.position - player.transform.position;
                Vector3 b2 = cursor.transform.position - player.transform.position;
                player.transform.position = position;
                player.transform.rotation = rotation;
                Camera.main.transform.position = player.transform.position + b;
                cursor.transform.position = player.transform.position + b2;
            }
        }
    }
}
