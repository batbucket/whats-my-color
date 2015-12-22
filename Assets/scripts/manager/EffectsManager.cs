using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class EffectsManager : MonoBehaviour {

	/**
	 * Point to the success and failure gameobjects
	 * which hold the their respective sounds and sprites
	 */
	GameObject success;
	GameObject failure;

	public const int DECAY_SPEED = 10;

	// Use this for initialization
	void Start () {
		this.success = GameObject.Find("Effects/success");
		this.failure = GameObject.Find("Effects/failure");
		Assert.IsNotNull(this.success);
		Assert.IsNotNull(this.failure);
	}

    /**
     * Correct answer FX
     */
	public void indicateSuccess() {
		imageDisplay(success);
		playSuccessSound();
	}
	
    /**
     * Incorrect answer FX
     */
	public void indicateFailure() {
		imageDisplay(failure);
		playFailureSound();
	}
	
    /**
     * Makes images fade out
     */
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

    /**
     * Reset image alpha and scale to initial
     */
	void imageDisplay(GameObject g) {
		g.GetComponent<Transform>().localScale.Set(150, 150, 150);
		Color c = g.GetComponent<SpriteRenderer>().color;
		c.a = 1;
		g.GetComponent<SpriteRenderer>().color = c;
		
	}

	/**
	 * Images decay by growing larger and having their alpha reduced
	 */
	void imageDecay(GameObject g) {
		Color c = g.GetComponent<SpriteRenderer>().color;
		
		if (c.a > 0) {
			c.a -= Time.deltaTime * DECAY_SPEED;
			Vector3 v = g.GetComponent<Transform>().localScale;
			v.x += Time.deltaTime * DECAY_SPEED;
			v.y += Time.deltaTime * DECAY_SPEED;
			g.GetComponent<Transform>().localScale = v;
			g.GetComponent<SpriteRenderer>().color = c;
		}
	}
	
	// Update is called once per frame
	void Update () {
		imageDecay();
	}
}
