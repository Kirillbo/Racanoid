using System;

public class SystemWin : IAwake, IReceive<EventWin>, IDisposable {

	
	public void OnAwake()
	{
		EventManager.Instance.Add(this);
	}

	public void HandleSignal(EventWin arg)
	{
		GameManager.Instance.Systems.Clear();
		EventManager.Instance.Dispose();
	}

	public void Dispose()
	{
		EventManager.Instance.Remove(this);
	}
}
