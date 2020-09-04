using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(QuaternionController)), CanEditMultipleObjects]
    public class QuaternionControllerEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           axes,
           TargetCamera,
           TargetVectorInput,
           sensitivityX,
           sensitivityY,
           minimumX,
           maximumX,
           minimumY,
           maximumY
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            axes = serializedObject.FindProperty("axes");
            TargetCamera = serializedObject.FindProperty("TargetCamera");
            TargetVectorInput = serializedObject.FindProperty("TargetVectorInput");
            sensitivityX = serializedObject.FindProperty("sensitivityX");
            sensitivityY = serializedObject.FindProperty("sensitivityY");
            minimumX = serializedObject.FindProperty("minimumX");
            maximumX = serializedObject.FindProperty("maximumX");
            minimumY = serializedObject.FindProperty("minimumY");
            maximumY = serializedObject.FindProperty("maximumY");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(axes, true);
                EditorGUILayout.PropertyField(TargetCamera, true);
                if (TargetCamera.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(TargetVectorInput, true);
                if (TargetVectorInput.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(sensitivityX, true);
                EditorGUILayout.PropertyField(sensitivityY, true);
                EditorGUILayout.PropertyField(minimumX, true);
                EditorGUILayout.PropertyField(maximumX, true);
                EditorGUILayout.PropertyField(minimumY, true);
                EditorGUILayout.PropertyField(maximumY, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}