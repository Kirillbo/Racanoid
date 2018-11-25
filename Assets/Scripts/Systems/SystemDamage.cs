using UnityEngine;

public class SystemDamage : IAwake, IReceive<EventDestroy> {


	public void OnAwake()
	{
		EventManager.Instance.Add(this);
	}

	public void HandleSignal(EventDestroy arg)
	{
		var enemy = arg.Target.GetComponent<EnemyMonoBehaviour>();
		if (enemy != null)
		{
			enemy.Hp--;

			if (enemy.Hp < 1)
			{
				enemy.gameObject.SetActive(false);
			}
		}
	}
}
