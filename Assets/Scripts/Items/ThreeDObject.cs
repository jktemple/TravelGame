using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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
    [SerializeField]
    ThreeDObject secondObject;
  

    int index = 0;
    public override void NextVersion()
    {
        if (index < materials.Length - 1)
        {
            index++;
            render.material = materials[index];
            if (index > meshes.Length)
            {
                meshFilter.gameObject.SetActive(false);
            }
            else
            {
                meshFilter.mesh = meshes[index];
            }
        }
        if(secondObject!= null)
        {
            secondObject.NextVersion();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.name);
        meshFilter.mesh = meshes[0];
        render.material = materials[0];
  
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
