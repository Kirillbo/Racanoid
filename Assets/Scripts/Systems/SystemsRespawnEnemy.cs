
using System;
using UnityEngine;

public class SystemsRespawnEnemy : IAwake, IDisposable
{
	private PoolManager _pool;

	public void OnAwake()
	{
		_pool = PoolManager.Instance;
		_pool.CreatePool(PoolType.ActiveEnemy, 0, null);
		var map = _pool.Get<ComponentMap>().Map;
		
		for (int x = 0; x < map.GetLength(0); x++)
		{
			for (int y = 0; y < map.GetLength(1); y++)
			{
				if(map[x, y].IsActive == false) continue;

				var pos = map[x, y].Position;
				var enemyType = map[x, y].EnemyType;
				
				var block = _pool.ReSpawn(PoolType.Enemy);
				block.transform.position = (Vector2)pos;
				block.GetComponent<EnemyMonoBehaviour>().SetTypeEnemy(enemyType); 
				block.SetActive(true);
				
				_pool.Add(PoolType.ActiveEnemy, block);
			}
		}
		
	}

	public void Dispose()
	{
		var countObject = _pool.GetStack(PoolType.ActiveEnemy).Count;

		for (int i = 0; i < countObject; i++)
		{
			var obj = _pool.ReSpawn(PoolType.ActiveEnemy);
			_pool.DeSpawn(PoolType.Enemy, obj);
		}

		_pool = null;
	}
}
