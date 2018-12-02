
using System;
using UnityEngine;

public class SystemRefelction : IAwake, IReceive<EventCollision>, IDisposable
{
	private ComponentSettingsGame _settings;
	private ComponentDirection _directionBall;
	private Vector2 lastFrameVelocity;
	
	public void OnAwake()
	{
		EventManager.Instance.Add<EventCollision>(this);
		_directionBall = PoolManager.Instance.Get<ComponentDirection>();
		_settings = PoolManager.Instance.Get<ComponentSettingsGame>();
	}
	
	
	public void HandleSignal(EventCollision arg)
	{
		lastFrameVelocity = _settings.SpeedBall * _directionBall.value;		
		var direction = Vector3.Reflect(lastFrameVelocity.normalized, arg.NormalColliision);
		_directionBall.value = direction;
	}

	public void Dispose()
	{
		EventManager.Instance.Remove<EventCollision>(this);
	}
}
