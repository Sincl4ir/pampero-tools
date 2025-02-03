// TODO Migrate Strategies, Beliefs, Actions and Goals to Scriptable Objects and create Node Editor for them

public struct GOAPAgentData
{
    public BaseGoapAgentSO Agent { get; }

    public GOAPAgentData(BaseGoapAgentSO agent)
    {
        Agent = agent;
    }
}
//EOF.