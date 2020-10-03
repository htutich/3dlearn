using UnityEngine;

namespace learn3d
{
    public class BombController : MonoBehaviour
    {
        #region Fields

        private AudioSource _audioSource;
        private Rigidbody _myRigidbody;
        private float _forceExplosion = 10.0f;
        private float _radiusExplosion = 5.0f;
        private float _timeToDetonate = 5.0f;
        private int _minDamage = 40;
        private int _maxDamage = 80;
        private int _damage;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();

            _myRigidbody = GetComponent<Rigidbody>();
            _myRigidbody.AddForce(transform.forward * 10.0f, ForceMode.Impulse);

            _damage = Random.Range(_minDamage, _maxDamage);
            Invoke(nameof(Detonate), _timeToDetonate);
        }

        private void OnTriggerEnter(Collider enemy)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                Detonate();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            var EnemyHealthManager = other.gameObject.GetComponent<EnemyHealthManager>();
            EnemyHealthManager?.HurtEnemy(_damage);

            var DoorHealthManager = other.gameObject.GetComponent<DoorHealthManager>();
            DoorHealthManager?.HurtEnemy(_damage);
        }

        #endregion


        #region Methods


        private void Detonate()
        {
            _audioSource.Play();

            Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusExplosion);

            foreach (var collider in colliders)
            {
                if (collider.gameObject.CompareTag("Enemy"))
                {
                    var enemyGameObject = collider.gameObject;
                    var enemyRigidBody = enemyGameObject?.GetComponent<Rigidbody>();
                    var enemyHealthManager = enemyGameObject?.GetComponent<EnemyHealthManager>();
                    enemyHealthManager?.HurtEnemy(_damage);

                    var direction = enemyGameObject.transform.position - transform.position;
                    direction.Normalize();

                    if (enemyRigidBody != null)
                    {
                        enemyRigidBody.AddForce(direction * _forceExplosion * enemyRigidBody.mass, ForceMode.Impulse);
                    }
                    Destroy(gameObject);
                }
            }
        }

        #endregion
    }
}
