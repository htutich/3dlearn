using UnityEngine;


public class GunController : MonoBehaviour
{
    #region PrivateData

    private float _timeBetweenShots = 0.3f;
    private float _shotCounter;
    private AudioSource _audioSource;
    [SerializeField] private Transform _ShootPoint;
    [SerializeField] private GameObject _bullet;

    #endregion


    #region UnityMethods

    private void OnEnable()
    {
        PlayerController.onPlayerShoot += PlayerShoot;
    }

    private void OnDisable()
    {
        PlayerController.onPlayerShoot -= PlayerShoot;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(_shotCounter > 0.0f)
        {
            _shotCounter -= Time.deltaTime;
        }
    }

    #endregion


    #region Methods

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
