using System;

public class SystemWin : IAwake, IReceive<EventWin>, IDisposable {

	
	public void OnAwake()
	{
		EventManager.Instance.Add<EventWin>(this);
	}

	public void HandleSignal(EventWin arg)
	{
		GameManager.Instance.Systems.Clear();
	}

	public void Dispose()
	{
		EventManager.Instance.Remove<EventWin>(this);
	}
}
