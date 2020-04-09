using XNode.FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using UnityEngine.AI;

namespace XNode.FSMG
{
    [CreateNodeMenu("States/Decisions/FinishPath"), NodeTint("#FFCD00"), NodeWidth(150)]
    public class Node_Decision_FinishPath : NodeBase_Decision
    {
        //usado somente para renomear o nó assim que é criado, para evitar o nome feio vindo do arquivo.cs
        private bool isAlreadyRename = false;

        [Output(typeConstraint = TypeConstraint.Strict)]
        public NodeBase_Decision outDecision;

        protected override void Init()
        {
            //Renomeia o nó na primeira vez que o nó é iniciado, isto previne o nome feio original do .cs
            if (!isAlreadyRename)
                this.name = "IsFinishPath?";

            isAlreadyRename = true;

            base.Init();
        }

        public override bool Execute(FSMBehaviour fsm)
        {
            if (Application.isEditor && Application.isPlaying == false)
                return false;

            return CheckDecision(fsm);
        }

        //Modificando as propriedades da label de conexões
        public override string GetNoodleLabel()
        {
            //Quero mostrar o nome da ação anexada ao nó caso ela exista           
            return "IsFinishPath";

        }
        public override INodeNoodleLabelActiveType GetNoodleLabelActive()
        {
            return INodeNoodleLabelActiveType.SelectedPair;
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