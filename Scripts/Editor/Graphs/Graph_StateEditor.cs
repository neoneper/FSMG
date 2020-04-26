
using System;
using UnityEditor;
using UnityEngine;
using XNode;
using FSMG;
using XNodeEditor;


namespace FSMGEditor
{
    [CustomNodeGraphEditor(typeof(Graph_State))]
    public class Graph_StateEditor : NodeGraphEditor
    {

        Graph_State graph;

        /// <summary> 
        /// Overriding GetNodeMenuName lets you control if and how nodes are categorized.
        /// In this example we are sorting out all node types that are not in the XNode.Examples namespace.
        /// </summary>
        public override string GetNodeMenuName(System.Type type)
        {
            if (type.Namespace == "FSMG")
            {
                string m = base.GetNodeMenuName(type).Replace("FSMG/Graph_State/", "");
                return m;
            }
            else
            {
                return null;
            }
        }

        public override void OnGUI()
        {
            base.OnGUI();

            if (graph == null)
                graph = (Graph_State)target;

            GUILayout.BeginHorizontal(EditorStyles.toolbar, GUILayout.ExpandWidth(true));

            //if (GUILayout.Button("Play", EditorStyles.toolbarButton)) { graph.UpdateGraph(null); }
            if (GUILayout.Button("Globals", EditorStyles.toolbarButton)) { FSMGSettingsPreferences.OpenSeetingsWindows(); }


            GUILayout.EndHorizontal();

            NodeGraphEditorUtility.DrawNoodleLabels(window);
        }

        public override void OnCreate()
        {
            // Undo.ClearAll();

            if (graph == null)
                graph = (Graph_State)target;

            graph.SetSettings(FSMGSettingsPreferences.GetOrCreateSettings());


            base.OnCreate();

            //   Undo.RecordObject(graph, "OnCreate");

        }

        public override Node CreateNode(Type type, Vector2 position)
        {
            Node node = base.CreateNode(type, position);

            // Undo.RecordObject(graph, "CreateNode");
            //Undo.RegisterCompleteObjectUndo(graph, "CreateNode");
            return node;
        }

        public override void RemoveNode(Node node)
        {
            base.RemoveNode(node);
        }

        public override void OnDropObjects(UnityEngine.Object[] objects)
        {

            Event.current.Use();

         
            foreach (UnityEngine.Object unityObject in objects)
            {
                switch (unityObject.GetFSMGType())
                {
                    case GraphObjectType.AIActionBase:
                        CreateCustomActionNode(unityObject);
                        break;
                    case GraphObjectType.AIDecisionBase:
                        CreateCustomDecisionNode(unityObject);
                        break;
                    default:
                        break;
                }

            }

            // base.OnDropObjects(objects);
        }

        private void CreateCustomActionNode(UnityEngine.Object go)
        {
            float randomPosx = UnityEngine.Random.Range(-50.0f, 50.0f);
            float randomPosy = UnityEngine.Random.Range(-50.0f, 50.0f);
            Vector2 gridPos = window.WindowToGridPosition(Event.current.mousePosition);
            gridPos.x += randomPosx;
            gridPos.y += randomPosy;
          
            Node node = CreateNode(typeof(Node_ActionCustom), gridPos);
            Node_ActionCustom actionNode = (Node_ActionCustom)node;
            actionNode.SetAI_ActionBase((AI_ActionBase)go);
        }
        private void CreateCustomDecisionNode(UnityEngine.Object go)
        {
          
            float randomPosx = UnityEngine.Random.Range(-50.0f, 50.0f);
            float randomPosy = UnityEngine.Random.Range(-50.0f, 50.0f);
            Vector2 gridPos = window.WindowToGridPosition(Event.current.mousePosition);
            gridPos.x += randomPosx;
            gridPos.y += randomPosy;

            Node node = CreateNode(typeof(Node_DecisionCustom), gridPos);
            Node_DecisionCustom decisionNode = (Node_DecisionCustom)node;
            decisionNode.SetAI_DecisionBase((AI_DecisionBase)go);
        }

    }




    [CustomEditor(typeof(Graph_State), true)]
    public class GlobalGraphEditor : Editor
    {
        private bool show_newVarPanel = false;
        private bool show_defaultErrorPanel = false;

        private string new_varErrorMsg = "";
        private string new_varName = FSMGUtility.StringTag_Undefined;
        private GraphVarType new_varType = GraphVarType.Integer;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (GUILayout.Button("Edit graph"))
            {
                NodeEditorWindow.Open(serializedObject.targetObject as XNode.NodeGraph);
            }

            Draw_CreateVariablePanel();
            DrawDefaultErrorPanel();

            GUILayout.Space(EditorGUIUtility.singleLineHeight);
            GUILayout.Label("Raw data", "BoldLabel");

            DrawDefaultInspector();

            serializedObject.ApplyModifiedProperties();
        }
        private void Draw_CreateVariablePanel()
        {
            Graph_State graph = (Graph_State)target;

            if (graph == null) { show_newVarPanel = false; return; }


            if (show_newVarPanel == false)
            {
                if (GUILayout.Button("Create New Variable")) { ClearNewVarProperties(true); }
            }
            else if (show_newVarPanel)
            {
                if (GUILayout.Button("Cancel")) { ClearNewVarProperties(); }


                //Create Pannel
                EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

                //NewVar field name
                new_varName = EditorGUILayout.TextField(new_varName);
                //NewVar field type
                new_varType = (GraphVarType)EditorGUILayout.EnumPopup(new_varType);
                //Create newVariable at fsm componnent and reply the tag at graph
                if (GUILayout.Button("Create"))
                {
                    GraphVarAddErrorsType error = graph.AddTagVariable(new_varName, new_varType);

                    if (error != GraphVarAddErrorsType.none)
                    {
                        show_defaultErrorPanel = true;
                        new_varErrorMsg = "Error: " + error.ToString();
                    }

                    new_varType = GraphVarType.Integer;
                    new_varName = FSMGUtility.StringTag_Undefined;
                    show_newVarPanel = false;
                }

                EditorGUILayout.EndHorizontal();
            }
        }
        private void DrawDefaultErrorPanel()
        {
            if (show_defaultErrorPanel)
            {
                EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
                EditorGUILayout.LabelField(new_varErrorMsg);
                EditorGUILayout.EndHorizontal();
            }
        }
        private void ClearNewVarProperties(bool openNewVarPanel = false)
        {
            show_defaultErrorPanel = false;
            new_varErrorMsg = "";
            new_varName = FSMGUtility.StringTag_Undefined;
            new_varType = GraphVarType.Integer;
            show_newVarPanel = openNewVarPanel;
        }


    }

}
