using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TechnomediaLabs;
using UnityEngine;
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
    public bool Debuging;
    [ConditionalField("Debuging")] [SerializeField] private bool firstPick;
    [ConditionalField("Debuging")] [SerializeField] private bool secondPick;
    [ConditionalField("Debuging")] [SerializeField] private int firstPickIndex, secondPickIndex;
    [ConditionalField("Debuging")] [SerializeField] private string firstPickName, secondPickName;

    private void Awake()
    {
        frontSprite = Resources.LoadAll<Sprite>(frontSpritePath);
        puzzleField.GetComponent<GridLayoutGroup>().cellSize = cardSize;
        currentCard.SetCurrentValue(maxCard);

        for (int i = 0; i < maxCard; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.name = "" + i;
            newCard.transform.SetParent(puzzleField.transform, false);
        }
    }

    private void Start()
    {
        GetCards();
        AddListener();
        AddFrontCardSprite();
        Shuffle();

    }

    void GetCards()
    {
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

    public void IsFinished()
    {
        Debug.Log("You Win!");
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
        
    }
}
