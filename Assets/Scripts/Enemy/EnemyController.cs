using UnityEngine;


namespace learn3d
{
    public class EnemyController : MonoBehaviour
    {
        #region Fields

        private Rigidbody _myRigidbody;
        private GameObject _player;
        private Vector3 _startPoint;
        private float _speedMove = 0.6f;
        private float _playerCheckArea = 5.0f;
        private float _playerAttackArea = 1.0f;
        private float _attackTimer;
        private float _timeBetweenAttack = 0.2f;
        private float _minDamage = 10.0f;
        private float _maxDamage = 40.0f;
        private float _damageToGive;
        private float _timeToSpawnReturn = 2.0f;
        private bool _isWallkToSpawnPoint = false;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _myRigidbody = GetComponent<Rigidbody>();
            //_player = FindObjectOfType<PlayerController>().gameObject;
            _player = FindObjectOfType<PlayerAnimationMovement>().gameObject;
            _damageToGive = Random.Range(_minDamage, _maxDamage);
            _startPoint = transform.position;
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

                transform.LookAt(new Vector3(_player.transform.position.x, _player.transform.position.y + 0.5f, _player.transform.position.z));

                var vectorX = _player.transform.position.x - transform.position.x;
                var vectorZ = _player.transform.position.z - transform.position.z;
                var vectorMove = new Vector3(vectorX, 0.0f, vectorZ);
                vectorMove.Normalize();
                _myRigidbody.velocity = vectorMove * _speedMove;
            }

            if (distanceToPlayer > _playerCheckArea * _playerCheckArea)
            {
                if (!_isWallkToSpawnPoint && _timeToSpawnReturn > 0.0f)
                {
                    _isWallkToSpawnPoint = true;
                    _timeToSpawnReturn -= Time.deltaTime;
                }
                else
                {
                    GoToSpawnPoint();
                }
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


        #region Methods

        private void GoToSpawnPoint()
        {

            var distanceToSpawnPoint = (_startPoint - transform.position).sqrMagnitude;
            if (distanceToSpawnPoint <= 0.1f)
            {
                _isWallkToSpawnPoint = false;
                _timeToSpawnReturn = 2.0f;
            }
            else
            {
                transform.LookAt(_startPoint);

                var vectorX = _startPoint.x - transform.position.x;
                var vectorZ = _startPoint.z - transform.position.z;
                var vectorMove = new Vector3(vectorX, 0.0f, vectorZ);
                vectorMove.Normalize();
                _myRigidbody.velocity = vectorMove * _speedMove;
            }
        }

        #endregion
    }
}


