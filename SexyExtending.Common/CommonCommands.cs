using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SexyExtending
{
    public class CommonCommands : GameCommands
    {
        public static void Finish(bool addNumWins = true)
        {
            MonoBehaviour.print("Finished");
            PlayerPrefs.SetFloat("LastTime", UnityEngine.Object.FindObjectOfType<Narrator>().timePlayedThisGame);
            PlayerPrefs.DeleteKey("NumSaves");
            PlayerPrefs.DeleteKey("SaveGame0");
            PlayerPrefs.DeleteKey("SaveGame1");
            PlayerPrefs.DeleteKey("targetScene");
            if (addNumWins)
            {
                int num = PlayerPrefs.GetInt("NumWins");
                PlayerPrefs.SetInt("NumWins", num + 1);
            }
            Debug.Log("Game Done, deleting saves");
            PlayerPrefs.Save();
            SceneManager.LoadScene("Reward Loader");
        }
    }
}
