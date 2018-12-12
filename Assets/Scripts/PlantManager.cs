using UnityEngine;
using UnityEngine.UI;

public class PlantManager : MonoBehaviour {

	public Transform panelStats;
	//private Transform facebookButton;
	//private Transform cancelButton;
	private Transform nameText;
	private Transform dateText;
	private Transform ageText;
	private Transform phaseText;
	private Transform spritePlant;

	[HideInInspector] public string plantName;
	[HideInInspector] public string plantDate;
	[HideInInspector] public short plantAge;
	[HideInInspector] public short plantPhase;


	// Use this for initialization
	void Start ()
	{
		//facebookButton = panelStats.GetChild (3);
		//cancelButton = panelStats.GetChild (4);
		nameText = panelStats.GetChild (5);
		dateText = panelStats.GetChild (6);
		ageText = panelStats.GetChild (7);
		phaseText = panelStats.GetChild (8);
		spritePlant = panelStats.GetChild (9);

	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && FindObjectOfType<MasterG> ().panelRegularView.gameObject.activeSelf)
		{
			CheckTouchSelection (Input.GetTouch (0));
		}

		/*
		else if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && FindObjectOfType<MasterG> ().panelSowing.gameObject.activeSelf)
		{
			
		}
		*/
	}

	void CheckTouchSelection(Touch auxTouch)
	{
		RaycastHit hit = new RaycastHit ();
		Ray mouseRay = Camera.main.ScreenPointToRay (auxTouch.position);

		if (Physics.Raycast (mouseRay.origin, mouseRay.direction, out hit))
		{
			if (hit.transform.name == this.transform.name && hit.transform.gameObject.GetComponent<SpriteRenderer> ().enabled)
			{
				//Debug.Log ("I hit " + hit.transform.name + " . Speaking from " + this.transform.name);
				//Debug.Log ("(If the slot is not used, then you will no see me prompting)");

				nameText.GetComponent<Text> ().text = plantName;
				dateText.GetComponent<Text> ().text = plantDate;
				ageText.GetComponent<Text> ().text = plantAge.ToString ();
				phaseText.GetComponent<Text> ().text = "Fase " + plantPhase.ToString ();
				spritePlant.GetComponent<Image> ().sprite = hit.transform.gameObject.GetComponent<SpriteRenderer> ().sprite;


				FindObjectOfType<MasterG> ().LeaveInfo ();
				FindObjectOfType<MasterG> ().panelRegularView.gameObject.SetActive (false);
				panelStats.gameObject.SetActive (true);
			}

		}
	}
}
