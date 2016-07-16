using UnityEngine;
using System.Collections;

public class ThirdSkill : Skill {

	public ThirdSkill(){
		skillName = "ThirdSkill";
		icon = Resources.Load (skillName, typeof(Sprite)) as Sprite;
	}

	override public void Use(){

	}		
}
