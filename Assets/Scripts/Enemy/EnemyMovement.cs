using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	Transform playerTransform;
	NavMeshAgent nav;

	// Use this for initialization
	void Start () {
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		nav.SetDestination (playerTransform.position);
		transform.LookAt (new Vector3 (playerTransform.position.x, 0, playerTransform.position.z));
	}
}
