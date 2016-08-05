using UnityEngine;
using System.Collections;

public class Scores : MonoBehaviour {

	public int leftPlayerWins = 0;
	public int rightPlayerWins = 0;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (this);
	}
}
