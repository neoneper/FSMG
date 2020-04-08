using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode.FSMG;
using XNode.FSMG.Components;

[CreateAssetMenu(menuName = "FSMG/AI/Actions/AgentStop")]
public class AI_ActionStopAgent : AI_ActionBase
{
    public override void Execute(FSMBehaviour fsm)
    {
        fsm.navMeshAgent.isStopped = true;
        fsm.navMeshAgent.ResetPath();
        fsm.navMeshAgent.velocity = Vector3.zero;

        if (fsm.rigidBody.isKinematic == false)
            fsm.rigidBody.velocity = Vector3.zero;
    }
}
