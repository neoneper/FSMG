using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSMG.Components;

using XNode;
namespace FSMG
{
    [NodeTint("#4AA7FF")]
    public abstract class NodeBase_Action : Node, INodeNoodleLabel
	{
		public abstract void Execute(FSMBehaviour fsm);
        
        public override object GetValue(NodePort port)
        {
            return this;
        }

        //INodeNoodles: Label das conexões
        public virtual string GetNoodleLabel(NodePort port)
        {
            return this.name;
        }
        public virtual INodeNoodleLabelActiveType GetNoodleLabelActive()
        {
            return INodeNoodleLabelActiveType.Never;
        }

       
    }
}