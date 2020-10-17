using UnityEngine;

public class switchPlayer : MonoBehaviour
{
    public GameObject Cepot;
    public GameObject Dawala;
    public GameObject Gareng;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Player.currentCharacter = "CEPOT";
            Cepot.GetComponent<SpriteRenderer>().enabled = true;
            Dawala.GetComponent<SpriteRenderer>().enabled = false;
            Gareng.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Player.currentCharacter = "DAWALA";
            Cepot.GetComponent<SpriteRenderer>().enabled = false;
            Dawala.GetComponent<SpriteRenderer>().enabled = true;
            Gareng.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Player.currentCharacter = "GARENG";
            Cepot.GetComponent<SpriteRenderer>().enabled = false;
            Dawala.GetComponent<SpriteRenderer>().enabled = false;
            Gareng.GetComponent<SpriteRenderer>().enabled = true;
        }

    }
}
