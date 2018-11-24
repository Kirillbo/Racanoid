using UnityEngine;

public class SystemMovePlayer : IAwake, ITick
{
	private PoolManager _pool;
	private Transform _player;
	private ComponentSettingsGame _settings;
	private ComponentInput _input;

	private Vector2 _leftEdgeScreen, _rightEdgeScreen;
	
	public void OnAwake()
	{	
		_pool = PoolManager.Instance;
		_player = _pool.Get(PoolType.Player).transform;
		_player.gameObject.SetActive(true);
		_settings = _pool.Get<ComponentSettingsGame>();
		_input = _pool.Get<ComponentInput>();
		
		_leftEdgeScreen = Camera.main.ViewportToWorldPoint(new Vector2(0.1f, 0));
		_rightEdgeScreen = Camera.main.ViewportToWorldPoint(new Vector2(0.9f, 0f));
	}

	public void Tick()
	{
		var currentPos = _player.position; 
		var acceleration = Vector3.right * _settings.SpeedPlayer * _input.value * Time.deltaTime;
		currentPos += acceleration;
		
		var finalPos = new Vector3(
			
			Mathf.Clamp(currentPos.x, _leftEdgeScreen.x, _rightEdgeScreen.x),
			currentPos.y,
			currentPos.z
			
			);

		_player.position = finalPos;
	}
}
