using UnityEngine;
using System.Collections;

public enum WalkingState
{
	Idle,
	Walking,
	Running,
	Action
}

public enum Direction
{
	Right = 0,
	Down = 1,
	Left = 2,
	Up = 3,
	UR,
	UL,
	DR,
	DL
}

public class CharacterStates : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
