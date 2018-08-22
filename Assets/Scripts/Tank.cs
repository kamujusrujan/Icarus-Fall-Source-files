using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {

	public int h = 1;

	 Player playercomp;
	 public GameObject player;
	void Start(){
		player = GameObject.Find ("Player");
		playercomp = player.GetComponent<Player>();
		Invoke ("End",playercomp.data.Power.TankTime);
		Invoke ("D",30);
	}
	void LateUpdate(){
		transform.Translate (Vector3.forward *Player.speedcontrol * Time.deltaTime * h);
	}

	void OnTriggerEnter(Collider cld){

		if (cld.tag == "Obstacle" || cld.tag == "Passive" || cld.tag =="Money" || cld.tag == "Inhale") {
			Destroy (cld.gameObject);
		}
	}

	void End(){
		h = 0;
			
		if (gameObject.activeSelf) {
						

			if (player.transform.position.x == -5) {
				playercomp.ShiftLeft ();
			}
			Vector3 s = transform.position + new Vector3 (0, 0, -Player.speedcontrol * 1.5f);
			StartCoroutine (Player.SmoothM (gameObject, transform.position, s, 0.8f));
			// Destroy (gameObject);
		}
	}

	void D(){
		Destroy (gameObject);

	}
}
