using UnityEngine;
using System.Collections;

public class ServerPlayerManager : MonoBehaviour {
	
	Hashtable players = new Hashtable();
 
	public void spawnPlayer(NetworkPlayer player) {
	    PlayerInfo ply = (PlayerInfo) GameObject.FindObjectOfType(typeof(PlayerInfo));
	    GameObject go = (GameObject) Network.Instantiate(ply.playerPrefab, Vector3.up*3, Quaternion.identity, 0);
	    players[player] = go;
	}
	
	public void deletePlayer(NetworkPlayer player) {
	    GameObject go = (GameObject) players[player];
	    Network.RemoveRPCs(go.networkView.viewID); 
	    Network.Destroy(go); 
	    players.Remove(player); 	
	}
	
	[RPC]
	public void handlePlayerInput(NetworkPlayer player, float vertical, float horizontal) {
	    GameObject go = (GameObject) players[player];
	    go.transform.position = go.transform.position + Vector3.right*horizontal;
	    go.transform.position = go.transform.position + Vector3.forward*vertical;
	}
	
}
