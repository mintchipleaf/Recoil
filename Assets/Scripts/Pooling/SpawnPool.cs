using UnityEngine;
public class SpawnPool : MonoBehaviour
{
	private ObjectPooling pool;
	private GameObject go;

	void Start ()
	{
		pool = GameObject.FindGameObjectWithTag("ObjectPooler").GetComponent<ObjectPooling> ();
	}

	public Vector3 Spawn(Vector3 position, Quaternion rotation, float multiplier, Color color)
	{
		Vector3 force = Vector3.zero;
		go = pool.RetrieveInstance();
		if(go){
			go.GetComponent<Renderer>().material.color = color;
			go.transform.position = position;
			go.transform.rotation = rotation;

			force = rotation * Vector3.forward * multiplier;
			Rigidbody rigidbody = go.GetComponent<Rigidbody>();
			rigidbody.AddForce(force, ForceMode.Impulse);
			force *= rigidbody.mass;
		}
		return force;
	}
}