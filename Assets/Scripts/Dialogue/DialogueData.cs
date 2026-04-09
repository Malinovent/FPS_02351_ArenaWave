using UnityEngine;
//ScriptableObject
[CreateAssetMenu(fileName = "new Dialogue", menuName = "ArenaWave/Create New Dialogue", order = 0)]
public class DialogueData : ScriptableObject
{
    //References 
    public DialogueInfo[] dialogues;

}
