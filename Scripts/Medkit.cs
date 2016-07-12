using UnityEngine;
using System.Collections.Generic;

abstract public class Medkit : InventoryItem
{
	public float regenHp;
	abstract public void Use ();
	abstract public void Set ();
}


