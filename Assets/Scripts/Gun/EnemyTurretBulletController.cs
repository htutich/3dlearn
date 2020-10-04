using UnityEngine;

namespace learn3d
{
    public class EnemyTurretBulletController : MonoBehaviour
    {
        #region Fields

        private Rigidbody _myRigidbody;
        private float _speed = 20.0f;
        private float _lifeTime = 2.0f;
        private float _minDamage = 10.0f;
        private float _maxDamage = 40.0f;
        private float _damageToGive;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _myRigidbody = GetComponent<Rigidbody>();
            _myRigidbody.AddForce(transform.forward * _speed, ForceMode.Impulse);
            _damageToGive = Random.Range(_minDamage, _maxDamage);
            Destroy(gameObject, _lifeTime);
        }

        private void OnCollisionEnter(Collision other)
        {
            var playerHealthManager = other.gameObject.GetComponent<PlayerHealthManager>();
            playerHealthManager?.HurtPlayer(_damageToGive);

            var dorHealthManager = other.gameObject.GetComponent<DoorHealthManager>();
            dorHealthManager?.HurtEnemy(_damageToGive);

            Destroy(gameObject);
        }

        #endregion
    }
}
