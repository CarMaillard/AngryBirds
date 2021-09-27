using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public Rigidbody2D hook;
	public float releaseTime = .15f;
	public float waitTime = 2f;
	public float maxDragDistance = 2f;
	public GameObject nextBall;
	private Rigidbody2D rb;
	private bool isPressed = false;

    private void Start()
    {
		rb = GetComponent<Rigidbody2D>();
    }

    void Update()
	{
		if (isPressed)
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (Vector3.Distance(mousePos, hook.position) > maxDragDistance)
				rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
			else
				rb.position = mousePos;
		}
	}

	void OnMouseDown()
	{
		isPressed = true;
		rb.isKinematic = true;
	}

	void OnMouseUp()
	{
		isPressed = false;
		rb.isKinematic = false;
		StartCoroutine(Release());
	}

	IEnumerator Release()
	{
		yield return new WaitForSeconds(releaseTime);
		GetComponent<SpringJoint2D>().enabled = false;
		this.enabled = false;
		yield return new WaitForSeconds(waitTime);
		if (nextBall != null)
		{
			nextBall.SetActive(true);
		}
		else
		{
			Enemy.EnemiesAlive = 0;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
