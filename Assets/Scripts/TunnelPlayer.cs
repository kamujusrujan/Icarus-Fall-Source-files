using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelPlayer : MonoBehaviour {
	public GameObject NormalSewer;
	public float maxlength;
	public LayerMask lmask;
	public GameObject maincamera;
	public Animator CameraAnim;
	private Player p;


	private	 bool isSwipe = false;
	private	float minSwipeDist  = 50.0f;
	private	float maxSwipeTime = 0.5f;
	// private	Vector3 swipedistance = new Vector3 (-5,0,0);
	private float fingerStartTime  = 0.0f;
	private	Vector2 fingerStartPos = Vector2.zero;

	public Transform starting;
	public LayerMask mask;

	public static bool getgnd  = false;


	public  GameObject AbilityController;
	public  GameObject tunnel_prefernce;


	void gndtrue(){
		getgnd = true;
	}


	void casting(){
		

		Vector3 direction = new Vector3 (transform.position.x,transform.position.y,starting.position.z) - starting.position;
		Ray ray = new Ray (starting.position,direction);
		RaycastHit info;

		float max = Mathf.Abs (transform.position.z - starting.position.z) + 20;
		if (Physics.Raycast (ray, out info,max,mask)) {
		//	print (info.collider.name);
			Debug.DrawRay (ray.origin, info.point - ray.origin, Color.green);
		} else {
		//print ("HIT SHIT");
			p.CameraAnim.SetBool("Tunnel",false);
			p.Dead ();
			AbilityController.SetActive (true);
			//tunnel_prefernce.SetActive (false);

			CancelInvoke ();
			Debug.DrawRay (ray.origin,direction,Color.white);
			}
	}



	void OnEnable(){
		GetComponent<Player> ().UImanagment.Pie_Menu.SetActive (false);
		Time.timeScale = 1;
		Invoke("Blink",0.5f);
		AbilityController.SetActive (false);

		CameraAnim.SetBool ("Tunnel",true);
		InvokeRepeating ("casting", 1.5f, 0.5f);
		//	GameObject.Find ("bik").SetActive (false);
		gameObject.GetComponent<Player> ().Bike.SetActive (false);
		//	Setup ();
		p = GameObject.Find ("Player").GetComponent<Player> ();
		tunnel_prefernce.SetActive (true);
		Invoke ("gndtrue",Random.Range(10,20));
		}

	void Start(){
		
	}


	void Update(){

		// float scorefactor = 1;
		TunnelTouch ();
		Bounds (transform.position);
		p.ScorePoints = Time.deltaTime *    Player.speedcontrol   + p.ScorePoints * 0.75f;
	//	p.scoret.text =  p.ScorePoints.ToString("0")   ;
	}



	void Bounds(Vector3 p ){

		if (p.x >= 0) {
			transform.position = new Vector3 (0,transform.position.y,transform.position.z);

		}
		if (p.x <= -10) {
			transform.position = new Vector3 (-10,transform.position.y,transform.position.z);
		}
	}

	void GetGround(){
		CameraAnim.SetBool ("Tunnel",false);
		CameraAnim.Play ("c_idle");
	//	Vector3 relative = new Vector3 (0,-12.5f,0);
		Vector3 up = new Vector3 (transform.position.x,0.4f,transform.position.z);
		GetComponent<TunnelPlayer> ().enabled = false;
		GetComponent<Player> ().enabled = true;
	//	Player.Moveto (gameObject,transform.position ,  transform.position - relative , 1.5f);
		// 	transform.position = transform.position - relative;
		CancelInvoke();
		StartCoroutine (Player.SmoothM (gameObject,transform.position,up,1f));
		AbilityController.SetActive (true);
		tunnel_prefernce.SetActive (false);
	}

	public GameObject[] SewerPosition;

	GameObject StateSpawn(Vector3 _postion){
	//	GameObject returngame;
		if (_postion.x == -5) {
			//middle
			return	 SewerPosition[1];
		}
		if (_postion.x == 0) {
			//right
			return  SewerPosition[0];
		}
		if (_postion.x == -10) {
			//left
			return SewerPosition [2];
		} else
			return null;

	}

	public  void EndTunnel(){
		AbilityController.SetActive (true);
		tunnel_prefernce.SetActive (false);
	}

	void Blink(){
		tunnel_prefernce.SetActive (true);

	}

	void RightSwing(){
		transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3 (5f,0,0),1) ;
		CameraAnim.Play ("c_right");
		tunnel_prefernce.SetActive(false);
		Invoke ("Blink", 0.3f);

	
	//	StartCoroutine (Player.SmoothM (gameObject,transform.position,transform.position + new Vector3 (5f,0,0),0.5f));
	}
	void LeftSwing(){
		transform.position = Vector3.Lerp(transform.position, transform.position - new Vector3 (5f,0,0),1) ;
		CameraAnim.Play ("c_left");
		tunnel_prefernce.SetActive(false);
		Invoke ("Blink", 0.3f);
		// StartCoroutine (Player.SmoothM (gameObject,transform.position,transform.position - new Vector3 (5f,0,0),0.5f));
	}

	public void Setup(){
		getgnd = false;
		GetComponent<Player> ().enabled = false;
		GetComponent<TunnelPlayer> ().enabled = true;
		CameraAnim.Play ("c_s_idle");
		Vector3 relative = new Vector3 (-5-transform.position.x,-12.5f,0) ;
		Player.speedcontrol = Player.speedcontrol - 10;


		GameObject Sewer = (GameObject) Instantiate (StateSpawn(transform.position),transform.position + relative , transform.rotation);
		Sewer.tag ="Sewer";

		transform.position =  transform.position + new Vector3(0,-12.5f,0)  + new Vector3(0,0.5f,0) ;
		// StartCoroutine (Player.SmoothM (gameObject,transform.position,transform.position + relative  + new Vector3(0,1,0),1f));

}
			void LateUpdate(){
		transform.Translate (Vector3.forward * Player.speedcontrol * Time.deltaTime * 0.7f);
	}

	void OnTriggerEnter(Collider cld){
	/*
		if (cld.tag == "TunnelObst") {
			p.Dead ();
	

		}
*/
		if (cld.tag == "ManHole") {
			
	//		Setup ();
		

		}

			if (cld.tag == "Sewer") {
				//	print ("NIggah");
				GameObject tunnel = (GameObject)Instantiate (NormalSewer, cld.transform.position + new Vector3 (0, 0, 236), transform.rotation);
				tunnel.tag = "Sewer";
			}

			if (cld.tag == "HighGround") {
				GetGround ();
			}



	}












	void TunnelTouch(){
		


		if (Input.touchCount > 0) {

			foreach (Touch touch in Input.touches) {
				switch (touch.phase) {
				case TouchPhase.Began:
					/* this is a new touch */
					isSwipe = true;
					fingerStartTime = Time.time;
					fingerStartPos = touch.position;
					break;

				case TouchPhase.Canceled:
					/* The touch is being canceled */
					isSwipe = false;
					break;

				case TouchPhase.Ended:

					float gestureTime = Time.time - fingerStartTime;
					float gestureDist = (touch.position - fingerStartPos).magnitude;

					if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist) {
						Vector2 direction = touch.position - fingerStartPos;
						Vector2 swipeType = Vector2.zero;

						if (Mathf.Abs (direction.x) > Mathf.Abs (direction.y)) {
							// the swipe is horizontal:
							swipeType = Vector2.right * Mathf.Sign (direction.x);
						} else {
							// the swipe is vertical:
							swipeType = Vector2.up * Mathf.Sign (direction.y);
						}

						if (swipeType.x != 0.0f) {
							if (swipeType.x > 0.0f) {
								// MOVE RIGHT

								RightSwing ();
							
							} else {
								// MOVE LEFT
								LeftSwing ();
							
							}
						}

						if (swipeType.y != 0.0f) {
							if (swipeType.y > 0.0f) {
								// MOVE UP
								//	Jump ();
							} else {
								// MOVE DOWN
								//	Slide();
							}
						}

					}

					break;
				}
			}
		}
	}
		

		IEnumerator Delay(float interval){

		yield return new WaitForSeconds(interval);

		}


	}














