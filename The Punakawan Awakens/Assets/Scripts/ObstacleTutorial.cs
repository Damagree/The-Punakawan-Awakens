using UnityEngine;
using UnityEngine.Events;

public class ObstacleTutorial : MonoBehaviour
{
    // Who's game will be played
    public string playerCharacter = "CEPOT";
    public Sentences[] sentences;
    public Sentences[] sentencesAfterWin;
    public bool isChecked = false;
    public UnityEvent afterWinEvent;

    public DialogueManager dialogue;


}
