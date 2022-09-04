using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rigi : MonoBehaviour
{
    private void Awake()
    {
        int level;
        if (PlayerPrefs.HasKey("Level"))
        {
            level = PlayerPrefs.GetInt("Level");
        }
        else
        {
            PlayerPrefs.SetInt("Level", 1);
            level = 1;
        }
        //PlayerPrefs.SetInt("TotalLevelText", 10);
        SceneManager.LoadScene(level);
        print(level);
    }
}