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
        if (BGMController.isDisabled)
        {
            ONButton.GetComponent<Button>().interactable = false;
            onText.alpha = 100f;

            OFFButton.GetComponent<Button>().interactable = true;
            offText.alpha = 255f;
        }
        else
        {
            ONButton.GetComponent<Button>().interactable = true;
            onText.alpha = 255f;

            OFFButton.GetComponent<Button>().interactable = false;
            offText.alpha = 100f;
        }
    }

    private void FixedUpdate()
    {
        if (BGMController.isDisabled)
        {
            ONButton.GetComponent<Button>().interactable = false;
            onText.alpha = 100f;

            OFFButton.GetComponent<Button>().interactable = true;
            offText.alpha = 255f;
        }
        else
        {
            ONButton.GetComponent<Button>().interactable = true;
            onText.alpha = 255f;

            OFFButton.GetComponent<Button>().interactable = false;
            offText.alpha = 100f;
        }
    }

    public void SetBGM()
    {
        BGMController.isDisabled = !BGMController.isDisabled;
    }
}
