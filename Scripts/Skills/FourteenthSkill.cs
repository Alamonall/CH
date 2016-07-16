using UnityEngine;
using System.Collections;

public class FourteenthSkill : Skill {

	public FourteenthSkill(){		
		skillName = "FourteenthSkill";
		icon = Resources.Load (skillName, typeof(Sprite)) as Sprite;
	}

	override public void Use(){}
//	override public Sprite SkillIcon(){
//		return this.icon;
//	}
}