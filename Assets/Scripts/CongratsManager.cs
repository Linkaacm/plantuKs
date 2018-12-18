using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CongratsManager : MonoBehaviour {

	public Sprite[] sprites;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		CheckForMaxPlant ();
	}

	public void CheckForMaxPlant()
	{
		for (int i = 0; i < 9; i++)
		{
			if (AllGameData.instance.playerData.plant_isUsed [i] && AllGameData.instance.playerData.plant_phase [i] == (short)10 && AllGameData.instance.playerData.plant_congratulations[i] == false)
			{

				FindObjectOfType<MasterG> ().LeaveAllPanels ();
				this.transform.GetChild (0).gameObject.SetActive (true);
				this.transform.GetChild (1).gameObject.SetActive (true);
				this.transform.GetChild (2).gameObject.SetActive (true);
				this.transform.GetChild (3).gameObject.SetActive (true);
				DetermineRenderSprite (i);

				AllGameData.instance.playerData.plant_congratulations [i] = true;
				AllGameData.instance.SaveData ();

				this.enabled = false;


			}
		}
	}

	void DetermineRenderSprite(int index)
	{
		//bool auxIsPlantShiny;
		//short auxPlantPhase;
		string auxName;
		int spriteIndex;
		//const int cactusHash = "Cactus".GetHashCode ();
		//const int cempaHash = "Cempasúchil".GetHashCode ();
		//const int girasolHash = "Girasol".GetHashCode ();
		//const int narcisoHash = "Narciso".GetHashCode ();
		//const int nocheHash = "Nochebuena".GetHashCode ();
		//const int sabilaHash = "Sábila".GetHashCode ();

		auxName = AllGameData.instance.playerData.plant_name [index];
		//auxPlantPhase = AllGameData.instance.playerData.plant_phase [index];
		//auxIsPlantShiny = AllGameData.instance.playerData.plant_isShiny [index];
		spriteIndex = 0;

		switch (auxName)
		{
		case "Cactus":
			spriteIndex += 0;
			break;
		case "Cempasuchil":
			spriteIndex += 1;
			break;
		case "Girasol":
			spriteIndex += 2;
			break;
		case "Narciso":
			spriteIndex += 3;
			break;
		case "Nochebuena":
			spriteIndex += 4;
			break;
		case "Sabila":
			spriteIndex += 5;
			break;
		default:
			spriteIndex += 0;
			break;
		}

		this.transform.GetChild (1).GetComponent<Image> ().sprite = sprites [spriteIndex];
		//slots [index].gameObject.GetComponent<SpriteRenderer> ().sprite = allSprites [spriteIndex];
	}
}
