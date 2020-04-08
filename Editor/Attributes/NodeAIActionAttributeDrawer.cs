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
    [CustomPropertyDrawer(typeof(NodeAIActionAttribute))]
    public class NodeAIActionAttributeDrawer : PropertyDrawer
    {
        private GUIContent gotoIcon;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
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

            EditorGUI.BeginChangeCheck();
            position = EditorGUI.PrefixLabel(position, label);

            Rect buttonRect = position;
            Rect buttonGo = position;

            string buttonLabel = "Select";
            AI_ActionBase currentAiActionBase = property.objectReferenceValue as AI_ActionBase;


            if (currentAiActionBase != null)
            {
                buttonLabel = currentAiActionBase.name;
            }

            if (GUI.Button(buttonRect, buttonLabel, EditorStyles.miniButton))
            {
                NodeAIActionAttribute attr = (NodeAIActionAttribute)attribute;

                if (attr.UseNodeEnum)
                    NodeEditorWindow.current.onLateGUI += () => ShowContextMenuAtMouse(property, currentAiActionBase);
                else
                {
                    ShowContextMenuAtMouse(property, currentAiActionBase);
                }
            }

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();

        }
        private void ShowContextMenuAtMouse(SerializedProperty property, AI_ActionBase currentAiActionBase)
        {
            GenericMenu menu = new GenericMenu();

            NodeBase_Action target = (NodeBase_Action)PropertyUtility.GetTargetObjectWithProperty(property);


            menu.AddItem(new GUIContent("None"), currentAiActionBase == null, () => SelectMatInfo(property, null, target));

            string[] guids = AssetDatabase.FindAssets("t:AI_ActionBase");
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                AI_ActionBase matInfo = AssetDatabase.LoadAssetAtPath(path, typeof(AI_ActionBase)) as AI_ActionBase;

                if (matInfo != null && target != null)
                {
                    if (matInfo.Graph == target.graph || matInfo.Graph == null)
                    {
                        if (matInfo != currentAiActionBase)
                        {
                            GUIContent content = new GUIContent(matInfo.name);
                            string[] nameParts = matInfo.name.Split(' ');
                            if (nameParts.Length > 1) content.text = nameParts[0] + "/" + matInfo.name.Substring(nameParts[0].Length + 1);
                            menu.AddItem(content, matInfo == currentAiActionBase, () => SelectMatInfo(property, matInfo, target));
                        }
                    }
                }
            }

            
            menu.ShowAsContext();
        }

        private void SelectMatInfo(SerializedProperty property, AI_ActionBase stateNode, object target)
        {
            NodeAIActionAttribute nodeFilter = (NodeAIActionAttribute)attribute;

            property.objectReferenceValue = stateNode;
            FieldInfo fi = ReflectionUtility.GetField(target, property.name);
            object oldValue = fi.GetValue(target);
            property.serializedObject.ApplyModifiedProperties(); // We must apply modifications so that the new value is updated in the serialized object
            object newValue = fi.GetValue(target);
            property.serializedObject.Update();

            MethodInfo callbackMethod = ReflectionUtility.GetMethod(target, nodeFilter.CallbackName);
            PropertyUtility.InvoKeCallback(callbackMethod, property, target, fieldInfo, oldValue, newValue);
        }

    }
}