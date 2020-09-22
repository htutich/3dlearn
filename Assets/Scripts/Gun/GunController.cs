using System;
using UnityEngine;

public class GunController : MonoBehaviour
{

    private float _timeBetweenShots = 0.1f;
    private float _shotCounter;
    private float _bulletSpeed = 2.0f;
    [SerializeField] private Transform _ShootPoint;
    [SerializeField] private GameObject _bullet;

    private void Start()
    {
        PlayerController.PlayerShoot += onPlayerShoot;
    }

    private void Update()
    {
        if(_shotCounter > 0.0f)
        {
            _shotCounter -= Time.deltaTime;
        }
    }

    public void onPlayerShoot()
    {
        if (_shotCounter <= 0.0f)
        {
            _shotCounter = _timeBetweenShots;
            Instantiate(_bullet, _ShootPoint.position, _ShootPoint.rotation);
        }
    }

}
