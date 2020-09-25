using UnityEngine;


public class EnemyTurretController : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform _turretHead;

    private Rigidbody _myRigidbody;
    private GameObject _player;
    private float _playerCheckArea = 3.5f;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
        _player = FindObjectOfType<PlayerController>().gameObject;
    }

    private void Update()
    {
        var distanceToPlayer = (_player.transform.position - transform.position).sqrMagnitude;
        if (distanceToPlayer < _playerCheckArea * _playerCheckArea)
        {
            _turretHead.LookAt(_player.transform.position);
        }
    }

    #endregion
}
