using UnityEngine;
using System.Collections;

public class HumanPlayer : MonoBehaviour {
	
	/*void Update() {
		
		float rotation = 0; 
		if (controller.isGrounded) {
			// We are grounded, so recalculate
			// move direction directly from axes
			moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			
			rotation = Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed;
			
			if (Input.GetButton ("Jump")) {
				moveDirection.y = jumpSpeed;
			}
		}
		
		// Apply gravity
		moveDirection.y -= gravity * Time.deltaTime;
		
		// Move the controller
		controller.Move(moveDirection * Time.deltaTime);
		Camera.main.transform.position = new Vector3(this.transform.position.x, 2, this.transform.position.z);
		Camera.main.transform.Rotate(0, rotation, 0);
		
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
	*/
}
