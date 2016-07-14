using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Growth : MonoBehaviour , IPointerClickHandler{

	public CharacterMenuAction _cma;

	public int growthMax = 5;

	public bool bDuration = false;
	public bool bRecoil = false;
	public bool bAction = false;

	public Sprite[] activSprites;
	public Sprite[] confSprites;
	public Sprite thisSprite;

	public int state = -1; // -1 - недоступно, 0 - доступно, 2 - на максимуме

	public int temp;
	public int cons;

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left){
			GrowUp ();
//			print ("Left Click!");
		}
		else if (eventData.button == PointerEventData.InputButton.Right) {
			GrowDown ();
//			print ("Right Click!");
		}
		
	}

	public void ShowUp(){
		this.gameObject.transform.localScale = new Vector3 (1, 1, 1);	

		//Возможно стоит избавиться
//		if (bAction) {
//			cons = growthAction;
//			temp = tempGrowthAction;
//		}
//		if (bTime) {
//			cons = growthTime;
//			temp = tempGrowthTime;
//		}	
//		if (bRecoil) {
//			cons = growthRecoil;
//			temp = tempGrowthRecoil;
//		}
		//
	}


	public void Avalaible(){	
		state = 0;	
		//		print ("state = 0");
//		image.sprite = iconAvalaible;
	}

	public void DontAvalaible(){
		state = -1;
//		image.sprite = iconDontAvalaible;
	}

	public void OnMaximum(){
		//изменить визуально
	}

	public void Confirmed(){
		print ("I'm confirmed!");
		cons = temp;
		if (growthMax == cons) {
			OnMaximum ();
			state = 2;
		}
		NotifyCharacter ();
//		this.gameObject.GetComponent<Image> ().sprite = confSprites [temp];
	}

	public void NotifyCharacter(){
		print ("I'm notify = " + this.gameObject.name);
		//сообщить персонажу, какие умения и развития были приняты
	}

	//функции для кнопок
	public void GrowUp(){			
		if (temp < growthMax)
			if(_cma.GrowthHandler("Up")) {
				temp++;
//				this.gameObject.GetComponent<Image> ().sprite = activSprites [temp];
			//изменить визуально		
			}
	}

	//функции для кнопок
	public void GrowDown(){
		if (temp > cons)
			if(_cma.GrowthHandler("Down")) {
				temp--;
//				this.gameObject.GetComponent<Image> ().sprite = activSprites [temp];
				//изменить визуально
//				anim.CrossFade("GrowthDown", 2);
			}
	}
}
