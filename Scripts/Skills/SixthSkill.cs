using UnityEngine;
using System.Collections;

public class SixthSkill : Skill {

	public SixthSkill(){		
		skillName = "SixthSkill";
		icon = Resources.Load (skillName, typeof(Sprite)) as Sprite;
	}

	override public void Use(){}
//	override public Sprite SkillIcon(){
//		return this.icon;
//	}
}

