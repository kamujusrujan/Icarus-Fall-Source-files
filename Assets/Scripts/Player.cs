using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;




public class Player : MonoBehaviour {
    [Header("SpeedControl")]
    public static float speedcontrol = 50; // default 50
	public float referncetime ; 
	public float stamina;


	 


	private float fingerStartTime  = 0.0f;
	private Vector2 fingerStartPos = Vector2.zero;

	private bool isSwipe = false;
	private float minSwipeDist  = 70;
	private float maxSwipeTime = 0.4f;
	private Vector3 swipedistance = new Vector3 (-5,0,0);




//	[Header("UI elements and Score")]
	public  float ScorePoints;
//	public Text ScoreEnd;


	public  float Money;

	public UI_managment UImanagment;


//	public Text scoret;
//	public Scrollbar staminabar;


	


	[Header("Animators")]
	public Animator CameraAnim;
	public Animator watchanimation;


	private Collider Cld; // collider of this object


	static float reset = 5;


	//public bool IsSlowMo = false;


	public  Player_data data;


	public GameObject tank;

	[SerializeField]


	static public int hitpoints ;
	public GameObject Hurt;
	static public float InhaleCapacity = 1;
	public GameObject Bike;
	//float[] ShiftingPlaces = {-5 ,0 ,-10};

	public int weather; 

	string path;

	public Image fill;
	

	void Awake(){
		path = Path.Combine (Application.persistentDataPath,"Player_Data.json");

		if(!File.Exists(path)){
			
		//	int waitman = 50 ;
			File.CreateText (path).Dispose();
		
		//File.CreateText (path);
		//	File.Create (path);
			data.Power.SnailTime = 0.6f;
			data.Power.PowerForward = 0.75f;
			data.Power.Bike_Time = 10;
			data.Power.SnailRate = 20f;
			data.Power.PowerRate = 15f;
			data.Profile.Money = 200;
			data.Profile.Skill_Point = 3;
			data.Profile.Money = 1500;
			data.Power.healDuration = 10;
			data.Power.TankTime = 10;
			data.Store.Tank = 2;
			data.Profile.InhaleRate = 20;
			data.Profile.Instructions = true;

			SaveData ();
	}


		weather = Random.Range (0,2);
		print ("Weather is " + weather);


		string data1 = File.ReadAllText (path);

		data = JsonUtility.FromJson <Player_data>(data1);
	//	print (data.Time_factor.Food_Time);

	}



	public void Gamestart(){
		hitpoints = 1;
		speedcontrol = 50;
		referncetime = 0;


		//	GOUI.SetActive (false);
		Cld = GetComponent<Collider> ();
		InvokeRepeating ("SpeedShift",0,10);
		//	StartCoroutine (SpeedShift ());
		// Stamina ();
		GetComponent<TunnelPlayer> ().EndTunnel ();

		data.Store.Inhaler = data.Store.Inhaler + 2;
		StartCoroutine (HeadsUpDisplay ("2 inhaler given"));
		InvokeRepeating ("ScoreDisplay", 5, 15);


	}

