using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LFWheel : MonoBehaviour {

	public GameObject Wheel;
	WheelCollider collider;

	// Use this for initialization
	void Start () {
		collider = Wheel.GetComponent<WheelCollider>();

	}
	
	// Update is called once per frame
	void Update () {



	}

	void OnCollisionEnter (Collision other)
	{
		Debug.Log ("HEHEHEHE");
		if (other.gameObject.tag == "Oil") {
			
			WheelFrictionCurve fric = collider.sidewaysFriction;
			fric.stiffness = 0.1f;
			collider.sidewaysFriction = fric;

		}


	}

	void onTriggerEnter(Collider other) {
		Debug.Log ("HEHEHEHE");
	}
}
