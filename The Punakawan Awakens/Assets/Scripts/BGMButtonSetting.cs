using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BGMButtonSetting : MonoBehaviour
{
    public GameObject ONButton;
    public GameObject OFFButton;
    TextMeshProUGUI onText, offText;

    private void Awake()
    {
        onText = ONButton.GetComponentInChildren<TextMeshProUGUI>();
        offText = OFFButton.GetComponentInChildren<TextMeshProUGUI>();
        //if (BGMController.isDisabled)
        //{
        //    ONButton.GetComponent<Button>().interactable = false;

        //    OFFButton.GetComponent<Button>().interactable = true;
        //}
        //else
        //{
        //    ONButton.GetComponent<Button>().interactable = true;

        //    OFFButton.GetComponent<Button>().interactable = false;
        //}

        if (BGMController.isDisabled)
        {
            ONButton.SetActive(false);
            OFFButton.SetActive(true);
        }
        else
        {
            ONButton.SetActive(true);
            OFFButton.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (BGMController.isDisabled)
        {
            ONButton.SetActive(false);
            OFFButton.SetActive(true);
        }
        else
        {
            ONButton.SetActive(true);
            OFFButton.SetActive(false);
        }
    }

    public void SetBGM()
    {
        BGMController.isDisabled = !BGMController.isDisabled;
    }
}
