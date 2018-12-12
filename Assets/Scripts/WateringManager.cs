using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringManager : MonoBehaviour {

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
			if (AllGameData.instance.playerData.plant_isUsed [i] && AllGameData.instance.playerData.plant_phase[i] < (short)10)
			{
				if (AllGameData.instance.playerData.plant_timeLeftSeconds [i] > 0)				//Still have to wait
				{
					ninePossibleSlots [i].gameObject.SetActive (false);							//... So no possible use of that slot
				}
				else
				{
					ninePossibleSlots [i].gameObject.SetActive (true);							//Otherwise, you may use that slot
				}
			}

			else
			{
				ninePossibleSlots [i].gameObject.SetActive (false);								//can't use that slot either (no plant data or max phase)
			}

		}
	}

}
