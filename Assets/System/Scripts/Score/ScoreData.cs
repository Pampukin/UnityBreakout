using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "ScriptableObject/Create ScoreData")]
public class ScoreData : ScriptableObject
{
    [SerializeField]
    private MonoBehaviour _object;
    
    [SerializeField]
    private int score = 100;

    public bool _isMatchObject(MonoBehaviour hitObject)
    {
        return hitObject.GetType() == _object.GetType();
    }

    public int GetScore(MonoBehaviour hitObject)
    {
        if (_isMatchObject(hitObject))
        {
            return score;
        }
        else
        {
            return 0;
        }

    }
}
