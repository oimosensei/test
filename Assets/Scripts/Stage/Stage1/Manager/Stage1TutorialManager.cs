using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Stage1TutorialManager : TutorialManagerBase
{
    [SerializeField] private FanGimmick fanGimmick;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        SetTutorialObservables();
    }

    protected override void SetTutorialObservables()
    {
        gameStateManager.CurrentGameState
            .Where(state => state == GameState.Playing)
            .First()
            .Delay(TimeSpan.FromSeconds(1))
            .AsUnitObservable()
            .Subscribe(onTutorialStarted);

        fanGimmick.IsClickedForTutorial
            .AsUnitObservable()
            .Subscribe(onTutorialFinished);
    }
}
