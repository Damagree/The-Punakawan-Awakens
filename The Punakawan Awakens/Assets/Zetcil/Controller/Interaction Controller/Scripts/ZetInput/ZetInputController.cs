using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{

    public class ZetInputController : MonoBehaviour
    {
        public bool isEnabled;

        [System.Serializable]
        public class CInput3DController
        {
            public CharacterController TargetController;
            [Header("Axis Settings")]
            public string horizontalAxis = "Horizontal";
            public string verticalAxis = "Vertical";
            public string jumpButton = "Jump";
            [Header("Movement Speed")]
            public float MoveSpeed;
            public float JumpSpeed;
            public float gravity;
            [Header("Ground Status")]
            public float Offset;
            public bool isGrounded;
            [HideInInspector] public float inputHorizontal;
            [HideInInspector] public float inputVertical;
            [HideInInspector] public bool inputJump;
            [HideInInspector] public Vector3 moveDirection;
        }

        [System.Serializable]
        public class CInput2DController
        {
            public Rigidbody2D TargetController;
            [Header("Axis Settings")]
            public string horizontalAxis = "Horizontal";
            public string verticalAxis = "Vertical";
            public string jumpButton = "Jump";
            [Header("Movement Speed")]
            public float MoveSpeed;
            public float JumpSpeed;
            public float gravity;
            [Header("Ground Status")]
            public float Offset;
            public bool isGrounded;
            [HideInInspector] public float inputHorizontal;
            [HideInInspector] public float inputVertical;
            [HideInInspector] public bool inputJump;
            [HideInInspector] public Vector3 moveDirection;
        }

        [Header("2D Controller Settings")]
        public bool usingInput2DController;
        public CInput2DController Input2DController;

        [Header("3D Controller Settings")]
        public bool usingInput3DController;
        public CInput3DController Input3DController;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                if (usingInput3DController)
                {
                    Input3DController.inputHorizontal = ZetInput.GetAxis(Input3DController.horizontalAxis);
                    Input3DController.inputVertical = ZetInput.GetAxis(Input3DController.verticalAxis);

                    Input3DController.moveDirection = new Vector3(Input3DController.inputHorizontal, 0, Input3DController.inputVertical);
                    Input3DController.moveDirection = Input3DController.TargetController.transform.TransformDirection(Input3DController.moveDirection);
                    Input3DController.moveDirection *= Input3DController.MoveSpeed;

                    Input3DController.inputJump = ZetInput.GetButtonDown(Input3DController.jumpButton);

                    if (Input3DController.inputJump && Input3DController.TargetController.isGrounded)
                    {
                        Input3DController.moveDirection.y = Input3DController.JumpSpeed;
                    }

                    Input3DController.moveDirection.y -= Input3DController.gravity * Time.deltaTime;
                    Input3DController.TargetController.Move(Input3DController.moveDirection * Time.deltaTime);
                }
            }
        }

        bool IsGrounded3D()
        {
            //Input3DController.isGrounded = Physics.Raycast(Input3DController.TargetController.transform.position, Vector3.down, Input3DController.Offset);
            Input3DController.isGrounded = Input3DController.TargetController.isGrounded;
            return Input3DController.isGrounded;
        }
    }
}
