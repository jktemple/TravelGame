using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    [Header("Correct Guess String")]
    public string correctGuessString;

    Item item;
    // Start is called before the first frame update
    void Start()
    {
        item = GetComponent<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerDialogue() 
    {
        if (!DialogueManager.GetInstance().DialogueIsPlaying)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON, correctGuessString, item);
        }
    }


}
