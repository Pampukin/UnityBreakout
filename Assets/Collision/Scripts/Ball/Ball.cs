using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StageManager.INSTANCE.PreAction = () => _rb.bodyType = RigidbodyType2D.Static;
        StageManager.INSTANCE.StartAction = () => _rb.bodyType = RigidbodyType2D.Dynamic;
        StageManager.INSTANCE.ClearAction = () => _rb.bodyType = RigidbodyType2D.Static;
        StageManager.INSTANCE.PauseAction = () => _rb.bodyType = RigidbodyType2D.Static;
        StageManager.INSTANCE.ResumeAction = () => _rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
