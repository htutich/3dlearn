using UnityEngine;

namespace learn3d
{
    public class PlayerGunShootController : MonoBehaviour
    {
        #region Fields
        [SerializeField] private Transform _ShootPoint;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private GameObject _bomb;
        private AudioSource _audioSource;

        private float _timeBetweenShots = 0.05f;
        private float _timeBetweenBombs = 1.5f;
        private float _shotTimer;
        private float _bombTimer;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            EventManager.StartListening("PlayerShoot", PlayerShoot);
            EventManager.StartListening("PlayerBomb", PlayerBomb);
        }

        private void OnDisable()
        {
            EventManager.StopListening("PlayerShoot", PlayerShoot);
            EventManager.StopListening("PlayerBomb", PlayerBomb);
        }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_shotTimer > 0.0f)
            {
                _shotTimer -= Time.deltaTime;
            }

            if (_bombTimer > 0.0f)
            {
                _bombTimer -= Time.deltaTime;
            }
        }

        #endregion


        #region Methods

        private void PlayerShoot(EventParam eventParam)
        {
            if (_shotTimer <= 0.0f)
            {
                _audioSource.Play();
                _shotTimer = _timeBetweenShots;
                Instantiate(_bullet, _ShootPoint.position, _ShootPoint.rotation);
            }
        }

        private void PlayerBomb(EventParam eventParam)
        {
            if (_bombTimer <= 0.0f)
            {
                _audioSource.Play();
                _bombTimer = _timeBetweenBombs;
                Instantiate(_bomb, _ShootPoint.position, _ShootPoint.rotation);
            }
        }

        #endregion
    }
}
