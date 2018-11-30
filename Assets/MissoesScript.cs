using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissoesScript : MonoBehaviour {

	private Text gText;
	private Vector3 origin;
	private RectTransform rect;
	public bool ativado, sumir;

	void Start() {
		gText = GetComponent<Text>();
		rect = GetComponent<RectTransform>();
		origin = rect.localPosition;

		rect.localPosition = new Vector3(origin.x + 400, origin.y, origin.z);
		ativado = false;
		sumir = false;
		
		Color white = new Color(255, 255, 255);
		white.a = 0.0f;

		gText.material = new Material(gText.material);
		gText.material.color = white;
	}

	// Update is called once per frame
	void Update() {
		if (ativado)
		{
			if (rect.localPosition.x > origin.x)
				rect.localPosition = new Vector3(rect.localPosition.x - (800f * Time.deltaTime), rect.localPosition.y, rect.localPosition.z);

			Color white = gText.material.color;
			if (white.a < 1)
			{
				white.a += 1f * Time.deltaTime;
				gText.material.color = white;
			}
		}
		else if (sumir)
		{
			if (rect.localPosition.x < origin.x + 400f)
				rect.localPosition = new Vector3(rect.localPosition.x + (1000f * Time.deltaTime), rect.localPosition.y, rect.localPosition.z);

			Color white = gText.material.color;
			if (white.a > 0)
			{
				white.a -= 1f * Time.deltaTime;
				gText.material.color = white;
			}
		}
	}

	public void Ativar(bool som)
	{
		if (som)
			SongManager.Instance.NovaMissao();

		ativado = true;
	}

	public void Desativar(bool som)
	{
		if (som)
			SongManager.Instance.SucessoMissao();

		ativado = false;
		sumir = true;
	}
}
