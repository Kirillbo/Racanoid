

public class SystemGameOver : IAwake, IReceive<EventGameOver> {
	
	public void OnAwake()
	{
		EventManager.Instance.Add(this);
	}

	public void HandleSignal(EventGameOver arg)
	{
		GameManager.Instance.Systems.Clear();
	}
}
