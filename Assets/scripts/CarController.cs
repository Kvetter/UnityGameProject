using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CarController : MonoBehaviour
{
	private float idealRPM = 500f;
	private float maxMotorTorque = 1000f;
	// maximum torque the motor can apply to wheel
	public float maxSteeringAngle;
	// maximum steer angle the wheel can have
	public WheelCollider RRW;
	public WheelCollider LRW;
	public WheelCollider LFW;
	public WheelCollider RFW;
	Rigidbody rigid;
	float steering;
	private float hitPoints;
	private float speed;
	public Text healthText;
	public Text speedText;
	public Text WinText;

	public void Start ()
	{
		steering = 0;
		speed = 0;
		speedText.text = "Speed: " + speed.ToString (); 
		WinText.enabled = false;
		hitPoints = 100;
		rigid = this.GetComponent<Rigidbody> ();

		healthText.text = "Health: " + hitPoints.ToString ();

		// This does so the car wont flip over
		rigid.centerOfMass = new Vector3 (0, -0.9f, 0);
	}

	public void FixedUpdate ()
	{
		
		float motor = maxMotorTorque * Input.GetAxis ("Vertical");

		if(RRW.rpm < idealRPM)
			motor = Mathf.Lerp(motor/10f, motor, RRW.rpm / idealRPM );
		else 
			motor = Mathf.Lerp(motor, 0,  (RRW.rpm-idealRPM) / (maxMotorTorque-idealRPM) );


		if (Input.GetKey ("d")) {
			if (steering < (this.transform.rotation.y + maxSteeringAngle))
				steering += Input.GetAxis ("Horizontal");
			else
				steering = (this.transform.rotation.y + maxSteeringAngle) - 0.0000000001f;
		} else if (Input.GetKey ("a")) {
			if (steering > (this.transform.rotation.y - maxSteeringAngle))
				steering += Input.GetAxis ("Horizontal");
			else
				steering = (this.transform.rotation.y - maxSteeringAngle) + 0.0000000001f;
		}

		// Calc Speed

		if(speedText!=null)
			speedText.text = "Speed: " + Speed().ToString("f0") + " km/h";


		// WHEELS

		LRW.steerAngle = steering;
		RRW.steerAngle = steering;

		Vector3 posLeft = LRW.transform.rotation.eulerAngles;

		Vector3 posRight = RRW.transform.rotation.eulerAngles;

		LRW.transform.localRotation = Quaternion.Euler (0, LFW.steerAngle, 0);
		RRW.transform.localRotation = Quaternion.Euler (0, RFW.steerAngle, 0);

		LFW.motorTorque = motor;
		RFW.motorTorque = motor;
		RRW.motorTorque = motor;
		LRW.motorTorque = motor;

				//LFW.transform.Rotate (axleInfo.leftWheel.rpm / 60 * 360 * Time.deltaTime, 0, 0);

				//RFW.transform.Rotate (axleInfo.leftWheel.rpm / 60 * 360 * Time.deltaTime, 0, 0);




	}
	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "Wall") {
			rigid.AddForce (0, 0, 555555);
			hitPoints -= 10;
			healthText.text = "Health: " + hitPoints.ToString ();
		} else if (other.gameObject.tag == "Pedestrian") {
			hitPoints -= 10;
			healthText.text = "Health: " + hitPoints.ToString ();
		}
		

	}


	void OnTriggerEnter( Collider other)
	{
		if (other.gameObject.tag == "Finish")
		{
			WinText.enabled = true;
		}
		if (other.gameObject.tag == "Oil")
		{
			WheelFrictionCurve lw = LFW.sidewaysFriction;
			lw.stiffness = 0.1f;
			LFW.sidewaysFriction = lw;
			RFW.sidewaysFriction = lw;
			LRW.sidewaysFriction = lw;
			RRW.sidewaysFriction = lw;


		}
	}

	void OnTriggerExit( Collider other)
	{
		if (other.gameObject.tag == "Oil") {
			WheelFrictionCurve lw = LFW.sidewaysFriction;
			lw.stiffness = 1.5f;
			LFW.sidewaysFriction = lw;
			RFW.sidewaysFriction = lw;
			LRW.sidewaysFriction = lw;
			RRW.sidewaysFriction = lw;
		}
	}

	public float Speed() {
		return RRW.radius * Mathf.PI * RRW.rpm * 60f / 1000f;
	}

}
	
