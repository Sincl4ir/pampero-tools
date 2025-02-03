using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGoapAgentSO : MonoBehaviour
{
    public AgentGoalSO CurrentGoal;
    public ActionPlan ActionPlan;
    public AgentActionSO CurrentAction;

    public Dictionary<string, BaseAgentBeliefSO> Beliefs;
    public HashSet<AgentActionSO> Actions;
    public HashSet<AgentGoalSO> Goals;
}
//EOF.