using System.Collections.Generic;

public class ActionPlan 
{
    public AgentGoalSO AgentGoal { get; }
    public Stack<AgentActionSO> Actions { get; }
    public float TotalCost { get; set; }
    
    public ActionPlan(AgentGoalSO goal, Stack<AgentActionSO> actions, float totalCost) 
    {
        AgentGoal = goal;
        Actions = actions;
        TotalCost = totalCost;
    }
}
//EOF.