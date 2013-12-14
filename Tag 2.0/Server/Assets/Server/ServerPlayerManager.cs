using UnityEngine;
using System.Collections;

public class ServerPlayerManager : MonoBehaviour {
	
	Hashtable players = new Hashtable();
	Hashtable whosIt = new Hashtable();
	Hashtable playerNum = new Hashtable();
	
	float speed = 7.0f;
	float jumpSpeed = 10.0f;
	float gravity = 20.0f;
	float turnSpeed  = 80.0f;
	private float rotation;
	Vector3 moveDirection = Vector3.zero;
	
	private Vector3 lastPosition;
	private CharacterController controller;
	
 
	public void spawnPlayer(NetworkPlayer player) {
	    PlayerInfo ply = (PlayerInfo) GameObject.FindObjectOfType(typeof(PlayerInfo));
	    GameObject go = (GameObject) Network.Instantiate(ply.playerPrefabFresh, Vector3.up*3, Quaternion.identity, 0);
		
		if(players.Count == 0) {
			whosIt[go.GetHashCode()] = true;
			
			//create message to tell the clients who is it
			MessageInfo messInfo = (MessageInfo) GameObject.FindObjectOfType(typeof(MessageInfo));
			Transform o = (Transform) messInfo.messPrefab;
			o.localPosition = new Vector3(0.4f, 1, 0);
			o.Rotate(new Vector3(0,0,0));
			o.localScale = new Vector3(1,1,1);
			Network.Instantiate(o, o.position, o.rotation, 0);
		} else {
			whosIt[go.GetHashCode()] = false;
		}
	    players[player] = go;
		playerNum[go.GetHashCode()] = players.Count;
	}
	
	public void deletePlayer(NetworkPlayer player) {
	    GameObject go = (GameObject) players[player];
	    Network.RemoveRPCs(go.networkView.viewID); 
	    Network.Destroy(go); 
	    players.Remove(player); 	
	}
	
	[RPC]
	void amIIt(NetworkPlayer player){
	}
	
	[RPC]
	void handlePlayerInput(NetworkPlayer player, float vertical, float horizontal, bool jump) {
		GameObject go = (GameObject) players[player];
		CharacterController controller = (CharacterController) go.GetComponent<CharacterController>();
		Animation anim = (Animation) go.GetComponent<Animation>();
		anim.Play("run");
		
		if (controller.isGrounded) {
			// We are grounded, so recalculate
			// move direction directly from axes
			moveDirection = new Vector3(0, 0, vertical);
			moveDirection = go.transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			
			rotation = horizontal * Time.deltaTime * turnSpeed;
			go.transform.Rotate(0, rotation, 0);
			
			if (jump) {
				moveDirection.y = jumpSpeed;
			}
		}
		// Apply gravity
		moveDirection.y -= gravity * Time.deltaTime;
		//move controller
		controller.Move(moveDirection * Time.deltaTime);
		
		bool isIt = (bool) whosIt[go.GetHashCode()];
		Vector3 playerPos = go.transform.position;
		if(isIt) { //if player is it, find if touching other players
			foreach(GameObject gg in players.Values) {
				GameObject g = (GameObject) gg;
				if(g.GetHashCode() != go.GetHashCode()){ //don't compare same object
					float distance = Vector3.Distance(g.transform.position, playerPos);
					if(distance < 3) {
						g.transform.position = Vector3.up*3; //when you're it, teleport to middle
						whosIt[go.GetHashCode()] = false;
						whosIt[g.GetHashCode()] = true;
						
						GameObject [] gObjects = GameObject.FindGameObjectsWithTag("GuiMessage");
						foreach(GameObject gObject in gObjects) {
							Network.RemoveRPCs(gObject.networkView.viewID);
	    					Network.Destroy(gObject);
						}
						
						int playerIt = (int) playerNum[g.GetHashCode()];
						MessageInfo messInfo = (MessageInfo) GameObject.FindObjectOfType(typeof(MessageInfo));
						Transform o = null;
						if(playerIt == 1) {//showing correct message across all clients depending on which player is it.
							o = (Transform) messInfo.messPrefab;
						} else if(playerIt == 2) {
							o = (Transform) messInfo.messPrefab2;
						} else if(playerIt == 3) {
							o = (Transform) messInfo.messPrefab3;
						} else if(playerIt == 4) {
							o = (Transform) messInfo.messPrefab4;
						}
						
						o.localPosition = new Vector3(0.4f, 1, 0);
						o.Rotate(new Vector3(0,0,0));
						o.localScale = new Vector3(1,1,1);
						Network.Instantiate(o, o.position, o.rotation, 0);
						
					}
				}
			}
		}
	}
	
}
