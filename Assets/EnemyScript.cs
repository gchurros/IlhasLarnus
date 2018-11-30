using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : InteragivelScript {

	public float lookRadius = 500f;
	public int danoPrimario = 10;
	public int danoSecundario = 50;
	public float distanciaParaAtacar = 10.0f;
	public bool temPoderSecundario = false;
	public int ataqueAnimacao = 0;
	public int vidaTotal;
	private int vidaAtual;
	private bool tomarDano = false;
	private bool tomandoDanoAux = false;
	private bool morreu = false;
	private bool estaAtacando = false;
	public bool esqueleto = false;
	public bool ativado = true;

	private Transform target;
	private NavMeshAgent agent;
	private Animator anim;

	protected override void Start () {
		base.Start();
		gameObject.layer = LayerMask.NameToLayer("Interagivel");
		transform.tag = "enemy";
		anim = GetComponent<Animator>();
		agent = GetComponentInChildren<NavMeshAgent>();
		target = PlayerManager.instance.player.transform;

		vidaAtual = vidaTotal;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!ativado)
		{
			anim.SetFloat("movimentSpeed", 0);
			return;
		}

		float distance = Vector3.Distance(target.position, transform.position);

		if (!morreu)
		{
			if (distance <= distanciaParaAtacar)
			{
				agent.enabled = false;
				if (ataqueAnimacao == 0 && !tomarDano && !estaAtacando)
				{
					StartCoroutine(Atacar());
					StartCoroutine(DelayAtacar());
				}
			}
			else if (ataqueAnimacao == 0 && vidaAtual > 0)
			{
				if (lookRadius > 0 && distance < lookRadius)
				{
					agent.enabled = true;
					agent.SetDestination(target.position);
				}
			}
		}
		else
		{
			agent.enabled = false;
			StartCoroutine(Destruir());
		}

		if (tomarDano)
		{
			if (!tomandoDanoAux)
			{
				StartCoroutine(TomarDano());
				anim.SetBool("atacado", vidaAtual > 0);
				anim.SetBool("morreu", vidaAtual <= 0);
			}
		}
		else
		{
			float speed = agent.velocity.magnitude;
			anim.SetFloat("movimentSpeed", speed);
			anim.SetInteger("atacou", ataqueAnimacao);
			anim.SetBool("atacado", false);
			anim.SetBool("morreu", false);
		}
	}
	
	IEnumerator Destruir()
	{
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}

	IEnumerator Atacar()
	{
		if (temPoderSecundario)
			ataqueAnimacao = Random.Range(1, 3);
		else
			ataqueAnimacao = 1;

		estaAtacando = true;
		yield return new WaitForSeconds(2.76f);
		float distance = Vector3.Distance(target.position, transform.position);
		if (distance <= distanciaParaAtacar)
		{
			GameManager.instance.RemoverVida(GetDano());
		}

		ataqueAnimacao = 0;
	}

	IEnumerator DelayAtacar()
	{

		yield return new WaitForSeconds(4.0f);
		estaAtacando = false;
	}


	IEnumerator TomarDano()
	{
		tomandoDanoAux = true;
		if (vidaAtual > 0)
			yield return new WaitForSeconds(1.3f);
		else
			yield return new WaitForSeconds(2.17f);

		morreu = vidaAtual <= 0;

		tomandoDanoAux = false;
		tomarDano = false;

		if (morreu && esqueleto)
		{
			GameManager.esqueletosVivos--;
			GameManager.instance.attEsqueletos();
		}
	}

	public int GetDano()
	{
		if (ataqueAnimacao == 1)
		{
			return danoPrimario;
		}
		else
		if (ataqueAnimacao == 2)
		{
			return danoSecundario;
		}

		return 0;
	}

	public override void onHover(GameObject interactor)
	{
	}


	public override void OnLeave(GameObject interactor)
	{
	}

	public override void onInteract(GameObject interactor)
	{
		if (esqueleto)
		{
			if (Input.GetMouseButton(0))
			{
				if (!tomandoDanoAux)
				{
					if (interactor.GetComponent<PlayerMovimento>().atacando)
					{
						tomarDano = true;
						vidaAtual -= PlayerManager.instance.player.GetComponent<PlayerCombatScript>().dano;
					}
				}
			}
		}
	}

	public override void onInteracting(GameObject interactor)
	{
	}
}
