using System.Collections.Generic;

public class Node 
{
    public Node Parent { get; }
    public AgentActionSO Action { get; }
    public HashSet<BaseAgentBeliefSO> RequiredEffects { get; }
    public List<Node> Leaves { get; }
    public float Cost { get; }
    
    public bool IsLeafDead => Leaves.Count == 0 && Action == null;
    
    public Node(Node parent, AgentActionSO action, HashSet<BaseAgentBeliefSO> effects, float cost) {
        Parent = parent;
        Action = action;
        RequiredEffects = new HashSet<BaseAgentBeliefSO>(effects);
        Leaves = new List<Node>();
        Cost = cost;
    }
}
//EOF.