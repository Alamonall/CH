using UnityEngine;
using System.Collections;

public class AssaultRifleAmmo : Ammo
{
	float damage;
	int currentAmmo;
	int maxAmmo;

	override public GameObject GetPrefab(){
		Debug.Log ("Get Pref from Assault Rifle");
		return Resources.Load ("AssaultRifleBullet", typeof(GameObject)) as GameObject;
	} 

	public int CurrentAmmo {
		get {
			return currentAmmo;
		}
		set {
			currentAmmo = value;
		}
	}

	public int MaxAmmo {
		get {
			return maxAmmo;
		}
		set {
			maxAmmo = value;
		}
	}

	public float Damage {
		get {
			return damage;
		}
		set {
			damage = value;
		}
	}
}

