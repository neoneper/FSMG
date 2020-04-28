using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using FSMG;
using FSMG.Components;
using XNodeEditor;

namespace FSMGEditor
{
    [CustomPropertyDrawer(typeof(FSMTargetsAttribute))]
    public class FSMTargetsAttributeDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {


            EditorGUI.BeginProperty(position, label, property);

            ContextPopUp(position, property, label);

            EditorGUI.EndProperty();

        }

        private void ContextPopUp(Rect position, SerializedProperty property, GUIContent label)
        {
            // Throw error on wrong type
            if (property.propertyType != SerializedPropertyType.String)
            {
                throw new ArgumentException("Parameter selected must be of type System.String");
            }

            // Add label
            // position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);


            EditorGUI.BeginChangeCheck();

            // Store old indent level and set it to 0, the PrefixLabel takes care of it

            // position = EditorGUI.PrefixLabel(position, label);

            //int indent = EditorGUI.indentLevel;
            //EditorGUI.indentLevel = 0;

            Rect buttonRect = position;
            Rect buttonGo = position;


            string currentValue = property.stringValue;
            if (string.IsNullOrEmpty(currentValue))
            {
                property.stringValue = FSMTargetBehaviour.UndefinedTag;
                currentValue = property.stringValue;
            }




            if (GUI.Button(buttonRect, currentValue))
            {
                FSMTargetsAttribute attr = (FSMTargetsAttribute)attribute;

                if (attr.UseNodeEnum)
                {

                    NodeEditorWindow.current.onLateGUI += () => ShowContextMenuAtMouse(property);
                }
                else
                {
                    ShowContextMenuAtMouse(property);
                }
            }


            // position.x += buttonRect.width + 4;
            // position.width -= buttonRect.width + 4;
            //EditorGUI.ObjectField(position, property, typeof(StateNode), GUIContent.none);

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();

            //EditorGUI.indentLevel = indent;
        }
        private void ShowContextMenuAtMouse(SerializedProperty property)
        {
            GenericMenu menu = new GenericMenu();
            FSMTargetsAttribute attr = (FSMTargetsAttribute)attribute;

            object target = PropertyUtility.GetTargetObjectWithProperty(property);

            menu.AddItem(new GUIContent(FSMTargetBehaviour.UndefinedTag), false, () => SelectMatInfo(property, FSMTargetBehaviour.UndefinedTag, target));

            List<string> guids = null;

            List<FSMTargetBehaviour> _targets = Resources.FindObjectsOfTypeAll<FSMTargetBehaviour>().ToList();;

            if (attr.IsUseOwnListTargets == false)
            {
                guids = FSMGSettingsPreferences.GetOrCreateSettings().TargetNames.ToList();
               
            }
            else
            {
                guids = GetTargesFromOwnList(property, target);
            }

            _targets.RemoveAll(r => r.IsUndefindedTarget);

            if (guids != null)
            {
                for (int i = 0; i < guids.Count; i++)
                {

                    GUIContent content = new GUIContent(guids[i]);

                    if (attr.IsFilterEnnable == true && _targets.Exists(r => r.targetName.Equals(guids[i]))
                        || (guids[i] == FSMTargetBehaviour.UndefinedTag && guids[i] == property.stringValue))
                    {
                        menu.AddDisabledItem(content, guids[i] == property.stringValue);
                    }
                    else
                    {
                        menu.AddItem(content, guids[i] == property.stringValue, () => SelectMatInfo(property, content.text, target));
                    }

                }
            }

            menu.ShowAsContext();
        }

        private void SelectMatInfo(SerializedProperty property, string stateNode, object target)
        {
            FSMTargetsAttribute nodeFilter = (FSMTargetsAttribute)attribute;

            property.stringValue = stateNode;

            FieldInfo fieldInfo = ReflectionUtility.GetField(target, property.name);
            object oldValue = fieldInfo.GetValue(target);
            property.serializedObject.ApplyModifiedProperties(); // We must apply modifications so that the new value is updated in the serialized object
            object newValue = fieldInfo.GetValue(target);
            property.serializedObject.Update();

            MethodInfo callbackMethod = ReflectionUtility.GetMethod(target, nodeFilter.CallbackName);

            InvoKeCallback(callbackMethod, property, target, oldValue, newValue);
        }

        private List<string> GetTargesFromOwnList(SerializedProperty property, object target)
        {

            FSMTargetsAttribute nodeFilter = (FSMTargetsAttribute)attribute;
            MethodInfo func = ReflectionUtility.GetMethod(target, nodeFilter.GetListFunctionName);

            List<string> result = null;

            if (func != null && func.ReturnType == typeof(List<string>) && func.GetParameters().Length == 0)
            {
                result = (List<string>)func.Invoke(target, new object[] { });
            }

            return result;
        }
        private void InvoKeCallback(MethodInfo callbackMethod, SerializedProperty property, object target, object oldValue, object newValue)
        {


            if (callbackMethod != null &&
                    callbackMethod.ReturnType == typeof(void) &&
                    callbackMethod.GetParameters().Length == 2)
            {
                ParameterInfo oldValueParam = callbackMethod.GetParameters()[0];
                ParameterInfo newValueParam = callbackMethod.GetParameters()[1];

                if (fieldInfo.FieldType == oldValueParam.ParameterType &&
                    fieldInfo.FieldType == newValueParam.ParameterType)
                {
                    callbackMethod.Invoke(target, new object[] { oldValue, newValue });
                }
                else
                {
                    string warning = string.Format(
                        "The field '{0}' and the parameters of callback '{1}' must be of the same type." + Environment.NewLine +
                        "Field={2}, Param0={3}, Param1={4}",
                        fieldInfo.Name, callbackMethod.Name, fieldInfo.FieldType, oldValueParam.ParameterType, newValueParam.ParameterType);

                    Debug.LogWarning(warning, property.serializedObject.targetObject);
                }
            }

        }
    }
}