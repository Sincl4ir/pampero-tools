using Pampero.Tools.IdSO;

public abstract class BaseAgentBeliefSO : ScriptableObjectWithId, IBelief
{
    public abstract void Setup(GOAPAgentData data);
    public abstract bool Evaluate();

}
//EOF.