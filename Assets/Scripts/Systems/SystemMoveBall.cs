using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemMoveBall : IAwake, IFixTick
{

	private Rigidbody _ball;
	private ComponentSettingsGame _settingsGame;
	private ComponentDirection _directionBall;
	
	public void OnAwake()
	{
		PoolManager pool = PoolManager.Instance;
		_ball = pool.Get(PoolType.Ball).GetComponent<Rigidbody>();
		_settingsGame = pool.Get<ComponentSettingsGame>();
		_directionBall = pool.Get<ComponentDirection>();
	}


	public void FixTick()
	{
		_ball.velocity = _directionBall.value * _settingsGame.SpeedBall;

	}
}
