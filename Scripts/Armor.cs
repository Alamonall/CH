using UnityEngine;
using System.Collections;

public class Armor : InventoryItem
{
	public string description;

	public float physicalResistance;
	public float fireResistance;
	public float electricResistance;
	public float plasmaResistance;
	public float poisonResistance;
	public float corrosionResistance;
	public float energyResistance;
	//public ArmoryEffect[] armoryEffect;
	
	public Armor(int id, string name, float price, Sprite icon, string description, string type){
		this.id = id;
		this.itemName = name;
		this.itemPrice = price;
		this.itemIcon = icon;
		this.description = description;
		this.type = type;
	}

	public Armor()
	{
		this.itemName = "empty";
	}
}

