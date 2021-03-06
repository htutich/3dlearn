﻿using UnityEngine;

namespace learn3d
{
    public class BombController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _bombModel;
        [SerializeField] private GameObject _explosionBomb;
        private Rigidbody _myRigidbody;

        private float _forceExplosion = 10.0f;
        private float _radiusExplosion = 5.0f;
        private float _timeToDetonate = 5.0f;

        private int _minDamage = 40;
        private int _maxDamage = 80;
        private int _damage;

        private bool _isDetonate = false;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _myRigidbody = GetComponent<Rigidbody>();
            _myRigidbody.AddForce(transform.forward * 10.0f, ForceMode.Impulse);

            _damage = Random.Range(_minDamage, _maxDamage);
            Invoke(nameof(Detonate), _timeToDetonate);
        }

        private void Update()
        {
            if (_isDetonate)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider enemy)
        {
            if (enemy.gameObject.CompareTag("Enemy") && !_isDetonate)
            {
                Detonate();
            }
        }

        #endregion


        #region Methods

        private void Detonate()
        {
            _isDetonate = true;
            Instantiate(_explosionBomb, _bombModel.transform.position, _bombModel.transform.rotation);
            Destroy(_bombModel);

            Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusExplosion);

            foreach (var collider in colliders)
            {
                if (collider.gameObject.CompareTag("Enemy"))
                {
                    var enemyGameObject = collider.gameObject;
                    var enemyRigidBody = enemyGameObject?.GetComponent<Rigidbody>();

                    var enemyHealthManager = enemyGameObject.GetComponent<EnemyHealthManager>();
                    enemyHealthManager?.HurtEnemy(_damage);

                    var doorHealthManager = enemyGameObject.GetComponent<DoorHealthManager>();
                    doorHealthManager?.HurtEnemy(_damage);

                    var direction = enemyGameObject.transform.position - transform.position;
                    direction.Normalize();

                    if (enemyRigidBody != null)
                    {
                        enemyRigidBody.AddForce(direction * _forceExplosion * enemyRigidBody.mass, ForceMode.Impulse);
                    }
                }
            }
            
        }

        #endregion
    }
}
