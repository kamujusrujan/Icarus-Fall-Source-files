using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {
	public Player p;

	void Update(){
		transform.Translate (Vector3.forward * (Player.speedcontrol - 10) * Time.deltaTime);
	}


	void OnTriggerEnter(Collider destroy){

		Destroy(destroy.gameObject);
	}

}
