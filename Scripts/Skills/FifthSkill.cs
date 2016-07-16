using UnityEngine;
using System.Collections;

public class FifthSkill : Skill {

	public FifthSkill(){		
		skillName = "FifthSkill";
		icon = Resources.Load (skillName, typeof(Sprite)) as Sprite;
	}
//	override public Sprite SkillIcon(){
//		return this.icon;
//	}
	override public void Use(){}
}