using UnityEngine;
using System.Collections;

public class SceneProperties : MonoBehaviour {

	public static string levelSize = "75x75"; //set defaults
	public static string obstacleSize = "50";
	public static string playerSize = "4";
	
	void Awake() {
		DontDestroyOnLoad(this);
	}
}
