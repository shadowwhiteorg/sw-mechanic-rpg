using _Game.Enums;

namespace _Game.DataStructures
{
    public class StatModifier
    {
        public float Value;
        public ModifierType Type;
        public bool IsTemporary;
        public float Duration;
    
        public StatModifier(float value, ModifierType type,  float duration = 0)
        {
            Value = value;
            Type = type;
            IsTemporary =  !(duration > 0);
            Duration = duration;
        }
    }
}