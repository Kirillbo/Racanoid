using UnityEngine;

public class SystemInput :  IAwake, ITick
{
	private PoolManager _pool;
	private ComponentInput _inputComponent;
	
	public void OnAwake()
	{
		_pool = PoolManager.Instance;
		_inputComponent = _pool.Get<ComponentInput>();
		
	}

	public void Tick()
	{
		_inputComponent.value = Input.GetAxis("Horizontal");
	}
}
