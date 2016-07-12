﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Skill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public GameObject target;
	public Image image;
	public Sprite iconAvalaible;
	public Sprite iconDontAvalaible;
	public Sprite iconTaken;
	public Sprite iconConfirmed;

	public int openingLevel; 	//уровень открытия умения
	public int state = -1; // -1 - недоступно, 0 - доступно, 1 - взято, 2 - подтверждено
	public Growth[] Growths;

	bool mouseInside = false;
	public GameObject previewGo;

	void Start(){
		//print ("start = " + target.GetComponent<Image> ().sprite.name);
		image = target.GetComponent<Image> ();
		image.sprite = iconDontAvalaible;
	}

	void Update(){	
		if (Input.GetMouseButtonUp (1) && mouseInside) {
			print ("Right click!");
			ShowContextMenu ();
		}
	}

	#region ShowContexMenu
	public void ShowContextMenu(){
		if (state != 2) {
			return;
		}
		previewGo.transform.localPosition = this.gameObject.transform.localPosition;
		previewGo.GetComponent<PreviewScript> ().TurnOnPreview ();
	}
	#endregion

	public void OnPointerEnter(PointerEventData data){
		mouseInside = true;
	}

	public void OnPointerExit(PointerEventData data){
		mouseInside = false;
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
		if (state == 1) {
			state = 2;
			image.sprite = iconConfirmed;
			NotifyCharacter ();
			ShowGrowths ();
		}
		//Есть возможность оптимизировать. Выделить в отдельную функцию без проверки
		if (state == 2) {
			foreach (Growth g in Growths)
				g.Confirmed ();
		}
	}

	public void ShowGrowths(){
		foreach (Growth g in Growths)
			g.ShowUp ();
	}

	public void NotifyCharacter(){
		print ("I'm notify = " + this.gameObject.name);
		//сообщить персонажу, какие умения и развития были приняты
	}


		
}
