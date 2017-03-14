using UnityEngine;

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
	public void Fire()
	{
		if(shootOK){
			shootOK = false;

			go = pool.RetrieveInstance();
			if(go){
				go.transform.position = transform.position;
				go.transform.rotation = transform.rotation;

				go.GetComponent<Rigidbody>().AddForce(go.transform.forward * force, ForceMode.Impulse);
			}
		}
	}
}