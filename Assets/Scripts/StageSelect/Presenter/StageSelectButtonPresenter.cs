using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

public class StageSelectButtonPresenter : MonoBehaviour
{
    [SerializeField] private StageSelectButtonView[] stageSelectButtonViews;
    [Inject] private StageManager stageManager;
    // Start is called before the first frame update
    void Start()
    {
        stageSelectButtonViews.Select(x => x.OnPushed)
            .Merge()
            .Take(1)
            .Subscribe(stageNumber =>
            {
                Debug.Log($"Stage {stageNumber} is selected.");
                stageManager.GoToStageScene(stageNumber);
            });
    }
}
