using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(RandomPlaneCreator))]
public class RandomPlaneCreatorInspector : Editor {

	public override void OnInspectorGUI () {
		DrawDefaultInspector();

		if(GUILayout.Button("Count tiles")) {
			RandomPlaneCreator plane = (RandomPlaneCreator)target;
			plane.SetTileChances();
		}

		if (GUILayout.Button("Regenerate")) {
			RandomPlaneCreator plane = (RandomPlaneCreator)target;
			plane.BuildMesh();
		}


	}

}
