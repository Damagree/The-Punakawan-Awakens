using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{

    public class ZetDeviceMapper : MonoBehaviour
    {

        public bool isEnabled;

        [System.Serializable]
        public class CDeviceButton
        {
            [Header("Direction Mapping")]
            public VarFloat VarButton;

            [Header("Button Value")]
            public float ButtonValue;
        }

        [Header("Device Button Settings")]
        public bool usingDeviceButton;
        public List<CDeviceButton> DeviceButton;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (isEnabled)
            {
                if (usingDeviceButton)
                {
                    foreach (CDeviceButton thisButton in DeviceButton)
                    {
                        thisButton.ButtonValue = Input.GetAxis("Horizontal");
                        thisButton.VarButton.CurrentValue = thisButton.ButtonValue;
                    }
                }
            }
        }
    }
}
