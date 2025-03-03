using _Game.CombatSystem;

namespace _Game.Interfaces
{
    public interface ICharacterState
    {
        void EnterState(BaseCharacter character);
        void UpdateState(BaseCharacter character);
        void ExitState(BaseCharacter character);
    }
}