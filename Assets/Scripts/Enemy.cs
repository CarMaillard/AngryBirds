using UnityEngine;

public class Enemy : MonoBehaviour
{
	public GameObject deathEffect;
	public float health = 4f;
	public static int EnemiesAlive = 0;

	void Start()
	{
		EnemiesAlive++;
	}

	void OnCollisionEnter2D(Collision2D colInfo)
	{
		if (colInfo.relativeVelocity.magnitude > health)
		{
			Die();
		}
	}

	void Die()
	{
		GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 2f);
		EnemiesAlive--;
		if (EnemiesAlive <= 0)
			Debug.Log("LEVEL WON!");
		Destroy(gameObject);
	}
}
