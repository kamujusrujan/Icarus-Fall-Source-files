using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanagerMain : MonoBehaviour {

	public GameObject[] TaptoEnter;
	public GameObject[] PlayUI;
	public GameObject[] RunningElement;
	public GameObject[] GameoverUI;
	private GameObject Player1;

	public Material sky;

	public AudioClip[] starttrack;

	void Start(){
		Player1 = GameObject.Find ("Player");
		AudioSource src = GetComponent <AudioSource> ();
		src.clip = starttrack [Random.Range (0, starttrack.Length )];
		src.Play ();

	}
	void Update(){
		//float rotatesky =Time.deltaTime * 100 ;
		RenderSettings.skybox.SetFloat ("_Rotation",Time.time  );


		//GetComponent<Skybox> ().material.SetFloat ("_Rotation",Time.time * 3);

	}

	void Awake(){

		QualitySettings.vSyncCount = 0;  // VSync must be disabled
			Application.targetFrameRate = 30;
	}
	public void exit(){
		Application.Quit ();
	}


	public void Playbutton(){

		for( int i =1; i <= PlayUI.Length; i++){
			PlayUI [i-1].SetActive (false);
		}
		for( int i =1; i <= RunningElement.Length; i++){
			RunningElement[i-1].SetActive (true);
		}

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

	public void tapbutton(){
		for( int i =1; i <= TaptoEnter.Length; i++){
			TaptoEnter [i-1].SetActive (false);
		}
		for( int i =1; i <= PlayUI.Length; i++){
			PlayUI [i-1].SetActive (true);
		}
		// Player.GetComponent<Player> ().enabled = true;
		Player1.GetComponent<Player>().StartWatch();
		Player.speedcontrol = 1;
		Player1.GetComponent<PlayerStats> ().enabled = false;


	}


	public void TryAgain(){
	//	Player.SetActive (false);
	//	Player.SetActive (true);

	//	Player.GetComponent<Player> ().enabled = false;
	//	Player1.GetComponent<TunnelPlayer> ().enabled = false;

		Player1.transform.position =  new Vector3(Player1.transform.position.x,0.4f,Player1.transform.position.z );	
	//	Player1.transform.position.y = 0.4f;
		Player1.GetComponent<Player> ().Reset ();
		Time.timeScale = 1;

	GameObject[] s = GameObject.FindGameObjectsWithTag ("Obstacle");
		for( int i =1; i <= s.Length; i++){
			s [i-1].SetActive (false);
		}
		for( int i =1; i <= GameoverUI.Length; i++){
			GameoverUI [i-1].SetActive (false);
		}

	}



}
