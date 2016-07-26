using UnityEngine;
using System.Collections;

public class ThirteenthSkill : Skill {

	public ThirteenthSkill(){		
		skillName = "ThirteenthSkill";
		icon = Resources.Load (skillName, typeof(Sprite)) as Sprite;
	}

	override public void Use(){}
}



