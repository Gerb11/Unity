using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {
	
	public static float distanceTraveled;
	
	public float acceleration;

	private bool touchingPlatform;
	
	private bool touchingSafetyNet;
	
	public Vector3 boostVelocity, jumpVelocity, jumpFromNetVelocity;
	
	private Vector3 startPosition;
	
	public static int score;
	
	public int doubleJump;

	void Start () {
		GameEventManager.GameStart += GameStart;
		startPosition = transform.localPosition;
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
	}

	private void GameStart () {
		score = 0;
		GUIManager.SetScore(score);
		distanceTraveled = 0f;
		transform.localPosition = startPosition;
		renderer.enabled = true;
		rigidbody.isKinematic = false;
		enabled = true;
	}
	
	void Update () {
		if(Input.GetButtonDown("Jump")){
			if(touchingPlatform){
				rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
				touchingPlatform = false;
				doubleJump = 1;
			} else if(touchingSafetyNet) {
				rigidbody.AddForce(jumpFromNetVelocity, ForceMode.VelocityChange);
				touchingSafetyNet = false;
				doubleJump = 1;
			} else if(doubleJump > 0){
					rigidbody.AddForce(boostVelocity, ForceMode.VelocityChange);
					doubleJump -= 1;
			}
		}
		distanceTraveled = transform.localPosition.x;
	}

	void FixedUpdate () {
		if(touchingPlatform || touchingSafetyNet){
			rigidbody.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
		}
	}

	void OnCollisionEnter (Collision other) {
		
		if(other.gameObject.tag == "Safety Net") {
			touchingSafetyNet = true;
			GUIManager.SetHighScore(score);
			score = 0;
			GUIManager.SetScore(score);
		} else {
			touchingPlatform = true;
		}

	}

	void OnCollisionExit () {
		touchingPlatform = false;
		touchingSafetyNet = false;
	}
	
	public static void AddBoost(){
		score += 1;
		GUIManager.SetScore(score);
		GUIManager.SetHighScore(score);
	}
	
}
