using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovimento : RayCastSelect
{

	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	private bool grounded = false;
	public bool atacando = false;
	private bool deveAtacar = false;
	private Rigidbody rigidbody;
	private Animator anim;

	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		anim = GetComponentInChildren<Animator>();

		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
	}

	protected override void FixedUpdate()
	{

		if (GameManager.pausar)
			return;

		base.FixedUpdate();
		if (grounded && !atacando)
		{
			if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && (!Input.GetKey("left alt")))
			{
				transform.rotation = Quaternion.Euler(0f, pivo.rotation.eulerAngles.y, 0f);
				//Quaternion newRotation = Quaternion.LookRotation(moveDirection);
				//playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
			}
			
			if (Input.GetMouseButton(0) && !atacando)
			{
				deveAtacar = true;
			}

			// Calculate how fast we should be moving
			Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			targetVelocity = transform.TransformDirection(targetVelocity);

			if (Input.GetKey("left shift"))
				targetVelocity *= speed * 2;
			else
				targetVelocity *= speed;

			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rigidbody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

			// Jump
			if (canJump && Input.GetButton("Jump"))
			{
				var y = CalculateJumpVerticalSpeed();
				rigidbody.velocity = new Vector3(velocity.x, y, velocity.z);
			}
		}

		// We apply gravity manually for more tuning control
		rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));

		if (deveAtacar && !atacando)
		{
			StartCoroutine(Atacar());
			anim.SetBool("atacando", true);
		}
		else
		{
			if (!atacando)
			{
				anim.SetFloat("velocidadePlayer", Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")));
				anim.SetBool("isGrounded", grounded);
				anim.SetBool("atacando", false);
			}
		}

		grounded = false;
	}

	void OnCollisionStay()
	{
		grounded = true;
	}

	float CalculateJumpVerticalSpeed()
	{
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}

	IEnumerator Atacar()
	{
		atacando = true;
		yield return new WaitForSeconds(.3f);
		atacando = false;
		deveAtacar = false;
	}
}
