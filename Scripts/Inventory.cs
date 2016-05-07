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
		if (itemList == null) {
//			Debug.Log ("Item List is null");
			return;
		}
		for(int i = 0; i < maxSize; i++){
			if (itemList[i] == null) {
				itemList[i] = item;
//				Debug.Log ("Inventory take to " + item.itemName);
				currSize++;
				return;
			}	
		}
	}

	//запрашиваем количество выбрасываемых предметов
	public void DropItem(InventoryItem item){
		if (itemList == null)
			return;
		for(int i = 0; i < currSize; i++)
			if (itemList[i].itemName.Equals (item.itemName)) {
				itemList[i] = null;
				currSize--;
				break;
			}
	}

	public InventoryItem[] ItemList {
		get {
			return itemList;
		}
	}
}

