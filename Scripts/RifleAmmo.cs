﻿using UnityEngine;
using System.Collections;

public class RifleAmmo : Ammo {
	float damage;
	int currentAmmo;
	int maxAmmo;

	override public GameObject GetPrefab(){
		return (GameObject)Resources.Load ("RifleBullet", typeof(GameObject));
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