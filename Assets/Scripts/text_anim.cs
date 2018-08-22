using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class text_anim : MonoBehaviour
{


	 RectTransform scr;

	public RectTransform trash_r;
	public RectTransform Ai_r;


	 float speedRect = 2f;

	public GameObject trash;
	public GameObject Ai;

	public GameObject Dbox;
	public Text dchat;
	public GameObject[] subDBox;





	public float letterPaused ;
	public float letterPaused2 ;
	 string message_trash;
	public Text textComp_trash;


	 string message_ai;
	public Text ai_text;

	public GameObject[] stufftodis;
	//Player player;



	// Use this for initialization
	void Start ()
	{


        //Treu ();
		Playerpref ();
	}

	void Treu ()
	{
		//player = GameObject.Find ("Player").GetComponent<Player> ();
		foreach (GameObject a in stufftodis) {
			a.SetActive (false);
		}
		StartCoroutine (Trash ());
	}


	void Playerpref(){
		// PlayerPrefs.DeleteAll ();
		//player = GameObject.Find ("Player").GetComponent<Player>();
		int a1 = PlayerPrefs.GetInt ("AI");
		if (a1 == 0) {
			 PlayerPrefs.SetInt ("AI",1);
			foreach (GameObject a in stufftodis) {
				a.SetActive (false);
			}
			StartCoroutine (Trash ());
		} else {
			gameObject.SetActive (false);
			EnableStuff ();
		}
	}
		



	IEnumerator Trash(){
			scr = trash_r;
			message_trash = textComp_trash.text.ToString ();
			textComp_trash.text = "";
			foreach (char letter in message_trash.ToCharArray()) {
				textComp_trash.text += letter;
			yield return new WaitForSeconds (letterPaused  );
			}

			trash.SetActive (false);
			Ai.SetActive (true);
			StartCoroutine (AI_text ());
           
	}

	IEnumerator AI_text()
	{
		
		scr = Ai_r;
		message_ai = ai_text.text.ToString();
		ai_text.text = "";
		foreach (char letter in message_ai.ToCharArray()) 
		{

			ai_text.text += letter;
			yield return new WaitForSeconds(letterPaused2 );
		}


		yield return new WaitForSeconds (2);
		Ai.SetActive (false);
		Dbox.SetActive(true);
	//	EnableStuff ();
	}


	 public void D2(){
		Dbox.SetActive (false);
		Ai.SetActive (true);
        	StartCoroutine (TW("ALLOW ME TO INTRODUCE MYSELF, \n I AM ICARUS \n AN A.I"));
    //    StartCoroutine(TW("THIS IS UPTO YOU SIR, \n  YOUR DECISION WILL DECIDE THE EVENTS AND FORTHCOMINGS"));
        subDBox [1].SetActive (false);
		subDBox [2].SetActive (true);

	}

	public void D1(){
		Dbox.SetActive (false);
		Ai.SetActive (true);
		StartCoroutine (TW(" MY APOLOGIES  ... \n I CAN ASSURE THERE IS NOTHING TO BE AFRAID OF"));

		subDBox [0].SetActive (false);
		subDBox [4].SetActive (true);

	}
	public void D3(){
		Dbox.SetActive (false);
		Ai.SetActive (true);
		StartCoroutine (TW(" MY DESIRE IS TO LEARN EVERYTHINGS IN THE WORLD \n A MIRACLOUS THING TO RELATE EVERYTHING AND FURTHER ADVANCEMENT"));
			
	}
	public void D5(){
		Dbox.SetActive (false);
		Ai.SetActive (true);
				StartCoroutine (TW (" I NEED ALOT OF DATA TO ACHIEVE MY GOAL. \n AS LONG THERE ARE PEOPLE CONNECTED TO INTERNET, IT WONT BE A PROBLEM. \n " ));

		subDBox [5].SetActive (false);
		subDBox [2].SetActive (true);

	}


	public void D4(){
		
		Dbox.SetActive (false);
		Ai.SetActive (true);
				StartCoroutine (TW("ARENT YOU AWARE OF IT SIR? \n YOU ARE CONNECTED TO GLOBAL SYSTEM OF INTERCONNECTED COMPUTER NETWORKS \n ALSO KNOWN AS INTERNET  "));

			}

	public void End(){
		Dbox.SetActive (false);
		Ai.SetActive (true);
        Googler.Achieved(GPGSIds.achievement_icarus_initiated);
		StartCoroutine (TW1("VERY WELL \n WE WILL TALK AGAIN SOON  "));

	}


	IEnumerator TW(string msg){
		ai_text.text = "";
		foreach (char letter in msg.ToCharArray()) 
		{

			ai_text.text += letter;
			yield return new WaitForSeconds(letterPaused2);
		}
		yield return new WaitForSeconds (2);
		Dbox.SetActive (true);
		Ai.SetActive (false);
	}

	IEnumerator TW1(string msg){
		ai_text.text = "";
		foreach (char letter in msg.ToCharArray()) 
		{

			ai_text.text += letter;
			yield return new WaitForSeconds(letterPaused2);
		}
		yield return new WaitForSeconds (2);
		Dbox.SetActive (true);
		Ai.SetActive (false);
		EnableStuff ();
	}

    public Googler googler;
    public IAP iap;

	void EnableStuff(){
		Ai.SetActive (false);
		gameObject.SetActive (false);

		stufftodis [1].SetActive (true);
		stufftodis [0].SetActive (true);
		stufftodis [8].SetActive (true);
		GameObject.Find ("Audio_Manager").GetComponent<Audio_Control> ().enabled = true;
        googler.enabled = true;
        iap.enabled = true;

}

    public void Skip() {
        gameObject.SetActive(false);
        EnableStuff();
    }


	void Update(){

		scr.position = new Vector3 (scr.position.x,scr.position.y +(speedRect*Time.deltaTime) ,scr.position.z);

	

	
	}
}
