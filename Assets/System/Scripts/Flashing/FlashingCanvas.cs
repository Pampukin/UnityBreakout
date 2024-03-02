using UnityEngine;

public abstract class FlashingCanvas : MonoBehaviour
{
    [SerializeField]
    private FlashingText _flashingText;
    
    //開始時の色。
    [SerializeField]
    private Color32 _startColor = new Color32(255, 255, 255, 255);
	
    //終了(折り返し)時の色。
    [SerializeField]
    private Color32 _endColor = new Color32(255, 255, 255, 16);

    [SerializeField]
    private float _duration = 0.75f;

    private float _startTime;

    private void Start()
    {
        _startTime = Time.realtimeSinceStartup;
    }

    private void _flashing()
    {
        _flashingText.Text.color = Color.Lerp(_startColor,_endColor,Mathf.PingPong((Time.realtimeSinceStartup - _startTime) / _duration, 1.0f));
    }
    
    private void Update()
    {
        _flashing();
    }
}
