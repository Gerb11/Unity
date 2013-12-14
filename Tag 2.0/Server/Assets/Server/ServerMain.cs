using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ServerPlayerManager))]
public class ServerMain : MonoBehaviour {

	int listenPort = 25000;
	int maxClients = 4;
	
	int screenHeight;
	int screenWidth;
	 
	void startServer() {
	    Network.InitializeServer(maxClients, listenPort, false);    
	}
	 
	void stopServer() {
	    Network.Disconnect();
	}
	
	void OnGUI ()
	{
		
	    if (Network.peerType == NetworkPeerType.Disconnected) {
	        if(GUI.Button (new Rect (screenWidth -275, screenHeight - 130,100,25), "Start Game!"))
	        {   
				Application.LoadLevel("Tag");
				startServer();
	        }
	    }
	    else {
	        if (Network.peerType == NetworkPeerType.Connecting)
	            GUILayout.Label("Network server is starting up...");
	        else { 
	            GUILayout.Label("Network server is running.");          
	            showServerInformation();    
	            showClientInformation();
	        }
	        if (GUI.Button (new Rect (screenWidth -275, screenHeight - 130,100,25), "Stop Server"))
	        {               
	            stopServer();   
	        }
			if(GUI.Button (new Rect (screenWidth - 325, screenHeight - 90,200,25), "Change Game Options")) {
				Application.LoadLevel("Menu");
				stopServer();
				GameObject go = (GameObject) GameObject.FindGameObjectWithTag("GameController");
				Destroy(go); //make sure there's only ever one game controller
				go = (GameObject) GameObject.FindGameObjectWithTag("GameParams");
				Destroy(go);
			}
	    }
	}
	
	void showClientInformation() {
	    GUILayout.Label("Clients: " + Network.connections.Length + "/" + maxClients);
	    foreach(NetworkPlayer p in Network.connections) {
	        GUILayout.Label(" Player from ip/port: " + p.ipAddress + "/" + p.port); 
	    }
	}
	 
	void showServerInformation() {
	    GUILayout.Label("IP: " + Network.player.ipAddress + " Port: " + Network.player.port);  
	}
	
	private ServerPlayerManager spm;
	void Awake() {
		screenWidth = Screen.width;
		screenHeight = Screen.height;
	    spm = (ServerPlayerManager) gameObject.GetComponent(typeof(ServerPlayerManager));
		maxClients = int.Parse(SceneProperties.playerSize);
	}
	 
	void OnPlayerConnected(NetworkPlayer player) { 
		spm.spawnPlayer(player);
	}
	
	void OnPlayerDisconnected(NetworkPlayer player) {
	    spm.deletePlayer(player);
	}
}