	// Use this for initialization
	void Start () {

        
		Gamestart ();
        
	}
	public bool IsDead = false;
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward *speedcontrol * Time.deltaTime);
	//	InhaleCapacity = InhaleCapacity/100 - Time.deltaTime / 15;
		referncetime = referncetime + Time.deltaTime ;
		// transform.Translate (Vector3.forward *speedcontrol * Time.deltaTime);
		ScoreCount ();
		touch2();
		black ();
		DoubleTap ();
		fill.fillAmount = fill.fillAmount - (0.009f *Time.deltaTime*  (    (200 - data.Profile.InhaleRate)/(100*0.75f)  ));
		if (fill.fillAmount == 0 && !IsDead ) {
			Dead ();
		}

	

}
	void LateUpdate(){

	}




	void SpeedShift(){



		/*
		 * 
		 * 

			yield return new WaitForSeconds (20);
			speedcontrol = 50 + 30;
			yield return new WaitForSeconds (7);
			speedcontrol = speedcontrol  - 10;
			yield return new WaitForSeconds (1);
			speedcontrol = speedcontrol  - 10;
			yield return new WaitForSeconds (1);
	
			speedcontrol = speedcontrol + 5 - 10;
*/
	//print ("invoked");
		int variablespeed = Random.Range (0, 6);
		if (variablespeed == 4) {
			speedcontrol = speedcontrol + 5;
	//		print (variablespeed);
		}else if(variablespeed == 3) {
			
	//		print (variablespeed);
		}else if(variablespeed == 2) {
			speedcontrol = speedcontrol +3;
	//		print (variablespeed);
		}else if(variablespeed == 1) {
			speedcontrol = speedcontrol +5;
	//		print (variablespeed);
		}
		else if(variablespeed == 5) {
			speedcontrol = speedcontrol -5;
	//		print (variablespeed);
		}else if(variablespeed == 0) {
			speedcontrol = speedcontrol -5;
	//		print (variablespeed);
		}


		if(speedcontrol < 45){
			speedcontrol = 45;
		}

		}


	





	void EndHurt(){
		Hurt.SetActive (false);
		hitpoints = 1;

	}

	public GameObject Headup;
	public Text headT;

	public 	IEnumerator HeadsUpDisplay(string s){
		Headup.SetActive (true);
		Headup.GetComponentInChildren<Text> ().text = s;
		yield return new WaitForSeconds (2);
		Headup.SetActive (false);

	}

	public Image fillHeal;

	IEnumerator FILL(float end){
		float duration = 0;
		while( duration <= end){
			
			fillHeal.fillAmount =  Mathf.Lerp (0, 1, duration / end);
			duration += Time.deltaTime;
			yield return null;
		}
	}
	public GameObject blackoutImage;
	void  black(){

		if (fill.fillAmount < 0.25) {
			blackoutImage.SetActive (true);
		} else {
			blackoutImage.SetActive (false);
		}
	
	}

	void OnTriggerEnter (Collider Obj){
		if (Obj.tag == "Obstacle") {
//			Dead ();	
				if (hitpoints == 0) {
					Dead ();
				Hurt.SetActive (false);

				} else {
                data.Achievements.bumps++;
                Googler.instance.CheckBump();
				Hurt.SetActive (true);
				fillHeal.fillAmount = 0;

				hitpoints = 0;
				StartCoroutine (FILL(data.Power.healDuration));
				Invoke ("EndHurt",data.Power.healDuration);
					
				if (transform.position.x == -10) {
					ShiftRight ();
				} else {
					ShiftLeft();
				}

				}
		}	

		if (Obj.tag == "Money") {
			Money += 50;
			StartCoroutine (HeadsUpDisplay("Money 50 is found"));
		}
		if (Obj.tag == "Inhale") {
			fill.fillAmount += 0.20f;
			StartCoroutine (HeadsUpDisplay("Inhaler rate 20%  is increased"));

		}
		if(Obj.tag == "Bike" ){
			BikeEnabled ();

		}
		if(Obj.tag == "Skill"){
			data.Profile.Skill_Point++;
			StartCoroutine (HeadsUpDisplay("Skillpoint has found"));
            Googler.instance.CheckSkill();
			SaveData();
		}if(Obj.tag == "Unknown"){
			int a = Random.Range (0, 2);
			if (a == 0) {
				int m1 = Random.Range (0, 201);
				Money += m1;
                if (m1 == 200) {
                    Googler.Achieved(GPGSIds.achievement_the_lucky_duck);
                }
				StartCoroutine (HeadsUpDisplay ("Money " + m1 + "  is found"));
			} else {
				float ab = Random.Range(0,0.3f);

				fill.fillAmount += ab;
				string asw = (ab*100).ToString("0") ;
				StartCoroutine (HeadsUpDisplay("Inhaler rate " + asw + "%  is increased"));
			}

		}

	}
	int bikecount = 0;
	public void BikeEnabled(){

		if (Bike.activeSelf) {
			speedcontrol += 10;
			bikecount++;
		} else {



			CameraAnim.Play ("c_start2Run");
			CameraAnim.SetBool ("Bike", true);

		 
	
			speedcontrol += 30;

			Bike.SetActive (true);
			Invoke ("BikeDisable", data.Power.Bike_Time);
			//	hitpoints = 1;

		}

	}
	public void BikeDisable(){
		CameraAnim.SetBool ("Bike",false);
		Bike.SetActive (false);
	//	hitpoints = 0;
		speedcontrol -= 25 + (bikecount* 10)  ;
		bikecount = 0;
	}

	public float prevspeed;
	public	float finalScore;
	public float r_time;
