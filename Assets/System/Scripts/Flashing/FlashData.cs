using UnityEngine;

[CreateAssetMenu(fileName = "FlashData", menuName = "ScriptableObject/Create FlashData")]
public class FlashData : ScriptableObject
{
    //開始時の色。
    [SerializeField]
    private Color32 _startColor = new Color32(255, 255, 255, 255);

    public Color32 StartColor => _startColor;

    //終了(折り返し)時の色。
    [SerializeField]
    private Color32 _endColor = new Color32(255, 255, 255, 16);

    public Color32 EndColor => _endColor;

    [SerializeField]
    private float _duration = 0.75f;

    public float Duration => _duration;
}
