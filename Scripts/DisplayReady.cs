using UnityEngine;
using System.Collections;

public class DisplayReady : MonoBehaviour {

	private bool display;
	private GameController gameController;
	private SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		} else {
			Debug.Log ("Cannot find 'GameController' script");
		}
		renderer = gameObject.GetComponent<SpriteRenderer>();
		renderer.enabled = true;
		display = true;
	}
	
	// Update is called once per frame
	void Update () {
		display = !gameController.playersReady;
		if (!display) {
			renderer.enabled = false;
		}
	}
}
