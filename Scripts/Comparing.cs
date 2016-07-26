using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Comparing : MonoBehaviour {

	public GameObject CompareWeapon;

	public Text CompareName;
	public Text PWName;
	public Text SWName;

	public Text CompareDamage;
	public Text CompareAmmo;
	public Text CompareFullReload;
	public Text CompareFastReload;

	public Text PWDamage;
	public Text PWAmmo;
	public Text PWFullReload;
	public Text PWFastReload;

	public Text SWDamage;
	public Text SWAmmo;
	public Text SWFullReload;
	public Text SWFastReload;

	public void ComparingItem(InventoryItem item2, Character script){
//		print ("Type of weapon = " + item2.type);
		switch(item2.Type){
		case "Weapon":
			Weapon item = item2 as Weapon;
			CompareName.text = item.itemName;
			PWName.text = "PW: " + script.PrimaryWeapon.itemName;
			SWName.text = "SW: " + script.SecondaryWeapon.itemName;
			CompareAmmo.text = " " + item.Ammo;
			CompareFullReload.text = " " + item.FullReload;
			CompareFastReload.text = " " + item.FastReload;
			SetCompare (item, script.PrimaryWeapon, PWDamage, PWAmmo, PWFullReload, PWFastReload);
			SetCompare (item, script.SecondaryWeapon, SWDamage, SWAmmo, SWFullReload, SWFastReload);
			ShowCompareMenu (CompareWeapon);
			break;
		case "Armor":
			// доработать
			Armor ar = item2 as Armor;
			break;
		}
	}

	void SetCompare(Weapon item, Weapon weap, Text damage, Text ammo, Text full, Text fast){
		SetCompareTwo (ammo, item.ammo, weap.ammo);	
	}

	void SetCompareTwo(Text t, float one, float two){
		if (one > two)
			t.text = " - " + (one - two);
		if (one < two)
			t.text = " + " + (two - one);
		if (one == two)
			t.text = " equals ";
	}

	void HideCompareMenu(GameObject menu){
		menu.transform.localScale = new Vector3 (0, 0, 0);
	}

	void ShowCompareMenu(GameObject menu){
		menu.transform.localScale = new Vector3 (1, 1, 1);
	}
}
