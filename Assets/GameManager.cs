using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameManager : MonoBehaviour
{
	public GameObject golem;
	public GameObject final;
	public static bool pausar;
	public static GameManager instance;
	public static int esqueletosVivos = 17;
	public static bool podeInteragir = false;
	public Slider barraDeVida;
	public Slider barraDeFome;
	public Slider vidaMago;
	public Transform barreira;
	public Text textoVida;
	public Text textoFome;
	public Text missaoMadeira;
	public Text missaoPedra;
	public Text missaoEsqueleto;
	public AudioSource novo;
	public GameObject area3;
	public bool ativouVidaMago = false;

	public int vida = 500;
	public int fome = 150;
	public int madeira = 0;
	public int pedra = 0;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		SetVida(500);
	}

	void Update()
	{
		if (ativouVidaMago)
		{
			vidaMago.value = vidaMago.value + (1f * Time.deltaTime);

			if (vidaMago.value == 100)
			{
				ativouVidaMago = false;
				golem.GetComponent<EnemyScript>().ativado = false;
				vidaMago.gameObject.SetActive(false);

				PainelScript.instance.MandarTexto("Otimo! Agora que o mago esta curado ele pode tomar o poder da floresta novamente e do golem guardiao!");
				PainelScript.instance.MandarTexto("Agora o proximo passo e ir em busca das pedras sagradas para montar uma arma capaz de derrotar o mago negro!");
				StartCoroutine(finalJogo());
			}
		}
	}

	IEnumerator finalJogo()
	{
		yield return new WaitForSeconds(15f);

		pausar = true;
		instance.final.SetActive(true);
	}

	public void restartLevel()
	{
		ResetarFase.instance.checkPoint = ResetarFase.instance.p1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		final.SetActive(false);
		pausar = false;
	}

	public void attEsqueletos()
	{
		missaoEsqueleto.text = "• Derrote os esqueletos (" + (17 - esqueletosVivos).ToString() + "/17)";

		if (esqueletosVivos == 0)
		{
			QuestManager.Instance.QuestEsqueletos(false);
			barreira.gameObject.SetActive(false);
		}
	}

	public void ColetouPlanta()
	{
		QuestManager.Instance.QuestPlanta(false);
		PainelScript.instance.MandarTexto("O golem acordou! Leve a planta para o mago e sobreviva até ele se curar!");
		area3.SetActive(true);

		StartCoroutine(acordarGolem());
	}

	IEnumerator acordarGolem()
	{
		yield return new WaitForSeconds(1f);
		golem.GetComponent<EnemyScript>().ativado = true;
	}

	public void AdicionarVida(int _vida)
	{
		SetVida(vida + _vida);
	}

	public void RemoverVida(int _vida)
	{
		SetVida(vida - _vida);
	}

	public void AdicionarFome(int _fome)
	{
		SetVida(vida + _fome);
	}

	public void RemoverFome(int _fome)
	{
		SetVida(vida - _fome);
	}

	public void SetVida(int _vida)
	{
		vida = _vida;

		if (vida > 500)
			vida = 500;

		if (vida <= 0)
			restartLevel();

		barraDeVida.value = vida;
		textoVida.text = vida.ToString();
	}

	public void SetFome(int _fome)
	{
		fome = _fome;

		if (fome > 150)
			fome = 150;

		barraDeFome.value = fome;
		textoFome.text = fome.ToString();
	}

	public void AdicionarMadeira(int _madeira)
	{
		SetMadeira(madeira + _madeira);
	}

	public void RemoverMadeira(int _madeira)
	{
		SetMadeira(madeira - _madeira);
	}

	public void AdicionarPedra(int _pedra)
	{
		SetPedra(pedra + _pedra);
	}

	public void RemoverPedra(int _pedra)
	{
		SetPedra(pedra - _pedra);
	}

	public void SetMadeira(int _madeira)
	{
		madeira = _madeira;

		if (madeira >= 500)
		{
			madeira = 500;
			QuestManager.Instance.CompletouMadeira();
		}

		missaoMadeira.text = "• Coletar madeira (" + madeira.ToString() + "/500)";
	}

	public void SetPedra(int _pedra)
	{
		pedra = _pedra;

		if (pedra >= 300)
		{
			pedra = 300;
			QuestManager.Instance.CompletouPedra();
		}

		missaoPedra.text = "• Coletar pedra (" + pedra.ToString() + "/300)";
	}
}
