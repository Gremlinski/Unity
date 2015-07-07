using UnityEngine;
using System.Collections;

public class MoveInGrid : MonoBehaviour {

	public float speed = 10.0f;
	private Vector3 position;
	private Vector3 previousPosition;
	private Transform tr;
	private int playerDamage = 10;
	
	void Start() {
		position = transform.position;
		previousPosition = transform.position;
		tr = transform;
	}
	
	void Update() {

		if (Input.GetKeyDown(KeyCode.Q) && tr.position == position)
		{
			position += Vector3.up + Vector3.left;
		}
		else if (Input.GetKeyDown(KeyCode.W) && tr.position == position)
		{
			position += Vector3.up;
		}
		else if (Input.GetKeyDown(KeyCode.E) && tr.position == position)
		{
			position += Vector3.up + Vector3.right;
		}
		else if (Input.GetKeyDown(KeyCode.A) && tr.position == position)
		{
			position += Vector3.left;
		}
		else if (Input.GetKeyDown(KeyCode.D) && tr.position == position)
		{
			position += Vector3.right;
		}
		else if (Input.GetKeyDown(KeyCode.Z) && tr.position == position)
		{
			position += Vector3.down + Vector3.left;
		}
		else if (Input.GetKeyDown(KeyCode.X) && tr.position == position)
		{
			position += Vector3.down;
		}
		else if (Input.GetKeyDown(KeyCode.C) && tr.position == position)
		{
			position += Vector3.down + Vector3.right;
		}

		foreach(GameObject enemyObject in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			if(enemyObject.transform.position == position)
			{
				enemyObject.SendMessage("TakeDamage", playerDamage);

				//DestroyObject(enemyObject);

				position = previousPosition;
			}
		}


		transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * speed);
		//transform.position = position;
		previousPosition = position;
	}  

}
