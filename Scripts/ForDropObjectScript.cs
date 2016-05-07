using UnityEngine;
using System.Collections;

public class ForDropObjectScript : MonoBehaviour {
	UIManager _uis;

	void Start(){
		_uis = UIManager._instanceUIM;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag.Equals ("pickUp"))
			_uis.forDropObject = other.gameObject;
	}

	void OnTriggerExit2D(Collider2D other){
			_uis.forDropObject = null;
	}
}
