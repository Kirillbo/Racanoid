using System.Collections;
using UnityEngine;

public class BallMonoBehaviour : MonoBehaviour
{
	
	private void OnCollisionEnter(Collision collision)
	{
		
		EventCollision rebound = new EventCollision();
		rebound.NormalColliision = collision.contacts[0].normal;
		EventManager.Instance.Send(rebound);
		
		if (collision.gameObject.CompareTag("DefensiveWall"))
		{
			EventGameOver end;
			EventManager.Instance.Send(end);
		}
		
		else if (collision.gameObject.CompareTag("Enemy"))
		{

			EventDestroy d = new EventDestroy();
			d.Target = collision.gameObject;
			EventManager.Instance.Send(d);

			EventUpdateScore score = new EventUpdateScore();
			score.Value = 1;
			EventManager.Instance.Send(score);
		}
	}

}
