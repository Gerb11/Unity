using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	public string levelSize;
	public string playerSize;
	public string obstacleSize;
	
	private bool showListOb = false;
	private bool showListPlayers = false;
	private bool showListSize = false;
	private int listEntryOb = 0;
	private int listEntryPlayers = 0;
	private int listEntrySize = 0;
	private GUIContent [] guiContentOb;
	private GUIContent [] guiContentPlayers;
	private GUIContent [] guiContentSize;
	private GUIStyle guiStyle;
	private bool pickedOb = false;
	private bool pickedPlayers = false;
	private bool pickedSize = false;
	
	void Start() {
		guiContentOb = new GUIContent[5];
		guiContentOb[0] = new GUIContent("0");
		guiContentOb[1] = new GUIContent("10");
		guiContentOb[2] = new GUIContent("20");
		guiContentOb[3] = new GUIContent("50");
		guiContentOb[4] = new GUIContent("75");
		
		guiContentPlayers = new GUIContent[3];
		guiContentPlayers[0] = new GUIContent("2");
		guiContentPlayers[1] = new GUIContent("3");
		guiContentPlayers[2] = new GUIContent("4");
		
		guiContentSize = new GUIContent[3];
		guiContentSize[0] = new GUIContent("25x25");
		guiContentSize[1] = new GUIContent("50x50");
		guiContentSize[2] = new GUIContent("75x75");
		
		guiStyle = new GUIStyle();
		guiStyle.normal.textColor = Color.white;
		Texture2D tex = new Texture2D(2,2);
		Color [] colors = new Color[5];
		for(int i = 0; i < 5; i++) {
			colors[i] = Color.white;
		}
		tex.SetPixels(colors);
		tex.Apply();
		guiStyle.hover.background = tex;
		guiStyle.onHover.background = tex;
		guiStyle.padding.left = guiStyle.padding.right = guiStyle.padding.bottom = guiStyle.padding.top = 4;
		
	}
	
	// Update is called once per frame
	void Update () {
		animation.Play("run");
	}
	
	void OnGUI () {
		int screenHeight = Screen.height;
		if(Popup.List( new Rect(5, screenHeight - 200,150,25), ref showListOb, ref listEntryOb, new GUIContent("Number Of Obstacles: "), guiContentOb, guiStyle)) {
			pickedOb = true;	
		}
		if(Popup.List( new Rect(170, screenHeight - 200,150,25), ref showListPlayers, ref listEntryPlayers, new GUIContent("Number Of Players: "), guiContentPlayers, guiStyle)) {
			pickedPlayers = true;	
		}
		if(Popup.List( new Rect(335, screenHeight - 200,120,25), ref showListSize, ref listEntrySize, new GUIContent("Size Of Map: "), guiContentSize, guiStyle)) {
			pickedSize = true;	
		}
		if (pickedOb) {
			GUIStyle gs = new GUIStyle();
			gs.normal.textColor = Color.black;
			GUI.Label (new Rect(75, screenHeight - 160,150,25), guiContentOb[listEntryOb].text, gs);
			SceneProperties.obstacleSize = guiContentOb[listEntryOb].text;
		}
		if (pickedPlayers) {
			GUIStyle gs = new GUIStyle();
			gs.normal.textColor = Color.black;
			GUI.Label (new Rect(240, screenHeight - 160,150,25), guiContentPlayers[listEntryPlayers].text, gs);
			SceneProperties.playerSize = guiContentPlayers[listEntryPlayers].text;
		}
		if (pickedSize) {
			GUIStyle gs = new GUIStyle();
			gs.normal.textColor = Color.black;
			GUI.Label (new Rect(365, screenHeight - 160,150,25), guiContentSize[listEntrySize].text, gs);
			SceneProperties.levelSize = guiContentSize[listEntrySize].text;
		}
		
	}
}
