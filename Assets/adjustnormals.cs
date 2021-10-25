using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adjustnormals : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var mf = GetComponent<MeshFilter>();
        var mesh = mf.mesh;
        //mesh.RecalculateNormals();
        //mesh.RecalculateBounds();
        //mesh.RecalculateTangents();
        Debug.Log($"Recalculating normals for {name}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
