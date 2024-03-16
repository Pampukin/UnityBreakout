using System;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    [SerializeField]
    private int _lifeNum = 3;

    public bool IsGameOver => _lives.IsGameOver;

    private static LivesManager _instance;

    public static LivesManager INSTANCE => _instance;

    private Lives _lives;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        _lives = GetComponentInChildren<Lives>();
    }

    private void Start()
    {
        _lives.LifeNum = _lifeNum;
    }

    public void DecreaseLife()
    {
        _lives.DecreaseLife();
    }
}
