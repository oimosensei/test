using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

public enum GameState
    {
        Ready,
        Tutorial,
        Playing,
        Paused,
        Finish,
        GameOver,
    }

public class GameStateManager : MonoBehaviour
{
    [Inject] private StageManager stageManager;
    public ReactiveProperty<GameState> CurrentGameState = new ReactiveProperty<GameState>(GameState.Ready);
    // Start is called before the first frame update
    async void Start()
    {
        CurrentGameState.Subscribe(async state =>
        {
            switch (state)
            {
                case GameState.Ready:
                    Debug.Log("Ready");
                    Time.timeScale = 0;
                    break;
                case GameState.Tutorial:
                    Debug.Log("Tutorial");
                    Time.timeScale = 0;
                    break;
                case GameState.Playing:
                    Debug.Log("Playing");
                    Time.timeScale = 1;
                    break;
                case GameState.Paused:
                    Debug.Log("Paused");
                    Time.timeScale = 0;
                    break;
                case GameState.Finish:
                    Debug.Log("Finish");
                    Time.timeScale = 1;
                    break;
                case GameState.GameOver:
                    Debug.Log("GameOver");
                    await UniTask.Delay(3000);
                    stageManager.GoToStageScene(stageManager.currentStage);
                    break;

            }
        });        
    }


    public void OnStartButtonPushed()
    {
        if(CurrentGameState.Value == GameState.Ready)
        {
            CurrentGameState.Value = GameState.Playing;
        }
    }

    public void OnPauseButtonPushed()
    {
        if (CurrentGameState.Value == GameState.Playing)
        {
            CurrentGameState.Value = GameState.Paused;
        }
    }

    public void OnResumeButtonPushed()
    {
        if (CurrentGameState.Value == GameState.Paused)
        {
            CurrentGameState.Value = GameState.Playing;
        }
    }

    public void OnTutorialStarted()
    {
        if (CurrentGameState.Value == GameState.Playing)
        {
            CurrentGameState.Value = GameState.Tutorial;
        }
    }

    public void OnTutorialFinished()
    {
        if (CurrentGameState.Value == GameState.Tutorial)
        {
            CurrentGameState.Value = GameState.Playing;
        }
    }

    public void OnGameFinished()
    {
        if (CurrentGameState.Value == GameState.Playing)
        {
            CurrentGameState.Value = GameState.Finish;
        }
    }
}
