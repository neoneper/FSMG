
using System;
using UnityEditor;
using UnityEngine;
using XNode;
using FSMG;
using XNodeEditor;
using FSMG.Components;

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
                    case GraphObjectType.FSMTarget:
                        CreateGetTargetGlobalNode(unityObject);
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
        private void CreateGetTargetGlobalNode(UnityEngine.Object go)
        {
            GameObject gameObject = (GameObject)go;
            FSMTarget fsmt_global = gameObject.GetComponent<FSMTarget>();

            float randomPosx = UnityEngine.Random.Range(-50.0f, 50.0f);
            float randomPosy = UnityEngine.Random.Range(-50.0f, 50.0f);
            Vector2 gridPos = window.WindowToGridPosition(Event.current.mousePosition);
            gridPos.x += randomPosx;
            gridPos.y += randomPosy;

            Node node = CreateNode(typeof(Node_TargetsGet), gridPos);
            Node_TargetsGet getTarget = (Node_TargetsGet)node;

            getTarget.SetTarget(fsmt_global.targetName, TargetComponentType.global);



        }

    }

    

}
