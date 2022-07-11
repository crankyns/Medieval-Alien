using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreator : MonoBehaviour
{
    public Material meshMaterial;
    MeshFilter meshFilter;
    Mesh mesh;
    public List<Vector3> vertices = new List<Vector3>();

    public List<int> triangles = new List<int>();
    void Start()
    {
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = meshMaterial;
        meshFilter = gameObject.AddComponent<MeshFilter>();
      
        
    }


    void Update()
    {
        // meshFilter.mesh.Clear();
        // vertices.Add(new Vector3(0, 1, 0));
        // mesh = new Mesh();

		// mesh.vertices = vertices.ToArray();

        // triangles.Add(23);
        // triangles.Add(0);
        // triangles.Add(1);
        // for (int i = 0; i < vertices.Count - 3; i++)
        // {
        //     triangles.Add(23);
        //     triangles.Add(i);
        //     triangles.Add(i+2);
        // }


		// meshFilter.mesh = mesh;
        // triangles.Clear();
        // vertices.Clear();
        
    }

    
}
