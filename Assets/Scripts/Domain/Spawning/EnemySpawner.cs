using UnityEngine;
using Dodger.Domain.Enemies;

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
        private Spawner<Enemy> _spawner;
        private Vector3 _randomPointInCameraBounds;
        private float _leftXCameraBound;
        private float _rightXCameraBound;

        private void GetCameraXBounds()
        {
            _leftXCameraBound = _camera.ViewportToWorldPoint(new Vector3(0, 0)).x;
            _rightXCameraBound = _camera.ViewportToWorldPoint(new Vector3(1, 0)).x;
        }
        private void Awake()
        {
            _spawner = new Spawner<Enemy>();
            _spawner.SetPrefab(_enemyPrefab);
            _spawner.SetParent(_enemiesParent);

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
            _spawner.SetSpawnPosition(_randomPointInCameraBounds);
            _spawner.Spawn();
        }

        private void Start()
        {
            InvokeRepeating("SpawnAtRandomPointInCameraBounds", 0, _spawnRepeatTimeInSeconds);
        }
    }
}
