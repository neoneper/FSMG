using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "FSMG/AI/Stats/Agent")]
public class AIAgentStats : ScriptableObject
{
    [System.Serializable]
    public class LookStats
    {
        public LayerMask layermask;
        public float lookRange = 40f;
        public float lookSphereCastRadius = 1f;
    }

    [System.Serializable]
    public class MoveStats
    {
        public float moveSpeed = 1;

    }

    [System.Serializable]
    public class AgentStats
    {
        public float searchDuration = 4f;
        public float searchingTurnSpeed = 120f;
        public float pathEndThreshold = 0.1f;
    }

    public AgentStats agent;
    public LookStats look;
    public MoveStats move;


}
