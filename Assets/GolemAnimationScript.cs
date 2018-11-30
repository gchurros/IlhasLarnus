using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemAnimationScript : MonoBehaviour {

	private Animator anim;
	private NavMeshAgent agent;
	private EnemyScript enemyInfo;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		agent = GetComponentInChildren<NavMeshAgent>();
		enemyInfo = GetComponent<EnemyScript>();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
