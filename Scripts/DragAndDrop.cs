using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class DragAndDrop : MonoBehaviour,
IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler{
	
	AdditionalMenuAction amaScript;
	Character character;

	Sprite tempInventoryCell;
	GameObject icon;
	GameObject drop;
	bool isDragged = false;
	public Camera mainCamera;
	GameObject inventoryPanel;
	Vector3 screenPoint;
	Vector3 offset;
	public Sprite emptyInventoryCell;
	bool mouseInside = false;

	public bool isInventory = false;
	public int idCell;
	public InventoryItem item;
	public string type;


	void Awake()
	{		
		inventoryPanel = GameObject.Find ("InventoryPanel");
		character = Character._instanceCharacter;
	}

	void Update(){	
		if (amaScript == null) {
//			print ("ama null");
			amaScript = AdditionalMenuAction._instanceAMA;
		}
		if (Input.GetMouseButtonUp (0) && mouseInside) {
//			print ("Right click!");
			amaScript.ShowPreview (idCell, isInventory, this.gameObject);
		}
	}

	public void OnPointerEnter(PointerEventData data){
		mouseInside = true;
	}

	public void OnPointerExit(PointerEventData data){
		mouseInside = false;
	}

	public void UpdateItem(InventoryItem item){
		this.item = item;
		GetComponent<Image> ().sprite = item.ItemIcon;
	}

	public void OnDrop(PointerEventData data)
	{
		DragAndDrop tempScript = data.pointerDrag.GetComponent<DragAndDrop> ();
		if (tempScript.isDragged) {
//			print ("Куда = " + this.gameObject.name);
//			print ("Что = " + data.pointerDrag.name);
			if (type.Equals("Any")) {				
				InventoryItem temp = tempScript.item;
				tempScript.item = item;
				item = temp;

			} else if(type.Equals(tempScript.item.Type)){
				switch (type) {
				case "Weapon":
					Weapon weapon = item as Weapon;
					item = tempScript.item;
					tempScript.item = weapon;
					if (idCell == 0)
						character.PrimaryWeapon = tempScript.item as Weapon;
					else if (idCell == 1)
						character.SecondaryWeapon = tempScript.item as Weapon;
						break;
				case "Armor":
					character.Armor = tempScript.item as Armor;					
					break;
				}
				data.pointerDrag.GetComponent<Image> ().sprite = tempScript.tempInventoryCell;
			}
			data.pointerDrag.GetComponent<Image> ().sprite = tempScript.item.ItemIcon;
			GetComponent<Image> ().sprite = item.ItemIcon;
			tempScript.isDragged = false;
		} else {
			print ("Opps! Missed!");
		}
	}


	public void OnBeginDrag(PointerEventData eventData)
	{
		if (item == null) {			
			return;
		}
		else
		{
			icon = new GameObject ("Icon");
			icon.transform.SetParent (inventoryPanel.transform);
			icon.AddComponent<Image> ().sprite = GetComponent<Image> ().sprite;
			icon.transform.localScale = new Vector2 (1, 1);
			icon.AddComponent<CanvasGroup>().blocksRaycasts = false;
			tempInventoryCell = GetComponent<Image> ().sprite;
			GetComponent<Image> ().sprite = emptyInventoryCell;
			isDragged = true;

			SetDraggedPosition ();
		}
	}

	public void OnDrag(PointerEventData eventData)
	{			
		if (!isDragged)
			return;
		else
		{
			if (icon != null)
				SetDraggedPosition ();		
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Destroy (icon);
		icon = null;
	}

	private void SetDraggedPosition()
	{
		if (mainCamera == null)
//			mainCamera = Camera.
			return;
			//mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
//		Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
//		Vector3 curPosition = mainCamera.WorldToScreenPoint (curScreenPoint) + offset;
//		icon.transform.position = curPosition;
//		Debug.Log ("MouseX = " + Input.mousePosition.x + "; MouseY = " + Input.mousePosition.y);
//		Debug.Log ("Icon Position = " + icon.transform.position);
		icon.transform.position = mainCamera.WorldToScreenPoint(Input.mousePosition);
//		icon.transform.localPosition = Input.mousePosition;
	}
		
}
