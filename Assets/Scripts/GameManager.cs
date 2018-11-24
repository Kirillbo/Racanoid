using System.Collections.Generic;
using Homebrew;
using TMPro;
using UnityEngine;


public class GameManager : BaseGameManager
{

    public ScriptableEntities Entities;
    [Foldout("Settings Game")] public float SpeedPlayer; 
    [Foldout("Settings Game")] public float SpeedMoveEnemy; 
    [Foldout("Settings Game")] public float SpeedMoveBall; 
    
    [HideInInspector] public PoolManager Pool;
    private AState _activeState;

    
    protected override void Awake()
    {
        base.Awake();
        Pool = PoolManager.Instance;
        InitPool(Pool);
        
        Systems = new SystemProcessings();
    }


    protected override void Start()
    {
        base.Start();
        
        Systems.Add<SystemRespawnPlayerAndBall>();
        Systems.Add<SystemsRespawnEnemy>();
        Systems.Add<SystemInput>();
        Systems.Add<SystemMovePlayer>();
        Systems.Add<MoveEnemy>();
        Systems.Add<SystemMoveBall>();
        Systems.Add<SystemCollisions>();
        Systems.Add<SystemDestroy>();
    }

    

    void InitPool( PoolManager pool)
    {

        var component = pool.AddComponent<ComponentSettingsGame>();
        component.SpeedMoveEnemy = SpeedMoveEnemy;
        component.SpeedPlayer = SpeedPlayer;
        component.SpeedBall = SpeedMoveBall;

        pool.AddComponent<ComponentDirection>().value = new Vector2(Random.value, 1);
        pool.AddComponent<ComponentInput>();
        pool.CreatePool(PoolType.Player, 1, Entities.Player);
        pool.CreatePool(PoolType.Enemy, 60, Entities.Enemy);
        pool.CreatePool(PoolType.Ball, 1, Entities.Ball);

        
    }

    private void FixedUpdate()
    {
        Systems.FixUpdate();
    }

    void Update () {
        
        Systems.Update();
    }
}
