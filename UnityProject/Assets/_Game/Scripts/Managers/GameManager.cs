using System;
using _Game.CombatSystem;
using _Game.Utils;
using UnityEngine;

namespace _Game.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public PlayerCharacter PlayerCharacter { get; private set; }

        private void Awake()
        {
            PlayerCharacter = FindObjectOfType<PlayerCharacter>();
        }

        private void Start()
        {
            SkillManager.Instance.InitializeSkills();
            CombatManager.Instance.InitializeCombat();
        }
    }
}
