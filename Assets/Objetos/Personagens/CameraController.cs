using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Transform target, pivo;
	public float rotateSpeed = 5.0f;
	public Vector3 offset;
	public float maxView = 45f;
	public float minView = 30f;
	// Use this for initialization
	void Start () {
		offset = target.position - transform.position;
		pivo.transform.parent = null;

		Cursor.lockState = CursorLockMode.Locked;
		StartCoroutine(IniciarMissao());

		ResetarFase.instance.checkPoint = ResetarFase.instance.p1;
		PlayerManager.instance.player.transform.position = ResetarFase.instance.checkPoint.transform.position;
		PlayerManager.instance.player.transform.rotation = ResetarFase.instance.checkPoint.transform.rotation;
	}

	IEnumerator IniciarMissao()
	{
		yield return new WaitForSeconds(0);
		QuestManager.Instance.QuestMago(true);
		ResetarFase.instance.checkPoint = ResetarFase.instance.p1;
	}

	// Update is called once per frame
	void LateUpdate () {
		if (GameManager.pausar)
		{
			Cursor.lockState = CursorLockMode.None;
			return;
		}

		float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
		float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;

		Vector3 newPosition = target.transform.position;
		newPosition.y = newPosition.y + 5;
		pivo.transform.position = newPosition;

		Vector3 newRotation = pivo.transform.rotation.eulerAngles + new Vector3(vertical * -1, horizontal, 0);

		if (newRotation.x > maxView && newRotation.x < 180f)
			newRotation.x = maxView;

		if (newRotation.x > 180f && newRotation.x < 360f - minView)
			newRotation.x = 360f - minView;

		newRotation.z = 0;
		pivo.transform.rotation = Quaternion.Euler(newRotation);
	}
}
