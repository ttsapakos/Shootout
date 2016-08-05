using UnityEngine;
using System.Collections;

public class PlayerImage : MonoBehaviour {

	public Sprite Ready;
	public Sprite Fired;
	public Sprite Dead;
	public Sprite Crouched;
	public bool isRightPlayer;
	private GameController gameController;
	private Player thisPlayer;
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
	}
	
	// Update is called once per frame
	void Update () {
		if (isRightPlayer) {
			thisPlayer = gameController.rightPlayer;
		} else {
			thisPlayer = gameController.leftPlayer;
		}

		if (thisPlayer.isAlive && thisPlayer.isLoaded) {
			renderer.sprite = Ready;
		}

		if (thisPlayer.isAlive && thisPlayer.isCrouched) {
			renderer.sprite = Crouched;
		}

		if (thisPlayer.isAlive && !thisPlayer.isLoaded) {
			renderer.sprite = Fired;
		}

		if (!thisPlayer.isAlive) {
			renderer.sprite = Dead;
		}
	}
}
