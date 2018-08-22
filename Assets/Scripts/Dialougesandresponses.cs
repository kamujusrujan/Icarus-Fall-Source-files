using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Dialougesandresponses : MonoBehaviour {

	string path;
	public 	QnA ai;
	public GameObject[] buttons;
	//text_anim method;
	public Text ai_text;
	public GameObject dbox;
	public GameObject ai2;
	void Awake(){

		//method = GetComponent<text_anim> ();
		if (Application.platform == RuntimePlatform.WindowsEditor) {
	//		path = Application.dataPath + "/StreamingAssets";
			path = Path.Combine (Application.streamingAssetsPath ,"icarus.json");
			print (path);
			string data = File.ReadAllText (path);

			ai = JsonUtility.FromJson<QnA> (data);


		}
		if (Application.platform == RuntimePlatform.Android) {
			//path = "jar:file://" + Application.dataPath + "!/assets/";
		
			path = Path.Combine (Application.streamingAssetsPath,"icarus.json");
			WWW sample = new WWW (path);
			if(sample.isDone){
				string data = sample.text;
				ai = JsonUtility.FromJson<QnA> (data);
			}


		}
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
		//	path = Application.dataPath + "/Raw";
			path = Path.Combine (Application.streamingAssetsPath ,"icarus.json");
			// string path = Path.Combine ("jar:file://" + Application.streamingAssetsPath + "!assets/", "icarus.json");

			print (path);
			string data = File.ReadAllText (path);
			ai = JsonUtility.FromJson<QnA> (data);
		}

		print (ai.responses.Length);

	}


	public void Resp(int i ){
		StartCoroutine (TW(ai.responses[i]));
	}
	public void Playgame(int  i ){
		StartCoroutine (TW(ai.responses[i]));
	}
	void Start(){
		for (int i = 0; i <= buttons.Length - 1 ; i++) {
			int a = i;
			buttons[i].GetComponentInChildren<Text> ().text = ai.question [i];
			buttons [i].GetComponentInChildren<Button> ().onClick.AddListener (() => Resp(a) );

		}
			Activation ();
	}



	public 	IEnumerator TW(string msg){
		dbox.SetActive (false);
		ai_text.text = "";
		foreach (char letter in msg.ToCharArray()) 
		{

			ai_text.text += letter;
			yield return new WaitForSeconds(0.09f);
		}
		yield return new WaitForSeconds (2);

		ai_text.text = "";
		dbox.SetActive (true);
	}



	int[] xp = {0,1000,5000,10000,20000};

	public void Playgame(){

		ai2.SetActive (false);

	}
	void Activation(){

		Player playercom = GameObject.Find ("Player").GetComponent<Player> ();
		for (int i = 1; i <= xp.Length -1  ; i++) {
			int s = i;
			print (xp.Length);

			// print (xp[s]+ "anmd " + xp[s+1]);
			if (playercom.data.Profile.Gain > xp [s-1] && playercom.data.Profile.Gain < xp [s]) {
				print ("passed 1 if");
				if(ai.check[i-1]){
					print ("passed 2 if");
					ai2.SetActive (true);
					for (int y =( (i-1)*3) + 1; y <= ((i)* 3) ; y++) {
						int r = y;
						buttons [r-1].SetActive (true);
						ai.check [s-1] = false;
						save ();

					}

				}
				break;
			}
			

		}
	}

	void save(){
		string a = JsonUtility.ToJson (ai,true);
		File.WriteAllText (path,a);
	}

}



[System.Serializable]
public class QnA{

	public string[] question;
	public string[] responses;
	public bool[] check ;

}
