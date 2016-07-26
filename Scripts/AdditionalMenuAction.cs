using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class AdditionalMenuAction : MonoBehaviour {
	public static AdditionalMenuAction _instanceAMA;

	public UIManager uiScript;
	public Character characterScript;

	public bool bInventory;
	public bool bSkills;
	public bool bJournal;
	public bool bMap;

	public bool bAMenu = false; //button additional menu
	public bool bMMenu = false; //button main menu
	public bool bDMenu = false; // drop menu flag

	public bool dontShoot;

	public GameObject AdditionalMenu;
	public GameObject pInventory;
	public GameObject pSkills;
	public GameObject pJournal;
	public GameObject pMap;
	public GameObject pQuickNavButtons;
	public GameObject dropListMenu;
	public GameObject prevActivePanel;
	public GameObject mainMenu;

	public Comparing comparingMenu;
	public GameObject previewImage;
	Vector3 uiOff = new Vector3 (0,0,0); // вектор для выключения элемента интерфейса
	Vector3 uiOn = new Vector3(1,1,1);// вектор для включения элемента интерфейса

	public GameObject[] inventoryCellList; //список предметов в инвентаре
	public InventoryItem[] inventoryItemList; //список предметов в инвентаре для графического представления


	DragAndDrop dadForContextMenu; //обьект для контекстного меню

	void Awake()
	{
		if (_instanceAMA == null) {
			_instanceAMA = this;
		} else if (_instanceAMA != this)
			Destroy (this.gameObject);
		

		characterScript = Character._instanceCharacter;
		HideAddMenu ();
	}


	void Start(){
		uiScript = UIManager._instanceUIM;
		bInventory = false;
		bSkills = false;
		bJournal = false;
		bMap = false;
		prevActivePanel = null;
		dontShoot = true;
		bAMenu = false;
	}

	void OnGUI(){
		if (Input.GetKeyDown (KeyCode.I)) {
			bInventory = !bInventory;
			if (bInventory)
				SwitchUIObjects ("bInventory");
			else
				HideAddMenu ();
		} else if (Input.GetKeyDown (KeyCode.P)) {
			bSkills = !bSkills;
			if (bSkills)
				SwitchUIObjects ("bSkills");
			else
				HideAddMenu ();
		} else if (Input.GetKeyDown (KeyCode.J)) {
			bJournal = !bJournal;
			if (bJournal)
				SwitchUIObjects ("bJournal");
			else
				HideAddMenu ();
		} else if (Input.GetKeyDown (KeyCode.M)) {
			bMap = !bMap;
			if (bMap)
				SwitchUIObjects ("bMap");
			else
				HideAddMenu ();	
		}
		if(Input.GetKeyDown(KeyCode.Escape))
			WasPressedEscape ();
	}

	void Update ()
	{		
		//temp 
		if (!uiScript.gameActive)
			HideAddMenu ();
		//TEMP
		if(bAMenu || bDMenu || bMMenu)
			dontShoot = true;
		else 
			dontShoot = false;

		OnGUI ();
	}


	#region WhichBuutonIsPressed
	public void SwitchUIObjects(string buttonName){
		switch (buttonName) {
		case "bInventory":
			bSkills = false;
			bMap = false;
			bJournal = false;
			ShowUIObject (pInventory);
			break;
		case "bSkills":
			bMap = false;
			bJournal = false;
			bInventory = false;
			ShowUIObject (pSkills);
			break;
		case "bJournal":
			bSkills = false;
			bMap = false;
			bInventory = false;
			ShowUIObject (pJournal);
			break;
		case "bMap":
			bSkills = false;
			bInventory = false;
			bJournal = false;
			ShowUIObject (pMap);
			break;
		case "close":
			bMap = false;
			bSkills = false;
			bInventory = false;
			bJournal = false;
			HideAddMenu ();
			break;
		}
	}
	#endregion

	#region EscapeWasPressed
	public void WasPressedEscape(){
		if (bAMenu) {
			bAMenu = false;
			bMap = false;
			bSkills = false;
			bInventory = false;
			bJournal = false;
			this.gameObject.transform.localScale = uiOff;
		} else if (bMMenu) {
			bMMenu = false;
			mainMenu.transform.localScale = uiOff;
		} else if (bDMenu) {
			bDMenu = false;
			dropListMenu.transform.localScale = uiOff;
		} else {
			bMMenu = !bMMenu;
			mainMenu.transform.localScale = uiOn;
		}
	}
	#endregion

	#region Hide&ShowAddMenu
	public void HideAddMenu()
	{
		//temp if
		if(bAMenu)
			bAMenu = !bAMenu;
		AdditionalMenu.transform.localScale = uiOff;
	}

	#endregion

	#region Hide&ShowUIObject
	void ShowUIObject(GameObject uiObj){	
		try{
			if (!bAMenu){
				bAMenu = true;
				AdditionalMenu.transform.localScale = uiOn;
			}
			if(prevActivePanel != null)			
				prevActivePanel.transform.localScale = uiOff;
			uiObj.transform.localScale = uiOn;
			prevActivePanel = uiObj;

		} catch(Exception exc){
			Debug.LogException (exc, this);
		}
	}

	void HideObject(){	
		if (!bInventory)
			pInventory.transform.localScale = uiOff;
		if (bSkills)
			pSkills.transform.localScale = uiOff;		
		if (bJournal)
			pJournal.transform.localScale = uiOff;		
		if (bMap)
			pMap.transform.localScale = uiOff;
	}
	#endregion

	#region SetupInventory
	public void SetupInventory (){		
		for (int i = 0; i < characterScript.Inventory.itemList.Length; i++) {		
//			Debug.Log ("ItemList[" + i + "] = " + itemList [i]);
			if (characterScript.Inventory.itemList [i] != null) {
//				Debug.Log("Name = " + itemList[i].itemName + "; Icon = " + itemList [i].ItemIcon);
				inventoryCellList [i].GetComponent<Image> ().sprite = characterScript.Inventory.itemList [i].itemIcon;	
			}
		}	
	}
	#endregion

	#region UpdateInventoryCellList
	public void UpdateInventoryCellList(){
//		print ("UpdateInventoryCellList");
		if (!characterScript.PrimaryWeapon.itemName.Equals ("hands")) {
//			print ("Sprite Primary Weapon is Upadate");
			GameObject.Find ("PrimaryWeaponCell").GetComponent<DragAndDrop> ().UpdateItem (characterScript.PrimaryWeapon);
		} 
		if (!characterScript.SecondaryWeapon.itemName.Equals ("hands")) {
//			print ("Sprite Secondary Weapon is Upadate");
			GameObject.Find ("SecondaryWeaponCell").GetComponent<DragAndDrop> ().UpdateItem (characterScript.SecondaryWeapon);
		}
		if (!characterScript.Armor.itemName.Equals ("empty")) {
			GameObject.Find ("ArmorCell").GetComponent<DragAndDrop> ().UpdateItem (characterScript.Armor);
		}
		if (!characterScript.Granade.itemName.Equals ("empty")) {
			GameObject.Find ("GranadeCell").GetComponent<DragAndDrop> ().UpdateItem (characterScript.Granade);
		}
//		if (!characterScript.Medkit.itemName.Equals ("empty")) {
//			GameObject.Find ("MedkitCell").GetComponent<DragAndDrop> ().UpdateItem (characterScript.Medkit);
//		}
		for (int i = 0; i < characterScript.Inventory.ItemList.Length; i++) {
			if (characterScript.Inventory.ItemList [i] != null) {
//				print (characterScript.Inventory.itemList [i].itemName);
				if (characterScript.Inventory.itemList [i].itemIcon == null) {
					print ("item icon is null");
					return;
				}
				inventoryCellList [i].GetComponent<DragAndDrop> ().UpdateItem (characterScript.Inventory.itemList [i]);
//				Debug.Log (" inventoryCellList = " + inventoryCellList [i].name);
			}
		}
		inventoryItemList = characterScript.Inventory.ItemList;
	}
	#endregion


	#region PutItemFromItemList
	public void GetItemToCharacter(InventoryItem item)
	{	
		switch (item.type) {
		case "Weapon":
			{
//				print ("PrimaryWeapon = " + characterScript.PrimaryWeapon.itemName);
//				print ("SecondaryWeapon = " + characterScript.SecondaryWeapon.itemName);
				if (characterScript.PrimaryWeapon.itemName.Equals ("hands")) {
					characterScript.PrimaryWeapon = item as Weapon;
					characterScript.activeWeapon = true;
				} else if (characterScript.SecondaryWeapon.itemName.Equals ("hands")) {
					characterScript.SecondaryWeapon = item as Weapon; 
				} else {
					characterScript.Inventory.PutItem (item); 
				}
//				print ("PrimaryWeapon = " + characterScript.PrimaryWeapon.itemName);
//				print ("SecondaryWeapon = " + characterScript.SecondaryWeapon.itemName);
				break;
			}
		case "Armor":
			{
//				print ("armor = " + item.itemName);
				if (characterScript.Armor.itemName == "empty")
					characterScript.Armor = item as Armor;
				else
					characterScript.Inventory.PutItem (item);
				break;
			}
		case "Medkit":
			{
//				print ("medkit = " + item.itemName);
				if (characterScript.Medkit == null)
					characterScript.Medkit = item as Medkit;
				else
					characterScript.Inventory.PutItem (item);
				break;
			}

		case "Granade":
			{
//				print ("granade = " + item.itemName);
				if (characterScript.Granade == null)
					characterScript.Granade = item as Granade;
				else
					characterScript.Inventory.PutItem (item);
				break;
			}
		}
		UpdateInventoryCellList ();
	}
	#endregion

	#region GetCharCellType
	private string GetCharCellType(string name)
	{
		switch(name)
		{
		case "PrimaryWeaponCell" :
			return "Weapon";
		case "SecondaryWeaponCell": 
			return "Weapon";
		case "ArmorCell":
			return "Armor";
		case "GranadeCell":
			return "Granade";
		case "MedkitCell":
			return "Medkit";
		default:
			return "InventoryItem";	
		}
	}
	#endregion


	#region ShowContexMenu
	public void ShowContextMenu(GameObject go, DragAndDrop dad){
		if (dad.item == null)
			return;
		previewImage.transform.localPosition = go.transform.localPosition;
		previewImage.GetComponent<PreviewScript> ().TurnOnPreview ();
		dadForContextMenu = dad;
	}
	#endregion

	#region Comparing
	public void Comparing(){
		comparingMenu.ComparingItem (dadForContextMenu.item, characterScript);
		dadForContextMenu = null;
		previewImage.GetComponent<PreviewScript> ().TurnOffPreview ();
	}
	#endregion

	#region Droping
	public void Droping(){
		uiScript.AddToDropZone (dadForContextMenu.item);
		characterScript.Inventory.DropItem (dadForContextMenu.item);
		dadForContextMenu.UpdateItem (null);
		dadForContextMenu = null;
		previewImage.GetComponent<PreviewScript> ().TurnOffPreview ();
	}
	#endregion 


}
