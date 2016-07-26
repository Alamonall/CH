using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Rigidbody2D pro;
	public float speed = 20;
	public Quaternion tempRot;
	public Vector3 lookPos;
	public float bulletTimeLimit = 50.0f;
	public float bulletSpeed = 10;

	void Update ()
	{
		if (bulletTimeLimit > 0) {	
			bulletTimeLimit -= Time.deltaTime;	
		} else {
			bulletTimeLimit = 1.0f;
			Destroy (this.gameObject);
		}
	}
}





