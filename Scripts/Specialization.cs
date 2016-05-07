using UnityEngine;
using System.Collections;

public class Specialization {
	public string name;
	public float nextLevelExperience; // необходимо опыта до получения нового уровня
	public int nextLevelSkillPoints;	// количество очков опыта за получение нового уровня
	public float maxStaminaPoints; //  максимальное количество выносливости
	public float modHealtPoints; // модификатор жизней при получении нового уровня
	public float maxHealtPoints; // максимальное количество очков жизней на данном уровне
	public float speed;  // текущая скорость
	public float overSpeed; //скорость при ускорении

	void FirstSkill(){}
	void SecondSkill(){}
	void ThirdSkill(){}
	void FourthSkill(){}
}
