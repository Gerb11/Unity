﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ServerPlayerManager))]
public class ServerMain : MonoBehaviour {

	int listenPort = 25000;
	int maxClients = 5;
	 
	void startServer() {
	    Network.InitializeServer(maxClients, listenPort, false);    
	}
	 
	void stopServer() {
	    Network.Disconnect();
	}
	
	void OnGUI ()
	{
	    if (Network.peerType == NetworkPeerType.Disconnected) {
	        GUILayout.Label("Network server is not running.");
	        if (GUILayout.Button ("Start Server"))
	        {               
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
	        if (GUILayout.Button ("Stop Server"))
	        {               
	            stopServer();   
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
	    spm = (ServerPlayerManager) gameObject.GetComponent(typeof(ServerPlayerManager));
	}
	 
	void OnPlayerConnected(NetworkPlayer player) { 
		spm.spawnPlayer(player);
	}
	
	void OnPlayerDisconnected(NetworkPlayer player) {
	    spm.deletePlayer(player);
	}
}
