using UnityEngine;
using System.Collections;

public class SecondSkill : Skill {
	public static SecondSkill _instSecondS;

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
