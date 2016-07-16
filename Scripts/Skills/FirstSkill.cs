using UnityEngine;
using System.Collections;

public class FirstSkill : Skill {

	public FirstSkill(){
		skillName = "FirstSkill";
		icon = Resources.Load (skillName, typeof(Sprite)) as Sprite;
	}

	override public void Use(){
		
	}

//	override public Sprite SkillIcon(){
//		return this.icon;
//	}
}
