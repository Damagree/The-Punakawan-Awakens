using System.Collections.Generic;
using TechnomediaLabs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zetcil;

public class AksaraSunda : MonoBehaviour
{
    [Space(10)]
    [Header("Initialize")]
    [SerializeField] GameObject scrambledObject;
    [SerializeField] GameObject panelPickedWord;
    [SerializeField] GameObject pickedWord;
    [SerializeField] GameObject buttons;
    [SerializeField] int currentIdx = 0;
    [SerializeField] private VarString currentSentence;
    [SerializeField] private CheckerController checkerController;

    [Space(10)]
    [Header("Timer")]
    [SerializeField] private Text timerText;
    [SerializeField] private VarTime timer;

    [Space(10)]
    [Header("Quetions")]
    public string aksaraPrefabsPath;
    public GameObject panelSoal;
    [SerializeField] private GameObject[] aksaraPrefabs;

    [Space(10)]
    [Header("List Button")]
    public List<Button> btns = new List<Button>();
    public FindController findButtonController;
    public FindController findPickedWordImageController;

    [Space(10)]
    [Header("Player Setting")]
    public Image playerIconSprite;
    public string playerIconPath = "";
    public GameObject Cepot;
    public VarHealth cepotHealth;
    public GameObject Dawala;
    public VarHealth dawalaHealth;
    public GameObject Gareng;
    public VarHealth garengHealth;
    public float attackValue = 50f;
    [SerializeField] private Sprite[] playerIcons;

    [Space(10)]
    [Header("Enemy Setting")]
    public Image enemyIconSprite;
    public GameObject enemy;
    private VarHealth enemyHealth;
    public float enemyAttack = 10f;
    public UnityEvent eventTimesUp;

    [Space(10)]
    [Header("Debug")]
    [SerializeField] List<GameObject> pickedWordImage = new List<GameObject>();
    [SerializeField] private char[] sentences;
    private bool checkHit;


    public void Init()
    {
        if (enemyHealth.CurrentValue > 0 && cepotHealth.CurrentValue > 0 && garengHealth.CurrentValue > 0 && dawalaHealth.CurrentValue > 0)
        {
            if (!btns.IsNullOrEmpty())
            {
                btns = new List<Button>();
                pickedWordImage = new List<GameObject>();
                sentences = null;

                findButtonController.InvokeFindController();
                for (int i = 0; i < findButtonController.findingObjectTag.Length; i++)
                {
                    Destroy(findButtonController.findingObjectTag[i]);
                }

                findPickedWordImageController.InvokeFindController();
                for (int i = 0; i < findPickedWordImageController.findingObjectTag.Length; i++)
                {
                    Destroy(findPickedWordImageController.findingObjectTag[i]);
                }
            }

            PickASentences();
            Shuffle();

            for (int i = 0; i < sentences.Length; i++)
            {
                GameObject newButton = Instantiate(buttons);
                newButton.name = " " + i;
                newButton.GetComponentInChildren<Text>().text = sentences[i].ToString().ToUpper();
                newButton.transform.SetParent(scrambledObject.transform, false);
            }

            Invoke("ButtonListen", 0);
        }
        currentSentence.CurrentValue = "";
    }

    public void ButtonListen()
    {
        GetButtons();
        AddListener();
    }

