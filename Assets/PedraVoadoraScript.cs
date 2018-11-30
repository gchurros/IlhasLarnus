using UnityEngine;

public class PedraVoadoraScript : MonoBehaviour
{

	bool entrou = false;
	bool saiu = false;
	Vector3 origem;

	void Start()
	{
		origem = transform.localPosition;
	}

	void Update()
	{
		var local = transform.localPosition;
		if (entrou)
		{
			if (local.y > origem.y - .5f)
			{
				local.y -= 3f * Time.deltaTime;
				transform.localPosition = local;
			}
			else if (saiu)
			{
				if (local.y < origem.y)
				{
					local.y += 3f * Time.deltaTime;
					transform.localPosition = local;
				}
			}
		}

	}

	void OnCollisionEnter(Collision collision)
	{
		entrou = true;
	}

	void OnCollisionExit(Collision collision)
	{
		saiu = true;
	}
}
