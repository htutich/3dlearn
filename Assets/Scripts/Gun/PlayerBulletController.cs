using UnityEngine;


namespace learn3d
{
    public class PlayerBulletController : MonoBehaviour
    {
        #region Fields

        private Rigidbody _myRigidbody;
        private float _speed = 20.0f;
        private float _lifeTime = 2.0f;
        private int _minDamage = 10;
        private int _maxDamage = 20;
        private int _damageToGive;

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
            var EnemyHealthManager = other.gameObject.GetComponent<EnemyHealthManager>();
            EnemyHealthManager?.HurtEnemy(_damageToGive);

            var DoorHealthManager = other.gameObject.GetComponent<DoorHealthManager>();
            DoorHealthManager?.HurtEnemy(_damageToGive);

            Destroy(gameObject);
        }

        #endregion
    }
}
