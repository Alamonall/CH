using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryItem {
	public int id;
	public string itemName; //название предмета
	public float itemPrice; //цена предмета
	public Sprite itemIcon;
	public string type;

	public InventoryItem(Sprite sprite){
		this.itemIcon = sprite;
	}

	public InventoryItem(){
		
	}

	public Sprite ItemIcon {
		get {
			return itemIcon;
		}
		set {
			itemIcon = value;
		}
	}

	public string Type {
		get {
			return type;
		}
		set {
			type = value;
		}
	}
}


