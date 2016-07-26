using UnityEngine;
using System.Collections;

public class SixteenthSkill : Skill {

	public SixteenthSkill(){		
		skillName = "SixteenthSkill";
		icon = Resources.Load (skillName, typeof(Sprite)) as Sprite;
	}

	override public void Use(){}
//	override public Sprite SkillIcon(){
//		return this.icon;
//	}
}

