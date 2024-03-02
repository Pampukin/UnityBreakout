using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private PauseCanvas _pauseCanvas;

    private GameObject _pauseobject;

    private void Start()
    {
        StageManager.INSTANCE.PauseAction = () => { _pauseobject = Instantiate(_pauseCanvas,this.transform).gameObject; };
        StageManager.INSTANCE.ResumeAction = () =>
        {
            if (_pauseobject != null)
            {
                Destroy(_pauseobject);
            }
        };
    }
}
