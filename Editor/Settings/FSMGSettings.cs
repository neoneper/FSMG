
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using XNode.FSMG;

namespace XNodeEditor.FSMG
{
    public class FSMGSettings : ScriptableObject
    {
        public const string _customSettingsPath = "Assets/Editor/FSMGSettings/FSMGSettings.asset";

        [SerializeField]
        private TargetList targets = null;
        [SerializeField]
        private IntVarList intVars = null;
        [SerializeField]
        private FloatVarList floatVars = null;
        [SerializeField]
        private DoubleVarList doubleVars = null;
        [SerializeField]
        private BoolVarList boolVars = null;

        public string[] Targets
        {
            get
            {
                List<string> result = new List<string>() { FSMGUtility.StringTag_Undefined };
                result.AddRange(targets.Keys);
                return result.ToArray();
            }
        }
        public string[] Int_Variables
        {
            get
            {
                List<string> result = new List<string>() { FSMGUtility.StringTag_Undefined };
                result.AddRange(intVars.Keys);
                return result.ToArray();
            }
        }
        public string[] Float_Variables
        {
            get
            {
                List<string> result = new List<string>() { FSMGUtility.StringTag_Undefined };
                result.AddRange(floatVars.Keys);
                return result.ToArray();
            }
        }
        public string[] Double_Variables
        {
            get
            {
                List<string> result = new List<string>() { FSMGUtility.StringTag_Undefined };
                result.AddRange(doubleVars.Keys);
                return result.ToArray();
            }
        }
        public string[] Bool_Variables
        {
            get
            {
                List<string> result = new List<string>() { FSMGUtility.StringTag_Undefined };
                result.AddRange(boolVars.Keys);
                return result.ToArray();
            }
        }

        public static FSMGSettings GetOrCreateSettings()
        {
            if (AssetDatabase.IsValidFolder("Assets/Editor") == false)
                AssetDatabase.CreateFolder("Assets", "Editor");
            if (AssetDatabase.IsValidFolder("Assets/Editor/FSMGSettings") == false)
                AssetDatabase.CreateFolder("Assets/Editor", "FSMGSettings");


            FSMGSettings[] allSettings = ScriptableObjectUtility.GetAllInstances<FSMGSettings>();
            FSMGSettings settings = allSettings.ToList().FirstOrDefault();

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