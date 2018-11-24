using UnityEngine;

public class MoveEnemy : IAwake
{

    private GameObject[] _blocks;
    
    public void OnAwake()
    {
        var pool = PoolManager.Instance;
        
        _blocks = pool.GetStack(PoolType.ActiveEnemy).ToArray();
        var timeMoveEnemy = pool.Get<ComponentSettingsGame>().SpeedMoveEnemy;
        
        Timer.Add(timeMoveEnemy, Step, true);
    }

    void Step()
    {
        if(_blocks.Length < 1) return;

        foreach (var block in _blocks)
        {
            block.transform.position += Vector3.down;
        }

        
    }
}

