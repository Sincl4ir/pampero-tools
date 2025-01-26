using UnityEngine;

namespace Pampero.Tools.Effects
{
    /// <summary>
    /// Abstract base class for effect properties that implements the IStrategy interface.
    /// </summary>
    public abstract class BaseEffectProperty : ScriptableObject, IEffectStrategy
    {
        /// <summary>
        /// Attempts to execute the effect property.
        /// </summary>
        /// <returns>True if the effect property was executed successfully; otherwise, false.</returns>
        public virtual bool TryToExecute(out bool success)
        {
            success = false;
            if (CanExecute())
            {
                Execute(out success);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the effect property can be executed.
        /// </summary>
        /// <returns>True if the effect property can be executed; otherwise, false.</returns>
        public virtual bool CanExecute() => true;

        /// <summary>
        /// Executes the effect property.
        /// </summary>
        public abstract void Execute(out bool success);
    }
}
//EOF.