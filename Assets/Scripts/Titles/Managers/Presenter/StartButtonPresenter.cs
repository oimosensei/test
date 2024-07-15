using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class StartButtonPresenter : MonoBehaviour
{
    [SerializeField] private TitleManager titleManager;
    [SerializeField] private Button startButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton.OnClickAsObservable().Subscribe(_ => titleManager.GoToStageSelectScene()).AddTo(this); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
