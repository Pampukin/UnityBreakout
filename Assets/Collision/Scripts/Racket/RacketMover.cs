using System;
using UnityEngine;

public class RacketMover : MonoBehaviour
{
    private Rigidbody2D _rb;

    private Vector2 _moveVector;

    [SerializeField]
    private float _moveSpeed = 3;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _moveVector = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        _GetSpeed();
    }

    /// <summary>
    /// 移動速度の設定
    /// </summary>
    private void _GetSpeed()
    {
        _moveVector.x = Input.GetAxisRaw("Horizontal");
        _rb.velocity = _moveSpeed * _moveVector;
    }
    
}
