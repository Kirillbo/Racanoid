using System;
using UnityEngine;

public class SystemRespawnPlayerAndBall : IAwake, IDisposable
{

	public void OnAwake()
	{
		var pool = PoolManager.Instance;
		Vector2 placeRespawn = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.1f));
		
		var player = pool.Get(PoolType.Player).transform;
		var ball = pool.Get(PoolType.Ball).transform;
		
		player.position = placeRespawn;
		ball.position = placeRespawn + Vector2.up;
		ball.gameObject.SetActive(true);
	}


	public void Dispose()
	{
		PoolManager.Instance.Get(PoolType.Ball).gameObject.SetActive(false);
	}
}
