using UnityEngine;
using System.Collections;

public class AIMovement : MonoBehaviour {

	public Transform goal;
	UnityEngine.AI.NavMeshAgent agent;

	void Start () {
		
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		 
	}

	void Update() {
		agent.destination = goal.position;
	}
}
