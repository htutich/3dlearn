using UnityEngine;


namespace learn3d
{
    public class PlayerBulletController : MonoBehaviour
    {
        #region Fields

        private Rigidbody _myRigidbody;
        private float _speed = 40.0f;
        private float _lifeTime = 5.0f;
        private int _minDamage = 10;
        private int _maxDamage = 40;
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

        private void OnCollisionEnter(Collision enemy)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                var enemyHealthManager = enemy.gameObject.GetComponent<EnemyHealthManager>();
                enemyHealthManager?.HurtEnemy(_damageToGive);

                var doorHealthManager = enemy.gameObject.GetComponent<DoorHealthManager>();
                doorHealthManager?.HurtEnemy(_damageToGive);
            }
            if (!enemy.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}
