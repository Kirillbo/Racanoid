
using UnityEngine;

public class SystemsRespawnEnemy : IAwake
{
	private PoolManager _pool;

	public void OnAwake()
	{
		_pool = PoolManager.Instance;
		_pool.CreatePool(PoolType.ActiveEnemy, 0, null);
		
		
		for (int x = 0; x < 10; x++)
		{
			for (int y = 0; y < 5; y++)
			{

				var block = _pool.ReSpawn(PoolType.Enemy);
				block.transform.position = new Vector2(x, y);
				block.SetActive(true);
				_pool.Add(PoolType.ActiveEnemy, block);
			}
		}
		
	}
}
