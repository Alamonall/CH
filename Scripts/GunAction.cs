using UnityEngine;
using System.Collections;

public class GunAction : MonoBehaviour {

	public static GunAction _instanceGA;
	Character characterScript;
	UIManager uiScript;

	private GameObject bulletClone; //
	public GameObject bullet; //ссылка на префаб пули

	public int ammo; // количество пуль в обойме
	public int holder; //текущее количество пуль в обойме
	public int maxAmmo; // общее количество патронов в рюкзаке персонажа
	public float fullReload; 	// Время перезарядки с пустой обоймой
	public float fastReload; 	//Время перезарядки с не пустой обоймой
	public float tempReload; // текущее время на перезарядку
	public bool overReload = true; //закончилась ли перезарядка?
	public float rate; //промежуток между выстрелами, получаем из оружия
	public float tempRate;
	public bool bRate = true; // промежуток между выстрелами

	public GameObject bulletPull;
	public Quaternion tempRot;
	public Vector3 lookPos;
	public string shootingMode;

	//tempering var
	Weapon weaponTemp; 
	bool weaponTriggerUp;
	int tempQueue;

	#region Awake
	void Awake(){
		if (_instanceGA == null)
			_instanceGA = this;
		else if (_instanceGA != this) {
			print ("GA not alone");
		}
		lookPos = new Vector3 (0, 0, 0);
		uiScript = UIManager._instanceUIM;
		characterScript = Character._instanceCharacter;
		UpdateParameters ();
	}
	#endregion

	#region Update
	void FixedUpdate(){
		//промежуток между выстрелами
		if (!bRate) {
			if (tempRate <= 0) {
				bRate = true;
				tempRate = rate;
			}
			tempRate -= Time.deltaTime*10;
		}

		if (!overReload) {
			tempReload -= Time.deltaTime;
			if (tempReload < 0){
				maxAmmo -= ammo - holder;
				characterScript.SetAmmo (maxAmmo);
				holder = ammo;
				overReload = true;
				SaveHolder ();
			}	
		}
	}

	void Update () {
		if (Input.GetMouseButton (0)) {
			Shooting();
		}

		if (Input.GetMouseButtonUp (0)) {
			weaponTriggerUp = true;
		}

		//быстрая смена оружия
		if(Input.GetKeyUp(KeyCode.Q)){
			characterScript.SwapWeapon();
			tempReload = -1;
		}
		//перезарядка
		if(Input.GetKeyUp(KeyCode.R)){
//			print ("RELOAD!");
			Reload();
		}

		Vector3 mousePosMain = Input.mousePosition;
		mousePosMain.z = Camera.main.transform.position.z; 
		lookPos = Camera.main.ScreenToWorldPoint(mousePosMain);
		lookPos = lookPos - transform.position;
		float angle  = Mathf.Atan2(lookPos.y * -1, lookPos.x * -1) * Mathf.Rad2Deg;
		tempRot = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = tempRot;
	}
	#endregion

	#region Shooting
	public void Shooting()
	{
		if (uiScript.bAMenu || uiScript.bMMenu || uiScript.bDMenu)
			return;
		//проверка не активна ли сейчас перезарядка или не кончились ли патроны
		if (overReload) {			
			if (holder == 0) {
//				print ("NEED A RELOAD!");
				return;
			}
			if (bRate && weaponTriggerUp) {
				bulletClone = Instantiate(bullet, transform.position, tempRot) as GameObject; 
//				bulletClone.transform.SetParent (bulletPull.transform);
				Rigidbody2D rg2 = bulletClone.GetComponent<Rigidbody2D> ();
				rg2.AddForce ((tempRot*Vector2.right));
				holder--;
				SaveHolder ();
				bRate = false;
				weaponTriggerUp = ShootingMode();
			}			
		} else {	
			overReload = false;
		}
	}
	#endregion

	#region Shootingmode
	//отвечает за тип стрельбы
	//
	public bool ShootingMode(){
		
		switch(shootingMode){
		case "Single":
			return false;
		case "Queue":
			tempQueue++;
			if (tempQueue == 4) {
				tempQueue = 0;
				return false;
			}
			break;
		}	
		return true;	
	}
	#endregion

	public void SaveHolder(){
		if (characterScript.activeWeapon) 
			characterScript.PrimaryWeapon.Holder = holder;
		if (!characterScript.activeWeapon) 
			characterScript.SecondaryWeapon.Holder = holder;
	}

	#region UpdateParameters
	//обновляем параметры при изменении оружия у персонажа в руках
	public void UpdateParameters(){
//		print ("Update Weapon!");
		if (uiScript == null){
			print ("uiscript null");
			return;
		}
		if (characterScript == null) {
			print ("charScript is null");
			return;
		}

		//проверку на наличие оружия в руках
		if (characterScript.activeWeapon) 
			weaponTemp = characterScript.PrimaryWeapon;
		if (!characterScript.activeWeapon)
			weaponTemp = characterScript.SecondaryWeapon;
		holder = 0;
		ammo = weaponTemp.Ammo;
		holder = weaponTemp.Holder;
		fullReload = weaponTemp.FullReload;
		fastReload = weaponTemp.FastReload;
		rate = weaponTemp.Rate;
		SaveHolder ();
		maxAmmo = characterScript.GetAmmo();
		tempReload = fullReload;
		tempRate = rate;
		shootingMode = weaponTemp.ShootingMode;
	}
	#endregion


	#region Reload
	public void Reload(){
		if (holder > 0 && holder != ammo)
			tempReload = fastReload;
		else
			tempReload = fullReload;	
		overReload = false;
	}
	#endregion
}
