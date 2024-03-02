using System;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StageManager.INSTANCE.PreAction += DisableRigidbody;
        StageManager.INSTANCE.StartAction +=  AbleRigidbody;
        StageManager.INSTANCE.ClearAction +=  DisableRigidbody;
        StageManager.INSTANCE.PauseAction +=  DisableRigidbody;
        StageManager.INSTANCE.ResumeAction +=  AbleRigidbody;
    }
    
    private void OnDestroy()
    {
        StageManager.INSTANCE.PreAction -= DisableRigidbody;
        StageManager.INSTANCE.StartAction -=  AbleRigidbody;
        StageManager.INSTANCE.ClearAction -= DisableRigidbody;
        StageManager.INSTANCE.PauseAction -=  DisableRigidbody;
        StageManager.INSTANCE.ResumeAction -=  AbleRigidbody;
    }
    
    private void AbleRigidbody()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
    }
    
    private void DisableRigidbody()
    {
        _rb.bodyType = RigidbodyType2D.Static;
    }



}
