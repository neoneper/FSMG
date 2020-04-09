using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XNode.FSMG.Components;

namespace XNode.FSMG
{
    [CreateNodeMenu("States/Actions/Action"), NodeTint("#0071E8")]
    public class Node_Action : NodeBase_Action
    {
        //usado somente para renomear o nó assim que é criado, para evitar o nome feio vindo do arquivo.cs
        private bool isAlreadyRename = false;

        [Output]
        public NodeBase_Action outAction = null;

        [SerializeField, NodeAIAction(true)]
        private AI_ActionBase action = null;


        protected override void Init()
        {
            //Renomeia o nó na primeira vez que o nó é iniciado, isto previne o nome feio original do .cs
            if (!isAlreadyRename)
                this.name = "Action";

            isAlreadyRename = true;

            base.Init();
        }
        public override void Execute(FSMBehaviour fsm)
        {
            if (action != null)
                action.Execute(fsm);

        }

        //Modificando as propriedades da label de conexões
        public override string GetNoodleLabel()
        {
            //Quero mostrar o nome da ação anexada ao nó caso ela exista
            string label = FSMGUtility.StringTag_Undefined;
            if (action != null) label = action.name;
            return label;

        }
        public override INodeNoodleLabelActiveType GetNoodleLabelActive()
        {
            //Esonde a label caso nao haja nenhuma ação referenciada

            if (action == null)
                return INodeNoodleLabelActiveType.Never;
            else return INodeNoodleLabelActiveType.SelectedPair;
        }

    }
}