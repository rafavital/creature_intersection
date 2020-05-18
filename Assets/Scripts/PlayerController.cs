using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[System.Serializable] public class FloatEvent : UnityEvent <float> {}

public class PlayerController : MonoBehaviour {

	[SerializeField] private Animator animator;
	[SerializeField] private float walkSpeed = 2;
	[SerializeField] private float runSpeed = 6;
	[SerializeField] private float gravity = -12;
	[SerializeField] private float jumpHeight = 1;
	[Range(0,1)][SerializeField] private float airControlPercent;

	[Range (0,0.5f)] [SerializeField] private float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	[SerializeField] private float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;
	public float CurrentSpeed {
		get => currentSpeed; 
		set {
			currentSpeed = value; 
			animator.SetFloat ("CharacterSpeed", value/runSpeed);
		}
	}
	float velocityY;

	// Animator animator;
	Transform cameraT;
	CharacterController controller;

	void Start () {
		// animator = GetComponent<Animator> ();
		cameraT = Camera.main.transform;
		controller = GetComponent<CharacterController> ();
	}

	void Update () {
		// input
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		Vector2 inputDir = input.normalized;
		bool running = Input.GetKey (KeyCode.LeftShift);

		Move (inputDir, running);

		if (Input.GetKeyDown (KeyCode.Space)) {
			Jump ();
		}
		// animator
		float animationSpeedPercent = ((running) ? CurrentSpeed / runSpeed : CurrentSpeed / walkSpeed * .5f);
		animator.SetFloat ("CharacterSpeed", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

	}

	void Move(Vector2 inputDir, bool running) {
		if (inputDir != Vector2.zero) {
			float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
		}
			
		float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
		CurrentSpeed = Mathf.SmoothDamp (CurrentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

		velocityY += Time.deltaTime * gravity;
		Vector3 velocity = transform.forward * CurrentSpeed + Vector3.up * velocityY;

		controller.Move (velocity * Time.deltaTime);
		CurrentSpeed = new Vector2 (controller.velocity.x, controller.velocity.z).magnitude;

		if (controller.isGrounded) {
			velocityY = 0;
		}

	}

	void Jump() {
		if (controller.isGrounded) {
			float jumpVelocity = Mathf.Sqrt (-2 * gravity * jumpHeight);
			velocityY = jumpVelocity;
		}
	}

	float GetModifiedSmoothTime(float smoothTime) {
		if (controller.isGrounded) {
			return smoothTime;
		}

		if (airControlPercent == 0) {
			return float.MaxValue;
		}
		return smoothTime / airControlPercent;
	}
}