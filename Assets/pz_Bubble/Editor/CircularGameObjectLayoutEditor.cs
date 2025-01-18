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

        // 初始化 SerializedProperties
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
            // 启动变更检查
            serializedObject.Update();

            EditorGUILayout.LabelField("Circular Layout Settings", EditorStyles.boldLabel);

            // 半径
            EditorGUILayout.PropertyField(radius, new GUIContent("Radius"));

            // 角度间隔
            EditorGUILayout.PropertyField(angleDelta, new GUIContent("Angle Delta"));

            // 起始方向
            string[] directionNames = { "Right", "Up", "Left", "Down" };
            startDirection.intValue = EditorGUILayout.Popup("Start Direction", startDirection.intValue, directionNames);

            // 是否控制子物体大小
            EditorGUILayout.PropertyField(controlChildSize, new GUIContent("Control Child Size"));

            // 如果启用了大小控制，显示子物体大小
            if (controlChildSize.boolValue)
            {
                EditorGUILayout.PropertyField(childSize, new GUIContent("Child Size"));
            }

            // 应用所做的更改
            if (serializedObject.ApplyModifiedProperties())
            {
                layout.RefreshLayout(); // 当属性更新时刷新布局
            }

            // 增加一个手动刷新按钮
            if (GUILayout.Button("Refresh Layout Manually"))
            {
                layout.RefreshLayout();
            }
        }
    }
}
#endif