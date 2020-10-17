using UnityEngine;
using UnityEngine.Events;

public class PlayerCollided : MonoBehaviour
{

    public DialogueManager dialogue;
    public GameObject pressF;

    [Space(20)]
    [Header("Battle Scene")]
    public UnityEvent cepotEventAfterDialogue;
    [Space(20)]
    public UnityEvent dawalaEventAfterDialogue;
    [Space(20)]
    public UnityEvent garengEventAfterDialogue;

    private ObstacleTutorial obstacle;
    private bool isCollided;
    

    private void Update()
    {
        if (isCollided)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                pressF.SetActive(false);

                dialogue.sentences = obstacle.sentences;
                Player.currentCharacter = obstacle.playerCharacter;

                if (obstacle.playerCharacter.ToUpper().Trim() == "CEPOT")
                {
                    dialogue.eventAfterDialogue = cepotEventAfterDialogue;
                }
                else if (obstacle.playerCharacter.ToUpper().Trim() == "GARENG")
                {
                    dialogue.eventAfterDialogue = garengEventAfterDialogue;
                }
                else if (obstacle.playerCharacter.ToUpper().Trim() == "DAWALA")
                {
                    dialogue.eventAfterDialogue = dawalaEventAfterDialogue;
                }
                obstacle.isChecked = true;
                dialogue.gameObject.SetActive(true);
                dialogue.init();
            }

            if (Player.isWinning)
            {
                dialogue.sentences = obstacle.sentencesAfterWin;
                dialogue.eventAfterDialogue = obstacle.afterWinEvent;
                foreach (Transform item in dialogue.transform)
                {
                    item.gameObject.SetActive(false);
                }
                dialogue.gameObject.SetActive(true);
                dialogue.init();
                obstacle.enabled = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            obstacle = collision.gameObject.GetComponent<ObstacleTutorial>();
            if (!obstacle.isChecked)
            {
                isCollided = true;
                pressF.SetActive(true);
            }
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            obstacle = collision.gameObject.GetComponent<ObstacleTutorial>();
            if (!obstacle.isChecked)
            {
                isCollided = false;
                pressF.SetActive(false);
            }
        }
    }

}
