using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using XNode;
using XNode.FSMG;

namespace XNodeEditor.FSMG
{
    [CustomPropertyDrawer(typeof(GraphStateAttribute))]
    public class StateGraphFilterDrawer : PropertyDrawer
    {
        private GUIContent gotoIcon;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (gotoIcon == null)
                gotoIcon = EditorGUIUtility.IconContent("BillboardAsset Icon", "GoToAsset");

            EditorGUI.BeginProperty(position, label, property);

            ContextPopUp(position, property, label);

            EditorGUI.EndProperty();

        }

        private void ContextPopUp(Rect position, SerializedProperty property, GUIContent label)
        {
            // Throw error on wrong type
            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                throw new ArgumentException("Parameter selected must be of type System.Enum");
            }

            // Add label
            // position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);


            EditorGUI.BeginChangeCheck();

            // Store old indent level and set it to 0, the PrefixLabel takes care of it

            position = EditorGUI.PrefixLabel(position, label);

            //int indent = EditorGUI.indentLevel;
            //EditorGUI.indentLevel = 0;

            Rect buttonRect = position;
            Rect buttonGo = position;

            string buttonLabel = "Select";
            Graph_State currentStateGraph = property.objectReferenceValue as Graph_State;


            if (currentStateGraph != null)
            {
                buttonRect.width -= 30;
                buttonRect.position = new Vector2(position.x + 30, position.y);
                buttonGo.width = 30;

                buttonLabel = currentStateGraph.name;
            }

            if (GUI.Button(buttonRect, buttonLabel))
            {
                GraphStateAttribute attr = (GraphStateAttribute)attribute;

                if (attr.UseNodeEnum)
                    NodeEditorWindow.current.onLateGUI += () => ShowContextMenuAtMouse(property, currentStateGraph);
                else
                {
                    ShowContextMenuAtMouse(property, currentStateGraph);
                }
            }

            if (currentStateGraph != null)
            {
                if (GUI.Button(buttonGo, gotoIcon))
                {
                    EditorGUIUtility.PingObject(currentStateGraph);
                    NodeEditorWindow.Open(currentStateGraph);
                    Selection.activeObject = currentStateGraph;

                }
            }



            // position.x += buttonRect.width + 4;
            // position.width -= buttonRect.width + 4;
            //EditorGUI.ObjectField(position, property, typeof(StateNode), GUIContent.none);

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();

            //EditorGUI.indentLevel = indent;
        }
        private void ShowContextMenuAtMouse(SerializedProperty property, Graph_State currentStateGraph)
        {
            GenericMenu menu = new GenericMenu();

            //StateNodeBase target = (StateNodeBase)PropertyUtility.GetTargetObjectWithProperty(property);
            object target = PropertyUtility.GetTargetObjectWithProperty(property);


            menu.AddItem(new GUIContent("None"), currentStateGraph == null, () => SelectMatInfo(property, null, target));

            string[] guids = AssetDatabase.FindAssets("t:Graph_State");
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                Graph_State matInfo = AssetDatabase.LoadAssetAtPath(path, typeof(Graph_State)) as Graph_State;
                if (matInfo != null)
                {
                    bool isOwn = false;
                    if (target is Graph_State)
                        if ((Graph_State)target == matInfo)
                            isOwn = true;

                    //if (matInfo.ParentGraph == null && isOwn == false)
                    if (isOwn == false)//Não mostra o proprio grafico.
                    {
                        GUIContent content = new GUIContent(matInfo.name);
                        string[] nameParts = matInfo.name.Split(' ');
                        if (nameParts.Length > 1) content.text = nameParts[0] + "/" + matInfo.name.Substring(nameParts[0].Length + 1);
                        menu.AddItem(content, matInfo == currentStateGraph, () => SelectMatInfo(property, matInfo, target));
                    }
                }
            }

            menu.ShowAsContext();
        }

        private void SelectMatInfo(SerializedProperty property, Graph_State stateNode, object target)
        {
            GraphStateAttribute nodeFilter = (GraphStateAttribute)attribute;

            property.objectReferenceValue = stateNode;
            FieldInfo fi = ReflectionUtility.GetField(target, property.name);
            object oldValue = fi.GetValue(target);
            property.serializedObject.ApplyModifiedProperties(); // We must apply modifications so that the new value is updated in the serialized object
            object newValue = fi.GetValue(target);
            property.serializedObject.Update();

            if (stateNode) EditorGUIUtility.PingObject(stateNode);

            MethodInfo callbackMethod = ReflectionUtility.GetMethod(target, nodeFilter.CallbackName);
            PropertyUtility.InvoKeCallback(callbackMethod, property, target, fieldInfo, oldValue, newValue);
        }

    }
}