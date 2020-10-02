using UnityEngine;


namespace learn3d
{
    public class EnemySpawnTrigger : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform _enemySpawnPoint;
        [SerializeField] private GameObject _enemy;

        private float _minSpawnRadius = -3.0f;
        private float _maxSpawnRadius = 3.0f;
        private int _maxEnemyCount = 10;

        #endregion


        #region UnityMethods

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (_maxEnemyCount > 0)
                {
                    _maxEnemyCount--;
                    var random = Random.Range(_minSpawnRadius, _maxSpawnRadius);
                    var spawnPosition = new Vector3(_enemySpawnPoint.transform.position.x + random, _enemySpawnPoint.transform.position.y, _enemySpawnPoint.transform.position.z + random);
                    Instantiate(_enemy, spawnPosition, _enemySpawnPoint.rotation);
                }
            }
        }

        #endregion
    }
}
