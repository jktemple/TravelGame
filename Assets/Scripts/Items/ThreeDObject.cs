using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDObject : Item
{
    [SerializeField]
    Mesh[] meshes;
    [SerializeField]
    Material[] materials;
    [SerializeField]
    MeshFilter meshFilter;
    [SerializeField]
    Renderer render;

    int index = 0;
    public override void NextVersion()
    {
        if (index < materials.Length - 1)
        {
            index++;
            render.material = materials[index];
            meshFilter.mesh = meshes[index];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        meshFilter.mesh = meshes[0];
        render.material = materials[0];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
