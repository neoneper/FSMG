using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XNode 
{
    public enum INodeNoodleLabelActiveType
    {
        Never,
        Selected,
        Alwes,
        SelectedPair
    }
    public interface INodeNoodleLabel
    {
        string GetNoodleLabel();
        INodeNoodleLabelActiveType GetNoodleLabelActive();

    }
}