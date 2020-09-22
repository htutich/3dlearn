using UnityEngine;


public class GunController : MonoBehaviour
{

    #region PrivateData

    private float _timeBetweenShots = 0.1f;
    private float _shotCounter;
    private float _bulletSpeed = 2.0f;
    [SerializeField] private Transform _ShootPoint;
    [SerializeField] private GameObject _bullet;

    #endregion


    #region OnEnable

    private void OnEnable()
    {
        PlayerController.PlayerShoot += onPlayerShoot;
    }

    #endregion


    #region OnEnable

    private void OnDisable()
    {
        PlayerController.PlayerShoot -= onPlayerShoot;
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

    public void onPlayerShoot()
    {
        if (_shotCounter <= 0.0f)
        {
            _shotCounter = _timeBetweenShots;
            Instantiate(_bullet, _ShootPoint.position, _ShootPoint.rotation);
        }
    }

    #endregion

}
