using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsqueletoAnimacaoScript : MonoBehaviour {

	private Animator anim;
	private UnityEngine.AI.NavMeshAgent agent;
	private EnemyScript enemyInfo;
	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator>();
		agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
		enemyInfo = GetComponent<EnemyScript>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
}
