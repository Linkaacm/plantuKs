using System.Collections;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MasterG : MonoBehaviour
{

	public Transform panelStats;
	public Transform panelRegularView;
	public Transform panelInventory;
	public Transform panelTrophies;
	public Transform panelStore;
	public Transform panelNotEnoughMoney;
	public Transform panelSowing;
	public Transform panelNoResources;
	public Transform panelFertilizing;
	public Transform panelWatering;

	//public SpriteRenderer activeMe;

	private float timeCounter;
	//private bool isPaused = false;
	private bool isProcessing = false;
	private bool isFocus = false;

	//public Transform masterPlantSlot;

	// Use this for initialization
	void Start ()
	{
		//Sets all visual content at the game
		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		SustractTime ();
		timeCounter = 0f;
		StartCoroutine (CalculateTime ());
	}

	public void SustractTime()
	{
		ulong ticksNow = (ulong)DateTime.Now.Ticks;

		//Debug.Log (auxHour + ":" + auxMinute + ":" + auxSecond + " del " + auxDay + " de " + auxMonth + " del " + auxYear);

		for (int i = 0; i < 9; i++)
		{
			if (AllGameData.instance.playerData.plant_isUsed[i] && AllGameData.instance.playerData.plant_timeLeftSeconds[i] > 0)
			{
				int theWholeTime = 0;
				ulong diff = ticksNow - AllGameData.instance.playerData.plant_lastTick [i];
				theWholeTime = (int)(diff / TimeSpan.TicksPerSecond);


				AllGameData.instance.playerData.plant_timeLeftSeconds [i] -= theWholeTime;
				AllGameData.instance.playerData.plant_lastTick [i] = ticksNow;

				AllGameData.instance.SaveData ();
			}

		}

		//timeCounter = 0f;
		//Debug.Log (System.DateTime.Now.ye);
	}

	// Update is called once per frame
	void Update ()
	{
		//Debug.Log ((ulong)System.DateTime.Now.Ticks);

		/*
		if (isPaused)
		{
			Debug.Log ("So, this is a pause");
		}
		*/
	}

	//TOP SECRET
	public void TOPSECRET()
	{
		AllGameData.instance.playerData.koins += 1000;
		AllGameData.instance.SaveData ();
	}

	public void LeaveInfo()
	{
		panelStats.gameObject.SetActive (false);
		panelInventory.gameObject.SetActive (false);
		panelStore.gameObject.SetActive (false);
		panelTrophies.gameObject.SetActive (false);
		panelNotEnoughMoney.gameObject.SetActive (false);
		panelNoResources.gameObject.SetActive (false);
		panelSowing.gameObject.SetActive (false);
		panelFertilizing.gameObject.SetActive (false);
		panelWatering.gameObject.SetActive(false);

		panelRegularView.gameObject.SetActive (true);
	}

	public void ReturnFromNoMoney()
	{
		panelNotEnoughMoney.gameObject.SetActive (false);
	}

	public void ReturnFromNoResources()
	{
		panelNoResources.gameObject.SetActive (false);
	}

	public void ShareFacebook()
	{
		if (!isProcessing)
		{
			StartCoroutine (ShareScreenshot ());
		}
	}

	public void goToInventory()
	{
		panelRegularView.gameObject.SetActive (false);
		panelStats.gameObject.SetActive (false);
		panelInventory.gameObject.SetActive (false);
		panelStore.gameObject.SetActive (false);
		panelTrophies.gameObject.SetActive (false);
		panelNotEnoughMoney.gameObject.SetActive (false);
		panelNoResources.gameObject.SetActive (false);
		panelSowing.gameObject.SetActive (false);
		panelFertilizing.gameObject.SetActive (false);
		panelWatering.gameObject.SetActive(false);

		panelInventory.gameObject.SetActive (true);
		StartCoroutine (WaitOneUpdateCycle ());
		panelInventory.GetComponent<InventoryManager> ().UpdateAllText ();
	}

	public void goToStore()
	{
		panelRegularView.gameObject.SetActive (false);
		panelStats.gameObject.SetActive (false);
		panelInventory.gameObject.SetActive (false);
		panelTrophies.gameObject.SetActive (false);
		panelNotEnoughMoney.gameObject.SetActive (false);
		panelNoResources.gameObject.SetActive (false);
		panelSowing.gameObject.SetActive (false);
		panelFertilizing.gameObject.SetActive (false);
		panelWatering.gameObject.SetActive(false);
		panelInventory.gameObject.SetActive (false);

		//Gets AvailableKoinsPanel then gets KoinsText gameObject to set the text
		panelStore.gameObject.SetActive (true);
		StartCoroutine (WaitOneUpdateCycle ());
		panelStore.GetComponent<StoreManager> ().UpdateKoinText ();
		panelStore.GetComponent<StoreManager> ().UpdateOrderOfSprites (0);
		panelStore.GetComponent<StoreManager> ().UpdateSprites (0);
		//Debug.Log (panelStore.GetComponent<StoreManager> ().orderOfSprites);
	}
		
	public void goToTrophies()
	{
		panelRegularView.gameObject.SetActive (false);
		panelStats.gameObject.SetActive (false);
		panelInventory.gameObject.SetActive (false);
		panelStore.gameObject.SetActive (false);
		panelNotEnoughMoney.gameObject.SetActive (false);
		panelNoResources.gameObject.SetActive (false);
		panelSowing.gameObject.SetActive (false);
		panelFertilizing.gameObject.SetActive (false);
		panelWatering.gameObject.SetActive(false);
		panelInventory.gameObject.SetActive (false);

		panelTrophies.gameObject.SetActive (true);
	}

	public void GoForwardButton()
	{
		short auxState;
		auxState = FindObjectOfType<StoreManager> ().orderOfSprites;

		if (auxState == 5)
		{
			auxState = 0;
		}
		else
		{
			auxState++;
		}

		panelStore.GetComponent<StoreManager> ().UpdateSprites (auxState);
		panelStore.GetComponent<StoreManager> ().UpdateOrderOfSprites (auxState);
	}

	public void GoBackButton()
	{
		short auxState;
		auxState = FindObjectOfType<StoreManager> ().orderOfSprites;

		if (auxState == 0)
		{
			auxState = 5;
		}
		else
		{
			auxState--;
		}

		panelStore.GetComponent<StoreManager> ().UpdateSprites (auxState);
		panelStore.GetComponent<StoreManager> ().UpdateOrderOfSprites (auxState);
	}

	public void SowCactus()
	{
		if (AllGameData.instance.playerData.flowerPots > 0 && AllGameData.instance.playerData.seedEnvelopes [0] > 0)
		{
			panelRegularView.gameObject.SetActive (false);
			panelStats.gameObject.SetActive (false);
			panelInventory.gameObject.SetActive (false);
			panelStore.gameObject.SetActive (false);
			panelTrophies.gameObject.SetActive (false);
			panelNotEnoughMoney.gameObject.SetActive (false);
			panelNoResources.gameObject.SetActive (false);
			panelFertilizing.gameObject.SetActive (false);
			panelWatering.gameObject.SetActive(false);
			panelInventory.gameObject.SetActive (false);

			panelSowing.gameObject.SetActive (true);
			StartCoroutine (WaitOneUpdateCycle ());
			panelSowing.GetComponent<SowingManager> ().SetNewPlantIndex (0);
			panelSowing.GetComponent<SowingManager> ().CheckForFreeSlots ();
		}
		else
		{
			panelNoResources.gameObject.SetActive (true);
		}
	}

	public void SowCempa()
	{
		if (AllGameData.instance.playerData.flowerPots > 0 && AllGameData.instance.playerData.seedEnvelopes [1] > 0)
		{
			panelRegularView.gameObject.SetActive (false);
			panelStats.gameObject.SetActive (false);
			panelInventory.gameObject.SetActive (false);
			panelStore.gameObject.SetActive (false);
			panelTrophies.gameObject.SetActive (false);
			panelNotEnoughMoney.gameObject.SetActive (false);
			panelNoResources.gameObject.SetActive (false);
			panelFertilizing.gameObject.SetActive (false);
			panelWatering.gameObject.SetActive(false);
			panelInventory.gameObject.SetActive (false);

			panelSowing.gameObject.SetActive (true);
			StartCoroutine (WaitOneUpdateCycle ());
			panelSowing.GetComponent<SowingManager> ().SetNewPlantIndex (1);
			panelSowing.GetComponent<SowingManager> ().CheckForFreeSlots ();
		}
		else
		{
			panelNoResources.gameObject.SetActive (true);
		}
	}

	public void SowGira()
	{
		if (AllGameData.instance.playerData.flowerPots > 0 && AllGameData.instance.playerData.seedEnvelopes [2] > 0)
		{
			panelRegularView.gameObject.SetActive (false);
			panelStats.gameObject.SetActive (false);
			panelInventory.gameObject.SetActive (false);
			panelStore.gameObject.SetActive (false);
			panelTrophies.gameObject.SetActive (false);
			panelNotEnoughMoney.gameObject.SetActive (false);
			panelNoResources.gameObject.SetActive (false);
			panelFertilizing.gameObject.SetActive (false);
			panelWatering.gameObject.SetActive(false);
			panelInventory.gameObject.SetActive (false);

			panelSowing.gameObject.SetActive (true);
			StartCoroutine (WaitOneUpdateCycle ());
			panelSowing.GetComponent<SowingManager> ().SetNewPlantIndex (2);
			panelSowing.GetComponent<SowingManager> ().CheckForFreeSlots ();
		}
		else
		{
			panelNoResources.gameObject.SetActive (true);
		}
	}

	public void SowNarci()
	{
		if (AllGameData.instance.playerData.flowerPots > 0 && AllGameData.instance.playerData.seedEnvelopes [3] > 0)
		{
			panelRegularView.gameObject.SetActive (false);
			panelStats.gameObject.SetActive (false);
			panelInventory.gameObject.SetActive (false);
			panelStore.gameObject.SetActive (false);
			panelTrophies.gameObject.SetActive (false);
			panelNotEnoughMoney.gameObject.SetActive (false);
			panelNoResources.gameObject.SetActive (false);
			panelFertilizing.gameObject.SetActive (false);
			panelWatering.gameObject.SetActive(false);
			panelInventory.gameObject.SetActive (false);

			panelSowing.gameObject.SetActive (true);
			StartCoroutine (WaitOneUpdateCycle ());
			panelSowing.GetComponent<SowingManager> ().SetNewPlantIndex (3);
			panelSowing.GetComponent<SowingManager> ().CheckForFreeSlots ();
		}
		else
		{
			panelNoResources.gameObject.SetActive (true);
		}
	}

	public void SowNoche()
	{
		if (AllGameData.instance.playerData.flowerPots > 0 && AllGameData.instance.playerData.seedEnvelopes [4] > 0)
		{
			panelRegularView.gameObject.SetActive (false);
			panelStats.gameObject.SetActive (false);
			panelInventory.gameObject.SetActive (false);
			panelStore.gameObject.SetActive (false);
			panelTrophies.gameObject.SetActive (false);
			panelNotEnoughMoney.gameObject.SetActive (false);
			panelNoResources.gameObject.SetActive (false);
			panelFertilizing.gameObject.SetActive (false);
			panelWatering.gameObject.SetActive(false);
			panelInventory.gameObject.SetActive (false);

			panelSowing.gameObject.SetActive (true);
			StartCoroutine (WaitOneUpdateCycle ());
			panelSowing.GetComponent<SowingManager> ().SetNewPlantIndex (4);
			panelSowing.GetComponent<SowingManager> ().CheckForFreeSlots ();
		}
		else
		{
			panelNoResources.gameObject.SetActive (true);
		}
	}

	public void SowSabi()
	{
		if (AllGameData.instance.playerData.flowerPots > 0 && AllGameData.instance.playerData.seedEnvelopes [5] > 0)
		{
			panelRegularView.gameObject.SetActive (false);
			panelStats.gameObject.SetActive (false);
			panelInventory.gameObject.SetActive (false);
			panelStore.gameObject.SetActive (false);
			panelTrophies.gameObject.SetActive (false);
			panelNotEnoughMoney.gameObject.SetActive (false);
			panelNoResources.gameObject.SetActive (false);
			panelFertilizing.gameObject.SetActive (false);
			panelWatering.gameObject.SetActive(false);
			panelInventory.gameObject.SetActive (false);

			panelSowing.gameObject.SetActive (true);
			StartCoroutine (WaitOneUpdateCycle ());
			panelSowing.GetComponent<SowingManager> ().SetNewPlantIndex (5);
			panelSowing.GetComponent<SowingManager> ().CheckForFreeSlots ();
		}
		else
		{
			panelNoResources.gameObject.SetActive (true);
		}
	}

	public void putManure()
	{
		if (AllGameData.instance.playerData.manure > 0)
		{
			panelRegularView.gameObject.SetActive (false);
			panelStats.gameObject.SetActive (false);
			panelInventory.gameObject.SetActive (false);
			panelStore.gameObject.SetActive (false);
			panelTrophies.gameObject.SetActive (false);
			panelNotEnoughMoney.gameObject.SetActive (false);
			panelNoResources.gameObject.SetActive (false);
			panelSowing.gameObject.SetActive (false);
			panelWatering.gameObject.SetActive(false);
			panelInventory.gameObject.SetActive (false);

			panelFertilizing.gameObject.SetActive (true);
			StartCoroutine (WaitOneUpdateCycle ());
			panelFertilizing.GetComponent<FertilizeManager> ().CheckForFreeSlots ();
		}
	}

	public void WaterPlant()
	{
		panelRegularView.gameObject.SetActive (false);
		panelStats.gameObject.SetActive (false);
		panelInventory.gameObject.SetActive (false);
		panelStore.gameObject.SetActive (false);
		panelTrophies.gameObject.SetActive (false);
		panelNotEnoughMoney.gameObject.SetActive (false);
		panelNoResources.gameObject.SetActive (false);
		panelSowing.gameObject.SetActive (false);
		panelFertilizing.gameObject.SetActive (false);
		panelInventory.gameObject.SetActive (false);

		panelWatering.gameObject.SetActive (true);
		StartCoroutine (WaitOneUpdateCycle ());
		panelWatering.GetComponent<WateringManager> ().CheckForFreeSlots ();
	}

	public void BuyManure()
	{
		if (AllGameData.instance.playerData.koins >= 250)
		{
			//Internally...
			AllGameData.instance.playerData.manure += 1;
			AllGameData.instance.playerData.koins -= 250;
			AllGameData.instance.SaveData ();

			//Visually...
			panelStore.GetChild (4).GetChild (1).GetComponent<Text> ().text = AllGameData.instance.playerData.koins.ToString ();
		}
		else
		{
			panelNotEnoughMoney.gameObject.SetActive (true);
		}
	}

	public void BuyPots()
	{
		if (AllGameData.instance.playerData.koins >= 50)
		{
			//Internally...
			AllGameData.instance.playerData.flowerPots += 1;
			AllGameData.instance.playerData.koins -= 50;
			AllGameData.instance.SaveData ();

			//Visually...
			panelStore.GetChild (4).GetChild (1).GetComponent<Text> ().text = AllGameData.instance.playerData.koins.ToString ();
		}
		else
		{
			panelNotEnoughMoney.gameObject.SetActive (true);
		}
	}

	public void BuyFirstItem()
	{
		if (AllGameData.instance.playerData.koins >= 100)
		{
			short auxIndex;
			auxIndex = panelStore.GetComponent<StoreManager> ().orderOfSprites;
			//Internally...
			AllGameData.instance.playerData.seedEnvelopes [auxIndex] += 1;
			AllGameData.instance.playerData.koins -= 100;
			AllGameData.instance.SaveData ();

			//Visually...
			panelStore.GetChild (4).GetChild (1).GetComponent<Text> ().text = AllGameData.instance.playerData.koins.ToString ();
		}
		else
		{
			panelNotEnoughMoney.gameObject.SetActive (true);
		}
	}

	public void BuySecondItem()
	{
		if (AllGameData.instance.playerData.koins >= 100)
		{
			short auxIndex;
			auxIndex = panelStore.GetComponent<StoreManager> ().orderOfSprites;
			auxIndex += 1;

			if (auxIndex >= 6)
			{
				auxIndex -= 6;
			}

			//Internally...
			AllGameData.instance.playerData.seedEnvelopes [auxIndex] += 1;
			AllGameData.instance.playerData.koins -= 100;
			AllGameData.instance.SaveData ();

			//Visually...
			panelStore.GetChild (4).GetChild (1).GetComponent<Text> ().text = AllGameData.instance.playerData.koins.ToString ();
		}
		else
		{
			panelNotEnoughMoney.gameObject.SetActive (true);
		}
	}

	public void BuyThirdItem()
	{
		if (AllGameData.instance.playerData.koins >= 100)
		{
			short auxIndex;
			auxIndex = panelStore.GetComponent<StoreManager> ().orderOfSprites;
			auxIndex += 2;

			if (auxIndex >= 6)
			{
				auxIndex -= 6;
			}

			//Internally...
			AllGameData.instance.playerData.seedEnvelopes [auxIndex] += 1;
			AllGameData.instance.playerData.koins -= 100;
			AllGameData.instance.SaveData ();

			//Visually...
			panelStore.GetChild (4).GetChild (1).GetComponent<Text> ().text = AllGameData.instance.playerData.koins.ToString ();
		}
		else
		{
			panelNotEnoughMoney.gameObject.SetActive (true);
		}
	}

	public void StartSlotOne()
	{
		string auxNewName = "";

		switch (panelSowing.GetComponent<SowingManager> ().indexForNewPlant)
		{
		case 0:
			auxNewName = "Cactus";
			break;
		case 1:
			auxNewName = "Cempasuchil";
			break;
		case 2:
			auxNewName = "Girasol";
			break;
		case 3:
			auxNewName = "Narciso";
			break;
		case 4:
			auxNewName = "Nochebuena";
			break;
		case 5:
			auxNewName = "Sabila";
			break;
		}

		AllGameData.instance.playerData.plant_isUsed [0] = true;
		AllGameData.instance.playerData.plant_name [0] = auxNewName;
		AllGameData.instance.playerData.plant_phase [0] = 0;

		AllGameData.instance.playerData.plant_yearDate [0] = System.DateTime.Now.Year;
		AllGameData.instance.playerData.plant_monthDate [0] = System.DateTime.Now.Month;
		AllGameData.instance.playerData.plant_dayDate [0] = System.DateTime.Now.Day;
		AllGameData.instance.playerData.plant_tickDate [0] = (ulong)System.DateTime.Now.Ticks;
		AllGameData.instance.playerData.plant_lastTick [0] = (ulong)System.DateTime.Now.Ticks;

		AllGameData.instance.playerData.plant_timeLeftSeconds[0] = 30;

		AllGameData.instance.playerData.seedEnvelopes [panelSowing.GetComponent<SowingManager> ().indexForNewPlant] -= 1;
		AllGameData.instance.playerData.flowerPots -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void StartSlotTwo()
	{
		string auxNewName = "";

		switch (panelSowing.GetComponent<SowingManager> ().indexForNewPlant)
		{
		case 0:
			auxNewName = "Cactus";
			break;
		case 1:
			auxNewName = "Cempasuchil";
			break;
		case 2:
			auxNewName = "Girasol";
			break;
		case 3:
			auxNewName = "Narciso";
			break;
		case 4:
			auxNewName = "Nochebuena";
			break;
		case 5:
			auxNewName = "Sabila";
			break;
		}

		AllGameData.instance.playerData.plant_isUsed [1] = true;
		AllGameData.instance.playerData.plant_name [1] = auxNewName;
		AllGameData.instance.playerData.plant_phase [1] = 0;

		AllGameData.instance.playerData.plant_yearDate [1] = System.DateTime.Now.Year;
		AllGameData.instance.playerData.plant_monthDate [1] = System.DateTime.Now.Month;
		AllGameData.instance.playerData.plant_dayDate [1] = System.DateTime.Now.Day;
		AllGameData.instance.playerData.plant_tickDate [1] = (ulong)System.DateTime.Now.Ticks;
		AllGameData.instance.playerData.plant_lastTick [1] = (ulong)System.DateTime.Now.Ticks;

		AllGameData.instance.playerData.plant_timeLeftSeconds[1] = 30;

		AllGameData.instance.playerData.seedEnvelopes [panelSowing.GetComponent<SowingManager> ().indexForNewPlant] -= 1;
		AllGameData.instance.playerData.flowerPots -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void StartSlotThree()
	{
		string auxNewName = "";

		switch (panelSowing.GetComponent<SowingManager> ().indexForNewPlant)
		{
		case 0:
			auxNewName = "Cactus";
			break;
		case 1:
			auxNewName = "Cempasuchil";
			break;
		case 2:
			auxNewName = "Girasol";
			break;
		case 3:
			auxNewName = "Narciso";
			break;
		case 4:
			auxNewName = "Nochebuena";
			break;
		case 5:
			auxNewName = "Sabila";
			break;
		}

		AllGameData.instance.playerData.plant_isUsed [2] = true;
		AllGameData.instance.playerData.plant_name [2] = auxNewName;
		AllGameData.instance.playerData.plant_phase [2] = 0;

		AllGameData.instance.playerData.plant_yearDate [2] = System.DateTime.Now.Year;
		AllGameData.instance.playerData.plant_monthDate [2] = System.DateTime.Now.Month;
		AllGameData.instance.playerData.plant_dayDate [2] = System.DateTime.Now.Day;
		AllGameData.instance.playerData.plant_tickDate [2] = (ulong)System.DateTime.Now.Ticks;
		AllGameData.instance.playerData.plant_lastTick [2] = (ulong)System.DateTime.Now.Ticks;

		AllGameData.instance.playerData.plant_timeLeftSeconds[2] = 30;

		AllGameData.instance.playerData.seedEnvelopes [panelSowing.GetComponent<SowingManager> ().indexForNewPlant] -= 1;
		AllGameData.instance.playerData.flowerPots -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void StartSlotFour()
	{
		string auxNewName = "";

		switch (panelSowing.GetComponent<SowingManager> ().indexForNewPlant)
		{
		case 0:
			auxNewName = "Cactus";
			break;
		case 1:
			auxNewName = "Cempasuchil";
			break;
		case 2:
			auxNewName = "Girasol";
			break;
		case 3:
			auxNewName = "Narciso";
			break;
		case 4:
			auxNewName = "Nochebuena";
			break;
		case 5:
			auxNewName = "Sabila";
			break;
		}

		AllGameData.instance.playerData.plant_isUsed [3] = true;
		AllGameData.instance.playerData.plant_name [3] = auxNewName;
		AllGameData.instance.playerData.plant_phase [3] = 0;

		AllGameData.instance.playerData.plant_yearDate [3] = System.DateTime.Now.Year;
		AllGameData.instance.playerData.plant_monthDate [3] = System.DateTime.Now.Month;
		AllGameData.instance.playerData.plant_dayDate [3] = System.DateTime.Now.Day;
		AllGameData.instance.playerData.plant_tickDate [3] = (ulong)System.DateTime.Now.Ticks;
		AllGameData.instance.playerData.plant_lastTick [3] = (ulong)System.DateTime.Now.Ticks;

		AllGameData.instance.playerData.plant_timeLeftSeconds[3] = 30;

		AllGameData.instance.playerData.seedEnvelopes [panelSowing.GetComponent<SowingManager> ().indexForNewPlant] -= 1;
		AllGameData.instance.playerData.flowerPots -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void StartSlotFive()
	{
		string auxNewName = "";

		switch (panelSowing.GetComponent<SowingManager> ().indexForNewPlant)
		{
		case 0:
			auxNewName = "Cactus";
			break;
		case 1:
			auxNewName = "Cempasuchil";
			break;
		case 2:
			auxNewName = "Girasol";
			break;
		case 3:
			auxNewName = "Narciso";
			break;
		case 4:
			auxNewName = "Nochebuena";
			break;
		case 5:
			auxNewName = "Sabila";
			break;
		}

		AllGameData.instance.playerData.plant_isUsed [4] = true;
		AllGameData.instance.playerData.plant_name [4] = auxNewName;
		AllGameData.instance.playerData.plant_phase [4] = 0;

		AllGameData.instance.playerData.plant_yearDate [4] = System.DateTime.Now.Year;
		AllGameData.instance.playerData.plant_monthDate [4] = System.DateTime.Now.Month;
		AllGameData.instance.playerData.plant_dayDate [4] = System.DateTime.Now.Day;
		AllGameData.instance.playerData.plant_tickDate [4] = (ulong)System.DateTime.Now.Ticks;
		AllGameData.instance.playerData.plant_lastTick [4] = (ulong)System.DateTime.Now.Ticks;

		AllGameData.instance.playerData.plant_timeLeftSeconds[4] = 30;

		AllGameData.instance.playerData.seedEnvelopes [panelSowing.GetComponent<SowingManager> ().indexForNewPlant] -= 1;
		AllGameData.instance.playerData.flowerPots -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void StartSlotSix()
	{
		string auxNewName = "";

		switch (panelSowing.GetComponent<SowingManager> ().indexForNewPlant)
		{
		case 0:
			auxNewName = "Cactus";
			break;
		case 1:
			auxNewName = "Cempasuchil";
			break;
		case 2:
			auxNewName = "Girasol";
			break;
		case 3:
			auxNewName = "Narciso";
			break;
		case 4:
			auxNewName = "Nochebuena";
			break;
		case 5:
			auxNewName = "Sabila";
			break;
		}

		AllGameData.instance.playerData.plant_isUsed [5] = true;
		AllGameData.instance.playerData.plant_name [5] = auxNewName;
		AllGameData.instance.playerData.plant_phase [5] = 0;

		AllGameData.instance.playerData.plant_yearDate [5] = System.DateTime.Now.Year;
		AllGameData.instance.playerData.plant_monthDate [5] = System.DateTime.Now.Month;
		AllGameData.instance.playerData.plant_dayDate [5] = System.DateTime.Now.Day;
		AllGameData.instance.playerData.plant_tickDate [5] = (ulong)System.DateTime.Now.Ticks;
		AllGameData.instance.playerData.plant_lastTick [5] = (ulong)System.DateTime.Now.Ticks;

		AllGameData.instance.playerData.plant_timeLeftSeconds[5] = 30;

		AllGameData.instance.playerData.seedEnvelopes [panelSowing.GetComponent<SowingManager> ().indexForNewPlant] -= 1;
		AllGameData.instance.playerData.flowerPots -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void StartSlotSeven()
	{
		string auxNewName = "";

		switch (panelSowing.GetComponent<SowingManager> ().indexForNewPlant)
		{
		case 0:
			auxNewName = "Cactus";
			break;
		case 1:
			auxNewName = "Cempasuchil";
			break;
		case 2:
			auxNewName = "Girasol";
			break;
		case 3:
			auxNewName = "Narciso";
			break;
		case 4:
			auxNewName = "Nochebuena";
			break;
		case 5:
			auxNewName = "Sabila";
			break;
		}

		AllGameData.instance.playerData.plant_isUsed [6] = true;
		AllGameData.instance.playerData.plant_name [6] = auxNewName;
		AllGameData.instance.playerData.plant_phase [6] = 0;

		AllGameData.instance.playerData.plant_yearDate [6] = System.DateTime.Now.Year;
		AllGameData.instance.playerData.plant_monthDate [6] = System.DateTime.Now.Month;
		AllGameData.instance.playerData.plant_dayDate [6] = System.DateTime.Now.Day;
		AllGameData.instance.playerData.plant_tickDate [6] = (ulong)System.DateTime.Now.Ticks;
		AllGameData.instance.playerData.plant_lastTick [6] = (ulong)System.DateTime.Now.Ticks;

		AllGameData.instance.playerData.plant_timeLeftSeconds[6] = 30;

		AllGameData.instance.playerData.seedEnvelopes [panelSowing.GetComponent<SowingManager> ().indexForNewPlant] -= 1;
		AllGameData.instance.playerData.flowerPots -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void StartSlotEight()
	{
		string auxNewName = "";

		switch (panelSowing.GetComponent<SowingManager> ().indexForNewPlant)
		{
		case 0:
			auxNewName = "Cactus";
			break;
		case 1:
			auxNewName = "Cempasuchil";
			break;
		case 2:
			auxNewName = "Girasol";
			break;
		case 3:
			auxNewName = "Narciso";
			break;
		case 4:
			auxNewName = "Nochebuena";
			break;
		case 5:
			auxNewName = "Sabila";
			break;
		}

		AllGameData.instance.playerData.plant_isUsed [7] = true;
		AllGameData.instance.playerData.plant_name [7] = auxNewName;
		AllGameData.instance.playerData.plant_phase [7] = 0;

		AllGameData.instance.playerData.plant_yearDate [7] = System.DateTime.Now.Year;
		AllGameData.instance.playerData.plant_monthDate [7] = System.DateTime.Now.Month;
		AllGameData.instance.playerData.plant_dayDate [7] = System.DateTime.Now.Day;
		AllGameData.instance.playerData.plant_tickDate [7] = (ulong)System.DateTime.Now.Ticks;
		AllGameData.instance.playerData.plant_lastTick [7] = (ulong)System.DateTime.Now.Ticks;

		AllGameData.instance.playerData.plant_timeLeftSeconds[7] = 30;

		AllGameData.instance.playerData.seedEnvelopes [panelSowing.GetComponent<SowingManager> ().indexForNewPlant] -= 1;
		AllGameData.instance.playerData.flowerPots -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void StartSlotNine()
	{
		string auxNewName = "";

		switch (panelSowing.GetComponent<SowingManager> ().indexForNewPlant)
		{
		case 0:
			auxNewName = "Cactus";
			break;
		case 1:
			auxNewName = "Cempasuchil";
			break;
		case 2:
			auxNewName = "Girasol";
			break;
		case 3:
			auxNewName = "Narciso";
			break;
		case 4:
			auxNewName = "Nochebuena";
			break;
		case 5:
			auxNewName = "Sabila";
			break;
		}

		AllGameData.instance.playerData.plant_isUsed [8] = true;
		AllGameData.instance.playerData.plant_name [8] = auxNewName;
		AllGameData.instance.playerData.plant_phase [8] = 0;

		AllGameData.instance.playerData.plant_yearDate [8] = System.DateTime.Now.Year;
		AllGameData.instance.playerData.plant_monthDate [8] = System.DateTime.Now.Month;
		AllGameData.instance.playerData.plant_dayDate [8] = System.DateTime.Now.Day;
		AllGameData.instance.playerData.plant_tickDate [8] = (ulong)System.DateTime.Now.Ticks;
		AllGameData.instance.playerData.plant_lastTick [8] = (ulong)System.DateTime.Now.Ticks;

		AllGameData.instance.playerData.plant_timeLeftSeconds[8] = 30;

		AllGameData.instance.playerData.seedEnvelopes [panelSowing.GetComponent<SowingManager> ().indexForNewPlant] -= 1;
		AllGameData.instance.playerData.flowerPots -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void FertilizeOne()
	{
		AllGameData.instance.playerData.plant_timeLeftSeconds [0] = 0;
		AllGameData.instance.playerData.manure -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void FertilizeTwo()
	{
		AllGameData.instance.playerData.plant_timeLeftSeconds [1] = 0;
		AllGameData.instance.playerData.manure -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void FertilizeThree()
	{
		AllGameData.instance.playerData.plant_timeLeftSeconds [2] = 0;
		AllGameData.instance.playerData.manure -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void FertilizeFour()
	{
		AllGameData.instance.playerData.plant_timeLeftSeconds [3] = 0;
		AllGameData.instance.playerData.manure -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void FertilizeFive()
	{
		AllGameData.instance.playerData.plant_timeLeftSeconds [4] = 0;
		AllGameData.instance.playerData.manure -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void FertilizeSix()
	{
		AllGameData.instance.playerData.plant_timeLeftSeconds [5] = 0;
		AllGameData.instance.playerData.manure -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void FertilizeSeven()
	{
		AllGameData.instance.playerData.plant_timeLeftSeconds [6] = 0;
		AllGameData.instance.playerData.manure -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void FertilizeEight()
	{
		AllGameData.instance.playerData.plant_timeLeftSeconds [7] = 0;
		AllGameData.instance.playerData.manure -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void FertilizeNine()
	{
		AllGameData.instance.playerData.plant_timeLeftSeconds [8] = 0;
		AllGameData.instance.playerData.manure -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void WaterOne()
	{
		short auxPhase;
		int newTimeLeft;
		auxPhase = AllGameData.instance.playerData.plant_phase [0];
		newTimeLeft = 0;

		switch (auxPhase)
		{
		case 0:
			newTimeLeft = 60;
			AllGameData.instance.playerData.plant_timeLeftSeconds [0] = newTimeLeft;
			break;
		case 1:
			newTimeLeft = 120;
			AllGameData.instance.playerData.plant_timeLeftSeconds [0] = newTimeLeft;
			break;
		case 2:
			newTimeLeft = 300;
			AllGameData.instance.playerData.plant_timeLeftSeconds [0] = newTimeLeft;
			break;
		case 3:
			newTimeLeft = 600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [0] = newTimeLeft;
			break;
		case 4:
			newTimeLeft = 1800;
			AllGameData.instance.playerData.plant_timeLeftSeconds [0] = newTimeLeft;
			break;
		case 5:
			newTimeLeft = 3600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [0] = newTimeLeft;
			break;
		case 6:
			newTimeLeft = 10800;
			AllGameData.instance.playerData.plant_timeLeftSeconds [0] = newTimeLeft;
			break;
		case 7:
			newTimeLeft = 21600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [0] = newTimeLeft;
			break;
		case 8:
			newTimeLeft = 43200;
			AllGameData.instance.playerData.plant_timeLeftSeconds [0] = newTimeLeft;
			break;
		}

		AllGameData.instance.playerData.plant_phase [0] += (short)1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void WaterTwo()
	{
		short auxPhase;
		int newTimeLeft;
		auxPhase = AllGameData.instance.playerData.plant_phase [1];
		newTimeLeft = 0;

		switch (auxPhase)
		{
		case 0:
			newTimeLeft = 60;
			AllGameData.instance.playerData.plant_timeLeftSeconds [1] = newTimeLeft;
			break;
		case 1:
			newTimeLeft = 120;
			AllGameData.instance.playerData.plant_timeLeftSeconds [1] = newTimeLeft;
			break;
		case 2:
			newTimeLeft = 300;
			AllGameData.instance.playerData.plant_timeLeftSeconds [1] = newTimeLeft;
			break;
		case 3:
			newTimeLeft = 600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [1] = newTimeLeft;
			break;
		case 4:
			newTimeLeft = 1800;
			AllGameData.instance.playerData.plant_timeLeftSeconds [1] = newTimeLeft;
			break;
		case 5:
			newTimeLeft = 3600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [1] = newTimeLeft;
			break;
		case 6:
			newTimeLeft = 10800;
			AllGameData.instance.playerData.plant_timeLeftSeconds [1] = newTimeLeft;
			break;
		case 7:
			newTimeLeft = 21600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [1] = newTimeLeft;
			break;
		case 8:
			newTimeLeft = 43200;
			AllGameData.instance.playerData.plant_timeLeftSeconds [1] = newTimeLeft;
			break;
		}

		AllGameData.instance.playerData.plant_phase [1] += (short)1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void WaterThree()
	{
		short auxPhase;
		int newTimeLeft;
		auxPhase = AllGameData.instance.playerData.plant_phase [2];
		newTimeLeft = 0;

		switch (auxPhase)
		{
		case 0:
			newTimeLeft = 60;
			AllGameData.instance.playerData.plant_timeLeftSeconds [2] = newTimeLeft;
			break;
		case 1:
			newTimeLeft = 120;
			AllGameData.instance.playerData.plant_timeLeftSeconds [2] = newTimeLeft;
			break;
		case 2:
			newTimeLeft = 300;
			AllGameData.instance.playerData.plant_timeLeftSeconds [2] = newTimeLeft;
			break;
		case 3:
			newTimeLeft = 600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [2] = newTimeLeft;
			break;
		case 4:
			newTimeLeft = 1800;
			AllGameData.instance.playerData.plant_timeLeftSeconds [2] = newTimeLeft;
			break;
		case 5:
			newTimeLeft = 3600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [2] = newTimeLeft;
			break;
		case 6:
			newTimeLeft = 10800;
			AllGameData.instance.playerData.plant_timeLeftSeconds [2] = newTimeLeft;
			break;
		case 7:
			newTimeLeft = 21600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [2] = newTimeLeft;
			break;
		case 8:
			newTimeLeft = 43200;
			AllGameData.instance.playerData.plant_timeLeftSeconds [2] = newTimeLeft;
			break;
		}

		AllGameData.instance.playerData.plant_phase [2] += (short)1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void WaterFour()
	{
		short auxPhase;
		int newTimeLeft;
		auxPhase = AllGameData.instance.playerData.plant_phase [3];
		newTimeLeft = 0;

		switch (auxPhase)
		{
		case 0:
			newTimeLeft = 60;
			AllGameData.instance.playerData.plant_timeLeftSeconds [3] = newTimeLeft;
			break;
		case 1:
			newTimeLeft = 120;
			AllGameData.instance.playerData.plant_timeLeftSeconds [3] = newTimeLeft;
			break;
		case 2:
			newTimeLeft = 300;
			AllGameData.instance.playerData.plant_timeLeftSeconds [3] = newTimeLeft;
			break;
		case 3:
			newTimeLeft = 600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [3] = newTimeLeft;
			break;
		case 4:
			newTimeLeft = 1800;
			AllGameData.instance.playerData.plant_timeLeftSeconds [3] = newTimeLeft;
			break;
		case 5:
			newTimeLeft = 3600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [3] = newTimeLeft;
			break;
		case 6:
			newTimeLeft = 10800;
			AllGameData.instance.playerData.plant_timeLeftSeconds [3] = newTimeLeft;
			break;
		case 7:
			newTimeLeft = 21600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [3] = newTimeLeft;
			break;
		case 8:
			newTimeLeft = 43200;
			AllGameData.instance.playerData.plant_timeLeftSeconds [3] = newTimeLeft;
			break;
		}

		AllGameData.instance.playerData.plant_phase [3] += (short)1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void WaterFive()
	{
		short auxPhase;
		int newTimeLeft;
		auxPhase = AllGameData.instance.playerData.plant_phase [4];
		newTimeLeft = 0;

		switch (auxPhase)
		{
		case 0:
			newTimeLeft = 60;
			AllGameData.instance.playerData.plant_timeLeftSeconds [4] = newTimeLeft;
			break;
		case 1:
			newTimeLeft = 120;
			AllGameData.instance.playerData.plant_timeLeftSeconds [4] = newTimeLeft;
			break;
		case 2:
			newTimeLeft = 300;
			AllGameData.instance.playerData.plant_timeLeftSeconds [4] = newTimeLeft;
			break;
		case 3:
			newTimeLeft = 600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [4] = newTimeLeft;
			break;
		case 4:
			newTimeLeft = 1800;
			AllGameData.instance.playerData.plant_timeLeftSeconds [4] = newTimeLeft;
			break;
		case 5:
			newTimeLeft = 3600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [4] = newTimeLeft;
			break;
		case 6:
			newTimeLeft = 10800;
			AllGameData.instance.playerData.plant_timeLeftSeconds [4] = newTimeLeft;
			break;
		case 7:
			newTimeLeft = 21600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [4] = newTimeLeft;
			break;
		case 8:
			newTimeLeft = 43200;
			AllGameData.instance.playerData.plant_timeLeftSeconds [4] = newTimeLeft;
			break;
		}

		AllGameData.instance.playerData.plant_phase [4] += (short)1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void WaterSix()
	{
		short auxPhase;
		int newTimeLeft;
		auxPhase = AllGameData.instance.playerData.plant_phase [5];
		newTimeLeft = 0;

		switch (auxPhase)
		{
		case 0:
			newTimeLeft = 60;
			AllGameData.instance.playerData.plant_timeLeftSeconds [5] = newTimeLeft;
			break;
		case 1:
			newTimeLeft = 120;
			AllGameData.instance.playerData.plant_timeLeftSeconds [5] = newTimeLeft;
			break;
		case 2:
			newTimeLeft = 300;
			AllGameData.instance.playerData.plant_timeLeftSeconds [5] = newTimeLeft;
			break;
		case 3:
			newTimeLeft = 600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [5] = newTimeLeft;
			break;
		case 4:
			newTimeLeft = 1800;
			AllGameData.instance.playerData.plant_timeLeftSeconds [5] = newTimeLeft;
			break;
		case 5:
			newTimeLeft = 3600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [5] = newTimeLeft;
			break;
		case 6:
			newTimeLeft = 10800;
			AllGameData.instance.playerData.plant_timeLeftSeconds [5] = newTimeLeft;
			break;
		case 7:
			newTimeLeft = 21600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [5] = newTimeLeft;
			break;
		case 8:
			newTimeLeft = 43200;
			AllGameData.instance.playerData.plant_timeLeftSeconds [5] = newTimeLeft;
			break;
		}

		AllGameData.instance.playerData.plant_phase [5] += (short)1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void WaterSeven()
	{
		short auxPhase;
		int newTimeLeft;
		auxPhase = AllGameData.instance.playerData.plant_phase [6];
		newTimeLeft = 0;

		switch (auxPhase)
		{
		case 0:
			newTimeLeft = 60;
			AllGameData.instance.playerData.plant_timeLeftSeconds [6] = newTimeLeft;
			break;
		case 1:
			newTimeLeft = 120;
			AllGameData.instance.playerData.plant_timeLeftSeconds [6] = newTimeLeft;
			break;
		case 2:
			newTimeLeft = 300;
			AllGameData.instance.playerData.plant_timeLeftSeconds [6] = newTimeLeft;
			break;
		case 3:
			newTimeLeft = 600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [6] = newTimeLeft;
			break;
		case 4:
			newTimeLeft = 1800;
			AllGameData.instance.playerData.plant_timeLeftSeconds [6] = newTimeLeft;
			break;
		case 5:
			newTimeLeft = 3600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [6] = newTimeLeft;
			break;
		case 6:
			newTimeLeft = 10800;
			AllGameData.instance.playerData.plant_timeLeftSeconds [6] = newTimeLeft;
			break;
		case 7:
			newTimeLeft = 21600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [6] = newTimeLeft;
			break;
		case 8:
			newTimeLeft = 43200;
			AllGameData.instance.playerData.plant_timeLeftSeconds [6] = newTimeLeft;
			break;
		}

		AllGameData.instance.playerData.plant_phase [6] += (short)1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void WaterEight()
	{
		short auxPhase;
		int newTimeLeft;
		auxPhase = AllGameData.instance.playerData.plant_phase [7];
		newTimeLeft = 0;

		switch (auxPhase)
		{
		case 0:
			newTimeLeft = 60;
			AllGameData.instance.playerData.plant_timeLeftSeconds [7] = newTimeLeft;
			break;
		case 1:
			newTimeLeft = 120;
			AllGameData.instance.playerData.plant_timeLeftSeconds [7] = newTimeLeft;
			break;
		case 2:
			newTimeLeft = 300;
			AllGameData.instance.playerData.plant_timeLeftSeconds [7] = newTimeLeft;
			break;
		case 3:
			newTimeLeft = 600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [7] = newTimeLeft;
			break;
		case 4:
			newTimeLeft = 1800;
			AllGameData.instance.playerData.plant_timeLeftSeconds [7] = newTimeLeft;
			break;
		case 5:
			newTimeLeft = 3600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [7] = newTimeLeft;
			break;
		case 6:
			newTimeLeft = 10800;
			AllGameData.instance.playerData.plant_timeLeftSeconds [7] = newTimeLeft;
			break;
		case 7:
			newTimeLeft = 21600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [7] = newTimeLeft;
			break;
		case 8:
			newTimeLeft = 43200;
			AllGameData.instance.playerData.plant_timeLeftSeconds [7] = newTimeLeft;
			break;
		}

		AllGameData.instance.playerData.plant_phase [7] += (short)1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void WaterNine()
	{
		short auxPhase;
		int newTimeLeft;
		auxPhase = AllGameData.instance.playerData.plant_phase [8];
		newTimeLeft = 0;

		switch (auxPhase)
		{
		case 0:
			newTimeLeft = 60;
			AllGameData.instance.playerData.plant_timeLeftSeconds [8] = newTimeLeft;
			break;
		case 1:
			newTimeLeft = 120;
			AllGameData.instance.playerData.plant_timeLeftSeconds [8] = newTimeLeft;
			break;
		case 2:
			newTimeLeft = 300;
			AllGameData.instance.playerData.plant_timeLeftSeconds [8] = newTimeLeft;
			break;
		case 3:
			newTimeLeft = 600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [8] = newTimeLeft;
			break;
		case 4:
			newTimeLeft = 1800;
			AllGameData.instance.playerData.plant_timeLeftSeconds [8] = newTimeLeft;
			break;
		case 5:
			newTimeLeft = 3600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [8] = newTimeLeft;
			break;
		case 6:
			newTimeLeft = 10800;
			AllGameData.instance.playerData.plant_timeLeftSeconds [8] = newTimeLeft;
			break;
		case 7:
			newTimeLeft = 21600;
			AllGameData.instance.playerData.plant_timeLeftSeconds [8] = newTimeLeft;
			break;
		case 8:
			newTimeLeft = 43200;
			AllGameData.instance.playerData.plant_timeLeftSeconds [8] = newTimeLeft;
			break;
		}

		AllGameData.instance.playerData.plant_phase [8] += (short)1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}


	public IEnumerator WaitOneUpdateCycle()
	{
		yield return null;
	}

	IEnumerator CalculateTime()
	{
		while(true)
		{
			//timeLeft.text = Application.persistentDataPath;
			//timeLeft.text = AllGameData.instance.playerData.plant_timeLeftSeconds [0].ToString ();
			yield return null;
			timeCounter += Time.deltaTime;

			if (timeCounter >= 1f)
			{
				timeCounter = 0f;

				for (int i = 0; i < 9; i++)
				{

					if (AllGameData.instance.playerData.plant_isUsed [i] && AllGameData.instance.playerData.plant_timeLeftSeconds [i] > 0)
					{
						AllGameData.instance.playerData.plant_timeLeftSeconds [i] -= 1;
						AllGameData.instance.SaveData ();
					}

				}
			}
		}
	}

	IEnumerator ShareScreenshot()
	{
		isProcessing = true;

		yield return new WaitForEndOfFrame();

		ScreenCapture.CaptureScreenshot("screenshot.png", 2);
		string destination = Application.persistentDataPath + "/" + "screenshot.png";

		yield return new WaitForSecondsRealtime(0.3f);


		#if UNITY_ANDROID


		//Do we really go in here?
		AllGameData.instance.playerData.trophy_name[0] = "Entered at facebook button";
		AllGameData.instance.SaveData();

		//Reference of AndroidJavaClass class for intent
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");

		//Reference of AndroidJavaObject class for intent
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

		//Call setAction method of the Intent object created
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

		//The uri class/object is used for... erm, somehow detecting the screenshot we took
		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
		AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", destination);

		//set the type of sharing that is happening
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"),
			uriObject);

		//add data to be passed to the other activity i.e., the data to be sent
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"),
			"¡Mira mi PlantuKa!");
		intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");

		//get the current activity
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

		//start the activity by sending the intent data
		AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser",
			intentObject, "Comparte tu progreso");
		currentActivity.Call("startActivity", chooser);
		yield return new WaitForSecondsRealtime(1);


		#endif

		yield return new WaitUntil(() => isFocus);
		//CanvasShareObj.SetActive(false);
		isProcessing = false;
	}

	void OnApplicationFocus(bool hasFocus)
	{
		isFocus = hasFocus;


		if (!hasFocus)
		{
			//isPaused = !hasFocus;
			//Debug.Log ("PAUSA");

			for (int i = 0; i < 9; i++)
			{
				if (AllGameData.instance.playerData.plant_isUsed[i] && AllGameData.instance.playerData.plant_timeLeftSeconds[i] > 0)
				{
					//AllGameData.instance.playerData.plant_timeLeftSeconds [i];
					AllGameData.instance.playerData.plant_lastTick [i] = (ulong)DateTime.Now.Ticks;

					AllGameData.instance.SaveData ();
				}

			}
		}
		else
		{
			//Debug.Log ("Reanuda");
			SustractTime();
			timeCounter = 0f;
			//StartCoroutine (CalculateTime ());
		}
		//isPaused = !hasFocus;

	}

	void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			//isPaused = pauseStatus;
			//Debug.Log ("PAUSA");

			for (int i = 0; i < 9; i++)
			{
				if (AllGameData.instance.playerData.plant_isUsed[i] && AllGameData.instance.playerData.plant_timeLeftSeconds[i] > 0)
				{
					//AllGameData.instance.playerData.plant_timeLeftSeconds [i];
					AllGameData.instance.playerData.plant_lastTick [i] = (ulong)DateTime.Now.Ticks;

					AllGameData.instance.SaveData ();
				}

			}
		}
		else
		{
			//Debug.Log ("Reanuda");
			SustractTime();
			timeCounter = 0f;
			//StartCoroutine (CalculateTime ());
		}
	}

	void OnApplicationQuit()
	{
		for (int i = 0; i < 9; i++)
		{
			if (AllGameData.instance.playerData.plant_isUsed[i] && AllGameData.instance.playerData.plant_timeLeftSeconds[i] > 0)
			{
				//AllGameData.instance.playerData.plant_timeLeftSeconds [i];
				AllGameData.instance.playerData.plant_lastTick [i] = (ulong)DateTime.Now.Ticks;

				AllGameData.instance.SaveData ();
			}

		}
	}
}
