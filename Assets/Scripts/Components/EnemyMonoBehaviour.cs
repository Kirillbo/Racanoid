using UnityEngine;

public class EnemyMonoBehaviour : MonoBehaviour
{
	private SpriteRenderer _view;

	private void Awake()
	{
		_view = GetComponentInChildren<SpriteRenderer>();
	}

	
	public int Hp;

	public void SetTypeEnemy(TypeEnemy type)
	{
		switch (type)
		{
			case	TypeEnemy.Simple:
				Hp = 1;
				_view.sprite = GameManager.Instance.DataSpritesEnemy.SpritesEnemy[0];
				break;
			
			case TypeEnemy.Medium:
				Hp = 2;
				_view.sprite = GameManager.Instance.DataSpritesEnemy.SpritesEnemy[1];
				break;
			
			case TypeEnemy.Hard:
				Hp = 3;
				_view.sprite = GameManager.Instance.DataSpritesEnemy.SpritesEnemy[2];
				break;
		}
	}
}
