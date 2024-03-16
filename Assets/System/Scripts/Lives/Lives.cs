using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public int LifeNum
    {
        set
        {
            _lifeNum = value;
        }
    }
    
    private int _lifeNum;
    
    [SerializeField]
    private Life _life;
    
    [SerializeField]
    private List<Life> _lives = new List<Life>();

    private void Start()
    {
        _SetLives();
    }
    
    private void _SetLives()
    {
        for (int i = 0; i < _lifeNum + 1; i++)
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
    
    public void DecreaseLife()
    {
        if (_lives.Count == 0)
        {
            return;
        }
        
        var life = _lives[^1];
        
        _lives.Remove(life);
        life.Destroy();
        
        if (IsGameOver)
        {
            StateManager.INSTANCE?.GameOver();
        }
    }

    public bool IsGameOver => _lives.Count == 0;
    
}
