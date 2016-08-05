using UnityEngine;
using System.Collections;

public struct Player {
	public bool isLoaded;
	public bool isAlive;
	public bool isCrouched;
}

public class GameController : MonoBehaviour {

	private int ringCount = 0;
	private bool ringsStarted = false;
	private bool restart = false;
	private SpriteRenderer renderer;
	private int leftPlayerWins;
	private int rightPlayerWins;
	private GameObject scoresObject;

	public Player rightPlayer;
	public Player leftPlayer;
	public bool playersReady = false;
	public AudioClip Bell;
	public AudioClip Gunshot;

	void Start () {
		rightPlayer.isAlive = true;
		rightPlayer.isLoaded = true;
		rightPlayer.isCrouched = false;
		leftPlayer.isAlive = true;
		leftPlayer.isLoaded = true;
		leftPlayer.isCrouched = false;

		renderer = gameObject.GetComponent<SpriteRenderer>();
		renderer.enabled = false;

		scoresObject = GameObject.FindWithTag ("Scores");
		if (scoresObject != null) {
			rightPlayerWins = scoresObject.GetComponent<Scores>().rightPlayerWins;
			leftPlayerWins = scoresObject.GetComponent<Scores>().leftPlayerWins;
		} else {
			Debug.Log ("Cannot find 'Scores' script");
		}
	}

	public IEnumerator Ring () {
		WaitForSeconds delay = new WaitForSeconds(Random.Range(2, 8));
		while (ringCount < 3) {
			yield return delay;
			AudioSource.PlayClipAtPoint(Bell, transform.position);
			ringCount++;
			delay = new WaitForSeconds(Random.Range(2, 8));
		}
	}
	
	void Update () {
		GetButtonPresses ();
		if (playersReady && !ringsStarted) {
			ringsStarted = true;
			StartCoroutine ("Ring");
		}

		CheckForWin ();

		if(restart)
		{
			if(Input.GetKeyDown ("space"))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}

		//Debug.Log ("Left: " + leftPlayerWins + " Right: " + rightPlayerWins);
	}

	void GetButtonPresses () {
		if (Input.GetKeyDown ("space")) {
			playersReady = true;
			GetComponent<AudioSource>().Stop ();
		}

		if (!playersReady)
			return;

		GetLeftPlayerPresses ();
		GetRightPlayerPresses ();

	}

	void GetLeftPlayerPresses () {
		if (Input.GetKeyDown ("d") && leftPlayer.isAlive && leftPlayer.isLoaded && ringCount == 3) {
			leftPlayer.isCrouched = true;
		}
		
		if (Input.GetKeyUp ("d") && leftPlayer.isAlive && leftPlayer.isCrouched && ringCount == 3) {
			leftPlayer.isCrouched = false;
		}
		
		if (Input.GetKeyDown ("f") && leftPlayer.isAlive && leftPlayer.isLoaded && !leftPlayer.isCrouched) {
			if (ringCount == 3) {
				leftPlayer.isLoaded = false;
				if (!rightPlayer.isCrouched) {
					rightPlayer.isAlive = false;
					scoresObject.GetComponent<Scores>().leftPlayerWins++;
				}
			} else {
				leftPlayer.isLoaded = false;
			}
			AudioSource.PlayClipAtPoint (Gunshot, transform.position);
		}
	}

	void GetRightPlayerPresses () {
		if (Input.GetKeyDown ("k") && rightPlayer.isAlive && rightPlayer.isLoaded && ringCount == 3) {
			rightPlayer.isCrouched = true;
		}
		
		if (Input.GetKeyUp ("k") && rightPlayer.isAlive && rightPlayer.isCrouched && ringCount == 3) {
			rightPlayer.isCrouched = false;
		}

		if (Input.GetKeyDown ("j") && rightPlayer.isAlive && rightPlayer.isLoaded && !rightPlayer.isCrouched) {
			if (ringCount == 3) {
				rightPlayer.isLoaded = false;
				if (!leftPlayer.isCrouched) {
					leftPlayer.isAlive = false;
					scoresObject.GetComponent<Scores>().rightPlayerWins++;
				}
			} else {
				rightPlayer.isLoaded = false;
			}
			AudioSource.PlayClipAtPoint (Gunshot, transform.position);
		}
	}

	void CheckForWin () {
		if (!rightPlayer.isLoaded && !leftPlayer.isLoaded) {
			StopCoroutine ("Ring");
			restart = true;
			renderer.enabled = true;
		}

		if (!leftPlayer.isAlive) {
			restart = true;
			renderer.enabled = true;
		}

		if (!rightPlayer.isAlive) {
			restart = true;
			renderer.enabled = true;
		}
	}
}