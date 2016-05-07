using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DropListAction : MonoBehaviour {

	public static DropListAction _instanceDLA;

	AdditionalMenuAction amaScript;
	UIManager uiScript;

	bool isActive = false; 
	int[] dropList;
	List<GameObject> dropListCells;
	Sprite defaultSprite;

	private Vector3 uiOff = new Vector3 (0,0,0); // вектор для выключения элемента интерфейса
	private Vector3 uiOn = new Vector3(1,1,1);// вектор для включения элемента интерфейса

	int count = 0;
	bool isEmpty = false;
	BagAction myChest; //к какому обьекту принадлежит всплывающее окно в данный момент

	void Start(){
		if (_instanceDLA == null) {
//			DontDestroyOnLoad (this.gameObject);
			_instanceDLA = this;
		} else if (_instanceDLA != this)
			Destroy (this.gameObject);
		uiScript = UIManager._instanceUIM;
		amaScript = AdditionalMenuAction._instanceAMA;
	}

	void Update(){
		if (!uiScript.bDMenu)
			dropList = null;

		if (count == 0) 	
			HideDropList ();		
	}


	public void ToogleActivity(){
		isActive = !isActive;
	}


	//взятие предмета с id в обьекте
	public void TakeItemFromBag(int id){
//		print ("TakeItemFromBag = " + id);
		amaScript.PutItemFromItemList(dropList[id]);
		dropList [id] = 0;
		count--;
		dropListCells [id].GetComponent<Button> ().interactable = false;
		dropListCells [id].GetComponent<Image> ().sprite = defaultSprite;
	}

	// взятие всех предметов в обьекте
	public void TakeAllItemFromBag()
	{
//		print ("Take All");
		for(int i = 0; i < dropList.Length; i++)
			TakeItemFromBag(i);
		HideDropList ();
	}

	public void HideDropList(){
		if (myChest != null && count == 0) {
			myChest.AmEmpty ();
			myChest = null;
		}	
		dropList = null;
		this.transform.localScale = uiOff;
		if (uiScript == null)
			return;
		uiScript.bDMenu = false;
	}

	public void ShowDropList(int[] list, BagAction go){
		dropList = list;
		myChest = go;
		count = dropList.Length;
		if (uiScript == null)
			return;
		if (list.Length == 1) {			
			TakeAllItemFromBag ();
		} else {		
			for (int i = 0; i < dropList.Length; i++) {			
				if (dropList [i] != 0) {
//					print ("dropListCells = " + dropListCells [i].name);
					dropListCells [i].GetComponent<Image> ().sprite = uiScript.GetItemFromAll (dropList [i]).ItemIcon;
					dropListCells [i].GetComponent<Button> ().interactable = true;
				}
			}
			this.transform.localScale = uiOn;
			uiScript.bDMenu = true;
		}

		/// Добавить Обновление графического представления 
	}
}