using UnityEngine;
using InControl;

public class PlayerActions : PlayerActionSet
{
	//Right stick
	public PlayerAction RFire;
	public PlayerAction RLeft;
	public PlayerAction RRight;
	public PlayerAction RUp;
	public PlayerAction RDown;
	public PlayerTwoAxisAction RMove;
	
	//Left stick
	public PlayerAction LFire;
	public PlayerAction LLeft;
	public PlayerAction LRight;
	public PlayerAction LUp;
	public PlayerAction LDown;
	public PlayerTwoAxisAction LMove;

	public PlayerActions()
	{
		//Right
		RFire = CreatePlayerAction( "R Fire" );
		RLeft = CreatePlayerAction( "R Move Left" );
		RRight = CreatePlayerAction( "R Move Right" );
		RUp = CreatePlayerAction( "R Move Up" );
		RDown = CreatePlayerAction( "R Move Down" );
		RMove = CreateTwoAxisPlayerAction( RLeft, RRight, RDown, RUp );
		//Left
		LFire = CreatePlayerAction( "L Fire" );
		LLeft = CreatePlayerAction( "L Move Left" );
		LRight = CreatePlayerAction( "L Move Right" );
		LUp = CreatePlayerAction( "L Move Up" );
		LDown = CreatePlayerAction( "L Move Down" );
		LMove = CreateTwoAxisPlayerAction( LLeft, LRight, LDown, LUp );
	}
}


public class PlayerController : MonoBehaviour
{
	public GameObject rightArm;
	public GameObject leftArm;
	public float armDistanceMultiplier = 1;

	private SpawnPool rightPool;
	private SpawnPool leftPool;

	PlayerActions playerInput;

	void Start () {
		playerInput = new PlayerActions();

		playerInput.RFire.AddDefaultBinding( InputControlType.RightTrigger);
		playerInput.LFire.AddDefaultBinding( InputControlType.LeftTrigger);
	    //Temp mouse control
		playerInput.RFire.AddDefaultBinding( Mouse.LeftButton );

		playerInput.RLeft.AddDefaultBinding( InputControlType.RightStickLeft );
		playerInput.RRight.AddDefaultBinding( InputControlType.RightStickRight );
		playerInput.RDown.AddDefaultBinding( InputControlType.RightStickDown );
		playerInput.RUp.AddDefaultBinding( InputControlType.RightStickUp );
		//Temp mouse control
        playerInput.RLeft.AddDefaultBinding( Mouse.NegativeX );
        playerInput.RRight.AddDefaultBinding( Mouse.PositiveX );
		playerInput.RDown.AddDefaultBinding( Mouse.NegativeY );
		playerInput.RUp.AddDefaultBinding( Mouse.PositiveY );
		
		playerInput.LLeft.AddDefaultBinding( InputControlType.LeftStickLeft );
		playerInput.LRight.AddDefaultBinding( InputControlType.LeftStickRight );
		playerInput.LDown.AddDefaultBinding( InputControlType.LeftStickDown );
		playerInput.LUp.AddDefaultBinding( InputControlType.LeftStickUp );

		//Get spawn pool components
		rightPool = rightArm.GetComponent<SpawnPool>();
		leftPool = leftArm.GetComponent<SpawnPool>();
	}
	
	// Update is called once per frame
	void Update () {
		//Set L/R arm positions (Value.normalized to snap to player i.e. all magnitude 1)
		rightArm.transform.localPosition = playerInput.RMove.Value * armDistanceMultiplier;
		leftArm.transform.localPosition = playerInput.LMove.Value * armDistanceMultiplier;
		//Set L/R arm rotations
		//if(rightArm.transform.position != Vector3.zero){
		rightArm.transform.rotation = Quaternion.LookRotation(rightArm.transform.position - transform.position);
		//if(leftArm.transform.position != Vector3.zero){
		leftArm.transform.rotation = Quaternion.LookRotation(leftArm.transform.position - transform.position);

		//Check L/R Fire
		if(playerInput.RFire){
			rightPool.Fire();
		}
		if(playerInput.LFire){
			leftPool.Fire();
		}
	}
}
