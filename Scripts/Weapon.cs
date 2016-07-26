using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : InventoryItem {
	
	public float rate; 		// скорострельность
	public float fullReload; 	// Время перезарядки с пустой обоймой
	public float fastReload; 	//Время перезарядки с не пустой обоймой
	public int ammo; 		// максимальное количетсво патронов в обойме
	public int holder; 		// текущее количетсво патронов в обойме
	public string ammoType; 	//тип патронов
	public string description;
	public string shootingMode; // Single Auto Semi-auto Queue

//	public float physicalDamage;
//	public float fireDamage;
//	public float electricDamage;
//	public float iceDamage;
//	public float poisonDamage;
//	public float energyDamage;
//	public float corrosionDamage;

	Dictionary<string, Damages> damages;

	public Weapon(){
		this.itemName = "hands";
		this.itemPrice = 0;
		this.itemIcon = null;
		this.rate = 0;
		this.fullReload = 0;
		this.fastReload = 0;
		this.ammo = 0;
		this.type = "";
		this.holder = 0;
		this.shootingMode = "Continuous";
		this.ammoType = "";
	}

	public Weapon(		int id, 	//1
						string wname,//2
						string itemIcon,//3						
						float rate,//4
						string shootingMode, //5
						int ammo,//6
						float wprice, //7
						float fullReload,//8
						float fastReload,//9
						string ammoType, //14
		Damages physical,
		Damages fire,
		Damages electrial,
		Damages energy,
		Damages poison,
		Damages corrosive,
		Damages ice,
		string type
	){
		damages = new Dictionary<string, Damages> ();
		damages.Add ("physical", physical);
		damages.Add ("fire", fire);
		damages.Add ("electrial", electrial);
		damages.Add ("energy", energy);
		damages.Add ("poison", poison);
		damages.Add ("corrosive", corrosive);
		damages.Add ("ice", ice);
		this.id = id;
		this.itemName = wname;
		this.itemPrice = wprice;
		this.itemIcon = Resources.Load (itemIcon,typeof(Sprite)) as Sprite;
		Debug.Log ("ICON = " + this.itemIcon.name);
		this.rate = rate;
		this.fullReload = fullReload;
		this.fastReload = fastReload;
		this.ammo = ammo;
		this.holder = ammo;
		this.shootingMode = shootingMode;
		this.ammoType = ammoType;
		this.type = type;
	}

	public void Use(){
		
	}

	public void Set(){
		
	}

	public float Rate {
		get {
			return rate;
		}
	}

	public int Ammo {
		get {
			return ammo;
		}
	}


	public int Holder {
		get {
			return holder;
		}
		set {
			holder = value;
		}
	}

	public float FullReload {
		get {
			return fullReload;
		}
	}

	public float FastReload {
		get {
			return fastReload;
		}
	}

	public string ShootingMode {
		get {
			return shootingMode;
		}
	}
}
