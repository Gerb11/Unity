using UnityEngine;
using System.Collections;

public class SkeletonManager : MonoBehaviour {

	/// This script moves the character controller forward 
	/// and sideways based on the arrow keys.
	/// It also jumps when pressing space.
	/// Make sure to attach a character controller to the same game object.
	/// It is recommended that you make only one call to Move or SimpleMove per frame.	
	float speed = 7.0f;
	float jumpSpeed = 10.0f;
	float gravity = 20.0f;
	float turnSpeed  = 80.0f;
	private Vector3 moveDirection = Vector3.zero;
	private float rotation;
	
	private Vector3 lastPosition;
	private CharacterController controller;
	
	public bool it = false;
	void Start() {
		controller = GetComponent<CharacterController>();
		lastPosition = transform.position;
	}
	
	void Update() {
		
		if (controller.isGrounded) {
			// We are grounded, so recalculate
			// move direction directly from axes
			moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			
			float rotation = Input.GetAxis("Horizontal");
			transform.Rotate(0, rotation * Time.deltaTime * turnSpeed, 0);
			
			if (Input.GetButton ("Jump")) {
				moveDirection.y = jumpSpeed;
			}
		}
		
		// Apply gravity
		moveDirection.y -= gravity * Time.deltaTime;
		
		// Move the controller
		controller.Move(moveDirection * Time.deltaTime);
		
		if(charMoved()) {
			animation.Play("run");
		} else {
			animation.Play("idle");
		}
	}
	
	bool charMoved() {
		Vector3 displacement = transform.position - lastPosition;
 		lastPosition = transform.position;
  		return displacement.magnitude > 0.001; // return true if char moved 1mm
	}
	
	void OnTriggerEnter(Collider col) {
		if(col.transform.tag == "Enemy" && !it) {
			it = true;
		} else if(col.transform.tag == "Enemy") {
			it = false;
		}
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(hit.transform.tag=="Obstacle"){
				
		}
	}
}
