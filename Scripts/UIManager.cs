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
	public ArrayList allItemsList;
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
		allItemsList = new ArrayList ();
//		addMenuAction = additionalMenu.GetComponent<AdditionalMenuAction> ();
//		for (int i = 0; i < allItemsList.Count; i++) {
//			print ("all[" + i + "] = " + allItemsList [i]);
//		}
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
		LoadingItemsFromXml ();
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
		print ("List = " + allItemsList.Count);
		foreach (InventoryItem ii in allItemsList) {
			print ("id = " + ii.id + "; ii = " + ii.itemName);
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

		XmlNodeList weapons = xmld.GetElementsByTagName ("weapon");
		foreach (XmlNode weapon in weapons) {
			Debug.Log ("weapon = " + weapon.Name);	
			allItemsList.Add (
				new Weapon (
					int.Parse (weapon.SelectSingleNode ("id").InnerText), //int
					weapon.SelectSingleNode ("name").InnerText, //string
					weapon.SelectSingleNode ("sprite").InnerText, //string
					float.Parse (weapon.SelectSingleNode ("rate").InnerText),//float
					weapon.SelectSingleNode ("shootingmode").InnerText,//string
					int.Parse (weapon.SelectSingleNode ("holder").InnerText),/*int*/ 
					float.Parse (weapon.SelectSingleNode ("price").InnerText), //float
					float.Parse (weapon.SelectSingleNode ("fullreload").InnerText),//float
					float.Parse (weapon.SelectSingleNode ("fastreload").InnerText),//float
					weapon.SelectSingleNode ("ammo").InnerText, //string	
					new Damages (float.Parse (weapon.SelectSingleNode ("minphysicaldamage").InnerText),
						float.Parse (weapon.SelectSingleNode ("maxphysicaldamage").InnerText)),
					new Damages (float.Parse (weapon.SelectSingleNode ("minfiredamage").InnerText),
						float.Parse (weapon.SelectSingleNode ("maxfiredamage").InnerText)),
					new Damages (float.Parse (weapon.SelectSingleNode ("minelectrialdamage").InnerText),
						float.Parse (weapon.SelectSingleNode ("maxelectrialdamage").InnerText)),
					new Damages (float.Parse (weapon.SelectSingleNode ("minenergydamage").InnerText),
						float.Parse (weapon.SelectSingleNode ("maxenergydamage").InnerText)),
					new Damages (float.Parse (weapon.SelectSingleNode ("minpoisondamage").InnerText),
						float.Parse (weapon.SelectSingleNode ("maxpoisondamage").InnerText)),
					new Damages (float.Parse (weapon.SelectSingleNode ("mincorrosivedamage").InnerText),
						float.Parse (weapon.SelectSingleNode ("maxcorrosivedamage").InnerText)),
					new Damages (float.Parse (weapon.SelectSingleNode ("minicedamage").InnerText),
						float.Parse (weapon.SelectSingleNode ("maxicedamage").InnerText)),
					weapon.SelectSingleNode ("type").InnerText));						
		}

		XmlNodeList armors = xmld.GetElementsByTagName ("armor");
		foreach (XmlNode armor in armors) {										
			allItemsList.Add (
				new Armor (
					int.Parse (armor.Attributes ["id"].Value),
					armor.SelectSingleNode ("name").InnerText,
					int.Parse (armor.SelectSingleNode ("price").InnerText),
					armor.SelectSingleNode ("description").InnerText, armor.SelectSingleNode ("type").InnerText));					
		}

		XmlNodeList medkits = xmld.GetElementsByTagName ("medkit");
		foreach (XmlNode medkit in medkits) {	
			switch (medkit.Name) {
			case "FirstTierMedkit":
				allItemsList.Add (new FirstTierMedkit ());
				break;
			case "SecondTierMedkit":
				allItemsList.Add (new SecondTierMedkit ());
				break;
			case "ThirdTierMedkit":
				allItemsList.Add (new ThirdTierMedkit ());
				break;
			case "FourTierMedkit":
				allItemsList.Add (new FourTierMedkit ());
				break;			
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
