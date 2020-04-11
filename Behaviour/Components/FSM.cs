using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using XNode.FSMG;

namespace XNode.FSMG.Components
{
    [AddComponentMenu("FSM/Controller")]
    public class FSM : FSMBehaviour
    {
        private void Start()
        {
            if (graph != null)
            {
                graph.InitGraph(this);
                isReady = true;
            }
        }

        private void Update()
        {
            if (!isReady) return;
            graph.UpdateGraph(this);
        }



    }
}