using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
	public float projectileForceMultiplier = 1;
	public int framesPerShot = 10;
	public int maxShots = 0;

	private SpawnPool spawner;
	private int frameCounter = 0;
	private bool shootOK = true;
	private int currentShots = 0;

	void Start () {
		spawner = GameObject.FindGameObjectWithTag("ObjectPooler").GetComponent<SpawnPool> ();
		currentShots = maxShots;
	}
	
	void Update () {
		//Gate shots to one every X frames
		if(frameCounter > framesPerShot){
			frameCounter = 0;
			shootOK = true;
		}
		frameCounter++;	
	}

	public Vector3 Fire(){
		Vector3 velocity = Vector3.zero;
		if(currentShots <= 0){
			shootOK = false;
		}
		if(shootOK){
			shootOK = false;
			/*Todo: Spawn projectile outside launcher model
			 */
            //Spawn projectile
			velocity = spawner.Spawn(transform.position, transform.rotation, projectileForceMultiplier);
			//Reduce shots by 1
			--currentShots;
		}
		return velocity;
	}

	public void Reload(){
		currentShots = maxShots;
	}
}
