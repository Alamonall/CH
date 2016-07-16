using UnityEngine;
using System.Collections;

public class EighthSkill : Skill {

	public EighthSkill(){		
		skillName = "EighthSkill";
		icon = Resources.Load (skillName, typeof(Sprite)) as Sprite;
	}

	override public void Use(){}
}
