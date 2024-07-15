using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using UnityEngine.SceneManagement;
using Zenject;

public class ResultUIPresenter : MonoBehaviour
{
    [SerializeField] private GameObject resultUIParent;
    [SerializeField] private StageNavigateButtonView[] stageNavigateButtonViews;
    [Inject] private StageManager stageManager;
    [Inject] private GameStateManager gameStateManager;
    // Start is called before the first frame update
    void Start()
    {
        stageNavigateButtonViews.Select(x => x.OnPushed)
            .Merge()
            .Take(1)
            .Subscribe(type =>
            {
                int currentStage = stageManager.currentStage;
                switch (type)
                {
                    case StageNavigateButtonType.Next:
                        Debug.Log("Next button is pushed.");
                        stageManager.GoToStageScene(currentStage+1);
                        break;
                    case StageNavigateButtonType.Again:
                        Debug.Log("Again button is pushed.");
                        stageManager.GoToStageScene(currentStage);
                        break;
                    case StageNavigateButtonType.StageSelect:
                        Debug.Log("StageSelect button is pushed.");
                        SceneManager.LoadScene("StageSelect");
                    break;
                }
            });
        
        gameStateManager.CurrentGameState.Subscribe(state =>
        {
            if (state == GameState.Finish)
            {
                resultUIParent.SetActive(true);
            }
            else
            {
                resultUIParent.SetActive(false);
            }
        });

    }
}
