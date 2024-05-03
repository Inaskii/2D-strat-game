using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGrowth : MonoBehaviour
{
    public List<Vector3> vertices = new List<Vector3>();
    public List<Vector2> uv = new List<Vector2>();
    public List<int> triangles = new List<int>();
    Mesh mesh;
    public GameObject mouse;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void Update()
    {
        
        for(int i =0;i<vertices.Count;i++)
        {
            Vector3 dir = (mouse.transform.position - vertices[i]).normalized/100;
            vertices[i] += dir;
        }

        mesh.vertices = vertices.ToArray();
        mesh.uv = uv.ToArray();
        mesh.triangles = triangles.ToArray();
    }
}
