using Assets.Scripts.Tools;
using Homebrew;
using UnityEngine;
using UnityEngine.Serialization;


public class GameManager : SingltoonBehavior<GameManager>
{

    public ScriptableEntities Entities;
    public SpritesScriptable DataSpritesEnemy;
    
    
    [Foldout("Settings Game")] public float SpeedPlayer; 
    [Foldout("Settings Game")] public int TimeAttack; 
    [Foldout("Settings Game")] public float SpeedMoveBall; 
    
    [HideInInspector] public PoolManager Pool;
    public SystemProcessings Systems;
    public SystemProcessings GlobalSystems;
    
    private bool _tick;
    
    protected override void Awake()
    {
        base.Awake();
        Tick(false);
        
        InitPool(Pool);
        InitBaseSystem(GlobalSystems);
        
        Systems = new SystemProcessings();
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
        Systems.Add<SystemWin>();
    }

    void InitBaseSystem(SystemProcessings system)
    {
        GlobalSystems = new SystemProcessings();
        GlobalSystems.Add<ProcessingTimer>();
        GlobalSystems.Add<SystemUI>();
    }

    void InitPool( PoolManager pool)
    {
        pool = PoolManager.Instance;

        var component = pool.AddComponent<ComponentSettingsGame>();
        component.TimeAttack = TimeAttack;
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
        if(_tick)
        Systems.FixUpdate();
        GlobalSystems.FixUpdate();
    }

    void Update () {
        
        if(_tick)
        Systems.Update();
        GlobalSystems.Update();
    }

    public void RestartGenerateMap()
    {
       Systems.Clear();
       Start();
    }
   
    public void Tick(bool val)
    {
        _tick = val;
    }
}
