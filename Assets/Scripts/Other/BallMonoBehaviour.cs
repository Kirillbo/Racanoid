using UnityEngine;

public class BallMonoBehaviour : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("DefensiveWall"))
		{
			EventGameOver end;
			EventManager.Instance.Send(end);
		}
		
		if (collision.gameObject.CompareTag("Enemy"))
		{
			EventDestroy d = new EventDestroy();
			d.Target = collision.gameObject;
			EventManager.Instance.Send(d);
		}
		
		EventCollision col = new EventCollision();
		col.NormalColliision = collision.contacts[0].normal;
		EventManager.Instance.Send(col);
	}
}
