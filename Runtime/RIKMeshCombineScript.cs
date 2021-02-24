using UnityEngine;
using UnityEditor;

namespace RIK.MeshCombine{

    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class RIKMeshCombineScript : MonoBehaviour{
        
        // Start is called before the first frame update
        public void Run(){

            #if UNITY_EDITOR

            Vector3 oriPos = transform.position;
            transform.position = Vector3.zero;

            int i = 1;

            MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];
            
            while (i < meshFilters.Length)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.SetActive(false);

                i++;
            }

            Mesh mesh = new Mesh();
            mesh.CombineMeshes(combine, true, true);
            Unwrapping.GenerateSecondaryUVSet(mesh);

            var targetMeshFilter = transform.GetComponent<MeshFilter>();
            targetMeshFilter.mesh = new Mesh();
            targetMeshFilter.sharedMesh = mesh;
            //transform.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine, true, true, true);
            transform.GetComponent<Renderer>().sharedMaterials = meshFilters[1].gameObject.GetComponent<Renderer>().sharedMaterials;
            transform.gameObject.SetActive(true);

            transform.position = oriPos;

            #endif

        }

        public int GetPolygonCount(){
            
            int polygonCount = 0;

            MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
            int detectedCount = meshFilters.Length;

            for(int i = 1; i < detectedCount; i++){

                polygonCount += meshFilters[i].sharedMesh.triangles.Length;


            }

            return polygonCount;

        }

        public void RevertSetting(){

            //Delete the combined mesh
            GetComponent<MeshFilter>().mesh = null;

            //Active all child
            int childCount = transform.childCount;

            for(int i = 0; i < childCount; i++){
                
                Transform currentTrans = transform.GetChild(i);
                currentTrans.gameObject.SetActive(true);

                //Check is this have child
                ChildActiveRevert(currentTrans);

            }

        }

        void ChildActiveRevert(Transform currentTrans) {
            
            int childCount = currentTrans.childCount;

            if(childCount > 0){
                
                for(int i = 0; i < childCount; i++){

                    Transform childTrans = currentTrans.GetChild(i);
                    childTrans.gameObject.SetActive(true);

                    ChildActiveRevert(childTrans);  //Recursive check


                }

            }

        }

    }

}