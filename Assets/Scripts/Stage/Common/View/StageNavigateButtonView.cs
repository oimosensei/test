using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;


public enum StageNavigateButtonType
{
    Next,
    Again,
    StageSelect
}


public class StageNavigateButtonView : MonoBehaviour
{
    [SerializeField] private StageNavigateButtonType type = StageNavigateButtonType.Next;
    public Subject<StageNavigateButtonType> OnPushed = new Subject<StageNavigateButtonType>();

    void Start()
    {
        GetComponent<Button>()
            .OnClickAsObservable()
            .Select(_ => type)
            .Subscribe(OnPushed);
    }
}
