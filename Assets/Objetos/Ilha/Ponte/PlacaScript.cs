using UnityEngine;
using System.Collections.Generic;

public class PlacaScript : InteragivelScript {	

	public PonteScript ponte;	
	private bool desativada;

	protected override void Start()
	{
		base.Start();
		gameObject.layer = LayerMask.NameToLayer("InteragivelSemColisao");
				
		var novosMat = new Material[cacheMateriais.Length];
		for (int i = 0; i < novosMat.Length; i++)
		{
			novosMat[i] = MaterialManager.Instance.missingObject;
		}

		mr.materials = novosMat;	
		
		desativada = true;
	}

	public override void onHover(GameObject interactor)
	{
		if (desativada)
		{
			var novosMat = new Material[cacheMateriais.Length];
			for (int i = 0; i < novosMat.Length; i++)
			{
				novosMat[i] = MaterialManager.Instance.selectedObject;
			}

			mr.materials = novosMat;
		}
	}

	public override void OnLeave(GameObject interactor)
	{
		if (desativada)
		{
			var novosMat = new Material[cacheMateriais.Length];
			for (int i = 0; i < novosMat.Length; i++)
			{
				novosMat[i] = MaterialManager.Instance.missingObject;
			}

			mr.materials = novosMat;
		}
	}

	public override void onInteract(GameObject interactor)
	{
		base.onInteract(interactor);
		if (Input.GetKey("e"))
		{
			if (desativada)
			{
				if (!QuestManager.Instance.madeiraOk || !QuestManager.Instance.pedraOk)
					return; 

				gameObject.layer = LayerMask.NameToLayer("Default");
				gameObject.tag = "chao";
				desativada = false;
				mr.materials = cacheMateriais;
				ponte.AdicionarPlaca();
				SongManager.Instance.ColocarPlaca();
			}
		}
	}

	public override void onInteracting(GameObject interactor)
	{
	}
}