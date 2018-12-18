using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RearrangeInitiator : MonoBehaviour {

	public Transform[] ninePossibleSlots = new Transform[9];

	private Transform[] nineRedSlots = new Transform[9];
	private Transform[] nineGreenSlots = new Transform[9];

	// Use this for initialization
	void Awake ()
	{
		//Sets the red "slots" and the green ones
		for (int i = 0; i < ninePossibleSlots.Length; i++)
		{
			//First red
			nineRedSlots [i] = ninePossibleSlots [i].GetChild (0);
			//Then green
			nineGreenSlots [i] = ninePossibleSlots [i].GetChild (1);
		}

		CheckForFreeSlots ();
	}

	//If used, space turns green, otherwise turns red
	public void CheckForFreeSlots()
	{
		
		for (int i = 0; i < 9; i++)
		{
			if (AllGameData.instance.playerData.plant_isUsed [i])
			{
				//Turn on green, turn off red
				nineGreenSlots [i].gameObject.SetActive (true);
				nineRedSlots [i].gameObject.SetActive (false);
			}

			else
			{
				//Turn on red, turn off green
				nineGreenSlots [i].gameObject.SetActive (false);
				nineRedSlots [i].gameObject.SetActive (true);
			}
		}
	}
}
