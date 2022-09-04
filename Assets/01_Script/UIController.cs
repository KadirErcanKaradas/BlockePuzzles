using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject levelCompPanel;
    [SerializeField] private GameObject retryButton;
    [SerializeField] private ParticleSystem levelCompPart;
    [SerializeField] private ParticleSystem levelCompPart1;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private int levelCount = 1;
  
    void Start()
    {
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
            levelText.text = "" + PlayerPrefs.GetInt("Level");
        }
        else
        {
            levelText.text = "" + PlayerPrefs.GetInt("Level");
        }

    }
    void Update()
    {
        
    }
    public void LevelComplated()
    {
        retryButton.SetActive(false);
        levelCompPanel.SetActive(true);
        levelCompPart.Play();
        levelCompPart1.Play();
    }
    public void NextLevelButton()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        levelText.text = PlayerPrefs.GetInt("Level").ToString();
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
        {
            int randomLevel = Random.Range(0, 5);
            SceneManager.LoadScene(randomLevel);
        }
       
    }
    public void NextButton()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        levelText.text = PlayerPrefs.GetInt("Level").ToString();
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
        {
            SceneManager.LoadScene(1);
            PlayerPrefs.SetInt("Level",1);
            levelText.text = PlayerPrefs.GetInt("Level").ToString();
        }
    }
    public void BackButton()
    {
        
        //if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        if(PlayerPrefs.GetInt("Level")==1)
        {
            SceneManager.LoadScene(1);
            PlayerPrefs.SetInt("Level", 1);
            levelText.text = PlayerPrefs.GetInt("Level").ToString();
        }
        else if (PlayerPrefs.GetInt("Level")>1)
        {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") - 1);
            levelText.text = PlayerPrefs.GetInt("Level").ToString();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LevelText()
    {
        levelText.text = (SceneManager.GetActiveScene().buildIndex + 1).ToString();
    }
}
