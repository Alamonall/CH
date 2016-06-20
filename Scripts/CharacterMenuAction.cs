using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterMenuAction : MonoBehaviour {

	public static CharacterMenuAction _instanceCMA;

	public Character characterScript;
	public UIManager uiScript;
	public AdditionalMenuAction amScript;

	public Text skillPointCount;
	public Text levelCount;

	public Skill[] allSkills;
	public ArrayList takenSkills;
	public Skill acceptSkill;
	public Skill[] acceptSkills;

	public int LEVEL = -1;

	void AWake ()
	{
		if (_instanceCMA == null)
			_instanceCMA = this;
		else if (_instanceCMA != this) {
			print ("CMA not alone");
		}
		characterScript = uiScript.characterScript;

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
		foreach (Skill s in allSkills) {
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

	public void Clicked(Skill self){	
//		print ("self name = " + self.name);	
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
		CheckOnAvalaible ();
	}


	public void AddForConfirm(Skill skill){
		if(takenSkills == null)
			takenSkills = new ArrayList ();
		takenSkills.Add (skill);
	}

	public void DeleteFromConfirm(Skill skill){
		takenSkills.Remove (skill);
	}

	public void ConfirmedAll(){
		foreach (Skill s in takenSkills)
			s.Confirmed ();
		takenSkills.Clear ();
	}	
}
