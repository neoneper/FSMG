using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XNode 
{
    public enum INodeNoodleLabelActiveType
    {
        Never,
        Selected,
        Alwes
    }
    public interface INodeNoodleLabel
    {
        string GetNoodleLabel(NodePort port);
        INodeNoodleLabelActiveType GetNoodleLabelActive();

    }
}