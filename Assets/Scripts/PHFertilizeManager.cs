using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHFertilizeManager : MonoBehaviour {

	//private bool masterIsWatering = false;
	private bool panelFertilizeIsOn = false;

	void Update ()
	{
		//masterIsWatering = FindObjectOfType<MasterG> ().getIsWatering ();
		panelFertilizeIsOn = FindObjectOfType<MasterG> ().panelFertilizing.gameObject.activeSelf;

		//Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && regularViewIsOn && !masterIsWatering
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && panelFertilizeIsOn)
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
				FindObjectOfType<MasterG> ().Fertilize1 ();
				break;
			case "PH2_SpriteYES":
				FindObjectOfType<MasterG> ().Fertilize2 ();
				break;
			case "PH3_SpriteYES":
				FindObjectOfType<MasterG> ().Fertilize3 ();
				break;
			case "PH4_SpriteYES":
				FindObjectOfType<MasterG> ().Fertilize4 ();
				break;
			case "PH5_SpriteYES":
				FindObjectOfType<MasterG> ().Fertilize5 ();
				break;
			case "PH6_SpriteYES":
				FindObjectOfType<MasterG> ().Fertilize6 ();
				break;
			case "PH7_SpriteYES":
				FindObjectOfType<MasterG> ().Fertilize7 ();
				break;
			case "PH8_SpriteYES":
				FindObjectOfType<MasterG> ().Fertilize8 ();
				break;
			case "PH9_SpriteYES":
				FindObjectOfType<MasterG> ().Fertilize9 ();
				break;
			default:
				break;
			}

		}
	}
}
