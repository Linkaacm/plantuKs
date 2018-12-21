using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHRearrangeFinishes : MonoBehaviour {

	//private bool masterIsWatering = false;
	private bool panelRearrangeFinishesIsOn = false;

	void Update ()
	{
		//masterIsWatering = FindObjectOfType<MasterG> ().getIsWatering ();
		panelRearrangeFinishesIsOn = FindObjectOfType<MasterG> ().panelRearrangeFinishes.gameObject.activeSelf;

		//Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && regularViewIsOn && !masterIsWatering
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && panelRearrangeFinishesIsOn)
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
				FindObjectOfType<MasterG> ().InterchangeSlot1 ();
				break;
			case "PH2_SpriteYES":
				FindObjectOfType<MasterG> ().InterchangeSlot2 ();
				break;
			case "PH3_SpriteYES":
				FindObjectOfType<MasterG> ().InterchangeSlot3 ();
				break;
			case "PH4_SpriteYES":
				FindObjectOfType<MasterG> ().InterchangeSlot4 ();
				break;
			case "PH5_SpriteYES":
				FindObjectOfType<MasterG> ().InterchangeSlot5 ();
				break;
			case "PH6_SpriteYES":
				FindObjectOfType<MasterG> ().InterchangeSlot6 ();
				break;
			case "PH7_SpriteYES":
				FindObjectOfType<MasterG> ().InterchangeSlot7 ();
				break;
			case "PH8_SpriteYES":
				FindObjectOfType<MasterG> ().InterchangeSlot8 ();
				break;
			case "PH9_SpriteYES":
				FindObjectOfType<MasterG> ().InterchangeSlot9 ();
				break;
			default:
				break;
			}

		}
	}
}
