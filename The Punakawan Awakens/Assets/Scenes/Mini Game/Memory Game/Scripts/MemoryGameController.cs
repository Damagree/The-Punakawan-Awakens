using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TechnomediaLabs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zetcil;

public class MemoryGameController : MonoBehaviour
{
    [Space(10)]
    [Header("Timer")]
    [SerializeField] private Text timerText;
    [SerializeField] private VarTime timer;

    [Space(10)]
    [Header("Card Sprites settings")]
    [SerializeField] private Sprite backSprite;
    [SerializeField] private string frontSpritePath;
    [SerializeField] private Sprite[] frontSprite;
    [Space(5)]
    public List<Sprite> usedSprites = new List<Sprite>();

    [Space(10)]
    [Header("Card Settings")]
    [SerializeField] private VarInteger currentScore;
    [SerializeField] private VarInteger currentCard;
    [SerializeField] private GameObject puzzleField;
    [SerializeField] private GameObject card;
    public int maxCard;
    public Vector2 cardSize;

    [Space(10)]
    [Header("Card Animations Settings")]
    [SerializeField] private VarFloat flipSpeed;

    [Space(10)]
    [Header("List Card Setting")]
    public List<Button> cards = new List<Button>(); 
    public FindController findController;

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

    [Space(10)]
    [Header("Win Setting")]
    public GameObject winPanel;
    public UnityEvent winCondition;
    
    [Space(10)]
    [Header("Lose Setting")]
    public GameObject losePanel;
    public UnityEvent loseCondition;

    [Space(10)]
    public bool Debuging;
    [ConditionalField("Debuging")] [SerializeField] private bool firstPick;
    [ConditionalField("Debuging")] [SerializeField] private bool secondPick;
    [ConditionalField("Debuging")] [SerializeField] private int firstPickIndex, secondPickIndex;
    [ConditionalField("Debuging")] [SerializeField] private string firstPickName, secondPickName;

    private void Awake()
    {
        InitCards();

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

        enemyHealth = enemy.GetComponent<VarHealth>();
    }

    public void Init()
    {

        GetCards();
        AddListener();
        AddFrontCardSprite();
        Shuffle();
    }

    private void Start()
    {
        Init();
    }

    public void InitCards()
    {
        frontSprite = Resources.LoadAll<Sprite>(frontSpritePath);
        puzzleField.GetComponent<GridLayoutGroup>().cellSize = cardSize;
        currentCard.SetCurrentValue(maxCard);
        if (!cards.IsNullOrEmpty())
        {
            cards = new List<Button>();
            usedSprites = new List<Sprite>();
            findController.InvokeFindController();
            for (int i = 0; i < findController.findingObjectTag.Length; i++)
            {
                Destroy(findController.findingObjectTag[i]);
            }
        }

        for (int i = 0; i < maxCard; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.name = "" + i;
            newCard.transform.SetParent(puzzleField.transform, false);
        }
    }

    void GetCards()
    {
        findController.InvokeFindController();
        Debug.Log("findcontroller " + findController.findingObjectTag.Length);
        for (int i = 0; i < findController.findingObjectTag.Length; i++)
        {
            cards.Add(findController.findingObjectTag[i].GetComponent<Button>());
            cards[i].image.sprite = backSprite;
        }
    }

    void AddListener()
    {
        foreach (Button item in cards)
        {
            item.onClick.AddListener(() => PickACard());
        }
    }

    public void PickACard()
    {
        string objectName = EventSystem.current.currentSelectedGameObject.name;

        if (!firstPick)
        {

            firstPick = true;
            firstPickIndex = int.Parse(objectName);
            firstPickName = usedSprites[firstPickIndex].name;
            cards[firstPickIndex].image.sprite = usedSprites[firstPickIndex];
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;

        } 
        else if (!secondPick)
        {

            secondPick = true;
            secondPickIndex = int.Parse(objectName);
            secondPickName = usedSprites[secondPickIndex].name;
            cards[secondPickIndex].image.sprite = usedSprites[secondPickIndex];
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;

            StartCoroutine(CheckCard());
        }

    }

    IEnumerator CheckCard()
    {
        yield return new WaitForSeconds(flipSpeed.CurrentValue);

        if (firstPickName == secondPickName)
        {
            cards[firstPickIndex].interactable = false;
            cards[secondPickIndex].interactable = false;

            cards[firstPickIndex].image.color = new Color(0, 0, 0, 0);
            cards[secondPickIndex].image.color = new Color(0, 0, 0, 0);

            currentScore.AddToCurrentValue(100);
            currentCard.SubtractFromCurrentValue(2);
        }
        else
        {
            cards[firstPickIndex].image.sprite = backSprite;
            cards[firstPickIndex].GetComponent<Button>().interactable = true;

            cards[secondPickIndex].image.sprite = backSprite;
            cards[secondPickIndex].GetComponent<Button>().interactable = true;
        }

        firstPick = false;
        secondPick = false;

    }

    public void IsWinning()
    {
        Player.isWinning = true;
        Debug.Log("You Win!");
    }

    public void IsLoosing()
    {
        Player.isWinning = false;
        Debug.Log("You Lose");
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

        enemy.GetComponent<Animator>().SetTrigger("Attack");

        InitCards();
    }

    public void EnemyHit()
    {
        if (enemy.GetComponent<Animator>() != null)
        {
            enemy.GetComponent<Animator>().SetTrigger("Hit");
        }
        enemyHealth.SubFromCurrentValue(attackValue);
        
        InitCards();
    }

    void AddFrontCardSprite()
    {
        int currentSpriteIndex = 0;

        for (int i = 0; i < cards.Count; i++)
        {
            if (currentSpriteIndex == cards.Count / 2)
            {
                currentSpriteIndex = 0;
            }

            usedSprites.Add(frontSprite[currentSpriteIndex]);
            currentSpriteIndex++;
        }
    }

    void Shuffle()
    {
        for (int i = 0; i < usedSprites.Count; i++)
        {
            Sprite tmp = usedSprites[i];
            int randomIndex = Random.Range(0, usedSprites.Count);
            usedSprites[i] = usedSprites[randomIndex];
            usedSprites[randomIndex] = tmp;
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

        if (timer.CurrentValue <= 0f)
        {
            IsLoosing();
        }
    }
}
