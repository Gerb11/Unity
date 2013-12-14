using UnityEngine;
using System.Collections.Generic;

public class ObstaclesManager : MonoBehaviour {
	
	public Vector3 minPosition, maxPosition;
	public int numberOfObstacles;
	
	// Use this for initialization
	void Start () {
		int levelSize = int.Parse(SceneProperties.levelSize.Split('x')[0]);
		
		numberOfObstacles = int.Parse(SceneProperties.obstacleSize);
		//Random.seed = System.DateTime.Now;
		for(int i= 0; i < numberOfObstacles; i++) {
			LevelInfo info = (LevelInfo) GameObject.FindObjectOfType(typeof(LevelInfo));
			Transform o = (Transform) info.obstaclePrefab;
			
			Vector3 position = new Vector3(
				Random.Range(-(levelSize) / 2, levelSize / 2),
												.5f,
				Random.Range(-(levelSize) / 2, levelSize / 2));
			
			float randomXScale = Random.Range(-(levelSize) / 2, levelSize / 2);
			Vector3 scale = new Vector3(
									randomXScale,
												3,
												1);
			
			Vector3 rotation = new Vector3(0, Random.Range(0, 180), 0);
			
			o.localPosition = position;
			o.localScale = scale;
			o.Rotate(rotation);
			o.renderer.material.mainTextureScale = new Vector2(randomXScale, 3); //sets the texture tiling to take into account the different size objects
			
			Network.Instantiate(o, o.position, o.rotation, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
