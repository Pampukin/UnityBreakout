using UnityEngine;

public abstract class FlashingCanvas : MonoBehaviour
{
    [SerializeField]
    private FlashingText _flashingText;

    [SerializeField]
    private FlashData _flashData;

    private float _startTime;

    private void Start()
    {
        _startTime = Time.realtimeSinceStartup;
    }

    private void _flashing()
    {
        _flashingText.Text.color = Color.Lerp(_flashData.StartColor,_flashData.EndColor,Mathf.PingPong((Time.realtimeSinceStartup - _startTime) / _flashData.Duration, 1.0f));
    }
    
    private void Update()
    {
        _flashing();
    }
}