    private void Awake()
    {
        aksaraPrefabs = Resources.LoadAll<GameObject>(aksaraPrefabsPath);
        enemyHealth = enemy.GetComponent<VarHealth>();
        Init();

        playerIcons = Resources.LoadAll<Sprite>(playerIconPath);

        for (int i = 0; i < playerIcons.Length; i++)
        {
            if (playerIcons[i].name == Player.currentCharacter)
            {
                playerIconSprite.sprite = playerIcons[i];
            }
        }

        if (Player.currentCharacter == "CEPOT")
        {
            Cepot.GetComponent<SpriteRenderer>().enabled = true;
            Gareng.GetComponent<SpriteRenderer>().enabled = false;
            Dawala.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (Player.currentCharacter == "GARENG")
        {
            Cepot.GetComponent<SpriteRenderer>().enabled = false;
            Gareng.GetComponent<SpriteRenderer>().enabled = true;
            Dawala.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (Player.currentCharacter == "DAWALA")
        {
            Cepot.GetComponent<SpriteRenderer>().enabled = false;
            Gareng.GetComponent<SpriteRenderer>().enabled = false;
            Dawala.GetComponent<SpriteRenderer>().enabled = true;
        }

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

        if (timer.CurrentValue <= 0)
        {
            checkHit = true;
            if (checkHit)
            {
                eventTimesUp.Invoke();
                checkHit = false;
            }
        }

    }

    void PickASentences()
    {
        int randomIndex = Random.Range(0, aksaraPrefabs.Length);
        GameObject aks = Instantiate(aksaraPrefabs[randomIndex]);
        aks.transform.SetParent(panelSoal.transform);
        aks.transform.localScale = new Vector3(.1f, .1f, .1f);
        aks.transform.localPosition = new Vector3(0f, 0f, 0f);
        checkerController.EqualConditionValue = aksaraPrefabs[randomIndex].name.Trim().ToUpper();
        checkerController.NotEqualConditionValue = aksaraPrefabs[randomIndex].name.Trim().ToUpper();
        sentences = aksaraPrefabs[randomIndex].name.ToUpper().ToCharArray();
        
        currentIdx = randomIndex;
    }

    void GetButtons()
    {
        findButtonController.InvokeFindController();
        for (int i = 0; i < findButtonController.findingObjectTag.Length; i++)
        {
            btns.Add(findButtonController.findingObjectTag[i].GetComponent<Button>());
        }
    }

    void Shuffle()
    {
        for (int i = 0; i < sentences.Length; i++)
        {
            char tmp = sentences[i];
            int randomIndex = Random.Range(0, sentences.Length);
            sentences[i] = sentences[randomIndex];
            sentences[randomIndex] = tmp;
        }
    }

    void AddListener()
    {
        Debug.Log("Listener");
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
            currentSentence.AddToCurrentValue(gameObject.GetComponentInChildren<Text>().text.Trim());
            UpdatePickedWordImage(gameObject);
            gameObject.GetComponent<Button>().image.color = Color.gray;
        }
        else
        {
            char[] tmpWord = currentSentence.CurrentValue.ToUpper().Trim().ToCharArray();
            string newWord = "";

            foreach (char item in tmpWord)
            {
                for (int i = 0; i < gameObject.GetComponentInChildren<Text>().text.Trim().ToUpper().ToCharArray().Length; i++)
                {
                    if (item != gameObject.GetComponentInChildren<Text>().text.Trim().ToUpper().ToCharArray()[i])
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

    public void Attack()
    {
        Cepot.GetComponent<Animator>().SetTrigger("Attack");
        Gareng.GetComponent<Animator>().SetTrigger("Attack");
        Dawala.GetComponent<Animator>().SetTrigger("Attack");
    }

    public void GetHit()
    {
        Cepot.GetComponent<Animator>().SetTrigger("Hit");
        Gareng.GetComponent<Animator>().SetTrigger("Hit");
        Dawala.GetComponent<Animator>().SetTrigger("Hit");
        cepotHealth.SubFromCurrentValue(enemyAttack);
        dawalaHealth.SubFromCurrentValue(enemyAttack);
        garengHealth.SubFromCurrentValue(enemyAttack);

    }

    public void EnemyHit()
    {
        if (enemy.GetComponent<Animator>() != null)
        {
            enemy.GetComponent<Animator>().SetTrigger("Hit");
        }
        enemyHealth.SubFromCurrentValue(attackValue);
    }

    public void IsWinning()
    {
        Player.isWinning = true;
    }

    public void IsLoosing()
    {
        Player.isWinning = false;
    }
}
