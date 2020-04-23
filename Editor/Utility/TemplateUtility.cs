using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace XNodeEditor.FSMG
{
    public static class TemplateUtility
    {
        /// <summary>Creates a new C# Class.</summary>
        [MenuItem("Assets/Create/FSMG/AI/Action C# Script", false, 90)]
        private static void CreateAIAction()
        {
            string[] guids = AssetDatabase.FindAssets("FSMG_AIActionTemplate.cs");
            if (guids.Length == 0)
            {
                Debug.LogWarning("FSMG_AIActionTemplate.cs.txt not found in asset database");
                return;
            }
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            NodeEditorUtilities.CreateFromTemplate(
                "New_AIAction.cs",
                path
            );
        }

        /// <summary>Creates a new C# Class.</summary>
        [MenuItem("Assets/Create/FSMG/AI/Decision C# Script", false, 91)]
        private static void CreateAIDecision()
        {
            string[] guids = AssetDatabase.FindAssets("FSMG_AIDecisionTemplate.cs");
            if (guids.Length == 0)
            {
                Debug.LogWarning("FSMG_AIDecisionTemplate.cs.txt not found in asset database");
                return;
            }
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            NodeEditorUtilities.CreateFromTemplate(
                "New_AIDecision.cs",
                path
            );
        }
    }
}