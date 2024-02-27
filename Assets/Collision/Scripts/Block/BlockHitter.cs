using UnityEngine;

public class BlockHitter : MonoBehaviour,IHit
{
    /// <summary>
    /// 破壊処理
    /// </summary>
    public void Hit()
    {
        this.gameObject.SetActive(false);
    }
}
