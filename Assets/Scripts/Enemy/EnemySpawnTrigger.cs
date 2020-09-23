using UnityEngine;


public class EnemySpawnTrigger : MonoBehaviour
{
    #region PrivateData

    [SerializeField] private Transform EnemySpawnPoint;
    [SerializeField] private GameObject Enemy;

    #endregion


    #region OnTriggerEnter

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(Enemy, EnemySpawnPoint);
        }
    }

    #endregion
}
