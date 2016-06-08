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
	}

	void Update()
	{
		if (uiScript.gameActive) {
			skillPointCount.text = "" + characterScript.currentSkillPoints;
			levelCount.text = "" + characterScript.currentLevel;	
		}	
	}

	//Деактивирует ячейки умений в массиве ta
	public void DeactivateAbilitys(Toggle[] ta){
		foreach (Toggle t in ta) {
			if (t != null)
				if(t.interactable && !t.isOn)
					t.interactable = false;
		}
	}

	//Проверяет сколько умений может активировать полльзователь
	public void CheckAbillitysForAccess(){
		if (characterScript.currentSkillPoints == 0)
			DeactivateAbilitys (allToggles);
		else
			CharacterGetLevel (characterScript);
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

	//Активирует ячейки умений в массиве ta
	void ActivateAbilitys(Toggle[] ta){
		foreach (Toggle t in ta) {
			t.interactable = true;
		}
	}

	public void TakeSkillPoint(Toggle self){
		//проверка на сохраненность
//		print("Take a skill" + self.name);
		if (!self.isOn) {			
			characterScript.currentSkillPoints++;		 
		} else{
			characterScript.currentSkillPoints--;		
		}
		CheckAbillitysForAccess ();
	}

	public void SetSkill(){		
		for (int i = 0; i < allToggles.Length; i++) {
			if (allToggles [i].isOn) {
//				allToggles[i].image.overrideSprite = new Sprite(
				//запись в персонажа, что данный скилл подтвержден
			}
		}
	}

	
}
