using UnityEngine;
using System.Collections;

public class FifteenthSkill : Skill {

	public FifteenthSkill(){		
		skillName = "FifteenthSkill";
		icon = Resources.Load (skillName, typeof(Sprite)) as Sprite;
	}

	override public void Use(){}
//	override public Sprite SkillIcon(){
//		return this.icon;
//	}
}
