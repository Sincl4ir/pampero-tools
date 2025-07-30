using System;

namespace Pampero.Blackboard
{
    public interface IBlackboardVariable
    {
        object GetValue();
        void SetValue(object value);
        Type GetValueType();
    }
}
//EOF.