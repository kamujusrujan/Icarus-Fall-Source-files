using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
	 




	// Use this for initialization
	void Start () {
		startup ();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.Translate (Vector3.forward * 3 * Time.deltaTime);
	}

	public void startup(){
		
		Player.speedcontrol = 3;
		// GetComponent<Player> ().speedcontrol = 3;
		GetComponent<Player> ().enabled = false;
		GameObject stufftodisable = GameObject.Find ("Spawn Manager");
		stufftodisable.GetComponentInChildren<Spawner> ().enabled = false;
		stufftodisable.GetComponentInChildren<EnviSpawner> ().lagtime = 40;
		GetComponent<BoxCollider> ().enabled = false;

	}
/*
	public void Playbutton(){
		GetComponent<Player> ().enabled = true;
		GameObject stufftodisable = GameObject.Find ("Spawn Manager");
		stufftodisable.GetComponentInChildren<Spawner> ().enabled = true;
		stufftodisable.GetComponentInChildren<EnviSpawner> ().lagtime = 20;
		GetComponent<PlayerStats> ().enabled = false;
		GetComponent<Player> ().speedcontrol = 50;

	}

	public void tapbutton(){
		GetComponent<Player> ().enabled = true;
		GetComponent<Player> ().speedcontrol = 1;
		GetComponent<PlayerStats> ().enabled = false;

	}
*/


}
