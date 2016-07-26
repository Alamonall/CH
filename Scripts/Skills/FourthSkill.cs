using UnityEngine;
using System.Collections;

public class FourthSkill : Skill {

	public FourthSkill(){		
		skillName = "FourthSkill";
		icon = Resources.Load (skillName, typeof(Sprite)) as Sprite;
	}

	override public void Use(){}
//	override public Sprite SkillIcon(){
//		return this.icon;
//	}
}
