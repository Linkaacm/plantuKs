using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHRearrangeBegins : MonoBehaviour {

	//private bool masterIsWatering = false;
	private bool panelRearrangeBeginsIsOn = false;

	void Update ()
	{
		//masterIsWatering = FindObjectOfType<MasterG> ().getIsWatering ();
		panelRearrangeBeginsIsOn = FindObjectOfType<MasterG> ().panelRearrangeBegins.gameObject.activeSelf;

		//Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && regularViewIsOn && !masterIsWatering
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && panelRearrangeBeginsIsOn)
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
				FindObjectOfType<MasterG> ().FinishRerrange1 ();
				break;
			case "PH2_SpriteYES":
				FindObjectOfType<MasterG> ().FinishRerrange2 ();
				break;
			case "PH3_SpriteYES":
				FindObjectOfType<MasterG> ().FinishRerrange3 ();
				break;
			case "PH4_SpriteYES":
				FindObjectOfType<MasterG> ().FinishRerrange4 ();
				break;
			case "PH5_SpriteYES":
				FindObjectOfType<MasterG> ().FinishRerrange5 ();
				break;
			case "PH6_SpriteYES":
				FindObjectOfType<MasterG> ().FinishRerrange6 ();
				break;
			case "PH7_SpriteYES":
				FindObjectOfType<MasterG> ().FinishRerrange7 ();
				break;
			case "PH8_SpriteYES":
				FindObjectOfType<MasterG> ().FinishRerrange8 ();
				break;
			case "PH9_SpriteYES":
				FindObjectOfType<MasterG> ().FinishRerrange9 ();
				break;
			default:
				break;
			}

		}
	}
}
