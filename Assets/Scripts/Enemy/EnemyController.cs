using UnityEngine;


public class EnemyController : MonoBehaviour
{
    #region Fields

    private Rigidbody _myRigidbody;
    private GameObject _player;
    private float _speedMove = 0.6f;
    private float _playerCheckArea = 7.0f;
    private float _playerAttackArea = 1.0f;
    private float _attackTimer;
    private float _timeBetweenAttack = 0.2f;
    private float _minDamage = 10.0f;
    private float _maxDamage = 40.0f;
    private float _damageToGive;
    #endregion


    #region UnityMethods

    private void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
        _player = FindObjectOfType<PlayerController>().gameObject;
        _damageToGive = Random.Range(_minDamage, _maxDamage);
    }

    private void Update()
    {
        if (transform.position.y < 0.0f)
        {
            Destroy(gameObject);
        }

        var distanceToPlayer = (_player.transform.position - transform.position).sqrMagnitude;
        if (distanceToPlayer < _playerCheckArea * _playerCheckArea)
        {
            transform.LookAt(_player.transform.position);

            var vectorX = _player.transform.position.x - transform.position.x;
            var vectorZ = _player.transform.position.z - transform.position.z;
            var vectorMove = new Vector3(vectorX, 0.0f, vectorZ);
            vectorMove.Normalize();
            _myRigidbody.velocity = vectorMove * _speedMove;
        }

        if (distanceToPlayer < _playerAttackArea * _playerAttackArea)
        {
            if (_attackTimer > 0.0f)
            {
                _attackTimer -= Time.deltaTime;
            }

            if (_attackTimer <= 0.0f)
            {
                _attackTimer = _timeBetweenAttack;
                var PlayerHealthManager = _player.gameObject.GetComponent<PlayerHealthManager>();
                PlayerHealthManager?.HurtPlayer(_damageToGive);
            }
        }
    }

    #endregion
}

