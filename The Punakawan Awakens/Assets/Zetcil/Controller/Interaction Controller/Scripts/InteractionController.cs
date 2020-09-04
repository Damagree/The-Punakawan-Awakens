using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class InteractionController : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;

        [Header("Vector Settings")]
        public VarVector3 InputVector;

        [System.Serializable]
        public class CAxis2Horizontal
        {
            [Header("Axis Button")]
            public AxisInputUI LeftButton;
            public AxisInputUI RightButton;
        }

        [System.Serializable]
        public class CAxis2Vertical
        {
            [Header("Axis Button")]
            public AxisInputUI UpButton;
            public AxisInputUI DownButton;
        }

        [System.Serializable]
        public class CDirectionPad
        {
            [Header("DirectionPad Button")]
            public Dpad PadButton;
        }

        [System.Serializable]
        public class CAnalogPad
        {
            [Header("AnalogPad Button")]
            public Analog AnalogButton;
        }

        [System.Serializable]
        public class CSteeringWheel
        {
            [Header("SteeringWheel Button")]
            public SteeringWheel Wheel;
        }

        [System.Serializable]
        public class CActionButton
        {
            [Header("Axis Button")]
            public AxisInputUI ActionButton;
            public VarBoolean ActionStatus;
            public float CoolDown;
        }

        [Header("Axis 2Horizontal Settings")]
        public bool usingAxis2Horizontal;
        public CAxis2Horizontal Axis2Horizontal;

        [Header("Axis 2Vertical Settings")]
        public bool usingAxis2Vertical;
        public CAxis2Vertical Axis2Vertical;

        [Header("DirectionPad Settings")]
        public bool usingDirectionPad;
        public CDirectionPad DirectionPad;

        [Header("Jump Button Settings")]
        public bool usingJumpButton;
        public AxisInputUI JumpButton;

        [Header("Action Button Settings")]
        public bool usingActionButton;
        public List<CActionButton> ActionButton;

        [Header("AnalogPad Settings")]
        public bool usingAnalogPad;
        public CAnalogPad AnalogPad;

        [Header("Steering Settings")]
        public bool usingSteering;
        public CSteeringWheel Steering;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
           if (usingAxis2Horizontal)
           {
                InputVector.CurrentValue.x = 0;
               if (Axis2Horizontal.LeftButton.isPress)
               {
                    InputVector.CurrentValue.x = Axis2Horizontal.LeftButton.axis.value;
               }
               if (Axis2Horizontal.RightButton.isPress)
               {
                    InputVector.CurrentValue.x = Axis2Horizontal.RightButton.axis.value;
               }
           }
           if (usingAxis2Vertical)
           {
                InputVector.CurrentValue.y = 0;
               if (Axis2Vertical.UpButton.isPress)
               {
                    InputVector.CurrentValue.y = Axis2Vertical.UpButton.axis.value;
               }
               if (Axis2Vertical.DownButton.isPress)
               {
                    InputVector.CurrentValue.y = Axis2Vertical.DownButton.axis.value;
               }
           }
           if (usingDirectionPad)
           {
                InputVector.CurrentValue.x = 0;
                InputVector.CurrentValue.y = 0;
               if (DirectionPad.PadButton.isPress)
               {
                    InputVector.CurrentValue.x = DirectionPad.PadButton.xAxis.value;
                    InputVector.CurrentValue.y = DirectionPad.PadButton.yAxis.value;
               }
           }
           if (usingAnalogPad)
           {
                InputVector.CurrentValue.x = 0;
                InputVector.CurrentValue.y = 0;
               if (AnalogPad.AnalogButton.isPress)
               {
                    InputVector.CurrentValue.x = AnalogPad.AnalogButton.xAxis.value;
                    InputVector.CurrentValue.y = AnalogPad.AnalogButton.yAxis.value;
               }
           }
           if (usingSteering)
           {
               InputVector.CurrentValue.x = 0;
               InputVector.CurrentValue.y = 0;
               if (Steering.Wheel.isPress)
               {
                    InputVector.CurrentValue.x = Steering.Wheel.axis.value;
               }
           }
           if (usingJumpButton)
           {
               if (JumpButton.isPress)
               {
                    InputVector.CurrentValue.y = JumpButton.axis.value;
               }
           }
           if (usingActionButton)
           {
               for (int i = 0; i < ActionButton.Count; i++)
               {
                   ActionButton[i].ActionStatus.CurrentValue = ActionButton[i].ActionButton.isPress;
               }
           }
        }
    }
}
