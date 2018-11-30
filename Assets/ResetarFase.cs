using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetarFase : MonoBehaviour {
	public static ResetarFase instance;
	public Transform checkPoint;
	public Transform p1;
	public Transform p2;
	public Transform p3;
	// Use this for initialization

	void Awake()
	{
		instance = this;
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		GameManager.instance.SetVida(500);
		PlayerManager.instance.player.transform.position = checkPoint.position;
	}
}
