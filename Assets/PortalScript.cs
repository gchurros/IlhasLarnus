using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour {

	public AudioSource velho;
	public AudioSource novo;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		velho.Stop();
		ResetarFase.instance.checkPoint = ResetarFase.instance.p3;
		PlayerManager.instance.player.transform.position = ResetarFase.instance.p3.transform.position;
		PlayerManager.instance.player.transform.rotation = ResetarFase.instance.p3.transform.rotation;
		StartCoroutine(novaMusica());
		StartCoroutine(msg());

		GameManager.podeInteragir = false;
		GameManager.instance.novo = novo; 
	}

	IEnumerator novaMusica()
	{
		yield return new WaitForSeconds(.9f);
		novo.Play();
	}

	IEnumerator msg()
	{
		yield return new WaitForSeconds(1.5f);
		PainelScript.instance.MandarTexto("Parece que encontramos o mago, vá falar com ele");
	}
}
