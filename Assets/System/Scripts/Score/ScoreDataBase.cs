using UnityEngine;

[CreateAssetMenu(fileName = "ScoreDataBase", menuName = "ScriptableObject/Create ScoreDataBase")]
public class ScoreDataBase : ScriptableObject
{
    [SerializeField]
    private ScoreData[] _scoreDataList;

    public ScoreData[] ScoreDataList => _scoreDataList;

    public int GetScore(MonoBehaviour hitObject)
    {
        foreach (var scoreData in _scoreDataList)
        {
            var score = scoreData.GetScore(hitObject);
            if (score != 0)
            {
                return score;
            }
        }

        return 0;
    }
}
