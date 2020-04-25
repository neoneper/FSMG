using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using FSMG;

namespace FSMGEditor
{
    [CustomPropertyDrawer(typeof(CallbackAttribute))]
    public class CallbackAttributeDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {


            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.BeginChangeCheck();


            EditorGUI.PropertyField(position, property, true);

            if (EditorGUI.EndChangeCheck())
            {
                if (property.serializedObject.ApplyModifiedProperties())
                {
                    Debug.Log("finish");

                }
            }

            EditorGUI.EndProperty();

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