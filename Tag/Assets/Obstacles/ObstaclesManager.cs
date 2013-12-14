using UnityEngine;
using System.Collections.Generic;

public class ObstaclesManager : MonoBehaviour {
	
	public Transform prefab;
	public Vector3 minPosition, maxPosition;
	public Vector3 minScale, maxScale;
	public int numberOfObstacles;
	
	// Use this for initialization
	void Start () {
		for(int i= 0; i < numberOfObstacles; i++) {
			Transform o = (Transform)Instantiate(prefab);
			
			Vector3 position = new Vector3(
				Random.Range(minPosition.x, maxPosition.x),
												.5f,
				Random.Range(minPosition.z, maxPosition.z));
			
			Vector3 scale = new Vector3(
				Random.Range(minPosition.x, maxPosition.x),
												3,
												1);
			
			Vector3 rotation = new Vector3(
												0,
								Random.Range(0, 180),
												0);
			
			o.localPosition = position;
			o.localScale = scale;
			o.Rotate(rotation); 
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
