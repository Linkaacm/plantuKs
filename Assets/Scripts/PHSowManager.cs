using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHSowManager : MonoBehaviour {

	//private bool masterIsWatering = false;
	private bool panelSowingIsOn = false;
	
	void Update ()
	{
		//masterIsWatering = FindObjectOfType<MasterG> ().getIsWatering ();
		panelSowingIsOn = FindObjectOfType<MasterG> ().panelSowing.gameObject.activeSelf;

		//Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && regularViewIsOn && !masterIsWatering
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && panelSowingIsOn)
		{
			CheckTouchSelection (Input.GetTouch (0));
		}
	}

	void CheckTouchSelection(Touch auxTouch)
	{
		RaycastHit hit = new RaycastHit ();
		Ray mouseRay = Camera.main.ScreenPointToRay (auxTouch.position);

		if (Physics.Raycast (mouseRay.origin, mouseRay.direction, out hit))
		{
			switch (hit.transform.name)
			{
			case "PH1_SpriteYES":
				FindObjectOfType<MasterG> ().StartSlot1 ();
				break;
			case "PH2_SpriteYES":
				FindObjectOfType<MasterG> ().StartSlot2 ();
				break;
			case "PH3_SpriteYES":
				FindObjectOfType<MasterG> ().StartSlot3 ();
				break;
			case "PH4_SpriteYES":
				FindObjectOfType<MasterG> ().StartSlot4 ();
				break;
			case "PH5_SpriteYES":
				FindObjectOfType<MasterG> ().StartSlot5 ();
				break;
			case "PH6_SpriteYES":
				FindObjectOfType<MasterG> ().StartSlot6 ();
				break;
			case "PH7_SpriteYES":
				FindObjectOfType<MasterG> ().StartSlot7 ();
				break;
			case "PH8_SpriteYES":
				FindObjectOfType<MasterG> ().StartSlot8 ();
				break;
			case "PH9_SpriteYES":
				FindObjectOfType<MasterG> ().StartSlot9 ();
				break;
			default:
				break;
			}

		}
	}
}
