using System.Collections.Generic;

public interface IGoapPlanner 
{
    ActionPlan Plan(BaseGoapAgentSO agent, HashSet<AgentGoalSO> goals, AgentGoalSO mostRecentGoal = null);
}
