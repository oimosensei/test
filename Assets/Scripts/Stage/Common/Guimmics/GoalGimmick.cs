using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

public class GoalGimmick : MonoBehaviour
{
    [Inject] private GameStateManager gameStateManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Debug.Log("Goal!!!");
            gameStateManager.OnGameFinished();
        }
    }
}
