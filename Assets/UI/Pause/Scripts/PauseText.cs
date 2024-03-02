using System;
using TMPro;
using UnityEngine;

public class PauseText : MonoBehaviour
{
    private TextMeshProUGUI _text;

    public TextMeshProUGUI Text => _text;
    
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
}
