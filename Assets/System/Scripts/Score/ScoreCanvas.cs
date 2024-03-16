using UnityEngine;

public class ScoreCanvas : MonoBehaviour
{
    private ScoreText _scoreText;

    private void Awake()
    {
        _scoreText = this.GetComponentInChildren<ScoreText>();
    }

    public void SetScore(int score)
    {
        _scoreText.SetScore(score);
    }
}
