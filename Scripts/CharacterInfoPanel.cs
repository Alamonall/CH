using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ProgressBar;

public class CharacterInfoPanel : MonoBehaviour {
	
	public GameObject progressBarObject;
	public GameObject primaryWeaponGO;
	public Image weaponIcon;
	//not public
	public Character characterScript;
	public GunAction gunActionScript;
	public CharacterAction characterActionScript;
	public UIManager uiScript;
	public ProgressBarBehaviour progressBarScript;
	public ProgressBarBehaviour hpProgressBarScript;
	public ProgressBarBehaviour spProgressBarScript;

	public Image FirstQuickCell;
	public Image SecondQuickCell;
	public Image ThirdQuickCell;

	string iPrimaryWeapon = "";
	string iSecondaryyWeapon = "";
	float iAmmo = 0;
	float iHolder = 0;
	float iMaxAmmo = 0;

	public Text weaponAmmoPanel;
	public Text weaponReloadPanel;
	public Text weaponMaxAmmoInfoPanel;

	float tempAmmo;
	bool activeWeaponCheck = true;

	void Awake(){
		gunActionScript = GunAction._instanceGA;
		characterScript = Character._instanceCharacter;
		characterActionScript = CharacterAction._instanceCA;
	}

	void Update () {		
		if (gunActionScript == null && uiScript.gameActive)
			gunActionScript = GunAction._instanceGA;
		if (characterScript == null && uiScript.gameActive)
			characterScript = Character._instanceCharacter;
		if(characterActionScript == null && uiScript.gameActive)
			characterActionScript = CharacterAction._instanceCA;
		if (characterScript == null || gunActionScript == null) {
			return;
		} else {
			if (characterActionScript.FirstQuickCellSkill != null)
				FirstQuickCell.sprite = characterActionScript.FirstQuickCellSkill.icon;
			if (characterActionScript.SecondQuickCellSkill != null)
				SecondQuickCell.sprite = characterActionScript.SecondQuickCellSkill.icon;
			if (characterActionScript.ThirdQuickCellSkill != null)
				ThirdQuickCell.sprite = characterActionScript.ThirdQuickCellSkill.icon;

			iPrimaryWeapon = characterScript.PrimaryWeapon.itemName;
			iSecondaryyWeapon = characterScript.SecondaryWeapon.itemName;
			iHolder = gunActionScript.holder;
			iAmmo = gunActionScript.ammo;
			iMaxAmmo = gunActionScript.maxAmmo;
			if (characterScript.activeWeapon) {
				weaponIcon.sprite = characterScript.PrimaryWeapon.ItemIcon;	
				weaponAmmoPanel.text = "Primary: " + iHolder + " / " + iAmmo;
			} else {
				weaponIcon.sprite = characterScript.SecondaryWeapon.ItemIcon;
				weaponAmmoPanel.text = "Secondary: " + iHolder + " / " + iAmmo;
			}
			weaponMaxAmmoInfoPanel.text = "Max: " + iMaxAmmo;

			if (!characterScript.activeWeapon && !activeWeaponCheck) {
				primaryWeaponGO.GetComponent<Image> ().color = Color.white;
				activeWeaponCheck = !activeWeaponCheck;
			}
			if (!gunActionScript.overReload)
				weaponReloadPanel.text = " Reload...";
			else
				weaponReloadPanel.text = " ";

			SetValueForProgressBar (progressBarScript, characterScript.currentExperience, characterScript.nextLevelExperience);
			SetValueForProgressBar (hpProgressBarScript, characterScript.currentHealthPoints, characterScript.maxHealtPoints);
		}
	}

	public void SetValueForProgressBar(ProgressBarBehaviour pb, float current, float max){
		pb.SetMaxValue (current, max);
		pb.SetFillerSizeAsPercentage (max);
		pb.Value = (current / (max/100));
	}
}
