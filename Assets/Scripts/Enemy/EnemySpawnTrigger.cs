using UnityEngine;


public class EnemySpawnTrigger : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform _enemySpawnPoint;
    [SerializeField] private GameObject _enemy;

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
                Instantiate(_enemy, _enemySpawnPoint);
            }
        }
    }

    #endregion
}
