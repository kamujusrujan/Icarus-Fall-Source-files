using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour {

	public GameObject loadUI;
	private Animator loadanim;
	public GameObject previousUI;

	public Shader sky;
	void Start(){

		loadanim = loadUI.GetComponent<Animator> ();
		loadUI.gameObject.SetActive (false);

	}

	public void Exit(){
		Application.Quit();

	}
	IEnumerator loadingscene(string level){
		previousUI.gameObject.SetActive (false);
		loadUI.gameObject.SetActive (true);
		loadanim.Play ("load");
		yield return null;
		yield return new WaitForSeconds (1.5f);
	//	loadUI.gameObject.SetActive (false);
	//	Application.LoadLevel  (level);
	//	Application.LoadLevelAsync(level);
	}

	public void TryAgain(){
		StartCoroutine( loadingscene ("Main_1"));

	

	}
	public void MainMenu(){
		StartCoroutine( loadingscene ("Main_menu"));
	
	}
				

}
