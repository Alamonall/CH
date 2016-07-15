using UnityEngine;
using System.Collections;

public class FirstSkill : Skill {
	public static FirstSkill _instFirstS;

	public float recoil;
	public float time;
	public Sprite icon;

	void Start()
	{
		icon = Resources.Load (this.name, typeof(Sprite)) as Sprite;
	}

	override public void Use(){
		
	}

	public Sprite Icon {
		get {
			return icon;
		}
	}
}
