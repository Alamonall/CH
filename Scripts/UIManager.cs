using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

public class UIManager : MonoBehaviour {

	public static UIManager _instanceUIM;

	Character characterScript; 
	CharacterAction characterActionScript;
	GunAction gunActionScript;
	AdditionalMenuAction addMenuAction;

	public Sprite[] inventoryItemIconList;
	public TextAsset itemsXml;
	public ArrayList allItemList;
	public GameObject additionalMenu;
	public GameObject mainMenu;
	public GameObject dropListMenu;
	public GameObject dropZone;
	public GameObject characterSelectionMenu;
	public GameObject multiplayerMenu;

	private GameObject inventoryCellClone; // копия префаба ячейки инвентаря
	public GameObject prefabEmptyInventoryCell; // пребаф пустой ячейки инвентаря
	public GameObject prefabDropItem;
	public bool bAMenu = false; //button additional menu
	public bool bMMenu = false; //button main menu
	public bool bDMenu = false; // drop menu flag
	private bool single = true;
	private string prevMenu;
	public string selectedCharacter;
	public bool gameActive = false; //активна ли сейчас игра
	private Vector3 uiOff = new Vector3 (0,0,0); // вектор для выключения элемента интерфейса
	private Vector3 uiOn = new Vector3(1,1,1);// вектор для включения элемента интерфейса
	public GameObject forDropObject; //ближайший drop-обьект

	void Awake(){
		if (_instanceUIM == null) {
			DontDestroyOnLoad (this.gameObject);
			_instanceUIM = this;
		} else if (_instanceUIM != this)
			Destroy (this.gameObject);
		characterSelectionMenu.transform.localScale = uiOff;
		allItemList = new ArrayList ();
		addMenuAction = additionalMenu.GetComponent<AdditionalMenuAction> ();
	}

	void Update ()
	{	
		
		if (Input.GetKeyDown (KeyCode.Escape))
			WasPressedEscape ();

		if (gameActive) {
			if(characterActionScript == null)
			{
				characterActionScript = CharacterAction._instanceCA;
			}
			if(gunActionScript == null)
			{
				gunActionScript = GunAction._instanceGA;
			}
			if (characterScript == null) {
				characterScript = Character._instanceCharacter;
				if (characterScript != null) {
					switch (selectedCharacter) {
					case "charOne":
						characterScript.SelectSpecialization (new Assault ());
						break;
					case "charTwo":
						characterScript.SelectSpecialization (new Ingeneer ());
						break;
					case "charThree":
						characterScript.SelectSpecialization (new Support ());
						break;
					case "charFour":
						characterScript.SelectSpecialization (new Recon ());
						break;						
					}
					print ("character script DONT NULL");
				}
				if (characterScript == null) {
					print ("character script is null");
					return;
				}
				//TEMP
				characterScript.assaultRiflesAmmo.CurrentAmmo = 500;
				//TEMP
			}
			
			if (characterScript.needUpdate) {
				if (characterScript == null) {
					print ("charScript is null");
					return;
				}
				characterScript.needUpdate = false;
				characterActionScript.UpdateParameters ();
			}

			if (bAMenu && !bMMenu) {
				if (bDMenu)
					dropListMenu.transform.localScale = uiOff;
				additionalMenu.transform.localScale = uiOn;
			}
			if (!bAMenu)
				additionalMenu.transform.localScale = uiOff;
			
			if (!bDMenu)
				dropListMenu.transform.localScale = uiOff;
		}
	}

	#region EscapeWasPressed
	public void WasPressedEscape(){
		if (bAMenu) {
			bAMenu = !bAMenu;
			additionalMenu.transform.localScale = uiOff;
		} else if (bMMenu) {
			mainMenu.transform.localScale = uiOff;
			bMMenu = !bMMenu;
		} else if (bDMenu) {
			bDMenu = !bDMenu;
			dropListMenu.transform.localScale = uiOff;
		} else {
			bMMenu = !bMMenu;
			mainMenu.transform.localScale = uiOn;
		}
	}
	#endregion
	#region ButtonFunctions
	public void buttonMultiplayerGameMenu(){
		single = false;
		prevMenu = "mainMenu";
		GameObject.FindGameObjectWithTag (prevMenu).transform.localScale = uiOff;
		multiplayerMenu.transform.localScale = uiOn;	
	}

	public void buttonSettings()
	{
		//меню настроек
	}

