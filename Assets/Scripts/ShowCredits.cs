using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCredits : MonoBehaviour
{
    [SerializeField]
    GameObject credits;
    [SerializeField]
    GameObject assets;

    // Start is called before the first frame update
    void Start()
    {
        credits.SetActive(false);
        assets.SetActive(false);   
    }

    public void RevealCredits() 
    {
        credits.SetActive(true);
        assets.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
