using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.SearchService;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Ink GLobal Variable JSON")]
    [SerializeField] private TextAsset loadVariablesJSON;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameTagText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;

    [Header("Guess UI")]
    [SerializeField] private GameObject[] guessButtons;

    [Header("Object UI")]
    [SerializeField] private GameObject[] objectButtons;

    [Header("Level Fader")]
    [SerializeField] private LevelChanger levelChanger;


    private TextMeshProUGUI[] choicesText;

    private Story currentStory;

    public bool DialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;

    private Coroutine displayLineCoroutine;

    private static DialogueManager instance;

    private PlayerControls controls;

    private DialogueVariables dialogueVariables;

    private const string SPEAKER_TAG = "speaker";
    private const string DIALOGUE_TYPE_TAG = "type";
    private const string INTERNAL_TYPE_TAG = "internal";
    private const string EXTERNAL_TYPE_TAG = "external";

    private string correctGuessString;

    private Item heldItem;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager");
        }
        instance = this; 
        dialogueVariables = new DialogueVariables(loadVariablesJSON);
    }

    public static DialogueManager GetInstance() { return instance; }

    private void Start()
    {

        DialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        controls = InputManager.Instance().playerControls;
        controls.Enable();

        //Get all of the choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
        ExitGuessMode();
        HideObjectButtons();
    }

    private void Update()
    {
        if(!DialogueIsPlaying) { return; }

        if (controls.UI.Submit.WasPerformedThisFrame() && canContinueToNextLine && currentStory.currentChoices.Count == 0)
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON, string correctGuess, Item item)
    {
        heldItem = item;
        currentStory = new Story(inkJSON.text);
        DialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        HideObjectButtons();
        dialogueVariables.StartListening(currentStory);

        currentStory.BindExternalFunction("enterGuessMode", (bool mode) =>
        {
            EnterGuessMode();
        });

        currentStory.BindExternalFunction("endGame", (bool b) => { EndGame(); });
        currentStory.BindExternalFunction("nextVersion", (bool b) => { NextItemVersion(); });
        nameTagText.text = "???";
        correctGuessString = correctGuess;
        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        DialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = " ";
        dialogueVariables.StopListening(currentStory);
        currentStory.UnbindExternalFunction("enterGuessMode");
        currentStory.UnbindExternalFunction("endGame");
        currentStory.UnbindExternalFunction("nextVersion");
        ShowObjectButtons();
    }

    private void EndGame()
    {
        levelChanger.FadeToLevel(2);
    }

    private void NextItemVersion()
    {
        if (heldItem!= null)
        {
            heldItem.NextVersion();
        }
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if(displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
            HandleTags(currentStory.currentTags);

        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void HandleTags(List<string> tags)
    {
        foreach (string tag in tags)
        {
            string[] splitTag = tag.Split(':');
            if(splitTag.Length != 2 ) 
            {
                Debug.LogError("Tag could not be parsed correctly: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    nameTagText.text = tagValue;
                    break;
                case DIALOGUE_TYPE_TAG:
                    if(tagValue == INTERNAL_TYPE_TAG)
                    {
                        dialogueText.fontStyle = FontStyles.Italic;
                    } else if(tagValue == EXTERNAL_TYPE_TAG)
                    {
                        dialogueText.fontStyle = FontStyles.Normal;
                    }
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }

        }
    }

    private IEnumerator DisplayLine(string line)
    {
        //empty text
        dialogueText.text = "";

        continueIcon.SetActive(false);
        HideChoices();
        canContinueToNextLine = false;
        //display the letters one at a time
        foreach (char letter in line.ToCharArray())
        {
            if (InputManager.Instance().playerControls.UI.Submit.IsPressed())
            {
                dialogueText.text = line;
                break;
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        continueIcon.SetActive(true);
        DisplayChoices();
        canContinueToNextLine = true;
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        //Defensive check to make sure UI can support the number of choices coming in.
        if(currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        //enable and add text to the needed choices
        foreach(Choice choice in currentChoices)
        {
            choices[index].SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        //go through the remaining chouices and make sure they're hidden

        for(int i = index; i < choices.Length; i++)
        {
            choices[i].SetActive(false);
        }

    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        }
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        dialogueVariables.Variables.TryGetValue(variableName, out Ink.Runtime.Object variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }
    
    private void EnterGuessMode() 
    { 
        foreach(GameObject b in guessButtons)
        {
            b.SetActive(true);
            string text = b.GetComponentInChildren<TextMeshProUGUI>().text;
            b.GetComponentInChildren<Button>().onClick.AddListener(delegate { Guess(text); });
        }
    }
     
    private void Guess(string s)
    {
        if (s == correctGuessString)
        {
            currentStory.ChoosePathString("correctAnswer");
            ContinueStory();
        } else
        {
            currentStory.ChoosePathString("incorrectAnswer");
            ContinueStory();
        }
        ExitGuessMode();
    }

    private void ExitGuessMode()
    {
        foreach (GameObject b in guessButtons)
        {
            b.SetActive(false);
            b.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        }
    }

    private void HideObjectButtons()
    {
        foreach(GameObject o in objectButtons)
        {
            o.SetActive(false);
        }
    }

    private void ShowObjectButtons()
    {
        foreach (GameObject o in objectButtons)
        {
            o.SetActive(true);
        }
    }
    
}
