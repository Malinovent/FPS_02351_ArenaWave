using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueData dialogue;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    public void Interact()
    {
        StartDialogue();
    }

    public void StartDialogue()
    {
        DialogueManager.Singleton.StartDialogue(dialogue);
    }
}