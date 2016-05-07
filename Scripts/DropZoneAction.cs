using UnityEngine;
using System.Collections;

public class DropZoneAction : MonoBehaviour {

	public static DropZoneAction _instanceDropZone;
	Canvas canvas;
	UIManager uiScript;

	void Awake () {
		if (_instanceDropZone == null) {
			DontDestroyOnLoad (this.gameObject);
			_instanceDropZone = this;
		} else if (_instanceDropZone != this)
			Destroy (this.gameObject);
		uiScript = UIManager._instanceUIM;

	}

	void Update()
	{
		if (uiScript.gameActive) 
			this.gameObject.transform.localScale = new Vector3(1,1,1);
	}
}
