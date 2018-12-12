using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class DataSample
{
	public bool hasAlreadyData = false;
	public int koins = 50000;
	public short flowerPots = 0;
	public short manure = 0;
	public short[] seedEnvelopes = new short[6];

	public bool[] plant_isUsed = new bool[9];
	public bool[] plant_isShiny = new bool[9];
	public string[] plant_name = new string[9];
	public string[] plant_nickname = new string[9];

	public ulong[] plant_lastTick = new ulong[9];

	public ulong[] plant_tickDate = new ulong[9];
	public int[] plant_yearDate = new int[9];
	public int[] plant_monthDate = new int[9];
	public int[] plant_dayDate = new int[9];
	//public short[] plant_ageMonths = new short[9];

	public short[] plant_phase = new short[9];
	public int[] plant_timeLeftSeconds = new int[9];



	public bool[] trophy_isShown = new bool[10];
	public short[] trophy_ageMonths = new short[10];
	public string[] trophy_sowDate = new string[10];
	public string[] trophy_name = new string[10];
	public string[] trophy_nickname = new string[10];

	/*
	public bool plant01_isUsed = false;
	public bool plant01_isShiny = false;
	public string plant01_name = "-";
	public short plant01_phase = 0;
	public short plant01_timeLeftSeconds = 0;
	public short plant01_timeLeftMinutes = 0;
	public short plant01_timeLeftHours = 0;

	public bool plant02_isUsed = false;
	public bool plant02_isShiny = false;
	public string plant02_name = "-";
	public short plant02_phase = 0;
	public short plant02_timeLeftSeconds = 0;
	public short plant02_timeLeftMinutes = 0;
	public short plant02_timeLeftHours = 0;

	public bool plant03_isUsed = false;
	public bool plant03_isShiny = false;
	public string plant03_name = "-";
	public short plant03_phase = 0;
	public short plant03_timeLeftSeconds = 0;
	public short plant03_timeLeftMinutes = 0;
	public short plant03_timeLeftHours = 0;

	public bool plant04_isUsed = false;
	public bool plant04_isShiny = false;
	public string plant04_name = "-";
	public short plant04_phase = 0;
	public short plant04_timeLeftSeconds = 0;
	public short plant04_timeLeftMinutes = 0;
	public short plant04_timeLeftHours = 0;

	public bool plant05_isUsed = false;
	public bool plant05_isShiny = false;
	public string plant05_name = "-";
	public short plant05_phase = 0;
	public short plant05_timeLeftSeconds = 0;
	public short plant05_timeLeftMinutes = 0;
	public short plant05_timeLeftHours = 0;

	public bool plant06_isUsed = false;
	public bool plant06_isShiny = false;
	public string plant06_name = "-";
	public short plant06_phase = 0;
	public short plant06_timeLeftSeconds = 0;
	public short plant06_timeLeftMinutes = 0;
	public short plant06_timeLeftHours = 0;

	public bool plant07_isUsed = false;
	public bool plant07_isShiny = false;
	public string plant07_name = "-";
	public short plant07_phase = 0;
	public short plant07_timeLeftSeconds = 0;
	public short plant07_timeLeftMinutes = 0;
	public short plant07_timeLeftHours = 0;

	public bool plant08_isUsed = false;
	public bool plant08_isShiny = false;
	public string plant08_name = "-";
	public short plant08_phase = 0;
	public short plant08_timeLeftSeconds = 0;
	public short plant08_timeLeftMinutes = 0;
	public short plant08_timeLeftHours = 0;

	public bool plant09_isUsed = false;
	public bool plant09_isShiny = false;
	public string plant09_name = "-";
	public short plant09_phase = 0;
	public short plant09_timeLeftSeconds = 0;
	public short plant09_timeLeftMinutes = 0;
	public short plant09_timeLeftHours = 0;

	public bool trophy01_isShown = false;
	public short trophy01_age = 0;
	public string trophy01_date = "***";
	public string trophy01_name = "-";
	public string trophy01_nickname = "--";

	public bool trophy02_isShown = false;
	public short trophy02_age = 0;
	public string trophy02_date = "***";
	public string trophy02_name = "-";
	public string trophy02_nickname = "--";

	public bool trophy03_isShown = false;
	public short trophy03_age = 0;
	public string trophy03_date = "***";
	public string trophy03_name = "-";
	public string trophy03_nickname = "--";

	public bool trophy04_isShown = false;
	public short trophy04_age = 0;
	public string trophy04_date = "***";
	public string trophy04_name = "-";
	public string trophy04_nickname = "--";

	public bool trophy05_isShown = false;
	public short trophy05_age = 0;
	public string trophy05_date = "***";
	public string trophy05_name = "-";
	public string trophy05_nickname = "--";

	public bool trophy06_isShown = false;
	public short trophy06_age = 0;
	public string trophy06_date = "***";
	public string trophy06_name = "-";
	public string trophy06_nickname = "--";

	public bool trophy07_isShown = false;
	public short trophy07_age = 0;
	public string trophy07_date = "***";
	public string trophy07_name = "-";
	public string trophy07_nickname = "--";

	public bool trophy08_isShown = false;
	public short trophy08_age = 0;
	public string trophy08_date = "***";
	public string trophy08_name = "-";
	public string trophy08_nickname = "--";

	public bool trophy09_isShown = false;
	public short trophy09_age = 0;
	public string trophy09_date = "***";
	public string trophy09_name = "-";
	public string trophy09_nickname = "--";

	public bool trophy10_isShown = false;
	public short trophy10_age = 0;
	public string trophy10_date = "***";
	public string trophy10_name = "-";
	public string trophy10_nickname = "--";
	*/

}