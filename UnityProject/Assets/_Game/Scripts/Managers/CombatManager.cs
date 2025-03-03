using System;
using System.Collections.Generic;
using _Game.CombatSystem;
using _Game.Interfaces;
using _Game.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Managers
{
    public class CombatManager : Utils.Singleton<CombatManager>
    {
        [SerializeField] private EnemyCharacter enemyPrefab;
        [SerializeField] private int enemyCount;
        [SerializeField] private float spawnRadius;
        [SerializeField] private GameObject spawnCenter;
        
        private QuadTree<EnemyCharacter> _enemyQuadTree;
        private List<EnemyCharacter> _enemies = new List<EnemyCharacter>();

        public void InitializeCombat()
        {
            _enemyQuadTree = new QuadTree<EnemyCharacter>(new Rect(-50,-50,100,100), 4);
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            for (int i = 0; i < enemyCount; i++)
            {
                var enemy = Instantiate(enemyPrefab, spawnCenter.transform.position + new Vector3(Random.insideUnitSphere.x, 0, Random.insideUnitSphere.z) * spawnRadius, Quaternion.identity);
                enemy.transform.LookAt(GameManager.Instance.PlayerCharacter.transform);
                AddEnemy(enemy);
            }
        }
        

        private void OnEnable()
        {
            EventManager.OnSearchEnemies += UpdateAllEnemyPositions;
            EventManager.OnEnemyDeath += SpawnNewEnemy;
        }

        private void OnDisable()
        {
            EventManager.OnSearchEnemies -= UpdateAllEnemyPositions;
            EventManager.OnEnemyDeath -= SpawnNewEnemy;
        }
        
        public void AddEnemy(EnemyCharacter enemy)
        {
            _enemies.Add(enemy);
            _enemyQuadTree.Insert(enemy);
        }
        
        public void RemoveEnemy(EnemyCharacter enemy)
        {
            _enemies.Remove(enemy);
            _enemyQuadTree.Remove(enemy);
        }

        private void UpdateAllEnemyPositions()
        {
            _enemyQuadTree = new QuadTree<EnemyCharacter>(new Rect(-50, -50, 100, 100), 4);
            foreach (var enemy in _enemies)
            {
                _enemyQuadTree.Insert(enemy);
            }
        }

        public IDamageable FindNearestEnemy(Vector3 position, float searchRadius, EnemyCharacter exclude = null, List<EnemyCharacter> excludeList = null)
        {
            EventManager.FireOnEnemySearch();
            return _enemyQuadTree.FindNearest(position, searchRadius,exclude, excludeList);
        }

        private void SpawnNewEnemy()
        {
            var enemy = Instantiate(enemyPrefab, spawnCenter.transform.position + new Vector3(Random.insideUnitSphere.x, 0, Random.insideUnitSphere.z) * spawnRadius, Quaternion.identity);
            enemy.transform.LookAt(GameManager.Instance.PlayerCharacter.transform);
            AddEnemy(enemy);
        }
        
        
    }
}