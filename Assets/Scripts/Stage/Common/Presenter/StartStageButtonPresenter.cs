using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Zenject;

public class StartStageButtonPresenter : MonoBehaviour
{
    [Inject] private GameStateManager gameStateManager;
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject startUIParent;

    // Start is called before the first frame update
    void Start()
    {
        startButton.OnClickAsObservable().Subscribe(_ =>
        {
            gameStateManager.OnStartButtonPushed();
            startUIParent.SetActive(false);
        });        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
