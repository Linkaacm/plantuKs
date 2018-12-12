using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
	public Sprite[] allSprites;
	private Image firstSeed;
	private Image secondSeed;
	private Image thirdSeed;
	[HideInInspector] public short orderOfSprites;


	void Awake ()
	{
		//You know the drill, access SeedsMiniPanel, then the respective seed and voilá
		firstSeed = this.transform.GetChild (5).GetChild (0).GetComponent<Image> ();
		secondSeed = this.transform.GetChild (5).GetChild (1).GetComponent<Image> ();
		thirdSeed = this.transform.GetChild (5).GetChild (2).GetComponent<Image> ();

		//orderOfSprites = 0;
		//Debug.Log("calling from Awake function, about to start Render Functon");
		//RenderWhichSprite (orderOfSprites);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void UpdateOrderOfSprites(short newValue)
	{
		orderOfSprites = newValue;
	}

	public void UpdateKoinText()
	{
		this.transform.GetChild (4).GetChild (1).GetComponent<Text> ().text = AllGameData.instance.playerData.koins.ToString ();
	}

	public void UpdateSprites(short state)
	{
		switch (state)
		{
		case 0:
			firstSeed.sprite = allSprites [0];
			secondSeed.sprite = allSprites [1];
			thirdSeed.sprite = allSprites [2];
			break;
		case 1:
			firstSeed.sprite = allSprites [1];
			secondSeed.sprite = allSprites [2];
			thirdSeed.sprite = allSprites [3];
			break;
		case 2:
			firstSeed.sprite = allSprites [2];
			secondSeed.sprite = allSprites [3];
			thirdSeed.sprite = allSprites [4];
			break;
		case 3:
			firstSeed.sprite = allSprites [3];
			secondSeed.sprite = allSprites [4];
			thirdSeed.sprite = allSprites [5];
			break;
		case 4:
			firstSeed.sprite = allSprites [4];
			secondSeed.sprite = allSprites [5];
			thirdSeed.sprite = allSprites [0];
			break;
		case 5:
			firstSeed.sprite = allSprites [5];
			secondSeed.sprite = allSprites [0];
			thirdSeed.sprite = allSprites [1];
			break;
		default:
			firstSeed.sprite = allSprites [0];
			secondSeed.sprite = allSprites [1];
			thirdSeed.sprite = allSprites [2];
			break;
		}
	}
}
