using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using FSMG;
using FSMG.Components;

namespace FSMGEditor
{
    [CustomEditor(typeof(FSMBehaviour), true), CanEditMultipleObjects]
    public class FSMBehaviourEditor : Editor
    {
        private bool onSyncVariables = false;
        private bool show_newVarPanel = false;
        private bool show_defaultErrorPanel = false;
        private bool show_graphErrorPanel = false;

        private string new_varErrorMsg = "";
        private string new_varName = FSMGUtility.StringTag_Undefined;
        private GraphVarType new_varType = GraphVarType.Integer;
        FSMBehaviour fsmBehaviour;


        public override void OnInspectorGUI()
        {

            base.OnInspectorGUI();


            fsmBehaviour = (FSMBehaviour)target;

            if (onSyncVariables == false)
            {
                fsmBehaviour.SyncVariablesAndTargets();
                onSyncVariables = true;
                return;
            }



            Draw_CreateVariablePanel();
            DrawDefaultErrorPanel();
            DrawGrahErrorPanel();


        }

        private void Draw_CreateVariablePanel()
        {

            if (fsmBehaviour.graph == null) { show_newVarPanel = false; return; }


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
                    GraphVarAddErrorsType error = fsmBehaviour.AddVariable(new_varName, new_varType);

                    if (error != GraphVarAddErrorsType.none)
                    {
                        if (error == GraphVarAddErrorsType.graph_already_exists)
                            show_graphErrorPanel = true;

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
        private void DrawGrahErrorPanel()
        {
            if (show_graphErrorPanel)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                string syncAlert = "there was a sync problem with the state graph and for this reason the component updated its variables. Loss of variables may have occurred.";
                if (GUILayout.Button("Close"))
                {
                    show_graphErrorPanel = false;
                }
                EditorGUILayout.LabelField(syncAlert);
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