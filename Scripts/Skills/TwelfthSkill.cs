using UnityEngine;
using System.Collections;

public class TwelfthSkill : Skill {

	public TwelfthSkill(){		
		skillName = "TwelfthSkill";
		icon = Resources.Load (skillName, typeof(Sprite)) as Sprite;
	}

	override public void Use(){}
}
