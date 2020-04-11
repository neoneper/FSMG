﻿using XNode.FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using UnityEngine.AI;

namespace XNode.FSMG
{
    [CreateNodeMenu("Decisions/FinishPath")]
    public class Node_Decision_FinishPath : NodeBase_Decision
    {
     
        [Output(typeConstraint = TypeConstraint.Strict)]
        public NodeBase_Decision outDecision;

    

        public override bool Execute(FSMBehaviour fsm)
        {
            if (Application.isEditor && Application.isPlaying == false)
                return false;

            return CheckDecision(fsm);
        }       

        private bool CheckDecision(FSMBehaviour fsm)
        {
            NavMeshAgent agent = fsm.navMeshAgent;

            if (agent.remainingDistance <= agent.stoppingDistance + fsm.agentStats.agent.pathEndThreshold)
            {
                if (agent.pathPending == false)
                {
                    //agent.isStopped = true;
                    return true;
                }
            }

            return false;
        }

    }
}