public void Dead(){
		 r_time = referncetime;

		CancelInvoke ("ScoreDisplay");

		GetComponent<BoxCollider> ().enabled = false;

		IsDead = true;
		prevspeed = speedcontrol;
		Bike.SetActive (false);
		CancelInvoke ("BikeDisable");
		CancelInvoke ("SpeedShift");
		CameraAnim.SetBool ("Bike",false);
		CameraAnim.SetBool ("Tunnel",false);
	//	GOUI.SetActive (true);
		PlayerPrefs.SetFloat("Highscore",ScorePoints);
	//	print (PlayerPrefs.GetFloat("HighScore")  + "is the present score");
		 finalScore = ScorePoints;
	//	ScoreEnd.gameObject.SetActive (true);
	//	ScoreEnd.text = " NEWB SCORE " + finalScore.ToString("0");
		speedcontrol = 0f;
		CameraAnim.Play ("c_dead");



	//	 GetComponent<Player> ().enabled = false;
		GetComponent<TunnelPlayer> ().enabled = false;
		GameObject stufftodisable = GameObject.Find ("Spawn Manager");
		stufftodisable.GetComponentInChildren<Spawner> ().enabled = false;
		UImanagment.Dead ();
	//	stufftodisable.GetComponentInChildren<EnviSpawner> ().lagtime = 50;
	//	scoret.enabled = false;

		data.Profile.Gain = ScorePoints + data.Profile.Gain ;
		if (ScorePoints/700 > data.Profile.TopRun) {
            StartCoroutine(HeadsUpDisplay("New HighScore" + (ScorePoints / 700).ToString("0.0")));
			data.Profile.TopRun = ScorePoints / 700;
            Googler.instance.addleader();
}
		data.Profile.Money = Money + data.Profile.Money;
		Money = 0;
		SaveData ();
        Googler.instance.CheckMoney();
        Googler.instance.CheckScore();
        Googler.instance.Checkgain();
        

}
	
	void ScoreDisplay(){
		StartCoroutine (HeadsUpDisplay ("The Distance is " + (ScorePoints/700).ToString("0.0") +"kms" ));
	}

	void ScoreCount(){
		ScorePoints = referncetime * 5f  ;
//		scoret.text =  ScorePoints.ToString("0")   ;
		/*Vector3 startpoint = new Vector3 (-5f,1.1f,-203f);	 
		ScorePoints = (transform.position - startpoint).magnitude * 0.2f ;



		if (staminabar.size == 0) {
			Dead ();
		}
		*/
}


	public void ShiftRight(){
		NearMiss (transform.position);
		transform.position = Vector3.Lerp (transform.position,-swipedistance + transform.position,1);
		//StartCoroutine (Player.SmoothM (gameObject,transform.position, -swipedistance + transform.position  + new Vector3(0,1,0),0.7f));
		CameraAnim.Play ("c_right");
		Bounds (transform.position);
	/*	bool reached = false;
		while(!reached){
			Vector3 desiredposition =-swipedistance + transform.position ;
			transform.position = Vector3.Lerp (transform.position,desiredposition , Time.deltaTime * 10);
			if(transform.position ==  desiredposition ){

				reached = true;
				break;
			}
	}
		reached = false;
*/

}
	public  void ShiftLeft(){
		NearMiss (transform.position);
		CameraAnim.Play ("c_left");
		transform.position = Vector3.Lerp (transform.position,  swipedistance + transform.position, 1);
	//	StartCoroutine (Player.SmoothM (gameObject,transform.position, swipedistance + transform.position  + new Vector3(0,1,0),0.7f));
		Bounds (transform.position);
		/*bool reached = false;
	  while (!reached) {
			Vector3 desiredposition = swipedistance + transform.position;
			transform.position = Vector3.Lerp (transform.position, desiredposition, 0.2f);
			if (transform.position == desiredposition) {
				reached = true;
				break;
			}
					
		}
		reached = false;
		*/

}




	void Jump(){
		        
		    NearMiss (transform.position);
			CameraAnim.Play ("c_Jump");
			StartCoroutine (ColliderDisable(0.45f));
            data.Achievements.jumps++;
             Googler.instance.CheckJump();


	}

	void Slide(){
		
			CameraAnim.Play ("c_Slide");
			 StartCoroutine (ColliderDisable(0.45f));

		}


	IEnumerator ColliderDisable(float DisableLag){
		
	
			Cld.enabled = false;
			yield return new WaitForSeconds (DisableLag);
			Cld.enabled = true;
}


	 	void touch2(){
		
		if (Input.touchCount > 0){

			foreach (Touch touch in Input.touches)
			{
				switch (touch.phase)
				{
				case TouchPhase.Began :
					/* this is a new touch */
					isSwipe = true;
					fingerStartTime = Time.time;
					fingerStartPos = touch.position;
					break;

				case TouchPhase.Canceled :
					/* The touch is being canceled */
					isSwipe = false;
					break;

				case TouchPhase.Ended :

					float gestureTime = Time.time - fingerStartTime;
					float gestureDist = (touch.position - fingerStartPos).magnitude;

					if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist){
						Vector2 direction = touch.position - fingerStartPos;
						Vector2 swipeType = Vector2.zero;

						if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
							// the swipe is horizontal:
							swipeType = Vector2.right * Mathf.Sign(direction.x);
						}else{
							// the swipe is vertical:
							swipeType = Vector2.up * Mathf.Sign(direction.y);
						}

						if(swipeType.x != 0.0f){
							if(swipeType.x > 0.0f){
								// MOVE RIGHT

							
								ShiftRight ();
							}else{
								// MOVE LEFT
							
								ShiftLeft ();
							}
						}

						if(swipeType.y != 0.0f ){
							if(swipeType.y > 0.0f){
								// MOVE UP
								Jump ();
							}else{
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

	public void StartWatch(){
		CameraAnim.Play ("c_start");
		watchanimation.Play ("watch");
	
	}

	public void rebirth(){
		CameraAnim.Play ("c_idle");
		GetComponent<TunnelPlayer> ().EndTunnel ();

		
		GameObject[] s = GameObject.FindGameObjectsWithTag ("Obstacle");
		foreach(GameObject g1 in s){
			Destroy (g1);
		}

		referncetime = r_time;
		GetComponent<BoxCollider> ().enabled = true;
		GameObject[] ob = GameObject.FindGameObjectsWithTag ("Obstacle");
		foreach(GameObject a in ob){
			Destroy (a);
		}

	//	StopCoroutine ("FILL"); 
		fillHeal.fillAmount = 1;
		fill.fillAmount = 1;
		hitpoints = 1;




		//	scoret.enabled = true;
		GetComponent<Player> ().enabled = true;
		CancelInvoke ();
		reset = reset + 1.5f;
		speedcontrol = prevspeed ;
		CameraAnim.SetBool ("Bike",false);
		CameraAnim.SetBool ("Tunnel",false);
		IsDead = false;

		GameObject stufftodisable = GameObject.Find ("Spawn Manager");
		stufftodisable.GetComponentInChildren<Spawner> ().enabled = true;
		// stufftodisable.GetComponentInChildren<Spawner> ().referencetime = 0;
		GetComponent<Player> ().CameraAnim.Play ("c_start2Run");
		//		staminabar.size = 1;
		InvokeRepeating ("SpeedShift", 0,reset  );
		InvokeRepeating ("ScoreDisplay", 5, 15);


	}

	public void Reset(){
		CameraAnim.Play ("c_idle");
		GetComponent<TunnelPlayer> ().EndTunnel ();

	
		GetComponent<BoxCollider> ().enabled = true;
		IsDead = false;
		StopCoroutine ("FILL"); 
		fillHeal.fillAmount = 1;
		hitpoints = 1;

	//	scoret.enabled = true;
		GetComponent<Player> ().enabled = true;
		CancelInvoke ();
		reset = reset + 1.5f;
	//	print (reset);
		referncetime = 0;
		ScorePoints = 0;
		speedcontrol = 50;
		Money = 0;



		GameObject stufftodisable = GameObject.Find ("Spawn Manager");
		stufftodisable.GetComponentInChildren<Spawner> ().enabled = true;
		stufftodisable.GetComponentInChildren<Spawner> ().referencetime = 0;
		GetComponent<Player> ().CameraAnim.Play ("c_start2Run");
//		staminabar.size = 1;
		InvokeRepeating ("ScoreDisplay", 5, 15);
		InvokeRepeating ("SpeedShift", 0,reset );
	}

	void Bounds(Vector3 p ){
		
		if (p.x >= 0) {
			transform.position = new Vector3 (0,transform.position.y,transform.position.z);
		
		}
		if (p.x <= -10) {
			transform.position = new Vector3 (-10,transform.position.y,transform.position.z);
		}
	}

	float touchDuration;
	Touch touch;
//	public UI_managment uimain;

	void DoubleTap(){
		if(Input.touchCount > 0){ //if there is any touch
			touchDuration += Time.deltaTime;
			touch = Input.GetTouch(0);

			if(touch.phase == TouchPhase.Ended && touchDuration < 0.2f) //making sure it only check the touch once && it was a short touch/tap and not a dragging.

				StartCoroutine(UImanagment.singleOrDouble(touch));
		}
		else
			touchDuration = 0.0f;
	}


	public static IEnumerator SmoothM( GameObject body , Vector3 _initial,Vector3 _final , float duration){
		
		float strt = 0;
		while (strt <= duration) {
		
			body.transform.position = Vector3.Lerp (_initial,_final,strt/duration);
			strt += Time.deltaTime;
			yield return null;

		}

	}



	public void SaveData(){


		string savingdata = JsonUtility.ToJson (data,true);
		print (savingdata);
		print (path);

		File.WriteAllText (path,savingdata);

		print ("data svonmg");

	}


	public void OverRideData(){
//		data.Name = "Shashsank";
		SaveData ();
		string data1 = File.ReadAllText (path);
		JsonUtility.FromJsonOverwrite (data1,data);
		print (data);
	}

	 public void endtankanim (GameObject g)
	{
//		if (Bike.activeSelf) {
//			BikeDisable ();
//		}

		g.GetComponent<Tank> ().h = 0;
		SmoothM (gameObject,transform.position,g.transform.position,0.2f);
		//StartCoroutine (SmoothM (gameObject, transform.position, transform.position + new Vector3 (0, 0, 65), 0.3f));
	//	CameraAnim.Play ("c_tank_end");

	}

	void DestroyTank(){
		Destroy (spawntank);
	
	}

	GameObject spawntank;
	public GameObject body;
	public void Tank(){
		if (transform.position.x == -5) {
			ShiftLeft ();		}

		data.Store.Tank--;
		SaveData ();
		body.SetActive (false);

		spawntank = Instantiate (tank,new Vector3 (-5,transform.position.y,transform.position.z + 5 ),transform.rotation) as GameObject;
		Vector3 x  = new Vector3 (spawntank.transform.position.x,spawntank.transform.position.y,spawntank.transform.position.z + speedcontrol *1.5f  );
		StartCoroutine(SmoothM(spawntank,spawntank.transform.position,x,0.5f ));
		CameraAnim.Play ("c_tank");
		// StartCoroutine(SmoothM(gameObject,transform.position,x,1 ));
		GameObject[] obst = GameObject.FindGameObjectsWithTag ("Obstacle");
		for( int i = 0;i < obst.Length ; i++ ){
			if (obst [i].transform.position.z < transform.position.z) {
				Destroy (obst[i]);
			}

	}
		body.SetActive (true);
	//	Invoke ("endtankanim",data.Power.TankTime - 1.5f);
	//	Invoke ("DestroyTank",data.Power.TankTime);
	}

	public LayerMask mask; 
	public void NearMiss(Vector3 _p1 ){
		Vector3 _p = _p1 + new Vector3 (0, 0, 5f);
		Ray forwardRay  = new Ray (_p, Vector3.forward);
		RaycastHit _data;
		float maxdistance = 7.5f;
			if (Physics.Raycast (forwardRay, out _data, maxdistance)) {
			if (_data.collider.gameObject.tag == "Obstacle") {
                data.Achievements.nearmiss++;
                Googler.instance.CheckMiss();
				print (_data.collider.gameObject.name + "is hit");

				int a = Random.Range (0,50);
				if (a == 13) {
					data.Profile.Skill_Point++;
					StartCoroutine (HeadsUpDisplay ("Close call bonus : Skillpoint is gained"));
				} else if (a == 5 || a == 16 ) {
					int m1 = Random.Range (1,3);
					data.Store.Adernaline_Shot = 	data.Store.Adernaline_Shot + m1;
					StartCoroutine (HeadsUpDisplay ("Close Call Bonus : " + m1 + " adernaline were found"));
				} else if (a == 3 || a == 8 || a == 17  ) {
					int m1 = Random.Range (0, 4);
					data.Store.Bike =	data.Store.Bike + m1;
					StartCoroutine (HeadsUpDisplay ("Close Call Bonus : " + m1 + " bikes were added"));
				} else if (a == 12 || a== 20) {
					int m1 = Random.Range (1, 3);
					data.Store.Tank =	data.Store.Tank + m1;
					StartCoroutine (HeadsUpDisplay ("Close Call Bonus :" + m1 + " tanks were added"));
				} else {
					int m1 = Random.Range (0, 100);
					Money += m1;
					StartCoroutine (HeadsUpDisplay ("Close Call Bonus : " + m1 + " money were found"));
				}
			
			}
		}	

	}











//	public void SlowMo(){
//
//		if (IsSlowMo) {
//			Time.timeScale = 1f;
//			IsSlowMo = false;
//		} else {
//			Time.timeScale = 0.1f;
//			IsSlowMo = true;
//		}
//
//	}
//
//	public void Speed(){
//
//		Ray jump = new Ray (transform.position , Vector3.forward);
//		float maxd = 25;
//		if (Physics.Raycast (jump,maxd) ){
//			maxd = maxd + 10;
//			transform.position = new Vector3 (0,0,maxd) + transform.position;
//		}else{
//
//			transform.position = new Vector3 (0,0,maxd) + transform.position;
//			maxd = 25;
//		}
//
//		 ispeed = maxd / 0.3f;
//		speedcontrol = speedcontrol +ispeed;
//		Invoke ("effect1",0.3f);
//		GetComponent<BoxCollider> ().enabled = false;
//	}

//	public float ispeed;
//
//	void effect1(){
//
//		speedcontrol = speedcontrol - ispeed;
//		GetComponent<BoxCollider> ().enabled = true;
//	}


	 



}



