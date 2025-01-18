#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CustomUILayout
{
    [CustomEditor(typeof(CircularGameObjectLayout))]
    public class CircularGameObjectLayoutEditor : Editor
    {
        private CircularGameObjectLayout layout;

        // SerializedProperties for fields
        private SerializedProperty radius;
        private SerializedProperty angleDelta;
        private SerializedProperty startDirection;
        private SerializedProperty controlChildSize;
        private SerializedProperty childSize;

        // ��ʼ�� SerializedProperties
        private void OnEnable()
        {
            layout = (CircularGameObjectLayout)target;

            radius = serializedObject.FindProperty("radius");
            angleDelta = serializedObject.FindProperty("angleDelta");
            startDirection = serializedObject.FindProperty("startDirection");
            controlChildSize = serializedObject.FindProperty("controlChildSize");
            childSize = serializedObject.FindProperty("childSize");
        }

        public override void OnInspectorGUI()
        {
            // ����������
            serializedObject.Update();

            EditorGUILayout.LabelField("Circular Layout Settings", EditorStyles.boldLabel);

            // �뾶
            EditorGUILayout.PropertyField(radius, new GUIContent("Radius"));

            // �Ƕȼ��
            EditorGUILayout.PropertyField(angleDelta, new GUIContent("Angle Delta"));

            // ��ʼ����
            string[] directionNames = { "Right", "Up", "Left", "Down" };
            startDirection.intValue = EditorGUILayout.Popup("Start Direction", startDirection.intValue, directionNames);

            // �Ƿ�����������С
            EditorGUILayout.PropertyField(controlChildSize, new GUIContent("Control Child Size"));

            // ��������˴�С���ƣ���ʾ�������С
            if (controlChildSize.boolValue)
            {
                EditorGUILayout.PropertyField(childSize, new GUIContent("Child Size"));
            }

            // Ӧ�������ĸ���
            if (serializedObject.ApplyModifiedProperties())
            {
                layout.RefreshLayout(); // �����Ը���ʱˢ�²���
            }

            // ����һ���ֶ�ˢ�°�ť
            if (GUILayout.Button("Refresh Layout Manually"))
            {
                layout.RefreshLayout();
            }
        }
    }
}
#endif