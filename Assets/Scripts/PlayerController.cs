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
	public float defaultDrag = 1;
	public GameObject rightArm;
	public GameObject leftArm;
	public float armDistanceMultiplier = 1;

	private Rigidbody rb;
	private SpawnPool rightPool;
	private SpawnPool leftPool;
	private bool rCentered;
	private bool lCentered;
	private bool ceaseFire;

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

		//TODO: Default drag needs to be tweaked from just 1 or .5
		//Drag 
		rb = GetComponent<Rigidbody>();
		SetDrag(defaultDrag);
	}
	
	void Update () {
		//Set L/R arm positions (use Value.normalized to snap to player i.e. all magnitude 1)
		rightArm.transform.localPosition = playerInput.RMove.Value * armDistanceMultiplier;
		leftArm.transform.localPosition = playerInput.LMove.Value * armDistanceMultiplier;

		/*TODO: Check if positions have changed before advancing further to avoid double-checking
		/*	check both separately: +more likely to halt advance +pretty easy w/ now-consolidated component assignments
		/*	MOST OFTEN OCCURS WHILE CENTERED (may not matter)
		*/

        //Check if local positions are centered
		rCentered = rightArm.transform.localPosition == Vector3.zero;
		lCentered = leftArm.transform.localPosition == Vector3.zero;

		//Get global positions for rotation calculations
		Vector3 pos = transform.position;
        Vector3 rpos = rightArm.transform.position;
		Vector3 lpos = leftArm.transform.position;

        //Check center for rotations and reload
		ceaseFire = true;
		if(rCentered){
			rightArm.transform.localRotation = Quaternion.identity;
			rightPool.Reload();
		}
		else{
			rightArm.transform.rotation = Quaternion.LookRotation(rpos - pos);
			//Check trigger for firing and recoil
			if(playerInput.RFire){
				ceaseFire = false;
				Vector3 force = rightPool.Fire();
				Recoil(-force);
				SetDrag(0);
			}
		}
		if(lCentered){
			leftArm.transform.localRotation = Quaternion.identity;
			leftPool.Reload();
		}
		else{
			leftArm.transform.rotation = Quaternion.LookRotation(lpos - pos);
			//Check trigger for firing and recoil
			if(playerInput.LFire){
				ceaseFire = false;
				Vector3 force = leftPool.Fire();
				Recoil(-force);
				SetDrag(0);
			}
		}

		//TODO: Have drag reinstate ONLY when no projectiles are firing (currnly only when fire buttons aren't held) (feature?)
		//Reinstate drag
		if(ceaseFire)
			SetDrag(defaultDrag);
		}

	public void SetDrag(float drag){
		rb.drag = drag;
	}

    //Hey it's the name of the thing
	public void Recoil(Vector3 force){
		rb.AddForce(force, ForceMode.Impulse);
	}
}
