using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterMenuAction : MonoBehaviour {

	public static CharacterMenuAction _instanceCMA;

	public Character characterScript;
	public UIManager uiScript;
	public AdditionalMenuAction amScript;
	public CharacterAction caScript;

	public Text skillPointCount;
	public Text levelCount;

	public SkillAction[] allSkills;
	public ArrayList takenSkills;
	public SkillAction acceptSkill;
	public ArrayList acceptSkills;

	public int LEVEL = -1;

	void Awake ()
	{
		if (_instanceCMA == null) {
			DontDestroyOnLoad (this.gameObject);
			_instanceCMA = this;
		} else if (_instanceCMA != this)
			Destroy (this.gameObject);
		characterScript = uiScript.characterScript;
//		caScript = CharacterAction._instanceCA;
	}

	void Update()
	{
		LEVEL = characterScript.currentLevel;
		if (uiScript.gameActive) {
			skillPointCount.text = "" + characterScript.currentSkillPoints;
			levelCount.text = "" + characterScript.currentLevel;	
		}		
	}

	public void CheckOnAvalaible(){		
		foreach (SkillAction s in allSkills) {
//			print ("checkonavalaible = " + LEVEL);
			if (s.openingLevel <= LEVEL) {
//				print ("you should go");
				if (s.state == -1) {
//					print ("something");
					s.Avalaible ();
				} 
			}
		}
	}

	public void Clicked(SkillAction self){	
		print ("Clicked on " + self.name);	
		if (self.state == 1) {
//			print ("state 0!");
			self.Avalaible ();
			DeleteFromConfirm (self);
			characterScript.currentSkillPoints++;
		} else if (self.state == 0 && characterScript.currentSkillPoints > 0) {
//			print ("state 1");
			self.Taken ();
			AddForConfirm (self);
			characterScript.currentSkillPoints--;
		}
//		else if (self.state == 2) {
//			
//		}
		CheckOnAvalaible ();
	}

	public bool GrowthHandler(string dir){
		if (dir.Equals ("Down")) {
			characterScript.currentSkillPoints++;
//			print ("Down");
			return true;
		}
		if (dir.Equals ("Up") && characterScript.currentSkillPoints > 0) {
			characterScript.currentSkillPoints--;
//			print ("Up!");
			return true;
		} 	
		print ("false");
		return false;	
	}


	public void AddForConfirm(SkillAction skill){
		if(takenSkills == null)
			takenSkills = new ArrayList ();
		takenSkills.Add (skill);
	}

	public void DeleteFromConfirm(SkillAction skill){
		takenSkills.Remove (skill);
	}

	public void ConfirmedAll(){
		foreach (SkillAction s in takenSkills)
			s.Confirmed ();
	}

	public void SetFirstQuickCell(GameObject self){
		if (caScript == null) {
			Debug.Log ("caScript us null");
			caScript = CharacterAction._instanceCA;
		}
		caScript.FirstQuickCellSkill = characterScript.GetSkill(self.GetComponent<PreviewScript>().tempNumberSkill);
	}

	public void SetSecondQuickCell(GameObject self){
		caScript.SecondQuickCellSkill = characterScript.GetSkill(self.GetComponent<PreviewScript>().tempNumberSkill);
	}

	public void SetThirdQuickCell(GameObject self){
		caScript.ThirdQuickCellSkill = characterScript.GetSkill(self.GetComponent<PreviewScript>().tempNumberSkill);
	}
}
