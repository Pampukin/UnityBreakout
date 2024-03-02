using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    private Image _image;

    public Image Image => _image;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
