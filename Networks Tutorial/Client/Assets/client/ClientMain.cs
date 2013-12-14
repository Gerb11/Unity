using UnityEngine;
using System.Collections;

public class ClientMain : MonoBehaviour {

	string remoteIP = "127.0.0.1";
	int remotePort = 25000;
	 
	void connectToServer() {
	    Network.Connect(remoteIP, remotePort);  
	}
	 
	void disconnectFromServer() {
	    Network.Disconnect();
	}
	
	void OnGUI ()
	{   
	    if (Network.peerType == NetworkPeerType.Disconnected)
	    {   
	        GUILayout.Label("Not connected to server.");
	        if (GUILayout.Button ("Connect to server"))
	            connectToServer();                  
	    }
	    else
	    {
	        if(Network.peerType == NetworkPeerType.Connecting)
	            GUILayout.Label("Connecting to server...");
	        else {
	            GUILayout.Label("Connected to server.");
	            GUILayout.Label("IP/port: " + Network.player.ipAddress + "/" + Network.player.port);
	        }
	        if (GUILayout.Button ("Disconnect"))
	            disconnectFromServer();
	    }
	}
	
	void OnDisconnectedFromServer (NetworkDisconnection info) {
	    GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
	    foreach(GameObject go in gos) {
	        Destroy(go);
	    }
	}
	
	[RPC]
	void handlePlayerInput(NetworkPlayer player, float vertical, float horizontal) {
	}
	
	void Update() {
	    if(Input.anyKey) {
	        sendInputToServer();
	    }
	}
 
	void sendInputToServer() {
	    float vertical = Input.GetAxis("Vertical");
	    float horizontal = Input.GetAxis("Horizontal");
	    if((vertical!=0)||(horizontal!=0)) {
	        networkView.RPC("handlePlayerInput",RPCMode.Server ,Network.player, vertical, horizontal);
	    }
	}
}
