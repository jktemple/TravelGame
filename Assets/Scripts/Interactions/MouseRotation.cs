using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour
{
    Selection select;
    [SerializeField]
    float rotationSpeed = 1f;

    private void Start()
    {
        select = GameObject.FindFirstObjectByType<Selection>();
    }

    private void Update()
    {
        HandleRightClickDrag();
    }

    private bool isMouseDragging;
    private GameObject target;


    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject targetObject = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            targetObject = hit.collider.gameObject;
        }
        if (targetObject == this.gameObject) { return targetObject; }
        else return null;
        
    }
    /*
    private void OnMouseDrag()
    {
        if (!select.canRotate) { return; }
        Debug.Log("OnMouseDrag");
        float xAxisRot = Input.GetAxis("Mouse X") * -rotationSpeed;
        float yAxisRot = Input.GetAxis("Mouse Y") * rotationSpeed;

        transform.Rotate(-transform.up, xAxisRot);
        transform.Rotate(transform.right, yAxisRot);
    }
    */
    private void HandleRightClickDrag()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hitInfo;
            target = ReturnClickedObject(out hitInfo);
            if (target != null)
            {
                isMouseDragging = true;
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            isMouseDragging = false;
        }

        if (isMouseDragging && select.canRotate)
        {
            Debug.Log("OnMouseDrag");
            float xAxisRot = Input.GetAxis("Mouse X") * -rotationSpeed;
            float yAxisRot = Input.GetAxis("Mouse Y") * rotationSpeed;

            transform.Rotate(-transform.up, xAxisRot);
            transform.Rotate(transform.right, yAxisRot);
        }
    }
}
