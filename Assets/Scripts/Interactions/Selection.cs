using System;
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    Transform currentObject;
    Vector3 startPos;
    Quaternion startRot;
    private bool hasHeldItem = false;
    public bool canReturnItem = false;


    // Update is called once per frame
    void Update()
    {
        if (!hasHeldItem) { HandleSelection(); }
        
        //cant select if already holding an object
        if(Input.GetKey(KeyCode.Mouse0) && selection != null && hasHeldItem == false && !inMotion)
        {
            
            HoldItem(selection);
        }

        if(Input.GetKey(KeyCode.Mouse0) && hasHeldItem && !inMotion)
        {
            ReturnItem();
        }

        if (hasHeldItem && currentObject != null)
        {
            RotateItem(currentObject);
        }
     
        
    }

    private void HandleSelection()
    {
        if (highlight != null)
        {
            //highlight.GetComponent<Renderer>().material = originalMaterial;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit))
        {
            highlight = hit.transform;
            if (highlight.CompareTag("Selectable") && highlight != selection)
            {
                /*
                if (highlight.GetComponent<MeshRenderer>().material != selectionMaterial)
                {

                    originalMaterial = highlight.GetComponent<MeshRenderer>().material;
                    //highlight.GetComponent<MeshRenderer>().material = highlightMaterial;
                }
                */
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
                ///selection.GetComponent<MeshRenderer>().material = originalMaterial;
                selection = null;
            }


            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit))
            {
                selection = hit.transform;

                if (selection.CompareTag("Selectable"))
                {
                    //selection.GetComponent<MeshRenderer>().material = selectionMaterial;

                }
                else
                {
                    selection = null;
                }
            }


        }
    }

    private bool inMotion = false;
    bool rotation;
    public void HoldItem(Transform hold)
    {
        if (Input.GetKey(KeyCode.Mouse0) && hold != null && !inMotion)
        {
            currentObject = hold;
            startPos = currentObject.position;
            startRot = currentObject.rotation;
            //selection.position = targetPoint.position;
            Debug.Log("starting grab");
            Item item = currentObject.GetComponent<Item>();
            rotation = false;
            if (item != null)
            {
                Debug.Log("item found");
                rotation = item.shouldRotate;
            }
            Debug.Log(rotation);
            if (rotation)
            {
                StartCoroutine(MoveToPositionWithRotation(currentObject, currentObject.position, targetPoint.position, startRot, targetPoint.rotation, 0.25f));
            }
            else
            {
                StartCoroutine(MoveToPositionNoRotation(hold, selection.position, targetPoint.position, 0.25f));
            }
            hasHeldItem = true;
            if(currentObject.TryGetComponent<DialogueTrigger>(out var dialogue))
            {
                dialogue.TriggerDialogue();
            }
        }
    }

    public void ReturnItem()
    {
        if (!canReturnItem)
        {
            return;
        }
        Debug.Log("letting go");
        StartCoroutine(MoveToPositionWithRotation(currentObject, targetPoint.position, startPos,currentObject.rotation, startRot, 0.25f));
        hasHeldItem = false;
        currentObject = null;
    }

    private IEnumerator MoveToPositionNoRotation(Transform o, Vector3 start, Vector3 targetLocation, float time)
    {

        inMotion = true;
        float t = 0;
        while (t < 1)
        {
            o.position = Vector3.Lerp(start, targetLocation, t);
            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        o.position = targetLocation;
        inMotion = false;
        yield return null;
    }

    private IEnumerator MoveToPositionWithRotation(Transform o, Vector3 start, Vector3 targetLocation, Quaternion startRot, Quaternion targetRot, float time)
    {

        inMotion = true;
        float t = 0;
        while (t < 1)
        {
            o.SetPositionAndRotation(Vector3.Lerp(start, targetLocation, t), Quaternion.Lerp(startRot, targetRot, t));
            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        o.SetPositionAndRotation(targetLocation, targetRot);
        inMotion = false;
        yield return null;
    }


    private void RotateItem (Transform o)
    {
        if (Input.GetKey(KeyCode.W))
        {
            o.Rotate(new Vector3(1, 0, 0));
        } else if (Input.GetKey(KeyCode.S))
        {
            o.Rotate(new Vector3(-1, 0, 0));
        } else if (Input.GetKey(KeyCode.A))
        {
            o.Rotate(new Vector3(0,0,1));
        } else if (Input.GetKey(KeyCode.D))
        {
            o.Rotate(new Vector3(0, 0, -1));
        } else if (Input.GetKey(KeyCode.Mouse1))
        {
            if (rotation)
            {
                o.rotation = targetPoint.rotation;
            } else
            {
                o.rotation = Quaternion.Euler(Vector3.zero);
            }
        }
       
    }
   

}

