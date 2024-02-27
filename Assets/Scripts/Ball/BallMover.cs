using System;
using System.Collections.Generic;
using UnityEngine;

public class BallMover : MonoBehaviour, IHit
{
    private Rigidbody2D _rb;
    
    private Vector3 _moveVector;

    [SerializeField]
    private float _moveSpeed = 5;

    private Vector3 _hitPos;

    private Vector3 _reflectVector;

    private bool _hasHit = true;

    [SerializeField]
    private Crash _crash;

    private List<GameObject> _crashObjects = new List<GameObject>();
    
    private Collision2D _col;

    private void Awake()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _moveVector = Vector3.up;
    }
    
    private void FixedUpdate()
    {
        _rb.velocity = _moveSpeed * _moveVector.normalized;
    }

    /// <summary>
    /// 物体ごとの反射処理
    /// 別の書き方できそう
    /// </summary>
    public void Hit()
    {
        switch (_col.gameObject.tag)
        {
            case "Ball":
                _moveVector = _reflectVector;
                break;
            
            case "Racket":
                var racket = _col.gameObject.GetComponent<Racket>();
                if (racket == null) return;

                _moveVector = new Vector3(_hitPos.x - racket.transform.position.x, racket.transform.position.y + racket.Height / 2 - racket.transform.position.y,0);
                // var reflectDirection = (Vector3)_col.contacts[0].point - _col.gameObject.transform.position;
                // _moveVector = Vector2.Reflect(_moveVector, reflectDirection.normalized);
                //
                break;
            
            default:
                if (Vector2.Dot(_reflectVector.normalized, _moveVector) < 0)
                {
                    _moveVector = Vector2.Reflect(_moveVector, _reflectVector);
                }
                break;
        }
    }

    /// <summary>
    /// 反射イベント
    /// </summary>
    /// <param name="col"></param>
    private void _Reflect(Collision2D col)
    {
        _col = col;
        _reflectVector = col.contacts[0].normal;
        Hit();
    }

    /// <summary>
    /// 接触点の生成
    /// </summary>
    /// <param name="col"></param>
    private void _SetCrash(Collision2D col)
    {
        var crash = Instantiate(_crash, col.contacts[0].point, Quaternion.identity, this.transform);
        _crashObjects.Add(crash.gameObject);
        crash.MyObject = this.gameObject;
        crash.OtherObject = col.gameObject;
    }

    /// <summary>
    /// 接触点の更新
    /// </summary>
    /// <param name="col"></param>
    private void _RenewCrash(Collision2D col)
    {
        _crashObjects[^1].transform.position = col.contacts[0].point;
    }

    /// <summary>
    /// 接触点の削除
    /// </summary>
    private void _ResetCrash()
    {
        foreach (var crashObject in _crashObjects)
        {
            Destroy(crashObject);
        }
        _crashObjects.Clear();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _hitPos = col.contacts[0].point;
        _SetCrash(col);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        _hitPos = col.contacts[0].point;
        if (_crashObjects.Count != 0)
        {
            _RenewCrash(col);
            
            _Reflect(col);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        _hitPos = Vector3.zero;
        if (_crashObjects.Count != 0)
        {
            _ResetCrash();
        }
    }
}
