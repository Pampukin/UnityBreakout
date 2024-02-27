using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    private Vector3 _scale;

    public float Height
    {
        get
        {
            return _scale.x;
        }
    }
    
    public float Width
    {
        get
        {
            return _scale.y;
        }
    }

    [SerializeField]
    private Ball _ball;

    private Vector3 _offset = new Vector3(0,0.28f,0);
    
    private void Awake()
    {
        _scale = this.transform.localScale;
    }

    private void Update()
    {
        if (StageManager.INSTANCE.StageState == StageState.Pre)
        {
            _ball.transform.position = this.transform.position + _offset;
        }
    }

    public void SetScale(Vector3 scale)
    {
        this.transform.localScale = scale;
        _scale = scale;
    }
}
