namespace Pampero.Tools.Effects
{
    public struct EffectData : IEffectData
    {
        public IEffectTarget EffectTarget { get; }
        public IEffectSource EffectSource { get; }
        
        public EffectData(IEffectTarget effectTarget, IEffectSource effectSource)
        {
            EffectTarget = effectTarget;
            EffectSource = effectSource;
        }
    }
}   
//EOF.