using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private string _baseText = "Score : ";

    private void Awake()
    {
        _text = this.GetComponent<TextMeshProUGUI>();
    }

    public void SetScore(int score)
    {
        _text.SetText(_baseText + $"{score:0000000}");
    }
}
