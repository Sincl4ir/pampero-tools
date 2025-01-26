using UnityEngine;

namespace Pampero.Tools.Effects
{
    public abstract class EffectProperty : TypedEffectProperty<IEffectData> { }

    public interface IEffectData
    {
        IEffectTarget EffectTarget { get; }
        IEffectSource EffectSource { get; }
    }

    public interface IEffectTarget
    {
        Transform TargetTr { get; }
    }

    public interface IEffectSource
    {
        Transform SourceTr { get; }
    }
}   
//EOF.