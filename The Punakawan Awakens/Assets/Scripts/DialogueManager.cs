using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Zetcil;

public class DialogueManager : MonoBehaviour
{
    public GameObject[] playerList;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject leftSprite;
    public GameObject RightSprite;
    public GameObject middleSprite;
    public GameObject continueButton;
    public GameObject mainCamera;

    public Sentences[] sentences;
    private int index;
    private bool isPlayed;
    public float typingSpeed;
    private bool currentWriting;
    [Space(20)]
    public UnityEvent eventAfterDialogue;

    public void init()
    {
        mainCamera.SetActive(true);
        index = 0;
        StartCoroutine(Type());
        currentWriting = true;
        foreach (GameObject item in playerList)
        {
            item.GetComponent<KeyboardController>().enabled = false;
            item.GetComponent<SpritePositionController>().enabled = false;
            item.GetComponent<SpriteAnimationController>().enabled = false;
        }

        foreach (Transform item in transform)
        {
            item.gameObject.SetActive(true);
        }

        if (Player.isWinning)
        {
            
            Player.isWinning = false;
        }

        isPlayed = true;
    }
    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        playerList = GameObject.FindGameObjectsWithTag("Player");
        init();
    }

    private void Update()
    {
        if (isPlayed && SceneManager.sceneCount == 1)
        {
            ChangeSprite();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Next();
            }
            if (dialogueText.text == sentences[index].sentence)
            {
                continueButton.SetActive(true);
                currentWriting = false;
            }
        }
    }

    public void Next()
    {
        if (currentWriting)
        {
            dialogueText.text = sentences[index].sentence;

            currentWriting = false;
            StopAllCoroutines();
        }
        else if (!currentWriting)
        {
            currentWriting = true;
            index++;
            if (index < sentences.Length)
            {
                StartCoroutine(Type());
            }
            else
            {
                foreach (GameObject item in playerList)
                {
                    item.GetComponent<KeyboardController>().enabled = true;
                    item.GetComponent<SpritePositionController>().enabled = true;
                    item.GetComponent<SpriteAnimationController>().enabled = true;
                }
                RightSprite.SetActive(false);
                leftSprite.SetActive(false);
                isPlayed = false;
                eventAfterDialogue.Invoke();
            }
        }
    }

    IEnumerator Type()
    {
        ChangeSprite();
        dialogueText.text = "";
        nameText.text = sentences[index].name.ToUpper();
        foreach (char letter in sentences[index].sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void ChangeSprite()
    {
        continueButton.SetActive(false);
        if (sentences[index].characterSprite != null)
        {
            if (sentences[index].whereToPlace == Sentences.WhereToPlace.RIGHT)
            {
                if (sentences[index].dir == Sentences.Direction.RIGHT)
                {
                    RightSprite.transform.rotation = new Quaternion(RightSprite.transform.rotation.x, 180f, RightSprite.transform.rotation.z, RightSprite.transform.rotation.w);
                }
                else
                {
                    RightSprite.transform.rotation = new Quaternion(RightSprite.transform.rotation.x, 0f, RightSprite.transform.rotation.z, RightSprite.transform.rotation.w);
                }
                RightSprite.SetActive(true);
                RightSprite.GetComponent<SpriteRenderer>().sprite = sentences[index].characterSprite;
            }
            else
            {
                if (sentences[index].dir == Sentences.Direction.LEFT)
                {
                    leftSprite.transform.rotation = new Quaternion(leftSprite.transform.rotation.x, 180f, leftSprite.transform.rotation.z, leftSprite.transform.rotation.w);
                }
                else
                {
                    leftSprite.transform.rotation = new Quaternion(leftSprite.transform.rotation.x, 0f, leftSprite.transform.rotation.z, leftSprite.transform.rotation.w);
                }
                leftSprite.SetActive(true);
                leftSprite.GetComponent<SpriteRenderer>().sprite = sentences[index].characterSprite;
            }
        }

        if (sentences[index].item != null)
        {
            middleSprite.GetComponent<SpriteRenderer>().sprite = sentences[index].item;
            sentences[index].isMiddleSpriteActive = false;
        }

        if (sentences[index].isMiddleSpriteActive)
        {
            middleSprite.GetComponent<SpriteRenderer>().sprite = null;
        }
        
    }
}
