using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour {
	public Texture[] Casetex;

	// Use this for initialization
	void Start () {
		int state = Random.Range (0,2);

		Renderer[] m =	 gameObject.GetComponentsInChildren<Renderer> ();
		foreach(Renderer i in m){
			Material[] mat = i.materials;
			foreach(Material y in mat){
				if (state == 0) {
					y.mainTexture = Casetex [0];
					gameObject.tag = "Money";
				} if (state == 1) {
					y.mainTexture = Casetex [1];
					gameObject.tag = "Inhale";
				} else {
					y.mainTexture = Casetex [2];
					gameObject.tag = "Unknown";
				}

				}
		}
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider cld) {
		if (cld.tag == "Obstacle" || cld.tag == "Money"|| cld.tag == "Inhale"|| cld.tag == "Unknown") {
			transform.position = transform.position + new Vector3 (0, 0, Random.Range (0, 10));
		
		}
		
	}
}
