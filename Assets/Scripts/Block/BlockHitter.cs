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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            Hit();
        }
    }
}
