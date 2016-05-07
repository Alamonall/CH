using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class BoardManager : MonoBehaviour {
	
	public GameObject[] enemys;		// враги на сцене
	public GameObject[] npcs;		//персонажи на сцене
	public GameObject[] grounds; 	//декорации пола на сцене

    void Start () {}

	void SetupScene(int lvl){
		// Из массива с данными о каждой сцене берутся данные о сцене. Затем они загружаются и ставяться на нужных местах
	}

}
