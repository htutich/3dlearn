﻿using UnityEngine;


public class EnemyTurretShootController : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform _firstShootPoint;
    [SerializeField] private Transform _secondShootPoint;
    [SerializeField] private GameObject _bullet;
    private AudioSource _audioSource;

    private string _uniqueID;
    private float _timeBetweenShots = 0.3f;
    private float _timeBetweenCanonShots = 0.15f;
    private float _firstShotCounter = 0.0f;
    private float _secondShotCounter = 0.0f;

    #endregion

    public string UniqueID
    {
        set
        {
           _uniqueID = value;
        }
    }

    #region UnityMethods

    private void OnEnable()
    {
        EventManager.StartListening("TurretShoot", TurretShoot);
    }

    private void OnDisable()
    {
        EventManager.StopListening("TurretShoot", TurretShoot);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_firstShotCounter > 0.0f)
        {
            _firstShotCounter -= Time.deltaTime;
        }
        if (_secondShotCounter > 0.0f)
        {
            _secondShotCounter -= Time.deltaTime;
        }
    }

    #endregion


    #region Methods

    private void TurretShoot(EventParam eventParam)
    { 
        if(eventParam.uniqueID == _uniqueID)
        {
            if (_secondShotCounter <= 0.0f)
            {
                _audioSource.Play();
                _secondShotCounter = _timeBetweenShots - _timeBetweenCanonShots;
                Instantiate(_bullet, _secondShootPoint.position, _secondShootPoint.rotation);
            }

            if (_firstShotCounter <= 0.0f)
            {
                _audioSource.Play();
                _firstShotCounter = _timeBetweenShots;
                Instantiate(_bullet, _firstShootPoint.position, _firstShootPoint.rotation);
            }
        }
    }

    #endregion
}
