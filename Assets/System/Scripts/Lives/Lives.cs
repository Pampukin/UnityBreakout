using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    [SerializeField]
    private int _lifeNum = 3;
    
    [SerializeField]
    private Life _life;
    
    [SerializeField]
    private List<Life> _lives = new List<Life>();

    private void Start()
    {
        _SetLives();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _IncreaseLife();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            _DeleteLife();
        }
    }

    private void _SetLives()
    {
        for (int i = 0; i < _lifeNum; i++)
        {
            var life = Instantiate(_life, this.transform);
            _lives.Add(life);
        }
    }
    
    private void _ResetLivesList()
    {
        foreach (var life in GetComponentsInChildren<Life>())
        {
            _lives.Add(life);
        }
    }

    private void _IncreaseLife()
    {
        var life = Instantiate(_life, this.transform);
        _lives.Add(life);
    }
    
    private void _DeleteLife()
    {
        if (_lives.Count == 0)
        {
            return;
        }
        
        var life = _lives[^1];
        
        _lives.Remove(life);
        life.Destroy();
        
        if (_isClear())
        {
            StateManager.INSTANCE?.GameOver();
        }
    }

    private bool _isClear()
    {
        return _lives.Count == 0;
    }
}
