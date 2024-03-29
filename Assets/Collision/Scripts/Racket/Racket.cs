using UnityEngine;

public class Racket : MonoBehaviour
{
    private Vector3 _scale;
    
    public float Height
    {
        get
        {
            return _scale.x;
        }
    }
    
    public float Width
    {
        get
        {
            return _scale.y;
        }
    }

    [SerializeField]
    private Ball _ball;

    private GameObject _ballObject;

    private Vector3 _offset = new Vector3(0,0.3f,0);

    private Rigidbody2D _rb;
    
    private void Awake()
    {
        _scale = this.transform.localScale;
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
        _SetBallObject();
        StageManager.INSTANCE.ClearAction += () => _rb.bodyType = RigidbodyType2D.Static;
        StageManager.INSTANCE.PauseAction += () => _rb.bodyType = RigidbodyType2D.Static;
        StageManager.INSTANCE.ResumeAction += () => _rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void Update()
    {
        if (StageManager.INSTANCE?.StageState == StageState.Pre)
        {
            _SetBallObject();
            _ballObject.transform.position = this.transform.position + _offset;
        }
    }

    public void SetScale(Vector3 scale)
    {
        this.transform.localScale = scale;
        _scale = scale;
    }

    private void _SetBallObject()
    {
        if (_ballObject == null)
        {
            var isGameOver = LivesManager.INSTANCE?.IsGameOver ?? false;
            if (!isGameOver)
            {
                _ballObject = Instantiate(_ball).gameObject;
            }
            
        }
    }
}
