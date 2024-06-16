using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public void StartGame()
    {
        GetComponent<DialogueTrigger>().TriggerDialogue();
        gameObject.SetActive(false);
    }
   
}
