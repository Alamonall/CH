using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public static Camera _instanceCamera;

	void Awake ()
	{
		if (_instanceCamera == null)
			_instanceCamera = this.GetComponent<Camera>();
		else if (_instanceCamera != this)
			Destroy (this.gameObject);
	}
}
