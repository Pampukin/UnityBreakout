using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    void Start()
    {
        StageManager.INSTANCE.SetStageState(StageState.Pre);
    }

    void Update()
    {
        _Start();
        _Pause();
    }

    private void _Start()
    {
        if (StageManager.INSTANCE.StageState == StageState.Pre && Input.GetKeyDown(KeyCode.Space))
        {
            StageManager.INSTANCE.SetStageState(StageState.Start);
        }
    }

    private void _Pause()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (StageManager.INSTANCE.StageState != StageState.Pause)
            {
                StageManager.INSTANCE.SetStageState(StageState.Pause);
            }
            else
            {
                StageManager.INSTANCE.SetStageState(StageState.Resume);
            }
            
        }
    }
}
