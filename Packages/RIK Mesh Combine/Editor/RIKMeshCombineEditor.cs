using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RIK.MeshCombine;

[CustomEditor(typeof(RIKMeshCombineScript))]
public class RIKMeshCombineEditor : Editor{

    public override void OnInspectorGUI(){

        var meshCombineTarget = (RIKMeshCombineScript)target;
        
        if(GUILayout.Button("Combine"))
            meshCombineTarget.Run();

    }
   
}