	public void buttonQuit()
	{
		gameActive = false;
		Application.Quit ();	
	}

	public void buttonCharacterSelection(string prev)
	{
		prevMenu = prev;
		characterSelectionMenu.transform.localScale = uiOn;	
		GameObject.FindGameObjectWithTag (prev).transform.localScale = uiOff;	
	}

	public void buttonStartArena()
	{
		SceneManager.LoadScene ("Arena");
	}

	public void buttonBackToPrevMenu()
	{
		multiplayerMenu.transform.localScale = uiOff;
		characterSelectionMenu.transform.localScale = uiOff;
		GameObject.FindGameObjectWithTag (prevMenu).transform.localScale = uiOn;
		prevMenu = "mainMenu";
	}
	#endregion

	#region StartGame
	public void StartGame ()
	{
		if (!single) {
			//проверка на то, готовы ли все игроки
		} else {			
			characterSelectionMenu.transform.localScale = uiOff;
			GameObject.Find ("CharacterInfoPanel").transform.localScale = uiOn;		
			LoadingItemsFromXml ();
			gameActive = true;
			SceneManager.LoadScene (1);
		
		}

	}	
	#endregion

	#region AddToDropZone
	// функция добавления предмета на карту ври выбросе его на карту
	public void AddToDropZone(InventoryItem item){
		if (forDropObject == null) {
			forDropObject = Instantiate (prefabDropItem,
				new Vector3 (CharacterAction._instanceCA.GetCurrentPositionOfPlayer ().x,
					CharacterAction._instanceCA.GetCurrentPositionOfPlayer ().y,
					50), Quaternion.identity) as GameObject;
			forDropObject.transform.SetParent (dropZone.transform);
		}
		print ("drop id = " + item.id);
		forDropObject.GetComponent<BagAction> ().AddDropList (item.id);
	}
	#endregion

	#region GetItemFormAll
	public InventoryItem GetItemFromAll(int id){
		print ("id = " + id + "; list = " + allItemList.Count);
		foreach (InventoryItem ii in allItemList) {
			print (" ii = " + ii.itemName);
			if (ii.id == id) {				
				return ii;
			}
		}
		return null;
	}
	#endregion

	#region LoadingItemsFromXml
	public void LoadingItemsFromXml(){
		XmlDocument xmld = new XmlDocument ();
		xmld.LoadXml (itemsXml.text);
		XmlNodeList itemsList = xmld.GetElementsByTagName ("item");
		foreach (XmlNode itemInfo in itemsList) {
			if(itemInfo.Name == "item")
			{
				switch(itemInfo.SelectSingleNode ("type").InnerText){
				case "Weapon":						
					allItemList.Add (
						new Weapon (int.Parse(itemInfo.Attributes["id"].Value), //int
									itemInfo.SelectSingleNode ("name").InnerText, //string
									float.Parse (itemInfo.SelectSingleNode ("price").InnerText), //float
									inventoryItemIconList [int.Parse (itemInfo.SelectSingleNode ("icon").InnerText)],//sprite
									float.Parse (itemInfo.SelectSingleNode ("damage").InnerText),//float
									float.Parse (itemInfo.SelectSingleNode ("spread").InnerText),//float
									float.Parse (itemInfo.SelectSingleNode ("rate").InnerText),//float
									float.Parse (itemInfo.SelectSingleNode ("fullreload").InnerText),//float
									float.Parse (itemInfo.SelectSingleNode ("fastreload").InnerText),//float
									int.Parse (itemInfo.SelectSingleNode ("ammo").InnerText),/*int*/ 
									itemInfo.SelectSingleNode ("type").InnerText,//string
									itemInfo.SelectSingleNode ("description").InnerText, /*string*/
									itemInfo.SelectSingleNode ("ammotype").InnerText,//string
									itemInfo.SelectSingleNode("shootingmode").InnerText));//string
					break;
				case "Armor":						
					allItemList.Add (
						new Armor (
							int.Parse(itemInfo.Attributes["id"].Value),
							itemInfo.SelectSingleNode ("name").InnerText,
							int.Parse (itemInfo.SelectSingleNode ("price").InnerText),
							inventoryItemIconList [int.Parse (itemInfo.SelectSingleNode ("icon").InnerText)],
							itemInfo.SelectSingleNode ("description").InnerText, itemInfo.SelectSingleNode ("type").InnerText));					
					break;
				}
			}		
		}
	}
	#endregion



}
