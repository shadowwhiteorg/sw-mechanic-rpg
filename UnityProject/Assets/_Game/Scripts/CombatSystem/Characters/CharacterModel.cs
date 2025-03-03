using UnityEngine;
using UnityEngine.TextCore.Text;

namespace _Game.CombatSystem
{
    public class CharacterModel : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject characterModel;
        
        
        public void PlayHitAnimation()
        {
            animator.SetTrigger("Hit");
        }
        public void PlayAttackAnimation()
        {
            animator.SetTrigger("Attack");
        }
        public void PlayDeathAnimation()
        {
            animator.SetTrigger("Death");
        }
        
        public void PlayIdleAnimation()
        {
            animator.SetTrigger("Idle");
        }
        
        public void PlayWalkAnimation()
        {
            animator.SetTrigger("Walk");
        }
        
        
        public void SetAnimationTrigger(string trigger)
        {
            animator.SetTrigger(trigger);
        }
        
    }
}