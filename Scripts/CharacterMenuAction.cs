using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterMenuAction : MonoBehaviour {

	public Character characterScript;
	public UIManager uiScript;

	public Text skillPointCount;
	public Text levelCount;

	public ToggleGroup tgBefore5;
	public ToggleGroup tgBefore10;
	public ToggleGroup tgBefore15;
	public ToggleGroup tgBefore20;

	void Update()
	{
		if (characterScript == null && uiScript.gameActive)
			characterScript = Character._instanceCharacter;
		if (uiScript.gameActive) {
			skillPointCount.text = "" + characterScript.currentSkillPoints;
			levelCount.text = "" + characterScript.currentLevel;	
		if(characterScript.currentLevel >= 5)
			tgBefore10.allowSwitchOff = true;
		if(characterScript.currentLevel >= 10)
			tgBefore15.allowSwitchOff = true;
		if(characterScript.currentLevel >= 15)
			tgBefore20.allowSwitchOff = true;
		}	
	}
}
