﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using FSMG;

using XNodeEditor;   namespace FSMGEditor
{
    // Create MyCustomSettingsProvider by deriving from SettingsProvider:
    class FSMSettingsProvider : SettingsProvider
    {
        private SerializedObject m_CustomSettings;
        private SerializedProperty targetList;
        private SerializedProperty intList;
        private SerializedProperty floatList;
        private SerializedProperty doubleList;
        private SerializedProperty boolList;

        private int toogleIndex = 0;
        private string[] menus = new string[] { "Targets", "Integer", "Float", "Double", "Boolean" };

        class Styles
        {
            public static GUIContent targetsContent = new GUIContent("    Targets");
            public static GUIContent intContent = new GUIContent("    IntVariables");
            public static GUIContent floatContent = new GUIContent("    FloatVariables");
            public static GUIContent doubleContent = new GUIContent("    DoubleVariables");
            public static GUIContent boolContent = new GUIContent("    BooleanVariables");
        }

        public FSMSettingsProvider(string path, SettingsScope scope = SettingsScope.User) : base(path, scope) { }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            // This function is called when the user clicks on the MyCustom element in the Settings window.
            m_CustomSettings = FSMGSettingsPreferences.GetSerializedSettings();

            targetList = m_CustomSettings.FindProperty("targets");
            intList = m_CustomSettings.FindProperty("intVars");
            floatList = m_CustomSettings.FindProperty("floatVars");
            doubleList = m_CustomSettings.FindProperty("doubleVars");
            boolList = m_CustomSettings.FindProperty("boolVars");
        }

        public override void OnGUI(string searchContext)
        {
            if (m_CustomSettings == null)
            {
                EditorGUILayout.LabelField("No Settings Found");
                return;
            }

            if (targetList == null)
            {
                EditorGUILayout.LabelField("No Targets Found");
                return;
            }

            if (Event.current.type == EventType.Repaint)
                m_CustomSettings.Update();

            toogleIndex = GUILayout.SelectionGrid(toogleIndex, menus, menus.Length, EditorStyles.toolbarButton);

            switch (toogleIndex)
            {
                case 0:
                    if (targetList != null)
                        EditorGUILayout.PropertyField(targetList, Styles.targetsContent, true);
                    break;
                case 1:
                    if (intList != null)
                        EditorGUILayout.PropertyField(intList, Styles.intContent, true);
                    break;
                case 2:
                    if (floatList != null)
                        EditorGUILayout.PropertyField(floatList, Styles.floatContent, true);

                    break;
                case 3:
                    if (doubleList != null)
                        EditorGUILayout.PropertyField(doubleList, Styles.doubleContent, true);
                    break;
                case 4:
                    if (boolList != null)
                        EditorGUILayout.PropertyField(boolList, Styles.boolContent, true);
                    break;
            }


            if (Event.current.type == EventType.Repaint)
                m_CustomSettings.ApplyModifiedProperties();
        }

        // Register the SettingsProvider
        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {

            var provider = new FSMSettingsProvider("Project/FSMGraph Globals", SettingsScope.Project);

            // Automatically extract all keywords from the Styles.
            provider.keywords = GetSearchKeywordsFromGUIContentProperties<Styles>();
            return provider;



        }
    }

    public class FSMGSettingsPreferences
    {
        public const string _customSettingsPath = "Assets/Editor/FSMGSettings/FSMGSettings.asset";


        public static FSMGSettings GetOrCreateSettings()
        {
            FSMGSettings settings = null;

            if (AssetDatabase.IsValidFolder("Assets/Editor") == false)
                AssetDatabase.CreateFolder("Assets", "Editor");
            if (AssetDatabase.IsValidFolder("Assets/Editor/FSMGSettings") == false)
                AssetDatabase.CreateFolder("Assets/Editor", "FSMGSettings");

            FSMGSettings[] allSettings = ScriptableObjectUtility.GetAllInstances<FSMGSettings>();
            settings = allSettings.ToList().FirstOrDefault();

            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<FSMGSettings>();

                AssetDatabase.CreateAsset(settings, _customSettingsPath);
                AssetDatabase.SaveAssets();

            }


            return settings;
        }
        public static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }
        [MenuItem("FSMG/Globals")]
        public static void OpenSeetingsWindows()
        {

            SettingsService.OpenProjectSettings("Project/FSMGraph Globals");

        }
    }

}