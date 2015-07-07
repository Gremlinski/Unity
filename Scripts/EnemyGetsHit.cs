using UnityEngine;
using System.Collections;

public class EnemyGetsHit : MonoBehaviour {

	private float speed = 10.0f;
	private Vector3 position;
	private Vector3 previousPosition;
	private Transform tr;

	private int enemyHP = 100;

	void Start() {
		position = transform.position;
		previousPosition = transform.position;
		tr = transform;
	}
	
	void Update() {
		transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * speed);
		//transform.position = position;
		previousPosition = position;
	}

	void TakeDamage(int playerDamage) {

		enemyHP -= playerDamage;

		if (enemyHP <= 0)
			Death ();

		Debug.Log(gameObject.name + " was hit for " + playerDamage + "hp. " + enemyHP + "left.");
	}

	void Death() {
		DestroyObject(gameObject);
	}
}
