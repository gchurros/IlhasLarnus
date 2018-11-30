using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour {
	public static SongManager Instance { get; private set; }
	public AudioSource quest;
	public AudioSource interecoes;
	public AudioClip pegandoItem;
	public AudioClip placa;
	public AudioClip pedra;
	public AudioClip madeira;
	public AudioClip quebraMadeira;	
	public AudioClip novaMissao;
	public AudioClip sucessoMissao;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void PegarItem()
	{
		interecoes.clip = pegandoItem;
		interecoes.Play();
	}

	public void PegarMadeira()
	{
		interecoes.clip = madeira;
		interecoes.Play();
	}

	public void PegarPedra()
	{
		interecoes.clip = pedra;
		interecoes.Play();
	}

	public void QuebraMadeira()
	{
		interecoes.clip = quebraMadeira;
		interecoes.Play();
	}

	public void ColocarPlaca()
	{
		interecoes.clip = placa;
		interecoes.Play();
	}

	public void NovaMissao()
	{
		quest.clip = novaMissao;
		quest.Play();
	}

	public void SucessoMissao()
	{
		quest.clip = sucessoMissao;
		quest.Play();
	}
}
