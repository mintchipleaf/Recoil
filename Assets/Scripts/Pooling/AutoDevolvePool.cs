using UnityEngine;

public class AutoDevolvePool : MonoBehaviour
{
	public int time = 2;

	private float seconds = 0;
	private ObjectPooling pooler;

	void Start ()
	{
		pooler = GameObject.FindGameObjectWithTag("ObjectPooler").GetComponent<ObjectPooling>();
	}

	void Update ()
	{
		seconds += Time.deltaTime;

		if (seconds >= time) {
			UnityEngine.Rigidbody rb = GetComponent<Rigidbody>();
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			pooler.DevolveInstance(gameObject);
			seconds = 0;
		}
	}
}