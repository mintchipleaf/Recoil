using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public GameObject player1;
	public GameObject player2;

	//Returns controller device associated w/ player number
	public InputDevice GetPlayerController(PlayerType playerType) {
		InputDevice controller = null;
		if (playerType == PlayerType.Player1)
			controller = (InputManager.Devices.Count == 0) ? null : InputManager.Devices[0];
		else if (playerType == PlayerType.Player2)
			controller = (InputManager.Devices.Count < 2) ? null : InputManager.Devices[1];
		return controller;
	}
	void Awake(){
		instance = this;
	}
	void Update () {
		//Show player 2 based on controllers connected (should move/simplify)
		if (InputManager.Devices.Count == 1)
			player2.SetActive(false);
		else if (InputManager.Devices.Count == 2)
			player2.SetActive(true);
		//Temp quit key
		if(Input.GetKeyDown( KeyCode.Escape ))
			Application.Quit();
	}
}
