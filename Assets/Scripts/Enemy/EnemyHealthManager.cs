using UnityEngine;
using UnityEngine.UI;


namespace learn3d
{
    public class EnemyHealthManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _firstAidKit;
        private AudioSource _audioSource;

        private float _health = 100.0f;
        private float _currentHealth;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _currentHealth = _health;
        }

        #endregion


        #region Methods

        public void HurtEnemy(int damage)
        {
            _audioSource.Play();

            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Instantiate(_firstAidKit, new Vector3(transform.position.x, 0.0f, transform.position.z), transform.rotation);
                EventManager.TriggerEvent("EnemyDie");
                Destroy(gameObject);
            }
        }

        private float CalculateHealth()
        {
            return _currentHealth / _health;
        }

        #endregion
    }
}