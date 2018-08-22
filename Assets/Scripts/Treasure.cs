using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour {

	public Texture[] treasuretex;
	// Use this for initialization
	void Start () {
		int state = Random.Range (0,2);

		Renderer[] m =	 gameObject.GetComponentsInChildren<Renderer> ();
		foreach(Renderer i in m){
			Material[] mat = i.materials;
			foreach(Material y in mat){
				if (state == 0) {
					y.mainTexture = treasuretex [0];
					gameObject.tag = "Money";
				} if (state == 1) {
					y.mainTexture = treasuretex [1];
					gameObject.tag = "Inhale";
				} else {
					y.mainTexture = treasuretex [2];
					gameObject.tag = "Unknown";
				}

			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
