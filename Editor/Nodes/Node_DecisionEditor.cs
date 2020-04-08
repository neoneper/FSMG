using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNode;
using XNode.FSMG;

namespace XNodeEditor.FSMG
{
    [CustomNodeEditor(typeof(Node_Decision))]
    public class Node_DecisionEditor : NodeEditor
    {
        NodePort output;
        bool isSelected = false;
        bool done = false;
        SerializedProperty decisionProperty;
        Node_Decision node;

        public override void OnBodyGUI()
        {
            // base.OnBodyGUI();
            Node_Decision node = (Node_Decision)target;

            if (Event.current.type != EventType.Repaint)
            {
                decisionProperty = serializedObject.FindProperty("aiDecision");
                isSelected = Selection.activeObject == node;

                if (output == null)
                    output = node.GetOutputPort("outDecision");

                done = Event.current.type == EventType.Layout;
            }


            if (isSelected)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(decisionProperty, GUIContent.none, true, GUILayout.MinWidth(0));
                NodeEditorGUILayout.PortField(GUIContent.none, output, GUILayout.MaxWidth(0));
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                if (node.AIDecision != null)
                    EditorGUILayout.LabelField(node.AIDecision.name, GUILayout.MinWidth(0));
                else
                    EditorGUILayout.LabelField("Undefined", GUILayout.MinWidth(0));

                NodeEditorGUILayout.PortField(GUIContent.none, output, GUILayout.MaxWidth(0));
                EditorGUILayout.EndHorizontal();
            }

        }


    }
}