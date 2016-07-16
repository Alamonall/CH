using UnityEngine;
using System.Collections.Generic;

abstract public class Skill
{	public string skillName;
	public float recoil;
	public float time;
	public Sprite icon;

	abstract public void Use();

	public Sprite SkillIcon(){
		return this.icon;
	}
}


