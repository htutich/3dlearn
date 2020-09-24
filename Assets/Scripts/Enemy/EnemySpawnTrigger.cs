using UnityEngine;


public class EnemySpawnTrigger : MonoBehaviour
{
    #region PrivateData

    [SerializeField] private Transform _enemySpawnPoint;
    [SerializeField] private GameObject _enemy;

    #endregion


    #region UnityMethods

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(_enemy, _enemySpawnPoint);
        }
    }

    #endregion
}
