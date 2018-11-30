using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Objetos.Ilha;

public class InteragivelScript : MonoBehaviour {
	public bool deveDestruir;
	public int qtdRecurso;
	public TipoRecurso tipoRecurso;
	public Material[] materialsSelected;
	protected MeshRenderer mr;
	protected Material[] cacheMateriais;

	protected virtual void Start()
	{
		mr = GetComponent<MeshRenderer>();

		if (mr != null)
			cacheMateriais = mr.materials;
	}


	public virtual void onHover(GameObject interactor)
	{
		if (!GameManager.podeInteragir)
			return;

		mr.materials = materialsSelected;
	}

	public virtual void OnLeave(GameObject interactor)
	{
		if (!GameManager.podeInteragir)
			return;

		mr.materials = cacheMateriais;
	}

	public virtual void onInteract(GameObject interactor)
	{
		if (Input.GetKey("e"))
		{
			if (!GameManager.podeInteragir)
				return;

			interactor.GetComponent<PlayerInventario>().AddRecurso(tipoRecurso, qtdRecurso);

			if (deveDestruir)
				Destroy(gameObject);
		}
	}

	public virtual void onInteracting(GameObject interactor)
	{
		if (!GameManager.podeInteragir)
			return;

	}
}
