using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{


    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject leftSprite;
    public GameObject RightSprite;
    public GameObject middleSprite;
    public GameObject continueButton;

    public Sentences[] sentences;
    private int index;
    public float typingSpeed;
    private bool currentWriting;
    [Space(20)]
    public UnityEvent eventAfterDialogue;

    private void Start()
    {
       
        index = 0;
        StartCoroutine(Type());
    }

    private void Update()
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
