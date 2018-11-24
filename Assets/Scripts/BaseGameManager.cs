using Assets.Scripts.Tools;

public class BaseGameManager: SingltoonBehavior<BaseGameManager> {

    public SystemProcessings Systems; 
    
    protected virtual void Start()
    {
        //base systems
        Systems.Add<ProcessingTimer>();
        var proc = ProcessingEvent.Instance;
        Systems.Add(proc);
    }
}
