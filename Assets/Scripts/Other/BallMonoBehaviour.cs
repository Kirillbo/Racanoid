using UnityEngine;

public class BallMonoBehaviour : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			EventDestroy d = new EventDestroy();
			d.Target = collision.gameObject;
			ProcessingEvent.Instance.Send(d);
		}
		
		EventCollision e = new EventCollision();
		e.NormalColliision = collision.contacts[0].normal;
		ProcessingEvent.Instance.Send(e);
	}
}
