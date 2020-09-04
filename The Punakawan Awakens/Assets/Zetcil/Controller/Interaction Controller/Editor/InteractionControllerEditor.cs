using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{
    [CustomEditor(typeof(InteractionController)), CanEditMultipleObjects]
    public class InteractionControllerEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            InputVector,
            usingAxis2Horizontal,
            Axis2Horizontal,
            usingAxis2Vertical,
            Axis2Vertical,
            usingAnalogPad,
            AnalogPad,
            usingDirectionPad,
            DirectionPad,
            usingSteering,
            Steering,
            usingJumpButton,
            JumpButton,
            usingActionButton,
            ActionButton
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");
            InputVector = serializedObject.FindProperty("InputVector");
            usingAxis2Horizontal = serializedObject.FindProperty("usingAxis2Horizontal");
            Axis2Horizontal = serializedObject.FindProperty("Axis2Horizontal");
            usingAxis2Vertical = serializedObject.FindProperty("usingAxis2Vertical");
            Axis2Vertical = serializedObject.FindProperty("Axis2Vertical");
            usingAnalogPad = serializedObject.FindProperty("usingAnalogPad");
            AnalogPad = serializedObject.FindProperty("AnalogPad");
            usingDirectionPad = serializedObject.FindProperty("usingDirectionPad");
            DirectionPad = serializedObject.FindProperty("DirectionPad");
            usingSteering = serializedObject.FindProperty("usingSteering");
            Steering = serializedObject.FindProperty("Steering");
            usingJumpButton = serializedObject.FindProperty("usingJumpButton");
            JumpButton = serializedObject.FindProperty("JumpButton");
            usingActionButton = serializedObject.FindProperty("usingActionButton");
            ActionButton = serializedObject.FindProperty("ActionButton");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(isEnabled);

            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(InputVector, true);
                if (InputVector.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }


                EditorGUILayout.PropertyField(usingAxis2Horizontal, true);
                if (usingAxis2Horizontal.boolValue)
                {
                    EditorGUILayout.PropertyField(Axis2Horizontal, true);
                }

                EditorGUILayout.PropertyField(usingAxis2Vertical, true);
                if (usingAxis2Vertical.boolValue)
                {
                    EditorGUILayout.PropertyField(Axis2Vertical, true);
                }

                EditorGUILayout.PropertyField(usingDirectionPad, true);
                if (usingDirectionPad.boolValue)
                {
                    EditorGUILayout.PropertyField(DirectionPad, true);
                }

                EditorGUILayout.PropertyField(usingJumpButton, true);
                if (usingJumpButton.boolValue)
                {
                    EditorGUILayout.PropertyField(JumpButton, true);
                }

                EditorGUILayout.PropertyField(usingActionButton, true);
                if (usingActionButton.boolValue)
                {
                    EditorGUILayout.PropertyField(ActionButton, true);
                }

                EditorGUILayout.PropertyField(usingAnalogPad, true);
                if (usingAnalogPad.boolValue)
                {
                    EditorGUILayout.PropertyField(AnalogPad, true);
                }

                EditorGUILayout.PropertyField(usingSteering, true);
                if (usingSteering.boolValue)
                {
                    EditorGUILayout.PropertyField(Steering, true);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}