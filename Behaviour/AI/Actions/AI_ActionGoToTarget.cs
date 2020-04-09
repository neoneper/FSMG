using XNode.FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode.FSMG;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSMG/AI/Actions/GoToTarget")]
public class AI_ActionGoToTarget : AI_ActionBase
{
    [FSMTargets(filterIsEnnable: false), SerializeField]
    private string targetName = "";

    public override void Execute(FSMBehaviour fsm)
    {
        DoExecute(fsm, targetName);
    }

    public static void DoExecute(FSMBehaviour fsm, string targetName)
    {
        if (Application.isEditor && Application.isPlaying == false)
            return;

        NavMeshAgent agent = fsm.navMeshAgent;
        FSMTarget target = fsm.GetTarget(targetName);
        AIAgentStats agentStats = fsm.agentStats;

        Transform chase = null;

        if (targetName == FSMTarget.UndefinedTag || agentStats == null || target == null)
        {
            agent.ResetPath();
            agent.isStopped = true;
            return;
        }

        chase = target.transform;

        agent.destination = chase.position;
        agent.isStopped = false;

        if (agent.remainingDistance <= agent.stoppingDistance + agentStats.agent.pathEndThreshold)
        {
            if (agent.pathPending == false)
            {
                // agent.ResetPath();
                agent.isStopped = true;
            }
        }
    }



}
