using UnityEngine;

public class HintDict : MonoBehaviour
{
    public GameObject nextButton;
    public GameObject prevButton;
    private int currentPage = 0;
    public GameObject[] pages;

    public void Update()
    {
        if (currentPage == 0)
        {
            prevButton.SetActive(false);
        }
    }

    public void Next()
    {
        if (currentPage < pages.Length)
        {
            currentPage++;
            pages[currentPage - 1].SetActive(false);
            pages[currentPage].SetActive(true);
            prevButton.SetActive(true);
        }

        if (currentPage == pages.Length-1)
        {
            nextButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
        }
    }

    public void Previous()
    {
        if (currentPage >= 0)
        {
            currentPage--;
            pages[currentPage + 1].SetActive(false);
            pages[currentPage].SetActive(true);
            nextButton.SetActive(true);
        }

        if (currentPage == 0)
        {
            prevButton.SetActive(false);
        }
        else
        {
            prevButton.SetActive(true);
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
