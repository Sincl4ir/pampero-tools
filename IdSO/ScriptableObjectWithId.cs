using System;
using UnityEngine;

namespace Pampero.Tools.IdSO
{
    /// <summary>
    /// Base class for ScriptableObjects with an ID.
    /// </summary>
    public class ScriptableObjectWithId : ScriptableObject, IEquatable<ScriptableObjectWithId>
    {
        /// <summary>
        /// The ID of the ScriptableObject.
        /// </summary>
        [Tooltip("The ID of the ScriptableObject.")]
        [field: ScriptableObjectId][field: SerializeField] public string Id { get; private set; }

        protected void OnEnable()
        {
            RegisterWithTracker();
        }

        private void RegisterWithTracker()
        {
            //I won't import it yet since I'm not able to serialize dictionaries. 
            //SOWithIdTracker.Instance.Register(this);
        }

        /// <summary>
        /// Checks if the current ScriptableObject is equal to another ScriptableObject.
        /// </summary>
        /// <param name="other">The other ScriptableObject to compare with the current ScriptableObject.</param>
        /// <returns>true if the current ScriptableObject is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(ScriptableObjectWithId other)
        {
            if (ReferenceEquals(null, other)) { return false; }
            if (ReferenceEquals(this, other)) {return true; }

            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) { return false; }
            if (ReferenceEquals(this, obj)) { return true; }
            if (obj.GetType() != GetType()) { return false; }

            return Equals((ScriptableObjectWithId)obj);
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return HashCode.Combine(Id);
        }

        public static bool operator ==(ScriptableObjectWithId left, ScriptableObjectWithId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ScriptableObjectWithId left, ScriptableObjectWithId right)
        {
            return !Equals(left, right);
        }
    }
}
//EOF.