using System.Collections.Generic;
using UnityEngine;

public class BallMover : MonoBehaviour, IHit
{
    private Rigidbody2D _rb;

    /// <summary>
    /// ボールの速さ
    /// </summary>
    private Vector3 _moveVelocity
    {
        get
        {
            return _moveSpeed * _reflectVector.normalized;
        }
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
    
    private Collision2D _col;

    private void Awake()
    {
        _rb = this.GetComponent<Rigidbody2D>();
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
                _reflectVector =  this.transform.position - _hitPos;
                break;
            
            case "Racket":
                var racket = _col.gameObject.GetComponent<Racket>();
                if (racket == null) return;

                _SetReflectVector(_hitPos.x - racket.transform.position.x, racket.Height / 2, 0);
                break;
            
            default:
                if (Vector2.Dot(_col.contacts[0].normal.normalized, _moveVelocity) < 0)
                {
                    _reflectVector = Vector2.Reflect(_moveVelocity, _col.contacts[0].normal.normalized);
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
        _col = col;
        _hitPos = col.contacts[0].point;
        
        _SetCrash(col);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        _col = col;
        _hitPos = col.contacts[0].point;
        
        if (_crashObjects.Count != 0)
        {
            _RenewCrash(col);

            Hit();
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        _col = col;
        if (_crashObjects.Count != 0)
        {
            _ResetCrash();
        }
        _hitPos = Vector3.zero;
    }
}
