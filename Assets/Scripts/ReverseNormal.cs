using UnityEngine;
using System.Collections;

public class ReverseNormal : MonoBehaviour
{
    public GameObject sphere;

    void Awake()
    {
        InvertSphere();
    }

    void InvertSphere()
    {
        Vector3[] normals = sphere.GetComponent<MeshFilter>().mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        sphere.GetComponent<MeshFilter>().sharedMesh.normals = normals;

        int[] triangles = sphere.GetComponent<MeshFilter>().sharedMesh.triangles;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            int t = triangles[i];
            triangles[i] = triangles[i + 2];
            triangles[i + 2] = t;
        }

        sphere.GetComponent<MeshFilter>().sharedMesh.triangles = triangles;
    }
}