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
	public Image previewImage;
	Vector3 uiOff = new Vector3 (0,0,0); // вектор для выключения элемента интерфейса
	Vector3 uiOn = new Vector3(1,1,1);// вектор для включения элемента интерфейса

	public GameObject[] inventoryCellList; //список предметов в инвентаре
	public InventoryItem[] inventoryItemList; //список предметов в инвентаре для графического представления

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
				bSkills = false;
				bMap = false;
				bJournal = false;
			}
		}
		else if (Input.GetKeyDown (KeyCode.P)) { 
			bSkills = !bSkills;
			if (!bSkills)
				UIManager._instanceUIM.bAMenu = false;
			else {
				SwitchUIObjects ("bSkills");
				bMap = false;
				bJournal = false;
				bInventory = false;
			}
		}
		else if (Input.GetKeyDown (KeyCode.J)) { 
			bJournal = !bJournal;
			if (!bJournal)
				UIManager._instanceUIM.bAMenu = false;
			else {
				SwitchUIObjects ("bJournal");
				bSkills = false;
				bMap = false;
				bInventory = false;
			}
		}
		else if (Input.GetKeyDown (KeyCode.M)) { 
			bMap = !bMap;
			if (!bMap)				
				UIManager._instanceUIM.bAMenu = false;
			else {
				SwitchUIObjects ("bMap");
				bSkills = false;
				bInventory = false;
				bJournal = false;
			}
		}
	}


	public void WhichButtonIsPressed(Button button){
		SwitchUIObjects (button.name);
	}

	public void SwitchUIObjects(string buttonName){
		switch (buttonName) {
		case "bInventory":
			ShowUIObject (pInventory);
			break;
		case "bSkills":
			ShowUIObject (pSkills);
			break;
		case "bJournal":
			ShowUIObject (pJournal);
			break;
		case "bMap":
			ShowUIObject (pMap);
			break;
		}
	}

	#region ShowUIObject
	public void ShowUIObject(GameObject uiObj){	
		if (!UIManager._instanceUIM.bAMenu)
			UIManager._instanceUIM.bAMenu = true;
		if(prevActivePanel != null)			
			prevActivePanel.transform.localScale = uiOff;
		uiObj.transform.localScale = uiOn;
		prevActivePanel = uiObj;
	}
	#endregion

	#region ShowPreview
	public void ShowPreview(int id, bool inInventory, GameObject cell){
		if (inInventory) {
			print ("inventoryItemList null = " + inventoryItemList [id]);
			if (inventoryItemList [id] == null)
				return;
			InventoryItem temp = uiScript.GetItemFromAll (inventoryItemList [id].id);
			print ("Show Preview! = " + temp.itemName);
			if (characterScript == null) {
				print ("char is null");
				return;
			}
			comparingMenu.ComparingItem (temp, characterScript);
//			previewInventoryItem.transform.localScale = uiOn;
//			print (idCell + "; localPosition cellX = " + cell.transform.localPosition.x + "; cellY = " + cell.transform.localPosition.y);
//			print (idCell + "; position cellX = " + cell.transform.position.x + "; cellY = " + cell.transform.position.y);
//			previewInventoryItem.transform.localPosition = new Vector3 (cell.transform.localPosition.x + 225, cell.transform.localPosition.y - 323, 0);
//			previewInventoryItemText.text += temp.itemName + "; " + temp.itemPrice;
		}
	}
	#endregion


	#region SetupInventory
	public void SetupInventory (){		
		for (int i = 0; i < characterScript.Inventory.itemList.Length; i++) {		
//			Debug.Log ("ItemList[" + i + "] = " + itemList [i]);
			if (characterScript.Inventory.itemList [i] != null) {
//				Debug.Log("Name = " + itemList[i].itemName + "; Icon = " + itemList [i].ItemIcon);
				inventoryCellList [i].GetComponent<Image> ().sprite = characterScript.Inventory.itemList [i].ItemIcon;	
			}
		}	
	}
	#endregion

	#region UpdateInventoryCellList
	public void UpdateInventoryCellList(){
//		print ("UpdateInventoryCellList");
		if (!characterScript.PrimaryWeapon.itemName.Equals ("hands")) {
//			print ("Sprite Primary Weapon is Upadate");
			GameObject.Find ("PrimaryWeaponCell").GetComponent<Image> ().sprite = characterScript.PrimaryWeapon.ItemIcon;
		} 
		if (!characterScript.SecondaryWeapon.itemName.Equals ("hands")) {
//			print ("Sprite Secondary Weapon is Upadate");
			GameObject.Find ("SecondaryWeaponCell").GetComponent<Image> ().sprite = characterScript.SecondaryWeapon.ItemIcon;
		}
		if (!characterScript.Armor.itemName.Equals ("empty")) {
			GameObject.Find ("ArmorCell").GetComponent<Image> ().sprite = characterScript.Armor.ItemIcon;
		}
		if (!characterScript.Granade.itemName.Equals ("empty")) {
			GameObject.Find ("GranadeCell").GetComponent<Image> ().sprite = characterScript.Granade.ItemIcon;
		}
		if (!characterScript.Medkit.itemName.Equals ("empty")) {
			GameObject.Find ("MedkitCell").GetComponent<Image> ().sprite = characterScript.Medkit.ItemIcon;
		}
		for (int i = 0; i < characterScript.Inventory.ItemList.Length; i++) {
			if (characterScript.Inventory.ItemList [i] != null) {
//				print (characterScript.Inventory.itemList [i].itemName);
				if (characterScript.Inventory.itemList [i].ItemIcon == null) {
					print ("item icon is null");
					return;
				}
				inventoryCellList[i].GetComponent<Image> ().sprite = characterScript.Inventory.itemList [i].ItemIcon;	
//				Debug.Log (" inventoryCellList = " + inventoryCellList [i].name);
			}
		}
		inventoryItemList = characterScript.Inventory.ItemList;
	}
	#endregion


	#region PutItemFromItemList
	public void PutItemFromItemList(int id)
	{	
		InventoryItem item = uiScript.GetItemFromAll (id);
		switch (item.Type) {
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

	#region Swap Items
	public bool SwapItem(GameObject itemOne, GameObject itemTwo)
	{
		string typeOfFirstCell = "";
		bool onPrimWeapon = true;
		int compare, compareTwo;
//		print("itemOne = " + itemOne.name);
//		print ("itemTwo = " + itemTwo.name);

		//выброс предмета
		if (itemOne.name == "DropCell") {
			int two = itemTwo.GetComponent<DragAndDrop> ().idCell;
			switch (itemTwo.name) {
			case "InventoryItemCell":
//				print ("1");
				uiScript.AddToDropZone (inventoryItemList [two]);
				inventoryItemList [two] = null;
				break;
			case "PrimaryWeaponCell":
//				print ("2");
				uiScript.AddToDropZone (characterScript.PrimaryWeapon);
				characterScript.PrimaryWeapon = new Weapon (); //ЗАГЛУШКА 
				break;
			case "SecondaryWeaponCell": 
//				print ("3");
				uiScript.AddToDropZone (characterScript.SecondaryWeapon);
				characterScript.SecondaryWeapon = new Weapon (); //ЗАГЛУШКА
				break;
			case "ArmorCell":
//				print ("4");
				uiScript.AddToDropZone (characterScript.Armor);
				characterScript.Armor =  new Armor(); //ЗАГЛУШКА
				break;
			case "GranadeCell":
//				print ("5");
				uiScript.AddToDropZone (characterScript.Granade);
				characterScript.Granade =  new Granade(); //ЗАГЛУШКА
				break;
			case "MedkitCell":
//				print ("6");
				uiScript.AddToDropZone (characterScript.Medkit);
				characterScript.Medkit =  new Medkit(); //ЗАГЛУШКА
				break;
			}
			return true;
		}

		// с оружия 1/2 на оружие 2/1
		if (itemOne.name == "PrimaryWeaponCell" && itemTwo.name == "SecondaryWeaponCell" 
			|| itemOne.name == "SecondaryWeaponCell" && itemTwo.name == "PrimaryWeaponCell") 
		{
			Weapon temp = characterScript.PrimaryWeapon;
			characterScript.PrimaryWeapon = characterScript.SecondaryWeapon;
			characterScript.SecondaryWeapon = temp;
			return true;
		}

		//с инвентаря в инвентарь
		if (itemOne.name == "InventoryItemCell" && itemTwo.name == "InventoryItemCell") 
		{
			int one = itemOne.GetComponent<DragAndDrop> ().idCell, two = itemTwo.GetComponent<DragAndDrop> ().idCell;
//			Debug.Log ("One = " + one + "; Two = " + two);
			InventoryItem temp = inventoryItemList [one];
			inventoryItemList [one] = inventoryItemList [two];
			inventoryItemList [two] = temp;
			return true;
		}

		//с персонажа в инвентарь
		if (!itemTwo.name.Equals ("InventoryItemCell") && itemOne.name.Equals ("InventoryItemCell")) {		
			print ("с персонажа в инвентарь");
			compare = itemOne.GetComponent<DragAndDrop> ().idCell;
			switch (itemTwo.name) {
			case "PrimaryWeaponCell":
				inventoryItemList [compare] = characterScript.PrimaryWeapon;
				characterScript.PrimaryWeapon = new Weapon (); //ЗАГЛУШКА 
				break;
			case "SecondaryWeaponCell": 
				inventoryItemList [compare] = characterScript.SecondaryWeapon;
				characterScript.SecondaryWeapon = new Weapon (); //ЗАГЛУШКА
				break;
			case "ArmorCell":
				inventoryItemList [compare] = characterScript.Armor;
				characterScript.Armor =  new Armor(); //ЗАГЛУШКА
				break;
			case "GranadeCell":
				inventoryItemList [compare] = characterScript.Granade;
				characterScript.Granade =  new Granade(); //ЗАГЛУШКА
				break;
			case "MedkitCell":
				inventoryItemList [compare] = characterScript.Medkit;
				characterScript.Medkit =  new Medkit(); //ЗАГЛУШКА
				break;
			}
			return true;
		}

		//персонаж <- инвентарь
		if (!itemOne.name.Equals ("InventoryItemCell") && itemTwo.name.Equals ("InventoryItemCell")) {
			print ("//персонаж <- инвентарь");
			compare = itemTwo.GetComponent<DragAndDrop> ().idCell;
			typeOfFirstCell = GetCharCellType (itemOne.name);
			if (inventoryItemList [compare].Type.Equals (typeOfFirstCell)) {
				if (itemOne.name.Equals ("SecondaryWeaponCell"))
					onPrimWeapon = false;
				switch (typeOfFirstCell) {
				case "Weapon":
					if (onPrimWeapon) {
						Weapon temp = (Weapon)inventoryItemList [compare];
						inventoryItemList [compare] = characterScript.PrimaryWeapon;
						characterScript.PrimaryWeapon = temp;
					} else {
						Weapon temp = (Weapon)inventoryItemList [compare];
						inventoryItemList [compare] = characterScript.SecondaryWeapon;
						characterScript.SecondaryWeapon = temp;
					}
					characterScript.activeWeapon = onPrimWeapon;
					break;
				case "Armor":
					Armor temp = (Armor)inventoryItemList [compare];
					inventoryItemList [compare] = characterScript.Armor;
					characterScript.Armor = temp;
					break;
				case "Granade":
					break;
				case "Medkit":
					break;
				}
				onPrimWeapon = true;
				return true;
			} 
		}

		//оружие1 <-> оружие2
		if (!itemOne.name.Equals ("InventoryItemCell") && !itemTwo.name.Equals ("InventoryItemCell")) {
			if (GetCharCellType (itemTwo.name).Equals (GetCharCellType (itemOne.name))) {
				if (itemOne.name.Equals ("SecondaryWeaponCell"))
					onPrimWeapon = false;
				Weapon tempII = characterScript.PrimaryWeapon;
				characterScript.PrimaryWeapon = characterScript.SecondaryWeapon;
				characterScript.SecondaryWeapon = tempII;
				characterScript.activeWeapon = onPrimWeapon;
				onPrimWeapon = true;
				return true;
			}
		}
		return false;
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
}
