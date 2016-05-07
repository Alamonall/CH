using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class DragAndDrop : MonoBehaviour,
IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler{
	
	AdditionalMenuAction amaScript;
	Sprite tempInventoryCell;
	GameObject icon;
	GameObject drop;
	bool isDragged = false;
	bool isOnUi = true;
	public Camera mainCamera;
	GameObject inventoryPanel;
	Vector3 screenPoint;
	Vector3 offset;

	bool isInventory = false;
	bool isDropCell = false;
	Sprite emptyInventoryCell;

	public int idCell;

	void Awake()
	{		
		inventoryPanel = GameObject.Find ("InventoryPanel");
		amaScript = AdditionalMenuAction._instanceAMA;
	}

	void Update(){	
		if (Input.GetMouseButtonDown (1))
			amaScript.ShowPreview (idCell, isInventory);
	}


	public void OnDrop(PointerEventData data)
	{
		if (data.pointerDrag.GetComponent<DragAndDrop> ().isDragged) {
			print ("Куда = " + this.gameObject.name);
			print ("Что = " + data.pointerDrag.name);
			if (amaScript.SwapItem (this.gameObject, data.pointerDrag)) {
				if (isDropCell)
					return;
				Sprite newSprite = data.pointerDrag.GetComponent<DragAndDrop> ().tempInventoryCell;
				data.pointerDrag.GetComponent<Image> ().sprite = GetComponent<Image> ().sprite;
				GetComponent<Image> ().sprite = newSprite;
			} else {
				data.pointerDrag.GetComponent<Image> ().sprite = data.pointerDrag.GetComponent<DragAndDrop> ().tempInventoryCell;
			}
			data.pointerDrag.GetComponent<DragAndDrop> ().isDragged = false;
		} else {
			print ("Opps! Missed!");
		}
	}


	public void OnBeginDrag(PointerEventData eventData)
	{
		if (GetComponent<Image> ().sprite.Equals (emptyInventoryCell)) {			
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
