using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _score = 0;
    
    private static ScoreManager _instance;

    public static ScoreManager INSTANCE => _instance;
    
    private ScoreCanvas _scoreCanvas;

    [SerializeField]
    private ScoreDataBase _scoreDataBase;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        
        _scoreCanvas = this.GetComponentInChildren<ScoreCanvas>();
    }
    
    public void AddScore(MonoBehaviour hitObject)
    {
        _score += _scoreDataBase.GetScore(hitObject);
        _SetScore();
    }

    private void _SetScore()
    {
        _scoreCanvas.SetScore(_score);
    }
}
