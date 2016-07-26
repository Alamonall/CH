using System;
using UnityEngine;
using System.Collections.Generic;

public class Inventory
{
	[SerializeField]
	public InventoryItem[] itemList;  //список предметов в инвентаре
	public int currSize;    // количество предметов в инвентаре
	public int maxSize;     // максимально возможно предметов в инвентаре
	public string itemFiltr;

	public Inventory(int size){
		itemList = new InventoryItem[size];
		maxSize = size;
		currSize = 0;
	}

	public void PutItem(InventoryItem item){
		currSize++;
		if (itemList == null) {
//			Debug.Log ("Item List is null");
			return;
		}
		for(int i = 0; i < maxSize; i++){
			if (itemList[i] == null) {
				itemList[i] = item;
//				Debug.Log ("Inventory take to " + item.itemName);
				return;
			}	
		}
	}

	//запрашиваем количество выбрасываемых предметов
	public void DropItem(InventoryItem item){
		Debug.Log ("Finding " + item.itemName + "in inventory with size = " + currSize);
		if (itemList == null) {
			Debug.Log ("itemList is null");
			return;
		}
		for (int i = 0; i < currSize; i++) {
			Debug.Log ("Item = " + itemList [i].itemName);
			if (itemList [i] != null && itemList [i].itemName.Equals (item.itemName)) {
				Debug.Log ("Removing " + item.itemName + " from the inventory");
				itemList [i] = null;
				break;
			}
		}
	}

	public InventoryItem[] ItemList {
		get {
			return itemList;
		}
	}
}

