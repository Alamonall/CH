using UnityEngine;
using System.Collections;

public class EleventhSkill : Skill {

	public EleventhSkill(){		
		skillName = "EleventhSkill";
		icon = Resources.Load (skillName, typeof(Sprite)) as Sprite;
	}

	override public void Use(){}
//	override public Sprite SkillIcon(){
//		return this.icon;
//	}
}

