using UnityEngine;
using System.Collections;

abstract public class Ammo : InventoryItem
{
	public int modificatorAmmo;
	abstract public void Use ();
	abstract public void Set ();
}

