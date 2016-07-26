using UnityEngine;
using System.Collections;

public class SecondSkill : Skill {
	public static SecondSkill _instSecondS;

	public SecondSkill(){
		skillName = "SecondSkill";
		icon = Resources.Load (skillName, typeof(Sprite)) as Sprite;
	}

	override public void Use(){

	}
//	override public Sprite SkillIcon(){
//		return this.icon;
//	}
}
