using UnityEngine;

namespace learn3d
{
    public class PlayerGunShootController : MonoBehaviour
    {
        #region Fields
        [SerializeField] private Transform _ShootPoint;
        [SerializeField] private GameObject _bullet;
        private AudioSource _audioSource;

        private float _timeBetweenShots = 0.1f;
        private float _shotCounter;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            EventManager.StartListening("PlayerShoot", PlayerShoot);
        }

        private void OnDisable()
        {
            EventManager.StopListening("PlayerShoot", PlayerShoot);
        }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_shotCounter > 0.0f)
            {
                _shotCounter -= Time.deltaTime;
            }
        }

        #endregion


        #region Methods

        private void PlayerShoot(EventParam eventParam)
        {
            if (_shotCounter <= 0.0f)
            {
                _audioSource.Play();
                _shotCounter = _timeBetweenShots;
                Instantiate(_bullet, _ShootPoint.position, _ShootPoint.rotation);
            }
        }

        #endregion
    }
}
