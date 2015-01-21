using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(EnemyControllerNAV))]
public class EnemyControllerNAVInspector : Editor {
    
    public override void OnInspectorGUI () {
        DrawDefaultInspector();
        
        if(GUILayout.Button("addCam")) {
            EnemyControllerNAV Enemy = (EnemyControllerNAV)target;
            Enemy.addCamera();
        }

        if(GUILayout.Button("delCam")) {
            EnemyControllerNAV Enemy = (EnemyControllerNAV)target;
            Enemy.delCamera();
        }
        
        
    }
    
}
