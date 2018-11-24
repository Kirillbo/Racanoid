using UnityEngine;

public class SystemDestroy : IAwake, IReceive<EventDestroy> {


	public void OnAwake()
	{
		ProcessingEvent.Instance.Add(this);
	}

	public void HandleSignal(EventDestroy arg)
	{
		arg.Target.SetActive(false);
	}
}
