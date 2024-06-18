using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField]
    Selection selector;
    [SerializeField]
    Transform startingObject;
    [SerializeField]
    float delayTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(StartGame), delayTime);
    }

    void StartGame()
    {
        selector.HoldItem(startingObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
