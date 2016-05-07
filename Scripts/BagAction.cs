using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BagAction : MonoBehaviour {

	DropListAction dropListScript;
	UIManager uiScript;
	CharacterAction characterActionScript;

	public GameObject buttonE;
	public Sprite defaultSprite;
	public int[] dropList;

	ArrayList dropTempList;
	bool isNewChest = false;

	void Start () {
		dropListScript = DropListAction._instanceDLA;
		buttonE.SetActive (false);
		uiScript = UIManager._instanceUIM;
		characterActionScript = CharacterAction._instanceCA;
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = defaultSprite;
	}

	public void AmEmpty()
	{
		Destroy (this.gameObject);
	}

	public void AddDropList(int id){
		isNewChest = true;
		if (dropTempList == null)
			dropTempList = new ArrayList();
		dropTempList.Add (id);
	}

	void OnTriggerEnter2D(Collider2D other) {
//		print ("OnTriggerEnter");
		if(other.tag.Equals("Player")){
			buttonE.SetActive (true);
			characterActionScript.YouCanTakeMe (this.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other){
//		Debug.Log ("OnTriggerExit!");
		buttonE.SetActive (false);
		uiScript.bDMenu = false;
		characterActionScript.YouCantTakeMe ();
	}

	public void ShowItemList(){			
		if (isNewChest) {
			dropList = (int[])dropTempList.ToArray (typeof(int));
		}
		isNewChest = false;
		if (dropList == null) {
//			print ("doplist is null");
			return;
		}	
		dropListScript.ShowDropList (dropList, this);	
	}
}
