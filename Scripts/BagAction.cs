using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BagAction : MonoBehaviour {

	DropListAction dropListScript;
	UIManager uiScript;
	CharacterAction characterActionScript;
	public GameObject buttonE;
	public bool isAChest;

	public List<InventoryItem> dropListInGame; // лист для дропа походу игры
	public int[] dropListOfChest; // лист для дропа в сундкуе

	public Sprite closed;
	public Sprite open;
	public Sprite bag;

	void Start () {		
		buttonE.SetActive (false);
		uiScript = UIManager._instanceUIM;
		if (isAChest) {
			dropListInGame = new List<InventoryItem> ();
			foreach (int id in dropListOfChest) {
				dropListInGame.Add (uiScript.GetItemFromAll (id));
			}
		}
	}

	public void AmEmpty(){
		if (isAChest) {
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = open;
			return;
		}
			
		Destroy (this.gameObject);
	}

	void Update(){
		if (characterActionScript == null)
			characterActionScript = CharacterAction._instanceCA;
		if (dropListScript == null)
			dropListScript = DropListAction._instanceDLA;
	}

	//Если не сундук, то создаем новый список, либо добавляем существующий список выкинутых предметов
	public void AddDropList(InventoryItem item){
		if (isAChest)
			return;
		if (dropListInGame == null) {
			dropListInGame = new List<InventoryItem> ();
		}
		dropListInGame.Add (item);
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
		if (dropListScript == null) {
			print ("droplistscript null");
			return;
		}
		dropListScript.ShowDropList (dropListInGame.ToArray (), this);	
	}
}
