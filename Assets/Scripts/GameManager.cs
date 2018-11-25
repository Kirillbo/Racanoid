using Assets.Scripts.Tools;
using Homebrew;
using UnityEngine;


public class GameManager : SingltoonBehavior<GameManager>
{

    public ScriptableEntities Entities;
    public SpritesScriptable DataSpritesEnemy;
    public GameObject Canvas;
    
    [Foldout("Settings Game")] public float SpeedPlayer; 
    [Foldout("Settings Game")] public float SpeedMoveEnemy; 
    [Foldout("Settings Game")] public float SpeedMoveBall; 
    
    [HideInInspector] public PoolManager Pool;
    public SystemProcessings Systems;
    public SystemProcessings GlobalSystems;
    
    private bool _tick;
    
    protected override void Awake()
    {
        base.Awake();
        Pool = PoolManager.Instance;
        InitPool(Pool);
        
        Systems = new SystemProcessings();
        InitBaseSystem(Systems);

        _tick = false;
    }


    void Start()
    {        
        Systems.Add<SystemGenerateMap>();
        Systems.Add<SystemsRespawnEnemy>();
        Systems.Add<SystemRespawnPlayerAndBall>();
        Systems.Add<SystemInput>();
        Systems.Add<SystemMovePlayer>();
        Systems.Add<MoveEnemy>();
        Systems.Add<SystemMoveBall>();
        Systems.Add<SystemRefelction>();
        Systems.Add<SystemDamage>();
        Systems.Add<SystemGameOver>();
        Systems.Add<SystemUI>();
        
    }

    void InitBaseSystem(SystemProcessings system)
    {
        GlobalSystems = new SystemProcessings();
        GlobalSystems.Add<ProcessingTimer>();
        var eventManager = EventManager.Instance;
        GlobalSystems.Add(eventManager);
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
        if(_tick)
        Systems.FixUpdate();
    }

    void Update () {
        
        if(_tick)
        Systems.Update();
    }

    public void RestartGenerateMap()
    {
        Systems.Remove<SystemGenerateMap>();
        Systems.Remove<SystemsRespawnEnemy>();
        Systems.Remove<MoveEnemy>();

        Systems.Add<SystemGenerateMap>();
        Systems.Add<SystemsRespawnEnemy>();
        Systems.Add<MoveEnemy>();
    }
   
    public void Tick(bool val)
    {
        _tick = val;
    }
}
