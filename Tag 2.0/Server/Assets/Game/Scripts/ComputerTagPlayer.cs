using UnityEngine;
using System.Collections;

public class ComputerTagPlayer : MonoBehaviour {
	
	float jumpSpeed = 10.0f;
	float gravity = 20.0f;
	float speed = 6.0f;
	
	private Quaternion rotation;
	
	public Vector3 randomStartPos;
	public HumanPlayer player;
	
	private Vector3 lastPosition = Vector3.zero;
	private Vector3 playerPos = Vector3.zero;
	private Vector3 moveDirection = Vector3.zero;
	
	private CharacterController controller;
	private Quaternion newRotation;
	
	public bool it = true;
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		lastPosition = transform.position;
		playerPos = player.transform.position;
	}
	
	void Update() {
		playerPos = player.transform.position;
		
		if(it) {
			moveDirection = (playerPos - transform.position).normalized;
			newRotation = Quaternion.LookRotation(playerPos - transform.position, Vector3.up);
		} else {
			moveDirection = (transform.position - playerPos).normalized;
			newRotation = Quaternion.LookRotation(transform.position - playerPos, Vector3.up);
		}
		moveDirection *= speed;
		
		// Apply gravity
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
		
		newRotation.x = 0.0f;
   	 	newRotation.z = 0.0f;
    	transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8);
		
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
		if(col.transform.tag == "Player" && !it) {
			it = true;
			transform.position = new Vector3(Random.Range(-23, 23), 0, Random.Range(-23, 23));
		} else if(col.transform.tag == "Player") {
			it = false;
			transform.position = new Vector3(Random.Range(-23, 23), 0, Random.Range(-23, 23));
		}
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(controller.isGrounded) {
			if(hit.transform.tag=="Obstacle" && it){
				moveDirection.y = jumpSpeed;
				controller.Move(moveDirection * Time.deltaTime);
			}
		}
	}
	
}