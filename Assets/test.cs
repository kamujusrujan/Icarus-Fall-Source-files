using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
	public GameObject sewer;
	public GameObject[] sewerstrt;
	public Transform tunnelmain;

	public GameObject handler;
	void OnTriggerEnter( Collider cld){
		if (cld.tag == "ManHole") {
			Vector3 areal = new Vector3 (transform.position.x, -10, transform.position.z);
			print ("spawing sewer from manhole");
			transform.position = areal;

			GameObject tunnel = (GameObject)Instantiate (sewerstrt [PresentPosition ()], areal, transform.rotation);
			tunnel.tag = "Sewer";
			tunnel.transform.SetParent (tunnelmain);

		}

		if (cld.tag == "Sewer") {
			print ("Spawning sewer from other sewer");
			GameObject sewer1 = (GameObject)Instantiate (sewer, cld.gameObject.transform.position + new Vector3 (0, 0, 500), transform.rotation);
			sewer1.tag = "Sewer";
			sewer1.transform.SetParent (tunnelmain);
		

			if (cld.tag == "middle handler") {
				int r = Random.Range (0, 3);
				if (r == 0) {
					
				}
				if (r == 1) {
					
				}
				if (r == 2) {

				}
			}
			if (cld.tag == "left handler") {
				int r = Random.Range (0, 2);
				if (r == 0) {
				
				}
				if (r == 1) {
			    
				}
			}
			if (cld.tag == "right handler") {
				int r = Random.Range (0, 2);
				if (r == 0) {
				
				}
				if (r == 1) {
				
				}
			}


		}
	}




		int position;
	int PresentPosition(){
		// gets the position and returns the integer mapping
		// 1 is middle 0 is right 2 is left
	

		if (transform.position.x == -87) {
			position = 1;
		}if (transform.position.x == -50) {
			position = 0;
		}
		if (transform.position.x == -124) {
			position = 2;
		}
		return position;
	}

	void Update(){
		transform.Translate (Vector3.forward * 50 * Time.deltaTime * 4);
	}

	public	void SpawnHandlers( int _present){

		Vector3 spawnpoint;

		if (_present == 1) {
			int r = Random.Range (0, 2);
			if (r == 1) {
				// spawn left i.e. spawnpostin is 2 
				spawnpoint = new Vector3 (54,transform.position.y,transform.position.z + 212);
				GameObject c = (GameObject) Instantiate( handler, spawnpoint,transform.rotation);
				c.tag = "left handler";
			}if(r ==0){
				//spawn right i,e spawnpoint is 0
				spawnpoint = new Vector3 (122,transform.position.y,transform.position.z + 212);
				GameObject c = (GameObject) Instantiate( handler, spawnpoint,transform.rotation);
				c.tag = "right handler";
			}


		}
		if (_present == 0) {
			spawnpoint = new Vector3 (88,transform.position.y,transform.position.z + 212);
			GameObject c = (GameObject) Instantiate( handler, spawnpoint,transform.rotation);
			c.tag = " middle handler";
			// spawn  cenrtre i,e spawnpoint 1
		}
		if (_present == 2) {
			spawnpoint = new Vector3 (88,transform.position.y,transform.position.z+212);
			GameObject c = (GameObject) Instantiate( handler, spawnpoint,transform.rotation);
			c.tag = "middle handler";
			// spawn centre i,e spawnpiuint is 1
		}


	}



}
