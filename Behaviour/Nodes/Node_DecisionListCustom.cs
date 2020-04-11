using XNode.FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace XNode.FSMG
{
    [CreateNodeMenu("Decisions/Custom List")]
    public class Node_DecisionListCustom : NodeBase_Decision
    {
        //usado somente para renomear o nó assim que é criado, para evitar o nome feio vindo do arquivo.cs
        private bool isAlreadyRename = false;

        [SerializeField]
        private List<AI_DecisionBase> aiDecisions;

        [Output(typeConstraint = TypeConstraint.Strict)]
        public NodeBase_Decision outDecision;

        protected override void Init()
        {
            //Renomeia o nó na primeira vez que o nó é iniciado, isto previne o nome feio original do .cs
            if (!isAlreadyRename)
                this.name = "Custom Decision List";

            if (aiDecisions == null)
                aiDecisions = new List<AI_DecisionBase>();
            isAlreadyRename = true;

            base.Init();
        }

        public override bool Execute(FSMBehaviour fsm)
        {
            bool result = !aiDecisions.Exists(r => r.Execute(fsm) == false);
            return result;
        }

        //Modificando as propriedades da label de conexões
        public override string GetNoodleLabel()
        {
            //Quero mostrar o nome da ação anexada ao nó caso ela exista
            string label = FSMGUtility.StringTag_Undefined;

            List<AI_DecisionBase> tmp = new List<AI_DecisionBase>(aiDecisions);
            tmp.RemoveAll(r => r == null);

            if (tmp.Count > 0)
            {
                label = "";
                foreach (AI_DecisionBase decision in tmp)
                {
                    label += decision.name + "\n";

                }
            }
            tmp.Clear();
            tmp = null;

            return label;

        }
        public override INodeNoodleLabelActiveType GetNoodleLabelActive()
        {
            //Esonde a label caso nao haja nenhuma ação referenciada

            if (aiDecisions.Count <= 0)
                return INodeNoodleLabelActiveType.Never;
            else return INodeNoodleLabelActiveType.SelectedPair;
        }

        private bool CheckReferenceIsValid(AI_DecisionBase aiDecision)
        {
            if (aiDecision == null)
                return false;

            if (aiDecision.Graph != graph && aiDecision.Graph != null)
                return false;

            return true;
        }

    }
}