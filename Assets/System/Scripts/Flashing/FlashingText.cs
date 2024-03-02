using TMPro;
using UnityEngine;

public abstract class FlashingText : MonoBehaviour
{
    private TextMeshProUGUI _text;

    public TextMeshProUGUI Text => _text;
    
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
}
