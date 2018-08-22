using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plus_Minus : MonoBehaviour {

	 public Player playercomponent;
	public Text[] Shop; 
	public Text[] profile;

	public Text[] Money;

	public GameObject[] Upgrades;
	public GameObject UpgradePanel;

	public Text[] UpgradeTexts;

	public Text skill;

	public Animator upgradeanim;
	public Color active,idle;
	public GameObject[] upgradebutton;
	void Start(){
		updateList ();
	}

	public void updateList(){

		foreach (Text i in Money) {
			i.text = playercomponent.data.Profile.Money.ToString();
		}

		Shop [0].text = playercomponent.data.Store.Tank.ToString("0.0");
		Shop [1].text = playercomponent.data.Store.Inhaler.ToString();
		Shop [2].text = playercomponent.data.Store.Adernaline_Shot.ToString();
		Shop [3].text = playercomponent.data.Store.Bike.ToString ();
		profile [0].text = playercomponent.data.Profile.Gain.ToString ("0.0");
		profile [1].text = playercomponent.data.Profile.Money.ToString ();
		profile [2].text = playercomponent.data.Profile.TopRun.ToString ("0.0");
		profile [3].text = playercomponent.data.Profile.InhaleRate.ToString ("0");
		playercomponent.SaveData ();
	}

	public void UpdateUpgrades(){
		skill.text = playercomponent.data.Profile.Skill_Point.ToString ();
		UpgradeTexts [0].text = playercomponent.data.Power.SnailRate.ToString();
		UpgradeTexts [1].text = playercomponent.data.Power.PowerRate.ToString();
		UpgradeTexts [2].text = playercomponent.data.Power.Bike_Time .ToString();
		UpgradeTexts [3].text = playercomponent.data.Profile.InhaleRate .ToString();
		UpgradeTexts [4].text = playercomponent.data.Power.TankTime .ToString();
		UpgradeTexts [5].text = playercomponent.data.Power.healDuration .ToString();


	}

	public void BikeP(){
		if (playercomponent.data.Profile.Money >= 350) {
			playercomponent.data.Store.Bike++;
			playercomponent.data.Profile.Money -= 350;
			updateList ();
			StartCoroutine( playercomponent.HeadsUpDisplay ("Bike purchased"));
            Googler.instance.CheckBike();
		} else {
			StartCoroutine( playercomponent.HeadsUpDisplay ("350 required for Bike Shot"));
		}
	}
	public void BikeM(){


		if (playercomponent.data.Store.Bike > 0) {
			playercomponent.data.Store.Bike--;
			playercomponent.data.Profile.Money += 100;
			updateList ();
			StartCoroutine( playercomponent.HeadsUpDisplay ("Bike selledless amount of 100"));
		}
	}
	public void FoodCapsP(){
		if (playercomponent.data.Profile.Money >= 750) {
			playercomponent.data.Store.Tank++;
			playercomponent.data.Profile.Money -= 750;
			updateList ();
            Googler.instance.CheckTank();
			StartCoroutine (playercomponent.HeadsUpDisplay ("tank purchased for 750"));
		} 
			else {
				StartCoroutine( playercomponent.HeadsUpDisplay ("750 required for Tank"));
			}

	}
	public void FoodCapsM(){

		if (playercomponent.data.Store.Tank > 0) {
			playercomponent.data.Store.Tank--;
			playercomponent.data.Profile.Money += 400;
			updateList ();
			StartCoroutine( playercomponent.HeadsUpDisplay ("tank selled for less amount of 400"));
		}
	}
	public void InhalerP(){
		if (playercomponent.data.Profile.Money >=50) {
			playercomponent.data.Store.Inhaler++;
			playercomponent.data.Profile.Money -= 50;
			updateList ();
			StartCoroutine( playercomponent.HeadsUpDisplay ("inhaler purchased for 50"));
		} else {
			StartCoroutine( playercomponent.HeadsUpDisplay ("50 required for Inhaler"));
		}
	}
	public void InhalerM(){

		if(	playercomponent.data.Store.Inhaler > 0){
		playercomponent.data.Store.Inhaler--;
		playercomponent.data.Profile.Money += 25;
		updateList ();
			StartCoroutine( playercomponent.HeadsUpDisplay ("inhaler selled for less amount of 25"));
		}
	}
	public void AdernalineP(){
		if (playercomponent.data.Profile.Money >= 950) {
			playercomponent.data.Store.Adernaline_Shot++;
			playercomponent.data.Profile.Money -= 950;
			updateList ();
            Googler.instance.CheckAdernaline();
			StartCoroutine (playercomponent.HeadsUpDisplay ("adernaline purchased for 950"));
		} else {
			StartCoroutine( playercomponent.HeadsUpDisplay ("950 required for Adernaline Shot"));
		}
	}
	public void AdernalineM(){

		if (playercomponent.data.Store.Adernaline_Shot > 0) {
			playercomponent.data.Store.Adernaline_Shot--;
			playercomponent.data.Profile.Money += 500;
			updateList ();
			StartCoroutine( playercomponent.HeadsUpDisplay ("adernaline selled forless amount of 500"));
		}
	}
	public void STUpgrade(){

		for (int i = 0; i < Upgrades.Length; i++) {
			Upgrades [i].SetActive (false);
		}
		UpgradeTexts [0].text = playercomponent.data.Power.SnailRate.ToString();
		Upgrades [0].SetActive (true);
		upgradeanim.Play ("st_u");


		foreach (GameObject a in upgradebutton) {
			a.GetComponent<Image> ().color = idle;
		}
		upgradebutton [0].GetComponent<Image> ().color = active;

	}
	public void FFUpgrade(){
		for (int i = 0; i < Upgrades.Length; i++) {
			Upgrades [i].SetActive (false);
		}
		UpgradeTexts [1].text = playercomponent.data.Power.PowerRate.ToString();
		Upgrades [1].SetActive(true);
		upgradeanim.Play ("ff_u");

		foreach (GameObject a in upgradebutton) {
			a.GetComponent<Image> ().color = idle;
		}
		upgradebutton [1].GetComponent<Image> ().color = active;

	}
	public void BikeUpgrade(){
		for (int i = 0; i < Upgrades.Length; i++) {
			Upgrades [i].SetActive (false);
		}
		UpgradeTexts [2].text = playercomponent.data.Power.Bike_Time.ToString() ;
		Upgrades [2].SetActive(true);
		upgradeanim.Play ("bike_u");
		foreach (GameObject a in upgradebutton) {
			a.GetComponent<Image> ().color = idle;
		}
		upgradebutton [2].GetComponent<Image> ().color = active;

	}
	public void InhalerUpgrade(){
		for (int i = 0; i < Upgrades.Length; i++) {
			Upgrades [i].SetActive (false);
		}
		UpgradeTexts [2].text = playercomponent.data.Profile.InhaleRate.ToString ();
		Upgrades [3].SetActive (true);
		upgradeanim.Play ("Inhaler_u");
		foreach (GameObject a in upgradebutton) {
			a.GetComponent<Image> ().color = idle;
		}
		upgradebutton [3].GetComponent<Image> ().color = active;
	
	}

	public void TankUpgrade(){
		for (int i = 0; i < Upgrades.Length; i++) {
			Upgrades [i].SetActive (false);
		}
		UpgradeTexts [4].text = playercomponent.data.Power.TankTime.ToString() ;
		Upgrades [4].SetActive (true);
		upgradeanim.Play ("tank_u");
		foreach (GameObject a in upgradebutton) {
			a.GetComponent<Image> ().color = idle;
		}
		upgradebutton [4].GetComponent<Image> ().color = active;

	}

	public void HealUpgrade(){
		for (int i = 0; i < Upgrades.Length; i++) {
			Upgrades [i].SetActive (false);
		}
		UpgradeTexts [5].text = playercomponent.data.Power.healDuration.ToString() ;
		Upgrades [5].SetActive (true);
		upgradeanim.Play ("heal_u");

		foreach (GameObject a in upgradebutton) {
			a.GetComponent<Image> ().color = idle;
		}
		upgradebutton [5].GetComponent<Image> ().color = active;

	}

	public void CloseUpgrade(){
		PlayerPrefs.SetInt ("UpgradeT",1);
		UpgradePanel.SetActive (false);
		GetComponent<UI_managment> ().CloseUpgradeTuts ();
        UI_managment.pantomath();
	}

	public GameObject MoneyTb;

	public void MoneyTab(){
		MoneyTb.SetActive (true);
	}
	public void CloseMoneyTab(){
		MoneyTb.SetActive (false);
	}

	public void UST_b(){
		if (playercomponent.data.Profile.Skill_Point > 0) {
			playercomponent.data.Profile.Skill_Point--;
			playercomponent.data.Power.SnailRate -= 1;
			playercomponent.SaveData ();
			UpdateUpgrades ();
			StartCoroutine( playercomponent.HeadsUpDisplay ("Snail Time upgraded"));
		}

	}

	public void UFF_b(){
		if (playercomponent.data.Profile.Skill_Point > 0) {
			playercomponent.data.Profile.Skill_Point-- ;
			playercomponent.data.Power.PowerRate -= 1;
			playercomponent.SaveData ();
			UpdateUpgrades ();
			StartCoroutine( playercomponent.HeadsUpDisplay ("Power forward upgraded"));
		}
	}

	public void UB_b(){
		if (playercomponent.data.Profile.Skill_Point > 0) {
			playercomponent.data.Profile.Skill_Point--;
			playercomponent.data.Power.Bike_Time += 1;
			playercomponent.SaveData ();
			UpdateUpgrades ();
			StartCoroutine( playercomponent.HeadsUpDisplay (" BIke upgraded"));
		}
	}

	public void UI_b(){
		if (playercomponent.data.Profile.Skill_Point > 0) {
			playercomponent.data.Profile.Skill_Point--;
			playercomponent.data.Profile.InhaleRate  += 5f;
			playercomponent.SaveData ();
			UpdateUpgrades ();
			StartCoroutine( playercomponent.HeadsUpDisplay ("Inhaler upgraded"));
		}
	}



	public void UT_b(){
		if (playercomponent.data.Profile.Skill_Point > 0) {
			playercomponent.data.Profile.Skill_Point--;
			playercomponent.data.Power.TankTime += 1;
			playercomponent.SaveData ();
			UpdateUpgrades ();
			StartCoroutine( playercomponent.HeadsUpDisplay ("Tank upgraded"));
		}
	}

	public void UH_b(){
		if (playercomponent.data.Profile.Skill_Point > 0) {
			playercomponent.data.Profile.Skill_Point--;
			playercomponent.data.Power.healDuration -= 1;
			playercomponent.SaveData ();
			UpdateUpgrades ();
			StartCoroutine( playercomponent.HeadsUpDisplay ("Heal upgraded"));
		}
	}

}
