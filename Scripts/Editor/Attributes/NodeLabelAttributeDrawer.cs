using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using FSMG;

using XNodeEditor;   namespace FSMGEditor
{
    [CustomPropertyDrawer(typeof(NodeLabelAttribute))]
    public class NodeLabelAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            NodeLabelAttribute lb = (NodeLabelAttribute)attribute;
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), lb.Label);
            EditorGUI.LabelField(position, "asdf");
            EditorGUI.EndProperty();

        }

        private void AddPortField(Rect rect, XNode.NodePort port)
        {
            if (port == null) return;

           
            NodeEditor editor = NodeEditor.GetEditor(port.node, NodeEditorWindow.current);
            Color backgroundColor = editor.GetTint();
            Color col = NodeEditorWindow.current.graphEditor.GetPortColor(port);
            DrawPortHandle(rect, backgroundColor, col);

            // Register the handle position
            Vector2 portPos = rect.center;
            NodeEditor.portPositions[port] = portPos;
        }
        public static void DrawPortHandle(Rect rect, Color backgroundColor, Color typeColor)
        {
            Color col = GUI.color;
            GUI.color = backgroundColor;
            GUI.DrawTexture(rect, NodeEditorResources.dotOuter);
            GUI.color = typeColor;
            GUI.DrawTexture(rect, NodeEditorResources.dot);
            GUI.color = col;
        }
    }
}