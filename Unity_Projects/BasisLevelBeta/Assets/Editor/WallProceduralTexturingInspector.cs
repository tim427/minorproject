using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(WallProceduralTexturing))]
public class WallProceduralTexturingInspector : Editor {
	
	public override void OnInspectorGUI () {
		DrawDefaultInspector();

		if(GUILayout.Button("Count tiles")) {
			WallProceduralTexturing texture = (WallProceduralTexturing)target;
			texture.SetTileChances();
		}

		if (GUILayout.Button("Regenerate")) {
			WallProceduralTexturing builder = (WallProceduralTexturing)target;
			builder.BuildTexture();
		}
	}
	
}
