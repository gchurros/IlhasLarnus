using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastSelect : MonoBehaviour {
	public LayerMask layerMask;
	public Transform pivo;
	public Camera cam;

	private InteragivelScript currentTarget;
	private float selectionDistance = 20;
	protected virtual void FixedUpdate()
	{
		if (GameManager.pausar)
			return;

		RaycastHit hit;
		
		Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.green);
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, selectionDistance, layerMask))
		{
			if (currentTarget == null)
			{
				currentTarget = hit.transform.gameObject.GetComponent<InteragivelScript>();
				OnRaycastEnter(currentTarget);
			}
			else if (currentTarget != hit.transform.gameObject.GetComponent<InteragivelScript>())
			{
				OnRaycastLeave(currentTarget);
				currentTarget = hit.transform.gameObject.GetComponent<InteragivelScript>();
				OnRaycastEnter(currentTarget);
			}

			OnRaycast(hit.transform.gameObject.GetComponent<InteragivelScript>());
		}
		else if (currentTarget != null)
		{
			OnRaycastLeave(currentTarget);
			currentTarget = null;
		}
	}

	protected virtual void OnRaycastEnter(InteragivelScript target)
	{
		if (target != null)
		{
			target.onHover(transform.gameObject);
		}
	}

	protected virtual void OnRaycastLeave(InteragivelScript target)
	{
		if (target != null)
			target.OnLeave(transform.gameObject);
	}

	protected virtual void OnRaycast(InteragivelScript target)
	{
		if (target != null)
			target.onInteract(transform.gameObject);
	}
}
