using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MasterM : MonoBehaviour
{
	public RectTransform noDataPanel;
	public RectTransform withDataPanel;

	void Start ()
	{
		//At this point, we got all data (if any) at the file "GreenhouseData.json" thanks to AllGameData.cs (instance) attached to an empty GameObject
		//--

		//Debug.Log ("El tiempo en este momento es de: " + System.DateTime.Now);
		//Debug.Log (AllGameData.instance.playerData.plant_isUsed[0]);
		//AllGameData.instance.SaveData ();


		if (AllGameData.instance.playerData.hasAlreadyData)
		{
			//Debug.Log ("El archivo ya existía y su valor es de: " + AllGameData.instance.playerData.plant_name [0]);
			withDataPanel.gameObject.SetActive (true);
			AllGameData.instance.SaveData ();
		}
		else
		{
			//Debug.Log ("El archivo no existía y se ha creado!");
			noDataPanel.gameObject.SetActive (true);
			AllGameData.instance.playerData.hasAlreadyData = true;
			//AllGameData.instance.playerData.plant_name [0] = "Archivo guardado!!!";
			AllGameData.instance.SaveData ();
		}
		//Armando Arturo Cruz Mendoza. All right, again. My name is Armando Arturo Cruz Mendoza. 

		//Debug.Log (AllGameData.instance.playerData.plant01_name);
	}

	void Update()
	{
		Debug.Log (System.DateTime.Now.Ticks);
	}

	public void ChargeScene()
	{
		SceneManager.LoadScene (1, LoadSceneMode.Single);
	}
		
}
