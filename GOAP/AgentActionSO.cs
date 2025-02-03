using Pampero.Tools.IdSO;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AgentActionSO", menuName = "Pampero/GOAP/Actions/Agent Action")]
public class AgentActionSO : ScriptableObjectWithId
{
    [SerializeField] public string Name;
    [SerializeField] public float Cost = 1;
    [SerializeField] public List<BaseAgentBeliefSO> PreconditionsList;
    [SerializeField] public List<BaseAgentBeliefSO> EffectsList;
    [SerializeField] private BaseStrategy _strategy;

    public HashSet<BaseAgentBeliefSO> Preconditions { get; private set; } = new HashSet<BaseAgentBeliefSO>();
    public HashSet<BaseAgentBeliefSO> Effects { get; private set; } = new HashSet<BaseAgentBeliefSO>();

    //protected IActionStrategy _strategy;
    public bool Complete => _strategy.Completed;

    public void Setup(GOAPAgentData data)
    {
        PopulateBeliefHashSet(PreconditionsList, Preconditions, data);
        PopulateBeliefHashSet(EffectsList, Effects, data);
        _strategy.Setup(data);
    }

    private void PopulateBeliefHashSet(List<BaseAgentBeliefSO> sourceList, HashSet<BaseAgentBeliefSO> destHashSet, GOAPAgentData data)
    {
        if (sourceList is null || sourceList.Count <= 0) { return; }
        foreach (var belief in sourceList)
        {
            if (belief is null) { continue; }

            var beliefInstance = Instantiate(belief);
            beliefInstance.Setup(data);
            destHashSet.Add(beliefInstance);
        }
    }

    public void Start() => _strategy.Start();

    public void UpdateAction(float deltaTime)
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
}
//EOF.