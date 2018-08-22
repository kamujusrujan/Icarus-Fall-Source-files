using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Player_data  {
	public string Name;
	public Store Store;
	public Profile Profile;
	public Power Power;
    public Achievements Achievements;


}
[System.Serializable]
public class Store{
	public int Tank;
	public int Inhaler;
	public int Adernaline_Shot;
	public int Bike;
}
[System.Serializable]
public class Profile{
	public float Xp;
	public float InhaleRate;
	public float Gain;
	public float Money;
	public float TopRun;
	public int Skill_Point;
	public bool Instructions;
}
[System.Serializable]
public class Power{
	public float SnailTime;
	public float PowerForward;
	public float Bike_Time;
	public float SnailRate;
	public float PowerRate;
	public float healDuration;
	public float TankTime;
}
[System.Serializable]
public class Achievements {
    public int jumps;
    public int nearmiss;
    public int bumps;
}
