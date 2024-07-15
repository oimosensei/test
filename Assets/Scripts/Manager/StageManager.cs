using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{

    public int currentStage = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToStageScene(int stageNumber)
    {
        currentStage = stageNumber;
        string stageName = "Stage" + stageNumber.ToString();
        SceneManager.LoadScene(stageName);
    }
}
