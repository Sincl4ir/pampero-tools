using UnityEngine;

namespace Pampero.Tools.Effects
{
    /// <summary>
    /// Abstract generic base class for effect properties that implements the IStrategy interface
    /// with a generic type parameter.
    /// </summary>
    /// <typeparam name="T">The type of data used by the effect property.</typeparam>
    public abstract class EffectProperty<T> : ScriptableObject, ITypedEffectStrategy<T> where T : struct
        {
            /// <summary>
            /// Attempts to execute the effect property with the provided data.
            /// </summary>
            /// <param name="data">The data needed for the effect property execution.</param>
            /// <returns>True if the effect property was executed successfully; otherwise, false.</returns>
            public virtual bool TryToExecute(ref T data)
            {
                if (CanExecute(ref data))
                {
                    Execute(ref data);
                    return true;
                }

                return false;
            }

            /// <summary>
            /// Checks if the effect property can be executed with the provided data.
            /// </summary>
            /// <param name="data">The data to check for effect property execution eligibility.</param>
            /// <returns>True if the effect property can be executed; otherwise, false.</returns>
            public virtual bool CanExecute(ref T data) => true;

            /// <summary>
            /// Executes the effect property with the provided data.
            /// </summary>
            /// <param name="data">The data needed for the effect property execution.</param>
            public abstract void Execute(ref T data);
        }
    }
//EOF.