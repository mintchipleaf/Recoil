using UnityEngine;
using InControl;

public class SpawnPool : MonoBehaviour
{
	public bool rightStick;
	public float force = 1;
	public int framesPerShot = 10;

	private ObjectPooling pool;
	private GameObject go;
	private int shotCounter = 0;
	private bool notShot = true;

	void Start ()
	{
		pool = GameObject.FindGameObjectWithTag("ObjectPooler").GetComponent<ObjectPooling> ();
	}

	void Update ()
	{
		//Gate shots to one every X frames
		if(shotCounter > framesPerShot){
			shotCounter = 0;
			notShot = true;
		}
		//Input specific stuff- should be moved
		var inputDevice = InputManager.ActiveDevice;	
		bool trigger = rightStick? inputDevice.RightTrigger.IsPressed : inputDevice.LeftTrigger.IsPressed;
        
		if(trigger && notShot){
			notShot = false;

			go = pool.RetrieveInstance();
			if(go){
				go.transform.position = transform.position;
				go.transform.rotation = transform.rotation;

				Vector2 dir;
				dir = rightStick? new Vector2(inputDevice.RightStickX.Value, inputDevice.RightStickY.Value): new Vector2(inputDevice.LeftStickX.Value, inputDevice.LeftStickY.Value);
				//go.GetComponent<Rigidbody>().AddForce(new Vector3(go.transform.rotation.x * force, go.transform.rotation.y * force, 0));
				go.GetComponent<Rigidbody>().AddForce(dir,ForceMode.Impulse);
			}
		}
		shotCounter++;
	}
}