using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Animator))]

[RequireComponent(typeof(Rigidbody))]


public class DynamicObstacles : MonoBehaviour {



	private float[] ShiftingPlaces = {-5 ,0 ,-10};	
	private float referencetime;
	private GameObject player;
	public Animator Dynamicanimation;
	 float lag;
	//Vector3 presentlocation;
	float speed = 25;
	Vector3 p;



	public Texture[] texture;

//	public AudioClip[] vech; 
	 float[] timesetting = {0,0};
	void Start(){
		if (UI_managment.IsDifficult == 0) {
			timesetting [0] = 1.5f;
			timesetting [1] = 4f;
			print ("low settinfs ");
		} else {
			timesetting [0] = 1.5f;
			timesetting [1] = 2f;
			print ("High settings");
		}
		player = GameObject.Find ("Player");
		TextureChange ();
//		AudioSource	s = gameObject.AddComponent<AudioSource> ();
		gameObject.AddComponent<Rigidbody> ();
		Rigidbody rb =	GetComponent<Rigidbody> ();
		rb.isKinematic = true;
		rb.useGravity = false;
		StartCoroutine (Shift ());
		Dynamicanimation.GetComponent<Animator> ();
		 
//		s.clip = vech[Random.Range(0,vech.Length)];
//		s.Play ();
//		s.volume = 1f;
//		s.spatialBlend = 1;
//		s.priority = 80;
	}
	void Update(){
		transform.Translate (Vector3.back * speed * Time.deltaTime);
		referencetime = referencetime + Time.deltaTime; 
		 end ();


	}


	void TextureChange(){
	//	print (texture.Length);
		int random = Random.Range (0,texture.Length );
		Renderer[] m = GetComponentsInChildren<Renderer> ();
		foreach (Renderer i in m) {
			Material[] mat = i.materials;
			foreach(Material y in mat){
				y.mainTexture = texture[random];

				}
		}
	
	}



	void end(){
		
		if( player.transform.position.z > transform.position.z + 20){
			Destroy (gameObject);
		}
	}

	IEnumerator Shift(){
		Dynamicanimation = GetComponent<Animator> ();

		bool r = true;
		//presentlocation = transform.position;

		if(transform.position.z - player.transform.position.z < Random.Range(90,100)){
			yield  break;
		}
		while (r) {
			
			Vector3 desiredP =shiftplace(transform.position);
			animationcontrol (transform.position , desiredP);
			StartCoroutine (Player.SmoothM(gameObject,transform.position,desiredP,0.7f));
			yield return new WaitForSeconds (Lag());
		}

	}

	float Lag ()	{
		if ( Mathf.Abs( transform.position.z) - Mathf.Abs( player.transform.position.z) < Random.Range(150,300)) {
			lag = Random.Range (6,10);
		} else {
			lag = Random.Range (timesetting[0],timesetting[1]);
		}
		return lag;
	}

	Vector3 shiftplace(Vector3 _present){
		if (_present.x == -5) {
			return new Vector3 (ShiftingPlaces [Random.Range (0, ShiftingPlaces.Length)], transform.position.y, transform.position.z);
		
		} else {
			// return new Vector3 (-5, transform.position.y, transform.position.z);
			return new Vector3 (ShiftingPlaces [Random.Range (0, ShiftingPlaces.Length)], transform.position.y, transform.position.z);
		}


	
	}

	void animationcontrol(Vector3 a,Vector3 b){
		if (Mathf.Abs (a.x) < Mathf.Abs (b.x)) {
			// run right animation 
//			if (b.x == 0) {
//				Dynamicanimation.Play("Idle");
//
//			} else {
				Dynamicanimation.Play ("Global right small");
	//		}
		} 



		else if (Mathf.Abs (a.x) > Mathf.Abs (b.x)) {
		//	if (b.x == -10) {
			//	Dynamicanimation.Play("Idle");
		//	} else {
				Dynamicanimation.Play ("Global left small");
		//	}
			// run left animation 
		} else  {
			Dynamicanimation.Play("Idle");

		}
			
	}

	void OnTriggerEnter(Collider cld){
		if((cld.tag == "Obstacle" )){
			
			 Vector3 move = new Vector3 (transform.position.x,transform.position.y,transform.position.z + Random.Range(200,300));
			// transform.position = move;
			if (Mathf.Abs (player.transform.position.z - transform.position.z) > 150) {
				StartCoroutine (Player.SmoothM (gameObject, transform.position, move, 0.3f));
				print ("postion changed");
			} else {
				speed = 0;
			}
		
		}

	}





}



