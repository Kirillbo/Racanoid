using System;
using UnityEngine;

public class SystemDamage : IAwake, IReceive<EventDestroy>, IDisposable {


	public void OnAwake()
	{
		EventManager.Instance.Add(this);
	}

	public void HandleSignal(EventDestroy arg)
	{
		Debug.Log("Damage");
		var enemy = arg.Target.GetComponent<EnemyMonoBehaviour>();
		
		if (enemy != null)
		{
			enemy.Hp--;

			if (enemy.Hp < 1)
			{
				enemy.gameObject.SetActive(false);
				PoolManager.Instance.ReSpawn(PoolType.ActiveEnemy);

				if (PoolManager.Instance.GetStack(PoolType.ActiveEnemy).Count < 1)
				{
					EventWin win;
					EventManager.Instance.Send(win);
				}
			}
		}
	}

	public void Dispose()
	{
		EventManager.Instance.Remove(this);
	}
}
