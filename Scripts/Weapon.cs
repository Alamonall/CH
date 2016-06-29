using UnityEngine;
using System.Collections;

public class Weapon : InventoryItem {
	
	public float spread; 	// разброс
	public float rate; 		// скорострельность
	public float fullReload; 	// Время перезарядки с пустой обоймой
	public float fastReload; 	//Время перезарядки с не пустой обоймой
	public int ammo; 		// максимальное количетсво патронов в обойме
	public int holder; 		// текущее количетсво патронов в обойме
	public string ammoType; 	//тип патронов
	public string description;
	public string shootingMode;

	public float physicalDamage;
	public float fireDamage;
	public float electricDamage;
	public float plasmaDamage;
	public float poisonDamage;
	public float energyDamage;
	public float corrosionDamage;

	public Weapon(){
		this.itemName = "hands";
		this.itemPrice = 0;
		this.itemIcon = null;
		this.physicalDamage = 0;
		this.spread = 0;
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
						string name,//2
						float price,//3
						float damage,//6
						float spread,//7
						float rate,//8
						float fullReload,//9
						float fastReload,//10
						int ammo,//11
						string type,//12
						string description,//13
						string ammoType, //14
						string shootingMode
	){
		this.id = id;
		this.itemName = name;
		this.itemPrice = price;
		this.itemIcon = Resources.Load (name,typeof(Sprite)) as Sprite;
//		Debug.Log ("ICON = " + this.itemIcon.name);
		this.physicalDamage = damage;
		this.spread = spread;
		this.rate = rate;
		this.fullReload = fullReload;
		this.fastReload = fastReload;
		this.ammo = ammo;
		this.type = type;
		this.holder = ammo;
		this.description = description;
		this.shootingMode = shootingMode;
		this.ammoType = ammoType;
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
