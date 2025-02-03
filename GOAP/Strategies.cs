// TODO Migrate Strategies, Beliefs, Actions and Goals to Scriptable Objects and create Node Editor for them

public interface IActionStrategy {
    bool CanPerform { get; }
    bool Completed { get; }

    void Setup(GOAPAgentData data);
    void Start();
    void UpdateStrategy(float deltaTime);
    void Stop();
}
//EOF.