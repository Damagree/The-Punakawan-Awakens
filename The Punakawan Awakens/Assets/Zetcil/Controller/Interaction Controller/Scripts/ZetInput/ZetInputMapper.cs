using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class ZetInputMapper : MonoBehaviour
    {

        public bool isEnabled;

        [System.Serializable]
        public class CAxisButton
        {
            [Header("Direction Mapping")]
            public AxisInputUI ButtonAxis;
            public VarFloat VarButton;

            [Header("Button Value")]
            public float ButtonValue;

            [Header("Event Settings")]
            public bool usingEventSettings;
            public UnityEvent EventSettings;
        }

        [System.Serializable]
        public class CAnalogButton
        {
            [Header("Direction Mapping")]
            public Analog ButtonAxis;
            public VarFloat VarButtonX;
            public VarFloat VarButtonY;

            [Header("Button Value")]
            public float ButtonXValue;
            public float ButtonYValue;

            [Header("Event Settings")]
            public bool usingEventSettings;
            public UnityEvent EventSettings;
        }

        [System.Serializable]
        public class CDpadButton
        {
            [Header("Direction Mapping")]
            public Dpad ButtonAxis;
            public VarFloat VarButtonX;
            public VarFloat VarButtonY;

            [Header("Button Value")]
            public float ButtonXValue;
            public float ButtonYValue;

            [Header("Event Settings")]
            public bool usingEventSettings;
            public UnityEvent EventSettings;
        }

        [System.Serializable]
        public class CSteeringWheelButton
        {
            [Header("Direction Mapping")]
            public SteeringWheel ButtonAxis;
            public VarFloat VarButton;

            [Header("Button Value")]
            public float ButtonValue;

            [Header("Event Settings")]
            public bool usingEventSettings;
            public UnityEvent EventSettings;
        }

        [Header("Axis Button Settings")]
        public bool usingAxisButton;
        public List<CAxisButton> AxisButton;

        [Header("Analog Button Settings")]
        public bool usingAnalogButton;
        public List<CAnalogButton> AnalogButton;

        [Header("Dpad Button Settings")]
        public bool usingDpadButton;
        public List<CDpadButton> DpadButton;

        [Header("Steering Wheel Settings")]
        public bool usingSteeringWheel;
        public List<CSteeringWheelButton> SteeringWheelButton;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (isEnabled)
            {
                if (usingAxisButton)
                {
                    foreach (CAxisButton thisButton in AxisButton)
                    {
                        thisButton.ButtonValue = thisButton.ButtonAxis.axis.value;
                        thisButton.VarButton.CurrentValue = thisButton.ButtonValue;
                        if (thisButton.usingEventSettings)
                        {
                            thisButton.EventSettings.Invoke();
                        }
                    }
                }
                if (usingAnalogButton)
                {
                    foreach (CAnalogButton thisButton in AnalogButton)
                    {
                        thisButton.ButtonXValue = thisButton.ButtonAxis.xAxis.value;
                        thisButton.ButtonYValue = thisButton.ButtonAxis.yAxis.value;
                        thisButton.VarButtonX.CurrentValue = thisButton.ButtonXValue;
                        thisButton.VarButtonY.CurrentValue = thisButton.ButtonYValue;
                        if (thisButton.usingEventSettings)
                        {
                            thisButton.EventSettings.Invoke();
                        }
                    }
                }
                if (usingDpadButton)
                {
                    foreach (CDpadButton thisButton in DpadButton)
                    {
                        thisButton.ButtonXValue = thisButton.ButtonAxis.xAxis.value;
                        thisButton.ButtonYValue = thisButton.ButtonAxis.yAxis.value;
                        thisButton.VarButtonX.CurrentValue = thisButton.ButtonXValue;
                        thisButton.VarButtonY.CurrentValue = thisButton.ButtonYValue;
                        if (thisButton.usingEventSettings)
                        {
                            thisButton.EventSettings.Invoke();
                        }
                    }
                }
                if (usingSteeringWheel)
                {
                    foreach (CSteeringWheelButton thisButton in SteeringWheelButton)
                    {
                        thisButton.ButtonValue = thisButton.ButtonAxis.axis.value;
                        thisButton.VarButton.CurrentValue = thisButton.ButtonValue;
                        if (thisButton.usingEventSettings)
                        {
                            thisButton.EventSettings.Invoke();
                        }
                    }
                }
            }
        }
    }
}
