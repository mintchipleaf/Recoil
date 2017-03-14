using UnityEngine;
/*A lot of this code will either go into a per-item script or be modified by each item
 */
public class SpawnPool : MonoBehaviour
{
	public float force = 1;
	public int framesPerShot = 10;

	private ObjectPooling pool;
	private GameObject go;
	private int frameCounter = 0;
	private bool shootOK = true;

	void Start ()
	{
		pool = GameObject.FindGameObjectWithTag("ObjectPooler").GetComponent<ObjectPooling> ();
	}

	void Update ()
	{
		//Gate shots to one every X frames
		if(frameCounter > framesPerShot){
			frameCounter = 0;
			shootOK = true;
		}
		frameCounter++;
	}
	public Vector3 Fire()
	{
		Vector3 velocity = Vector3.zero;
		if(shootOK){
			shootOK = false;

			go = pool.RetrieveInstance();
			if(go){
				go.transform.position = transform.position;
				go.transform.rotation = transform.rotation;

				go.GetComponent<Rigidbody>().AddForce(go.transform.forward * force, ForceMode.Impulse);
				//velocity = go.GetComponent<Rigidbody>().velocity;
                //Probably doesn't accurately represent velocity of obj, but getting .velocity doesn't work (prob bc it's on same frame as AddForce)
				//Follow up: make size of proejctile matter
				velocity = go.transform.forward;
				//Debug.Log(velocity);
			}
		}
		return velocity;
	}
}