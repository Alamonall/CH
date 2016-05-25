using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AdditionalMenuAction : MonoBehaviour {
	public static AdditionalMenuAction _instanceAMA;

	public UIManager uiScript;
	public Character characterScript;

	public bool bInventory;
	public bool bSkills;
	public bool bJournal;
	public bool bMap;

	public GameObject pInventory;
	public GameObject pSkills;
	public GameObject pJournal;
	public GameObject pMap;

	public GameObject prevActivePanel;
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
		
		uiScript = UIManager._instanceUIM;
		characterScript = Character._instanceCharacter;

		bInventory = false;
		bSkills = false;
		bJournal = false;
		bMap = false;
	}

	void Update ()
	{		
		if (Input.GetKeyDown (KeyCode.I)) { 
			bInventory = !bInventory;
			if (!bInventory)
				UIManager._instanceUIM.bAMenu = false;
			else {
				SwitchUIObjects ("bInventory");	
			}
		}
		else if (Input.GetKeyDown (KeyCode.P)) { 
			bSkills = !bSkills;
			if (!bSkills)
				UIManager._instanceUIM.bAMenu = false;
			else {
				SwitchUIObjects ("bSkills");
			}
		}
		else if (Input.GetKeyDown (KeyCode.J)) { 
			bJournal = !bJournal;
			if (!bJournal)
				UIManager._instanceUIM.bAMenu = false;
			else {
				SwitchUIObjects ("bJournal");

			}
		}
		else if (Input.GetKeyDown (KeyCode.M)) { 
			bMap = !bMap;
			if (!bMap)				
				UIManager._instanceUIM.bAMenu = false;
			else {
				SwitchUIObjects ("bMap");
			}
		}
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
			UIManager._instanceUIM.bAMenu = !UIManager._instanceUIM.bAMenu;
			break;
		}
	}
	#endregion

	#region ShowUIObject
	void ShowUIObject(GameObject uiObj){	
		if (!UIManager._instanceUIM.bAMenu)
			UIManager._instanceUIM.bAMenu = true;
		if(prevActivePanel != null)			
			prevActivePanel.transform.localScale = uiOff;
		uiObj.transform.localScale = uiOn;
		prevActivePanel = uiObj;
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
		if (!characterScript.Medkit.itemName.Equals ("empty")) {
			GameObject.Find ("MedkitCell").GetComponent<DragAndDrop> ().UpdateItem (characterScript.Medkit);
		}
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
		previewImage.transform.localScale = uiOn;
		dadForContextMenu = dad;
	}
	#endregion

	#region Comparing
	public void Comparing(){
		comparingMenu.ComparingItem (dadForContextMenu.item, characterScript);
		dadForContextMenu = null;
		previewImage.transform.localScale = uiOff;
	}
	#endregion

	#region Droping
	public void Droping(){
		uiScript.AddToDropZone (dadForContextMenu.item);
		characterScript.Inventory.DropItem (dadForContextMenu.item);
		dadForContextMenu.UpdateItem (null);
		dadForContextMenu = null;
		previewImage.transform.localScale = uiOff;
	}
	#endregion 


}
