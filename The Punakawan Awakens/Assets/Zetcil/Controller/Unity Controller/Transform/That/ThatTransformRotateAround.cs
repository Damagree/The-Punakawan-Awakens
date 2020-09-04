using UnityEngine;
using System.Collections;

public class ThatTransformRotateAround : MonoBehaviour
{

    public GameObject ThatGameObject; // the object to rotate around
    public Transform target; // the object to rotate around
    public int speed; // the speed of rotation

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ThatGameObject.transform.RotateAround(target.transform.position, target.transform.up, speed * Time.deltaTime);
    }
}
