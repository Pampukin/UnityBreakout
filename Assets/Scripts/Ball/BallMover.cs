using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallMover : MonoBehaviour, IHit
{
    private Rigidbody2D _rb;

    [SerializeField]
    private Vector3 _moveVector;

    [SerializeField]
    private float _moveSpeed = 5;

    private Vector3 _hitPos;

    private bool _hasHit = true;

    [SerializeField]
    private Crash _crash;

    private List<GameObject> _crashObjects = new List<GameObject>();
    
    private Collision2D _col;

    private void Awake()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _moveVector = Vector3.up;
        _rb.velocity = _moveSpeed * _moveVector.normalized;
    }
    
    private void FixedUpdate()
    {
        _rb.velocity = _moveSpeed * _moveVector.normalized;
    }

    /// <summary>
    /// 反射の速度処理
    /// </summary>
    public void Hit()
    {
        if (!_col.gameObject.CompareTag("Ball"))
        {
            if (Vector2.Dot(_hitPos.normalized, _moveVector) < 0)
            {
                Vector3 preMoveVector = _moveVector;
                _moveVector = Vector2.Reflect(preMoveVector, _hitPos);
            }
        }
        else
        {
            _moveVector = _hitPos;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _col = col;
        _hitPos = col.contacts[0].normal;
        Hit();
        var crash = Instantiate(_crash, col.contacts[0].point, Quaternion.identity, this.transform);
        _crashObjects.Add(crash.gameObject);
        crash.MyObject = this.gameObject;
        crash.OtherObject = col.gameObject;
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (_crashObjects.Count != 0)
        {
            _crashObjects[^1].transform.position = col.contacts[0].point;
            _hitPos = col.contacts[0].normal;
            Hit();
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (_crashObjects.Count != 0)
        {
            foreach (var crashObject in _crashObjects)
            {
                Destroy(crashObject);
            }
            _crashObjects.Clear();
        }
    }
}
