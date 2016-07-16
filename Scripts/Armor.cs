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
	
	public Armor(int id, string name, float price, string description, string type){
		this.id = id;
		this.itemName = name;
		this.itemPrice = price;
		this.itemIcon = Resources.Load(name,typeof(Sprite)) as Sprite;
		this.description = description;
		this.type = type;
	}

	public Armor()
	{
		this.itemName = "empty";
	}
}

