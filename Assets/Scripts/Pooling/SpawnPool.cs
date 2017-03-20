using UnityEngine;
public class SpawnPool : MonoBehaviour
{
	private ObjectPooling pool;
	private GameObject go;

	void Start ()
	{
		pool = GameObject.FindGameObjectWithTag("ObjectPooler").GetComponent<ObjectPooling> ();
	}

	public Vector3 Spawn(Vector3 position, Quaternion rotation, float force)
	{
		Vector3 velocity = Vector3.zero;
		go = pool.RetrieveInstance();
		if(go){
			go.transform.position = position;
			go.transform.rotation = rotation;

			go.GetComponent<Rigidbody>().AddForce(go.transform.forward * force, ForceMode.Impulse);
			//velocity = go.GetComponent<Rigidbody>().velocity;
			//Probably doesn't accurately represent velocity of obj, but getting .velocity doesn't work (prob bc it's on same frame as AddForce)
			//Follow up: make size of proejctile matter
			velocity = go.transform.forward;
		}
		return velocity;
	}
}