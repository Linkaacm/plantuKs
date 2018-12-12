using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertilizeManager : MonoBehaviour
{
	public Transform[] ninePossibleSlots = new Transform[9];

	// Use this for initialization
	void Start ()
	{
		CheckForFreeSlots ();
	}

	public void CheckForFreeSlots()
	{
		//AllGameData.instance.LoadData ();
		for (int i = 0; i < 9; i++)
		{
			if (AllGameData.instance.playerData.plant_isUsed [i])
			{
				ninePossibleSlots [i].gameObject.SetActive (true);								//There's data, so can use that slot	
			}

			else
			{
				ninePossibleSlots [i].gameObject.SetActive (false);								//Otherwise, nope
			}
		}
	}
		
}
