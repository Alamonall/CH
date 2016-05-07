using UnityEngine;
using System.Collections;

public class Assault : Specialization{
	
	public Assault(){		
		name = "Rick";
		nextLevelExperience = 1000; // необходимо опыта до получения нового уровня
		nextLevelSkillPoints = 3;	// количество очков опыта за получение нового уровня
		maxStaminaPoints = 100; //  максимальное количество выносливости
		modHealtPoints = 50; // модификатор жизней при получении нового уровня
		maxHealtPoints = 200; // максимальное количество очков жизней на данном уровне
		speed = 50;  // текущая скорость
		overSpeed = 30; //скорость при ускорении
	}
}
