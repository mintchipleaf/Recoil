using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class InputTester : MonoBehaviour {

	public bool rightStick = false;
	private float leftX;
	private float leftY;
	private float rightX;
	private float rightY;
    
	void Update () {
		var inputDevice = InputManager.ActiveDevice;
		
		Vector2 pos;
		if(rightStick)
		{
			rightX = inputDevice.LeftStickY.Value;
			rightY = inputDevice.RightStickY.Value * -1;
			pos = new Vector2(rightX, rightY);
		}
		else
		{
			leftX = inputDevice.LeftStickX.Value;
			leftY = inputDevice.RightStickX.Value * -1;
			pos = new Vector2(leftX, leftY);
		}
		
		if(rightStick){
			Debug.Log(pos);
		}
		transform.position = pos;
	}
}
