using XNode.FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


namespace XNode.FSMG
{


	public abstract class NodeBase_Decision : Node, INodeNoodleLabel
	{
		public abstract bool Execute(FSMBehaviour fsm);
        public override object GetValue(NodePort port)
        {
            return this;
        }
               

        //INodeNoodles: Label das conexões
        public virtual string GetNoodleLabel()
        {
            return this.name;
        }
        public virtual INodeNoodleLabelActiveType GetNoodleLabelActive()
        {
            return  INodeNoodleLabelActiveType.Selected;
        }
    }
}