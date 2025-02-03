using Pampero.Tools.IdSO;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Agent Goal", menuName = "Pampero/GOAP/Goals")]
public class AgentGoalSO : ScriptableObjectWithId
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public float Priority { get; private set; }

    [SerializeField] private List<BaseAgentBeliefSO> _desiredEffects;

    public HashSet<BaseAgentBeliefSO> DesiredEffects { get; } = new();

    public void Setup(GOAPAgentData data)
    {
        PopulateBeliefHashSet(_desiredEffects, DesiredEffects, data);
    }

    protected void PopulateBeliefHashSet(List<BaseAgentBeliefSO> sourceList, HashSet<BaseAgentBeliefSO> destHashSet, GOAPAgentData data)
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
}
//EOF.