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
        HideMapPins();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMapPins()
    {
        if (UIMapPin != null) { UIMapPin.SetActive(true); }
        if (TableMapPin != null) { TableMapPin.SetActive(true); }
        
    }

    public void HideMapPins()
    {
        if (UIMapPin != null) { UIMapPin.SetActive(false); }
        if (TableMapPin != null) { TableMapPin.SetActive(false); }

    }
}
