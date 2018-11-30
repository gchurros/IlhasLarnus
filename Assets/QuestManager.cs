using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
	public static QuestManager Instance { get; private set; }
	public Text encontrarMago;
	public Text construirPonte;
	public Text esqueletos;
	public Text planta;
	public Text madeira;
	public Text pedra;
	public bool madeiraOk = false;
	public bool pedraOk = false;

	public GameObject ponte;

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

	public void QuestMago(bool ativar)
	{
		if (ativar)
		{
			PainelScript.instance.MandarTexto("Parece que o mago negro atacou a vila junto de seus monstro e uma maldicao paira sob a ilha");
			PainelScript.instance.MandarTexto("Precisamos da ajuda do mago Lich. Sua missao e encontrar-lo na floresta sagrada.");

			StartCoroutine(QuestMagoItem());
		}
		else
		{
			encontrarMago.text = "• Encontrar o mago (1/1)";
			StartCoroutine(CompleteMago());
		}
	}

	IEnumerator CompleteMago()
	{
		yield return new WaitForSeconds(5);
		MissaoMago(false);
	}

	IEnumerator QuestMagoItem()
	{
		yield return new WaitForSeconds(15);
		MissaoMago(true);
	}

	public void MissaoMago(bool ativar)
	{
		if (ativar)
			encontrarMago.GetComponent<MissoesScript>().Ativar(true);
		else
			encontrarMago.GetComponent<MissoesScript>().Desativar(true);
	}

	public void QuestPonte(bool ativar)
	{
		if (ativar && (construirPonte.GetComponent<MissoesScript>().ativado || ponte.GetComponent<PonteScript>().IsPonteOk()))
			return;

		if (ativar)
			construirPonte.GetComponent<MissoesScript>().Ativar(false);
		else
			construirPonte.GetComponent<MissoesScript>().Desativar(false);

		if (ativar)
			madeira.GetComponent<MissoesScript>().Ativar(false);
		else
			madeira.GetComponent<MissoesScript>().Desativar(false);

		if (ativar)
			pedra.GetComponent<MissoesScript>().Ativar(true);
		else
			pedra.GetComponent<MissoesScript>().Desativar(true);

		if (ativar)
		{
			GameManager.podeInteragir = true;
		}
	}

	public void QuestPlanta(bool ativar)
	{
		if (ativar)
			planta.GetComponent<MissoesScript>().Ativar(true);
		else
			planta.GetComponent<MissoesScript>().Desativar(true);
	}

	public void QuestEsqueletos(bool ativar)
	{
		if (ativar && (esqueletos.GetComponent<MissoesScript>().ativado || GameManager.esqueletosVivos == 0))
			return;

		if (ativar)
			esqueletos.GetComponent<MissoesScript>().Ativar(true);
		else
			esqueletos.GetComponent<MissoesScript>().Desativar(true);
	}

	public void CompletouMadeira()
	{
		madeiraOk = true;
		madeira.GetComponent<MissoesScript>().Desativar(true);
		VerificaMissaoPonte();
	}

	public void CompletouPedra()
	{
		pedraOk = true;
		pedra.GetComponent<MissoesScript>().Desativar(true);
		VerificaMissaoPonte();
	}

	public void VerificaMissaoPonte()
	{
		if (madeiraOk && pedraOk)
		{
			construirPonte.GetComponent<MissoesScript>().Desativar(true);
			StartCoroutine(SegundaMissaoPonte());
		}
	}

	IEnumerator SegundaMissaoPonte()
	{
		yield return new WaitForSeconds(5);
		construirPonte.text = "• Reconstrua a ponte!";
		construirPonte.GetComponent<MissoesScript>().Ativar(true);
	}
}
