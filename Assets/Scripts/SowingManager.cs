using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SowingManager : MonoBehaviour {

	[HideInInspector] public short indexForNewPlant;

	public Transform[] ninePossibleSlots = new Transform[9];

	// Use this for initialization
	void Start ()
	{
		/*
		for (int i = 0; i < 9; i++)
		{
			ninePossibleSlots [i] = this.transform.GetChild (i);
		}
		
		CheckForFreeSlots ();
		*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetNewPlantIndex(short newValue)
	{
		indexForNewPlant = newValue;
	}

	public void CheckForFreeSlots()
	{
		for (int i = 0; i < 9; i++)
		{
			if (AllGameData.instance.playerData.plant_isUsed [i])
			{
				ninePossibleSlots [i].gameObject.SetActive (false);
			}
			else
			{
				ninePossibleSlots [i].gameObject.SetActive (true);
			}
		}
	}
}
