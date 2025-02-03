using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoapPlanner : IGoapPlanner 
{
    public ActionPlan Plan(BaseGoapAgentSO agent, HashSet<AgentGoalSO> goals, AgentGoalSO mostRecentGoal = null)
    {
        // Order goals by priority, descending
        if (goals is null) 
        {
            Debug.Log("GOALS CAN NOT BE NULL");
            return null; 
        } 

        List<AgentGoalSO> orderedGoals = goals
            .Where(g => g.DesiredEffects.Any(b => !b.Evaluate()))
            .OrderByDescending(g => g == mostRecentGoal ? g.Priority - 0.01 : g.Priority)
            .ToList();

        Debug.Log("Ordered goals by priority: ");
        for (int i = 0; i < orderedGoals.Count; i++)
        {
            Debug.Log($"{orderedGoals[i].Name} with priority {orderedGoals[i].Priority}");
        }
        
        // Try to solve each goal in order
        foreach (var goal in orderedGoals) 
        {
            Debug.Log($"Evaluating goal {goal.Name}");
            Node goalNode = new Node(null, null, goal.DesiredEffects, 0);
            
            // If we can find a path to the goal, return the plan
            if (FindPath(goalNode, agent.Actions)) 
            {
                // If the goalNode has no leaves and no action to perform try a different goal
                if (goalNode.IsLeafDead) 
                {
                    Debug.Log("Leaf is dead...");
                    continue; 
                }
                
                Stack<AgentActionSO> actionStack = new Stack<AgentActionSO>();
                while (goalNode.Leaves.Count > 0) 
                {
                    var cheapestLeaf = goalNode.Leaves.OrderBy(leaf => leaf.Cost).First();
                    goalNode = cheapestLeaf;
                    actionStack.Push(cheapestLeaf.Action);
                }

                Debug.Log($"Returning new action plan {goal.Name}, action stack count: {actionStack.Count}, cost: {goalNode.Cost}");
                foreach(var action in actionStack)
                {
                    Debug.Log($"{action.Name}");
                }
                return new ActionPlan(goal, actionStack, goalNode.Cost);
            }
        }
        
        Debug.LogWarning("No plan found");
        return null;
    }

    // TODO: Consider a more powerful search algorithm like A* or D*
    private bool FindPath(Node parent, HashSet<AgentActionSO> actions) 
    {
        // Order actions by cost, ascending
        var orderedActions = actions.OrderBy(a => a.Cost);
        if (parent.Action is not null)
        {
            Debug.Log($"Checking path for Action {parent.Action.Name}");
        }
        Debug.Log($"Trying to find Path. Ordered actions by cost. Ordered actions amount: {actions.OrderBy(a => a.Cost).ToList().Count}...");
        foreach (var action in orderedActions) 
        {
            Debug.Log($"Checking action {action.Name}");
            var requiredEffects = parent.RequiredEffects;
            
            // Remove any effects that evaluate to true, there is no action to take
            requiredEffects.RemoveWhere(b => b.Evaluate());
            
            // If there are no required effects to fulfill, we have a plan
            if (requiredEffects.Count == 0) 
            {
                Debug.Log("There are no required effect to fulfill");
                return true;
            }

            if (action.Effects.Any(requiredEffects.Contains)) 
            {
                var newRequiredEffects = new HashSet<BaseAgentBeliefSO>(requiredEffects);
                newRequiredEffects.ExceptWith(action.Effects);
                newRequiredEffects.UnionWith(action.Preconditions);
                
                var newAvailableActions = new HashSet<AgentActionSO>(actions);
                newAvailableActions.Remove(action);
                
                var newNode = new Node(parent, action, newRequiredEffects, parent.Cost + action.Cost);
                
                // Explore the new node recursively
                if (FindPath(newNode, newAvailableActions)) 
                {
                    parent.Leaves.Add(newNode);
                    newRequiredEffects.ExceptWith(newNode.Action.Preconditions);
                }

                // If all effects at this depth have been satisfied, return true
                if (newRequiredEffects.Count == 0) { return true; }
            }
        }
        
        return parent.Leaves.Count > 0;
    }
}
//EOF.