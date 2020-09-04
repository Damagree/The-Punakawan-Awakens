using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VectorController)), CanEditMultipleObjects]
    public class VectorControllerEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TargetController,
           TargetVectorInput,
           MoveSpeed,
           RotateSpeed,
           gravity
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetController = serializedObject.FindProperty("TargetController");
            TargetVectorInput = serializedObject.FindProperty("TargetVectorInput");
            MoveSpeed = serializedObject.FindProperty("MoveSpeed");
            RotateSpeed = serializedObject.FindProperty("RotateSpeed");
            gravity = serializedObject.FindProperty("gravity");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(TargetController, true);
                if (TargetController.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(TargetVectorInput, true);
                if (TargetVectorInput.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(MoveSpeed, true);
                EditorGUILayout.PropertyField(RotateSpeed, true);
                EditorGUILayout.PropertyField(gravity, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}