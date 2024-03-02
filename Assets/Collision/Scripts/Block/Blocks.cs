using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Blocks : MonoBehaviour
{
    [SerializeField]
    private List<Block> _blocks = new List<Block>();

    public UnityAction<Block> BreakAction
    {
        set
        {
            _breakAction += value;
        }
    }

    private UnityAction<Block> _breakAction;

    private void Start()
    {
        foreach (var block in GetComponentsInChildren<Block>())
        {
            _blocks.Add(block);
        }
    }

    public void BrokeBlock(Block block)
    {
        _blocks.Remove(block);

        if (_isClear())
        {
            StateManager.INSTANCE?.AllBlocksDestroyed();
        }
    }

    private bool _isClear()
    {
        return _blocks.Count == 0;
    }
    
}
