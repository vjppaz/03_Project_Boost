using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour {

	[SerializeField] bool lightsOn = false;

	Light[] lights;

	// Use this for initialization
	void Start () {
		lights = GetComponentsInChildren<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (var l in lights) l.enabled = lightsOn;
	}
}
