using UnityEngine;
using Dodger.Domain.Enemies;
using Dodger.Domain.Pooling;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Dodger.Domain.Spawning
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Transform _enemiesParent;
        [Space]
        [SerializeField] private Camera _camera;
        [Space]
        [SerializeField, Min(0)] private float _spawnRepeatTimeInSeconds = 0.5f;
        private ObjectPool<Enemy> _spawner;
        private List<Enemy> _enemies;
        private Vector3 _randomPointInCameraBounds;
        private float _leftXCameraBound;
        private float _rightXCameraBound;
        private float _lowerYCameraBound;
        private float _lowerYScaledCameraBound;

        private void GetCameraXBounds()
        {
            _leftXCameraBound = _camera.ViewportToWorldPoint(new Vector3(0, 0)).x;
            _rightXCameraBound = _camera.ViewportToWorldPoint(new Vector3(1, 0)).x;
            _lowerYCameraBound = _camera.ViewportToWorldPoint(new Vector3(0, 0)).y;
            _lowerYScaledCameraBound = _lowerYCameraBound - _enemiesParent.transform.localScale.y;
        }
        private void Awake()
        {
            _spawner = new ObjectPool<Enemy>(_enemyPrefab, _enemiesParent);

            GetCameraXBounds();
        }

        private void GetRandomPointInCameraBounds()
        {
            float randomX = Random.Range(_leftXCameraBound, _rightXCameraBound);
            _randomPointInCameraBounds = new Vector3(randomX, transform.position.y);
        }

        private void SpawnAtRandomPointInCameraBounds()
        {
            GetRandomPointInCameraBounds();
            var newEnemy = _spawner.Unpool();
            _enemies.Add(newEnemy);
            newEnemy.transform.position = _randomPointInCameraBounds;
            // Pool enemy when enemy y is less than camera bounds y
        }

        private void Start()
        {
            InvokeRepeating("SpawnAtRandomPointInCameraBounds", 0, _spawnRepeatTimeInSeconds);
        }

        private void PoolOutOfCameraEnemies()
        {
            foreach (var enemy in _enemies)
            {
                if (enemy.transform.position.y < _lowerYScaledCameraBound)
                {
                    _spawner.Pool(enemy);
                }
            }
        }

        private void Update()
        {
            PoolOutOfCameraEnemies();
        }
    }
}
