using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Interface : MonoBehaviour {



	public GameObject Play_Screen;
	public GameObject Menu_Screen;
	public GameObject Profile_Layout;
	public GameObject Shop_layout;
	public GameObject Notification_Layout;
	public GameObject Control_System;


	private GameObject Player1;
	public List<GameObject> UI;


	public GameObject ImageScreening;
	public Sprite[] effects;

	void Awake(){
		 


		UI = new List<GameObject> ();
		UI.Add (Play_Screen);
		UI.Add (Menu_Screen);
		UI.Add (Profile_Layout);
		UI.Add (Shop_layout);
		UI.Add (Notification_Layout);
		UI.Add (Control_System);
		/*
		foreach(GameObject interface1 in UI){
			
			interface1.SetActive (false);
		}

		UI [0].SetActive (true);
	

		Play_Screen.SetActive (false);
		Menu_Screen.SetActive (false);
		Profile_Layout.SetActive (false);
		Shop_layout.SetActive (false);
		Notification_Layout.SetActive (false);
		Control_System.SetActive (false);
		*/

}


	void Start(){
		
			Player1 = GameObject.Find ("Player");
		ImageScreen ();
	}

	public void Play_B(){
		UI [0].SetActive (false);
		Player1.GetComponent<Player> ().CameraAnim.Play ("c_start2Run");
		Player1.GetComponent<Player> ().watchanimation.Play ("idle");


	//	GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
		Player1.GetComponent<Player> ().enabled = true;
		GameObject stufftodisable = GameObject.Find ("Spawn Manager");
		stufftodisable.GetComponentInChildren<Spawner> ().enabled = true;
		stufftodisable.GetComponentInChildren<EnviSpawner> ().lagtime = 20;
		Player1.GetComponent<PlayerStats> ().enabled = false;
		Player.speedcontrol= 50;
	}


	void ImageScreen(){
		
		Image test = ImageScreening.GetComponent<Image> ();
		test.sprite = effects [Random.Range (0, effects.Length)];
		Color alpha = test.color;
		alpha.a = Random.Range (0.25f,0.6f);
		test.color = alpha;
	

	}



}
