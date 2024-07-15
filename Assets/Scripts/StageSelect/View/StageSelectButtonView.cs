using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class StageSelectButtonView : MonoBehaviour
{
    [SerializeField] private int stageNumber;
    public Subject<int> OnPushed = new Subject<int>();

    void Start()
    {
        GetComponent<Button>()
            .OnClickAsObservable()
            .Select(_ => stageNumber)
            .Subscribe(OnPushed);
    }
}
