using UnityEngine;
using System.Collections.Generic;

abstract public class Skill : MonoBehaviour
{
	public float recoil;
	public float time;
	public Sprite icon;

	abstract public void Use();
}


