using UnityEngine;
using UnityEngine.Events;

public class AdvantureManager : MonoBehaviour
{
    public UnityEvent collideEventFalse;
    public UnityEvent collideEventTrue;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "realClue")
        {
            collideEventTrue.Invoke();
        }
        else if (collision.gameObject.tag == "falseClue")
        {
            collideEventFalse.Invoke();
        }
    }

}
