using System;
using UnityEngine;

public class MoveEnemy : IAwake, IDisposable
{

    private GameObject[] _blocks;
    private Timer _timer;
    
    public void OnAwake()
    {
        var pool = PoolManager.Instance;

        _blocks = pool.GetStack(PoolType.ActiveEnemy).ToArray();
        var timeMoveEnemy = pool.Get<ComponentSettingsGame>().SpeedMoveEnemy;
        
        _timer = Timer.Add(timeMoveEnemy, Step, true);
    }

    void Step()
    {
        if(_blocks.Length < 1) return;

        foreach (var block in _blocks)
        {
            block.transform.position += Vector3.down;
        }

        
    }

    public void Dispose()
    {
        _timer.Kill();
        _blocks = null;
    }
}

