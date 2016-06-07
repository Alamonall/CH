using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterMenuAction : MonoBehaviour {

	public static CharacterMenuAction _instanceCMA;

	public Character characterScript;
	public UIManager uiScript;

	public Text skillPointCount;
	public Text levelCount;

	public Toggle[] allToggles;
	public Toggle[] tgBefore5;
	public Toggle[] tgBefore10;
	public Toggle[] tgBefore15;
	public Toggle[] tgBefore20;

	void Start ()
	{
		if (_instanceCMA == null)
			_instanceCMA = this;
		else if (_instanceCMA != this) {
			print ("CMA not alone");
		}
		characterScript = Character._instanceCharacter;
		foreach (Toggle t in allToggles) {
			t.onValueChanged.AddListener (delegate {TakeSkillPoint(t);});
		}
	}

	void Update()
	{
		if (uiScript.gameActive) {
			skillPointCount.text = "" + characterScript.currentSkillPoints;
			levelCount.text = "" + characterScript.currentLevel;	
			if (characterScript.currentSkillPoints == 0) {
				DeactivateAbilitys (allToggles);
			}
		}	
	}

	public void DeactivateAbilitys(Toggle[] ta){
		foreach (Toggle t in ta) {
			t.interactable = false;
		}
	}

	public void CharacterGetLevel(Character self){
//		print ("CharacterGetLvl! " + characterScript.currentLevel + "!");
		ActivateAbilitys (tgBefore5);
		if (self.currentLevel >= 5) {
			ActivateAbilitys (tgBefore10);
//			print ("Ability for 5 activated!");
		}
		if (self.currentLevel >= 10) {
			ActivateAbilitys (tgBefore15);
//			print ("Ability for 10 activated!");
		}
		if (self.currentLevel >= 15) {
			ActivateAbilitys (tgBefore20);
//			print ("Ability for 15 activated!");
		}
	}

	void ActivateAbilitys(Toggle[] ta){
		foreach (Toggle t in ta) {
			t.interactable = true;
		}
	}

	public void TakeSkillPoint(Toggle self){
		//проверка на сохраненность
		print("Take a skill" + self.name);
		if (!self.isOn) {			
			characterScript.currentSkillPoints++;			
		} 
		if (characterScript.currentSkillPoints == 0) {
			if (self.isOn) {
				//проверка
				print ("WOW!");
				self.isOn = false;
			}
		} else {
			if (self.isOn)
				characterScript.currentSkillPoints--;		
		}


	}
	
}
