using System.Collections.Generic;

public class Node 
{
    public Node Parent { get; }
    public AgentAction Action { get; }
    public HashSet<AgentBelief> RequiredEffects { get; }
    public List<Node> Leaves { get; }
    public float Cost { get; }
    
    public bool IsLeafDead => Leaves.Count == 0 && Action == null;
    
    public Node(Node parent, AgentAction action, HashSet<AgentBelief> effects, float cost) {
        Parent = parent;
        Action = action;
        RequiredEffects = new HashSet<AgentBelief>(effects);
        Leaves = new List<Node>();
        Cost = cost;
    }
}
//EOF.