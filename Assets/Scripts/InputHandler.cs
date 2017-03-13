using UnityEngine;
using InControl;

public class InputHandler : MonoBehaviour {

	public bool rightStick = false;
	public float distanceMultiplier = 1;
	private float leftX;
	private float leftY;
	private float rightX;
	private float rightY;
    
	void Update ()
	{
		var inputDevice = InputManager.ActiveDevice;
		
		Vector2 pos;
		if(rightStick)
		{
			rightX = inputDevice.RightStickX.Value * distanceMultiplier;
			rightY = inputDevice.RightStickY.Value * distanceMultiplier;
			pos = new Vector2(rightX, rightY);
		}
		else
		{
			leftX = inputDevice.LeftStickX.Value * distanceMultiplier;
			leftY = inputDevice.LeftStickY.Value * distanceMultiplier;
			pos = new Vector2(leftX, leftY);
		}
		
		//Rel position from parent set to stick position from center
		transform.localPosition = pos;
		//Rotation just looks at parent lol
		transform.LookAt(gameObject.transform.parent);
	}
}
