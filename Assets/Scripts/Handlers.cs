using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handlers : MonoBehaviour {

	 public GameObject[] handler;
	// static int f;
	private Vector3 Spawnposition;
	private GameObject Player;

	void Start(){
		Player = GameObject.Find ("Player");
		
	}



	void OnTriggerEnter(Collider cld){
		if (cld.tag == "Player") {
	//		print ("Spawn handler");
			int r = Random.Range (0, 2);
			if (r == 0 && TunnelPlayer.getgnd == true  ) {
				//GameObject Handler = (GameObject)Instantiate (handler[1], HandlerSpawnManager (Player.transform.position), transform.rotation);
				Instantiate (handler[1], HandlerSpawnManager (Player.transform.position), transform.rotation);

			} else {
				//		Vector3 relative = new Vector3 (0,0,53);
				Instantiate (handler[0], HandlerSpawnManager (Player.transform.position), transform.rotation);
				//	Handler.tag = "Handlers";
			}
		}
	}


	Vector3 HandlerSpawnManager(Vector3 _previousSpawn){
	//	print (_previousSpawn.x);


			Vector3 relative = new Vector3 (0, 0, 50.4f);
		if (_previousSpawn.x == -5 ) {
				int r = Random.Range (0, 3);
				if (r == 0) {
					Spawnposition = new Vector3 (-5, transform.position.y, transform.position.z) + relative;
				}
				if (r == 1) {
					Spawnposition = new Vector3 (0f, transform.position.y, transform.position.z) + relative;
				}
				if (r == 2) {
					Spawnposition = new Vector3 (-10f, transform.position.y, transform.position.z) + relative;
				}
			}
		if (_previousSpawn.x == 0f || _previousSpawn.x == 0) {
			
				int r1 = Random.Range (0, 2);
				if (r1 == 0) {
					Spawnposition = new Vector3 (-5, transform.position.y, transform.position.z) + relative;
				}
				if (r1 == 1) {
					Spawnposition = new Vector3 (0, transform.position.y, transform.position.z) + relative;
				}

			}
		if (_previousSpawn.x == -10f|| _previousSpawn.x == -10f) {
				int r2 = Random.Range (0, 2);
				if (r2 == 0) {
					Spawnposition = new Vector3 (-10f, transform.position.y, transform.position.z) + relative;
				}
				if (r2 == 1) {
					Spawnposition = new Vector3 (-5F, transform.position.y, transform.position.z) + relative;
				}

			}

		return Spawnposition;
		}


}
