using UnityEngine;

namespace RIK.MeshCombine{

    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class RIKMeshCombineScript : MonoBehaviour{
        
        // Start is called before the first frame update
        public void Run(){

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

            transform.GetComponent<MeshFilter>().mesh = new Mesh();
            transform.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine, true, true, true);
            transform.GetComponent<Renderer>().sharedMaterials = meshFilters[1].gameObject.GetComponent<Renderer>().sharedMaterials;
            transform.gameObject.SetActive(true);

            transform.position = oriPos;

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

    }

}