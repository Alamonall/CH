using UnityEngine;
using System.Collections;

public class TenthSkill : Skill {

	public TenthSkill(){		
		skillName = "TenthSkill";
		icon = Resources.Load (skillName, typeof(Sprite)) as Sprite;
	}

	override public void Use(){}
}


