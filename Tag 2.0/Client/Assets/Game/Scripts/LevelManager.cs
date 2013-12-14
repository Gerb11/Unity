using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	
	void Start () {
		
		LevelInfo info = (LevelInfo) GameObject.FindObjectOfType(typeof(LevelInfo));
		Transform floor = (Transform) info.floorPrefab;
		
		int levelSize = int.Parse(SceneProperties.levelSize.Split('x')[0]);
		Vector3 scale = new Vector3(levelSize, 20, 1);
		
		floor.localScale = new Vector3(levelSize, 0.001f, levelSize);
		Network.Instantiate(floor, floor.position, floor.rotation, 0);
		
		Transform wall = (Transform) info.wallPrefab;
		wall.localScale = scale;
		wall.localPosition = new Vector3(0, .5f, -(levelSize / 2));
		wall.Rotate(0,90,0);
		Network.Instantiate(wall, wall.localPosition, wall.localRotation, 0);
		
		wall.localPosition = new Vector3((levelSize / 2), .5f, 0);
		wall.Rotate(0,90,0);
		Network.Instantiate(wall, wall.localPosition, wall.localRotation, 0);
		
		wall.localPosition = new Vector3(0, .5f, (levelSize / 2));
		wall.Rotate(0,90,0);
		Network.Instantiate(wall, wall.localPosition, wall.localRotation, 0);
		
		wall.localPosition = new Vector3(-(levelSize / 2), .5f, 0);
		wall.Rotate(0,90,0);
		Network.Instantiate(wall, wall.localPosition, wall.localRotation, 0);
		
	}
	
}
