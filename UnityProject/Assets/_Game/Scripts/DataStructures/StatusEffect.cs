using _Game.Enums;

namespace _Game.DataStructures
{
    public class StatusEffect
    {
        public StatusEffectType Type;
        public float DamagePerSecond;
        public float Duration;
    
        public StatusEffect(StatusEffectType type, float dps, float duration)
        {
            Type = type;
            DamagePerSecond = dps;
            Duration = duration;
        }

        public void ExtendDuration(float extraDuration)
        {
            Duration += extraDuration;
        }
    }
}