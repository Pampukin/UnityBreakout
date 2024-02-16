using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMover : MonoBehaviour, IHit
{
    private Rigidbody2D _rb;
    
    private Vector3 _moveVector;
    
    [SerializeField]
    private float _moveSpeed = 5;

    private Vector3 _hitPos;
    
    private void Awake()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _moveVector = Vector3.up;
    }

    private void Start()
    {
        _DebugMover();
    }

    private void _DebugMover()
    {
        _rb.velocity = _moveSpeed * _moveVector.normalized;
    }

    private void FixedUpdate()
    {
        _rb.position += (Vector2)(_moveSpeed * _moveVector.normalized);
    }

    /// <summary>
    /// 反射処理
    /// </summary>
    public void Hit()
    {
        Vector3 preMoveVector = _moveVector;
        _moveVector = Vector2.Reflect(preMoveVector, _hitPos);
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _hitPos = col.contacts[0].normal;
        Hit();
    }
}
