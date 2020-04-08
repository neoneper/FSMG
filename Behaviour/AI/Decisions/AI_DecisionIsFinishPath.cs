using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using XNode.FSMG;
using XNode.FSMG.Components;

[CreateAssetMenu(menuName = "FSMG/AI/Decisions/FinishPah")]
public class AI_DecisionIsFinishPath : AI_DecisionBase
{
    public override bool Execute(FSMBehaviour fsm)
    {
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
