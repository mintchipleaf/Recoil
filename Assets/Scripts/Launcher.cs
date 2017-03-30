using UnityEngine;

public class Launcher : MonoBehaviour
{
	public float projectileForceMultiplier = 1;
	public int framesPerShot = 10;
	public int maxShots = 0;

	private SpawnPool spawner;
	private Transform spawnPoint;
	private int frameCounter = 0;
	private bool shootOK = true;
	[SerializeField]
	private int currentShots = 0;
	new private Renderer renderer;
	[SerializeField]
	private float reloadPercentage;

	void Start () {
		spawner = GameObject.FindGameObjectWithTag("ObjectPooler").GetComponent<SpawnPool> ();
		spawnPoint = transform.Find("SpawnPoint");
		currentShots = maxShots;
		renderer = GetComponent<Renderer>();
	}
	
	void Update () {
		//Gate shots to one every X frames
		if(frameCounter > framesPerShot){
			frameCounter = 0;
			shootOK = true;
		}
		frameCounter++;	

		//Set shader reload percentage||
		reloadPercentage = (float)currentShots/(float)maxShots;
		renderer.material.SetFloat("_ReloadPercentage", reloadPercentage);
	}

	public Vector3 Fire(){
		Vector3 force = Vector3.zero;
		if(currentShots <= 0){
			shootOK = false;
		}
		if(shootOK){
			shootOK = false;
            //Spawn projectile
			force = spawner.Spawn(spawnPoint.position, transform.rotation, projectileForceMultiplier, renderer.material.color);
			//Reduce shots by 1
			--currentShots;
		}
		return force;
	}

	public void Reload(){
		currentShots = maxShots;
	}
}
