using UnityEngine;
using System.Collections;

public class ThisTransformRotateAround : MonoBehaviour
{

    public Transform target; // the object to rotate around
    public int speed; // the speed of rotation

    void Start()
    {
        if (target == null)
        {
            target = this.gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.transform.position, target.transform.up, speed * Time.deltaTime);
    }
}
