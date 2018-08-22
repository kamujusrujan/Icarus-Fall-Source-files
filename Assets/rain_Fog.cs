using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rain_Fog : MonoBehaviour {

	public GameObject[] fog;
	public GameObject[] snow;
	public GameObject[] rain;


	public int w;

	void Weather ()
	{
		Player playcom = GameObject.Find ("Player").GetComponent<Player> ();
		w = playcom.weather;
		if (w == 0) {
			for (int i = 0; i < fog.Length; i++) {
				Destroy (fog [i]);
			}
		}
		else {
			for (int i = 0; i < rain.Length; i++) {
				Destroy (rain [i]);
			}
		}
	}

	// Use this for initialization
	void Start () {
		Weather ();

	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
