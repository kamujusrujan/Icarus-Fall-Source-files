using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;




public class Googler : MonoBehaviour
{
    public Player playercomp;
    public static Googler instance { get; set; }

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start (){
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.DebugLogEnabled = true;
        Signin();
    }

    public  void  Signin() {
        Social.localUser.Authenticate(success => { });
    }

    public static void Achieved(string a) {
        Social.ReportProgress(a, 100, success => { });
    }

    public static void incrementsAch(string a) {
        PlayGamesPlatform.Instance.IncrementAchievement(a, 1, success => { });
    }

    public void Showachievemnts() {
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }

    public void ShowLeaderBoard() {
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }

    public void CheckScore() {
        if (playercomp.data.Profile.TopRun > 2) {
            Achieved(GPGSIds.achievement_crackerjack);
        }
        else if (playercomp.data.Profile.TopRun > 10)
        {
            Achieved(GPGSIds.achievement_adroit);
        }
        else if (playercomp.data.Profile.TopRun > 15)
        {
            Achieved(GPGSIds.achievement_ace);
        }
        else if (playercomp.data.Profile.TopRun > 20)
        {
            Achieved(GPGSIds.achievement_an_immortal);
        }
        else if (playercomp.data.Profile.TopRun > 30)
        {
            Achieved(GPGSIds.achievement_a_maestro);
        }
        else if (playercomp.data.Profile.TopRun > 40)
        {
            Achieved(GPGSIds.achievement_tour_de_force);
        }
        else if (playercomp.data.Profile.TopRun > 60)
        {
            Achieved(GPGSIds.achievement_a__true_professional);
        }
    }


    public void CheckBike()
    {
        if (playercomp.data.Store.Bike >= 5) {
            Achieved(GPGSIds.achievement_road_rage);
        }
        else if (playercomp.data.Store.Bike >=10) {
            Achieved(GPGSIds.achievement_on_your_bike);
        }
        else if (playercomp.data.Store.Bike >= 15)
        {
            Achieved(GPGSIds.achievement_the_bumpy_ride);
        }
        else if (playercomp.data.Store.Bike >= 25)
        {
            Achieved(GPGSIds.achievement_put_the_pedal_to_metal);
        }
        else if (playercomp.data.Store.Bike >= 50)
        {
            Achieved(GPGSIds.achievement_ride_the_light);
        }
    }
    // Update is called once per frame
    public void CheckTank() {
        if (playercomp.data.Store.Tank >= 5)
        {
            Achieved(GPGSIds.achievement_drunk_truck);
        }
        else if (playercomp.data.Store.Tank >= 10)
        {
            Achieved(GPGSIds.achievement_field_marshall);
        }
        else if (playercomp.data.Store.Tank >= 15)
        {
            Achieved(GPGSIds.achievement_want_you_for_us_amy);
        }
        else if (playercomp.data.Store.Tank >= 35)
        {
            Achieved(GPGSIds.achievement_son_of_a_gun);
        }
       
    }

    public void addleader() {
        string s = string.Format("{0:0.0000}", playercomp.data.Profile.TopRun);
        PlayGamesPlatform.Instance.ReportScore(long.Parse(s.Replace(".", "")), GPGSIds.leaderboard_digital_leaders,Social.localUser.userName, success => {
                
        });
    }


    public void CheckAdernaline() {
        if (playercomp.data.Store.Adernaline_Shot >= 5)
        {
            Achieved(GPGSIds.achievement_recovery);
        }
        else if (playercomp.data.Store.Adernaline_Shot >= 10)
        {
            Achieved(GPGSIds.achievement_alive_and_kicking);
        }
        else if (playercomp.data.Store.Adernaline_Shot >= 15)
        {
            Achieved(GPGSIds.achievement_handle_with_care);
        }
        else if (playercomp.data.Store.Adernaline_Shot >= 30)
        {
            Achieved(GPGSIds.achievement_elixir_of_life);
        }

    }

    public void CheckMoney() {
        if (playercomp.data.Profile.Money >= 2500)
        {
            Achieved(GPGSIds.achievement_deep_pockets);
        }
        else if (playercomp.data.Profile.Money >= 10000)
        {
            Achieved(GPGSIds.achievement_a_pot_of_gold);
        }
        else if (playercomp.data.Profile.Money >= 20000)
        {
            Achieved(GPGSIds.achievement_another_day_another_dollar);
        }
        else if (playercomp.data.Profile.Money >= 50000)
        {
            Achieved(GPGSIds.achievement_blood_money);
        }
        else if (playercomp.data.Profile.Money >= 80000)
        {
            Achieved(GPGSIds.achievement_break_the_bank);
        }
        else if (playercomp.data.Profile.Money >= 150000) {
            Achieved(GPGSIds.achievement_strike_it_rich);
        }
    }

    public void Checkgain() {
        if (playercomp.data.Profile.Gain >= 5000) {
            Achieved(GPGSIds.achievement_a_call_of_duty);
        }
        else if (playercomp.data.Profile.Gain >= 25000) {
            Achieved(GPGSIds.achievement_chin_up);
        }
        else if (playercomp.data.Profile.Gain >= 75000)
        {
            Achieved(GPGSIds.achievement_pride_and_joy);
        }
        else if (playercomp.data.Profile.Gain >= 99999)
        {
            Achieved(GPGSIds.achievement_a_magical_number);
        }
    }

    public void CheckSkill() {
        if (playercomp.data.Profile.Skill_Point >= 50 ) {
            Achieved(GPGSIds.achievement_a_stroke_of_genius);
        }
    }

    public void CheckJump() {
        if (playercomp.data.Achievements.jumps >= 50)
        {
            Achieved(GPGSIds.achievement_bouncy);
        }
        else if (playercomp.data.Achievements.jumps >= 100)
        {
            Achieved(GPGSIds.achievement_hop_skip_and_jump);
        }
        else if (playercomp.data.Achievements.jumps >= 150)
        {
            Achieved(GPGSIds.achievement_one_jump_ahead);
        }
        else if (playercomp.data.Achievements.jumps >= 300)
        {
            Achieved(GPGSIds.achievement_the_olympic_ideal);
        }
    }


    public void CheckMiss() {
        if (playercomp.data.Achievements.nearmiss >= 50) {
            Achieved(GPGSIds.achievement_near_squeak);
        }
        else if (playercomp.data.Achievements.nearmiss >= 100) {
            Achieved(GPGSIds.achievement_heart_stopper);
        }
        else if (playercomp.data.Achievements.nearmiss >= 150)
        {
            Achieved(GPGSIds.achievement_by_the_skin_of_your_teeth );
        }
        else if (playercomp.data.Achievements.nearmiss >= 300)
        {
            Achieved(GPGSIds.achievement_the_fatalist);
        }
      
    }
    public void CheckBump() {
        if (playercomp.data.Achievements.bumps >= 20) {
            Achieved(GPGSIds.achievement_hey_look_out);
        } 
        else if (playercomp.data.Achievements.bumps >= 50) {
            Achieved(GPGSIds.achievement_thunderstruck);    
        }
        else if (playercomp.data.Achievements.bumps >= 75)
        {
            Achieved(GPGSIds.achievement_couldnt_care_less);
        }
        else if (playercomp.data.Achievements.bumps >= 150)
        {
            Achieved(GPGSIds.achievement_war_torn);
        }
        else if (playercomp.data.Achievements.bumps >= 300)
        {
            Achieved(GPGSIds.achievement_dare_devil);
        }
    }

    void Update (){

		
	}
}
