using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LFWheel : MonoBehaviour {

	public GameObject Wheel;
	float hitPoints;
	WheelCollider collider;

	// Use this for initialization
	void Start () {
		hitPoints = 100;
		collider = Wheel.GetComponent<WheelCollider>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void onTriggerEnter(Collider other) {
		Wheel.transform.parent = null;
	}
}
