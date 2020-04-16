using XNode.FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace XNode.FSMG
{
    [CreateNodeMenu("Decisions/Custom")]
    public class Node_DecisionCustom : NodeBase_Decision
    {
        //usado somente para renomear o nó assim que é criado, para evitar o nome feio vindo do arquivo.cs
        private bool isAlreadyRename = false;

        [SerializeField, NodeAIDececision(true)]
        private AI_DecisionBase aiDecision = null;

        [Output(typeConstraint = TypeConstraint.Strict)]
        public NodeBase_Decision outDecision;

        public AI_DecisionBase AIDecision { get { return aiDecision; } }

        protected override void Init()
        {
            //Renomeia o nó na primeira vez que o nó é iniciado, isto previne o nome feio original do .cs
            if (!isAlreadyRename)
                this.name = "CustomDecision";

            isAlreadyRename = true;

            base.Init();
        }

        public override bool Execute(FSMBehaviour fsm)
        {
            CheckReferenceIsValid();

            if (aiDecision == null)
                return false;

            bool result = aiDecision.Execute(fsm);
            return result;
        }

        //Modificando as propriedades da label de conexões
        public override string GetNoodleLabel(NodePort port)
        {
            //Quero mostrar o nome da ação anexada ao nó caso ela exista
            string label = FSMGUtility.StringTag_Undefined;

            if (aiDecision != null)
            {
                label = aiDecision.name;
            }

            return label;

        }
        public override INodeNoodleLabelActiveType GetNoodleLabelActive()
        {
            CheckReferenceIsValid();

            //Esonde a label caso nao haja nenhuma ação referenciada
            if (aiDecision == null)
                return INodeNoodleLabelActiveType.Never;

            return INodeNoodleLabelActiveType.Selected;
        }

        private void CheckReferenceIsValid()
        {
            if (aiDecision == null)
                return;

            if (aiDecision.Graph != graph && aiDecision.Graph != null)
                aiDecision = null;
        }

    }
}