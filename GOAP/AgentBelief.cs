using System;
using UnityEngine;

public class AgentBelief : IBelief
{
    protected Func<bool> _condition = () => false;
    //protected Func<Vector3> _observedLocation = () => Vector3.zero;
    
    public string Name { get; }
    //public Vector3 Location => _observedLocation();

    public void Setup(GOAPAgentData data) { }
    AgentBelief(string name) 
    {
        Name = name;
    }
    
    public bool Evaluate() => _condition();

    #region Builder
    public class Builder 
    {
        readonly AgentBelief belief;
        
        public Builder(string name) 
        {
            belief = new AgentBelief(name);
        }
        
        public Builder WithCondition(Func<bool> condition) 
        {
            belief._condition = condition;
            return this;
        }
        
        public Builder WithLocation(Func<Vector3> observedLocation) 
        {
            //belief._observedLocation = observedLocation;
            return this;
        }
        
        public AgentBelief Build() 
        {
            return belief;
        }
    }
    #endregion
}
//EOF.