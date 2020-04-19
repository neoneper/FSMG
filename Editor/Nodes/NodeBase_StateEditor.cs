using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNode;
using XNode.FSMG;

namespace XNodeEditor.FSMG
{
    [CustomNodeEditor(typeof(NodeBase_State))]
    public class NodeBase_StateEditor : NodeEditor
    {
        NodeBase_State node;
        Graph_State graph;
        GUIContent favoriteIcon;
       
        public override void OnHeaderGUI()
        {
            if (favoriteIcon == null)
                favoriteIcon = GetFavoriteIcon();

            node = target as NodeBase_State;
            graph = node.graph as Graph_State;
            DrawOnHeaderGUI(graph, node, favoriteIcon);

        }

        public static void DrawOnHeaderGUI(Graph_State _graph, NodeBase_State _state, GUIContent _headerFavoriteIcon)
        {

            GUI.color = Color.white;


            if (_state.IsCurrentState) GUI.color = Color.green;

            string title = _state.name;

            GUILayout.Label(title, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30), GUILayout.MinWidth(0), GUILayout.ExpandWidth(true));
            Rect labelRect = GUILayoutUtility.GetLastRect();

            if (_state.IsRootState)
            {
                GUI.color = Color.black;
                EditorGUI.LabelField(labelRect, _headerFavoriteIcon);
            }
            GUI.color = Color.white;
        }
        public static GUIContent GetFavoriteIcon()
        {
            return EditorGUIUtility.IconContent("Favorite", "Is Root Node");
        }


    }
}