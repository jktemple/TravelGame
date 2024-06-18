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

    Transform currentObject;
    Vector3 startPos;
    private bool hasHeldItem = false;


    // Update is called once per frame
    void Update()
    {
        HandleSelection();
        //cant select if already holding an object
        if(Input.GetKey(KeyCode.Mouse0) && selection != null && hasHeldItem == false && !inMotion)
        {
            
            HoldItem();
        }

        if(Input.GetKey(KeyCode.Mouse0) && hasHeldItem && !inMotion)
        {
            ReturnItem();
        }

        if (hasHeldItem && currentObject != null)
        {
            rotateItem(currentObject);
        }
     
        
    }

    private void HandleSelection()
    {
        if (highlight != null)
        {
            highlight.GetComponent<Renderer>().material = originalMaterial;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit))
        {
            highlight = hit.transform;
            isselected = false;
            if (highlight.CompareTag("Selectable") && highlight != selection)
            {
                if (highlight.GetComponent<MeshRenderer>().material != selectionMaterial)
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



        if (Input.GetKey(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (selection != null)
            {
                selection.GetComponent<MeshRenderer>().material = originalMaterial;
                selection = null;
                isselected = true;
            }


            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit))
            {
                selection = hit.transform;

                if (selection.CompareTag("Selectable"))
                {
                    selection.GetComponent<MeshRenderer>().material = selectionMaterial;

                }
                else
                {
                    selection = null;
                }
            }


        }
    }

    private bool inMotion = false;
    public void HoldItem()
    {
        if (Input.GetKey(KeyCode.Mouse0) && selection != null && !inMotion)
        {
            currentObject = selection;
            startPos = currentObject.position;
            //selection.position = targetPoint.position;
            Debug.Log("starting grab");
            StartCoroutine(MoveToPosition(selection, selection.position, targetPoint.position, 0.25f));
            hasHeldItem = true;
        }
    }

    private void ReturnItem()
    {
        Debug.Log("letting go");
        StartCoroutine(MoveToPosition(currentObject, targetPoint.position, startPos, 0.25f));
        hasHeldItem = false;
        currentObject = null;

        
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


    private void rotateItem (Transform o)
    {
        if (Input.GetKey(KeyCode.W))
        {
            o.Rotate(Vector3.right, 1);
        }

        if (Input.GetKey(KeyCode.S))
        {
            o.Rotate(Vector3.right, -1);
        }

        if (Input.GetKey(KeyCode.A))
        {
            o.Rotate(Vector3.forward, 1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            o.Rotate(Vector3.forward, -1);
        }
       
    }
   

}

