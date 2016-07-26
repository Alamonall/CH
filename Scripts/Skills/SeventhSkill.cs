using UnityEngine;
using System.Collections;

public class SeventhSkill : Skill {

	public SeventhSkill(){		
		skillName = "SeventhSkill";
		icon = Resources.Load (skillName, typeof(Sprite)) as Sprite;
	}

	override public void Use(){}
//	override public Sprite SkillIcon(){
//		return this.icon;
//	}
}
