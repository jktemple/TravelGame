using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    public PlayerControls playerControls;
    private void Awake()
    {
        if (instance != null) Debug.LogWarning("Found more than one InputManagerInstance");
        instance = this; 
        playerControls = new PlayerControls();
    }

    public static InputManager Instance() { return instance; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(playerControls.UI.Submit.WasPressedThisFrame()) Debug.Log(playerControls.UI.Submit.WasPressedThisFrame());

    }
}
