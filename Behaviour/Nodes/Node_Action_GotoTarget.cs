using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using XNode;
using XNode.FSMG.Components;

namespace XNode.FSMG
{
    [CreateNodeMenu("States/Actions/GoToTarget"), NodeTint("#0071E8")]
    public class Node_Action_GotoTarget : NodeBase_Action
    {
        //usado somente para renomear o nó assim que é criado, para evitar o nome feio vindo do arquivo.cs
        private bool isAlreadyRename = false;

        [Output]
        public NodeBase_Action outAction = null;

        [FSMTargets(filterIsEnnable: false, nodeEnum: true), SerializeField]
        private string targetName = "";


        protected override void Init()
        {
            //Renomeia o nó na primeira vez que o nó é iniciado, isto previne o nome feio original do .cs
            if (!isAlreadyRename)
                this.name = "GoToTarget";

            isAlreadyRename = true;

            base.Init();
        }
        public override void Execute(FSMBehaviour fsm)
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

        //Modificando as propriedades da label de conexões
        public override string GetNoodleLabel()
        {
            return "Go To " + targetName;
        }
        public override INodeNoodleLabelActiveType GetNoodleLabelActive()
        {
            return INodeNoodleLabelActiveType.SelectedPair;
        }


    }
}