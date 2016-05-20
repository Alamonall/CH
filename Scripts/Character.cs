using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
	public static Character _instanceCharacter;
	public bool activeWeapon = true; // при true используется primary, иначе secondary
	public bool needUpdate = false;

	//[SerializeField]
	public Specialization currentSpec;
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

	public Ammo activeAmmoType;

	public AssaultRiflesAmmoType assaultRiflesAmmo;

	void Awake(){
		if (_instanceCharacter == null) {
			DontDestroyOnLoad (this.gameObject);
			_instanceCharacter = this;
			print ("Character Awake!");
		}
//		else if (_instanceCharacter != this)
//			Destroy (this.gameObject);
		
	}

	public void SelectSpecialization(Specialization spec){
		this.currentSpec = spec;
		this.nextLevelExperience = spec.nextLevelExperience;
		this.nextLevelSkillPoints = spec.nextLevelSkillPoints;
		this.maxStaminaPoints = spec.maxStaminaPoints;
		this.maxHealtPoints = spec.maxHealtPoints;
		this.modHealtPoints = spec.modHealtPoints;
		this.speed = spec.speed;
		this.overSpeed = spec.overSpeed;

		currentLevel = 0;
		currentMoney = 100;

		currentExperience = 0;
		nextLevelExperience = 1000;

		currentStaminaPoints = maxStaminaPoints;
		currentHealthPoints = maxHealtPoints;

		primaryWeapon = new Weapon();
		secondaryWeapon = new Weapon();
		armor = new Armor();
		granade = new Granade ();
		medkit = new Medkit ();
		Inventory = new Inventory (24);
		//Temp
		assaultRiflesAmmo = new AssaultRiflesAmmoType ();
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
		Debug.Log ("Level Up!");
		currentLevel++;
		maxHealtPoints += modHealtPoints;
		currentHealthPoints = maxHealtPoints;
		currentExperience = currentExperience-nextLevelExperience;
		nextLevelExperience = nextLevelExperience*2;
	}

	public int GetAmmo(){
		Ammo temp;
		if (activeWeapon)
			temp = primaryWeapon.ammoType;
		else
			temp = secondaryWeapon.ammoType;			
		if(temp is AssaultRiflesAmmoType){
			return assaultRiflesAmmo.CurrentAmmo;//				
		}	
			return 0;
	}

	public void SetAmmo(int ammo){
		Ammo temp;
		if (activeWeapon)
			temp = primaryWeapon.ammoType;
		else
			temp = secondaryWeapon.ammoType;			
		if (temp is AssaultRiflesAmmoType) {
			assaultRiflesAmmo.CurrentAmmo = ammo;
		}
	}

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

	public AssaultRiflesAmmoType AssaultRiflesAmmo {
		get {
			return assaultRiflesAmmo;
		}
		set {
			assaultRiflesAmmo = value;
		}
	}
}

