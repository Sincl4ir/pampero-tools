using System.Collections.Generic;
public class AgentAction 
{
    public string Name { get; }
    public float Cost { get; private set; }
    
    public HashSet<AgentBelief> Preconditions { get; } = new();
    public HashSet<AgentBelief> Effects { get; } = new();
    
    protected IActionStrategy _strategy;
    public bool Complete => _strategy.Completed;
    
    AgentAction(string name) 
    {
        Name = name;
    }

    public void Setup(GOAPAgentData data) => _strategy.Setup(data); 
    public void Start() => _strategy.Start();

    public void Update(float deltaTime) 
    {
        // Check if the action can be performed and update the strategy
        if (_strategy.CanPerform) 
        {
            _strategy.UpdateStrategy(deltaTime);
        }

        // Bail out if the strategy is still executing
        if (!_strategy.Completed) { return; }
        
        // Apply effects
        foreach (var effect in Effects) 
        {
            effect.Evaluate();
        }
    }
    
    public void Stop() => _strategy.Stop();

    #region Builder
    public class Builder {
        readonly AgentAction action;
        
        public Builder(string name) {
            action = new AgentAction(name) {
                Cost = 1
            };
        }
        
        public Builder WithCost(float cost) {
            action.Cost = cost;
            return this;
        }
        
        public Builder WithStrategy(IActionStrategy strategy) {
            action._strategy = strategy;
            return this;
        }
        
        public Builder AddPrecondition(AgentBelief precondition) {
            action.Preconditions.Add(precondition);
            return this;
        }
        
        public Builder AddEffect(AgentBelief effect) {
            action.Effects.Add(effect);
            return this;
        }
        
        public AgentAction Build() {
            return action;
        }
    }
    #endregion
}
//EOF.