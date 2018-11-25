using System.Collections.Generic;
using Assets.Scripts.Tools;
using Homebrew;
using UnityEngine;


public class GameManager : SingltoonBehavior<GameManager>
{

    public ScriptableEntities Entities;
    public SpritesScriptable DataSpritesEnemy;
    
    [Foldout("Settings Game")] public float SpeedPlayer; 
    [Foldout("Settings Game")] public float SpeedMoveEnemy; 
    [Foldout("Settings Game")] public float SpeedMoveBall; 
    
    [HideInInspector] public PoolManager Pool;
    public SystemProcessings Systems; 

    
    protected override void Awake()
    {
        base.Awake();
        Pool = PoolManager.Instance;
        InitPool(Pool);
        
        Systems = new SystemProcessings();
        InitBaseSystem(Systems);
    }


    void Start()
    {
        
        Systems.Add<SystemGenerateMap>();
        Systems.Add<SystemRespawnPlayerAndBall>();
        Systems.Add<SystemsRespawnEnemy>();
        Systems.Add<SystemInput>();
        Systems.Add<SystemMovePlayer>();
        Systems.Add<MoveEnemy>();
        Systems.Add<SystemMoveBall>();
        Systems.Add<SystemRefelction>();
        Systems.Add<SystemDamage>();
        Systems.Add<SystemGameOver>();
    }

    void InitBaseSystem(SystemProcessings system)
    {
        system.Add<ProcessingTimer>();
        var eventManager = EventManager.Instance;
        system.Add(eventManager);
    }

    void InitPool( PoolManager pool)
    {

        var component = pool.AddComponent<ComponentSettingsGame>();
        component.SpeedMoveEnemy = SpeedMoveEnemy;
        component.SpeedPlayer = SpeedPlayer;
        component.SpeedBall = SpeedMoveBall;

        pool.AddComponent<ComponentDirection>().value = new Vector2(Random.value, 1);
        pool.AddComponent<ComponentInput>();
        //pool.AddComponent<>()
        
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
