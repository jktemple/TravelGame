using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UIElements;

public class Selection : MonoBehaviour
{
    public Material highlightMaterial;
    public Material selectionMaterial;

    private Material originalMaterial;
    private Transform selection;
    private Transform highlight;
    private RaycastHit hit;



    public Transform targetPoint;

    [SerializeField]
    Volume shortDepthOfField;
    [SerializeField]
    Volume longDepthOfField;

    // Start is called before the first frame update
    void Start()
    {
        shortDepthOfField.weight = 0.0f;
        longDepthOfField.weight = 1.0f;
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
        if (hold != null && !inMotion)
        {
            currentObject = hold;
            startPos = currentObject.position;
            startRot = currentObject.rotation;
            //selection.position = targetPoint.position;
            //Debug.Log("starting grab");
            Item item = currentObject.GetComponent<Item>();
            rotation = false;
            if (item != null)
            {
                //Debug.Log("item found");
                rotation = item.shouldRotate;
                if (item.useDepthOfField)
                {
                    shortDepthOfField.weight = 1.0f;
                }

                if (currentObject.TryGetComponent<Photo>(out _))
                {
                    AudioManager.instance.Play("PageTurn");
                }
            }
            //Debug.Log(rotation);
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
            longDepthOfField.weight = 0.0f;
            
            
        }
    }

    public void ReturnItem()
    {
        if (!canReturnItem)
        {
            return;
        }
        //Debug.Log("letting go");
        StartCoroutine(MoveToPositionWithRotation(currentObject, targetPoint.position, startPos,currentObject.rotation, startRot, 0.25f));
        if (currentObject.TryGetComponent<Photo>(out _))
        {
            AudioManager.instance.Play("PageTurn");
        } else if(currentObject.TryGetComponent<ThreeDObject>(out _))
        {
            AudioManager.instance.Play("PutDownObject");
        }

        if(currentObject.TryGetComponent<DialogueTrigger>(out var dialogue))
        {
            if(dialogue.correctGuessString== "Singapore")
            {
                currentObject.tag = "Selectable";
            }
        } else { currentObject.tag = "Untagged"; }
        
        hasHeldItem = false;
        currentObject = null;
        longDepthOfField.weight = 1.0f;
        shortDepthOfField.weight = 0.0f;
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

    bool canRotate = true;
    private void RotateItem (Transform o)
    {
        if (!canRotate) { return; }
        if (Input.GetKey(KeyCode.W))
        {
            o.Rotate(new Vector3(1, 0, 0));
        } else if (Input.GetKey(KeyCode.S))
        {
            o.Rotate(new Vector3(-1, 0, 0));
        } else if (Input.GetKey(KeyCode.A))
        {
            o.Rotate(new Vector3(0, 0, 1));
        } else if (Input.GetKey(KeyCode.D))
        {
            o.Rotate(new Vector3(0, 0, -1));
        }else if (Input.GetKey(KeyCode.Q)) {
            o.Rotate(new Vector3(0, 1, 0));
        }
        else if (Input.GetKey(KeyCode.E))
        {
            o.Rotate(new Vector3(0, -1, 0));
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            if (rotation)
            {
                StartCoroutine(RotateObject(o, o.rotation, targetPoint.rotation, 0.2f));
            } else
            {
                StartCoroutine(RotateObject(o, o.rotation, Quaternion.Euler(Vector3.zero), 0.2f));
            }
        }
       
    }

    private IEnumerator RotateObject(Transform o, Quaternion start, Quaternion end, float time)
    {

        canRotate = false;
        float t = 0;
        while (t < 1)
        {
            o.rotation = Quaternion.Lerp(start, end, t);
            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        o.rotation = end;
        canRotate = true;
        yield return null;
    }


}

