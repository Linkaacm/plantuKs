using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
	public Text cactusNumber;
	public Text cempaNumber;
	public Text giraNumber;
	public Text narciNumber;
	public Text nocheNumber;
	public Text sabiNumber;
	public Text manureNumber;
	public Text potsNumber;

	void Start ()
	{
		UpdateAllText ();
	}
	
	void Update ()
	{
		
	}

	public void UpdateAllText()
	{
		cactusNumber.text = "x" + AllGameData.instance.playerData.seedEnvelopes [0].ToString ();
		cempaNumber.text = "x" + AllGameData.instance.playerData.seedEnvelopes [1].ToString ();
		giraNumber.text = "x" + AllGameData.instance.playerData.seedEnvelopes [2].ToString ();
		narciNumber.text = "x" + AllGameData.instance.playerData.seedEnvelopes [3].ToString ();
		nocheNumber.text = "x" + AllGameData.instance.playerData.seedEnvelopes [4].ToString ();
		sabiNumber.text = "x" + AllGameData.instance.playerData.seedEnvelopes [5].ToString ();
		manureNumber.text = "x" + AllGameData.instance.playerData.manure.ToString ();
		potsNumber.text = "x" + AllGameData.instance.playerData.flowerPots.ToString ();
	}
}
