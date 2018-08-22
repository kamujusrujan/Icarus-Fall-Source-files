using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviSpawner : MonoBehaviour {

	public Player P;
	public GameObject[] tree;
	public GameObject[] houses;
	public GameObject[] detail;
	public GameObject border;
	public Vector3 relative;
	public GameObject pole;
	public GameObject groundplane;
	public GameObject movingsense;
	public int lagtime ;
	public Transform staticO ;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		movingsense.transform.Translate (Vector3.forward * Player.speedcontrol * Time.deltaTime);
	}

	void OnTriggerEnter(Collider cld){
		int range = Random.Range (0, houses.Length);
	
		if (cld.tag == "Left") {
			range = Random.Range (0, houses.Length);
			GameObject LH =  Instantiate (houses [range], cld.transform.position + relative,cld.GetComponent<Transform>().rotation) as GameObject;
			LH.tag="Left";
			LH.transform.SetParent (staticO);


		//	StartCoroutine (DestroyLag (LH,lagtime));
		}
		if (cld.tag == "Right") {
			range = Random.Range (0, houses.Length);
			GameObject RH = Instantiate (houses [range],cld.transform.position + relative,cld.GetComponent<Transform>().rotation) as GameObject;
			RH.tag="Right";
			RH.transform.SetParent (staticO);
		//	StartCoroutine (DestroyLag (RH,lagtime));

		}
		if (cld.tag == "Detail") {
			range = Random.Range (0, detail.Length);
			GameObject D = Instantiate (detail [range],cld.GetComponent<Transform>().position + relative,cld.GetComponent<Transform>().rotation) as GameObject;
			D.tag = "Detail";
			D.transform.SetParent (staticO);
			//	StartCoroutine (DestroyLag (D,lagtime));

		}

		if (cld.tag == "Border") {
			GameObject B = Instantiate (border,cld.transform.position + relative,cld.GetComponent<Transform>().rotation) as GameObject;
			B.tag="Border";
			B.transform.SetParent (staticO);
		//	StartCoroutine (DestroyLag (B,lagtime));

		}
		if (cld.tag == "Pole") {
			GameObject P1 =  Instantiate (pole,cld.transform.position + relative,cld.GetComponent<Transform>().rotation) as GameObject;
			P1.tag = "Pole";
			P1.transform.SetParent (staticO);
			//	StartCoroutine (DestroyLag (P1,lagtime));

		}
		if (cld.tag == "Tree") {
			range = Random.Range (0, tree.Length);
			GameObject T =  Instantiate (tree [range],cld.transform.position + relative,cld.GetComponent<Transform>().rotation) as GameObject;
			T.tag = "Tree";
			T.transform.SetParent (staticO);
			//	StartCoroutine (DestroyLag (T,lagtime));

		}
		if (cld.tag == "Ground") {
			GameObject G = Instantiate (groundplane,cld.transform.position + new Vector3(0,0,277),cld.GetComponent<Transform>().rotation);
			G.tag = "Ground";
		//	StartCoroutine (DestroyLag (G,lagtime));
			G.transform.SetParent (staticO);
		}
	}

	IEnumerator DestroyLag(GameObject IsDestroying,float _lagtime){
	 

			yield return new WaitForSeconds (_lagtime);
		DestroyImmediate (IsDestroying);

	}











}
