using System;
using UnityEngine;
using UnityEngine.Events;

public class StageManager : MonoBehaviour
{
    private static StageManager _instance;

    public static StageManager INSTANCE => _instance;
 
    private bool _isStateChanged = false;
    
    private StageState _preState = StageState.None;

    public StageState StageState => _stageState;


    private StageState _stageState = StageState.None;

    private void _SetStageState(StageState state)
    {
        _isStateChanged = true;
        _stageState = state;
    }
    
    public UnityAction PreAction
    {
        set
        {
            _preAction += value;
        }
    }
    
    private UnityAction _preAction;

    public UnityAction StartAction
    {
        set
        {
            _startAction += value;
        }
    }
    private UnityAction _startAction;

    public UnityAction PlayingAction
    {
        set
        {
            _playingAction += value;
        }
    }
    private UnityAction _playingAction;

    public UnityAction EndAction
    {
        set
        {
            _endAction += value;
        }
    }
    private UnityAction _endAction;
    
    public UnityAction PauseAction
    {
        set
        {
            _pauseAction += value;
        }
    }
    private UnityAction _pauseAction;
    
    public UnityAction ResumeAction
    {
        set
        {
            _resumeAction += value;
        }
    }
    private UnityAction _resumeAction;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        _SetStageState(StageState.Pre);
    }

    private void Update()
    {
        //操作系統は後々移動
        if (_stageState == StageState.Pre && Input.GetKeyDown(KeyCode.Space))
        {
            _SetStageState(StageState.Start);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_stageState != StageState.Pause)
            {
                _SetStageState(StageState.Pause);
            }
            else
            {
                _SetStageState(StageState.Resume);
            }
            
        }
        
        if (_isStateChanged)
        {
            switch (_stageState)
            {
                case StageState.Pre:
                    _Pre();
                    break;
                case StageState.Start:
                    _Start();
                    break;
                case StageState.Playing:
                    _Playing();
                    break;
                case StageState.End:
                    _End();
                    break;
                case StageState.Pause:
                    _Pause();
                    break;
                case StageState.Resume:
                    _Resume();
                    break;
                default:
                    Debug.LogError("ステートエラー");
                    break;
            }
        }
    }

    private void _Pre()
    {
        Debug.Log("Pre");
        _preAction?.Invoke();
        _preState = StageState.Pre;
        _isStateChanged = false;
    }

    private void _Start()
    {
        Debug.Log("Start");
        _startAction?.Invoke();
        _preState = StageState.Start;
        _SetStageState(StageState.Playing);
        _isStateChanged = false;
    }

    private void _Playing()
    {
        Debug.Log("Playing");
        _playingAction?.Invoke();
        _preState = StageState.Playing;
        _isStateChanged = false;
    }

    private void _End()
    {
        Debug.Log("End");
        _endAction?.Invoke();
        _preState = StageState.End;
        _isStateChanged = false;
    }

    private void _Pause()
    {
        Debug.Log("Pause");
        Time.timeScale = 0;
        _pauseAction?.Invoke();
        _isStateChanged = false;
    }

    private void _Resume()
    {
        Debug.Log("Resume");
        Time.timeScale = 1;
        _resumeAction?.Invoke();
        _SetStageState(_preState);
        Debug.Log(_stageState);
        _isStateChanged = false;
    }
}
