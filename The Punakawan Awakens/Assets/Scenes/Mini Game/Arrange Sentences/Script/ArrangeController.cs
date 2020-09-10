using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zetcil;

public class ArrangeController : MonoBehaviour
{
    [Space(10)]
    [Header("Initialize")]
    [SerializeField] GameObject scrambledObject;
    [SerializeField] GameObject panelPickedWord;
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject pickedWord;
    [SerializeField] private VarString currentSentence;
    [SerializeField] private CheckerController checkerController;
    [SerializeField] private List<string> peribahasa = new List<string>();

    [Space(10)]
    [Header("Timer")]
    [SerializeField] private Text timerText;
    [SerializeField] private VarTime timer;

    [Space(10)]
    [Header("Sound")]
    public string audioClipsPath;
    public AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    [Space(10)]
    [Header("List Button")]
    public List<Button> btns = new List<Button>();
    public FindController findButtonController;
    public FindController findPickedWordImageController;

    [Space(10)]
    [Header("Debug")]
    [SerializeField] List<GameObject> pickedWordImage = new List<GameObject>();
    [SerializeField] private string[] sentences;

    private void Awake()
    {
        PickASentences();
        Shuffle();

        for (int i = 0; i < sentences.Length; i++)
        {
            GameObject newButton = Instantiate(buttons);
            newButton.name = " " + i;
            newButton.GetComponentInChildren<Text>().text = sentences[i].ToUpper();
            newButton.transform.SetParent(scrambledObject.transform, false);
        }
    }

    private void Start()
    {
        GetButtons();
        AddListener();
        audioSource.Play();
    }

    private void Update()
    {
        int minute = timer.CurrentValue / 60;
        int second = timer.CurrentValue % 60;
        if (second != 0)
        {
            timerText.text = minute.ToString() + ":" + second.ToString();
        }
        else
        {
            timerText.text = minute.ToString() + ":00";
        }

    }

    void PickASentences()
    {
        int randomIndex = UnityEngine.Random.Range(0, peribahasa.Count);
        GetSound(randomIndex);
        checkerController.EqualConditionValue = peribahasa[randomIndex].Trim().ToUpper();
        checkerController.NotEqualConditionValue = peribahasa[randomIndex].Trim().ToUpper();
        sentences = peribahasa[randomIndex].Split(' ');
    }

    void GetButtons()
    {
        for (int i = 0; i < findButtonController.findingObjectTag.Length; i++)
        {
            btns.Add(findButtonController.findingObjectTag[i].GetComponent<Button>());
        }
    }

    void Shuffle()
    {
        for (int i = 0; i < sentences.Length; i++)
        {
            string tmp = sentences[i];
            int randomIndex = UnityEngine.Random.Range(0, sentences.Length);
            sentences[i] = sentences[randomIndex];
            sentences[randomIndex] = tmp;
        }
    }

    void AddListener()
    {
        foreach (Button item in btns)
        {
            item.onClick.AddListener(() => ClickSentences());
        }
    }

    void ClickSentences()
    {
        GameObject gameObject = EventSystem.current.currentSelectedGameObject;
        if (gameObject.GetComponent<Button>().image.color != Color.gray)
        {
            if (currentSentence.CurrentValue != "")
            {
                currentSentence.AddToCurrentValue(" " + gameObject.GetComponentInChildren<Text>().text.Trim());
            }
            else
            {
                currentSentence.AddToCurrentValue(gameObject.GetComponentInChildren<Text>().text.Trim());
            }
            Debug.Log(gameObject);
            UpdatePickedWordImage(gameObject);
            gameObject.GetComponent<Button>().image.color = Color.gray;
        }
        else
        {
            string[] tmpWord = currentSentence.CurrentValue.Split(' ');
            string newWord = "";

            foreach (string item in tmpWord)
            {
                if (item != gameObject.GetComponentInChildren<Text>().text.Trim())
                {
                    if (newWord != "")
                    {
                        newWord += " " + item;
                    }
                    else
                    {
                        newWord += item;
                    }
                }
            }

            currentSentence.SetCurrentValue(newWord);
            gameObject.GetComponent<Button>().image.color = Color.white;

            UpdatePickedWordImage(gameObject, false);
           
        }
    }

    void UpdatePickedWordImage(GameObject currentClicked, bool isAdd = true)
    {
        
        findPickedWordImageController.InvokeFindController();
        int startUpdate;
        List<GameObject> tmpImg = new List<GameObject>();

        if (isAdd)
        {
            if (pickedWordImage != null)
            {
                pickedWordImage.Add(currentClicked);
                tmpImg = pickedWordImage;
            }

            startUpdate = findPickedWordImageController.findingObjectTag.Length;
        }
        else
        {
            tmpImg.Clear();
            foreach (GameObject item in pickedWordImage)
            {
                if (item.name != currentClicked.name)
                {
                    tmpImg.Add(item);
                }
            }

            pickedWordImage = tmpImg;

            foreach (GameObject item in findPickedWordImageController.findingObjectTag)
            {
                Destroy(item);
            }

            startUpdate = 0;
        }

        if (pickedWordImage != null)
        {
            for (int i = startUpdate; i < pickedWordImage.Count; i++)
            {
                GameObject newButton = Instantiate(pickedWord);
                newButton.name = " " + pickedWordImage[i].name;
                newButton.GetComponentInChildren<Text>().text = pickedWordImage[i].gameObject.GetComponentInChildren<Text>().text;
                newButton.transform.SetParent(panelPickedWord.transform, false);
            }
        }
    }

    void GetSound(int indexParibasa)
    {
        audioClips = Resources.LoadAll<AudioClip>(audioClipsPath);
        foreach (AudioClip item in audioClips)
        {
            if (item.name.ToUpper() == peribahasa[indexParibasa].ToUpper())
            {
                audioSource.clip = item;
            }
        }

        audioSource.gameObject.GetComponent<Button>().onClick.AddListener(audioSource.Play);
    }

}
