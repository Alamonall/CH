using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class DragAndDrop : MonoBehaviour,
IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler{
	
	AdditionalMenuAction amaScript;
	Character character;
	public Sprite emptyInventoryCell;// спрайт пустой ячейки
	GameObject icon;
	GameObject drop;
	GameObject inventoryPanel;
	Vector3 screenPoint;
	Vector3 offset;
	public Camera mainCamera;
	public InventoryItem item;
	public bool isInventory = false;
	bool mouseInside = false;
	public string type; //тип InventoryItem, который может поместиться в эту ячейку

	InventoryItem tempInventoryItem; 
	bool isDragged = true;
	public int idCell;

	void Awake()
	{		
		inventoryPanel = GameObject.Find ("InventoryPanel");
	}

	void Update(){	
		if (amaScript == null) {
//			print ("ama null");
			amaScript = AdditionalMenuAction._instanceAMA;
		}
		if(character == null)
		{
//			print ("character is null");
			character = Character._instanceCharacter;
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


	#region UpdateItem
	public void UpdateItem(InventoryItem item){
		this.item = item;
		if(!isInventory)
			switch (idCell) {
			case 0:
				character.PrimaryWeapon = item as Weapon;
				break;
			case 1:
				character.SecondaryWeapon = item as Weapon;
				break;
			case 2:
				character.Armor = item as Armor;
				break;
			default:
				print ("nothing sacred");
				break;
			}
		if (item == null)
			GetComponent<Image> ().sprite = emptyInventoryCell;
		else
			GetComponent<Image> ().sprite = item.ItemIcon;
	}
	#endregion

	public void OnDrop(PointerEventData data)
	{
		DragAndDrop tempScript = data.pointerDrag.GetComponent<DragAndDrop> ();
		if (tempScript == null) {
			print ("tempScript is null");
			return;
		}
		if(character == null)
		{
			print ("character is null");
			return;
		}
		if (tempScript.isDragged) {
//			print ("Куда = " + this.gameObject.name);
//			print ("Что = " + data.pointerDrag.name);
			if (type.Equals ("Any")) {		
				InventoryItem temp = tempScript.item;
				tempScript.UpdateItem (item);
				UpdateItem (temp);
			} else if (type.Equals (tempScript.item.Type)) {
				print ("type is don't any");
				switch (type) {
				case "Weapon":
					Weapon weapon = item as Weapon;
					UpdateItem (tempScript.item);
					tempScript.UpdateItem (weapon);
					break;
				case "Armor":
					Armor armor = item as Armor;
					UpdateItem (tempScript.item);
					tempScript.UpdateItem (armor);
					break;
				}
			}
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

			GetComponent<Image> ().sprite = emptyInventoryCell;
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
		UpdateItem (this.item);
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
