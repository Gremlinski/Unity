using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 10.0f;
	private Vector3 position;
	private Vector3 previousPosition;
	private Transform tr;
	
	private int playerDamageMin = 1;
	private int playerDamageMax = 2;
	
	private int playerDamage;

	private bool playerMoving = false;

	public static Player instance = null;		

	public AudioClip attackSound1;		//First of two audio clips to play when attacking the player.
	public AudioClip attackSound2;	
	
	void Awake() {
		//Check if there is already an instance of SoundManager
		if (instance == null)
			//if not, set it to this.
			instance = this;
		//If instance already exists:
		else if (instance != this)
			//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
			Destroy (gameObject);
		
		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad (gameObject);

		position = transform.position;
		previousPosition = transform.position;
		tr = transform;
	}
	
	void Update() {

		if (Input.GetKeyDown(KeyCode.Q) && tr.position == position)
		{
			position += Vector3.up + Vector3.left;
			playerMoving = true;
		}
		else if (Input.GetKeyDown(KeyCode.W) && tr.position == position)
		{
			position += Vector3.up;
			playerMoving = true;
		}
		else if (Input.GetKeyDown(KeyCode.E) && tr.position == position)
		{
			position += Vector3.up + Vector3.right;
			playerMoving = true;
		}
		else if (Input.GetKeyDown(KeyCode.A) && tr.position == position)
		{
			position += Vector3.left;
			playerMoving = true;
		}
		else if (Input.GetKeyDown(KeyCode.D) && tr.position == position)
		{
			position += Vector3.right;
			playerMoving = true;
		}
		else if (Input.GetKeyDown(KeyCode.Z) && tr.position == position)
		{
			position += Vector3.down + Vector3.left;
			playerMoving = true;
		}
		else if (Input.GetKeyDown(KeyCode.X) && tr.position == position)
		{
			position += Vector3.down;
			playerMoving = true;
		}
		else if (Input.GetKeyDown(KeyCode.C) && tr.position == position)
		{
			position += Vector3.down + Vector3.right;
			playerMoving = true;
		}

		//Cycle through all objects with tag of Enemy and check if player is trying to move in its place (or attack)
		foreach(GameObject enemyObject in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			if(enemyObject.transform.position == position)
			{
				//Player attacks the enemy
				
				int playerDamage = Random.Range(playerDamageMin, playerDamageMax);
				
				enemyObject.SendMessage("TakeDamage", playerDamage);

				//DestroyObject(enemyObject);

				position = previousPosition;
			}
		}

		//Player moving
		if (playerMoving == true) {

			//Call the RandomizeSfx function of SoundManager passing in the two audio clips to choose randomly between.
			SoundManager.instance.RandomizeSfx (attackSound1, attackSound2);

			//transform.position = Vector3.MoveTowards (transform.position, position, Time.deltaTime * speed);
			transform.position = position;
			previousPosition = position;


		}

		//Check if player stopped moving
		if (tr.position == position) {
			playerMoving = false;
		}

	}  

}
