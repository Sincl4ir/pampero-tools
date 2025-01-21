namespace Pampero.Tools.Effects
{
    /// <summary>
    /// Defines a strategy pattern interface for executing actions without any data.
    /// </summary>
    public interface IEffectStrategy
    {
        /// <summary>
        /// Executes the strategy.
        /// </summary>
        public void Execute();

        /// <summary>
        /// Checks if the strategy can be executed.
        /// </summary>
        /// <returns>True if the strategy can be executed; otherwise, false.</returns>
        public bool CanExecute();

        /// <summary>
        /// Attempts to execute the strategy.
        /// </summary>
        /// <returns>True if the strategy was executed successfully; otherwise, false.</returns>
        public bool TryToExecute();
    }
}
//EOF.