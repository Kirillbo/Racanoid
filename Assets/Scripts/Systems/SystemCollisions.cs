
using UnityEngine;

public class SystemCollisions : IAwake, IReceive<EventCollision>
{
	private ComponentSettingsGame _settings;
	private ComponentDirection _directionBall;
	private Vector2 lastFrameVelocity;
	
	public void OnAwake()
	{
		ProcessingEvent.Instance.Add(this);
		_directionBall = PoolManager.Instance.Get<ComponentDirection>();
		_settings = PoolManager.Instance.Get<ComponentSettingsGame>();
	}
	
	
	public void HandleSignal(EventCollision arg)
	{
		lastFrameVelocity = _settings.SpeedBall * _directionBall.value;
		
		var speed = lastFrameVelocity.magnitude;
		var direction = Vector3.Reflect(lastFrameVelocity.normalized, arg.NormalColliision);

		_directionBall.value = direction;
	}
	
}
