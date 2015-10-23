using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class EffectsManager : MonoBehaviour {
	GameObject success;
	GameObject failure;

	// Use this for initialization
	void Start () {
		this.success = GameObject.Find("Effects/success");
		this.failure = GameObject.Find("Effects/failure");
		Assert.IsNotNull(this.success);
		Assert.IsNotNull(this.failure);
	}

	public void indicateSuccess() {
		imageDisplay(success);
		playSuccessSound();
	}
	
	public void indicateFailure() {
		imageDisplay(failure);
		playFailureSound();
	}
	
	void imageDecay() {
		imageDecay(success);
		imageDecay(failure);
	}

	void playSuccessSound() {
		success.GetComponent<AudioSource>().Play();
	}
	
	void playFailureSound() {
		failure.GetComponent<AudioSource>().Play();
	}

	void imageDisplay(GameObject g) {
		g.GetComponent<Transform>().localScale.Set(150, 150, 150);
		Color c = g.GetComponent<SpriteRenderer>().color;
		c.a = 1;
		g.GetComponent<SpriteRenderer>().color = c;
		
	}
	
	void imageDecay(GameObject g) {
		Color c = g.GetComponent<SpriteRenderer>().color;
		
		if (c.a > 0) {
			c.a -= Time.deltaTime;
			Vector3 v = g.GetComponent<Transform>().localScale;
			v.x += Time.deltaTime * 10;
			v.y += Time.deltaTime * 10;
			g.GetComponent<Transform>().localScale = v;
			g.GetComponent<SpriteRenderer>().color = c;
		}
	}
	
	// Update is called once per frame
	void Update () {
		imageDecay();
	}
}
