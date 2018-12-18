using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegularViewManager : MonoBehaviour
{

	public Transform[] miniInfos = new Transform[9];


	// Use this for initialization
	void Update ()
	{
		UpdateAllMiniInfos ();
	}
	
	public void UpdateAllMiniInfos ()
	{
		for (int i = 0; i < 9; i++)
		{
			if (AllGameData.instance.playerData.plant_isUsed[i] && AllGameData.instance.playerData.plant_phase[i] < (short)10)
			{
				
				miniInfos [i].gameObject.SetActive (true);

				if (AllGameData.instance.playerData.plant_timeLeftSeconds [i] > 0)
				{
					miniInfos[i].GetChild (0).gameObject.SetActive (false);					//We DO NOT need the sprite
					miniInfos[i].GetChild (1).gameObject.SetActive (true);					//We set the remaining time

					if (AllGameData.instance.playerData.plant_timeLeftSeconds [i] < 60)
					{
						miniInfos [i].GetChild (1).GetComponent<Text> ().text = AllGameData.instance.playerData.plant_timeLeftSeconds [i] + "s";
					}

					else if (AllGameData.instance.playerData.plant_timeLeftSeconds [i] >= 60 && AllGameData.instance.playerData.plant_timeLeftSeconds [i] < 3600)
					{
						miniInfos [i].GetChild (1).GetComponent<Text> ().text = (AllGameData.instance.playerData.plant_timeLeftSeconds [i] / 60) + "m";
					}

					else if (AllGameData.instance.playerData.plant_timeLeftSeconds [i] >= 3600 && AllGameData.instance.playerData.plant_timeLeftSeconds [i] < 100000)
					{
						miniInfos [i].GetChild (1).GetComponent<Text> ().text = (AllGameData.instance.playerData.plant_timeLeftSeconds [i] / 3600) + "h";
					}

				}
				else
				{
					miniInfos[i].GetChild (0).gameObject.SetActive (true);					//We set the sprite
					miniInfos[i].GetChild (1).gameObject.SetActive (false);					//We DO NOT the remaining time
				}
			}
			else
			{
				miniInfos [i].gameObject.SetActive (false);
			}
		}
	}
		
}
