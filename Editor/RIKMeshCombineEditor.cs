using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RIK.MeshCombine;

namespace RIK.MeshCombine {

    [CustomEditor(typeof(RIKMeshCombineScript))]
    public class RIKMeshCombineEditor : Editor {

        int totalPolygon = 0;
        const int maxPolygon = 65536;

        public override void OnInspectorGUI() {

            var meshCombineTarget = (RIKMeshCombineScript)target;

            if (GUILayout.Button("Combine")) {

                totalPolygon = meshCombineTarget.GetPolygonCount();

                if (totalPolygon <= maxPolygon) {

                    meshCombineTarget.Run();

                } else {

                    EditorUtility.DisplayDialog("Couldn't Perform combine",
                        string.Format("Current polygon detected {0} is more than max support polygon {1}", totalPolygon, maxPolygon),
                        "Okayyyy");

                }

            }

            if (GUILayout.Button("Update Detected Polygon")) {

                totalPolygon = meshCombineTarget.GetPolygonCount();

            }

            EditorGUILayout.HelpBox(string.Format("Current polygon detected {0} / {1}", totalPolygon, maxPolygon), MessageType.Info);

            if(GUILayout.Button("Revert")){
                
                meshCombineTarget.RevertSetting();

            }

        }

    }

}