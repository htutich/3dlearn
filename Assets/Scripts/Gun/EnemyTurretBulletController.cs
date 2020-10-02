using UnityEngine;

namespace learn3d
{
    public class EnemyTurretBulletController : MonoBehaviour
    {
        #region Fields

        private Rigidbody _myRigidbody;
        private float _speed = 15.0f;
        private float _lifeTime = 1.5f;
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
            var PlayerHealthManager = other.gameObject.GetComponent<PlayerHealthManager>();
            PlayerHealthManager?.HurtPlayer(_damageToGive);

            var DoorHealthManager = other.gameObject.GetComponent<DoorHealthManager>();
            DoorHealthManager?.HurtEnemy(_damageToGive);

            Destroy(gameObject);
        }

        #endregion
    }
}
