using UnityEngine;
using System.Linq;
using Assets.Objetos.Ilha;

public class PonteScript : MonoBehaviour {
	public bool desativarPlacas = true;

	private int qtdPlacasAtivas = 0;
	private int qtdPlacasDesativadas = 0;
	void Start ()
	{
		if (desativarPlacas)
		{
			qtdPlacasDesativadas = 0;
			var filhos = transform.GetComponentsInChildren<Transform>();
			Transform[] placas = filhos.Where(obj => obj.tag == "removivel").ToArray();
			foreach (Transform placa in placas)
			{
				var scriptPlaca = placa.gameObject.AddComponent<PlacaScript>();
				scriptPlaca.ponte = this;
				scriptPlaca.deveDestruir = false;
				scriptPlaca.tipoRecurso = TipoRecurso.nada;
				qtdPlacasDesativadas++;
			}
		}				
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AdicionarPlaca()
	{
		qtdPlacasAtivas++;

		if (qtdPlacasAtivas == qtdPlacasDesativadas)
		{
			QuestManager.Instance.QuestPonte(false);
		}		
	}

	public bool IsPonteOk()
	{
		return qtdPlacasAtivas == qtdPlacasDesativadas;
	}
}
