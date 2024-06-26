using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photo : Item
{
    

    [SerializeField]
    Material[] materials;
    [SerializeField]
    Renderer render;
    [SerializeField]
    Photo secondPhoto;
    private int index = 0;

    public override void NextVersion()
    {
        if(index < materials.Length-1) {
            index++;
            render.material = materials[index]; 
        } 
        if(secondPhoto!= null)
        {
            secondPhoto.NextVersion();
        }
      
    }

    // Start is called before the first frame update
    void Start()
    {
        render.material = materials[index];
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyUp(KeyCode.P)) { NextVersion(); }
        
    }
}
