using UnityEngine;
using System.Collections;
using System;

public class ClientMain : MonoBehaviour {

	string remoteIP = "127.0.0.1";
	string remotePort = "25000";
	
	int screenWidth = Screen.width;
	int screenHeight = Screen.height;
	 
	void connectToServer() {
	    Network.Connect(remoteIP, int.Parse(remotePort));  
	}
	 
	void disconnectFromServer() {
	    Network.Disconnect();
	}
	
	void OnGUI ()
	{   
	    if (Network.peerType == NetworkPeerType.Disconnected)
	    {   
			if(GUI.Button (new Rect (screenWidth -275, screenHeight - 130,100,25), "Start Game!"))
	        {   
				Application.LoadLevel("Tag");
	            connectToServer();
			}                  
			remoteIP = GUI.TextField(new Rect (screenWidth -175, screenHeight - 130,100,25), remoteIP, 25);
			remotePort = GUI.TextField(new Rect (screenWidth -175, screenHeight - 100,100,25), remotePort, 25);
			GUI.Button(new Rect (screenWidth -275, screenHeight - 100,100,25), "Remote Port: ");
	    }
	    else
	    {
	        if(Network.peerType == NetworkPeerType.Connecting)
			{	
			}
	        else {
				if (GUILayout.Button ("Disconnect")){
	            	disconnectFromServer();
				}
	        }
	    }
	}
	
	void OnDisconnectedFromServer (NetworkDisconnection info) {
	    GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
	    foreach(GameObject go in gos) {
	        Destroy(go);
	    }
	}
	
	[RPC]
	void handlePlayerInput(NetworkPlayer player, float vertical, float horizontal, bool jump) {
	}
	
	[RPC]
	void amIIt(NetworkPlayer player) {	
	}
	
	void Update() {
		sendInputToServer();
	}
 
	/*
	 * Used to send the keystrokes from the client to the server to update the player accordingly
	 */
	void sendInputToServer() {
	    float vertical = Input.GetAxis("Vertical");
	    float horizontal = Input.GetAxis("Horizontal");
		bool jump = Input.GetButton ("Jump");
	    if(Network.peerType != NetworkPeerType.Connecting && Network.peerType != NetworkPeerType.Disconnected) {
	        networkView.RPC("handlePlayerInput",RPCMode.Server ,Network.player, vertical, horizontal, jump);
	    }
	}
}
