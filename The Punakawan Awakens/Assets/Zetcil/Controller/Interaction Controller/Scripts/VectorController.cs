using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetcil
{
    public class VectorController : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("GameObject Settings")]
        public GameObject TargetController;

        [Header("Vector Settings")]
        public VarVector3 TargetVectorInput;

        [Header("Movement Settings")]
        public float MoveSpeed;
        public float RotateSpeed;
        public float gravity;

        Vector3 moveDirection;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                if (TargetVectorInput.CurrentValue != Vector3.zero)
                {
                    float xMovementTargetVector = 0; // The horizontal movement from joystick 01
                    float zMovementTargetVector = 0; // The vertical movement from joystick 01	

                    xMovementTargetVector = TargetVectorInput.CurrentValue.x; // The horizontal movement from joystick 01
                    zMovementTargetVector = TargetVectorInput.CurrentValue.y; // The vertical movement from joystick 01	

                    float tempAngle = Mathf.Atan2(zMovementTargetVector, xMovementTargetVector);
                    xMovementTargetVector *= Mathf.Abs(Mathf.Cos(tempAngle));
                    zMovementTargetVector *= Mathf.Abs(Mathf.Sin(tempAngle));

                    TargetVectorInput.CurrentValue = new Vector3(xMovementTargetVector, 0, zMovementTargetVector);
                    TargetVectorInput.CurrentValue = TargetController.transform.TransformDirection(TargetVectorInput.CurrentValue);
                    TargetVectorInput.CurrentValue *= MoveSpeed;

                    // rotate the player to face the direction of input
                    Vector3 temp = TargetController.transform.position;
                    temp.x += xMovementTargetVector;
                    temp.z += zMovementTargetVector;
                    Vector3 lookDirection = temp - TargetController.transform.position;
                    if (lookDirection != Vector3.zero)
                    {
                        TargetController.transform.localRotation = Quaternion.Slerp(TargetController.transform.localRotation, Quaternion.LookRotation(lookDirection), RotateSpeed * Time.deltaTime);
                    }

                    moveDirection = TargetController.transform.forward * MoveSpeed * 100 * Time.deltaTime;
                    moveDirection.y -= gravity * Time.deltaTime;
                    TargetController.GetComponent<CharacterController>().Move(moveDirection * Time.deltaTime);
                }
            }
        }
    }
}
