using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class BoardManager : MonoBehaviour {
	public static BoardManager _instanceBM;

	public GameObject[] enemys;		// враги на сцене
	public GameObject[] npcs;		//персонажи на сцене
	public GameObject[] grounds; 	//декорации пола на сцене

    void Start () {
		if (_instanceBM == null) {
			DontDestroyOnLoad (this.gameObject);
			_instanceBM = this;
		} else if (_instanceBM != this)
			Destroy (this.gameObject);
	}

	public void SetupScene(int lvl){
		// Из массива с данными о каждой сцене берутся данные о сцене. Затем они загружаются и ставяться на нужных местах
		if (lvl == 1) {
//			GameObject env = GameObject.Find ("Environment");
//			for (int i = 0; i < 50; i++) 
//				for(int j = 0; j < 50; j++){
//					Instantiate (grounds [0], new Vector2 (i * 41, j * 41), Quaternion.identity);
//			}
		}
	}

}
