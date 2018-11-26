﻿using System;

public class SystemGameOver : IAwake, IReceive<EventGameOver>, IDisposable {
	
	public void OnAwake()
	{
		EventManager.Instance.Add(this);
	}

	public void HandleSignal(EventGameOver arg)
	{
		GameManager.Instance.Systems.Clear();
	}

	public void Dispose()
	{
		EventManager.Instance.Remove(this);
	}
}
