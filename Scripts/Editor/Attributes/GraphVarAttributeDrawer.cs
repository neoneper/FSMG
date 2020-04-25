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
    [CustomPropertyDrawer(typeof(GraphVarAttribute))]
    public class GraphVarAttributeDrawer : PropertyDrawer
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
            EditorGUI.BeginChangeCheck();

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
                GraphVarAttribute attr = (GraphVarAttribute)attribute;

                if (attr.UseNodeEnum)
                {

                    NodeEditorWindow.current.onLateGUI += () => ShowContextMenuAtMouse(property);
                }
                else
                {
                    ShowContextMenuAtMouse(property);
                }
            }

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();

        }
        private void ShowContextMenuAtMouse(SerializedProperty property)
        {
            GenericMenu menu = new GenericMenu();
            GraphVarAttribute attr = (GraphVarAttribute)attribute;

            object target = PropertyUtility.GetTargetObjectWithProperty(property);

            object valuesObject = GetValues(property, attr.ValuesName);
            menu.AddItem(new GUIContent(FSMTargetBehaviour.UndefinedTag), false, () => SelectMatInfo(property, FSMTargetBehaviour.UndefinedTag, target));



            if (valuesObject is IList)
            {
                IList valuesList = (IList)valuesObject;

                for (int i = 0; i < valuesList.Count; i++)
                {
                    GUIContent content = new GUIContent(valuesList[i].ToString());
                    menu.AddItem(content, valuesList[i].ToString() == property.stringValue, () => SelectMatInfo(property, content.text, target));
                }
            }

            menu.ShowAsContext();
        }

        private void SelectMatInfo(SerializedProperty property, string stateNode, object target)
        {
            GraphVarAttribute nodeFilter = (GraphVarAttribute)attribute;

            property.stringValue = stateNode;

            FieldInfo fieldInfo = ReflectionUtility.GetField(target, property.name);
            object oldValue = fieldInfo.GetValue(target);
            property.serializedObject.ApplyModifiedProperties(); // We must apply modifications so that the new value is updated in the serialized object
            object newValue = fieldInfo.GetValue(target);
            property.serializedObject.Update();



            MethodInfo callbackMethod = ReflectionUtility.GetMethod(target, nodeFilter.CallbackName);
            InvoKeCallback(callbackMethod, property, target, oldValue, newValue);
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
        private object GetValues(SerializedProperty property, string valuesName)
        {
            object target = PropertyUtility.GetTargetObjectWithProperty(property);

            FieldInfo valuesFieldInfo = ReflectionUtility.GetField(target, valuesName);
            if (valuesFieldInfo != null)
            {
                return valuesFieldInfo.GetValue(target);
            }

            PropertyInfo valuesPropertyInfo = ReflectionUtility.GetProperty(target, valuesName);
            if (valuesPropertyInfo != null)
            {
                return valuesPropertyInfo.GetValue(target);
            }

            MethodInfo methodValuesInfo = ReflectionUtility.GetMethod(target, valuesName);
            if (methodValuesInfo != null &&
                methodValuesInfo.ReturnType != typeof(void) &&
                methodValuesInfo.GetParameters().Length == 0)
            {
                return methodValuesInfo.Invoke(target, null);
            }

            return null;
        }


    }
}