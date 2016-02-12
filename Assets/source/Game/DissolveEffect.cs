using UnityEngine;
using System.Collections;

public class DissolveEffect : MonoBehaviour {
	public float speed = 3f;
	private bool disolving = false;
	private float amount = 0;
	private new Renderer renderer;

	void Start() {
		renderer = GetComponent<Renderer>();
	}

	public void Dissolve()
	{
		disolving = true;
	}

	public void Constitute()
	{
		disolving = false;
	}

	void Update () {
		if (disolving && amount < 0.75f)
		{
			amount = Mathf.Lerp(amount, 0.76f, speed * Time.deltaTime);
			renderer.material.SetFloat("_DissolveAmount", amount);
		}
		else if (!disolving && amount > 0)
		{
			amount = Mathf.Lerp(amount, -0.01f, speed * Time.deltaTime);
			renderer.material.SetFloat("_DissolveAmount", amount);
		}
	}
}
