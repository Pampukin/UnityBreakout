using System.Collections.Generic;
using UnityEngine;

public class BallMover : MonoBehaviour, IHit
{
    private Rigidbody2D _rb;
    
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
                _moveVector = _hitPos;
                break;
            
            default:
                if (Vector2.Dot(_hitPos.normalized, _moveVector) < 0)
                {
                    Vector3 preMoveVector = _moveVector;
                    _moveVector = Vector2.Reflect(preMoveVector, _hitPos);
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
        _hitPos = col.contacts[0].normal;
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
        _SetCrash(col);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (_crashObjects.Count != 0)
        {
            _RenewCrash(col);
            
            _Reflect(col);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (_crashObjects.Count != 0)
        {
            _ResetCrash();
        }
    }
}
