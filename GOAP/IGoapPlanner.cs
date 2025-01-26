using System.Collections.Generic;

public interface IGoapPlanner 
{
    ActionPlan Plan(BaseGoapAgent agent, HashSet<AgentGoal> goals, AgentGoal mostRecentGoal = null);
}
