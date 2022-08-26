using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public event EventHandler<OnGameStageChangedEventArgs> OnGameStageChanged;

    public static GameController Instance { get; private set; }
    public GameStage GameStage { get; private set; }


    [SerializeField] private PlayerController playerController;
    [SerializeField] private UIController uiController;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SetGameStage(GameStage.Loaded);
    }

    public void SetGameStage(GameStage gameStage)
    {
        GameStage = gameStage;

        OnGameStageChanged?.Invoke(this, new OnGameStageChangedEventArgs { gameStage = gameStage });

    }

    public class OnGameStageChangedEventArgs : EventArgs
    {
        public GameStage gameStage;
    }
}

public enum GameStage { NotLoaded, Loaded, Started, Win, Fail }
