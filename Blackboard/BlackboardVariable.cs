using System;
using UnityEngine;

namespace Pampero.Blackboard
{
    [Serializable]
    public class BlackboardVariable<T> : IBlackboardVariable
    {
        [SerializeField] protected T _value;

        public event Action<T> OnValueChanged;

        public T Value
        {
            get => _value;
            set
            {
                if (!Equals(_value, value))
                {
                    _value = value;
                    OnValueChanged?.Invoke(_value);
                }
            }
        }

        public BlackboardVariable(T initialValue = default) => _value = initialValue;
        public object GetValue() => _value;
        public void SetValue(object value) => Value = (T)value;
        public Type GetValueType() => typeof(T);
    }
}
//EOF.