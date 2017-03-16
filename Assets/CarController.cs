using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarController : MonoBehaviour
{
	public List<AxleInfo> axleInfos;
	// the information about each individual axle
	public float maxMotorTorque;
	// maximum torque the motor can apply to wheel
	public float maxSteeringAngle;
	// maximum steer angle the wheel can have
	public GameObject LFW;
	public GameObject RFW;
	public GameObject car;
	Rigidbody rigid;
	float steering;
	float hitPoints;

	public void Start ()
	{
		steering = 0;
		hitPoints = 100;
		rigid = car.GetComponent<Rigidbody> ();
	}

	public void FixedUpdate ()
	{
		
		float motor = maxMotorTorque * Input.GetAxis ("Vertical");
		if (Input.GetKey ("d")) {
			if (steering < (car.transform.rotation.y + maxSteeringAngle))
				steering += Input.GetAxis ("Horizontal");
			else
				steering = (car.transform.rotation.y + maxSteeringAngle) - 0.0000000001f;
		} else if (Input.GetKey ("a")) {
			if (steering > (car.transform.rotation.y - maxSteeringAngle))
				steering += Input.GetAxis ("Horizontal");
			else
				steering = (car.transform.rotation.y - maxSteeringAngle) + 0.0000000001f;
		}


		foreach (AxleInfo axleInfo in axleInfos) {
			if (axleInfo.steering) {
				axleInfo.leftWheel.steerAngle = steering;
				axleInfo.rightWheel.steerAngle = steering;
				Vector3 posLeft = LFW.transform.rotation.eulerAngles;
				LFW.transform.localRotation = Quaternion.Euler (0, axleInfo.leftWheel.steerAngle, 0);
				LFW.transform.Rotate (axleInfo.leftWheel.rpm / 60 * 360 * Time.deltaTime, 0, 0);
				Vector3 posRight = RFW.transform.rotation.eulerAngles;
				RFW.transform.localRotation = Quaternion.Euler (0, axleInfo.rightWheel.steerAngle, 0);
				RFW.transform.Rotate (axleInfo.leftWheel.rpm / 60 * 360 * Time.deltaTime, 0, 0);

			}
			if (axleInfo.motor) {
				axleInfo.leftWheel.motorTorque = motor;
				axleInfo.rightWheel.motorTorque = motor;
			}
		}
	}

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "Wall") {
			rigid.AddForce (0, 0, 1000000);
		}

	}

}


[System.Serializable]
public class AxleInfo
{
	public WheelCollider leftWheel;
	public WheelCollider rightWheel;
	public bool motor;
	// is this wheel attached to motor?
	public bool steering;
	// does this wheel apply steer angle?
}

