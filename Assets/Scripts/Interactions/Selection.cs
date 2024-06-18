using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour
{
    public Material highlightMaterial;
    public Material selectionMaterial;

    private Material originalMaterial;
    private Transform selection;
    private Transform highlight;
    private RaycastHit hit;


    public Transform targetPoint;
 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(highlight != null)
        {
            highlight.GetComponent<Renderer>().material = originalMaterial;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit))
        {
            highlight = hit.transform;
            
            if(highlight.CompareTag("Selectable") && highlight != selection)
            {
                if(highlight.GetComponent<MeshRenderer>().material != selectionMaterial)
                {
                    originalMaterial = highlight.GetComponent<MeshRenderer>().material;
                    highlight.GetComponent<MeshRenderer>().material = highlightMaterial;
                }
            }
            else
            {

               highlight = null;
            }
        }

        if(Input.GetKey(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
        {
           if(selection != null)
           {
               selection.GetComponent<MeshRenderer>().material = originalMaterial;
               selection = null;
           }

           if(!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit))
           {
               selection = hit.transform;

               if(selection.CompareTag("Selectable"))
               {
                   selection.GetComponent<MeshRenderer>().material = selectionMaterial;
                   
               }
                else
                {
                    selection = null;
                }
           }
        
        }

        if(selection != null && selection != hit.transform)
        {
            selection.GetComponent<MeshRenderer>().material = originalMaterial;
            selection = null;
        }

        if(Input.GetKey(KeyCode.Mouse0) && selection != null)
        {
            selection.position = targetPoint.position;
        }
       
           
    }
}

