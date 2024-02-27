using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMoverTrigger : MonoBehaviour, IHit
{
    private Rigidbody2D _rb;

    private Transform _myTrans;

    /// <summary>
    /// ボールの速さ
    /// </summary>
    private Vector3 _moveVelocity
    {
        get { return _moveSpeed * _reflectVector.normalized; }
    }

    /// <summary>
    /// 速度
    /// </summary>
    [SerializeField]
    private float _moveSpeed = 5;

    /// <summary>
    /// 反射する方向
    /// </summary>
    private Vector3 _reflectVector;

    private Vector3 _hitPos;


    private bool _hasHit = true;

    [SerializeField]
    private Crash _crash;

    private List<GameObject> _crashObjects = new List<GameObject>();

    private Collider2D _col;

    private Ray2D _hitRay = new Ray2D();
    private RaycastHit2D _hit;

    private void Awake()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _myTrans = this.GetComponent<Transform>();
        _OnlyDebug();
    }

    private void _OnlyDebug()
    {
        _reflectVector = Vector3.up;
    }
    
    private void FixedUpdate()
    {
        _rb.velocity = _moveVelocity;
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
                _reflectVector = this.transform.position - _col.transform.position;
                break;

            case "Racket":
                var racket = _col.gameObject.GetComponent<Racket>();
                if (racket == null) return;

                _SetReflectVector(_hitPos.x - racket.transform.position.x, racket.Height / 2, 0);
                break;

            default:
                _SetHitRay(transform.position,_hitPos.normalized);
                _hit = Physics2D.Raycast(_hitRay.origin, _hitRay.direction, 1f); //Raycastを生成
                if (Vector2.Dot(_hit.normal, _moveVelocity) < 0)
                {
                    _reflectVector = Vector2.Reflect(_moveVelocity, _hit.normal);
                }
                if (_hit.collider)
                {
                    
                }
                break;
        }
    }

    private void _SetReflectVector(float x, float y, float z)
    {
        _reflectVector.x = x;
        _reflectVector.y = y;
        _reflectVector.z = z;
    }

    private void _SetHitRay(Vector2 origin, Vector2 direction)
    {
        _hitRay.origin = origin;
        _hitRay.direction = direction;
    }

    /// <summary>
    /// 接触点の生成
    /// </summary>
    /// <param name="col"></param>
    private void _SetCrash(Collider2D col)
    {
        var crash = Instantiate(_crash, col.ClosestPoint(_myTrans.position), Quaternion.identity, this.transform);
        _crashObjects.Add(crash.gameObject);
        crash.MyObject = this.gameObject;
        crash.OtherObject = col.gameObject;
    }

    /// <summary>
    /// 接触点の更新
    /// </summary>
    /// <param name="col"></param>
    private void _RenewCrash(Collider2D col)
    {
        _crashObjects[^1].transform.position = col.ClosestPoint(_myTrans.position);
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        _col = col;
        _hitPos = col.ClosestPoint(this.transform.position);
        Hit();
        
        _SetCrash(col);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        _col = col;
        _hitPos = col.ClosestPoint(_myTrans.position);
        Hit();
        
        if (_crashObjects.Count != 0)
        {
            _RenewCrash(col);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        _col = col;
        
        if (_crashObjects.Count != 0)
        {
            _ResetCrash();
        }
        _hitPos = Vector3.zero;
    }
    
}
