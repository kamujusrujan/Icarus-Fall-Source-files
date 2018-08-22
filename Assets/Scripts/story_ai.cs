using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class story_ai : MonoBehaviour {
	public Data d;
	string	path;
	string data;
	public Button[] b1;
	public Button[] b2;
	public Button[] b3;
	public GameObject[] blockui;
	public Text t;
	public GameObject dialougebox;
	public Player playercomp;
	int refe;
	 string path_persistnt;
	public Check1 data2;

	int[] xp = { 200,1500,3000,5500,8000,10000,12000,15000,17000,20000,23000,27000,30000,35000,50000 };
	void Awake()
	{
		print (xp.Length);
		path = Path.Combine (Application.streamingAssetsPath, "data.json");



		if (Application.platform == RuntimePlatform.WindowsEditor) {
			
			 data = File.ReadAllText (path);
			d = JsonUtility.FromJson<Data> (data);
		}

		if (Application.platform == RuntimePlatform.Android) {
			WWW sample = new WWW (path);
			if (sample.isDone) {
				 data = sample.text;
				d = JsonUtility.FromJson<Data> (data);

			}

		}
		CreatePersistentData ();
		gainDetector ();


	}

	void Start(){
		

	}
	void Update(){}

	void SetButtons(int  bnum){
		foreach (GameObject u in blockui) {
			u.SetActive (true);
		}

		for(int i = 0; i<= b1.Length -1 ; i++){
			int s = i;
			b1 [i].GetComponentInChildren<Text> ().text = d.Block [bnum].q[i];
			b1 [i].onClick.AddListener (()=> buttonActive(bnum,s,blockui[0]) );
		

		}
		for(int i = 0; i<= b1.Length -1 ; i++){
			int s1 = i;
			b2 [i].GetComponentInChildren<Text> ().text = d.Block [bnum +1 ].q[i];
			b2 [i].onClick.AddListener (()=> buttonActive(bnum +1,s1,blockui[1]) );
		}
		for(int i = 0; i<= b1.Length -1 ; i++){
			int s2= i;
			b3 [i].GetComponentInChildren<Text> ().text = d.Block [bnum + 2].q[i];
			b3 [i].onClick.AddListener (()=> buttonActive(bnum +2,s2,blockui[2]) );
		}


	}

	void buttonActive(int b,int x,GameObject ui){
		ui.SetActive (false);
		StartCoroutine (TW(d.Block [b].r [x]));
	}

	public Text gaintext;
	void gainDetector(){


		for (int i = 1; i <= xp.Length - 1; i++) {
			
			if (playercomp.data.Profile.Gain > xp [i - 1] && playercomp.data.Profile.Gain < xp [i]) {
				gaintext.text = xp [i].ToString();
				if(data2.Check[i-1]){
				//	mainAIobj.SetActive (true);
					SetButtons ((i-1)*3);
					data2.Check [i - 1] = false;
                    Googler.incrementsAch(GPGSIds.achievement_the_story);
					SavePdata ();
				}

			} 
		}
	}



	void SavePdata(){
	
		File.WriteAllText (path_persistnt,JsonUtility.ToJson(data2,true));
	}


	void CreatePersistentData(){
		 path_persistnt = Path.Combine (Application.persistentDataPath,"Check_data.json");



		if (!File.Exists (path_persistnt)) {
			string a = JsonUtility.ToJson (d);
			data2 = JsonUtility.FromJson<Check1> (a);
			File.CreateText (path_persistnt).Dispose();
			SavePdata ();
		} else {
			string read = File.ReadAllText (path_persistnt);
			data2 = JsonUtility.FromJson<Check1> (read);
		}

	}

//	void save(){
//		string a = JsonUtility.ToJson (d,true);
//		File.WriteAllText (path,a);
//	}
//

	public 	IEnumerator TW(string _msg){
		dialougebox.SetActive (true);
		string msg = _msg.ToUpper ();
		t.text = "";
		foreach (char letter in msg.ToCharArray()) 
		{

			t.text += letter;
			yield return new WaitForSeconds(0.09f);
		}
		yield return new WaitForSeconds (2);

		t.text = "";
		dialougebox.SetActive (false);
	}

	//public GameObject mainAIobj;
	public void Skip0(){

		blockui [0].SetActive (false);
	}
	public void Skip1(){

		blockui [1].SetActive (false);
	}
	public void Skip2(){

		blockui [2].SetActive (false);
	}




}

[System.Serializable]
public class Data{
	public Block[] Block;
	public bool[] Check;
}

[System.Serializable]
public class Block{
	public string[] q;
	public string[] r;

}
[System.Serializable]
public class Check1{
	public bool[] Check;
}

