using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System;

public class UIManager : MonoBehaviour {

	public static UIManager _instanceUIM;

	public Character characterScript; 
	public CharacterAction characterActionScript;
	public GunAction gunActionScript;
	public AdditionalMenuAction addMenuAction;
	public BoardManager bmScript;
	public CharacterMenuAction characterMenuActionScript;

	public TextAsset itemsXml;
	public ArrayList allItemList;
	public GameObject dropZone;

	private GameObject inventoryCellClone; // копия префаба ячейки инвентаря
	public GameObject prefabEmptyInventoryCell; // пребаф пустой ячейки инвентаря
	public GameObject prefabDropItem;

	private bool single = true;
	private string prevMenu;
	public string selectedCharacter;
	public bool gameActive = false; //активна ли сейчас игра
	public Vector3 uiOff = new Vector3 (0,0,0); // вектор для выключения элемента интерфейса
	public Vector3 uiOn = new Vector3(1,1,1);// вектор для включения элемента интерфейса
	public GameObject forDropObject; //ближайший drop-обьект

	void Awake(){
		if (_instanceUIM == null) {
			DontDestroyOnLoad (this.gameObject);
			_instanceUIM = this;
		} else if (_instanceUIM != this)
			Destroy (this.gameObject);
		bmScript = BoardManager._instanceBM;
		characterMenuActionScript = CharacterMenuAction._instanceCMA;
		allItemList = new ArrayList ();
//		addMenuAction = additionalMenu.GetComponent<AdditionalMenuAction> ();


		LoadingItemsFromXml ();
		for (int i = 0; i < allItemList.Count; i++) {
//			print ("all[" + i + "] = " + allItemList [i]);
		}
	}

	void Update (){	
		if (gameActive){
			InGameMenuActions ();
		}
	}

	#region InGameMenuActions
	public void InGameMenuActions(){
		//
		if(characterActionScript == null){
			characterActionScript = CharacterAction._instanceCA;
		}
		if(gunActionScript == null){
			gunActionScript = GunAction._instanceGA;
		}
		//

		if (characterScript.needUpdate) {			
			try{
				if (characterActionScript == null) {					
					Debug.Log ("char Action is null");
					return;
				}
				characterScript.needUpdate = false;
				characterActionScript.UpdateParameters ();
			}catch (Exception exc){
				Debug.LogException (exc, this);
			}
		}
	}
	#endregion



	public void buttonContinue()
	{
		
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

	public void buttonBackToPrevMenu()
	{
		GameObject.FindGameObjectWithTag (prevMenu).transform.localScale = uiOn;
		prevMenu = "mainMenu";
	}

	#region StartGame
	public void StartGame ()
	{
		if (!single) {
			//проверка на то, готовы ли все игроки
		} else {			
			addMenuAction.mainMenu.transform.localScale = uiOff;
			addMenuAction.bMMenu = false;
			//Оптимихация
			GameObject.Find ("PermanentGUI").transform.localScale = uiOn;		

			gameActive = true;
			SceneManager.LoadScene (1);	
			BoardManager._instanceBM.SetupScene (1);

			characterScript = Character._instanceCharacter;
			gunActionScript = GunAction._instanceGA;
			characterActionScript = CharacterAction._instanceCA;
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
//		print ("drop id = " + item.id);
		forDropObject.GetComponent<BagAction> ().AddDropList (item);
	}
	#endregion

	#region GetItemFromAll
	public InventoryItem GetItemFromAll(int id){
//		print ("List = " + allItemList.Count);
		foreach (InventoryItem ii in allItemList) {
//			print ("id = " + ii.id + "; ii = " + ii.itemName);
			if (ii.id == id) {	
				return ii;
			}
		}
		return null;
	}
	#endregion

	#region LoadingItemsFromXml
	public void LoadingItemsFromXml(){
//		print ("LoadingItemsFromXml");
		XmlDocument xmld = new XmlDocument ();
		xmld.LoadXml (itemsXml.text);
		XmlNodeList itemsList = xmld.GetElementsByTagName ("item");
		foreach (XmlNode itemInfo in itemsList) {
//			print ("allitemlist = " + allItemList.Count);
			if(itemInfo.Name == "item")
			{
				switch(itemInfo.SelectSingleNode ("type").InnerText){
				case "Weapon":						
					allItemList.Add (
						new Weapon (int.Parse(itemInfo.Attributes["id"].Value), //int
									itemInfo.SelectSingleNode ("name").InnerText, //string
									float.Parse (itemInfo.SelectSingleNode ("price").InnerText), //float
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
							itemInfo.SelectSingleNode ("description").InnerText, itemInfo.SelectSingleNode ("type").InnerText));					
					break;
				case "FirstTierMedkit":
					allItemList.Add (new FirstTierMedkit ());
					break;
				case "SecondTierMedkit":
					allItemList.Add (new SecondTierMedkit ());
					break;
				case "ThirdTierMedkit":
					allItemList.Add (new ThirdTierMedkit ());
					break;
				case "FourTierMedkit":
					allItemList.Add (new FourTierMedkit ());
					break;			
				}
			}		
		}
	}
	#endregion

	#region CharGetLevel
	public void CharGetLevel(Character self){
		if (characterMenuActionScript == null) {
			characterMenuActionScript = CharacterMenuAction._instanceCMA;
			Debug.Log ("character menu action script is null = " + characterMenuActionScript);
			return;
		}
		characterMenuActionScript.CheckOnAvalaible ();
	}
	#endregion

}
