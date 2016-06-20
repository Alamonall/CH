using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Skill : MonoBehaviour {

	public GameObject target;
	public Image image;
	public Sprite iconAvalaible;
	public Sprite iconDontAvalaible;
	public Sprite iconTaken;
	public Sprite iconConfirmed;

	public int openingLevel; 	//уровень открытия умения
	public int state = -1; // -1 - недоступно, 0 - доступно, 1 - взято, 2 - подтверждено

	public int growthMax = 5;
	public int growthTime = 0;
	public int tempGrowthTime = 0;
	public int growthAction = 0;
	public int tempGrowthAction = 0;
	public int growthRecoil = 0;
	public int tempGrowthRecoil = 0;

	void Start(){
		//print ("start = " + target.GetComponent<Image> ().sprite.name);
		image = target.GetComponent<Image> ();
		image.sprite = iconDontAvalaible;
	}

	public void Avalaible(){
		state = 0;
//		print ("state = 0");
		image.sprite = iconAvalaible;
	}

	public void DontAvalaible(){
		state = -1;
		image.sprite = iconDontAvalaible;
	}

	public void Given(){
		state = 0;
		image.sprite = iconAvalaible;
	}

	public void Taken(){
		state = 1;
		image.sprite = iconTaken;
	}

	//подтверждение Развития и Умения
	public void Confirmed(){
		if (state != 2) {
			state = 2;
			image.sprite = iconConfirmed;
		} else {
			growthTime = tempGrowthTime;
			growthAction = tempGrowthAction;
			growthRecoil = tempGrowthRecoil;
		}
		NotifyCharacter ();
	}

	public void NotifyCharacter(){
		//сообщить персонажу, какие умения и развития были приняты
	}

	//функции для кнопок
	public void GrowUp(string growth){
		switch(growth){
			case "action":
				if (tempGrowthAction < growthMax) {
					tempGrowthAction++;
					//изменить визуально
				}
				break;
			case "time":
				if (tempGrowthTime < growthMax) {
					tempGrowthTime++;
					//изменить визуально
				}
				break;
			case "recoil":
				if (tempGrowthRecoil < growthMax) {
					tempGrowthRecoil++;
					//изменить визуально
				}
				break;
		}
	}

	//функции для кнопок
	public void GrowDown(string growth){
		switch(growth){
			case "action":
				if(tempGrowthRecoil > growthAction){
						tempGrowthAction--;
					//изменить визуально
				}
				break;
			case "time":
				if(tempGrowthTime > growthTime){
						tempGrowthTime--;
					//изменить визуально
				}
				break;
			case "recoil":
				if(tempGrowthRecoil > growthRecoil){
						tempGrowthRecoil--;
					//изменить визуально
				}
				break;
		}
	}
		
}
