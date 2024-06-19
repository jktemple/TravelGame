using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPins : MonoBehaviour
{
    [SerializeField]
    GameObject UIMapPin;
    [SerializeField]
    GameObject TableMapPin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMapPins()
    {
        UIMapPin.SetActive(true);
        TableMapPin.SetActive(true);
    }

    public void HideMapPins()
    {
        UIMapPin.SetActive(false);
        TableMapPin.SetActive(false);
    }
}
