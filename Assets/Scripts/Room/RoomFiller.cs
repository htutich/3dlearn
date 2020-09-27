using System.Collections;
using UnityEngine;



public class RoomFiller : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform _roomCenter;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _enemyTurret;
    [SerializeField] private GameObject _wall;

    private int _minTurretCount = 1;
    private int _maxTurretCount = 5;

    private int _minEnemyCount = 1;
    private int _maxEnemyCount = 5;

    private float _minRadiusSpawn = -4.5f;
    private float _maxRadiusSpawn = 4.5f;

    #endregion


    #region UnityMethods

    private IEnumerator Start()
    {
        var turretCount = Random.Range(_minTurretCount, _maxTurretCount);

        for (int i = 0; i < turretCount; i++)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            var random = Random.Range(_minRadiusSpawn, _maxRadiusSpawn);
            var spawnPosition = new Vector3(_roomCenter.transform.position.x + random, _roomCenter.transform.position.y, _roomCenter.transform.position.z + random);
            Instantiate(_enemyTurret, spawnPosition, _roomCenter.rotation);
        }

        var enemyCount = Random.Range(_minEnemyCount, _maxEnemyCount);

        for (int i = 0; i < enemyCount; i++)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            var random = Random.Range(_minRadiusSpawn, _maxRadiusSpawn);
            var spawnPosition = new Vector3(_roomCenter.transform.position.x + random, _roomCenter.transform.position.y + 0.5f, _roomCenter.transform.position.z + random);
            Instantiate(_enemy, spawnPosition, _roomCenter.rotation);
        }
    }

    #endregion
}
