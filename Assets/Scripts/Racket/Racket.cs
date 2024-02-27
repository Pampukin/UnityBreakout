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

    private void Awake()
    {
        _scale = this.transform.localScale;
    }

    public void SetScale(Vector3 scale)
    {
        this.transform.localScale = scale;
        _scale = scale;
    }
}
