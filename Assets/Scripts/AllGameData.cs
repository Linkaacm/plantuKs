using System.IO;
using UnityEngine;

public class AllGameData : MonoBehaviour
{
	public static AllGameData instance;
	public DataSample playerData;
	private string fileName;

	private StreamReader sr;
	private StreamWriter sw;
	private string textToFile;

	/*
	public void IniateContent()
	{
		instance = this;
		fileName = "GreenhouseData.json";
		playerData = new DataSample ();
		LoadData ();
	}
	*/
		
	void Awake()
	{
		instance = this;

		fileName = "NewGreenhouseData3.json";
		playerData = new DataSample ();
		LoadData ();
	}

	public void LoadData()
	{
		if (File.Exists (Application.persistentDataPath + "/" + fileName))
		{
			sr = new StreamReader (Application.persistentDataPath + "/" + fileName);
			textToFile = sr.ReadToEnd ();
			sr.Close ();
			playerData = JsonUtility.FromJson<DataSample> (textToFile);
		}
		else
		{
			for(int i = 0; i<9; i++)
			{
				playerData.plant_isUsed [i] = false;
				//playerData.plant_ageMonths [i] = 0;
				playerData.plant_isShiny [i] = false;
				playerData.plant_congratulations [i] = false;
				playerData.plant_name [i] = "name";
				playerData.plant_nickname [i] = "nickname";

				playerData.plant_lastTick [i] = 0;
				playerData.plant_tickDate [i] = 0;
				playerData.plant_yearDate [i] = 0;
				playerData.plant_monthDate [i] = 0;
				playerData.plant_dayDate [i] = 0;

				playerData.plant_phase [i] = 0;
				playerData.plant_timeLeftSeconds [i] = 0;

				playerData.trophy_isShown [i] = false;
				playerData.trophy_name [i] = "trophyName";
				playerData.trophy_nickname [i] = "trophyNickname";
				playerData.trophy_sowDate [i] = "trophysowDate";
				playerData.trophy_ageMonths [i] = 0;
			}

			playerData.trophy_isShown [9] = false;
			playerData.trophy_name [9] = "trophyName";
			playerData.trophy_nickname [9] = "trophyNickname";
			playerData.trophy_sowDate [9] = "trophysowDate";
			playerData.trophy_ageMonths [9] = 0;

			for (int i = 0; i < playerData.seedEnvelopes.Length; i++)
			{
				playerData.seedEnvelopes [i] = 0;
			}

		}

		/*
		if (transform.name.GetHashCode() == "JsonAtGame".GetHashCode())
		{
			//Debug.Log ("Alright, we read all data");
			//FindObjectOfType<SlotManager> ().GetAndSetAllSlotData ();
		}
		*/

	}

	public void SaveData()
	{
		sw = new StreamWriter (Application.persistentDataPath + "/" + fileName, false);
		textToFile = JsonUtility.ToJson (playerData);
		sw.Write (textToFile);
		sw.Close ();
	}
}
