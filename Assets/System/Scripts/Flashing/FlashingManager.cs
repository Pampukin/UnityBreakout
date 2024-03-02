using System;
using UnityEngine;

public abstract class FlashingManager : MonoBehaviour
{
    [SerializeField]
    protected FlashingCanvas _flashingCanvas;

    protected GameObject _flashingObject;

    private void Start()
    {
        SetFlashing();
    }

    public abstract void SetFlashing();
}
