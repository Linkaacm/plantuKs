using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
	public Transform[] slots;
	//Alphabetical order, passing through all phases. Then the shiny, finally the next specimen
	//Cactus1,2,3...CactusShiny1,2,3...Cempa...Girasol...
	public Sprite[] allSprites;
	public Text timeLeft;



	//private float timeCounter;

	void Start ()
	{
		//GetAndSetAllSlotData ();
		//timeCounter = 0f;
		FindObjectOfType<MasterG> ().WaitOneUpdateCycle ();
		//StartCoroutine (CalculateTime ());
	}
	
	void Update ()
	{
		//timeLeft.text = AllGameData.instance.playerData.plant_timeLeftSeconds [0].ToString ();
	}

	public void GetAndSetAllSlotData()
	{
		ulong ticksAux = (ulong)System.DateTime.Now.Ticks;

		for (int i = 0; i < 9; i++)
		{
			if (AllGameData.instance.playerData.plant_isUsed[i])
			{
				//set the sprite
				slots [i].gameObject.GetComponent<SpriteRenderer> ().enabled = true;
				DetermineRenderSprite (i);

				//Calculate aprox. age from birth date in months
				ulong age = (ticksAux - AllGameData.instance.playerData.plant_tickDate[i]) / (System.TimeSpan.TicksPerDay * 30);

				//set local variables from json
				slots [i].gameObject.GetComponent<PlantManager> ().plantName = AllGameData.instance.playerData.plant_name [i];
				slots [i].gameObject.GetComponent<PlantManager> ().plantDate = AllGameData.instance.playerData.plant_dayDate [i].ToString () + "/" + AllGameData.instance.playerData.plant_monthDate [i].ToString () + "/" + AllGameData.instance.playerData.plant_yearDate [i].ToString ();
				slots [i].gameObject.GetComponent<PlantManager> ().plantAge = (short)age;
				slots [i].gameObject.GetComponent<PlantManager> ().plantPhase = AllGameData.instance.playerData.plant_phase [i];
			}
			else
			{
				//Otherwise stop rendering that slot
				slots [i].gameObject.GetComponent<SpriteRenderer> ().enabled = false;
				//Debug.Log (slots [i].gameObject.GetComponent<SpriteRenderer> ().sprite.name);
			}
		}
	}

	void DetermineRenderSprite(int index)
	{
		bool auxIsPlantShiny;
		short auxPlantPhase;
		string auxName;
		int spriteIndex;
		//const int cactusHash = "Cactus".GetHashCode ();
		//const int cempaHash = "Cempasúchil".GetHashCode ();
		//const int girasolHash = "Girasol".GetHashCode ();
		//const int narcisoHash = "Narciso".GetHashCode ();
		//const int nocheHash = "Nochebuena".GetHashCode ();
		//const int sabilaHash = "Sábila".GetHashCode ();

		auxName = AllGameData.instance.playerData.plant_name [index];
		auxPlantPhase = AllGameData.instance.playerData.plant_phase [index];
		auxIsPlantShiny = AllGameData.instance.playerData.plant_isShiny [index];
		spriteIndex = 0;

		switch (auxName)
		{
		case "Cactus":
			spriteIndex += 0;
			break;
		case "Cempasuchil":
			spriteIndex += 6;
			break;
		case "Girasol":
			spriteIndex += 12;
			break;
		case "Narciso":
			spriteIndex += 18;
			break;
		case "Nochebuena":
			spriteIndex += 24;
			break;
		case "Sabila":
			spriteIndex += 30;
			break;
		default:
			spriteIndex += 0;
			break;
		}

		if (auxPlantPhase == 1)
		{
			spriteIndex += 0;
		}
		else if (auxPlantPhase > 1 && auxPlantPhase <= 5)
		{
			spriteIndex += 1;
		}
		else if (auxPlantPhase > 5 && auxPlantPhase <= 10)
		{
			spriteIndex += 2;
		}

		if (auxIsPlantShiny)
		{
			spriteIndex += 3;
		}

		//Debug.Log (auxName);
		//Debug.Log ("Sábila y Cempasúchil");
		slots [index].gameObject.GetComponent<SpriteRenderer> ().sprite = allSprites [spriteIndex];
	}
}