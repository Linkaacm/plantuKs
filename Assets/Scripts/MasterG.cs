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
	public Transform panelRearrangeBegins;
	public Transform panelRearrangeFinishes;
	public Transform panelCongratulations;

	public Transform slots;

	public GameObject WateringCanPrefab;

	public Sprite[] spritesAtCongrats;

	//public SpriteRenderer activeMe;

	private float timeCounter;
	private int indexCongratulations = 0;
	//private bool isPaused = false;
	private bool isProcessing = false;
	private bool isFocus = false;
	private bool celebrateHoliday = false;
	private bool isWatering = false;

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

	void Update ()
	{
		CheckForMaxPlant ();
	}

	public void CheckForMaxPlant()
	{
		for (int i = 0; i < 9; i++)
		{
			if (AllGameData.instance.playerData.plant_isUsed [i] && AllGameData.instance.playerData.plant_phase [i] == (short)10 && AllGameData.instance.playerData.plant_congratulations[i] == false && celebrateHoliday == false && isWatering == false)
			{

				LeaveAllPanels ();
				panelCongratulations.gameObject.SetActive (true);
				//this.transform.GetChild (0).gameObject.SetActive (true);
				//this.transform.GetChild (1).gameObject.SetActive (true);
				//this.transform.GetChild (2).gameObject.SetActive (true);
				//his.transform.GetChild (3).gameObject.SetActive (true);
				DetermineRenderCongrats (i);


				//AllGameData.instance.playerData.plant_congratulations [i] = true;
				//celebrateHoliday = true;
				indexCongratulations = i;
				//AllGameData.instance.SaveData ();

				//this.enabled = false;


			}
		}
	}

	void DetermineRenderCongrats(int index)
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

		panelCongratulations.GetChild (1).GetComponent<Image> ().sprite = spritesAtCongrats [spriteIndex];
	}


	//TOP SECRET
	public void TOPSECRET()
	{
		AllGameData.instance.playerData.koins += 1000;
		AllGameData.instance.SaveData ();
	}

	public void LeaveAllPanels()
	{
		panelStats.gameObject.SetActive (false);
		panelRegularView.gameObject.SetActive (false);
		panelInventory.gameObject.SetActive (false);
		panelTrophies.gameObject.SetActive (false);
		panelStore.gameObject.SetActive (false);
		panelNotEnoughMoney.gameObject.SetActive (false);
		panelSowing.gameObject.SetActive (false);
		panelNoResources.gameObject.SetActive (false);
		panelFertilizing.gameObject.SetActive (false);
		panelWatering.gameObject.SetActive (false);
		panelRearrangeBegins.gameObject.SetActive (false);
		panelRearrangeFinishes.gameObject.SetActive (false);
		panelCongratulations.gameObject.SetActive (false);
	}

	public void LeaveInfo()
	{
		LeaveAllPanels ();
		panelRegularView.gameObject.SetActive (true);
		slots.gameObject.SetActive (true);
		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();

	}

	public void ReturnFromCongrats()
	{
		
		LeaveInfo ();

		AllGameData.instance.playerData.plant_congratulations [indexCongratulations] = true;
		AllGameData.instance.SaveData ();
		celebrateHoliday = false;
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
		LeaveAllPanels ();
		panelInventory.gameObject.SetActive (true);

		StartCoroutine (WaitOneUpdateCycle ());
		panelInventory.GetComponent<InventoryManager> ().UpdateAllText ();
	}

	public void goToStore()
	{
		LeaveAllPanels ();
		panelStore.gameObject.SetActive (true);

		//Gets AvailableKoinsPanel then gets KoinsText gameObject to set the text
		StartCoroutine (WaitOneUpdateCycle ());
		panelStore.GetComponent<StoreManager> ().UpdateKoinText ();
		panelStore.GetComponent<StoreManager> ().UpdateOrderOfSprites (0);
		panelStore.GetComponent<StoreManager> ().UpdateSprites (0);
		//Debug.Log (panelStore.GetComponent<StoreManager> ().orderOfSprites);
	}
		
	public void goToTrophies()
	{
		LeaveAllPanels ();
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
			LeaveAllPanels ();
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
			LeaveAllPanels ();
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
			LeaveAllPanels ();
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
			LeaveAllPanels ();
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
			LeaveAllPanels ();
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
			LeaveAllPanels ();
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
			LeaveAllPanels ();
			panelFertilizing.gameObject.SetActive (true);

			StartCoroutine (WaitOneUpdateCycle ());
			panelFertilizing.GetComponent<FertilizeManager> ().CheckForFreeSlots ();
		}
	}

	/*
	public void WaterPlant()
	{
		LeaveAllPanels ();
		panelWatering.gameObject.SetActive (true);

		StartCoroutine (WaitOneUpdateCycle ());
		panelWatering.GetComponent<WateringManager> ().CheckForFreeSlots ();
	}
	*/

	public void BeginRearrange()
	{
		LeaveAllPanels ();
		panelRearrangeBegins.gameObject.SetActive (true);

		StartCoroutine (WaitOneUpdateCycle ());
		panelRearrangeBegins.GetComponent<RearrangeInitiator> ().CheckForFreeSlots ();
	}

	public void FinishRerrange1()
	{
		LeaveAllPanels ();
		//slots.gameObject.SetActive (false);
		panelRearrangeFinishes.gameObject.SetActive (true);

		StartCoroutine (WaitOneUpdateCycle ());
		panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().SelectFirstSlotToChange (0);
		panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().CheckForFreeSlots ();
	}

	public void FinishRerrange2()
	{
		LeaveAllPanels ();
		//slots.gameObject.SetActive (false);
		panelRearrangeFinishes.gameObject.SetActive (true);

		StartCoroutine (WaitOneUpdateCycle ());
		panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().SelectFirstSlotToChange (1);
		panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().CheckForFreeSlots ();
	}

	public void FinishRerrange3()
	{
		LeaveAllPanels ();
		//slots.gameObject.SetActive (false);
		panelRearrangeFinishes.gameObject.SetActive (true);

		StartCoroutine (WaitOneUpdateCycle ());
		panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().SelectFirstSlotToChange (2);
		panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().CheckForFreeSlots ();
	}

	public void FinishRerrange4()
	{
		LeaveAllPanels ();
		//slots.gameObject.SetActive (false);
		panelRearrangeFinishes.gameObject.SetActive (true);

		StartCoroutine (WaitOneUpdateCycle ());
		panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().SelectFirstSlotToChange (3);
		panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().CheckForFreeSlots ();
	}

	public void FinishRerrange5()
	{
		LeaveAllPanels ();
		//slots.gameObject.SetActive (false);
		panelRearrangeFinishes.gameObject.SetActive (true);

		StartCoroutine (WaitOneUpdateCycle ());
		panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().SelectFirstSlotToChange (4);
		panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().CheckForFreeSlots ();
	}

	public void FinishRerrange6()
	{
		LeaveAllPanels ();
		//slots.gameObject.SetActive (false);
		panelRearrangeFinishes.gameObject.SetActive (true);

		StartCoroutine (WaitOneUpdateCycle ());
		panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().SelectFirstSlotToChange (5);
		panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().CheckForFreeSlots ();
	}

	public void FinishRerrange7()
	{
		LeaveAllPanels ();
		//slots.gameObject.SetActive (false);
		panelRearrangeFinishes.gameObject.SetActive (true);

		StartCoroutine (WaitOneUpdateCycle ());
		panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().SelectFirstSlotToChange (6);
		panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().CheckForFreeSlots ();
	}

	public void FinishRerrange8()
	{
		LeaveAllPanels ();
		//slots.gameObject.SetActive (false);
		panelRearrangeFinishes.gameObject.SetActive (true);

		StartCoroutine (WaitOneUpdateCycle ());
		panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().SelectFirstSlotToChange (7);
		panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().CheckForFreeSlots ();
	}

	public void FinishRerrange9()
	{
		LeaveAllPanels ();
		//slots.gameObject.SetActive (false);
		panelRearrangeFinishes.gameObject.SetActive (true);

		StartCoroutine (WaitOneUpdateCycle ());
		panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().SelectFirstSlotToChange (8);
		panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().CheckForFreeSlots ();
	}

	public void InterchangeSlot1()
	{
		//Of course, we make changes only if the sluts are different
		if (panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected != 0)
		{
			bool auxPlant_isUsed;
			bool auxPlant_isShiny;
			bool auxPlant_congratulations;
			string auxPlant_name;
			string auxPlant_nickname;
			ulong auxPlant_lastTick;
			ulong auxPlant_tickDate;
			int auxPlant_yearDate;
			int auxPlant_monthDate;
			int auxPlant_dayDate;
			short auxPlant_phase;
			int auxPlant_timeLeftSeconds;

			//First, set the auxiliary variables
			auxPlant_isUsed = AllGameData.instance.playerData.plant_isUsed [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_isShiny = AllGameData.instance.playerData.plant_isShiny [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_name = AllGameData.instance.playerData.plant_name [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_nickname = AllGameData.instance.playerData.plant_nickname [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_lastTick = AllGameData.instance.playerData.plant_lastTick [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_tickDate = AllGameData.instance.playerData.plant_tickDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_yearDate = AllGameData.instance.playerData.plant_yearDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_monthDate = AllGameData.instance.playerData.plant_monthDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_dayDate = AllGameData.instance.playerData.plant_dayDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_phase = AllGameData.instance.playerData.plant_phase [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_timeLeftSeconds = AllGameData.instance.playerData.plant_timeLeftSeconds [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_congratulations = AllGameData.instance.playerData.plant_congratulations [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];

			//Debug.Log (auxPlant_isUsed.ToString() + auxPlant_isShiny + auxPlant_name + auxPlant_nickname + auxPlant_lastTick + auxPlant_tickDate + auxPlant_yearDate
			//+ auxPlant_monthDate + auxPlant_dayDate + auxPlant_phase + auxPlant_timeLeftSeconds);

			//Then change first selected slot with second selected slot (slot 1)
			AllGameData.instance.playerData.plant_isUsed[panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_isUsed[0];
			AllGameData.instance.playerData.plant_isShiny [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_isShiny [0];
			AllGameData.instance.playerData.plant_name [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_name [0];
			AllGameData.instance.playerData.plant_nickname [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_nickname [0];
			AllGameData.instance.playerData.plant_lastTick [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_lastTick [0];
			AllGameData.instance.playerData.plant_tickDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_tickDate [0];
			AllGameData.instance.playerData.plant_yearDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_yearDate [0];
			AllGameData.instance.playerData.plant_monthDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_monthDate [0];
			AllGameData.instance.playerData.plant_dayDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_dayDate [0];
			AllGameData.instance.playerData.plant_phase [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_phase [0];
			AllGameData.instance.playerData.plant_timeLeftSeconds [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_timeLeftSeconds [0];
			AllGameData.instance.playerData.plant_congratulations [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_congratulations [0];

			//Finally we set the second select slot (slot 1) from auxiliaries
			AllGameData.instance.playerData.plant_isUsed[0] = auxPlant_isUsed;
			AllGameData.instance.playerData.plant_isShiny[0] = auxPlant_isShiny;
			AllGameData.instance.playerData.plant_name[0] = auxPlant_name;
			AllGameData.instance.playerData.plant_nickname[0] = auxPlant_nickname;
			AllGameData.instance.playerData.plant_lastTick[0] = auxPlant_lastTick;
			AllGameData.instance.playerData.plant_tickDate[0] = auxPlant_tickDate;
			AllGameData.instance.playerData.plant_yearDate[0] = auxPlant_yearDate;
			AllGameData.instance.playerData.plant_monthDate[0] = auxPlant_monthDate;
			AllGameData.instance.playerData.plant_dayDate[0] = auxPlant_dayDate;
			AllGameData.instance.playerData.plant_phase[0] = auxPlant_phase;
			AllGameData.instance.playerData.plant_timeLeftSeconds[0] = auxPlant_timeLeftSeconds;
			AllGameData.instance.playerData.plant_congratulations [0] = auxPlant_congratulations;

			//So, after all that Super-Hidraulic-Interdimensional-Trash, we save data, leave info, and back to business
			AllGameData.instance.SaveData();

			//FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
			LeaveInfo ();
		}
		else
		{
			LeaveInfo ();
		}
	}

	public void InterchangeSlot2()
	{
		//Of course, we make changes only if the sluts are different
		if (panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected != 1)
		{
			bool auxPlant_isUsed;
			bool auxPlant_isShiny;
			bool auxPlant_congratulations;
			string auxPlant_name;
			string auxPlant_nickname;
			ulong auxPlant_lastTick;
			ulong auxPlant_tickDate;
			int auxPlant_yearDate;
			int auxPlant_monthDate;
			int auxPlant_dayDate;
			short auxPlant_phase;
			int auxPlant_timeLeftSeconds;

			//First, set the auxiliary variables
			auxPlant_isUsed = AllGameData.instance.playerData.plant_isUsed [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_isShiny = AllGameData.instance.playerData.plant_isShiny [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_name = AllGameData.instance.playerData.plant_name [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_nickname = AllGameData.instance.playerData.plant_nickname [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_lastTick = AllGameData.instance.playerData.plant_lastTick [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_tickDate = AllGameData.instance.playerData.plant_tickDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_yearDate = AllGameData.instance.playerData.plant_yearDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_monthDate = AllGameData.instance.playerData.plant_monthDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_dayDate = AllGameData.instance.playerData.plant_dayDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_phase = AllGameData.instance.playerData.plant_phase [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_timeLeftSeconds = AllGameData.instance.playerData.plant_timeLeftSeconds [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_congratulations = AllGameData.instance.playerData.plant_congratulations [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];

			//Debug.Log (auxPlant_isUsed.ToString() + auxPlant_isShiny + auxPlant_name + auxPlant_nickname + auxPlant_lastTick + auxPlant_tickDate + auxPlant_yearDate
			//+ auxPlant_monthDate + auxPlant_dayDate + auxPlant_phase + auxPlant_timeLeftSeconds);

			//Then change first selected slot with second selected slot (slot 2)
			AllGameData.instance.playerData.plant_isUsed[panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_isUsed[1];
			AllGameData.instance.playerData.plant_isShiny [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_isShiny [1];
			AllGameData.instance.playerData.plant_name [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_name [1];
			AllGameData.instance.playerData.plant_nickname [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_nickname [1];
			AllGameData.instance.playerData.plant_lastTick [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_lastTick [1];
			AllGameData.instance.playerData.plant_tickDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_tickDate [1];
			AllGameData.instance.playerData.plant_yearDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_yearDate [1];
			AllGameData.instance.playerData.plant_monthDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_monthDate [1];
			AllGameData.instance.playerData.plant_dayDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_dayDate [1];
			AllGameData.instance.playerData.plant_phase [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_phase [1];
			AllGameData.instance.playerData.plant_timeLeftSeconds [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_timeLeftSeconds [1];
			AllGameData.instance.playerData.plant_congratulations [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_congratulations [1];

			//Finally we set the second select slot (slot 2) from auxiliaries
			AllGameData.instance.playerData.plant_isUsed[1] = auxPlant_isUsed;
			AllGameData.instance.playerData.plant_isShiny[1] = auxPlant_isShiny;
			AllGameData.instance.playerData.plant_name[1] = auxPlant_name;
			AllGameData.instance.playerData.plant_nickname[1] = auxPlant_nickname;
			AllGameData.instance.playerData.plant_lastTick[1] = auxPlant_lastTick;
			AllGameData.instance.playerData.plant_tickDate[1] = auxPlant_tickDate;
			AllGameData.instance.playerData.plant_yearDate[1] = auxPlant_yearDate;
			AllGameData.instance.playerData.plant_monthDate[1] = auxPlant_monthDate;
			AllGameData.instance.playerData.plant_dayDate[1] = auxPlant_dayDate;
			AllGameData.instance.playerData.plant_phase[1] = auxPlant_phase;
			AllGameData.instance.playerData.plant_timeLeftSeconds[1] = auxPlant_timeLeftSeconds;
			AllGameData.instance.playerData.plant_congratulations [1] = auxPlant_congratulations;

			//So, after all that Super-Hidraulic-Interdimensional-Trash, we save data, leave info, and back to business
			AllGameData.instance.SaveData();

			//FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
			LeaveInfo ();
		}
		else
		{
			LeaveInfo ();
		}
	}

	public void InterchangeSlot3()
	{
		//Of course, we make changes only if the sluts are different
		if (panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected != 2)
		{
			bool auxPlant_isUsed;
			bool auxPlant_isShiny;
			bool auxPlant_congratulations;
			string auxPlant_name;
			string auxPlant_nickname;
			ulong auxPlant_lastTick;
			ulong auxPlant_tickDate;
			int auxPlant_yearDate;
			int auxPlant_monthDate;
			int auxPlant_dayDate;
			short auxPlant_phase;
			int auxPlant_timeLeftSeconds;

			//First, set the auxiliary variables
			auxPlant_isUsed = AllGameData.instance.playerData.plant_isUsed [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_isShiny = AllGameData.instance.playerData.plant_isShiny [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_name = AllGameData.instance.playerData.plant_name [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_nickname = AllGameData.instance.playerData.plant_nickname [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_lastTick = AllGameData.instance.playerData.plant_lastTick [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_tickDate = AllGameData.instance.playerData.plant_tickDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_yearDate = AllGameData.instance.playerData.plant_yearDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_monthDate = AllGameData.instance.playerData.plant_monthDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_dayDate = AllGameData.instance.playerData.plant_dayDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_phase = AllGameData.instance.playerData.plant_phase [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_timeLeftSeconds = AllGameData.instance.playerData.plant_timeLeftSeconds [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_congratulations = AllGameData.instance.playerData.plant_congratulations [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];

			//Debug.Log (auxPlant_isUsed.ToString() + auxPlant_isShiny + auxPlant_name + auxPlant_nickname + auxPlant_lastTick + auxPlant_tickDate + auxPlant_yearDate
			//+ auxPlant_monthDate + auxPlant_dayDate + auxPlant_phase + auxPlant_timeLeftSeconds);

			//Then change first selected slot with second selected slot (slot 3)
			AllGameData.instance.playerData.plant_isUsed[panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_isUsed[2];
			AllGameData.instance.playerData.plant_isShiny [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_isShiny [2];
			AllGameData.instance.playerData.plant_name [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_name [2];
			AllGameData.instance.playerData.plant_nickname [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_nickname [2];
			AllGameData.instance.playerData.plant_lastTick [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_lastTick [2];
			AllGameData.instance.playerData.plant_tickDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_tickDate [2];
			AllGameData.instance.playerData.plant_yearDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_yearDate [2];
			AllGameData.instance.playerData.plant_monthDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_monthDate [2];
			AllGameData.instance.playerData.plant_dayDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_dayDate [2];
			AllGameData.instance.playerData.plant_phase [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_phase [2];
			AllGameData.instance.playerData.plant_timeLeftSeconds [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_timeLeftSeconds [2];
			AllGameData.instance.playerData.plant_congratulations [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_congratulations [2];

			//Finally we set the second select slot (slot 3) from auxiliaries
			AllGameData.instance.playerData.plant_isUsed[2] = auxPlant_isUsed;
			AllGameData.instance.playerData.plant_isShiny[2] = auxPlant_isShiny;
			AllGameData.instance.playerData.plant_name[2] = auxPlant_name;
			AllGameData.instance.playerData.plant_nickname[2] = auxPlant_nickname;
			AllGameData.instance.playerData.plant_lastTick[2] = auxPlant_lastTick;
			AllGameData.instance.playerData.plant_tickDate[2] = auxPlant_tickDate;
			AllGameData.instance.playerData.plant_yearDate[2] = auxPlant_yearDate;
			AllGameData.instance.playerData.plant_monthDate[2] = auxPlant_monthDate;
			AllGameData.instance.playerData.plant_dayDate[2] = auxPlant_dayDate;
			AllGameData.instance.playerData.plant_phase[2] = auxPlant_phase;
			AllGameData.instance.playerData.plant_timeLeftSeconds[2] = auxPlant_timeLeftSeconds;
			AllGameData.instance.playerData.plant_congratulations [2] = auxPlant_congratulations;

			//So, after all that Super-Hidraulic-Interdimensional-Trash, we save data, leave info, and back to business
			AllGameData.instance.SaveData();

			//FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
			LeaveInfo ();
		}
		else
		{
			LeaveInfo ();
		}
	}

	public void InterchangeSlot4()
	{
		//Of course, we make changes only if the sluts are different
		if (panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected != 3)
		{
			bool auxPlant_isUsed;
			bool auxPlant_isShiny;
			bool auxPlant_congratulations;
			string auxPlant_name;
			string auxPlant_nickname;
			ulong auxPlant_lastTick;
			ulong auxPlant_tickDate;
			int auxPlant_yearDate;
			int auxPlant_monthDate;
			int auxPlant_dayDate;
			short auxPlant_phase;
			int auxPlant_timeLeftSeconds;

			//First, set the auxiliary variables
			auxPlant_isUsed = AllGameData.instance.playerData.plant_isUsed [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_isShiny = AllGameData.instance.playerData.plant_isShiny [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_name = AllGameData.instance.playerData.plant_name [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_nickname = AllGameData.instance.playerData.plant_nickname [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_lastTick = AllGameData.instance.playerData.plant_lastTick [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_tickDate = AllGameData.instance.playerData.plant_tickDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_yearDate = AllGameData.instance.playerData.plant_yearDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_monthDate = AllGameData.instance.playerData.plant_monthDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_dayDate = AllGameData.instance.playerData.plant_dayDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_phase = AllGameData.instance.playerData.plant_phase [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_timeLeftSeconds = AllGameData.instance.playerData.plant_timeLeftSeconds [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_congratulations = AllGameData.instance.playerData.plant_congratulations [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];

			//Debug.Log (auxPlant_isUsed.ToString() + auxPlant_isShiny + auxPlant_name + auxPlant_nickname + auxPlant_lastTick + auxPlant_tickDate + auxPlant_yearDate
			//+ auxPlant_monthDate + auxPlant_dayDate + auxPlant_phase + auxPlant_timeLeftSeconds);

			//Then change first selected slot with second selected slot (slot 4)
			AllGameData.instance.playerData.plant_isUsed[panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_isUsed[3];
			AllGameData.instance.playerData.plant_isShiny [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_isShiny [3];
			AllGameData.instance.playerData.plant_name [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_name [3];
			AllGameData.instance.playerData.plant_nickname [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_nickname [3];
			AllGameData.instance.playerData.plant_lastTick [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_lastTick [3];
			AllGameData.instance.playerData.plant_tickDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_tickDate [3];
			AllGameData.instance.playerData.plant_yearDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_yearDate [3];
			AllGameData.instance.playerData.plant_monthDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_monthDate [3];
			AllGameData.instance.playerData.plant_dayDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_dayDate [3];
			AllGameData.instance.playerData.plant_phase [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_phase [3];
			AllGameData.instance.playerData.plant_timeLeftSeconds [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_timeLeftSeconds [3];
			AllGameData.instance.playerData.plant_congratulations [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_congratulations [3];

			//Finally we set the second select slot (slot 4) from auxiliaries
			AllGameData.instance.playerData.plant_isUsed[3] = auxPlant_isUsed;
			AllGameData.instance.playerData.plant_isShiny[3] = auxPlant_isShiny;
			AllGameData.instance.playerData.plant_name[3] = auxPlant_name;
			AllGameData.instance.playerData.plant_nickname[3] = auxPlant_nickname;
			AllGameData.instance.playerData.plant_lastTick[3] = auxPlant_lastTick;
			AllGameData.instance.playerData.plant_tickDate[3] = auxPlant_tickDate;
			AllGameData.instance.playerData.plant_yearDate[3] = auxPlant_yearDate;
			AllGameData.instance.playerData.plant_monthDate[3] = auxPlant_monthDate;
			AllGameData.instance.playerData.plant_dayDate[3] = auxPlant_dayDate;
			AllGameData.instance.playerData.plant_phase[3] = auxPlant_phase;
			AllGameData.instance.playerData.plant_timeLeftSeconds[3] = auxPlant_timeLeftSeconds;
			AllGameData.instance.playerData.plant_congratulations [3] = auxPlant_congratulations;

			//So, after all that Super-Hidraulic-Interdimensional-Trash, we save data, leave info, and back to business
			AllGameData.instance.SaveData();

			//FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
			LeaveInfo ();
		}
		else
		{
			LeaveInfo ();
		}
	}

	public void InterchangeSlot5()
	{
		//Of course, we make changes only if the sluts are different
		if (panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected != 4)
		{
			bool auxPlant_isUsed;
			bool auxPlant_isShiny;
			bool auxPlant_congratulations;
			string auxPlant_name;
			string auxPlant_nickname;
			ulong auxPlant_lastTick;
			ulong auxPlant_tickDate;
			int auxPlant_yearDate;
			int auxPlant_monthDate;
			int auxPlant_dayDate;
			short auxPlant_phase;
			int auxPlant_timeLeftSeconds;

			//First, set the auxiliary variables
			auxPlant_isUsed = AllGameData.instance.playerData.plant_isUsed [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_isShiny = AllGameData.instance.playerData.plant_isShiny [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_name = AllGameData.instance.playerData.plant_name [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_nickname = AllGameData.instance.playerData.plant_nickname [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_lastTick = AllGameData.instance.playerData.plant_lastTick [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_tickDate = AllGameData.instance.playerData.plant_tickDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_yearDate = AllGameData.instance.playerData.plant_yearDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_monthDate = AllGameData.instance.playerData.plant_monthDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_dayDate = AllGameData.instance.playerData.plant_dayDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_phase = AllGameData.instance.playerData.plant_phase [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_timeLeftSeconds = AllGameData.instance.playerData.plant_timeLeftSeconds [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_congratulations = AllGameData.instance.playerData.plant_congratulations [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];

			//Debug.Log (auxPlant_isUsed.ToString() + auxPlant_isShiny + auxPlant_name + auxPlant_nickname + auxPlant_lastTick + auxPlant_tickDate + auxPlant_yearDate
			//+ auxPlant_monthDate + auxPlant_dayDate + auxPlant_phase + auxPlant_timeLeftSeconds);

			//Then change first selected slot with second selected slot (slot 5)
			AllGameData.instance.playerData.plant_isUsed[panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_isUsed[4];
			AllGameData.instance.playerData.plant_isShiny [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_isShiny [4];
			AllGameData.instance.playerData.plant_name [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_name [4];
			AllGameData.instance.playerData.plant_nickname [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_nickname [4];
			AllGameData.instance.playerData.plant_lastTick [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_lastTick [4];
			AllGameData.instance.playerData.plant_tickDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_tickDate [4];
			AllGameData.instance.playerData.plant_yearDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_yearDate [4];
			AllGameData.instance.playerData.plant_monthDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_monthDate [4];
			AllGameData.instance.playerData.plant_dayDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_dayDate [4];
			AllGameData.instance.playerData.plant_phase [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_phase [4];
			AllGameData.instance.playerData.plant_timeLeftSeconds [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_timeLeftSeconds [4];
			AllGameData.instance.playerData.plant_congratulations [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_congratulations [4];

			//Finally we set the second select slot (slot 5) from auxiliaries
			AllGameData.instance.playerData.plant_isUsed[4] = auxPlant_isUsed;
			AllGameData.instance.playerData.plant_isShiny[4] = auxPlant_isShiny;
			AllGameData.instance.playerData.plant_name[4] = auxPlant_name;
			AllGameData.instance.playerData.plant_nickname[4] = auxPlant_nickname;
			AllGameData.instance.playerData.plant_lastTick[4] = auxPlant_lastTick;
			AllGameData.instance.playerData.plant_tickDate[4] = auxPlant_tickDate;
			AllGameData.instance.playerData.plant_yearDate[4] = auxPlant_yearDate;
			AllGameData.instance.playerData.plant_monthDate[4] = auxPlant_monthDate;
			AllGameData.instance.playerData.plant_dayDate[4] = auxPlant_dayDate;
			AllGameData.instance.playerData.plant_phase[4] = auxPlant_phase;
			AllGameData.instance.playerData.plant_timeLeftSeconds[4] = auxPlant_timeLeftSeconds;
			AllGameData.instance.playerData.plant_congratulations [4] = auxPlant_congratulations;

			//So, after all that Super-Hidraulic-Interdimensional-Trash, we save data, leave info, and back to business
			AllGameData.instance.SaveData();

			//FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
			LeaveInfo ();
		}
		else
		{
			LeaveInfo ();
		}
	}

	public void InterchangeSlot6()
	{
		//Of course, we make changes only if the sluts are different
		if (panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected != 5)
		{
			bool auxPlant_isUsed;
			bool auxPlant_isShiny;
			bool auxPlant_congratulations;
			string auxPlant_name;
			string auxPlant_nickname;
			ulong auxPlant_lastTick;
			ulong auxPlant_tickDate;
			int auxPlant_yearDate;
			int auxPlant_monthDate;
			int auxPlant_dayDate;
			short auxPlant_phase;
			int auxPlant_timeLeftSeconds;

			//First, set the auxiliary variables
			auxPlant_isUsed = AllGameData.instance.playerData.plant_isUsed [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_isShiny = AllGameData.instance.playerData.plant_isShiny [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_name = AllGameData.instance.playerData.plant_name [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_nickname = AllGameData.instance.playerData.plant_nickname [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_lastTick = AllGameData.instance.playerData.plant_lastTick [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_tickDate = AllGameData.instance.playerData.plant_tickDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_yearDate = AllGameData.instance.playerData.plant_yearDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_monthDate = AllGameData.instance.playerData.plant_monthDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_dayDate = AllGameData.instance.playerData.plant_dayDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_phase = AllGameData.instance.playerData.plant_phase [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_timeLeftSeconds = AllGameData.instance.playerData.plant_timeLeftSeconds [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_congratulations = AllGameData.instance.playerData.plant_congratulations [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];

			//Debug.Log (auxPlant_isUsed.ToString() + auxPlant_isShiny + auxPlant_name + auxPlant_nickname + auxPlant_lastTick + auxPlant_tickDate + auxPlant_yearDate
			//+ auxPlant_monthDate + auxPlant_dayDate + auxPlant_phase + auxPlant_timeLeftSeconds);

			//Then change first selected slot with second selected slot (slot 6)
			AllGameData.instance.playerData.plant_isUsed[panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_isUsed[5];
			AllGameData.instance.playerData.plant_isShiny [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_isShiny [5];
			AllGameData.instance.playerData.plant_name [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_name [5];
			AllGameData.instance.playerData.plant_nickname [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_nickname [5];
			AllGameData.instance.playerData.plant_lastTick [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_lastTick [5];
			AllGameData.instance.playerData.plant_tickDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_tickDate [5];
			AllGameData.instance.playerData.plant_yearDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_yearDate [5];
			AllGameData.instance.playerData.plant_monthDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_monthDate [5];
			AllGameData.instance.playerData.plant_dayDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_dayDate [5];
			AllGameData.instance.playerData.plant_phase [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_phase [5];
			AllGameData.instance.playerData.plant_timeLeftSeconds [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_timeLeftSeconds [5];
			AllGameData.instance.playerData.plant_congratulations [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_congratulations [5];

			//Finally we set the second select slot (slot 6) from auxiliaries
			AllGameData.instance.playerData.plant_isUsed[5] = auxPlant_isUsed;
			AllGameData.instance.playerData.plant_isShiny[5] = auxPlant_isShiny;
			AllGameData.instance.playerData.plant_name[5] = auxPlant_name;
			AllGameData.instance.playerData.plant_nickname[5] = auxPlant_nickname;
			AllGameData.instance.playerData.plant_lastTick[5] = auxPlant_lastTick;
			AllGameData.instance.playerData.plant_tickDate[5] = auxPlant_tickDate;
			AllGameData.instance.playerData.plant_yearDate[5] = auxPlant_yearDate;
			AllGameData.instance.playerData.plant_monthDate[5] = auxPlant_monthDate;
			AllGameData.instance.playerData.plant_dayDate[5] = auxPlant_dayDate;
			AllGameData.instance.playerData.plant_phase[5] = auxPlant_phase;
			AllGameData.instance.playerData.plant_timeLeftSeconds[5] = auxPlant_timeLeftSeconds;
			AllGameData.instance.playerData.plant_congratulations [5] = auxPlant_congratulations;

			//So, after all that Super-Hidraulic-Interdimensional-Trash, we save data, leave info, and back to business
			AllGameData.instance.SaveData();

			//FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
			LeaveInfo ();
		}
		else
		{
			LeaveInfo ();
		}
	}

	public void InterchangeSlot7()
	{
		//Of course, we make changes only if the sluts are different
		if (panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected != 6)
		{
			bool auxPlant_isUsed;
			bool auxPlant_isShiny;
			bool auxPlant_congratulations;
			string auxPlant_name;
			string auxPlant_nickname;
			ulong auxPlant_lastTick;
			ulong auxPlant_tickDate;
			int auxPlant_yearDate;
			int auxPlant_monthDate;
			int auxPlant_dayDate;
			short auxPlant_phase;
			int auxPlant_timeLeftSeconds;

			//First, set the auxiliary variables
			auxPlant_isUsed = AllGameData.instance.playerData.plant_isUsed [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_isShiny = AllGameData.instance.playerData.plant_isShiny [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_name = AllGameData.instance.playerData.plant_name [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_nickname = AllGameData.instance.playerData.plant_nickname [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_lastTick = AllGameData.instance.playerData.plant_lastTick [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_tickDate = AllGameData.instance.playerData.plant_tickDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_yearDate = AllGameData.instance.playerData.plant_yearDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_monthDate = AllGameData.instance.playerData.plant_monthDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_dayDate = AllGameData.instance.playerData.plant_dayDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_phase = AllGameData.instance.playerData.plant_phase [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_timeLeftSeconds = AllGameData.instance.playerData.plant_timeLeftSeconds [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_congratulations = AllGameData.instance.playerData.plant_congratulations [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];

			//Debug.Log (auxPlant_isUsed.ToString() + auxPlant_isShiny + auxPlant_name + auxPlant_nickname + auxPlant_lastTick + auxPlant_tickDate + auxPlant_yearDate
			//+ auxPlant_monthDate + auxPlant_dayDate + auxPlant_phase + auxPlant_timeLeftSeconds);

			//Then change first selected slot with second selected slot (slot 7)
			AllGameData.instance.playerData.plant_isUsed[panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_isUsed[6];
			AllGameData.instance.playerData.plant_isShiny [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_isShiny [6];
			AllGameData.instance.playerData.plant_name [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_name [6];
			AllGameData.instance.playerData.plant_nickname [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_nickname [6];
			AllGameData.instance.playerData.plant_lastTick [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_lastTick [6];
			AllGameData.instance.playerData.plant_tickDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_tickDate [6];
			AllGameData.instance.playerData.plant_yearDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_yearDate [6];
			AllGameData.instance.playerData.plant_monthDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_monthDate [6];
			AllGameData.instance.playerData.plant_dayDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_dayDate [6];
			AllGameData.instance.playerData.plant_phase [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_phase [6];
			AllGameData.instance.playerData.plant_timeLeftSeconds [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_timeLeftSeconds [6];
			AllGameData.instance.playerData.plant_congratulations [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_congratulations [6];

			//Finally we set the second select slot (slot 7) from auxiliaries
			AllGameData.instance.playerData.plant_isUsed[6] = auxPlant_isUsed;
			AllGameData.instance.playerData.plant_isShiny[6] = auxPlant_isShiny;
			AllGameData.instance.playerData.plant_name[6] = auxPlant_name;
			AllGameData.instance.playerData.plant_nickname[6] = auxPlant_nickname;
			AllGameData.instance.playerData.plant_lastTick[6] = auxPlant_lastTick;
			AllGameData.instance.playerData.plant_tickDate[6] = auxPlant_tickDate;
			AllGameData.instance.playerData.plant_yearDate[6] = auxPlant_yearDate;
			AllGameData.instance.playerData.plant_monthDate[6] = auxPlant_monthDate;
			AllGameData.instance.playerData.plant_dayDate[6] = auxPlant_dayDate;
			AllGameData.instance.playerData.plant_phase[6] = auxPlant_phase;
			AllGameData.instance.playerData.plant_timeLeftSeconds[6] = auxPlant_timeLeftSeconds;
			AllGameData.instance.playerData.plant_congratulations [6] = auxPlant_congratulations;

			//So, after all that Super-Hidraulic-Interdimensional-Trash, we save data, leave info, and back to business
			AllGameData.instance.SaveData();

			//FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
			LeaveInfo ();
		}
		else
		{
			LeaveInfo ();
		}
	}

	public void InterchangeSlot8()
	{
		//Of course, we make changes only if the sluts are different
		if (panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected != 7)
		{
			bool auxPlant_isUsed;
			bool auxPlant_isShiny;
			bool auxPlant_congratulations;
			string auxPlant_name;
			string auxPlant_nickname;
			ulong auxPlant_lastTick;
			ulong auxPlant_tickDate;
			int auxPlant_yearDate;
			int auxPlant_monthDate;
			int auxPlant_dayDate;
			short auxPlant_phase;
			int auxPlant_timeLeftSeconds;

			//First, set the auxiliary variables
			auxPlant_isUsed = AllGameData.instance.playerData.plant_isUsed [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_isShiny = AllGameData.instance.playerData.plant_isShiny [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_name = AllGameData.instance.playerData.plant_name [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_nickname = AllGameData.instance.playerData.plant_nickname [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_lastTick = AllGameData.instance.playerData.plant_lastTick [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_tickDate = AllGameData.instance.playerData.plant_tickDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_yearDate = AllGameData.instance.playerData.plant_yearDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_monthDate = AllGameData.instance.playerData.plant_monthDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_dayDate = AllGameData.instance.playerData.plant_dayDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_phase = AllGameData.instance.playerData.plant_phase [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_timeLeftSeconds = AllGameData.instance.playerData.plant_timeLeftSeconds [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_congratulations = AllGameData.instance.playerData.plant_congratulations [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];

			//Debug.Log (auxPlant_isUsed.ToString() + auxPlant_isShiny + auxPlant_name + auxPlant_nickname + auxPlant_lastTick + auxPlant_tickDate + auxPlant_yearDate
			//+ auxPlant_monthDate + auxPlant_dayDate + auxPlant_phase + auxPlant_timeLeftSeconds);

			//Then change first selected slot with second selected slot (slot 8)
			AllGameData.instance.playerData.plant_isUsed[panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_isUsed[7];
			AllGameData.instance.playerData.plant_isShiny [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_isShiny [7];
			AllGameData.instance.playerData.plant_name [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_name [7];
			AllGameData.instance.playerData.plant_nickname [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_nickname [7];
			AllGameData.instance.playerData.plant_lastTick [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_lastTick [7];
			AllGameData.instance.playerData.plant_tickDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_tickDate [7];
			AllGameData.instance.playerData.plant_yearDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_yearDate [7];
			AllGameData.instance.playerData.plant_monthDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_monthDate [7];
			AllGameData.instance.playerData.plant_dayDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_dayDate [7];
			AllGameData.instance.playerData.plant_phase [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_phase [7];
			AllGameData.instance.playerData.plant_timeLeftSeconds [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_timeLeftSeconds [7];
			AllGameData.instance.playerData.plant_congratulations [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_congratulations [7];

			//Finally we set the second select slot (slot 8) from auxiliaries
			AllGameData.instance.playerData.plant_isUsed[7] = auxPlant_isUsed;
			AllGameData.instance.playerData.plant_isShiny[7] = auxPlant_isShiny;
			AllGameData.instance.playerData.plant_name[7] = auxPlant_name;
			AllGameData.instance.playerData.plant_nickname[7] = auxPlant_nickname;
			AllGameData.instance.playerData.plant_lastTick[7] = auxPlant_lastTick;
			AllGameData.instance.playerData.plant_tickDate[7] = auxPlant_tickDate;
			AllGameData.instance.playerData.plant_yearDate[7] = auxPlant_yearDate;
			AllGameData.instance.playerData.plant_monthDate[7] = auxPlant_monthDate;
			AllGameData.instance.playerData.plant_dayDate[7] = auxPlant_dayDate;
			AllGameData.instance.playerData.plant_phase[7] = auxPlant_phase;
			AllGameData.instance.playerData.plant_timeLeftSeconds[7] = auxPlant_timeLeftSeconds;
			AllGameData.instance.playerData.plant_congratulations [7] = auxPlant_congratulations;

			//So, after all that Super-Hidraulic-Interdimensional-Trash, we save data, leave info, and back to business
			AllGameData.instance.SaveData();

			//FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
			LeaveInfo ();
		}
		else
		{
			LeaveInfo ();
		}
	}

	public void InterchangeSlot9()
	{
		//Of course, we make changes only if the sluts are different
		if (panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected != 8)
		{
			bool auxPlant_isUsed;
			bool auxPlant_isShiny;
			bool auxPlant_congratulations;
			string auxPlant_name;
			string auxPlant_nickname;
			ulong auxPlant_lastTick;
			ulong auxPlant_tickDate;
			int auxPlant_yearDate;
			int auxPlant_monthDate;
			int auxPlant_dayDate;
			short auxPlant_phase;
			int auxPlant_timeLeftSeconds;

			//First, set the auxiliary variables
			auxPlant_isUsed = AllGameData.instance.playerData.plant_isUsed [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_isShiny = AllGameData.instance.playerData.plant_isShiny [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_name = AllGameData.instance.playerData.plant_name [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_nickname = AllGameData.instance.playerData.plant_nickname [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_lastTick = AllGameData.instance.playerData.plant_lastTick [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_tickDate = AllGameData.instance.playerData.plant_tickDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_yearDate = AllGameData.instance.playerData.plant_yearDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_monthDate = AllGameData.instance.playerData.plant_monthDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_dayDate = AllGameData.instance.playerData.plant_dayDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_phase = AllGameData.instance.playerData.plant_phase [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_timeLeftSeconds = AllGameData.instance.playerData.plant_timeLeftSeconds [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];
			auxPlant_congratulations = AllGameData.instance.playerData.plant_congratulations [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected];

			//Debug.Log (auxPlant_isUsed.ToString() + auxPlant_isShiny + auxPlant_name + auxPlant_nickname + auxPlant_lastTick + auxPlant_tickDate + auxPlant_yearDate
			//+ auxPlant_monthDate + auxPlant_dayDate + auxPlant_phase + auxPlant_timeLeftSeconds);

			//Then change first selected slot with second selected slot (slot 9)
			AllGameData.instance.playerData.plant_isUsed[panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_isUsed[8];
			AllGameData.instance.playerData.plant_isShiny [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_isShiny [8];
			AllGameData.instance.playerData.plant_name [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_name [8];
			AllGameData.instance.playerData.plant_nickname [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_nickname [8];
			AllGameData.instance.playerData.plant_lastTick [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_lastTick [8];
			AllGameData.instance.playerData.plant_tickDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_tickDate [8];
			AllGameData.instance.playerData.plant_yearDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_yearDate [8];
			AllGameData.instance.playerData.plant_monthDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_monthDate [8];
			AllGameData.instance.playerData.plant_dayDate [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_dayDate [8];
			AllGameData.instance.playerData.plant_phase [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_phase [8];
			AllGameData.instance.playerData.plant_timeLeftSeconds [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_timeLeftSeconds [8];
			AllGameData.instance.playerData.plant_congratulations [panelRearrangeFinishes.GetComponent<RearrangeFinisher> ().slotSelected] = AllGameData.instance.playerData.plant_congratulations [8];

			//Finally we set the second select slot (slot 9) from auxiliaries
			AllGameData.instance.playerData.plant_isUsed[8] = auxPlant_isUsed;
			AllGameData.instance.playerData.plant_isShiny[8] = auxPlant_isShiny;
			AllGameData.instance.playerData.plant_name[8] = auxPlant_name;
			AllGameData.instance.playerData.plant_nickname[8] = auxPlant_nickname;
			AllGameData.instance.playerData.plant_lastTick[8] = auxPlant_lastTick;
			AllGameData.instance.playerData.plant_tickDate[8] = auxPlant_tickDate;
			AllGameData.instance.playerData.plant_yearDate[8] = auxPlant_yearDate;
			AllGameData.instance.playerData.plant_monthDate[8] = auxPlant_monthDate;
			AllGameData.instance.playerData.plant_dayDate[8] = auxPlant_dayDate;
			AllGameData.instance.playerData.plant_phase[8] = auxPlant_phase;
			AllGameData.instance.playerData.plant_timeLeftSeconds[8] = auxPlant_timeLeftSeconds;
			AllGameData.instance.playerData.plant_congratulations [8] = auxPlant_congratulations;

			//So, after all that Super-Hidraulic-Interdimensional-Trash, we save data, leave info, and back to business
			AllGameData.instance.SaveData();

			//FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
			LeaveInfo ();
		}
		else
		{
			LeaveInfo ();
		}
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

	public void StartSlot1()
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

	public void StartSlot2()
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

	public void StartSlot3()
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

	public void StartSlot4()
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

	public void StartSlot5()
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

	public void StartSlot6()
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

	public void StartSlot7()
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

	public void StartSlot8()
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

	public void StartSlot9()
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

	public void Fertilize1()
	{
		AllGameData.instance.playerData.plant_timeLeftSeconds [0] = 0;
		AllGameData.instance.playerData.manure -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void Fertilize2()
	{
		AllGameData.instance.playerData.plant_timeLeftSeconds [1] = 0;
		AllGameData.instance.playerData.manure -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void Fertilize3()
	{
		AllGameData.instance.playerData.plant_timeLeftSeconds [2] = 0;
		AllGameData.instance.playerData.manure -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void Fertilize4()
	{
		AllGameData.instance.playerData.plant_timeLeftSeconds [3] = 0;
		AllGameData.instance.playerData.manure -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void Fertilize5()
	{
		AllGameData.instance.playerData.plant_timeLeftSeconds [4] = 0;
		AllGameData.instance.playerData.manure -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void Fertilize6()
	{
		AllGameData.instance.playerData.plant_timeLeftSeconds [5] = 0;
		AllGameData.instance.playerData.manure -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void Fertilize7()
	{
		AllGameData.instance.playerData.plant_timeLeftSeconds [6] = 0;
		AllGameData.instance.playerData.manure -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void Fertilize8()
	{
		AllGameData.instance.playerData.plant_timeLeftSeconds [7] = 0;
		AllGameData.instance.playerData.manure -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void Fertilize9()
	{
		AllGameData.instance.playerData.plant_timeLeftSeconds [8] = 0;
		AllGameData.instance.playerData.manure -= 1;

		AllGameData.instance.SaveData ();

		FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		LeaveInfo ();
	}

	public void Water1()
	{
		//The miniInfo disappears, the water can shows for 1 seconds, and then dies
		//Pretty much this is only visual porposes
		//panelRegularView.GetChild (5).gameObject.SetActive (false);
		//GameObject wateringCan = Instantiate (WateringCanPrefab, new Vector3 (-1.75f, 2.5f, 0f), Quaternion.identity);
		isWatering = true;
		StartCoroutine (WaitDestroyAndShow (Instantiate(WateringCanPrefab, new Vector3 (-1.75f, 2.5f, 0f), Quaternion.identity), panelRegularView.GetChild (5).gameObject));

		//And logically (all game data)
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
		//LeaveInfo ();
	}

	public void Water2()
	{
		//The miniInfo disappears, the water can shows for 1 seconds, and then dies
		//Pretty much this is only visual porposes
		//panelRegularView.GetChild (6).gameObject.SetActive (false);
		//GameObject wateringCan = Instantiate (WateringCanPrefab, new Vector3 (-0.5f, 2.5f, 0f), Quaternion.identity);
		isWatering = true;
		StartCoroutine (WaitDestroyAndShow (Instantiate(WateringCanPrefab, new Vector3 (-0.5f, 2.5f, 0f), Quaternion.identity), panelRegularView.GetChild (6).gameObject));

		//And logically (all game data)
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
		//LeaveInfo ();
	}

	public void Water3()
	{
		//The miniInfo disappears, the water can shows for 1 seconds, and then dies
		//Pretty much this is only visual porposes
		//panelRegularView.GetChild (7).gameObject.SetActive (false);
		//GameObject wateringCan = Instantiate (WateringCanPrefab, new Vector3 (0.8f, 2.5f, 0f), Quaternion.identity);
		isWatering = true;
		StartCoroutine (WaitDestroyAndShow (Instantiate(WateringCanPrefab, new Vector3 (0.8f, 2.5f, 0f), Quaternion.identity), panelRegularView.GetChild (7).gameObject));

		//And logically (all game data)
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
		//LeaveInfo ();
	}

	public void Water4()
	{
		//The miniInfo disappears, the water can shows for 1 seconds, and then dies
		//Pretty much this is only visual porposes
		//panelRegularView.GetChild (8).gameObject.SetActive (false);
		//GameObject wateringCan = Instantiate (WateringCanPrefab, new Vector3 (-2f, 0.6f, 0f), Quaternion.identity);
		isWatering = true;
		StartCoroutine (WaitDestroyAndShow (Instantiate(WateringCanPrefab, new Vector3 (-2f, 0.6f, 0f), Quaternion.identity), panelRegularView.GetChild (8).gameObject));

		//And logically (all game data)
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
		//LeaveInfo ();
	}

	public void Water5()
	{
		//The miniInfo disappears, the water can shows for 1 seconds, and then dies
		//Pretty much this is only visual porposes
		//panelRegularView.GetChild (9).gameObject.SetActive (false);
		//GameObject wateringCan = Instantiate (WateringCanPrefab, new Vector3 (-0.5f, 0.6f, 0f), Quaternion.identity);
		isWatering = true;
		StartCoroutine (WaitDestroyAndShow (Instantiate(WateringCanPrefab, new Vector3 (-0.5f, 0.6f, 0f), Quaternion.identity), panelRegularView.GetChild (9).gameObject));

		//And logically (all game data)
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
		//LeaveInfo ();
	}

	public void Water6()
	{
		//The miniInfo disappears, the water can shows for 1 seconds, and then dies
		//Pretty much this is only visual porposes
		//panelRegularView.GetChild (10).gameObject.SetActive (false);
		//GameObject wateringCan = Instantiate (WateringCanPrefab, new Vector3 (1.15f, 0.6f, 0f), Quaternion.identity);
		isWatering = true;
		StartCoroutine (WaitDestroyAndShow (Instantiate(WateringCanPrefab, new Vector3 (1.15f, 0.6f, 0f), Quaternion.identity), panelRegularView.GetChild (10).gameObject));

		//And logically (all game data)
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
		//LeaveInfo ();
	}

	public void Water7()
	{
		//The miniInfo disappears, the water can shows for 1 seconds, and then dies
		//Pretty much this is only visual porposes
		//panelRegularView.GetChild (11).gameObject.SetActive (false);
		//GameObject wateringCan = Instantiate (WateringCanPrefab, new Vector3 (-2.15f, -1.5f, 0f), Quaternion.identity);
		isWatering = true;
		StartCoroutine (WaitDestroyAndShow (Instantiate(WateringCanPrefab, new Vector3 (-2.15f, -1.5f, 0f), Quaternion.identity), panelRegularView.GetChild (11).gameObject));

		//And logically (all game data)
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
		//LeaveInfo ();
	}

	public void Water8()
	{
		//The miniInfo disappears, the water can shows for 1 seconds, and then dies
		//Pretty much this is only visual porposes
		//panelRegularView.GetChild (12).gameObject.SetActive (false);
		//GameObject wateringCan = Instantiate (WateringCanPrefab, new Vector3 (-0.5f, -1.5f, 0f), Quaternion.identity);
		isWatering = true;
		StartCoroutine (WaitDestroyAndShow (Instantiate(WateringCanPrefab, new Vector3 (-0.5f, -1.5f, 0f), Quaternion.identity), panelRegularView.GetChild (12).gameObject));

		//And logically (all game data)
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
		//LeaveInfo ();
	}

	public void Water9()
	{
		//The miniInfo disappears, the water can shows for 1 seconds, and then dies
		//Pretty much this is only visual porposes
		//panelRegularView.GetChild (13).gameObject.SetActive (false);
		//GameObject wateringCan = Instantiate (WateringCanPrefab, new Vector3 (1.3f, -1.5f, 0f), Quaternion.identity);
		isWatering = true;
		StartCoroutine (WaitDestroyAndShow (Instantiate(WateringCanPrefab, new Vector3 (1.3f, -1.5f, 0f), Quaternion.identity), panelRegularView.GetChild (13).gameObject));

		//And logically (all game data)
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
		//LeaveInfo ();
	}

	public IEnumerator WaitOneUpdateCycle()
	{
		yield return null;
		//Debug.Log ("GGG, se esperó");
	}

	public IEnumerator WaitDestroyAndShow(GameObject prefab, GameObject show)
	{
		float timer = 0f;
		bool stopFlag = false;

		show.GetComponentInParent<RegularViewManager>().enabled = false;
		panelRegularView.GetChild (0).GetComponent<Button> ().interactable = false;
		panelRegularView.GetChild (1).GetComponent<Button> ().interactable = false;
		panelRegularView.GetChild (2).GetComponent<Button> ().interactable = false;
		panelRegularView.GetChild (14).GetComponent<Button> ().interactable = false;
		isWatering = true;
		show.SetActive(false);

		while (!stopFlag)
		{
			panelRegularView.GetChild (0).GetComponent<Button> ().interactable = false;
			panelRegularView.GetChild (1).GetComponent<Button> ().interactable = false;
			panelRegularView.GetChild (2).GetComponent<Button> ().interactable = false;
			panelRegularView.GetChild (14).GetComponent<Button> ().interactable = false;
			isWatering = true;
			show.GetComponentInParent<RegularViewManager>().enabled = false;
			yield return null;

			timer += Time.deltaTime;

			if (timer > 1f)
			{
				stopFlag = true;
			}
		}
			
		//yield return new WaitForSeconds (1f);
		Destroy (prefab);
		show.SetActive (true);
		show.GetComponentInParent<RegularViewManager>().enabled = true;
		panelRegularView.GetChild (0).GetComponent<Button> ().interactable = true;
		panelRegularView.GetChild (1).GetComponent<Button> ().interactable = true;
		panelRegularView.GetChild (2).GetComponent<Button> ().interactable = true;
		panelRegularView.GetChild (14).GetComponent<Button> ().interactable = true;
		isWatering = false;
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
