using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public  float referencetime = 0;
	public GameObject[] DynamicObst;
	public GameObject[] StaticObst;
	public float Spawnrate;
	private float Lag;
	private bool r = true;
	public Vector3[] spawnplace;
	public Transform dynamic;
	//Player player;

	public GameObject[] MPassive;

	float Highscore;
	//float presentscore;
	float timeinterval;
	float lagsecond = 1;


	void Start (){
		
		Highscore = PlayerPrefs.GetFloat ("Highscore");

		//player = GameObject.Find ("Player").GetComponent<Player>();
		StartingSpawns ();
		StartCoroutine (SpawnObj  ());
		StartCoroutine (SpawnSecondary ());
		StartCoroutine (ManholeSpawner());

		InvokeRepeating ("SpawnPassive", Random.Range(5,15), Random.Range(14,25));
		InvokeRepeating ("SkillSpawn",Random.Range(0,300),Random.Range(0,300));
		StartCoroutine(skillspawnrate());

	}


	IEnumerator skillspawnrate(){
		while (true) {
			
	
			yield return new WaitForSeconds (Random.Range(150,300));
			SkillSpawn ();

		}
	}

	void Update(){

		referencetime = Time.deltaTime + referencetime;
		//print (referencetime + "this sis ");

		if (referencetime > timeinterval) {
			referencetime = 0;
		}

	}
	public Texture[] casetex;
	public Texture[] treasuretex;

	void SpawnPassive(){
		
		GameObject Passive = Instantiate (MPassive[Random.Range(0,MPassive.Length)],spawnplace[Random.Range(0,spawnplace.Length)],dynamic.transform.rotation);
		Passive.transform.SetParent (dynamic);
	}

	IEnumerator SpawnObj( ){
		
		while(r){
			spawnplace[0].z = transform.position.z;
			spawnplace[1].z = transform.position.z;
			spawnplace[2].z = transform.position.z;
			int randomS = Random.Range (0, StaticObst.Length);
			int randomD = Random.Range (0, DynamicObst.Length);
			GameObject PresentSpawning = ObjSpawn (referencetime,randomS, randomD);

			GameObject D = (GameObject) Instantiate (PresentSpawning,spawnplace[Random.Range(0,spawnplace.Length)],PresentSpawning.transform.rotation);
			D.transform.SetParent (dynamic);
			Lag = timeinterval / SpawnRateShift(referencetime);

			print ("tjis is lag" + Lag + "fafsa " + SpawnRateShift(referencetime));
			yield return new WaitForSeconds (Lag + lagsecondfunc() + Random.Range(0,1) + Random.Range(0.5f,1) + Random.Range(0,0.5f));
		}
}


	float lagsecondfunc(){

		if (Highscore > 1000) {

			return lagsecond - Random.Range (0.5f, 1);	
		}
		else{
			return lagsecond - Random.Range (1 -(Highscore/1000),1);
		}


	}





	GameObject ObjSpawn (float referencetime1, int randomS, int randomD){

		if (referencetime1 < TimeintervalSets(1)) {
			
			int diff = Random.Range (0, 4);
			if (diff == 4) {
				return DynamicObst [randomD];
			} else {
				return StaticObst [randomS];
			}
		} 
		if (referencetime1 < TimeintervalSets(2) && referencetime1 > TimeintervalSets(1)) {

			int diff = Random.Range (0, 5);
			if (diff == 4 || diff == 3 ) {
				return DynamicObst [randomD];
			} else {
				return StaticObst [randomS];
			}
		}
		if (referencetime1 < TimeintervalSets(3) && referencetime1 > TimeintervalSets(2)) {

			int diff = Random.Range (0, 4);
			if (diff == 4 || diff == 3 || diff == 2) {
				return DynamicObst [randomD];
			} else {
				return StaticObst [randomS];
			}
		}

		if (referencetime1 < TimeintervalSets(4) && referencetime1 > TimeintervalSets(3)) {

			int diff = Random.Range (0, 4);
			if (diff == 4 || diff == 3 ) {
				return DynamicObst [randomD];
			} else {
				return StaticObst [randomS];
			}
		} else {
			return StaticObst [randomS];



		
		}
	}



	float SpawnRateShift(float reference){
	//	float spawnfactor = 1;
		if (reference< TimeintervalSets(1)) {
			return Spawnrate = TimeintervalSets(1) + 5 ;
			}
		else if (reference > TimeintervalSets(1) && reference < TimeintervalSets(2)) {
			return Spawnrate = TimeintervalSets(2) + 10;
		}
		else if (reference< TimeintervalSets(3) && reference> TimeintervalSets(2)) {
			return Spawnrate = TimeintervalSets(3) + 12;
		} 
		else {
			return Spawnrate = TimeintervalSets (4) + Random.Range (0, 4f) + Random.Range (4,6);
		}
	}


	IEnumerator SpawnSecondary(){
		while (r) {
			spawnplace [0].z = transform.position.z;
			spawnplace [1].z = transform.position.z;
			spawnplace [2].z = transform.position.z; 
			GameObject a;
			if (referencetime % 2 == 1) {
				a = StaticObst [Random.Range (0, StaticObst.Length)];
				GameObject D = (GameObject) Instantiate (a, spawnplace [Random.Range (0, spawnplace.Length)], a.transform.rotation);
				D.transform.SetParent (dynamic);			
			} else {
				a = DynamicObst [Random.Range (0, DynamicObst.Length)];
				GameObject D = (GameObject) Instantiate (a, spawnplace [Random.Range (0, spawnplace.Length)], a.transform.rotation);
				D.transform.SetParent (dynamic);
			}
	
			yield return new WaitForSeconds (Lag + 1.5f);
		}


	}

	public GameObject manhole;
	IEnumerator ManholeSpawner(){
		bool r = true;
		while(r){
			 Instantiate (manhole,spawnplace[Random.Range(0,spawnplace.Length)],transform.rotation);
			int timer = Random.Range (20,50);

			yield return new WaitForSeconds (timer);

		}
		}

	float Randomtime(float initial,float final){

		if (referencetime < 10) {
			return Random.Range (initial, final);
		} else if (referencetime > 10 && referencetime < 20) {
			return Random.Range (initial, final) - 0.5f;
		} else if (referencetime > 20 && referencetime < 30) {
			return Random.Range (initial,final)-1.5f;
		} 

		else {
			return Random.Range (initial, final)-1f;
		}
	
		}

	void StartingSpawns(){
		if(Highscore < 500){Highscore = 500;}

		//presentscore = player.ScorePoints;
		int timeintervalF;
		timeintervalF =   (int)(Highscore / 240)  + 1 ;
		print (timeintervalF + "is the timeinterval factor"  + "The highscore is "+Highscore ) ;
		timeinterval = Highscore / (10 * timeintervalF);

}

	float TimeintervalSets(int _part){

		float abc;
		if(_part == 1){
			 abc= timeinterval / 4;
			print ("the diff is "+ _part + "of total " +abc );
		}
		else if(_part == 2){
			 abc= timeinterval / 2;
			print ("the diff is "+ _part + "of total " +abc );
		}
		else if(_part == 3){
		  abc= timeinterval * (3/4);
			print ("the diff is "+ _part + "of total " +abc );
		}
		else {
			 abc=  timeinterval;
			print ("the diff is "+ _part + "of total " +abc );
		}

		return abc;
			


	}

	IEnumerator HealthPillSpawn(){
		while (true) {

			// spawn the pill and increse the bar by percentage from the stats

			yield return new WaitForSeconds (1); 		
		}


	}

	public GameObject skill;
	void SkillSpawn(){
		GameObject s= Instantiate (skill,spawnplace[Random.Range(0,spawnplace.Length)],dynamic.transform.rotation) as GameObject;
		s.tag = "Skill";
	}



}
