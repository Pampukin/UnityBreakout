using UnityEngine;

public class Crash : MonoBehaviour
{
    [SerializeField]
    private GameObject _myObject;

    public GameObject MyObject
    {
        set
        {
            _myObject = value;
        }
    }

    [SerializeField]
    private GameObject _otherObject;
    
    public GameObject OtherObject
    {
        set
        {
            _otherObject = value;
        }
    }
}
