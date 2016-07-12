using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PreviewScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	bool mouseInside = false;
	float consOvertime = 5;
	float overtime = 5;
	bool switchPreview = false;

	void FixedUpdate () {
		if(!mouseInside && switchPreview)
			overtime -= Time.deltaTime*10;
		if (overtime <= 0) {
			TurnOffPreview ();
			overtime = consOvertime;
		}
	}

	public void OnPointerEnter(PointerEventData data){
		mouseInside = true;
	}

	public void OnPointerExit(PointerEventData data){
		mouseInside = false;
	}

	public void TurnOffPreview(){
		this.gameObject.transform.localScale = new Vector3 (0,0,0);
		switchPreview = false;
	}
	public void TurnOnPreview(){
		this.gameObject.transform.localScale = new Vector3(1,1,1);
		switchPreview = true;
	}
}
