using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour {

	[SerializeField] Vector3 movementVector;

	[SerializeField]
	[Range(0, 1)]
	float movementFactor;

	Vector3 startingPosition;

	// Use this for initialization
	void Start () {
		startingPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = startingPosition + (movementVector * movementFactor);
	}
}
