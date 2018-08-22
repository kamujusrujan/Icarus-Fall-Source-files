using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manhole : MonoBehaviour {

	// 0 is right 1 is middle 2 is left

	public GameObject[] SewerPosition;
	public GameObject tunnel;
	 // public Vector3 relative;
	private GameObject Player;

	void Start(){
		Player = GameObject.Find ("Player");
//		int random = Random.Range (0,3);
//		if (random == 2 ) {
//			GetComponent<BoxCollider> ().enabled = true;
//		} else {
//			GetComponent<BoxCollider> ().enabled = false;
//			Destroy (gameObject);
//		}

	}

	void OnTriggerEnter(Collider cld){

		if (cld.tag == "Player") {
			
			Player.GetComponent<Player> ().enabled = false;
			Player.GetComponent<TunnelPlayer> ().enabled = true;
			Player.GetComponent<TunnelPlayer> ().Setup ();
		}
	}



/*	void OnTriggerEnter(Collider cld){

		if (cld.tag == "Player") {
			Player.GetComponent<Player> ().enabled = false;
			Player.GetComponent<TunnelPlayer> ().enabled = true;
			Vector3 relative = new Vector3 (0,-12,0);
			GameObject Sewer = (GameObject) Instantiate (StateSpawn(Player.transform.position),Player.transform.position + relative , transform.rotation);
			Sewer.tag ="Sewer";
	

		}
}
			GameObject StateSpawn(Vector3 _postion){
		GameObject returngame;
		if (_postion.x == -5) {
		//middle
			return	returngame = SewerPosition[1];
		}
		if (_postion.x == 0) {
		//right
			return returngame = SewerPosition[0];
		}
		if (_postion.x == -10) {
			//left
			return returngame = SewerPosition [2];
		} else
			return null;

	}
*/







}
