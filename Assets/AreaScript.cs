using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AreaScript : MonoBehaviour
{
	public bool jaEntrou = false;

	void OnTriggerEnter(Collider other)
	{
		if (!jaEntrou)
		{
			jaEntrou = true;
			var area = transform.name;
			switch (area)
			{
				case "0":
					QuestManager.Instance.QuestPonte(true);
					break;
				case "1":
					PainelScript.instance.MandarTexto("Parece que a vila foi invadida por esqueletos. Derrote-os para seguir");
					StartCoroutine(questEsqueletos());
					break;
				case "2":
					QuestManager.Instance.QuestMago(false);
					PainelScript.instance.MandarTexto("Encontramos o mago finalmente!");
					PainelScript.instance.MandarTexto("Durante o ataque do mago negro, o mago Lich foi derrotado e está ferido, precisamos ajudar ele a se curar");
					PainelScript.instance.MandarTexto("Vá mais adentro da floresta magica e procure uma planta magica perto das estatuas sagradas, mas cuide, pois existe um golem protegendo a area!");


					//StartCoroutine(final());

					GameManager.podeInteragir = true;
					StartCoroutine(planta());
					break;
				case "3":
					PainelScript.instance.MandarTexto("Aguente firme até o mago se curar!");
					GameManager.instance.ativouVidaMago = true;
					GameManager.instance.vidaMago.gameObject.SetActive(true);
					break;
			}
		}
	}


	IEnumerator questEsqueletos()
	{
		yield return new WaitForSeconds(0.4f);
		QuestManager.Instance.QuestEsqueletos(true);
	}

	IEnumerator planta()
	{
		yield return new WaitForSeconds(10f);
		QuestManager.Instance.QuestPlanta(true);
	}
}
