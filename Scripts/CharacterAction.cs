using UnityEngine;
using System.Collections;

public class CharacterAction : MonoBehaviour {
	
	public static CharacterAction _instanceCA;

	Character characterScript;
	private UIManager uiScript;


	public Transform player;
	private float speed; // скорость передвижения

	public GunAction gunActionScript;
	GameObject item;
	bool pressing = false;


	void Start () 
	{
		if (_instanceCA == null)
			_instanceCA = this;
		else if (_instanceCA != this) {
			print ("CA not alone");
		}
		uiScript = UIManager._instanceUIM;
		characterScript = Character._instanceCharacter;
		UpdateParameters ();
	}

	void Update () 
	{
		InputMovement();
		//при нажатие на Е персонажа говорит менаджеру, что предмет с idItem я хочу взять
		if (Input.GetKeyUp(KeyCode.E) && item != null) {
			item.GetComponent<BagAction> ().ShowItemList ();
			YouCantTakeMe ();
		}

		Cheats ();

		if (characterScript.CheckLevelUp ())
			characterScript.LevelUp ();
	}

	public void Cheats(){
		//чит TEMP
		if (Input.GetKeyDown (KeyCode.F4))
			characterScript.LevelUp ();
		if(Input.GetKeyDown(KeyCode.F3))
			characterScript.currentExperience += 10000;
		if(Input.GetKeyDown(KeyCode.F2))
			characterScript.currentExperience += 100;
		if (Input.GetKeyDown (KeyCode.F1))
			characterScript.currentHealthPoints -= 25;
	}

	public void YouCanTakeMe(GameObject me)
	{
		item = me;
	}

	public void YouCantTakeMe()
	{
		item = null;
	}

	//обновляем параметры при изменении оружия у персонажа в руках
	public void UpdateParameters(){
//		print ("UPDATE PARAMETRS");
		speed = characterScript.Speed;	
		gunActionScript.UpdateParameters ();
	}
		
	void InputMovement()
	{
		if (player == null)
			return;
		if (Input.GetKey (KeyCode.W)) {
			player.Translate (Vector2.up * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.D)) {
			player.Translate (Vector2.right * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.A)) {
			player.Translate (Vector2.left * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S)) {
			player.Translate (Vector2.down * speed * Time.deltaTime);
		}
	}

	public Vector3 GetCurrentPositionOfPlayer(){
		return GameObject.Find ("Player").transform.localPosition;
	}
}
