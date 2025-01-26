using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGoapAgent : MonoBehaviour 
{
    public AgentGoal CurrentGoal;
    public ActionPlan ActionPlan;
    public AgentAction CurrentAction;

    public Dictionary<string, AgentBelief> Beliefs;
    public HashSet<AgentAction> Actions;
    public HashSet<AgentGoal> Goals;
}
//EOF.