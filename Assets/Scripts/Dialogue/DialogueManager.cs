using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Unity.VisualScripting;
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject uiContainer;
    //[SerializeField] private Image actorPortait;
    [SerializeField] private TMP_Text actorName;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float typewriteSpeedInSeconds = 0.02f;
    [SerializeField] private TMP_Text[] choicesText;

    private DialogueData currentDialogue;
    private int dialogueIndex = 0;

    public static DialogueManager Singleton;

    public event Action<string> onDialogueEvent;

    void Awake()
    {
        if(Singleton == null)
        {
            Singleton = this;
            return;
        }

        Destroy(this.gameObject);
    }


    public void StartDialogue(DialogueData dialogue)
    {
        currentDialogue = dialogue;
        dialogueIndex = 0;
        uiContainer.SetActive(true);

        StartCoroutine();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private IEnumerator DialogueCoroutine()
    {
        DialogueInfo dialogueInfo = currentDialogue.dialogues[dialogueIndex];
        //actorPortait.sprite = dialogueInfo.actorData.actorPortait;

        actorName.text = dialogueInfo.actorData.actorName;
        dialogueText.text = "";

        SetChoices();

        foreach(char c in dialogueInfo.dialogueText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typewriteSpeedInSeconds);
        }
    }

    private void SetChoices()
    {
        //default
        DialogueInfo dialogueInfo = currentDialogue.dialogues[dialogueIndex];           

        for(int i = 0; i < choicesText.Length; i++)
        {

            if(i < dialogueInfo.choices.Length)
            {
                choicesText[i].text = dialogueInfo.choices[i].choiceText;
                choicesText[i].gameObject.SetActive(true);
                continue;
            }

            choicesText[i].gameObject.SetActive(false);
            choicesText[i].text = "";
        }

        if (dialogueInfo.choices.Length == 0)
        {
            choicesText[0].gameObject.SetActive(true);
            choicesText[0].text = "Next";
        }
    }

    public void SelectChoice(int choiceIndex)
    {
        DialogueInfo dialogueInfo = currentDialogue.dialogues[dialogueIndex];
        DialogueData nextDialogue = dialogueInfo.choices[choiceIndex].nextDialogue;

        string eventName = dialogueInfo.choices[choiceIndex].eventName;

        if ( String.IsNullOrEmpty(eventName))
        {
            onDialogueEvent?.Invoke(eventName);
        }


        StartDialogue(nextDialogue);
    }

    public void StartCoroutine()
    {
        StopAllCoroutines();
        StartCoroutine(DialogueCoroutine());
    }

    public void NextDialogue()
    {
        dialogueIndex++;

        if(dialogueIndex >= currentDialogue.dialogues.Length)
        {
            EndDialogue();
            return;
        }

        StartCoroutine();
    }

    private void EndDialogue()
    {
        uiContainer.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
