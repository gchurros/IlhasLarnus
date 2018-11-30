using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainelScript : MonoBehaviour {

	public static PainelScript instance;
	public Text texto;
	private List<string> listaMsg;
	private bool exibindo = false;
	private bool podeClarearTexto = false;
	private bool podeClarearFundo = false;
	private bool podeLimparMsg = false;
	private bool podeLimparFundo = false;

	private void Awake()
	{
		instance = this;
	}

	void Start () {
		listaMsg = new List<string>();


		var image = GetComponent<Image>();
		Color black = new Color(0, 0, 0);
		black.a = 0.0f;

		image.material = new Material(image.material);
		image.material.color = black;

		Color white = new Color(255, 255, 255);
		white.a = 0.0f;

		texto.material = new Material(texto.material);
		texto.material.color = white;
	}
	
	void Update () {
		if (!exibindo)
		{
			if (listaMsg.Count > 0)
			{
				var msg = listaMsg[0];
				listaMsg.RemoveAt(0);

				texto.text = msg;
				podeClarearFundo = true;
				exibindo = true;
			}	
		}
		else if (podeClarearFundo)
		{
			var image = GetComponent<Image>();
			Color black = image.material.color;
			if (black.a < 1f)
			{
				black.a += .2f * Time.deltaTime;
				image.material.color = black;
			}
			else { podeClarearFundo = false; podeClarearTexto = true; }
		}
		else if (podeClarearTexto)
		{
			Color white = texto.material.color;
			if (white.a < 1)
			{
				white.a += 1f * Time.deltaTime;
				texto.material.color = white;
			}
			else { podeClarearFundo = false; podeClarearTexto = false; StartCoroutine(LimparTexto()); }
		}

		if (podeLimparMsg)
		{
			Color white = texto.material.color;
			if (white.a > 0)
			{
				white.r -= 1f * Time.deltaTime;
				white.g -= 1f * Time.deltaTime;
				white.b -= 1f * Time.deltaTime;
				white.a -= 1f * Time.deltaTime;
				texto.material.color = white;
			}
			else { podeLimparMsg = false; if (listaMsg.Count == 0) podeLimparFundo = true; exibindo = false; }
		}
		else if (podeLimparFundo)
		{
			var image = GetComponent<Image>();
			Color black = image.material.color;
			if (black.a > 0)
			{
				black.r -= 1f * Time.deltaTime;
				black.g -= 1f * Time.deltaTime;
				black.b -= 1f * Time.deltaTime;
				black.a -= 1f * Time.deltaTime;
				image.material.color = black;
			}
			else { podeLimparFundo = false; }
		}
	}
	   
	IEnumerator LimparTexto()
	{
		yield return new WaitForSeconds(4);
		podeLimparMsg = true;
	}

	public void MandarTexto(string msg)
	{
		listaMsg.Add(msg);
	}
}
