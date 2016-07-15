using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
	public static Character _instanceCharacter;
	public bool activeWeapon = true; // при true используется primary, иначе secondary
	public bool needUpdate = false;

	//[SerializeField]
	public Weapon primaryWeapon;
	public Weapon secondaryWeapon;
	public Armor armor;
	public Medkit medkit;
	public Granade granade;
	public Inventory inventory;

	public int currentLevel;
	public float currentMoney; //текущее количество денег

	public float currentExperience; // текущий опыт		
	public float nextLevelExperience; // необходимо опыта до получения нового уровня

	public int currentSkillPoints; // текущее количество очков умений
	//????
	public int nextLevelSkillPoints;	// количество очков опыта за получение нового уровня
		
	public float currentStaminaPoints; // текущая выносливость
	public float maxStaminaPoints; //  максимальное количество выносливости

	public float currentHealthPoints; // текущие очки жизни
	public float modHealtPoints; // модификатор жизней при получении нового уровня
	public float maxHealtPoints; // максимальное количество очков жизней на данном уровне

	public float speed;  // текущая скорость
	public float overSpeed; //скорость при ускорении

	//temp
	public float physicalDamagePrimaryWeapon;
	public float fireDamagePrimaryWeapon;
	public float electricDamagePrimaryWeapon;
	public float plasmaDamagePrimaryWeapon;
	public float poisonDamagePrimaryWeapon;
	public float energyDamagePrimaryWeapon;
	public float corrosionDamagePrimaryWeapon;
	public float physicalDamageSecondaryWeapon;
	public float fireDamageSecondaryWeapon;
	public float electricDamageSecondaryWeapon;
	public float plasmaDamageSecondaryWeapon;
	public float poisonDamageSecondaryWeapon;
	public float energyDamageSecondaryWeapon;
	public float corrosionDamageSecondaryyWeapon;
	public float totalPhysicalResistance;
	public float totalFireResistance;
	public float totalElectricResistance;
	public float totalPlasmaResistance;
	public float totalPoisonResistance;
	public float totalCorrosionResistance;
	public float totalEnergyResistance;

	public int activeAmmoType;

	public int pistolAmmo;
	public int shotgunAmmo;
	public int rifleAmmo;
	public int assaultRifleAmmo;
	public int laserAmmo;
	public int beamAmmo;
	public int rocketAmmo;

	public Skill[] ArraySkills;

	void Awake(){
		if (_instanceCharacter == null) {
			DontDestroyOnLoad (this.gameObject);
			_instanceCharacter = this;
			print ("Character Awake!");
		}

		ArraySkills = new Skill[16];
		ArraySkills [0] = FirstSkill._instFirstS;
		ArraySkills [1] = SecondSkill._instSecondS;
		ArraySkills [2] = ThirdSkill._instThirdS;

		pistolAmmo = 100;
		shotgunAmmo = 100;
		rifleAmmo = 100;
		assaultRifleAmmo = 100;
		laserAmmo = 100;
		beamAmmo = 100;
		rocketAmmo = 100;
		PrimaryWeapon = new Weapon ();
		SecondaryWeapon = new Weapon ();

		this.nextLevelExperience = 1000;
		this.nextLevelSkillPoints = 1;
		this.maxStaminaPoints = 500;
		this.maxHealtPoints = 100;
		this.modHealtPoints = 100;
		this.speed = 5;
		this.overSpeed = 7;

		currentLevel = 1;
		currentMoney = 100;

		currentExperience = 0;
		nextLevelExperience = 1000;

		currentStaminaPoints = maxStaminaPoints;
		currentHealthPoints = maxHealtPoints;

		primaryWeapon = new Weapon();
		secondaryWeapon = new Weapon();
		armor = new Armor();
		granade = new Granade ();
		//????
		medkit = null;
		Inventory = new Inventory (24);
	}

	public void IncreaseGrowth(int incDuration, int incRecoil, int incAction){
		
	}

	public Skill GetSkill(int num){
		return ArraySkills [num];
	}

	public bool CheckLevelUp(){
		if (nextLevelExperience <= currentExperience)
			return true;
		else
			return false;
	}

	public bool CheckHeIsDead(){
		if (currentHealthPoints <= maxHealtPoints)
			return true;
		else
			return false;
	}

	public void SwapWeapon(){
		activeWeapon = !activeWeapon;
		UpdateParameters ();
	}

	public void LevelUp(){
//		Debug.Log ("Level Up! " + currentLevel + "!" );
		currentLevel++;
		maxHealtPoints += modHealtPoints;
		currentHealthPoints = maxHealtPoints;
		currentExperience = currentExperience-nextLevelExperience;
		nextLevelExperience = nextLevelExperience*2;
		currentSkillPoints++;
		gameObject.SendMessage ("CharGetLevel", this);
	}

	#region GetAmmo
	public int GetAmmo(){
		int temp = 0;
		string ammoType;
		if (activeWeapon)
			ammoType = primaryWeapon.ammoType;
		else
			ammoType = secondaryWeapon.ammoType;
	
		switch(ammoType){
			case "PistolAmmo": // Пистолеты, револьверы, пистолеты-пулеметы
				temp = pistolAmmo;
				
				break;
			case "ShotgunAmmo": //дробовики				
				temp = shotgunAmmo;
				break;
			case "RifleAmmo":				
				temp = rifleAmmo;
				break;
			case "AssaultRifleAmmo":
				temp = assaultRifleAmmo;
				break;
			case "LaserAmmo":
				temp = laserAmmo;
				break;
			case "BeamAmmo":
				temp = beamAmmo;
				break;
			case "RocketAmmo":				
				temp = rocketAmmo;
				break;
			}		
		print ("GetAmmo " + ammoType + " = " + temp);
		return temp;
	}
	#endregion

	#region SetAmmo
	public void SetAmmo(int ammo){
		string ammoType;

		if (activeWeapon)
			ammoType = primaryWeapon.ammoType;
		else
			ammoType = secondaryWeapon.ammoType;
		
		switch(ammoType){
			case "PistolAmmo": // Пистолеты, револьверы, пистолеты-пулеметы
				pistolAmmo = ammo;
				break;
			case "ShotgunAmmo": //дробовики
				shotgunAmmo = ammo;
				break;
			case "RifleAmmo":
				rifleAmmo = ammo;
				break;
			case "AssaultRifleAmmo":
			print ("SetAmmo " + ammoType + " = " + ammo);
				assaultRifleAmmo = ammo;
				break;
			case "LaserAmmo":
				laserAmmo = ammo;
				break;
			case "BeamAmmo":
				beamAmmo = ammo;
				break;
			case "RocketAmmo":
				rocketAmmo = ammo;
				break;
		}			
	}
	#endregion

	public Weapon PrimaryWeapon {
		get {
			return primaryWeapon;
		}
		set {
			if(value == null)
				primaryWeapon = new Weapon();
			else
				primaryWeapon = value;
			UpdateParameters ();
		}
	}

	public Weapon SecondaryWeapon {
		get {
			return secondaryWeapon;
		}
		set {
			if (value == null)
				secondaryWeapon = new Weapon ();
			else
				secondaryWeapon = value;
			UpdateParameters ();
		}
	}
	//Обновляет параметры после смены предметов (брони, оружия)
	public void UpdateParameters (){
		needUpdate = true;
	}

	public Armor Armor {
		get {
			return armor;
		}
		set {
			if (armor == null)
				armor = value;
			else {
				//Inventory.add (armor);
				armor = value;
			}
			UpdateParameters ();
		}
	}

	public float Speed {
		get {
			return speed;
		}
		set {
			speed = value;
		}
	}

	public Medkit Medkit {
		get {
			return medkit;
		}
		set {
			medkit = value;
			UpdateParameters ();
		}
	}

	public Granade Granade {
		get {
			return granade;
		}
		set {
			granade = value;
			UpdateParameters ();
		}
	}

	public Inventory Inventory {
		get {
			return inventory;
		}
		set {
			inventory = value;
		}
	}

	public float CurrentHealthPoints {
		get {
			return currentHealthPoints;
		}
		set {
			currentHealthPoints = value;
		}
	}

	public float MaxHealtPoints {
		get {
			return maxHealtPoints;
		}
		set {
			maxHealtPoints = value;
		}
	}

	public float OverSpeed {
		get {
			return overSpeed;
		}
		set {
			overSpeed = value;
		}
	}

	public float CurrentMoney {
		get {
			return currentMoney;
		}
		set {
			currentMoney = value;
		}
	}
}

