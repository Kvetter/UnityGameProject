using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarController : MonoBehaviour {
	public List<AxleInfo> axleInfos; // the information about each individual axle
	public float maxMotorTorque; // maximum torque the motor can apply to wheel
	public float maxSteeringAngle; // maximum steer angle the wheel can have
	public GameObject LFW;
	public GameObject RFW;
	public GameObject car;
	float steering;

	public void start(){
		steering = 0;
	}

	public void FixedUpdate()
	{
		float motor = maxMotorTorque * Input.GetAxis("Vertical");
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
				LFW.transform.localRotation = Quaternion.Euler(0, axleInfo.leftWheel.steerAngle, 0);
				Vector3 posRight = RFW.transform.rotation.eulerAngles;
				RFW.transform.localRotation = Quaternion.Euler(0, axleInfo.rightWheel.steerAngle, 0);

			}
			if (axleInfo.motor) {
				axleInfo.leftWheel.motorTorque = motor;
				axleInfo.rightWheel.motorTorque = motor;
			}
		}
	}
}


[System.Serializable]
public class AxleInfo {
	public WheelCollider leftWheel;
	public WheelCollider rightWheel;
	public bool motor; // is this wheel attached to motor?
	public bool steering; // does this wheel apply steer angle?
}