using System;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
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

    public void DecreaseLife()
    {
        _lives.DecreaseLife();
    }
}
