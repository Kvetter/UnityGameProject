using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController2 : MonoBehaviour {

	public Transform goal1;
	public Transform goal2;
	UnityEngine.AI.NavMeshAgent agent;
	bool target = true;

	void Start () {

		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.destination = goal1.position;
	}

	void Update() {

		float dist = agent.remainingDistance;

		if (dist != Mathf.Infinity && agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete && agent.remainingDistance <= 0) {
			if (target) {
				agent.destination = goal1.position;
				target = false;
			} else {
				agent.destination = goal2.position;
				target = true;
			}
		}

	}
}
