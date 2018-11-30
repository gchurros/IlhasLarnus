using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Objetos.Ilha;

public class PlayerInventario : MonoBehaviour {

	public void AddRecurso(TipoRecurso tp, int quantidade)
	{
		switch (tp) 
		{
			case TipoRecurso.madeira:
				SongManager.Instance.PegarMadeira();
				GameManager.instance.AdicionarMadeira(quantidade);
				break;
			case TipoRecurso.barril:
				SongManager.Instance.QuebraMadeira();
				GameManager.instance.AdicionarMadeira(quantidade);
				break;
			case TipoRecurso.caixa:
				SongManager.Instance.QuebraMadeira();
				GameManager.instance.AdicionarMadeira(quantidade);
				break;
			case TipoRecurso.pedra:
				SongManager.Instance.PegarPedra();
				GameManager.instance.AdicionarPedra(quantidade);
				break;
			case TipoRecurso.comida:
				SongManager.Instance.PegarItem();
				GameManager.instance.AdicionarFome(quantidade);
				break;
			case TipoRecurso.planta:
				SongManager.Instance.PegarItem();
				GameManager.instance.ColetouPlanta();
				break;
		}
	}
}
