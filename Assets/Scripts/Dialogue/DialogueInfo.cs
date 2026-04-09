using UnityEngine;
//Info pour chaque dialogue
[System.Serializable]
public struct DialogueInfo
{
    public ActorData actorData;
    [TextArea(1, 10)] public string dialogueText;

    public DialogueChoice[] choices;
}

[System.Serializable]
public struct DialogueChoice
{
    public string eventName;
    public string choiceText;
    public DialogueData nextDialogue;
}