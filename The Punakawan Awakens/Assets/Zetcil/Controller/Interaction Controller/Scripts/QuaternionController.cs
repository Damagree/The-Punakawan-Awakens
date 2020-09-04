using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetcil
{
    public class QuaternionController : MonoBehaviour
    {
        public enum RotationAxes { DirectionXAndY = 0, DirectionX = 1, DirectionY = 2 }

        [Space(10)]
        public bool isEnabled;

        [Header("GameObject Settings")]
        public RotationAxes axes = RotationAxes.DirectionXAndY;
        public GameObject TargetCamera;

        [Header("Vector Settings")]
        public VarVector3 TargetVectorInput;

        [Header("Movement Settings")]
        public float sensitivityX = 15F;
        public float sensitivityY = 15F;
        public float minimumX = -360F;
        public float maximumX = 360F;
        public float minimumY = -60F;
        public float maximumY = 60F;
        float rotationX = 0F;
        float rotationY = 0F;

        Quaternion originalRotation;

        // Start is called before the first frame update
        void Start()
        {
            originalRotation = transform.localRotation;
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                if (axes == RotationAxes.DirectionXAndY)
                {
                    // Read the mouse input axis
                    rotationX += TargetVectorInput.GetCurrentValueX() * sensitivityX;
                    rotationY += TargetVectorInput.GetCurrentValueY() * sensitivityY;
                    rotationX = ClampAngle(rotationX, minimumX, maximumX);
                    rotationY = ClampAngle(rotationY, minimumY, maximumY);
                    Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                    Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
                    TargetCamera.transform.localRotation = originalRotation * xQuaternion * yQuaternion;
                }
                else if (axes == RotationAxes.DirectionX)
                {
                    rotationX += TargetVectorInput.GetCurrentValueX() * sensitivityX;
                    rotationX = ClampAngle(rotationX, minimumX, maximumX);
                    Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                    TargetCamera.transform.localRotation = originalRotation * xQuaternion;
                }
                else if (axes == RotationAxes.DirectionY)
                {
                    rotationY += TargetVectorInput.GetCurrentValueY() * sensitivityY;
                    rotationY = ClampAngle(rotationY, minimumY, maximumY);
                    Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
                    TargetCamera.transform.localRotation = originalRotation * yQuaternion;
                }
            }
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
         angle += 360F;
            if (angle > 360F)
         angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }
    }
}
