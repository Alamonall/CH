using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BagAction : MonoBehaviour {

	DropListAction dropListScript;
	UIManager uiScript;
	CharacterAction characterActionScript;
	public GameObject buttonE;
	public Sprite defaultSprite;

	public int[] dropList; // temp

	public InventoryItem[] dropItemList;
	ArrayList dropTempList;
	bool isNewChest = false;

	void Start () {		
		buttonE.SetActive (false);
		uiScript = UIManager._instanceUIM;
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = defaultSprite;

	}

	public void AmEmpty(){
		Destroy (this.gameObject);
	}

	void Update(){
		if (characterActionScript == null)
			characterActionScript = CharacterAction._instanceCA;
		if (dropListScript == null)
			dropListScript = DropListAction._instanceDLA;
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
			if (characterActionScript == null) {
				print ("CAS is null");
				return;
			}
			characterActionScript.YouCanTakeMe (this.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other){
//		Debug.Log ("OnTriggerExit!");
		buttonE.SetActive (false);
		uiScript.bDMenu = false;
		if (characterActionScript == null) {
			print ("CAS is null");
			return;
		}
		characterActionScript.YouCantTakeMe ();
	}

	public void ShowItemList(){		
		dropTempList = new ArrayList ();
//		print ("droptemp = " + dropList.Length);
		for(int i = 0; i < dropList.Length; i++){
//			print ("droplist[i] = " + i);
//			print ("droplist[i] = " + dropList [i]);
			dropTempList.Add (uiScript.GetItemFromAll (dropList [i]));	
		}
		if (dropList == null) {
			print ("doplist is null");
			return;
		}	
		if (dropListScript == null) {
			print ("droplistscript null");
			return;
		}
		dropItemList = (InventoryItem[])dropTempList.ToArray (typeof(InventoryItem));
		dropListScript.ShowDropList (dropItemList, this);	
	}
}
