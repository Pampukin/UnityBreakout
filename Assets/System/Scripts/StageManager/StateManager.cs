using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager INSTANCE => _instance;
    private static StateManager _instance;

    private bool _canPause => StageManager.INSTANCE.StageState != StageState.Clear;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

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

    private void _Clear()
    {
        StageManager.INSTANCE.SetStageState(StageState.Clear);
    }

    public void AllBlocksDestroyed()
    {
        _Clear();
    }

    private void _Pause()
    {
        if (Input.GetKeyDown(KeyCode.R) && _canPause)
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

    private void _Over()
    {
        StageManager.INSTANCE.SetStageState(StageState.Over);
    }

    public void GameOver()
    {
        _Over();
    }
    
}
