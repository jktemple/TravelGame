using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private bool isselected = false;



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
            isselected = false;
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
               isselected = true;
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
        
        //cant select if already holding an object
        if(selection != null && isselected == false)
        {
            holdItem();
        }
 
           
    }
    private bool inMotion = false;
    public void holdItem()
    {
        if (Input.GetKey(KeyCode.Mouse0) && selection != null && !inMotion)
        {
            //selection.position = targetPoint.position;
            Debug.Log("starting grab");
            StartCoroutine(MoveToPosition(selection, selection.position, targetPoint.position, 0.25f));
        }
    }

    
    private IEnumerator MoveToPosition(Transform o, Vector3 start, Vector3 targetLocation, float time)
    {
        inMotion = true;
        float t = 0;
        while (t < 1)
        {
            o.position = Vector3.Lerp(start, targetLocation, t);
            t = t + Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        o.position = targetLocation;
        inMotion = false;
        yield return null;
    }
}

