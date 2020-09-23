using UnityEngine;


public class GunController : MonoBehaviour
{
    #region PrivateData

    private float _timeBetweenShots = 0.3f;
    private float _shotCounter;
    private float _bulletSpeed = 2.0f;
    
    private AudioSource _audioSource;
    [SerializeField] private Transform _ShootPoint;
    [SerializeField] private GameObject _bullet;

    #endregion


    #region OnEnable

    private void OnEnable()
    {
        PlayerController.onPlayerShoot += PlayerShoot;
    }

    #endregion


    #region OnDisable

    private void OnDisable()
    {
        PlayerController.onPlayerShoot -= PlayerShoot;
    }

    #endregion


    #region Start

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    #endregion


    #region Update

    private void Update()
    {
        if(_shotCounter > 0.0f)
        {
            _shotCounter -= Time.deltaTime;
        }
    }

    #endregion


    #region onPlayerShoot

    private void PlayerShoot()
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
