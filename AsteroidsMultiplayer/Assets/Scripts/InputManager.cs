using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine;

public static class InputManager {

	public static void ReadInputs(PlayerController player){
		player.accelerate = CnInputManager.GetButton("Accelerate");

		player.Turn (CnInputManager.GetAxisRaw ("Rotate"));

		if (CnInputManager.GetButton("Shoot"))
			player.CmdShoot ();
	}
}
