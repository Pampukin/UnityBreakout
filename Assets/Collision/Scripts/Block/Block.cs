using System;
using UnityEngine;

public class Block : MonoBehaviour,IHit
{
    private Blocks _blocks;

    private void Awake()
    {
        _blocks = transform.root.GetComponent<Blocks>();
    }

    /// <summary>
    /// 破壊処理
    /// </summary>
    public void Hit()
    {
        _blocks.BrokeBlock(this);
        ScoreManager.INSTANCE?.AddScore(this);
        this.gameObject.SetActive(false);
    }
}
