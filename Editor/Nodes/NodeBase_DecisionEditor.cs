using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNode;
using XNode.FSMG;

namespace XNodeEditor.FSMG
{
    [CustomNodeEditor(typeof(NodeBase_Decision))]
    public class NodeBase_DecisionEditor : NodeEditor
    {
        public enum ShowType
        {
            Nothing,
            InOutPair,
            OutInPair,
            Individual
        }

        List<NodePort> outputs;
        List<NodePort> inputs;
        ShowType showtype = ShowType.Nothing;
        bool isSelected = false;
        bool done = false;

        NodeBase_Decision node;

        public override void OnBodyGUI()
        {
            if (Event.current.type != EventType.Repaint)
            {
                NodeBase_Decision node = (NodeBase_Decision)target;

                isSelected = Selection.activeObject == node;

                if (outputs == null)
                    outputs = new List<NodePort>(node.Outputs);
                if (inputs == null)
                    inputs = new List<NodePort>(node.Inputs);

                if (inputs.Count > 0 && outputs.Count > 0)
                {
                    if (inputs.Count <= outputs.Count) { showtype = ShowType.InOutPair; }
                    else { showtype = ShowType.OutInPair; }
                }
                else { showtype = ShowType.Individual; }

                done = Event.current.type == EventType.Layout;
            }

            if (isSelected)
                base.OnBodyGUI();
            else if (done)
            {
                switch (showtype)
                {
                    case ShowType.Individual:
                    case ShowType.Nothing:
                        DrawInputOutput();
                        break;
                    case ShowType.InOutPair:
                        DrawInputOutPutPair();
                        break;
                    case ShowType.OutInPair:
                        DrawOutputInputPair();
                        break;
                }
            }
        }

        private void DrawInputOutPutPair()
        {
            int lastIndex = 0;


            for (int a = 0; a < inputs.Count; a++)
            {
                NodeEditorGUILayout.PortPair(inputs[a], outputs[a]);
                lastIndex = a;
            }

            for (int a = lastIndex; a < outputs.Count - 1; a++)
            {
                NodeEditorGUILayout.PortField(outputs[a]);
            }

        }
        private void DrawOutputInputPair()
        {

            int lastIndex = 0;

            for (int a = 0; a < outputs.Count; a++)
            {
                NodeEditorGUILayout.PortPair(inputs[a], outputs[a]);
                lastIndex = a;
            }

            for (int a = lastIndex; a < inputs.Count - 1; a++)
            {
                NodeEditorGUILayout.PortField(inputs[a]);
            }

        }
        private void DrawInputOutput()
        {
            foreach (NodePort input in inputs)
                NodeEditorGUILayout.PortField(input);
            foreach (NodePort output in outputs)
                NodeEditorGUILayout.PortField(output);
        }
    }
}