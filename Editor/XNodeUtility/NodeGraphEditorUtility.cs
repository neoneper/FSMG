using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using XNode;

namespace XNodeEditor
{
    public static class NodeGraphEditorUtility
    {
        /// <summary>
        /// Draw label field at noodle connections. Implement <seealso cref="XNode.INodeNoodleLabel"/> at their nodes to 
        /// set label properties.
        /// </summary>
        /// <param name="window">window of the NodeGraph</param>
        public static void DrawNoodleLabels(NodeEditorWindow window)
        {
            NodeGraph target = window.graph;
            List<Vector2> gridPoints = new List<Vector2>(2);
            Object selectedObject = Selection.activeObject;

            foreach (XNode.Node node in target.nodes)
            {
                //If a null node is found, return. This can happen if the nodes associated script is deleted. It is currently not possible in Unity to delete a null asset.
                if (node == null) continue;

                if (node is INodeNoodleLabel == false) continue;

                if (AllowShowLabe(node) == false) continue;

                // Draw full connections and output > reroute
                foreach (XNode.NodePort output in node.Outputs)
                {
                    //Needs cleanup. Null checks are ugly
                    Rect fromRect;
                    if (!window.portConnectionPoints.TryGetValue(output, out fromRect)) continue;

                    for (int k = 0; k < output.ConnectionCount; k++)
                    {
                        XNode.NodePort input = output.GetConnection(k);

                        // Error handling
                        if (input == null) continue; //If a script has been updated and the port doesn't exist, it is removed and null is returned. If this happens, return.
                        //if (!input.IsConnectedTo(output)) input.Connect(output);
                        Rect toRect;
                        if (!window.portConnectionPoints.TryGetValue(input, out toRect)) continue;

                        List<Vector2> reroutePoints = output.GetReroutePoints(k);

                        gridPoints.Clear();
                        gridPoints.Add(fromRect.center);
                        gridPoints.AddRange(reroutePoints);
                        gridPoints.Add(toRect.center);
                        DrawNoodleLabesPositions(gridPoints, node);


                    }
                }
            }

        }
        private static void DrawNoodleLabesPositions(List<Vector2> gridPoints, Node node)
        {
            NoodlePath path = NodeEditorPreferences.GetSettings().noodlePath;
            NoodleStroke stroke = NodeEditorPreferences.GetSettings().noodleStroke;

            float zoom = NodeEditorWindow.current.zoom;

            // convert grid points to window points
            for (int i = 0; i < gridPoints.Count; ++i)
                gridPoints[i] = NodeEditorWindow.current.GridToWindowPosition(gridPoints[i]);

            int length = gridPoints.Count;

            Vector2 point_a = Vector2.zero;
            Vector2 point_b = Vector2.zero;
            Vector2 labelPosition = Vector2.zero;

            switch (path)
            {
                case NoodlePath.Curvy:

                    if (length > 2)
                    {
                        labelPosition = gridPoints[length / 2];

                    }
                    else
                    {

                        point_a = gridPoints[0];
                        point_b = gridPoints[1];
                        labelPosition = (point_a + point_b) / 2;
                    }

                    break;
                case NoodlePath.Straight:
                case NoodlePath.Angled:


                    if (length > 2)
                    {
                        if (length % 2 == 0)
                        {
                            point_a = gridPoints[length / 2];
                            point_b = gridPoints[(length / 2) - 1];
                            labelPosition = (point_a + point_b) / 2;
                        }
                        else
                        {
                            labelPosition = gridPoints[length / 2];
                        }
                    }
                    else
                    {

                        point_a = gridPoints[0];
                        point_b = gridPoints[1];
                        labelPosition = (point_a + point_b) / 2;

                    }

                    break;
            }




            GUIContent content = new GUIContent(((INodeNoodleLabel)node).GetNoodleLabel());
            Vector2 size = EditorStyles.helpBox.CalcSize(content);

            labelPosition.y -= size.y / 2;
            labelPosition.x -= (size.x / 2);

            TextAnchor textAnchor = EditorStyles.helpBox.alignment;
            FontStyle fontStype = EditorStyles.helpBox.fontStyle;

            GUI.backgroundColor = new Color(0, 0, 0, 255);
            GUI.color = new Color(125, 125, 125, 255);            

            EditorStyles.helpBox.alignment = TextAnchor.MiddleCenter;          
            EditorStyles.helpBox.fontStyle = FontStyle.BoldAndItalic;


            Handles.Label(labelPosition, content, EditorStyles.helpBox);

            EditorStyles.helpBox.fontStyle = fontStype;
            EditorStyles.helpBox.alignment = textAnchor;

            GUI.color = Color.white;
            GUI.backgroundColor = Color.white;

        }
        private static bool AllowShowLabe(Node node)
        {
            Object selectedObject = Selection.activeObject;
            bool result = false;

            if (node is INodeNoodleLabel == false)
                return result;

            INodeNoodleLabel noodleLabel = (INodeNoodleLabel)node;

            switch (noodleLabel.GetNoodleLabelActive())
            {
                case INodeNoodleLabelActiveType.Alwes:
                    result = true;
                    break;
                case INodeNoodleLabelActiveType.Never:
                    result = false;
                    break;
                case INodeNoodleLabelActiveType.Selected:
                    result = selectedObject == node;
                    break;
                case INodeNoodleLabelActiveType.SelectedPair:

                    result = selectedObject == node;

                    if (!result)
                    {

                        List<NodePort> ports = node.Outputs.ToList();
                        ports.RemoveAll(r => r.Connection == null);
                        ports.RemoveAll(r => r.Connection.node == null);
                        if (ports.Count > 0)
                            result = ports.Exists(r => r.Connection.node == selectedObject);
                    }
                    break;
            }


            return result;

        }

    }
}