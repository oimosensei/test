using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Zenject;

public abstract class TutorialManagerBase : MonoBehaviour
{
    protected Subject<Unit> onTutorialStarted = new Subject<Unit>();
    protected Subject<Unit> onTutorialFinished = new Subject<Unit>();

    [Inject] protected GameStateManager gameStateManager;

    protected void Start()
    {
        onTutorialStarted.Subscribe(_ =>
        {
            gameStateManager.OnTutorialStarted();
            Debug.Log("Tutorial Started at TutorialManagerBase");
        });

        onTutorialFinished.Subscribe(_ =>
        {
            gameStateManager.OnTutorialFinished();
        });
    }

    protected abstract void SetTutorialObservables();
}
