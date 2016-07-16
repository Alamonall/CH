using UnityEngine;
using System.Collections;

public class NinthSkill : Skill {

	public NinthSkill(){		
		skillName = "NinthSkill";
		icon = Resources.Load (skillName, typeof(Sprite)) as Sprite;
	}

	override public void Use(){}
//	override public Sprite SkillIcon(){
//		return this.icon;
//	}
}
