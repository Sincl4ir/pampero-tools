using UnityEngine;

public abstract class BaseStrategy : ScriptableObject, IActionStrategy
{
    protected bool _completed;
    public abstract bool CanPerform { get; }
    public abstract bool Completed { get; protected set; }

    public abstract void Setup(GOAPAgentData data);
    public abstract void Start();
    public abstract void Stop();
    public abstract void UpdateStrategy(float deltaTime);
}
//EOF.