using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pampero.Blackboard
{
    [Serializable]
    public class Blackboard
    {
        private readonly Dictionary<string, IBlackboardVariable> _variables = new();

        public void Set<T>(string key, T value)
        {
            if (_variables.TryGetValue(key, out var variable))
            {
                if (variable is BlackboardVariable<T> typed)
                {
                    typed.Value = value;
                }
                else
                {
                    Debug.LogError($"Blackboard variable '{key}' is not of type {typeof(T)}");
                }
            }
            else
            {
                _variables[key] = new BlackboardVariable<T>(value);
            }
        }

        public T Get<T>(string key)
        {
            if (_variables.TryGetValue(key, out var variable))
            {
                if (variable is BlackboardVariable<T> typed)
                {
                    return typed.Value;
                }
                else
                {
                    Debug.LogError($"Blackboard variable '{key}' is not of type {typeof(T)}");
                }
            }

            Debug.LogError($"Blackboard variable '{key}' not found");
            return default;
        }

        public bool TryGet<T>(string key, out T value)
        {
            value = default;
            if (_variables.TryGetValue(key, out var variable) && variable is BlackboardVariable<T> typed)
            {
                value = typed.Value;
                return true;
            }

            return false;
        }

        public bool HasKey(string key) => _variables.ContainsKey(key);
        public Dictionary<string, IBlackboardVariable> GetAllVariables() => _variables;
    }
}
//EOF.